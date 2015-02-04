using System;
using NServiceBus;

namespace Service
{
    public class BuildWidgetSaga_CreateWidget : ICommand
    {
        public Guid BatchId { get; set; }

        public BuildWidgetSaga_CreateWidget(Guid batchId)
        {
            BatchId = batchId;
        }
    }
}