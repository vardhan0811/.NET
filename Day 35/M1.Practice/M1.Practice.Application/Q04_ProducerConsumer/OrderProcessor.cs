using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using M1.Practice.Domain.Q04_ProducerConsumer;

namespace M1.Practice.Application.Q04_ProducerConsumer
{
    public class OrderProcessor
    {
        public async Task<int> ProcessAsync(
            List<Order> orders)
        {
            var queue =
                new BlockingCollection<Order>();

            int processedCount = 0;

            // Producer
            var producer = Task.Run(() =>
            {
                foreach (var order in orders)
                {
                    queue.Add(order);
                }

                queue.CompleteAdding();
            });

            // Consumers (3 workers)
            var consumers = new List<Task>();

            for (int i = 0; i < 3; i++)
            {
                consumers.Add(Task.Run(async () =>
                {
                    foreach (var order in queue.GetConsumingEnumerable())
                    {
                        // Simulate work
                        await Task.Delay(200);

                        System.Threading.Interlocked.Increment(
                            ref processedCount);
                    }
                }));
            }

            await producer;
            await Task.WhenAll(consumers);

            return processedCount;
        }
    }
}
