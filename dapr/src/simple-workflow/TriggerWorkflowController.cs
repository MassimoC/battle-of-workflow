using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using simple_model;
using System.Text.Json;
using System.Text;

namespace simple_workflow
{
    [ApiController]
    [Route("workflows")]
	public class TriggerWorkflowController : ControllerBase
	{
        private readonly ILogger<TriggerWorkflowController> _logger;
        private readonly DaprClient _daprClient;
		private readonly IHttpClientFactory _httpClientFactory;

		public TriggerWorkflowController(ILogger<TriggerWorkflowController> logger, DaprClient daprClient, IHttpClientFactory httpClientFactory)
        {
            this._daprClient = daprClient;
            this._logger = logger;
			this._httpClientFactory = httpClientFactory;
        }

		private const string DaprComponentName = "daprbus";
		private const string TopicName = "inbound";

		[Topic(DaprComponentName, TopicName)]
		[HttpPost("trigger")]
		public async Task<IActionResult> TriggerWorkflow(simple_model.ParkingFacility parking)
		{
            var fwId = Guid.NewGuid().ToString();
			_logger.LogInformation($"...... Trigger :: {parking.id} received - triggering worfklow {fwId}. ");
			var httpClient = _httpClientFactory.CreateClient();
			httpClient.BaseAddress = new Uri("http://localhost:3622/");

			var httpRequest = new StringContent(
				JsonSerializer.Serialize(parking),
				Encoding.UTF8,
				System.Net.Mime.MediaTypeNames.Application.Json); 
			
			var wfInstanceId = Guid.NewGuid().ToString();
			_logger.LogInformation($"...... Trigger :: http post to workflow. {httpClient.BaseAddress.ToString()}");
			using var httpResponse =
				await httpClient.PostAsync($"/v1.0-alpha1/workflows/dapr/Orchestration/start?instanceId={wfInstanceId}", httpRequest);

			httpResponse.EnsureSuccessStatusCode();	
			var txtResponseContent = httpResponse.Content.ReadAsStringAsync().Result;

			_logger.LogInformation($"...... Trigger :: worfklow triggered. Content {txtResponseContent} ");

			return Ok();
			
		}
	}
}
