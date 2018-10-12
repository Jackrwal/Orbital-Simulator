using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ## To begin Implimenting Real Physics:

// Change Base Move to take all Vectors as parameters DONE
// Then Override the onTick in this class, calculating the acceleration from forces
// Handel All Physical Calculations in this class and parse the infomation impacting the movement of the model upto BaseMovingObject

// Use List Of every Interstella Object

// For Every Object:    ( This dosnt need an acctual Foreach because the onTick Method will be called on each object )
//   If Object in list has already been resolved and has a force acting on this object 
//   (Force will have been added to this Object's forces and because force of gravity is equal on each object nothing will be different here)
//   Then Ignore It as it has already been resolved

//   Else (Object is not already proccessed)

//      If (Check if the region of significant force of each object over lap)
//          Add To List Of Forces For this Object

//      Else ( Objects Exert no significant force on each other)
//          Ignore

//  Once All Objects have Been Proccessed Resolve All Forces on each object 
//  (It is now required for every force to be resolved on every object as each force will have different impacts on each object's resultant)
//  Find Acceleration From Mass and call the base Move Method

// To do ^This^ overide BaseMovingObject's onTick for interstellaObjects to Resolve All Forces Applied to that object, calculate the resulting Acceleration then pass this to the base
// Would I Pass the Acceration resulting of gravity to the Base as the new Acceleration ? I dont 'think' this would result in attempting to calculate acceleration allready

// Doing all of this could involve a 'ForcesEngine' class to effeciently calculate the Forces Acting on every object, And then objects must get their list of forces from here
// Although each Object Could store a list of every force acting on it which would be easier

namespace OrbitalSimulator_Objects
{
    public class InterstellaObject : BaseMovingObject
    {
        // ## View model should be moved to baseModelObject for consistancy with ViewModel
        InterstellaObjectViewModel _ViewModel;

        private const double G = 6.67E-11;

        // ## Define Radius of significant 

        private Vector _Momentum;

        //## Resultant Force will be evaluated from a number of gravitational forces 
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
           
            // Update the Radius of the cirlce baed on mass and density
            double volume = _Mass / _Density;
            _Radius = Math.Pow((3 * volume) / (4 * Math.PI), (1.0/3.0));
        }

        // Comparing Object Against Every Other Object (Although Realistic) brings a significant performance drop as more planets are included.
        // Must consider when using this how to decide which plannets it is nessesary to check against

        // Using functions for the motion of other objects i could evaluate which objects will come into the radius of the 'significant field'
        private double GetGrav()
        {
            throw new NotImplementedException("InterstellaObject L106");
        }

        /// <summary>
        /// Sets the Resultant Force equal to the sum of all individual forces acting on the object
        /// </summary>
        /// <param name="Forces"></param>
        private void RevolveForces(List<Vector> Forces)
        {
            throw new NotImplementedException("InterstellaObject L115");
        }

        // Later functions will be needed to update Volume, Mass and density for UI Sliders 
        // To Update Other Values
        // This could be compliacted because of the relation between them, When a user updates Mass should density or volume change?

        // I think Volume should not be able to be changed by the user and should be representative of Mass And Density Changes.

        // There for Volume changes to accomodate changes in Mass / Density
    }
}
