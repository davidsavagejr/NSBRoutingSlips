using System;
using Contracts;
using Models;
using NPoco;
using NServiceBus;
using NServiceBus.Saga;

namespace WidgetSaga
{
    public class BuildWidgetSagaData : IContainSagaData
    {
        public virtual Guid Id { get; set; }
        public virtual string Originator { get; set; }
        public virtual string OriginalMessageId { get; set; }

        [Unique]
        public virtual Guid BatchId { get; set; }

        public virtual Guid? ComponentAId { get; set; }
        public virtual Guid? ComponentBId { get; set; }
        public virtual bool NeedsComponentB { get; set; }
        public virtual bool Tracer { get; set; }
    }

    public class BuildWidgetSaga : Saga<BuildWidgetSagaData>,
        IAmStartedByMessages<BuildWidgetCommand>,
        IHandleMessages<BuildWidgetSaga_GetComponentA>,
        IHandleMessages<BuildWidgetSaga_GetComponentB>,
        IHandleMessages<BuildWidgetSaga_CreateWidget>
    {
        

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<BuildWidgetSagaData> mapper)
        {
            mapper.ConfigureMapping<BuildWidgetSaga_GetComponentA>(a => a.BatchId).ToSaga(s => s.BatchId);
            mapper.ConfigureMapping<BuildWidgetSaga_GetComponentB>(a => a.BatchId).ToSaga(s => s.BatchId);
            mapper.ConfigureMapping<BuildWidgetSaga_CreateWidget>(a => a.BatchId).ToSaga(s => s.BatchId);
        }

        public void Handle(BuildWidgetCommand message)
        {
            //Data.BatchId = Guid.NewGuid();
            //Data.NeedsComponentB = message.NeedsComponentB;
            //Data.Tracer = message.Tracer;

            //Bus.Send("ComponentAService", new BuildWidgetSaga_GetComponentA(Data.BatchId));
        }

        public void Handle(BuildWidgetSaga_GetComponentA message)
        {
            
        }

        public void Handle(BuildWidgetSaga_GetComponentB message)
        {
            using (var db = new Database("connstr"))
            {
                db.BeginTransaction();
                db.Execute("UPDATE TOP(1) Component_B SET batchid = @0 WHERE batchid IS NULL", Data.BatchId);
                var componentId = db.Single<Component_B>("WHERE batchid = @0", Data.BatchId);
                Data.ComponentBId = componentId.id;
                db.CompleteTransaction();

                Bus.Send("WidgetService", new BuildWidgetSaga_CreateWidget(Data.BatchId));
            }
        }

        public void Handle(BuildWidgetSaga_CreateWidget message)
        {
            
        }
    }
}