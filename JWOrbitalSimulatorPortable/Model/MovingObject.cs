using JWOrbitalSimulatorPortable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.Model
{
    public class MovingObject : NotifyingModelObject
    {
        private Vector _Position, _Velocity, _Acceleration;

        public MovingObject(Vector position, Vector velocity, Vector accelleration)
        {
            _Position = position;
            _Velocity = velocity;
            _Acceleration = accelleration;
        }

        public Vector Position { get => _Position; set { _Position = value; NotifyPropertyChanged(this, nameof(Position)); } }
        public Vector Velocity { get => _Velocity; set { _Velocity = value; NotifyPropertyChanged(this, nameof(Velocity)); } }
        public Vector Accelleration { get => _Acceleration; set { _Acceleration = value; NotifyPropertyChanged(this, nameof(Accelleration)); } }

        public event EventHandler ObjectUpdated;

        protected void move(double elapsedSeconds, Vector accleration = null)
        {
            if(accleration != null) { _Acceleration = accleration; }

            // Update the Object's Velocity acording to its accleration in the given frame
            _Velocity += _Acceleration * elapsedSeconds; 
 
            // Then Update the object to its new position acordingly
            _Position += _Velocity * elapsedSeconds;


            // Invokes Update Event so any subscribed objects know the object's position has updated.
            ObjectUpdated(this, new EventArgs());
        }
    }
}