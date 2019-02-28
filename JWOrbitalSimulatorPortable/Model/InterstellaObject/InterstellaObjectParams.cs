using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.Model
{
    public class InterstellaObjectParams
    {
        // Chained Constructor to convert Vector components into Vectors for main constructor
        public InterstellaObjectParams
            (
                double xPosition,
                double yPosition,
                double xVelocity,
                double yVelocity,
                double xAcceleration,
                double yAcceleration,
                InterstellaObjectType type,
                double mass = double.NaN,
                double radius = double.NaN
            ) :
            this
            (
                new Vector(xPosition, yPosition),
                new Vector(xVelocity, yVelocity),
                new Vector(xAcceleration, yAcceleration),
                type,
                mass,
                radius
            )
        { }

        // Main Constructor using Vector paramaters
        public InterstellaObjectParams
            (
                Vector position,
                Vector velocity,
                Vector acceleration, 
                InterstellaObjectType type, 
                double mass = double.NaN, 
                double radius = double.NaN
            )
        {
        
            // If mass or density are not supplied get their default values from Defaults
            if (double.IsNaN(mass)) mass = (double)InterstellaObjectTypeDefaults.getDefaults(type)["mass"];

            if (double.IsNaN(radius)) radius = (double)InterstellaObjectTypeDefaults.getDefaults(type)["radius"];
            

            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
            Mass = mass;
            Radius = radius;
            Type = type; 

        }

        public Vector Position { get; }
        public Vector Velocity { get; }
        public Vector Acceleration { get; }
        public double Mass { get; }
        public double Radius { get; }
        public InterstellaObjectType Type { get; }
    }
}
