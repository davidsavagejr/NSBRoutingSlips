using System;
using Contracts;
using Models;
using NPoco;
using NServiceBus;
using NServiceBus.MessageRouting.RoutingSlips;

namespace WidgetService
{
    public class BuildWidgetHandler : IHandleMessages<BuildWidgetCommand>
    {
        private readonly IBus _bus;
        private readonly RoutingSlip _routingSlip;

        public BuildWidgetHandler(IBus bus, RoutingSlip routingSlip)
        {
            _bus = bus;
            _routingSlip = routingSlip;
        }

        public void Handle(BuildWidgetCommand message)
        {
            var batchid = message.BatchId;

            var componentAId = Guid.Parse(_routingSlip.Attachments["componentAId"]);

            Guid? componentBId = null;
            if(message.NeedsComponentB)
                componentBId = Guid.Parse(_routingSlip.Attachments["componentBId"]);

            using (var db = new Database("connstr"))
            {
                db.BeginTransaction();

                if (!db.Exists<Widget>(batchid))
                {
                    var widget = new Widget()
                    {
                        id = batchid,
                        Name = DateTime.Now.Ticks.ToString(),
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        component_a_id = componentAId,
                        component_b_id = componentBId
                    };

                    db.Insert(widget);
                }

                if (message.Tracer)
                    _bus.Send("Service", new ProcessComplete());

                db.CompleteTransaction();
            }
           
        }
    }
}
