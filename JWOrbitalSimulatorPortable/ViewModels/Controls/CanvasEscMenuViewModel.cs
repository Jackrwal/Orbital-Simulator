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
    public class CanvasEscMenuViewModel : NotifyingViewModel
    {
        public bool Visiblity { get; set; } = false;

        public int EscBoxWidth { get; set; }
        public int EscBoxHeight { get; set; }

        public ICommand SaveAndReturnToMenu { get; set; }
        public ICommand ReturnToMenu { get; set; }
        public ICommand Continue { get; set; }

        public CanvasEscMenuViewModel(double dimensionsToWindowSizeWighting)
        {
            EscBoxWidth = (int)(MasterViewModel.Instance.WindowWidth * dimensionsToWindowSizeWighting);
            EscBoxHeight = (int)(MasterViewModel.Instance.WindowHeight * dimensionsToWindowSizeWighting);

            ReturnToMenu = new RelayCommand(NavigateToMenu);
            SaveAndReturnToMenu = new RelayCommand(Save);
            Continue = new RelayCommand(ResumeSim);
        }

        public void ToggleEscMenu()
        {
            if (Visiblity) Visiblity = false;
            else Visiblity = true;
            NotifyPropertyChanged(this, nameof(Visiblity));
        }

        private void ResumeSim()
        {
            Visiblity = false;
            NotifyPropertyChanged(this, nameof(Visiblity));
        }

        private void Save()
        {
            InterstellaSystem SystemToSave = CanvasPageViewModel.Instance.System;

            if (SystemToSave.SystemSaveName == "")
            {
                // If system has never been saved,Open New Enter Save Name Box
                OpenEnterSaveNameBox();
            }
            else
            {
                bool FoundSystemFile = false;
                // If the system has a save name, attempt to find its save file
                foreach (var SaveFile in SystemFileParser.GetReadableSaveFiles())
                {
                    string SystemToSaveFilePath = $"{SystemFileParser.ExecutingDomainFilePath}{SystemToSave.SystemSaveName}OSSaveFile.txt";
                    
                    // If A save File is found. overwrite this file
                    if(SaveFile == SystemToSaveFilePath)
                    {
                        SystemFileParser.OverWriteSystemFile(SystemToSave, SaveFile);
                        FoundSystemFile = true;
                    }
                        
                }
                if (FoundSystemFile)
                {
                    NavigateToMenu();
                }
                else
                {
                    // Save File must have gotten deleted, Open New Enter Save Name Box
                    OpenEnterSaveNameBox();
                }
            }
        }

        private void OpenEnterSaveNameBox()
        {
            CanvasPageViewModel.Instance.SaveNameBox.ShowBox();
            ToggleEscMenu();
        }

        private void NavigateToMenu()
        {
            MasterViewModel.Instance.NavigatePage(ApplicationPage.StartMenu);
        }
    }
}
