using OrbitalSimulator_Objects;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Diagnostics;

namespace OrbitalSimulator_UI
{
    /// <summary>
    /// Interaction logic for CanvasPage.xaml
    /// </summary>
    public partial class CanvasPage : BasePage<CanvasPageViewModel>
    {
        DispatcherTimer DebugTimer = new DispatcherTimer();
        
        public CanvasPage()
        {
            InitializeComponent();

            DebugTimer.Interval = new System.TimeSpan(0, 0, 0, 1);
            DebugTimer.Tick += DebugTimer_Tick;
            DebugTimer.Start();

            // !! This demonstrates that changes are filtering all the way up too the ViewModel's objects and changes are visable to the UI
            //    So the problem is definatly with updating the binding clients in the ellipse
        }


        private void DebugTimer_Tick(object sender, System.EventArgs e)
        {
            foreach (var Object in ViewModel.CanvasInterstellaObjects)
            {
                Debug.Print($"{Object.Type} : ({Object.X} , {Object.Y})");

            }

            
        }
    }
}



