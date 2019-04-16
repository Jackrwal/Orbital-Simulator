using JWOrbitalSimulatorPortable.Model;
using System.Collections.Generic;
using System.Linq;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// Controls the load System Page. Presents a list of loadable files.
    /// </summary>
    public class LoadSystemPageViewModel : NotifyingViewModel
    {
        // Save Files that are avaliable to load.
        List<LoadFileViewModel> _LoadableSaveFiles = new List<LoadFileViewModel>();
        public List<LoadFileViewModel> LoadableSaveFiles  => _LoadableSaveFiles;

        public LoadSystemPageViewModel()
        {
            // Loadable Files is given by the file strings of loadable files casted to load file view models
            _LoadableSaveFiles = new List<LoadFileViewModel>(
                SystemFileParser.GetReadableSaveFiles().Select( (FileString)=> new LoadFileViewModel(FileString) )
                );

        }

        /// <summary>
        /// Delete a save file.
        /// </summary>
        /// <param name="fileString"></param>
        public void DeleteSave(string fileString)
        {
            // Attempt to remove the file and notify the view a file has been deleted
            SystemFileParser.DeleteSaveFile(fileString);
            NotifyPropertyChanged(this, nameof(LoadableSaveFiles));
        }

        /// <summary>
        /// Load a given file string
        /// </summary>
        /// <param name="fileString"></param>
        public void LoadSaveFile(string fileString)
        {
            // Navigate to the canvas page
            MasterViewModel.Instance.NavigatePage(ApplicationPage.CanvasPage);

            // Load the given file as a system.
            CanvasPageViewModel.Instance.LoadSystem(fileString);
        }
    }
}
