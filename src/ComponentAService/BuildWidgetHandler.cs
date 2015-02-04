using Contracts;
using Models;
using NPoco;
using NServiceBus;
using NServiceBus.MessageRouting.RoutingSlips;

namespace ComponentAService
{
    public class BuildWidgetHandler : IHandleMessages<BuildWidgetCommand>
    {
        private readonly RoutingSlip _routingSlip;

        public BuildWidgetHandler(RoutingSlip routingSlip)
        {
            _routingSlip = routingSlip;
        }

        public void Handle(BuildWidgetCommand message)
        {
            using (var db = new Database("connstr"))
            {
                db.BeginTransaction();
                db.Execute("UPDATE TOP(1) Component_A SET batchid = @0 WHERE batchid IS NULL", message.BatchId);
                var componentA = db.Single<Component_A>("WHERE batchid = @0", message.BatchId);

                _routingSlip.Attachments["componentAId"] = componentA.id.ToString();
                
                db.CompleteTransaction();
            }
        }
    }


}
