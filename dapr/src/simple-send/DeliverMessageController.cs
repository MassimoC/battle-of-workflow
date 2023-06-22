using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace simple_send
{
    [ApiController]
    [Route("publishers")]
	public class DeliverMessageController : ControllerBase
	{
        private readonly ILogger<DeliverMessageController> _logger;
        private readonly DaprClient _daprClient;
		private readonly IHttpClientFactory _httpClientFactory;

		public DeliverMessageController(ILogger<DeliverMessageController> logger, DaprClient daprClient, IHttpClientFactory httpClientFactory)
        {
            this._daprClient = daprClient;
            this._logger = logger;
			this._httpClientFactory = httpClientFactory;
        }

		private const string DaprComponentName = "daprbus";
		private const string TopicName = "outbound";

		[Topic(DaprComponentName, TopicName)]
		[HttpPost("deliver")]
		public async Task<IActionResult> Deliver(simple_model.ParkingFacility parking)
		{
			_logger.LogInformation($"...... Deliver :: message processed - facility {parking.name}.");
			return Ok();		
		}
	}
}
