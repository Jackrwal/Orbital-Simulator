using JWOrbitalSimulatorPortable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.Model
{
    public class DemoSystemModel
    {
        public static InterstellaSystem DemoInstance;

        public static InterstellaSystem GetDemoInstance => DemoInstance?? new InterstellaSystem(DemoObjects);

        public static List<InterstellaObject> DemoObjects => new List<InterstellaObject>(DemoParams.Select((Params) => new InterstellaObject(Params)));
        
        private static List<InterstellaObjectParams> DemoParams = new List<InterstellaObjectParams>()
        {
            // The Sun
            new InterstellaObjectParams
            (
                new Vector(0, 0),
                new Vector(0, 0),
                new Vector(0, 0),
                InterstellaObjectType.Star,
                SolarConstants.SunMass,
                7E8
            ),

            // Mercury
            new InterstellaObjectParams
            (
                new Vector(SolarConstants.AU * 0.387098, 0),
                new Vector(0, 47000),
                new Vector(0,0),
                InterstellaObjectType.Mercury,
                3.3E23,
                2.440E6
            ),

            // Venus
            new InterstellaObjectParams
            (
                new Vector(SolarConstants.AU * 0.723332, 0),
                new Vector(0, 35000),
                new Vector(0,0),
                InterstellaObjectType.Venus,
                4.9E24,
                6.052E6
            ),
                                                                                                                                                                
            // The Earth
            new InterstellaObjectParams
            (
                new Vector(SolarConstants.AU, 0),
                new Vector(0, 30000),
                new Vector(0,0),
                InterstellaObjectType.EarthSizedPlannet,
                SolarConstants.EarthMass,
                6.4E6
            ),

            // The Moon
            new InterstellaObjectParams
            (
                new Vector(SolarConstants.AU + 0.00257 * SolarConstants.AU, 0),
                new Vector(0, 30E3 + 1.022E3),
                new Vector(0,0),
                InterstellaObjectType.Moon,
                7.342E22,
                1.7371E6
            ),

            // Mars
            new InterstellaObjectParams
            (
                new Vector(SolarConstants.AU * 1.523679, 0),
                new Vector(0, 24007),
                new Vector(0,0),
                InterstellaObjectType.Mars,
                6.4171E23,
                3.389E6
            ),

            // Ceres
            new InterstellaObjectParams
            (
                new Vector(SolarConstants.AU * 2.7675, 0),
                new Vector(0, 17.905E3),
                new Vector(0,0),
                InterstellaObjectType.Asteroid,
                9.393E20,
                4.73E5
            ),

            // Vesta
            new InterstellaObjectParams
            (
                new Vector(SolarConstants.AU * 2.36179 , 0),
                new Vector(0, 19.34E3),
                new Vector(0,0),
                InterstellaObjectType.Asteroid,
                2.59076E20,
                2.63E5
            ),

            // Pallas
            new InterstellaObjectParams
            (
                new Vector(SolarConstants.AU * 2.7728, 0),
                new Vector(0, 17924),
                new Vector(0,0),
                InterstellaObjectType.Asteroid,
                2.11E20,
                2.50E5
            ),

            // Hygiea
            new InterstellaObjectParams
            (
                new Vector(SolarConstants.AU * 3.1421, 0),
                new Vector(0, 16.76E3),
                new Vector(0,0),
                InterstellaObjectType.Asteroid,
                8.67E19,
                2.15E5
            ),

            //Jupiter
            new InterstellaObjectParams
            (
                new Vector(SolarConstants.AU * 5.2044, 0),
                new Vector(0, 13070),
                new Vector(0,0),
                InterstellaObjectType.Jupitor,
                1.8982E27,
                69911E3
            ),

            //Saturn
            new InterstellaObjectParams
            (
                new Vector(SolarConstants.AU * 9.5826, 0),
                new Vector(0, 9.68E3),
                new Vector(0,0),
                InterstellaObjectType.Saturn,
                5.6834E26,
                5.8232E7
            ),

            //Uranus
            new InterstellaObjectParams
            (
                new Vector(SolarConstants.AU * 19.2184, 0),
                new Vector(0, 6.8E3),
                new Vector(0,0),
                InterstellaObjectType.Uranus,
                8.6810E25,
                2.5362E7
            ),

            // Neptune
            new InterstellaObjectParams
            (
                new Vector(SolarConstants.AU * 30.11, 0),
                new Vector(0, 5.43E3),
                new Vector(0,0),
                InterstellaObjectType.Neptune,
                1.02413E26,
                2.4622E7
            ),

            //Pluto
            new InterstellaObjectParams
            (
                new Vector(SolarConstants.AU * 39.48, 0),
                new Vector(0, 4.67E3),
                new Vector(0,0),
                InterstellaObjectType.DwarfPlanet,
                1.303E22,
                1.1883E6
            )

        };
    }
}
