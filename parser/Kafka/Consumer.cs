using System.Diagnostics;
using System.Text.Json.Serialization;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace parser.Kafka;

public class Consumer
{
    private const string Topic = "product-urls";
    private const string BootstrapServers = "172.16.250.33:9092, 172.16.250.34:9092, 172.16.250.35:9092";
    private const string SchemaRegistryUrl = "http://172.16.250.43:8081/schemas/ids/11";
    
    /*static async Task Main(string[] args)
    {   
        var cts = new CancellationTokenSource();
        var consumer = Task.Run(() => StartConsumer(cts.Token));
        cts.Cancel();
        await consumer;
    }*/
    public static async Task Main()
    {
        var cts = new CancellationTokenSource();
        var consumer = Task.Run(() => StartConsumer(cts.Token));
        cts.Cancel();
        await consumer;
    }

    public static void StartConsumer(CancellationToken ct)
    {
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = BootstrapServers,
            GroupId = "product-urls-consumer"
        };

        using (var schemaRegistry = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = SchemaRegistryUrl }))
        using (var consumer = new ConsumerBuilder<string, ProductUrl>(consumerConfig)
                   .SetKeyDeserializer(new AvroDeserializer<string>(schemaRegistry).AsSyncOverAsync())
                   .SetValueDeserializer(new AvroDeserializer<ProductUrl>(schemaRegistry).AsSyncOverAsync())
                   .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                   .Build())
        {

            consumer.Subscribe(Topic);

            try
            {
                while (!ct.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(TimeSpan.FromMilliseconds(100));
                        if (consumeResult is null) continue;

                        var productUrl = consumeResult.Message.Value;
                        Console.WriteLine($"key: {consumeResult.Message.Key}, url: {productUrl.url}, fetchedAt: {productUrl.fetchedAt}");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Consume error: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }
    }
}
