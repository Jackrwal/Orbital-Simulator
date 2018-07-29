using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalSimulator_Objects
{
    public static class MassConstants
    {
        const double _EarthMass = 5.972E+24;

        public static double EarthMass => _EarthMass;

        //public static readonly ScientificNotationValue EarthMass2 = new ScientificNotationValue(5.972, 24);
    }
}
