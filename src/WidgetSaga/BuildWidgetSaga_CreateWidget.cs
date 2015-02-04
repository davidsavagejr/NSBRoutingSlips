using System;
using NServiceBus;

namespace WidgetSaga
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