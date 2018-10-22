using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalSimulator_Objects
{
    public class StartPageViewModel : BaseViewModel
    {
        WindowViewModel _Parent;

        public StartPageViewModel(WindowViewModel parent)
        {
            _Parent = parent;
        
        }

        // Contain:
        // parent View Model
        // Infomation for data triggers
        // Such as Whether there is data to load to enable load Button

    }

}