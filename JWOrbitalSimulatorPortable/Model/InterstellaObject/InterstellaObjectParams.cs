namespace JWOrbitalSimulatorPortable.Model
{
    /// <summary>
    /// Stores the infomation required to construct an instance of InterstellarObject
    /// </summary>
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

        public InterstellaObjectParams()
        {
            Position = new Vector(0, 0);
            Velocity = new Vector(0, 0);
            Acceleration = new Vector(0, 0);
            Mass = (double)InterstellaObjectTypeDefaults.getDefaults(InterstellaObjectType.Star)["mass"];
            Radius = (double)InterstellaObjectTypeDefaults.getDefaults(InterstellaObjectType.Star)["radius"];
            Type = InterstellaObjectType.Star;
        }

        // Auto-Properties used to construct Interstellar Object
        public Vector Position { get; set;  }
        public Vector Velocity { get; set;  }
        public Vector Acceleration { get; set;  }
        public double Mass { get; set; }
        public double Radius { get; set; }
        public InterstellaObjectType Type { get; set; }
    }
}