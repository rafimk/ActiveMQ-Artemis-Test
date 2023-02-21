using ActiveMQ.Artemis.Client;

var connectionFactory = new ConnectionFactory();
var endpoint = Endpoint.Create(host: "localhost", port: 5672, "guest", "guest");
var connection = await connectionFactory.CreateAsync(endpoint);
var address = "my-anycast-address";
await using var producer = await connection.CreateProducerAsync(address, RoutingType.Anycast);

Console.WriteLine("Producer created.");
Console.WriteLine($"Address: {address}");
Console.WriteLine($"RoutingType: {RoutingType.Anycast.ToString()}");

int counter = 1;
while (counter <= 10)
{
    var msg = new Message($"Message {counter}");
    await producer.SendAsync(msg);
    Console.WriteLine($"Message '{counter}' sent.");
    counter++;
    await Task.Delay(TimeSpan.FromSeconds(1));
}