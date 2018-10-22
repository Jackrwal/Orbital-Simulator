using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalSimulator_Objects
{
    public static class CollisionsHelper
    {
        // Create a Static Generic Method
        // Create an Interface that must be implimented by the objects we check for collision 

        // Interface forces items to impliment an X, Y Position (virtual so it can be overrided and renamed), a radius for circle circle collission
        // and potentially a method to get the next point

        // For a Cirlce Two Objects have colided is the Sum of their radii, is >= the distance between the centers 
        
        // For Circle Point, if the distance between the center and the point is <= the radius the point is in the circle
    }
}
