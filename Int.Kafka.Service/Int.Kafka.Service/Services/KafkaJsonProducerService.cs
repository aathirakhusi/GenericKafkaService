using Confluent.Kafka;
using Int.Kafka.Service.OptionsMiddleware;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Int.Kafka.Service.Services
{
    public class KafkaJsonProducerService<T> : IKafkaProducerService<T>
    {
        private readonly KafkaProducerOptions _options;
        private readonly ILogger _logger;

        public KafkaJsonProducerService(IOptions<KafkaProducerOptions> kafkaProducerOptions, ILogger<KafkaJsonProducerService<T>> logger)
        {
            _options = kafkaProducerOptions.Value;
            _logger = logger;
        }

        public async Task ProduceMessageAsync(T data)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _options.BootstrapServers
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var topic = "mytopic";
                var message = new Message<Null, string> { Value = Newtonsoft.Json.JsonConvert.SerializeObject(data) };
                var deliveryReport = await producer.ProduceAsync(topic, message);

                _logger.LogInformation($"Produced message to: {deliveryReport.TopicPartitionOffset}");
            }
        }
    }
}
