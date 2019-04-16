using System;

namespace JWOrbitalSimulatorPortable.Model
{
    /// <summary>
    /// Represents a magnitude in a given 2D direction. 
    /// </summary>
    public class Vector
    {
        // Store Vector as X and Y Components
        public double X { get; set; }
        public double Y { get; set; }

        //Construct a vector with a double for each component
        public Vector(double horizontalVector, double verticalVector)
        {
            X = horizontalVector;
            Y = verticalVector;
        }

        // construct a copy of a given vector
        public Vector(Vector V)
        {
            X = V.X;
            Y = V.Y;
        }

        // Return Magnitude Using the pythagorus of the components
        public double Magnitude => Math.Sqrt((Math.Pow(X, 2)) + (Math.Pow(Y, 2))); 

        // To return the normal swap the X and Y component
        public Vector Normal  => new Vector(Y, X);

        //Vector Addition operator
        public static Vector operator +(Vector V1, Vector V2)
            => new Vector(V1.X + V2.X, V1.Y + V2.Y);

        //Vector Subtraction operator
        public static Vector operator -(Vector V1, Vector V2)
            => new Vector(V1.X - V2.X, V1.Y - V2.Y);

        //Vector Subtract scaler
        public static Vector operator -(Vector V1, double s)
            => new Vector(V1.X - s, V1.Y - s);

        public static Vector operator +(Vector V1, double s)
            => new Vector(V1.X + s, V1.Y + s);

        //Vector mulitplication Scaler Operatoer
        public static Vector operator *(Vector V1, double Multiplier)
            => new Vector(V1.X * Multiplier, V1.Y * Multiplier);

        //Vector mulitplication Scaler Operatoer
        public static Vector operator *(double Multiplier, Vector V1)
            => new Vector(V1.X * Multiplier, V1.Y * Multiplier);

        //Vector division Scaler Operatoer
        public static Vector operator /(Vector V1, double Multiplier)
            => new Vector(V1.X / Multiplier, V1.Y / Multiplier);

        // implicitly convert a touple into a new vector
        public static implicit operator Vector((double, double) v) => new Vector(v.Item1, v.Item2);

        // Return the vector as a string in i j notation
        public override string ToString() => $"({X}i + {Y}j)";
    }
}


