namespace Service
{
    using NServiceBus;

    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.Transactions().DisableDistributedTransactions();
            configuration.Transactions().DoNotWrapHandlersExecutionInATransactionScope();
        }
    }
}
