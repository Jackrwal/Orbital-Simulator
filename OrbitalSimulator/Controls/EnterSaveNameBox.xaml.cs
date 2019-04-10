﻿using JWOrbitalSimulatorPortable.ViewModels;
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
    /// Interaction logic for EnterSaveNameBox.xaml
    /// </summary>
    public partial class EnterSaveNameBox : UserControl
    {
        public EnterSaveNameBox()
        {
            InitializeComponent();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ((EnterSaveNameBoxViewModel)DataContext).UpdateSaveButtonEnabled(((TextBox)sender).Text);
        }
    }
}
