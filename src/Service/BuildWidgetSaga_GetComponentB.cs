using System;
using NServiceBus;

namespace Service
{
    public class BuildWidgetSaga_GetComponentB : ICommand
    {
        public Guid BatchId { get; set; }

        public BuildWidgetSaga_GetComponentB(Guid batchId)
        {
            BatchId = batchId;
        }
    }
}