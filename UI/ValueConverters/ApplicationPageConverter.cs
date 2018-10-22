using OrbitalSimulator_Objects;
using OrbitalSimulator_UI.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalSimulator_UI
{
    public class ApplicationPageConverter : BaseValueConverter<ApplicationPageConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.Start:
                    return new StartPage();
                    //return new StartPage() { DataContext = new StartPageViewModel() }; // !! Parse MainWindow's data context
                
                case ApplicationPage.CanvasPage:
                    return new CanvasPage();

                default:
                    return null;
            }

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
