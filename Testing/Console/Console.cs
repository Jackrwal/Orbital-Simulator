using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrbitalSimulator_Objects;

namespace OrbitalSimulator_Testing_Console
{
    class Console
    {
        static void Main(string[] args)
        {
            // ## This returns string '5.927E+24', Maybe add a new Class for scientific format with a ToString override to return this,
            // But a value which can be used for calculatons
            System.Console.WriteLine(MassConstants.EarthMass);
            System.Console.WriteLine(5.792 * Math.Pow(10, 24));
            System.Console.ReadLine();

            
        }
    }
}
