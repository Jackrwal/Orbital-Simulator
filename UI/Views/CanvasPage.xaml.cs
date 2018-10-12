using OrbitalSimulator_Objects;
using System.Windows;
using OrbitalSimulator_Objects;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

// !! Impliment This for Canvas children to truly reflect Items in the Model

// Make CanvasPage Inherit from BasePage DONE

// Create a Canvas Page ViewModel DONE

// Correctly Pass VM Type into Base Page from CanvasPage DONE

// View Model Contains an observale Collection of Interstella Object View Models (each subscribed to a model object)
// This Collection should be only be a reflection of a collection in Model, ViewModel should only reformat infomation from the model
// This may require some sort of 'InterstellaSystem' Class to reflect the current System and could also support saving and loading the InterstellaSystems 
// And could potentially handle Ticking Every Interstella Object when told to do so by the UI (Although this is at current all done statically in BaseMovingObject)

// Canvas Children Bound too this collection    (This may require a Item Control / Item Template)
// Value Converted used with the InterstellaObjectType of VM to return the 'skin' of the object

// And then check that this all conforms to MVVM

namespace OrbitalSimulator_UI
{
    /// <summary>
    /// Interaction logic for CanvasPage.xaml
    /// </summary>
    public partial class CanvasPage : BasePage<CanvasPageViewModel>
    {
        public CanvasPage() : base(false)
        {
            InitializeComponent();

        }
    }
}



