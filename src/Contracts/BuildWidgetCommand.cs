using NServiceBus;

namespace Contracts
{
    public class BuildWidgetCommand : ICommand
    {
        public bool Tracer { get; set; }
        public bool NeedsComponentB { get; set; }
    }
}