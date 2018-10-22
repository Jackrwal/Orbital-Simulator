using OrbitalSimulator_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OrbitalSimulator_UI
{
    //Uses x:TypeArugments="local:StartPageViewModel" in XAML to parse the View Model Type in the XAML
    public class BasePage<VM> : Page
        where VM : BaseViewModel, new()
    {
        public BasePage()
        {
            ViewModel = new VM();
            base.DataContext = ViewModel;
        }

        public BasePage(VM viewModel)
        {
            ViewModel = viewModel;
            base.DataContext = ViewModel;
        }

        public VM ViewModel { get; set; }


        // !! Impliment for Navigation:
        // Every Page Inherits from Base Page
        // Every Page has a View Model ( of type VM )
        // Every ViewModel In this List has a refference to the Master ViewModel ( in the Base View Model )
        // Master View Model contains navigation commands and function.

        // Therefore every View Model can Impliment a 'Paramirised Relay command' containing the Master View Model's Navigate Command

        // Therefor every Page can Access its View Model from Base Page
        // Therefor can access the Navigation Command 

    }
}
