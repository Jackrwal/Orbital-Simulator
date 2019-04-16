namespace JWOrbitalSimulatorPortable.Model
{
    /// <summary>
    /// Gives an instance of each object that can be drag dropped from the sidebar.
    /// </summary>
    static public class CanvasSideBarObjects
    {
        // Set of params are defined for each default object
        private static InterstellaObjectParams DefaultStarParams => new InterstellaObjectParams(0, 0, 0, 0, 0, 0, InterstellaObjectType.Star);

        // The default instance is returned as a new instance of the default params.
        public static InterstellaObject StarInstance { get; } = new InterstellaObject(DefaultStarParams);

        private static InterstellaObjectParams DefaultRockyPlannetParams => new InterstellaObjectParams(0, 0, 0, 0, 0, 0, InterstellaObjectType.RockyPlanet);
        public static InterstellaObject RockyPlannetInstance { get; } = new InterstellaObject(DefaultRockyPlannetParams);

        private static InterstellaObjectParams DefaultEarthParams => new InterstellaObjectParams(0, 0, 0, 0, 0, 0, InterstellaObjectType.EarthSizedPlannet);
        public static InterstellaObject EarthInstance { get; } = new InterstellaObject(DefaultEarthParams);

        private static InterstellaObjectParams DefaultMoonParams => new InterstellaObjectParams(0, 0, 0, 0, 0, 0, InterstellaObjectType.Moon);
        public static InterstellaObject MoonInstance { get; } = new InterstellaObject(DefaultMoonParams);

        private static InterstellaObjectParams DefaultGasGiantParams => new InterstellaObjectParams(0, 0, 0, 0, 0, 0, InterstellaObjectType.GasGiant);
        public static InterstellaObject GasGiantInstance { get; } = new InterstellaObject(DefaultGasGiantParams);

        private static InterstellaObjectParams DefaultIceGiantParams => new InterstellaObjectParams(0, 0, 0, 0, 0, 0, InterstellaObjectType.IceGiant);
        public static InterstellaObject IceGiantInstance { get; } = new InterstellaObject(DefaultIceGiantParams);

        private static InterstellaObjectParams DefaultAstaroidParams => new InterstellaObjectParams(0, 0, 0, 0, 0, 0, InterstellaObjectType.Asteroid);
        public static InterstellaObject AstaroidInstance { get; } = new InterstellaObject(DefaultAstaroidParams);

        private static InterstellaObjectParams DefaultDwarfPlannetParams => new InterstellaObjectParams(0, 0, 0, 0, 0, 0, InterstellaObjectType.DwarfPlanet);
        public static InterstellaObject DwarfPlannetInstance { get; } = new InterstellaObject(DefaultDwarfPlannetParams);
    }
}
