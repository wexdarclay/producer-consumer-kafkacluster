using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using KafkaConsumer;

var consumerConfig = new ConsumerConfig
{
	GroupId = "my-consumer-group",
	BootstrapServers = "localhost:9092",
	AutoOffsetReset = AutoOffsetReset.Earliest
};

//var schemaRegistryConfig = new SchemaRegistryConfig
//{
//	Url = "http://localhost:8081"
//};

//var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);

//var serializerConfig = new AvroSerializerConfig
//{
//	AutoRegisterSchemas = true
//};


var consumer = new ConsumerBuilder<int, string>(consumerConfig).Build();

consumer.Subscribe("test-topic2");

CancellationTokenSource token = new CancellationTokenSource();

try
{
	while (true)
	{
		var response = consumer.Consume(token.Token);
		var user = response.Message.Value;
		Console.WriteLine(user.ToString());
	}
}
catch (Exception ex)
{
	Console.WriteLine($"An error occurred during consumation: {ex.Message}");
}
