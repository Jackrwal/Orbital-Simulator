using OrbitalSimulator_Objects;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Threading;

namespace OrbitalSimulator_Testing_Console
{
    class Console
    {
        void testingmethod() { }


        static void Main(string[] args)
        {
            //Dictionary<string,object> Defaults = InterstellaObjectTypeDefaults.getDefaults(InterstellaObjectType.EarthSizedPlannet);
            //foreach (string Key in Defaults.Keys)
            //{
            //    System.Console.WriteLine($"{Key}: {Defaults[Key]}");
            //}

            //ScientificNotationValue DummySNV = new ScientificNotationValue(100);

            //System.Console.WriteLine(DummySNV);
            //System.Console.WriteLine(DummySNV.GetType());

            InterstellaObject myObj = new InterstellaObject(
                new InterstellaObjectParams(
                        new Vector(0,0),
                        new Vector(2,0),
                        new Vector(0,0),
                        InterstellaObjectType.EarthSizedPlannet
                    )
                );

            DispatcherTimer Ticker = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 24) };

            //Ticker.Tick += InterstellaObject.OnTick(null, new EventArgs());

            System.Console.ReadLine();            
        }
    }

}
