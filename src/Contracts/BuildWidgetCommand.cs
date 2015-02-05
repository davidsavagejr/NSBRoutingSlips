using System;
using NServiceBus;

namespace Contracts
{
    public class BuildWidgetCommand : ICommand
    {
        public Guid BatchId { get; set; }
        public bool Tracer { get; set; }
        public bool NeedsComponentB { get; set; }
    }
}