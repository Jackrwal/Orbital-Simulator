using JWOrbitalSimulatorPortable.Model;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public class LoadFileViewModel : NotifyingViewModel
    {
        private string _SaveFileString;
        private string[] _FileMetaData;

        public string SaveFileString
        {
            get { return _SaveFileString; }
            set
            {
                _SaveFileString = value;
                _FileMetaData = SystemFileParser.PreReadSystemFile(value);
            }
        }

        public LoadFileViewModel(string saveFileString)
        {
            SaveFileString = saveFileString;
        }

        public string SaveDate => _FileMetaData?[1];

        public string SaveName => _FileMetaData?[0];

    }
}
