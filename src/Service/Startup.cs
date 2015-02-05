using System;
using System.Collections.Generic;
using System.Diagnostics;
using Contracts;
using Models;
using NPoco;
using NServiceBus;

namespace Service
{
    public class Startup : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            
            using (var db = new Database("connstr"))
            {
                var widgets = new List<Widget>();
                db.Execute("DELETE FROM Widget");
                db.Execute("DELETE FROM Component_A");
                db.Execute("DELETE FROM Component_B");

                for (var o = 0; o < 100; o++)
                {
                    for (var i = 0; i < 10000; i++)
                    {
                        widgets.Add(new Widget()
                        {
                            id = Guid.NewGuid(),
                            Name = DateTime.Now.Ticks.ToString(),
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        });
                    }
                        
                    db.InsertBulk(widgets);
                    widgets.Clear();
                }

                var componentsA = new List<Component_A>();
                for (var o = 0; o < 100; o++)
                {
                    for (var i = 0; i < 10000; i++)
                    {
                        componentsA.Add(new Component_A() {id = Guid.NewGuid()});
                    }

                    db.InsertBulk(componentsA);
                    componentsA.Clear();
                }

                var componentsB = new List<Component_B>();
                for (var o = 0; o < 100; o++)
                {
                    for (var i = 0; i < 10000; i++)
                    {
                        componentsB.Add(new Component_B() { id = Guid.NewGuid() });
                    }

                    db.InsertBulk(componentsB);
                    componentsB.Clear();
                }
            }

            Timer = new Stopwatch();
            Timer.Start();

            for (var i = 0; i < 10000; i++)
                Bus.SendLocal(new BuildWidgetCommand(){ NeedsComponentB = DateTime.Now.Ticks % 2 == 0, BatchId = Guid.NewGuid()});

            Bus.SendLocal(new BuildWidgetCommand() { Tracer = true, BatchId = Guid.NewGuid()});
        }

        public static Stopwatch Timer { get; set; }

        public void Stop()
        {
            
        }
    }
}