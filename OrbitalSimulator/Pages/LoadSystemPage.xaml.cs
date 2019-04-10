using JWOrbitalSimulatorPortable.ViewModels;
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

namespace OrbitalSimulator.Pages
{
    /// <summary>
    /// Interaction logic for LoadSystemPage.xaml
    /// </summary>
    public partial class LoadSystemPage : AbstractMVVMPage<LoadSystemPageViewModel>
    {
        public LoadSystemPage()
        {
            InitializeComponent();
        }

        private void LoadSaveOnClick(object sender, MouseButtonEventArgs e)
        {
            ((LoadSystemPageViewModel)DataContext).LoadSaveFile( ((LoadFileViewModel)((Grid)sender).DataContext).SaveFileString);
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            ((LoadSystemPageViewModel)DataContext).DeleteSave(((LoadFileViewModel)((Button)sender).DataContext).SaveFileString);
        }
    }
}
