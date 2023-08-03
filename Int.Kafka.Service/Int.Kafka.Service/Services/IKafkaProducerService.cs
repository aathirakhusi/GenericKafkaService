using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Int.Kafka.Service.Services
{
    public interface IKafkaProducerService<T>
    {
        Task ProduceMessageAsync(T data);
    }
}
