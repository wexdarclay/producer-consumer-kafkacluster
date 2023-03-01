using Confluent.Kafka;
using KafkaProducer;
using System.Reflection.Metadata;
using Faker;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using SolTechnology.Avro;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

//byte[] avroBytes = File.ReadAllBytes("schema.avro");
//var schemaInJsonFormat = AvroConvert.GetSchema(avroBytes);

//var schemaRegistryConfig = new SchemaRegistryConfig
//{
//	Url = "http://localhost:8081"
//};

//var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);

//var schemaId = schemaRegistry.RegisterSchemaAsync("testtopic", schemaInJsonFormat).Result;

var producerConfig = new ProducerConfig
{
	BootstrapServers = "localhost:9092",
	Acks = Acks.None
};

var producer = new ProducerBuilder<int, string?>(producerConfig)
	//.SetKeySerializer(new AvroSerializer<int>(schemaRegistry))
	//.SetValueSerializer(new AvroSerializer<User>(schemaRegistry))
	.Build();

try
{
	int num = 0;
	while (true)
	{
		var user = new User() { Id = num++, FirstName = Faker.Name.First(), LastName = Faker.Name.Last(), Email = "some-random-email@email.com" };
		var message = new Message<int, User>
		{
			Key = num,
			Value = user
		};
		var json = System.Text.Json.JsonSerializer.Serialize(message.Value);
		var response = producer.ProduceAsync("test-topic2", new Message<int, string?> { Key = message.Key, Value = json });
		Console.WriteLine($"message sent - id: {message.Key}");
		Thread.Sleep(4000);
	}
}
catch (ProduceException<Null, string> ex)
{
	Console.WriteLine($"error occurred producing {ex.Message}");

}