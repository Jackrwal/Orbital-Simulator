using System;

namespace JWOrbitalSimulatorPortable.Model
{
    /// <summary>
    /// Impliments the basic functionality for an object to move from one position to another of calculated displacment from the first.
    /// Based on stored velocity, acceleration. And given elaspsed time
    /// </summary>
    public class MovingObject
    {
        // The current position, velocity and acceleration of the object.
        private Vector _Position, _Velocity, _Acceleration;

        /// <summary>
        /// Construct a moving object at a given position with given velocity and acceleration
        /// </summary>
        public MovingObject(Vector position, Vector velocity, Vector acceleration)
        {
            _Position = position;
            _Velocity = velocity;
            _Acceleration = acceleration;
        }

        // Public Encapsulation of vector properties
        public Vector Position { get => _Position; set => _Position = value; }
        public Vector Velocity { get => _Velocity; set => _Velocity = value;  }
        public Vector Acceleration { get => _Acceleration; set => _Acceleration = value; }

        /// <summary>
        /// Rasied when the object is updated. 
        /// Subscribed to by view models to notify the view that the objects position may have changed
        /// </summary>
        public event EventHandler ObjectUpdated;

        protected void move(double elapsedSeconds, Vector accleration = null)
        {
            // If acceleration has not been given assume the object's acceleration has not been changed from the last update
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
