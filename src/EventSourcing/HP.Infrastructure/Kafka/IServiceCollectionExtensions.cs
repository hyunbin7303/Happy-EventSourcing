﻿using Confluent.Kafka;
using EventStore.Client;
using HP.Core.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace HP.Infrastructure.Kafka
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddKafkaEventProducer(this IServiceCollection services, string topicName)
        {
            return services.AddSingleton<IEventProducer>(c =>
            {
                var logger = c.GetRequiredService<ILogger<EventProducer>>();
                var getConfig = c.GetRequiredService<IOptions<ProducerConfig>>();
                return new EventProducer(getConfig, topicName);
            });
        }

        public static IServiceCollection AddEventStoreInfra(this IServiceCollection services)
        {
        //localhost:2113?tls=true&tlsVerifyCert=false
            var settings = EventStoreClientSettings.Create("esdb://localhost:2113?tls=false");
            //settings.DefaultCredentials = new UserCredentials("admin", "changeit");
            var esClient = new EventStoreClient(settings); 
            services.AddSingleton(esClient);
            services.AddScoped<IEventStore, EventStore>();
            return services;
        }
    }
}
 