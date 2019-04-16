using System;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;

namespace OrbitalSimulator.ValueConverters
{
    /// <summary>
    /// decodes images stored in base64 encoding and returns an image
    /// </summary>
    public class Base64ToImageConverter : AbstractValueConverter<Base64ToImageConverter>
    {
        /// <summary>
        /// Takes value as a Base64 String and returns it as a bitmap image.
        /// </summary>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Attempt to convert value from a string of base 64 to an array of bytes
            byte[] Base64AsBytes;
            try
            {
                Base64AsBytes = System.Convert.FromBase64String((string)value);
            }
            catch (Exception)
            {
                return null;
            }

            // Attempt to construct a bitmap image from the bytes
            BitmapFrame ImageBitMap;
            using(var stream = new MemoryStream(Base64AsBytes))
            {
                ImageBitMap = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }

            return ImageBitMap;
        }

        // Convert Back Not Implimented
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
