using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using QuickBite.Application.Orchestrators;
using QuickBite.Domain.Entities;

namespace QuickBite.Application.Orchestrators
{
    public class BatchOrderProcessor
    {
        private readonly OrderProcessingEngine _engine;

        private readonly SemaphoreSlim _globalLimit =
            new SemaphoreSlim(100);

        private readonly ConcurrentDictionary<string, SemaphoreSlim>
            _restaurantLimits = new();

        private readonly ConcurrentDictionary<string, int>
            _failures = new();

        private volatile bool _paused;

        public BatchOrderProcessor(
            OrderProcessingEngine engine)
        {
            _engine = engine;
        }

        // -------------------------------------

        public void Pause() => _paused = true;

        public void Resume() => _paused = false;

        // -------------------------------------

        public async Task ProcessBatchAsync(
            List<CustomerOrder> orders,
            CancellationToken ct)
        {
            var tasks = new List<Task>();

            foreach (var order in orders)
            {
                await _globalLimit.WaitAsync(ct);

                tasks.Add(
                    ProcessSingleAsync(order, ct));
            }

            await Task.WhenAll(tasks);
        }

        // -------------------------------------

        private async Task ProcessSingleAsync(
            CustomerOrder order,
            CancellationToken ct)
        {
            try
            {
                while (_paused)
                    await Task.Delay(500, ct);

                var restaurantLimit =
                    _restaurantLimits.GetOrAdd(
                        order.RestaurantId,
                        _ => new SemaphoreSlim(5));

                await restaurantLimit.WaitAsync(ct);

                // Circuit breaker
                if (_failures.TryGetValue(
                    order.RestaurantId,
                    out var count) &&
                    count >= 5)
                {
                    throw new Exception(
                        "Restaurant circuit open");
                }

                await _engine.ProcessOrderAsync(
                    order, ct);

                _failures[order.RestaurantId] = 0;
            }
            catch
            {
                _failures.AddOrUpdate(
                    order.RestaurantId,
                    1,
                    (_, c) => c + 1);

                throw;
            }
            finally
            {
                _globalLimit.Release();

                if (_restaurantLimits.TryGetValue(
                    order.RestaurantId,
                    out var sem))
                {
                    sem.Release();
                }
            }
        }
    }
}
