using Contracts;
using NServiceBus;
using NServiceBus.Logging;

namespace Service
{
    class ProcessCompletedHandler : IHandleMessages<ProcessComplete>
    {
        static readonly ILog Logger = LogManager.GetLogger<ProcessCompletedHandler>();

        public void Handle(ProcessComplete message)
        {
            Logger.InfoFormat("Elapsed seconds: {0}", Startup.Timer.Elapsed.TotalSeconds);
        }
    }
}
