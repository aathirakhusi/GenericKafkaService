using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Int.Kafka.Service.OptionsMiddleware;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Int.Kafka.Service.Services
{
   
        public class KafkaAvroProducerService<T> : IKafkaProducerService<T> where T : Avro.Specific.ISpecificRecord
        {
            private readonly KafkaProducerOptions _options;
            private readonly ILogger _logger;

            public KafkaAvroProducerService(IOptions<KafkaProducerOptions> kafkaProducerOptions, ILogger<KafkaAvroProducerService<T>> logger)
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

                var schemaRegistryConfig = new SchemaRegistryConfig
                {
                    Url = _options.SchemaRegistryUrl
                };

                var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
                var serializer = new AvroSerializer<T>(schemaRegistry);

                using (var producer = new ProducerBuilder<Null, T>(config).SetValueSerializer(serializer.AsSyncOverAsync()).Build())
                {
                    var topic = "mytopic";
                    var message = new Message<Null, T> { Value = data };
                    var deliveryReport = await producer.ProduceAsync(topic, message);

                    _logger.LogInformation($"Produced message to: {deliveryReport.TopicPartitionOffset}");
                }
            }
        }
    }

