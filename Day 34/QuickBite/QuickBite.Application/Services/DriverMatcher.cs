using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using QuickBite.Application.Interfaces;
using QuickBite.Application.Results;

namespace QuickBite.Application.Services
{
    public class DriverMatcher
    {
        private readonly List<IDriverService> _services;

        public DriverMatcher(List<IDriverService> services)
        {
            _services = services;
        }

        // -------------------------------------

        public async Task<DriverAssignment>
            FindBestDriverAsync(
                string orderId,
                Location pickup,
                Location dropoff,
                CancellationToken ct)
        {
            using var timeout =
                new CancellationTokenSource(
                    TimeSpan.FromSeconds(10));

            using var linked =
                CancellationTokenSource
                    .CreateLinkedTokenSource(
                        ct,
                        timeout.Token);

            var tasks = _services
                .Select(s =>
                    s.FindAvailableDriversAsync(
                        pickup,
                        dropoff,
                        linked.Token))
                .ToList();

            await Task.WhenAll(tasks);

            var allDrivers = tasks
                .SelectMany(t => t.Result)
                .ToList();

            if (!allDrivers.Any())
                throw new Exception(
                    "No drivers found");

            var ranked = allDrivers
                .OrderBy(d => d.EstimatedArrivalMinutes)
                .ThenByDescending(d => d.Rating)
                .ThenBy(d => d.Cost)
                .ToList();

            foreach (var driver in ranked)
            {
                foreach (var service in _services)
                {
                    try
                    {
                        var assignment =
                            await service
                                .AssignDriverAsync(
                                    orderId,
                                    driver.DriverId,
                                    ct);

                        if (assignment.IsAccepted)
                            return assignment;
                    }
                    catch
                    {
                        // Try next
                    }
                }
            }

            throw new Exception(
                "All drivers declined");
        }
    }
}
