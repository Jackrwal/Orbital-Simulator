using JWOrbitalSimulatorPortable.Model;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// Presents a Loadable Save File to the UI
    /// </summary>
    public class LoadFileViewModel : NotifyingViewModel
    {
        // The Full File Path of the save file
        private string _SaveFileString;

        // The Save Name and Save Time of the load file
        private string[] _FileMetaData;

        // When SaveFileString Property is set update the FileMetaData for the new file.
        public string SaveFileString
        {
            get { return _SaveFileString; }
            set
            {
                _SaveFileString = value;
                _FileMetaData = SystemFileParser.PreReadSystemFile(value);
            }
        }

        /// <summary>
        /// Construct a load file view model with the full file path of the file to present.
        /// </summary>
        public LoadFileViewModel(string saveFileString)
        {
            SaveFileString = saveFileString;
        }

        // If File MetaData is set return the Save Date
        public string SaveDate => _FileMetaData?[1];

        // If File MetaData is set return the Save Name
        public string SaveName => _FileMetaData?[0];

    }
}
