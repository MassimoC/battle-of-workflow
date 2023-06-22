using Dapr.Workflow;
using Dapr.Client;
using simple_model;



namespace simple_workflow
{
    public class DeclareToAuthorityActivity : WorkflowActivity<ParkingFacility, string>
    {

        private readonly DaprClient _daprClient; 
        private readonly ILogger<DeclareToAuthorityActivity> _logger;

		public DeclareToAuthorityActivity(ILogger<DeclareToAuthorityActivity> logger, DaprClient daprClient)
        {
            this._daprClient = daprClient;
            this._logger = logger;
        }        

        public override Task<string> RunAsync(WorkflowActivityContext context, ParkingFacility input)
        {
            var DaprComponentName = "daprbus";
            var TopicName = "outbound";

            _logger.LogInformation($"...... Authority Activity :: publish declaration message for facility {input.name} - WF instance {context.InstanceId}");
            _daprClient.PublishEventAsync<ParkingFacility>(DaprComponentName, TopicName, input);            

            var message = $"Authority - File received for facility {input.name} - WF instance {context.InstanceId}";
            return Task.FromResult(message);
        }
    }
}
