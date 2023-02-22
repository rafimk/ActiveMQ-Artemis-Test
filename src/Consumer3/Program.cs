using ActiveMQ.Artemis.Client;

var address = args[0];
var queue = args[1];
if (!Enum.TryParse(args[2], out RoutingType routingType))
{
    throw new ArgumentException($"{args[2]} is the invalid RoutingType");
}

var connectionFactory = new ConnectionFactory();
var endpoint = Endpoint.Create(host: "localhost", port: 5672, "guest", "guest");
var connection = await connectionFactory.CreateAsync(endpoint);

var topologyManager = await connection.CreateTopologyManagerAsync();
await topologyManager.DeclareQueueAsync(new QueueConfiguration
{
    Address = address,
    Name = queue,
    RoutingType = RoutingType.Multicast,
    AutoCreateAddress = true,
    Durable = true,
});

await using var consumer = await connection.CreateConsumerAsync(address: address, queue: queue);

Console.WriteLine("Consumer created.");
Console.WriteLine($"Queue: {queue}");
Console.WriteLine($"RoutingType: {routingType}");

while (true)
{
    var msg = await consumer.ReceiveAsync();
    await consumer.AcceptAsync(msg);
    Console.WriteLine($"Message '{msg.GetBody<string>()}' received.");
}