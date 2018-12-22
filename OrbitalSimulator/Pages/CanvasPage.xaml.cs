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
        
        // ## FIX WHEN FILE LOADING Curently Loading Test Sytem in CPVM remove this later to add objects via the UI or file load 
        public CanvasPage() : base(new CanvasPageViewModel(true))
        {
            InitializeComponent();
        }

        // ## This is a tempoary solution to implimenting dragdrop untill i find a way to DragDrop without violating MVVM
        //    I have to control the operation here.
        private void dropNewObject(object sender, DragEventArgs e)
        {
            InterstellaObject DroppedObject = e.Data.GetData(typeof(InterstellaObject)) as InterstellaObject;
            if (DroppedObject == null) return;

            Point DropPoint = e.GetPosition((Canvas)sender);

            double LogicalX = CanvasPageViewModel.InverseSeperationScaler(DropPoint.X);
            double LogicalY = CanvasPageViewModel.InverseSeperationScaler(DropPoint.Y);

            double CentreDropX = LogicalX - CanvasPageViewModel.RadiusScale(DroppedObject.Radius);
            double CentreDropY = LogicalY - CanvasPageViewModel.RadiusScale(DroppedObject.Radius);

            DroppedObject.Position =new Vector(CentreDropX,CentreDropY);

            _VM.AddObject(new InterstellaObject(DroppedObject));
        }

        private void CanvasObject_DisplayDebug(object sender, MouseButtonEventArgs e)
        {
            InterstellaObjectViewModel ObjectVm = (InterstellaObjectViewModel)((Ellipse)sender).DataContext;
            // Parsing Object VM to the SideBarControl's Debug Pannel
            ((CanvasPageViewModel)DataContext).SideBarVM.InfoPannelVM.AddDisplayObject(ObjectVm);
        }

        // Later i will try implimenting 'inside' pick up and drop to change the location of items around the canvas. but for now lets work on forces
    }
}