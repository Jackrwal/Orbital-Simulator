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
    public class EnterSaveNameBoxViewModel : NotifyingViewModel
    {
        // ~~ Move Visibility & Hide/Show Code into an abstract pop-up control View model class to inherit from for this functionality
        public bool Visibility { get; set; } = false;

        public bool EnterButtonEnabled { get; private set; } = false;
        
        private string _SaveNameText = "";
        public string SaveNameText
        {
            get { return _SaveNameText; }
            set { _SaveNameText = value; UpdateSaveButtonEnabled(value); }
        }
        
        public ICommand Save { get; private set; }
        public ICommand Back { get; private set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public void ShowBox()
        {
            Visibility = true;
            NotifyPropertyChanged(this, nameof(Visibility));
        }

        public void HideBox()
        {
            Visibility = false;
            NotifyPropertyChanged(this, nameof(Visibility));
        }

        public EnterSaveNameBoxViewModel(double dimensionsToWindowSizeWighting)
        {
            Width  = (int)(MasterViewModel.Instance.WindowWidth * dimensionsToWindowSizeWighting);
            Height = (int)(MasterViewModel.Instance.WindowHeight * dimensionsToWindowSizeWighting);

            Save = new RelayCommand(save);
            Back = new RelayCommand(back);
        }

        private void save()
        {
            CanvasPageViewModel.Instance.System.SystemSaveName = SaveNameText;
            SystemFileParser.SaveNewSystemFile(CanvasPageViewModel.Instance.System);

            HideBox();
            MasterViewModel.Instance.NavigatePage(ApplicationPage.StartMenu);
        }

        private void back()
        {
            HideBox();
            CanvasPageViewModel.Instance.EscMenu.ToggleEscMenu();
        }

        public void UpdateSaveButtonEnabled(string content)
        {
            if (content != "")
            {
                EnterButtonEnabled = true;
                NotifyPropertyChanged(this, nameof(EnterButtonEnabled));
            }
            else
            {
                EnterButtonEnabled = false;
                NotifyPropertyChanged(this, nameof(EnterButtonEnabled));
            }
        }
    }
}
