﻿using System;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using KGySoft.ComponentModel;
using KGySoft.CoreLibraries;
using Super_Copy_Paste;
using TomsToolbox.Essentials;
using CollectionExtensions = TomsToolbox.Essentials.CollectionExtensions;

namespace SuperCopyPaste
{
    public class ClipboardDataManagement
    {
        private const int ImageCopyPasteToleranceMs = 100;
        private readonly SortableBindingList<ClipboardItem> _clipboardItems;
        private readonly IClipboardStorage _clipboardStorage;

        private FilterCriteria _filterCriteria = new FilterCriteria();

        private DateTime? _lastTimeCopyPasted;

        public ClipboardDataManagement(IClipboardStorage clipboardStorage)
        {
            _clipboardStorage = clipboardStorage;
            _clipboardItems = new SortableBindingList<ClipboardItem>();
        }

        public SortableBindingList<ClipboardItem> DataSource { get; } = new SortableBindingList<ClipboardItem>();

        public event EventHandler<int> CountChanged;

        private void Add(ClipboardItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Data.Text) && item.Data.Image == null) return;
            _clipboardItems.Add(item);

            Filter(_filterCriteria);

            OnCountChanged(DataSource.Count);
        }

        public void Load()
        {
            try
            {
                var clipboardItemsList = _clipboardStorage.Read<List<ClipboardItem>>();
                CollectionExtensions.AddRange(_clipboardItems, clipboardItemsList);
                CollectionExtensions.AddRange(DataSource, clipboardItemsList);
                OnCountChanged(DataSource.Count);
            }
            catch (Exception err)
            {
                Trace.WriteLine(err);
            }
        }

        public void Save()
        {
            _clipboardStorage.Write(_clipboardItems.ToList());
        }

        public void Delete(ClipboardItem item)
        {
            if (item == null || item.Pinned)
            {
                return;
            }
            _clipboardItems.Remove(item);
            DataSource.Remove(item);
            OnCountChanged(_clipboardItems.Count);
        }

        public void Filter(FilterCriteria criteria)
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
                               (DateTime.Now - _lastTimeCopyPasted.Value).TotalMilliseconds < ImageCopyPasteToleranceMs;

            if (dataObject == null || isDuplicated) return;

            _lastTimeCopyPasted = DateTime.Now;

            var clipboardItem = new ClipboardItem();

            if (dataObject.GetDataPresent(DataFormats.Bitmap))
            {
                clipboardItem.Data = new ClipboardData
                {
                    Image = dataObject.GetData(DataFormats.Bitmap),
                    ClipboardType = ClipboardType.Image
                };
            }
            else
            {
                var data = dataObject.GetData(typeof(string));
                clipboardItem.Data = new ClipboardData
                {
                    Text = data.ToString(),
                    ClipboardType = ClipboardType.Text
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