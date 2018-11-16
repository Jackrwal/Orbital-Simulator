using JWOrbitalSimulatorPortable.Model;
using JWOrbitalSimulatorPortable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vector = JWOrbitalSimulatorPortable.Model.Vector;

namespace OrbitalSimulator.Pages
{
    /// <summary>
    /// Interaction logic for CanvasPage.xaml
    /// </summary>
    public partial class CanvasPage : AbstractMVVMPage<CanvasPageViewModel>
    {
        // !! Curently Loading Test Sytem in CPVM remove this later to add objects via the UI or file load
        public CanvasPage() : base(new CanvasPageViewModel(true))
        {
            InitializeComponent();
        }

        // ## This is a tempoary solution to implimenting dragdrop untill i find a way to DragDrop without violating MVVM
        //    I have to control the operation here.
        private void drop(object sender, DragEventArgs e)
        {
            InterstellaObject DroppedObject = e.Data.GetData(typeof(InterstellaObject)) as InterstellaObject;
            if (DroppedObject == null) return;

            Point DropPoint = e.GetPosition((Canvas)sender);
            DroppedObject.Position = new Vector(DropPoint.X, DropPoint.Y);

            //Get Drop Position and assign Object Co-Ords, use e.GetPosition and find a relative Canvas Co-Ord (or check if its inside the canvas)
            _VM.AddObject(new InterstellaObject(DroppedObject));
        }
    }
}