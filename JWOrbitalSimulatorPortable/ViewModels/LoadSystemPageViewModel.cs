using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public class LoadSystemPageViewModel : NotifyingViewModel
    {
        List<LoadFileViewModel> _LoadableSaveFiles = new List<LoadFileViewModel>();

        public List<LoadFileViewModel> LoadableSaveFiles { get => _LoadableSaveFiles; set { _LoadableSaveFiles = value; NotifyPropertyChanged(this, nameof(LoadableSaveFiles)); } }

        public LoadSystemPageViewModel()
        {
            LoadableSaveFiles = new List<LoadFileViewModel>(
                SystemFileParser.GetReadableSaveFiles().Select( (FileString)=> new LoadFileViewModel(FileString) )
                );

        }

        public void DeleteSave(string fileString)
        {
            SystemFileParser.DeleteSaveFile(fileString);
            NotifyPropertyChanged(this, nameof(LoadableSaveFiles));
        }

        public void LoadSaveFile(string fileString)
        {
            MasterViewModel.Instance.NavigatePage(ApplicationPage.CanvasPage);

            CanvasPageViewModel.Instance.LoadSystem(fileString);
        }
    }
}
