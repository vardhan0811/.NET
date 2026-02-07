using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using QuickBite.Application.Results;

namespace QuickBite.Application.Services
{
    public class OrderStatusBroadcaster
    {
        private readonly ConcurrentDictionary<
            string,
            Channel<OrderUpdate>> _channels =
            new();

        private readonly ConcurrentDictionary<
            string,
            OrderUpdate> _lastStatus =
            new();

        // Publisher
        public async Task PublishAsync(OrderUpdate update)
        {
            _lastStatus[update.OrderId] = update;

            var channel = _channels.GetOrAdd(
                update.OrderId,
                _ => Channel.CreateBounded<OrderUpdate>(50));

            await channel.Writer.WriteAsync(update);
        }

        // Subscriber
        public async IAsyncEnumerable<OrderUpdate>
            SubscribeToOrderUpdates(
                string orderId,
                [EnumeratorCancellation]
                CancellationToken ct)
        {
            if (_lastStatus.TryGetValue(orderId, out var last))
                yield return last;

            var channel = _channels.GetOrAdd(
                orderId,
                _ => Channel.CreateBounded<OrderUpdate>(50));

            await foreach (var update
                in channel.Reader.ReadAllAsync(ct))
            {
                yield return update;
            }
        }
    }
}
