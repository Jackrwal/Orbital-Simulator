using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace OrbitalSimulator.ValueConverters
{
    public class Base64ToImageConverter : AbstractValueConverter<Base64ToImageConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] Base64AsBytes = System.Convert.FromBase64String((string)value);


            BitmapFrame ImageBitMap;

            using(var stream = new MemoryStream(Base64AsBytes))
            {
                ImageBitMap = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }

            return ImageBitMap;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
