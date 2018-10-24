﻿using System.Windows;
using OrbitalSimulator_Objects;
using System.Threading;

namespace OrbitalSimulator_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {   
            InitializeComponent();
        
            DataContext = new WindowViewModel();

        }
    }
}
    