using System;
using Contracts;
using Models;
using NPoco;
using NServiceBus;
using NServiceBus.Logging;

namespace Service
{
    public class BuildWidgetHandler : IHandleMessages<BuildWidgetCommand>
    {
        static readonly ILog Logger = LogManager.GetLogger<BuildWidgetHandler>();

        public void Handle(BuildWidgetCommand message)
        {
            using (var db = new Database("connstr"))
            {
                var batchId = Guid.NewGuid();

                var componentA = GetComponentA(db, batchId);

                Component_B componentB = null;
                if (message.NeedsComponentB)
                    componentB = GetComponentB(db, batchId);

                var widget = new Widget()
                {
                    id = Guid.NewGuid(),
                    Name = DateTime.Now.Ticks.ToString(),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    component_a_id = componentA.id,
                    component_b_id = componentB == null ? null as Guid? : componentB.id
                };

                db.Insert(widget);
            }

            if (!message.Tracer) return;

            Startup.Timer.Stop();
            Logger.InfoFormat("Elapsed seconds: {0}", Startup.Timer.Elapsed.TotalSeconds);
        }

        private Component_B GetComponentB(Database db, Guid batchId)
        {
            db.Execute("UPDATE TOP(1) Component_B SET batchid = @0 WHERE batchid IS NULL", batchId);
            return db.Single<Component_B>("WHERE batchid = @0", batchId);
        }

        private static Component_A GetComponentA(Database db, Guid batchId)
        {
            db.Execute("UPDATE TOP(1) Component_A SET batchid = @0 WHERE batchid IS NULL", batchId);
            return db.Single<Component_A>("WHERE batchid = @0", batchId);
        }
    }
}