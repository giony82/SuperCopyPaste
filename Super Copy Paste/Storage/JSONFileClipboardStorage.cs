using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using SuperCopyPaste.Interfaces;

namespace SuperCopyPaste.Storage
{
    public class JSONFileClipboardStorage : IClipboardStorage
    {
        private const string FileStorageName = "storage.bin";

        private static string StorageFile => Path.Combine(Application.StartupPath, FileStorageName);

        /// <summary>Reads an object instance from a binary file.</summary>
        /// <typeparam name="T">The type of object to read from the JSON file.</typeparam>
        /// <returns>Returns a new instance of the object read from the JSON file.</returns>
        public T Read<T>() where T : new()
        {
            if (!File.Exists(StorageFile)) return new T();
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new ImageConverter());

            using (TextReader stream = new StreamReader(StorageFile))
            {
                using (JsonReader reader = new JsonTextReader(stream))
                {
                    var result = serializer.Deserialize<T>(reader);
                    return result;
                }
            }
        }

        /// <summary>
        ///     Writes the given object instance to a JSON file.
        /// </summary>
        /// <typeparam name="T">The type of object being written to the JSON file.</typeparam>
        /// <param name="objectToWrite">The object instance to write to the JSON file.</param>
        public void Write<T>(T objectToWrite)
        {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new ImageConverter());
            using (TextWriter stream = new StreamWriter(StorageFile))
            {
                using (var reader = new JsonTextWriter(stream))
                {
                    serializer.Serialize(reader, objectToWrite);
                }
            }
        }
    }
}