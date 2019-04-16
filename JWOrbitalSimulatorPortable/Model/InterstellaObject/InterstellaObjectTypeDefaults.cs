using System;
using System.Collections.Generic;

namespace JWOrbitalSimulatorPortable.Model
{
    /// <summary>
    /// Static class to provide default values for each InterstellarObjectType
    /// </summary>
    public static class InterstellaObjectTypeDefaults
    {
        /// <summary>
        /// Gets the set defaults for an interstellarObjectType
        /// </summary>
        /// <param name="type"></param>
        /// <returns>a dictionary of the defaults keys and values</returns>
        public static Dictionary<string,object> getDefaults(InterstellaObjectType type)
        {
            switch (type)
            {
                case InterstellaObjectType.Star:

                    Dictionary<string, object> StarDefaults = new Dictionary<string, object>();
                    StarDefaults.Add("mass", SolarSystemConstants.SunMass);
                    StarDefaults.Add("radius", 6.96E8);
                    return StarDefaults;

                case InterstellaObjectType.RockyPlanet:

                    Dictionary<string, object> RockyPlannetDefaults = new Dictionary<string, object>();
                    RockyPlannetDefaults.Add("mass", 4.9E24);
                    RockyPlannetDefaults.Add("radius", 6.052E6);
                    return RockyPlannetDefaults;

                case InterstellaObjectType.Moon:

                    Dictionary<string, object> MoonDefaults = new Dictionary<string, object>();
                    MoonDefaults.Add("mass", SolarSystemConstants.MoonMass);
                    MoonDefaults.Add("radius", 1.737E6);
                    return MoonDefaults;
                
                case InterstellaObjectType.EarthSizedPlannet:

                    Dictionary<string, object> EarthDefaults = new Dictionary<string, object>();
                    EarthDefaults.Add("mass", SolarSystemConstants.EarthMass);
                    EarthDefaults.Add("radius", 6.37E6);
                    return EarthDefaults;

                case InterstellaObjectType.GasGiant:

                    Dictionary<string, object> GasGiantDefaults = new Dictionary<string, object>();
                    GasGiantDefaults.Add("mass", 1.8982E27);
                    GasGiantDefaults.Add("radius", 69911E3);
                    return GasGiantDefaults;

                case InterstellaObjectType.IceGiant:

                    Dictionary<string, object> IceGiantDefaults = new Dictionary<string, object>();
                    IceGiantDefaults.Add("mass", 8.6810E25);
                    IceGiantDefaults.Add("radius", 2.5362E7);
                    return IceGiantDefaults;

                case InterstellaObjectType.Asteroid:

                    Dictionary<string, object> AsteroidDefaults = new Dictionary<string, object>();
                    AsteroidDefaults.Add("mass", 9.393E20);
                    AsteroidDefaults.Add("radius", 4.73E5);
                    return AsteroidDefaults;

                case InterstellaObjectType.DwarfPlanet:

                    Dictionary<string, object> DwarfPlanetDefaults = new Dictionary<string, object>();
                    DwarfPlanetDefaults.Add("mass", 1.303E22);
                    DwarfPlanetDefaults.Add("radius", 1.1883E6);
                    return DwarfPlanetDefaults;

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}