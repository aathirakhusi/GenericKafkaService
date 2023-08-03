using System;
using System.Collections.Generic;
using System.Text;

namespace Int.Kafka.Service.OptionsMiddleware
{
    public class KafkaProducerOptions
    {
        public string BootstrapServers { get; set; }
        public string SchemaRegistryUrl { get; set; }
        public MessageType MessageType { get; set; }
    }

    public enum MessageType
    {
        Json,
        Avro,
        None
    }
}
