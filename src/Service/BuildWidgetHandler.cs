using System.Collections.Generic;
using Contracts;
using NServiceBus;
using NServiceBus.MessageRouting.RoutingSlips;

namespace Service
{
    public class BuildWidgetHandler : IHandleMessages<BuildWidgetCommand>
    {
        private readonly IBus _bus;

        public BuildWidgetHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(BuildWidgetCommand message)
        {
            var destinations = new List<string>();
            destinations.Add("ComponentAService");
            
            if(message.NeedsComponentB)
                destinations.Add("ComponentBService");

            destinations.Add("WidgetService");

            _bus.Route(message, destinations.ToArray());
        }
    }
}
