namespace Service
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<NHibernatePersistence>();
            configuration.Transactions().DisableDistributedTransactions();
            configuration.Transactions().DoNotWrapHandlersExecutionInATransactionScope();
        }
    }
}
