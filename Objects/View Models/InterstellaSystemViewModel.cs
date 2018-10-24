using OrbitalSimulator_Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace OrbitalSimulator_Objects
{
    #region !!! Obsolte !!! InterstellaSystemViewModel

    // ## Do i really need this class, It is a view model that will never by bound too, this functionality could just be handled in the CanvasPage View Model
    //public class InterstellaSystemViewModel : BaseViewModel
    //{
    //    private InterstellaSystem _System;
    //    private ObservableCollection<InterstellaObjectViewModel> _ObjectVMs;

    //    public InterstellaSystemViewModel(InterstellaSystem system)
    //    {
    //        _System = system;

    //        // Project System InterstellaObejct's Into thier VMs
    //        ObjectVMs = new ObservableCollection<InterstellaObjectViewModel>(
    //            _System.InterstellaObjects.Select(
    //                InterstellaObject => new InterstellaObjectViewModel(InterstellaObject)
    //            ));
    //    }

    //    public ObservableCollection<InterstellaObjectViewModel> ObjectVMs
    //    {
    //        get => _ObjectVMs;
    //        set
    //        {
    //            _ObjectVMs = value;
    //            NotifyPropertyChanged(this , nameof(ObjectVMs));
    //        }
    //    }

    //    public void StartSystem()
    //    {
    //        _System.Start();
    //    }

    //    public void StopSystem()
    //    {
    //        _System.Stop();
    //    }
    //}

    #endregion
}