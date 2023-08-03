using Int.Kafka.Service.Services;
using Int.Kafka.Service.OptionsMiddleware;
using Int.Kafka.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Int.Kafka.Service.Extensions
{
    public static class KafkaProducerServiceExtensions
    {
        public static IServiceCollection AddKafkaProducerService<T>(this IServiceCollection services) where T : class
        {
            services.AddSingleton<IKafkaProducerService<T>, KafkaJsonProducerService<T>>();
            services.AddSingleton<IKafkaProducerService<Avro.Specific.ISpecificRecord>, KafkaAvroProducerService<Avro.Specific.ISpecificRecord>>();
            return services;
        }
           
    }
}