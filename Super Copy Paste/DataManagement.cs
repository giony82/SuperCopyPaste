using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

using KGySoft.ComponentModel;
using KGySoft.CoreLibraries;

using TomsToolbox.Essentials;

using CollectionExtensions = TomsToolbox.Essentials.CollectionExtensions;

namespace Super_Copy_Paste
{
    public class Storage
    {
        private SortableBindingList<ClipboardItem> _clipboardItems;
        private SortableBindingList<ClipboardItem> _filtered=new SortableBindingList<ClipboardItem>();

        private const string FileStorageName = "storage.bin";

        private string StorageFile => Path.Combine(Application.StartupPath,Storage.FileStorageName);

        public SortableBindingList<ClipboardItem> ClipboardItems => _clipboardItems;

        public event EventHandler<int> CountChanged;

        public Storage()
        {
            _clipboardItems=new SortableBindingList<ClipboardItem>();
        }

        /// <summary>Reads an object instance from a binary file.</summary>
        /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        private void Add(ClipboardItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Data.Text) && item.Data.Data == null)
            {
                return;
            }
            ClipboardItems.Add(item);
            OnCountChanged(ClipboardItems.Count);
        }

        /// <summary>Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the binary file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the binary file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public void Load()
        {
            if (!File.Exists(StorageFile))
            {
                return;
            }

            try
            {
                var clipboardItemsList = Storage.ReadFromBinaryFile<List<ClipboardItem>>(StorageFile);
                CollectionExtensions.AddRange(_clipboardItems, clipboardItemsList);
                OnCountChanged(_clipboardItems.Count);
            }
            catch (Exception err)
            {
                Trace.WriteLine(err);
            }
        }

        public void Save()
        {
            WriteToBinaryFile(StorageFile,_clipboardItems.ToList());
        }

        public void Delete(ClipboardItem item)
        {
            _clipboardItems.Remove(item);
            OnCountChanged(_clipboardItems.Count);
        }

        public void Filter(string txt)
        {
            if (_filtered.Count == 0)
            {
                CollectionExtensions.AddRange(_filtered, _clipboardItems);
            }
            
            if (string.IsNullOrWhiteSpace(txt) && _filtered.Count>0)
            {
                _clipboardItems.Clear();
                CollectionExtensions.AddRange(_clipboardItems, _filtered);
                _filtered.Clear();
                return;
            }

            List<ClipboardItem> items = (from clipboardItem in _clipboardItems
                                         let isTextMatched = clipboardItem.Data.Text != null && !clipboardItem.Data.Text.Contains(txt, StringComparison.InvariantCultureIgnoreCase)
                                         let isImage = clipboardItem.Data.ClipboardType == ClipboardType.Image
                                         where isTextMatched || isImage
                                         select clipboardItem).ToList();

            _clipboardItems.RemoveRange(items);
            OnCountChanged(_clipboardItems.Count);
        }

        public void Clear()
        {
            ClipboardItems.Clear();
            OnCountChanged(_clipboardItems.Count);
            Save();
        }

        public void DeleteItems(int daysOlder)
        {
            ClipboardItems.RemoveWhere(x => (DateTime.Now - x.Created).TotalDays >= daysOlder);
            OnCountChanged(_clipboardItems.Count);
        }

        protected virtual void OnCountChanged(int count)
        {
            CountChanged?.Invoke(this, count);
        }

        private DateTime? _lastTimeCopyPasted;


        public void AddClipboardData(IDataObject dataObject)
        {
            //When capturing images with CTRL SHIFT S, the event is triggered twice(same image).
            bool isDuplicated = _lastTimeCopyPasted!=null && (DateTime.Now-_lastTimeCopyPasted.Value).TotalMilliseconds<100;

            if (dataObject == null || isDuplicated)
            {
                return;
            }
            _lastTimeCopyPasted = DateTime.Now;
            if (dataObject.GetDataPresent(DataFormats.Bitmap))
            {
                Add(
                    new ClipboardItem
                    {
                        Data = new ClipboardData{
                            Data = dataObject.GetData(DataFormats.Bitmap),
                            ClipboardType = ClipboardType.Image
                        }
                    });
            }
            else
            {
                object data = dataObject.GetData(typeof(string));
                Add(
                    new ClipboardItem
                    {
                        Data = new ClipboardData
                        {
                            Text = data.ToString(),
                            ClipboardType = ClipboardType.Text
                        }
                    });
            }
        }
    }
}