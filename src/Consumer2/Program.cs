using ActiveMQ.Artemis.Client;

var connectionFactory = new ConnectionFactory();
var endpoint = Endpoint.Create(host: "localhost", port: 5672, "guest", "guest");
var connection = await connectionFactory.CreateAsync(endpoint);
var address = "my-anycast-address";
await using var consumer = await connection.CreateConsumerAsync(address, RoutingType.Anycast);

Console.WriteLine("Consumer1 created.");
Console.WriteLine($"Address: {address}");
Console.WriteLine($"RoutingType: {RoutingType.Anycast.ToString()}");

while (true)
{
    var msg = await consumer.ReceiveAsync();
    await consumer.AcceptAsync(msg);
    Console.WriteLine($"Message '{msg.GetBody<string>()}' received.");
}