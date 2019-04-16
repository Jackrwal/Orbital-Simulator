using JWOrbitalSimulatorPortable.Commands;
using JWOrbitalSimulatorPortable.Model;
using System.Windows.Input;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// Controls a Canvas Escape Menu Control
    /// </summary>
    public class CanvasEscMenuViewModel : NotifyingViewModel
    {
        // Controls whether the control is visible on the page.
        public bool Visiblity { get; set; } = false;

        // Dimensions of the control
        public int EscBoxWidth { get; set; }
        public int EscBoxHeight { get; set; }

        // Commands to be bound to each button's onclick
        public ICommand SaveAndReturnToMenu { get; set; }
        public ICommand ReturnToMenu { get; set; }
        public ICommand Continue { get; set; }

        /// <summary>
        /// Construct an instance of the canvas escape menu view model
        /// </summary>
        /// <param name="dimensionsToWindowSizeWighting">The percentage of window size the control's dimensions should use</param>
        public CanvasEscMenuViewModel(double dimensionsToWindowSizeWighting)
        {
            // Set the control's dimensions
            EscBoxWidth = (int)(MasterViewModel.Instance.WindowWidth * dimensionsToWindowSizeWighting);
            EscBoxHeight = (int)(MasterViewModel.Instance.WindowHeight * dimensionsToWindowSizeWighting);

            // Set the commands buttons are bound to, to relay appropriate viewmodel methods.
            ReturnToMenu = new RelayCommand(NavigateToMenu);
            SaveAndReturnToMenu = new RelayCommand(Save);
            Continue = new RelayCommand(ResumeSim);
        }

        /// <summary>
        /// Change the visibility of the control
        /// </summary>
        public void ToggleEscMenu()
        {
            if (Visiblity) Visiblity = false;
            else Visiblity = true;
            NotifyPropertyChanged(this, nameof(Visiblity));
        }

        /// <summary>
        /// Hides the control to return to the simulation.
        /// </summary>
        private void ResumeSim()
        {
            Visiblity = false;
            NotifyPropertyChanged(this, nameof(Visiblity));
        }

        /// <summary>
        /// Save the current simulation to file.
        /// </summary>
        private void Save()
        {
            // Get the current system from CPVM instance
            InterstellaSystem SystemToSave = CanvasPageViewModel.Instance.System;

            // If the system has no save name it has not been saved
            if (SystemToSave.SystemSaveName == "")
            {
                // If system has never been saved,Open New Enter Save Name Box
                OpenEnterSaveNameBox();
            }
            else
            {
                bool FoundSystemFile = false;
                // If the system has a save name, attempt to find its save file by comparing against each avaliable save file
                foreach (var SaveFile in SystemFileParser.GetReadableSaveFiles())
                {
                    // Get the path the file would be saved as.
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
                    // If a save file has been found and saved to, return to main menu
                    NavigateToMenu();
                }
                else
                {
                    // No save file found, must have been deleted or moved.
                    // Ask for a new save name and save to a new file.
                    OpenEnterSaveNameBox();
                }
            }
        }

        /// <summary>
        /// Open the Enter Save Name Box.
        /// </summary>
        private void OpenEnterSaveNameBox()
        {
            // Open the instance of enter save name box and hide the escape menu
            CanvasPageViewModel.Instance.SaveNameBox.ShowBox();
            ToggleEscMenu();
        }

        /// <summary>
        /// Navigate back to the main menu
        /// </summary>
        private void NavigateToMenu()
        {
            MasterViewModel.Instance.NavigatePage(ApplicationPage.StartMenu);
        }
    }
}
