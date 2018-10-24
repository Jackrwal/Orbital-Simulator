using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrbitalSimulator_UI.Views
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();

            // ## Start Page Needs Data Context to bind too, this could contain the relay commands for each button.
            //    However navigation would want to be implimented in the mainpage and accessable to all Views 
            //    So this would require the command to be in the MainPage VM and accessed by all Views for navigation

            //    This Could Be Done with a Navigation Command used by the MainWindow View Model to Change Current Page
        }

    }
}
