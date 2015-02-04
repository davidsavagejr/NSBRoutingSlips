using System;
using NServiceBus;

namespace Service
{
    public class BuildWidgetSaga_GetComponentA : ICommand
    {
        public Guid BatchId { get; set; }

        public BuildWidgetSaga_GetComponentA(Guid batchId)
        {
            BatchId = batchId;
        }
    }
}