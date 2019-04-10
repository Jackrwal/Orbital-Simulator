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

namespace OrbitalSimulator.Controls
{
    /// <summary>
    /// Interaction logic for HoverDataEntryBox.xaml
    /// </summary>
    public partial class HoverDataEntryBox : UserControl
    {
        public HoverDataEntryBox()
        {
            InitializeComponent();
        }

        private void RadiusBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox SenderAsTextBox = (TextBox)sender;

            HoverDataEntryBoxViewModel.ValidateField(SenderAsTextBox.Text, HoverDataEntryBoxViewModel.FieldType.Radius);

        }

        private void MassBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox SenderAsTextBox = (TextBox)sender;
            HoverDataEntryBoxViewModel.ValidateField(SenderAsTextBox.Text, HoverDataEntryBoxViewModel.FieldType.Mass);
        }

        private void RadiusBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) CanvasPageViewModel.Instance.DataBoxVM.Radius = ((TextBox)sender).Text;
        }

        private void MassBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) CanvasPageViewModel.Instance.DataBoxVM.Mass = ((TextBox)sender).Text;
        }
    }
}