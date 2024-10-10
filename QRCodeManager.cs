using System;
using System.Drawing;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace PasswordManagerSSDw41
{
    public class QRCodeManager
    {
        public string GenerateQRCode(string data, string filePath)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Height = 300,
                    Width = 300
                }
            };

            using (var bitmap = writer.Write(data))
            {
                bitmap.Save(filePath);
                return filePath;
            }
        }

        public string ReadQRCode(string filePath)
        {
            var reader = new BarcodeReader();
            using (var bitmap = (Bitmap)Image.FromFile(filePath))
            {
                var result = reader.Decode(bitmap);
                return result?.Text;
            }
        }
    }
}