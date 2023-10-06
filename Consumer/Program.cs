using MassTransit;

namespace Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<PrintConsumer>();
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.ReceiveEndpoint("print-listener", e =>
                            {
                                e.ConfigureConsumer<PrintConsumer>(context, c => c.UseMessageRetry(r =>
                                {
                                    r.Interval(10, TimeSpan.FromMilliseconds(200));
                                    r.Ignore<ArgumentNullException>();
                                }));
                            });
                        });
                    });
                })
                .Build();

            host.Run();
        }
    }
}