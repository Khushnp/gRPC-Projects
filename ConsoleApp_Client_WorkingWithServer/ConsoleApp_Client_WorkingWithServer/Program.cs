// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using GrpcService1_SimpleTalkGrpcService;

Console.WriteLine("Hello, World!");
using var channel = GrpcChannel.ForAddress("https://localhost:7176");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();