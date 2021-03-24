using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using KGySoft.ComponentModel;
using KGySoft.CoreLibraries;
using SuperCopyPaste.Models;
using TomsToolbox.Essentials;
using CollectionExtensions = TomsToolbox.Essentials.CollectionExtensions;

namespace SuperCopyPaste.Core
{
    public class ClipboardDataManagement
    {
        private readonly SortableBindingList<ClipboardItemModel> _clipboardItems;
        private readonly IClipboardStorage _clipboardStorage;

        private FilterCriteriaModel _filterCriteria = new FilterCriteriaModel();

        private DateTime? _lastTimeCopyPasted;
        private readonly SortableBindingList<ClipboardItemModel> _filteredClipboardItems = new SortableBindingList<ClipboardItemModel>();

        public ClipboardDataManagement(IClipboardStorage clipboardStorage)
        {
            _clipboardStorage = clipboardStorage;
            _clipboardItems = new SortableBindingList<ClipboardItemModel>();
        }

        public SortableBindingList<ClipboardItemModel> DataSource => _filteredClipboardItems;

        public event EventHandler<int> CountChanged;

        private void Add(ClipboardItemModel item)
        {
            if (string.IsNullOrWhiteSpace(item.Data.Text) && item.Data.Image == null)
            {
                return;
            }

            KeepListMaxAllowedLength();

            _clipboardItems.Add(item);

            Filter(_filterCriteria);

            OnCountChanged(DataSource.Count);
        }

        private void KeepListMaxAllowedLength()
        {
            if (_clipboardItems.Count > ClipboardConstants.MaxClipboardRecords)
            {
                var oldestClipboard = _clipboardItems.OrderByDescending(x => x.Created).First();
                _clipboardItems.Remove(oldestClipboard);
            }
        }

        public void Load()
        {
            try
            {
                var clipboardItemsList = _clipboardStorage.Read<List<ClipboardItemModel>>().OrderByDescending(x => x.Created)
                    .ToList();

                PopulateLists(clipboardItemsList);
            }
            catch (Exception err)
            {
                Trace.WriteLine(err);

                PopulateLists(GetErrorAsClipboardItem(err));
            }
        }

        private static List<ClipboardItemModel> GetErrorAsClipboardItem(Exception err)
        {
            var clipboardItemError = new ClipboardItemModel
            {
                Created = DateTime.Now,
                Data = new ClipboardDataModel
                {
                    Text = err.ToString()
                }
            };
            var errorAsClipboardItems = new List<ClipboardItemModel>
            {
                clipboardItemError
            };
            return errorAsClipboardItems;
        }

        private void PopulateLists(List<ClipboardItemModel> clipboardItemsList)
        {
            CollectionExtensions.AddRange(_clipboardItems, clipboardItemsList);
            CollectionExtensions.AddRange(DataSource, clipboardItemsList);
            OnCountChanged(DataSource.Count);
        }

        public void Save()
        {
            _clipboardStorage.Write(_clipboardItems.ToList());
        }

        public void Delete(ClipboardItemModel item)
        {
            if (item == null || item.Pinned)
            {
                return;
            }
            _clipboardItems.Remove(item);
            DataSource.Remove(item);
            OnCountChanged(_clipboardItems.Count);
        }

        public void Filter(FilterCriteriaModel criteria)
        {
            _filterCriteria = criteria;

            var hasText = !string.IsNullOrWhiteSpace(_filterCriteria.Text);
            var items = (from clipboardItem in _clipboardItems
                let isTextMatched = !hasText || clipboardItem.Data.ClipboardType == ClipboardType.Text &&
                    clipboardItem.Data.Text.Contains(_filterCriteria.Text, StringComparison.InvariantCultureIgnoreCase)
                let isImageOk = clipboardItem.Data.ClipboardType == ClipboardType.Image && !hasText || isTextMatched
                let isPinnedMatch = clipboardItem.Pinned == _filterCriteria.Pinned || !_filterCriteria.Pinned
                where isTextMatched && isImageOk && isPinnedMatch
                select clipboardItem).ToList().OrderByDescending(x => x.Created);

            DataSource.Clear();
            CollectionExtensions.AddRange(DataSource, items);
            OnCountChanged(DataSource.Count);
        }

        public void Clear()
        {
            _clipboardItems.Clear();
            DataSource.Clear();
            OnCountChanged(DataSource.Count);
            Save();
        }

        public void DeleteItems(int daysOlder)
        {
            _clipboardItems.RemoveWhere(x => (DateTime.Now - x.Created).TotalDays >= daysOlder && !x.Pinned);
            DataSource.RemoveWhere(x => (DateTime.Now - x.Created).TotalDays >= daysOlder && !x.Pinned);
            Filter(_filterCriteria);
            OnCountChanged(DataSource.Count);
        }

        protected virtual void OnCountChanged(int count)
        {
            CountChanged?.Invoke(this, count);
        }


        public void AddClipboardData(IDataObject dataObject)
        {
            //When capturing images with CTRL SHIFT S, the event is triggered twice(same image).
            var isDuplicated = _lastTimeCopyPasted != null &&
                               (DateTime.Now - _lastTimeCopyPasted.Value).TotalMilliseconds < ClipboardConstants.ImageCopyPasteToleranceMs;

            if (dataObject == null || isDuplicated) return;

            _lastTimeCopyPasted = DateTime.Now;

            var clipboardItem = new ClipboardItemModel();

            if (dataObject.GetDataPresent(DataFormats.Bitmap))
            {
                clipboardItem.Data = new ClipboardDataModel
                {
                    Image = dataObject.GetData(DataFormats.Bitmap),
                };
            }
            else
            {
                var data = dataObject.GetData(typeof(string));
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
    }
}