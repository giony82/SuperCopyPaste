using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KGySoft.ComponentModel;
using KGySoft.CoreLibraries;
using SuperCopyPaste.Constants;
using SuperCopyPaste.Interfaces;
using SuperCopyPaste.Models;
using TomsToolbox.Essentials;
using CollectionExtensions = TomsToolbox.Essentials.CollectionExtensions;

namespace SuperCopyPaste.Core
{
    public class ClipboardDataManagement
    {
        private readonly SortableBindingList<ClipboardItemModel> _clipboardItems;
        private readonly IClipboardStorage _clipboardStorage;

        public event EventHandler<string> Error;

        private FilterCriteriaModel _filterCriteria = new FilterCriteriaModel();

        private DateTime? _lastTimeCopyPasted;

        public ClipboardDataManagement(IClipboardStorage clipboardStorage)
        {
            _clipboardStorage = clipboardStorage;
            _clipboardItems = new SortableBindingList<ClipboardItemModel>();
        }

        public SortableBindingList<ClipboardItemModel> DataSource { get; } =
            new SortableBindingList<ClipboardItemModel>();

        public event EventHandler<(int displyed, int total)> CountChanged;

        private void Add(ClipboardItemModel item)
        {
            if (string.IsNullOrWhiteSpace(item.Data.Text) && item.Data.Image == null) return;

            KeepListMaxAllowedLength();

            _clipboardItems.Add(item);

            Filter(_filterCriteria);
        }

        private void KeepListMaxAllowedLength()
        {
            if (_clipboardItems.Count > ClipboardConstants.MaxClipboardRecords)
            {
                ClipboardItemModel oldestClipboard = _clipboardItems.OrderByDescending(x => x.Created).First();
                _clipboardItems.Remove(oldestClipboard);
            }
        }

        public void Load()
        {
            try
            {
                var clipboardItemsList = _clipboardStorage.Read<List<ClipboardItemModel>>()
                    .OrderByDescending(x => x.Created)
                    .ToList();

                if (!clipboardItemsList.Any())
                {
                    AddDefaultItem(clipboardItemsList);
                }

                PopulateLists(clipboardItemsList);
            }
            catch (Exception err)
            {
                Trace.WriteLine(err);
                OnError(err.Message);
            }
        }

        private static void AddDefaultItem(ICollection<ClipboardItemModel> clipboardItemsList)
        {
            clipboardItemsList.Add(new ClipboardItemModel
            {
                Created = DateTime.Now,
                Id = Guid.NewGuid(),
                Data = new ClipboardDataModel
                {
                    Text =
                        "No clipboard items are available. This is an example of a text shown here when you copy text into clipboard. To send this text to the first foreground application, just press ENTER, or double click it with the mouse."
                }
            });
        }

        private void PopulateLists(IReadOnlyCollection<ClipboardItemModel> clipboardItemsList)
        {
            CollectionExtensions.AddRange(_clipboardItems, clipboardItemsList);
            CollectionExtensions.AddRange(DataSource, clipboardItemsList.Take(ClipboardConstants.MaxResultsCount));
            OnCountChanged(DataSource.Count, _clipboardItems.Count);
        }

        public void Save()
        {
            _clipboardStorage.Write(_clipboardItems.ToList());
        }

        public void Delete(ClipboardItemModel item)
        {
            if (item == null || item.Pinned) return;
            _clipboardItems.Remove(item);
            DataSource.Remove(item);
            OnCountChanged(_clipboardItems.Count, _clipboardItems.Count);
        }

        public void Filter(FilterCriteriaModel criteria)
        {
            _filterCriteria = criteria;

            bool hasText = !string.IsNullOrWhiteSpace(_filterCriteria.Text);
            var items = (from clipboardItem in _clipboardItems
                    let isTextMatched = !hasText || clipboardItem.Data.ClipboardType == ClipboardType.Text &&
                        clipboardItem.Data.Text.Contains(_filterCriteria.Text,
                            StringComparison.InvariantCultureIgnoreCase)
                    let isImageOk = clipboardItem.Data.ClipboardType == ClipboardType.Image && !hasText ||
                                    isTextMatched
                    let isPinnedMatch = clipboardItem.Pinned == _filterCriteria.Pinned || !_filterCriteria.Pinned
                    where isTextMatched && isImageOk && isPinnedMatch
                    select clipboardItem).ToList().OrderByDescending(x => x.Created)
                .Take(ClipboardConstants.MaxResultsCount);

            DataSource.Clear();
            CollectionExtensions.AddRange(DataSource, items);
            OnCountChanged(DataSource.Count, _clipboardItems.Count);
        }

        public void Clear()
        {
            _clipboardItems.Clear();
            DataSource.Clear();
            OnCountChanged(DataSource.Count, _clipboardItems.Count);
            Save();
        }

        public void DeleteItems(int daysOlder)
        {
            _clipboardItems.RemoveWhere(x => (DateTime.Now - x.Created).TotalDays >= daysOlder && !x.Pinned);
            DataSource.RemoveWhere(x => (DateTime.Now - x.Created).TotalDays >= daysOlder && !x.Pinned);
            Filter(_filterCriteria);
        }

        protected virtual void OnCountChanged(int displayedCount, int total)
        {
            CountChanged?.Invoke(this, (displayedCount, total));
        }

        public void AddClipboardData(IDataObject dataObject)
        {
            //When capturing images with CTRL SHIFT S, the event is triggered twice(same image).
            bool isDuplicated = _lastTimeCopyPasted != null &&
                                (DateTime.Now - _lastTimeCopyPasted.Value).TotalMilliseconds <
                                ClipboardConstants.ImageCopyPasteToleranceMs;

            if (dataObject == null || isDuplicated) return;

            _lastTimeCopyPasted = DateTime.Now;

            var clipboardItem = new ClipboardItemModel();

            if (dataObject.GetDataPresent(DataFormats.Bitmap))
            {
                clipboardItem.Data = new ClipboardDataModel
                {
                    Image = (Bitmap)dataObject.GetData(DataFormats.Bitmap)
                };
            }
            else
            {
                object data = dataObject.GetData(typeof(string));
                clipboardItem.Data = new ClipboardDataModel
                {
                    Text = data.ToString()
                };
            }

            Add(clipboardItem);
        }

        public void UnpinAll()
        {
            DataSource.Where(x => x.Pinned).ToList().ForEach(x => x.Pinned = false);
        }

        protected virtual void OnError(string e)
        {
            Error?.Invoke(this, e);
        }
    }
}