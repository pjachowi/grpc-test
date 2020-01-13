using System;
using System.Net.Http;
using System.Threading.Tasks;
using GrpcGreeter;
using Grpc.Net.Client;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            AppContext.SetSwitch(
              "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress("http://localhost:5001");
            var client =  new Greeter.GreeterClient(channel);
            for (var i = 0; i < 10000; i++)
            {
                var reply = await client.SayHelloAsync(
                                new HelloRequest { Name = "GreeterClient" });
                // Console.WriteLine("Greeting: " + reply.Message);
            }
            // Console.WriteLine("Press any key to exit...");
            // Console.ReadKey();
        }
    }
}