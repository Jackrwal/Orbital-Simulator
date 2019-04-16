using JWOrbitalSimulatorPortable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    // ## Originonally i wanted this box to appear when an object was hovered over to display details, then allow data to be entered when the object is clicked.
    //    However due to changes in UI design choices and time limitations the box will not appear when an object is hovered over.
    public class HoverDataEntryBoxViewModel : PopUpControlViewModel
    {
        // Screen XY point to display the data box at.
        public Point ScreenXY { get; set; }

        // Type string of the data object
        public string ObjectType => DataObjectVM?.InterstellaObject.Type.ToString();

        // Position and velocity strings of the type object
        public string Position => DataObjectVM?.ScrPositionString;
        public string Velocity => DataObjectVM?.VelocityString;

        public string Radius
        {
            get { return DataObjectVM?.Radius.ToString(); }
            set
            {
                if (validateQuantity(value, typeof(double))) DataObjectVM.Radius = Convert.ToDouble(value);
            }
        }

        public string Mass
        {
            get { return DataObjectVM?.Mass.ToString(); }
            set
            {
                if (validateQuantity(value, typeof(double))) DataObjectVM.Mass = Convert.ToDouble(value);
            }
        }

        private string _RadiusBoxTextColour = "White";
        public string RadiusBoxTextColour { get { return _RadiusBoxTextColour;  } set { _RadiusBoxTextColour = value; NotifyPropertyChanged(this, nameof(RadiusBoxTextColour)); } }

        private string _MassBoxTextColour = "White";
        public string MassBoxTextColour { get { return _MassBoxTextColour; } set { _MassBoxTextColour = value; NotifyPropertyChanged(this, nameof(MassBoxTextColour)); } }


        public InterstellaObjectViewModel DataObjectVM;

        public HoverDataEntryBoxViewModel(double dimensionsToWindowSizeWighting) : base(dimensionsToWindowSizeWighting)
        {
        }

        public void DisplayEntryBox(InterstellaObjectViewModel dataObjectVM)
        {
            DataObjectVM = dataObjectVM;
            notifyOnDataObjectChanged();

            Show();

            //ScreenXY = new Point(dataObjectVM.ScreenPosition.X+CanvasPageViewModel.Instance.SideBarWidth, dataObjectVM.ScreenPosition.Y);

            ScreenXY = new Point(dataObjectVM.ScreenPosition.X + CanvasPageViewModel.Instance.SideBarWidth + DataObjectVM.Width/2, dataObjectVM.ScreenPosition.Y + DataObjectVM.Height / 2);
            NotifyPropertyChanged(this, nameof(ScreenXY));
        }

        static public bool ValidateField(string quantity, FieldType field)
        {
            switch (field)
            {
                case FieldType.Radius:

                    if (validateQuantity(quantity, typeof(double)))
                    {
                        CanvasPageViewModel.Instance.DataBoxVM.RadiusBoxTextColour = "White";
                        return true;
                    }
                    else
                    {
                        CanvasPageViewModel.Instance.DataBoxVM.RadiusBoxTextColour = "Red";
                        return false;
                    }
                        


                case FieldType.Mass:
                    if (validateQuantity(quantity, typeof(double)))
                    {
                        CanvasPageViewModel.Instance.DataBoxVM.MassBoxTextColour = "White";
                        return true;
                    }
                    else
                    {
                        CanvasPageViewModel.Instance.DataBoxVM.MassBoxTextColour = "Red";
                        return false;
                    }

                default:
                    return false;
            }
        }

        // Take a user entered string and validate it parses too a double
        static private bool validateQuantity(object quantity, Type desiredType)
        {
            object value;
            try
            {
                value = Convert.ChangeType(quantity, desiredType);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }

        private void notifyOnDataObjectChanged()
        {
            NotifyPropertyChanged(this, nameof(ObjectType));
            NotifyPropertyChanged(this, nameof(Mass));
            NotifyPropertyChanged(this, nameof(Radius));
            NotifyPropertyChanged(this, nameof(Position));
            NotifyPropertyChanged(this, nameof(Velocity));
        }

        public enum FieldType
        {
            Radius,
            Mass
        }
    }
}
