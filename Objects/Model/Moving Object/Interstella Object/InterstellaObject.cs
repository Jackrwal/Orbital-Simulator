﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// !! Implimenting Real Physics:

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
// Update, Force caclulations can be supported in the System.


// !! So Something i read at https://stackoverflow.com/questions/15439841/mvvm-in-wpf-how-to-alert-viewmodel-of-changes-in-model-or-should-i 
//    got my thinking about how this should all work and has the potential to solve my problems of updating the ViewModel from the Model AND where the timer should reside
//    This suggested that the Model, being purly data, should not contain methods and that these methods should be carried out in the view model. This leads to the solution of

//    All Methods for updating the data moves to the View Model Layer, the VM there for knows when data is being updated and can notify the UI
//    This leads to putting the clock in MainWindow, Then system updates are driven from the view along with any user interactions that involve updating the model
//    This makes the program fully driven by the UI

//   Other solution is to continue making the System Updates Model driven, keeping the clock in the Model and making the View Model Subscribe too the model 
//   so that It can update the UI that changes have occured in the model that have changed the values that the ViewModel Properties Get; from


// !! Time to change some stuff:

//    The Program Builds Main Window First, SO the View must be initaised first
//    Then the View Initalises the ViewModel by initialising WindowViewModel
//    Now currently the View model is temporarly populating a model for testing purposes
//    This is closer to the correcting thing to do than i thought it was, The View Model should initialsie the Model,
//    This is because there is no data to initialsie the model with until the User gives a system to load (View->ViewModel->Update the model)
//    This means that the clock should be run from the CanvasPage ViewModel
//    And potentially means that methods should be moved from the Model Into the ViewModel (As Actions are carried out on the model when the view prompts it)
//    The only problem with this is how the inheritence between MovingObject->InterstellaObject would work with functionality moved to the VM Layer

namespace OrbitalSimulator_Objects
{
    public class InterstellaObject : BaseMovingObject
    {
        private const double G = 6.67E-11;

        // ## Define Radius of significant area of influence

        //## Resultant Force will be evaluated from a number of gravitational forces 
        private Vector _ResultantForce;

        //kg
        private double _Mass;

        //kg/m^3
        private double _Density;

        private double _Radius;

        private InterstellaObjectType _Type;

        public Vector ResultantForce { get => _ResultantForce; set => _ResultantForce = value; }
        public double Radius { get => _Radius; } 
        public InterstellaObjectType Type { get => _Type; }

        public double Mass
        {
            get => _Mass;
            set { _Mass = value; updateRadius(); }
        }

        //View Model Only Encapsulted in public property for testing purposes, once a larger VM is in place this is unnessisary

        public InterstellaObject(InterstellaObjectParams paramaters) : base(paramaters.Position, paramaters.Velocity, paramaters.Acceleration)
        {
            _Mass = paramaters.Mass.ToDouble();
            _Density = paramaters.Density;
            _ResultantForce = paramaters.Force;
            _Type = paramaters.Type;
            updateRadius();
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
        /// <param name="forces"></param>
        private void RevolveForces(List<Vector> forces)
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
