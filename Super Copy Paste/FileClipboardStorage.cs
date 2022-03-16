// ------------------------------------------------------------------------------
//     <copyright file="FileClipboardStorage.cs" company="BlackLine">
//         Copyright (C) BlackLine. All rights reserved.
//     </copyright>
// ------------------------------------------------------------------------------

using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SuperCopyPaste
{
    public class FileClipboardStorage : IClipboardStorage
    {
        private const string FileStorageName = "storage.bin";

        private static string StorageFile => Path.Combine(Application.StartupPath, FileStorageName);

        /// <summary>Reads an object instance from a binary file.</summary>
        /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public T Read<T>()
        {
            var serializer = new JsonSerializer();

            using (TextReader stream = new StreamReader(StorageFile))
            {
                using (JsonReader reader = new JsonTextReader(stream))
                {
                    // read the json from a stream
                    // json size doesn't matter because only a small piece is read at a time from the HTTP request
                    var result = serializer.Deserialize<T>(reader);
                    return result;
                }
            }
        }

        /// <summary>
        ///     Writes the given object instance to a binary file.
        ///     <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        ///     <para>
        ///         To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be
        ///         applied to properties.
        ///     </para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the binary file.</typeparam>
        /// <param name="objectToWrite">The object instance to write to the binary file.</param>
        public void Write<T>(T objectToWrite)
        {
            var serializer = new JsonSerializer();
            using (TextWriter stream = new StreamWriter(StorageFile))
            {
                using (var reader = new JsonTextWriter(stream))
                {
                    // read the json from a stream
                    // json size doesn't matter because only a small piece is read at a time from the HTTP request
                    serializer.Serialize(reader, objectToWrite);
                }
            }
        }
    }
}