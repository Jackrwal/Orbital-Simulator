using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalSimulator_Objects
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
                    throw new NotImplementedException("Defaults for this object type are not implimented"); 

                case InterstellaObjectType.DwarfPlanet:
                    throw new NotImplementedException("Defaults for this object type are not implimented");

                case InterstellaObjectType.EarthSizedPlannet:

                    Dictionary<string, object> defaults = new Dictionary<string, object>();
                    defaults.Add("mass", MassConstants.EarthMass);
                    defaults.Add("density", 5510D);
                    return defaults;

                case InterstellaObjectType.GasGiant:
                    throw new NotImplementedException("Defaults for this object type are not implimented");

                case InterstellaObjectType.Star:
                    throw new NotImplementedException("Defaults for this object type are not implimented");

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