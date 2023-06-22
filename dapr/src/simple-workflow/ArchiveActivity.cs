using Dapr.Workflow;
using simple_model;

namespace simple_workflow
{
    public class ArchiveActivity : WorkflowActivity<ParkingFacility, string>
    {

        private readonly ILogger<ArchiveActivity> _logger;

		public ArchiveActivity(ILogger<ArchiveActivity> logger)
        {
            this._logger = logger;
        }        

        public override Task<string> RunAsync(WorkflowActivityContext context, ParkingFacility input)
        {
            _logger.LogInformation($"...... Archive Activity :: publish archive message for facility {input.name} - WF instance {context.InstanceId}");

            var message = $"Archive - Data archived {input.name} - WF instance {context.InstanceId}";
            return Task.FromResult(message);
        }
    }
}
