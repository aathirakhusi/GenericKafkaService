using Int.Kafka.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KafkaController : ControllerBase
    {
        private readonly ILogger<KafkaController> _logger;
        private readonly IKafkaProducerService<Avro.Specific.ISpecificRecord> _avroProducerService;

        public KafkaController(ILogger<KafkaController> logger, IKafkaProducerService<Avro.Specific.ISpecificRecord> avroProducerService)
        {
            _logger = logger;
            _avroProducerService = avroProducerService;
        }

        [HttpPost("produce-avro")]
        public IActionResult ProduceAvroMessage()
        {
            // TODO: Implement Kafka Avro producer logic
            _logger.LogInformation("Produced Avro message.");

            return Ok();
        }
    }
}
