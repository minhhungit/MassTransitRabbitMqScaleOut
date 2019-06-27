using MassTransit;
using MassTransitScaleOut.Messages;
using System;

namespace MassTransitScaleOut.Publisher
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
            });

            bus.StartAsync().GetAwaiter().GetResult();

            var number = 1;
            while (true)
            {
                var msg = new MyMessageItem(id: number, name: "Hello, World.");
                bus.Publish(msg).GetAwaiter().GetResult();
                Console.WriteLine($"{msg.Id} - {msg.Name}");

                System.Threading.Thread.Sleep(400);
                number++;
            }            

            Console.ReadLine();
            bus.StopAsync().GetAwaiter().GetResult();
        }
    }
}
