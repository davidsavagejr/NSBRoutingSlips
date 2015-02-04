using NServiceBus;
using NServiceBus.Logging;
using WidgetSaga;

namespace Service
{
    class ProcessCompletedHandler : IHandleMessages<ProcessComplete>
    {
        static readonly ILog Logger = LogManager.GetLogger<BuildWidgetSaga>();

        public void Handle(ProcessComplete message)
        {
            Logger.InfoFormat("Elapsed seconds: {0}", Startup.Timer.Elapsed.TotalSeconds);
        }
    }
}
