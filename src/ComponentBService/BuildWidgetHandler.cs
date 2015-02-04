using Contracts;
using Models;
using NPoco;
using NServiceBus;
using NServiceBus.MessageRouting.RoutingSlips;

namespace ComponentBService
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
                db.Execute("UPDATE TOP(1) Component_B SET batchid = @0 WHERE batchid IS NULL", message.BatchId);
                var componentB = db.Single<Component_B>("WHERE batchid = @0", message.BatchId);

                _routingSlip.Attachments["componentBId"] = componentB.id.ToString();
                
                db.CompleteTransaction();
            }
        }
    }


}
