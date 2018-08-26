using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ## Next in this class is to begin looking at implimenting Momentum and Gravity to create orbits.

// Apply Force too move this object rather than acceleration, this could involve some messing with the base 

// Updates in acceleration must by synchronised with ticks, other wise acceleration is not constant
// and SUVAT would not work (Would need calculus for variable accerlation which requires functions of motion)

namespace OrbitalSimulator_Objects
{
    public class InterstellaObject : BaseMovingObject
    {
        // !! View model should be moved to baseModelObject for consistancy with ViewModel
        InterstellaObjectViewModel _ViewModel;

        private Vector _Momentum;

        private Vector _ResultantForce;

        //kg
        private double _Mass;

        //kg/m^3
        private double _Density;

        private double _Radius;

        private InterstellaObjectType _Type;

        public Vector Momentum { get => _Momentum;}
        public Vector ResultantForce { get => _ResultantForce; set => _ResultantForce = value; }
        public double Radius { get => _Radius;}
        public InterstellaObjectType Type { get => _Type; }

        public double Mass
        {
            get => _Mass;
            set { _Mass = value; updateRadius(); NotifyPropertyChanged(this, nameof(Mass)); }
        }

        //View Model Only Encapsulted in public property for testing purposes, once a larger VM is in place this is unnessisary
        public InterstellaObjectViewModel ViewModel { get => _ViewModel;}

        public InterstellaObject(InterstellaObjectParams paramaters) : base(paramaters.Position, paramaters.Velocity, paramaters.Acceleration)
        {
            _Mass = paramaters.Mass.ToDouble();
            _Density = paramaters.Density;
            _ResultantForce = paramaters.Force;
            _Type = paramaters.Type;
            updateRadius();

            _ViewModel = new InterstellaObjectViewModel(this);
        }

        private void updateRadius()
        {
            // Volume = Mass / Denity
            // r = cube root((3V)/4 pi) 
            
            double volume = _Mass / _Density;
            _Radius = Math.Pow((3 * volume) / (4 * Math.PI), (1.0/3.0));
        }

        // Later functions will be needed to update Volume, Mass and density for UI Sliders 
        // To Update Other Values
        // This could be compliacted because of the relation between them, When a user updates Mass should density or volume change?

        // I think Volume should not be able to be changed by the user and should be representative of Mass And Density Changes.

        // There for Volume changes to accomodate changes in Mass / Density
    }
}
