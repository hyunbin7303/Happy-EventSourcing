using Confluent.Kafka;
using HP.Application;
using HP.Core.Common;
using HP.Core.Events;
using HP.Core.Models;
using HP.Domain;
using HP.Domain.Todos.Write;
using HP.Infrastructure;
using HP.Infrastructure.DbAccess;
using HP.Infrastructure.EventHandlers;
using HP.Infrastructure.Kafka;
using HP.Infrastructure.Repository;
using HP.Infrastructure.Repository.Write;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

var builder = WebApplication.CreateBuilder(args);
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if (env == "Development")
    builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false, true).AddEnvironmentVariables();
else
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, true).AddEnvironmentVariables();

BsonClassMap.RegisterClassMap<DomainEvent>();
BsonClassMap.RegisterClassMap<PersonDomainEvents.PersonCreated>();
BsonClassMap.RegisterClassMap<PersonDomainEvents.PersonInfoUpdated>();
BsonClassMap.RegisterClassMap<TodoDomainEvents.TodoCreated>();
BsonClassMap.RegisterClassMap<TodoDomainEvents.TodoUpdated>();
BsonClassMap.RegisterClassMap<TodoDomainEvents.TodoRemoved>();
BsonClassMap.RegisterClassMap<TodoDomainEvents.TodoItemCreated>();
BsonClassMap.RegisterClassMap<TodoDomainEvents.TodoItemUpdated>();

var getConfig = builder.Configuration;
builder.Services.AddScoped<IMongoDbContext, MongoDbContext>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<HpReadDbContext>(opt => opt.UseNpgsql(getConfig.GetConnectionString("postgres")), ServiceLifetime.Singleton);
builder.Services.Configure<ProducerConfig>(getConfig.GetSection(nameof(ProducerConfig)));
builder.Services.Configure<ConsumerConfig>(getConfig.GetSection(nameof(ConsumerConfig)));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IAggregateRepository<>), typeof(AggregateRepository<>));
builder.Services.AddScoped<ITodoAggregateRepository, TodoAggregateRepository>();

builder.Services.AddScoped<ITodoEventHandler, TodoEventHandler>();
builder.Services.AddScoped<IPersonEventHandler, PersonEventHandlers>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddKafkaEventProducer(getConfig["KafkaTopicName"]);
builder.Services.AddScoped<IEventConsumer, EventConsumer>();

builder.Services.AddScoped<IInMemoryBus, InMemoryBus>();
builder.Services.AddMediatR(typeof(DemoLibMediatREntryPoint).Assembly);

builder.Services.AddControllers();
builder.Services.AddHostedService<ConsumerHostedService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
