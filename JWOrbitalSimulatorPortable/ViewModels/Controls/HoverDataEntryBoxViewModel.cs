using JWOrbitalSimulatorPortable.Model;
using System;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    /// <summary>
    /// View Model controling the Hover Data Entry Box Control
    /// </summary>
    public class HoverDataEntryBoxViewModel : NotifyingViewModel
    {
        // The data object reflected and edited by the control.
        public InterstellaObjectViewModel DataObjectVM;

        // Controls whether the databox is visible
        public bool Visibility { get; set; } = false;

        // Width and Height of the data box
        public int Width { get; set; }
        public int Height { get; set; }

        // Screen XY point to display the data box at.
        public Vector ScreenXY { get; set; }

        // Type string of the object given by the DtaObject's type if data object is set
        public string ObjectType => DataObjectVM?.InterstellaObject.Type.ToString();

        // Position and velocity strings of the DataObject, if the Object is set.
        public string Position => DataObjectVM?.ScrPositionString;
        public string Velocity => DataObjectVM?.VelocityString;

        // Radius of the DataObject. This is a validated field.
        public string Radius
        {
            // Given by the radius of the data object if it is set.
            get { return DataObjectVM?.Radius.ToString(); }
            set
            {
                // If given value is valid set to DataObject's radius
                if (validateQuantity(value, typeof(double), out object Val)) DataObjectVM.Radius = (double)Val;
            }
        }

        // Mass of the DataObject. This is a validated field.
        public string Mass
        {
            // Given by the radius of the data object if it is set.
            get { return DataObjectVM?.Mass.ToString(); }
            set
            {
                // If given value is valid set to DataObject's mass
                if (validateQuantity(value, typeof(double), out object Val)) DataObjectVM.Mass = (double)Val;
            }
        }

        // The Colour of Text in the radius text box
        private string _RadiusBoxTextColour = "White";
        public string RadiusBoxTextColour { get { return _RadiusBoxTextColour;  } set { _RadiusBoxTextColour = value; NotifyPropertyChanged(this, nameof(RadiusBoxTextColour)); } }

        // The Colour of Text in the mass text box
        private string _MassBoxTextColour = "White";
        public string MassBoxTextColour { get { return _MassBoxTextColour; } set { _MassBoxTextColour = value; NotifyPropertyChanged(this, nameof(MassBoxTextColour)); } }

        /// <summary>
        /// Construct a viewmodel for the Hover Data Entry View Model
        /// </summary>
        /// <param name="dimensionsToWindowSizeWighting">>Percentage of overal window space for the control to take up</param>
        public HoverDataEntryBoxViewModel(double dimensionsToWindowSizeWighting)
        {
            // Set W/H relative to canvas size.
            Width = (int)(CanvasPageViewModel.Instance.CanvasWidth * dimensionsToWindowSizeWighting);
            Height = (int)(CanvasPageViewModel.Instance.CanvasHeight * dimensionsToWindowSizeWighting);
        }

        /// <summary>
        /// Display the data entry box over an object and display infomation about that object.
        /// </summary>
        /// <param name="dataObjectVM">Object to display infomation about</param>
        public void DisplayEntryBox(InterstellaObjectViewModel dataObjectVM)
        {
            // Set the data Object VM and notify of changed properties
            DataObjectVM = dataObjectVM;
            notifyOnDataObjectChanged();

            // Make the control visible
            Visibility = true;
            NotifyPropertyChanged(this, nameof(Visibility));

            // Set the screen position of the control.
            ScreenXY = new Vector(
                dataObjectVM.ScreenPosition.X + CanvasPageViewModel.Instance.SideBarWidth + DataObjectVM.Width/2,
                dataObjectVM.ScreenPosition.Y + DataObjectVM.Height / 2
                );

            NotifyPropertyChanged(this, nameof(ScreenXY));
        }

        /// <summary>
        /// Set the control to be hidden
        /// </summary>
        public void HideBox()
        {
            Visibility = false;
            NotifyPropertyChanged(this, nameof(Visibility));
        }

        /// <summary>
        /// Validate a given field of the control.
        /// </summary>
        /// <param name="quantity">Value to validate</param>
        /// <param name="field">The field to validate against</param>
        /// <returns></returns>
        static public bool ValidateField(string quantity, FieldType field, out object value)
        {
            switch (field)
            {
                case FieldType.Radius:

                    if (validateQuantity(quantity, typeof(double), out value))
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
                    if (validateQuantity(quantity, typeof(double), out value))
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
                    value = null;
                    return false;
            }
        }

        /// <summary>
        /// Validate a quantity against a given type
        /// </summary>
        /// <param name="quantity">Quantity to validate</param>
        /// <param name="desiredType">Type to attempt to cast to</param>
        /// <param name="value">Casted value if sucessfull</param>
        static private bool validateQuantity(object quantity, Type desiredType, out object value)
        {
            try
            {
                value = Convert.ChangeType(quantity, desiredType);
            }
            catch (FormatException)
            {
                value = null;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Notify the view of the properties changed on setting DataObject
        /// </summary>
        private void notifyOnDataObjectChanged()
        {
            NotifyPropertyChanged(this, nameof(ObjectType));
            NotifyPropertyChanged(this, nameof(Mass));
            NotifyPropertyChanged(this, nameof(Radius));
            NotifyPropertyChanged(this, nameof(Position));
            NotifyPropertyChanged(this, nameof(Velocity));
        }

        /// <summary>
        /// The Fields of the control requiring validation
        /// </summary>
        public enum FieldType
        {
            Radius,
            Mass
        }
    }
}
