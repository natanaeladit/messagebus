using MassTransit;
using System;
using System.Threading.Tasks;
using Message;

namespace Publisher
{
    internal class Program
    {
        public static async Task Main()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg => cfg.Host("localhost"));

            await busControl.StartAsync();
            try
            {
                do
                {
                    string value = await Task.Run(() =>
                    {
                        Console.WriteLine("Enter message (or quit to exit)");
                        Console.Write("> ");
                        return Console.ReadLine();
                    });

                    if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                        break;

                    await busControl.Publish<ValueEntered>(new
                    {
                        Value = value
                    });
                }
                while (true);
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}