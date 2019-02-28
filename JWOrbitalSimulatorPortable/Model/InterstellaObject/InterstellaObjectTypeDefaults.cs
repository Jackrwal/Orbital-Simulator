﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.Model
{
    public static class InterstellaObjectTypeDefaults
    {

        public static Dictionary<string,object> getDefaults(InterstellaObjectType type)
        {
            switch (type)
            {
                case InterstellaObjectType.Asteroid:
                    throw new NotImplementedException("Defaults for this object type are not implimented"); 

                case InterstellaObjectType.Comit:
                    throw new NotImplementedException("Defaults for this object type are not implimented"); 

                case InterstellaObjectType.Moon:
                    Dictionary<string, object> MoonDefaults = new Dictionary<string, object>();
                    MoonDefaults.Add("mass", MassConstants.MoonMass);
                    MoonDefaults.Add("radius", 1.737E6);
                    return MoonDefaults;

                case InterstellaObjectType.DwarfPlanet:
                    throw new NotImplementedException("Defaults for this object type are not implimented");

                case InterstellaObjectType.EarthSizedPlannet:

                    Dictionary<string, object> EarthDefaults = new Dictionary<string, object>();
                    EarthDefaults.Add("mass", MassConstants.EarthMass);
                    EarthDefaults.Add("radius", 6.37E6);
                    return EarthDefaults;

                case InterstellaObjectType.GasGiant:
                    throw new NotImplementedException("Defaults for this object type are not implimented");

                case InterstellaObjectType.Star:

                    Dictionary<string, object> StarDefaults = new Dictionary<string, object>();
                    StarDefaults.Add("mass", MassConstants.SunMass);
                    StarDefaults.Add("radius", 6.96E8);
                    return StarDefaults;

                case InterstellaObjectType.WhiteDwarf:
                    throw new NotImplementedException("Defaults for this object type are not implimented");

                case InterstellaObjectType.NeutronStar:
                    throw new NotImplementedException("Defaults for this object type are not implimented");

                case InterstellaObjectType.BlackHole:
                    throw new NotImplementedException("Defaults for this object type are not implimented");

                case InterstellaObjectType.Nebula:
                    throw new NotImplementedException("Defaults for this object type are not implimented");

                default:
                    return new Dictionary<string, object>();
            }
        }
    }


}