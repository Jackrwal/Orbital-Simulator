using OrbitalSimulator_Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Threading;

namespace OrbitalSimulator_Testing_Console
{
    class Console
    {
        static DispatcherTimer myTimer = new DispatcherTimer();

        static InterstellaSystemViewModel SystemVM;

        static void Main(string[] args)
        {
            InterstellaObject myObj = new InterstellaObject(
                new InterstellaObjectParams(
                        new Vector(15,15),
                        new Vector(1,0),
                        new Vector(0,0),
                        InterstellaObjectType.EarthSizedPlannet
                    )
                );

            InterstellaSystem mySystem = new InterstellaSystem( new List<InterstellaObject>() { myObj } );

            SystemVM = new InterstellaSystemViewModel(mySystem);

            foreach (var item in SystemVM.ObjectVMs)
            {
                System.Console.SetCursorPosition((int)item.X, (int)item.Y);
                System.Console.WriteLine("X");
            }

            myTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            myTimer.Tick += MyTimer_Tick;
            myTimer.Start();    

            SystemVM.StartSystem();
        }

        private static void MyTimer_Tick(object sender, EventArgs e)
        {
            System.Console.WriteLine("U");

            foreach (var item in SystemVM.ObjectVMs)
            {
                System.Console.SetCursorPosition((int)item.X, (int)item.Y);
                System.Console.WriteLine("X");
            }
        }
    }

}
