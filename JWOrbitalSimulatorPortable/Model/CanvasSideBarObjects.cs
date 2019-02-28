using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.Model
{
    static public class CanvasSideBarObjects
    {
        private static InterstellaObjectParams DefaultEarthParams => new InterstellaObjectParams(0, 0, 0, 0, 0, 0, InterstellaObjectType.EarthSizedPlannet);
        public static InterstellaObject EarthInstance { get; } = new InterstellaObject(DefaultEarthParams);

        private static InterstellaObjectParams DefaultStarParams => new InterstellaObjectParams(0, 0, 0, 0, 0, 0, InterstellaObjectType.Star);
        public static InterstellaObject StarInstance { get; } = new InterstellaObject(DefaultStarParams);

        private static InterstellaObjectParams DefaultMoonParams => new InterstellaObjectParams(0, 0, 0, 0, 0, 0, InterstellaObjectType.Moon);
        public static InterstellaObject MoonInstance { get; } = new InterstellaObject(DefaultMoonParams);

    }
}
