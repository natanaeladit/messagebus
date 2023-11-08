using MassTransit;
using Message;

namespace Consumer
{
    internal class PrintConsumer : IConsumer<ValueEntered>
    {
        async Task IConsumer<ValueEntered>.Consume(ConsumeContext<ValueEntered> context)
        {
            Console.WriteLine(context.Message.Value);
            await Task.Delay(5 * 1000);
            Console.WriteLine("Finished");
            //throw new Exception(context.Message.Value);
        }
    }
}
