// ------------------------------------------------------------------------------
//     <copyright file="ImageConverter.cs" company="BlackLine">
//         Copyright (C) BlackLine. All rights reserved.
//     </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Newtonsoft.Json;

namespace SuperCopyPaste.Storage
{
    public class ImageConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Bitmap);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var readerValue = (string)reader.Value;

            if (readerValue == null) return null;

            using (var m = new MemoryStream(Convert.FromBase64String(readerValue)))
            {
                var img = new Bitmap(Image.FromStream(m));
                return img;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var bmp = (Bitmap)value;
            using (var m = new MemoryStream())
            {
                bmp.Save(m, ImageFormat.Jpeg);

                byte[] inArray = m.ToArray();
                string base64String = Convert.ToBase64String(inArray);

                writer.WriteValue(base64String);
            }
        }
    }
}