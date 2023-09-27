using MassTransit;
using Message;

namespace Consumer
{
    internal class PrintConsumer : IConsumer<ValueEntered>
    {
        Task IConsumer<ValueEntered>.Consume(ConsumeContext<ValueEntered> context)
        {
            Console.WriteLine(context.Message.Value);
            return Task.CompletedTask;
        }
    }
}
