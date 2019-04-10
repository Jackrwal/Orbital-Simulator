using JWOrbitalSimulatorPortable.Commands;
using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public class StartMenuViewModel : NotifyingViewModel
    {
        public StartMenuViewModel()
        {
            NewSystem = new RelayCommand(loadNewSystem);
            Demo = new RelayCommand(loadDemo);
            LoadSystem = new RelayCommand(openLoadMenu);
        }

        private void openLoadMenu()
        {
            MasterViewModel.Instance.NavigatePage(ApplicationPage.LoadPage);
        }

        private void loadNewSystem()
        {
            MasterViewModel.Instance.NavigatePage(ApplicationPage.CanvasPage);
            CanvasPageViewModel.Instance.LoadNewSystem();
        }


        private void loadDemo()
        {
            MasterViewModel.Instance.NavigatePage(ApplicationPage.CanvasPage);

            List<string> ReadableSaveFiles = SystemFileParser.GetReadableSaveFiles();
            CanvasPageViewModel.Instance.LoadSystem(DemoSystemModel.GetDemoInstance);
        }

        public ICommand Demo { get; set; }
        public ICommand NewSystem { get; set; }
        public ICommand LoadSystem { get; set; }
    }
}
