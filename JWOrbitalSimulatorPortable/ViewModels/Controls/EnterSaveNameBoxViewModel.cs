using JWOrbitalSimulatorPortable.Commands;
using JWOrbitalSimulatorPortable.Model;
using System.Windows.Input;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// View model controling the Enter Save Name Box Control
    /// </summary>
    public class EnterSaveNameBoxViewModel : NotifyingViewModel
    {
        // Controls whether the control is visible
        public bool Visibility { get; set; } = false;

        // Controls whether the enter button is enabled 
        public bool EnterButtonEnabled { get; private set; } = false;
        
        // The Save Name of the system
        private string _SaveNameText = "";
        public string SaveNameText
        {
            get { return _SaveNameText; }
            set { _SaveNameText = value; UpdateSaveButtonEnabled(value); }
        }
        
        // Commands to be bound to control's buttons
        public ICommand Save { get; private set; }
        public ICommand Back { get; private set; }

        //  Control's Dimensions
        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// Make the Enter Save Name Box Control Visible
        /// </summary>
        public void ShowBox()
        {
            Visibility = true;
            NotifyPropertyChanged(this, nameof(Visibility));
        }

        /// <summary>
        /// Make the Enter Save Name Box Control Hidden
        /// </summary>
        public void HideBox()
        {
            Visibility = false;
            NotifyPropertyChanged(this, nameof(Visibility));
        }

        /// <summary>
        /// Construct a viewmodel for the entersave name box control
        /// </summary>
        /// <param name="dimensionsToWindowSizeWighting">Percentage of overal window space to take up</param>
        public EnterSaveNameBoxViewModel(double dimensionsToWindowSizeWighting)
        {
            // set control dimensions
            Width  = (int)(MasterViewModel.Instance.WindowWidth * dimensionsToWindowSizeWighting);
            Height = (int)(MasterViewModel.Instance.WindowHeight * dimensionsToWindowSizeWighting);

            // Set control Commands to relay apprioriate view model methods
            Save = new RelayCommand(save);
            Back = new RelayCommand(back);
        }

        /// <summary>
        /// Save the current system with the typed save name.
        /// </summary>
        private void save()
        {
            // Set the current system's save name to the typed in the control. 
            // and save to a new file
            CanvasPageViewModel.Instance.System.SystemSaveName = SaveNameText;
            SystemFileParser.SaveNewSystemFile(CanvasPageViewModel.Instance.System);

            // Hide the save box and navigate back to the main menu
            HideBox();
            MasterViewModel.Instance.NavigatePage(ApplicationPage.StartMenu);
        }

        /// <summary>
        /// Hide the control and return to the canvas escape menu
        /// </summary>
        private void back()
        {
            HideBox();
            CanvasPageViewModel.Instance.EscMenu.ToggleEscMenu();
        }

        /// <summary>
        /// Enable the save button if the enter save name box has content typed in it.
        /// </summary>
        /// <param name="content">Content of the save name text box</param>
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