using MassTransit;
using MassTransitScaleOut.Messages;
using System;

namespace MassTransitScaleOut.ConsumerOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost:/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.ReceiveEndpoint(host, "my_queue", endpoint =>
                {
                    endpoint.Handler<MyMessageItem>(async context =>
                    {
                        await Console.Out.WriteLineAsync($"Received: {context.Message.Id} / {context.Message.Name}");
                    });
                });
            });

            bus.StartAsync().GetAwaiter().GetResult();
            Console.ReadLine();
            bus.StopAsync().GetAwaiter().GetResult();
        }
    }
}
