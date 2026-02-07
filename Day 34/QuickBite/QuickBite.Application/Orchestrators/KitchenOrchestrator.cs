using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using QuickBite.Domain.Entities;

namespace QuickBite.Application.Orchestrators
{
    public class KitchenOrchestrator
    {
        public async Task CookOrderAsync(
            List<OrderItem> items,
            List<KitchenStation> stations,
            CancellationToken ct)
        {
            if (items == null || items.Count == 0)
                throw new ArgumentException("No items to cook");

            if (stations == null || stations.Count == 0)
                throw new ArgumentException("No stations available");

            var tasks = new List<Task>();

            foreach (var item in items)
            {
                var station = SelectBestStation(item, stations);

                if (station == null)
                    throw new Exception($"No station for {item.Name}");

                tasks.Add(CookItemAsync(item, station, ct));
            }

            await Task.WhenAll(tasks);
        }

        // --------------------------------------

        private KitchenStation SelectBestStation(
            OrderItem item,
            List<KitchenStation> stations)
        {
            return stations
                .Where(s =>
                    s.IsOperational &&
                    s.StationType == item.Category &&
                    s.CurrentLoad < s.MaxCapacity)
                .OrderBy(s => s.CurrentLoad)
                .FirstOrDefault();
        }

        // --------------------------------------

        private async Task CookItemAsync(
            OrderItem item,
            KitchenStation station,
            CancellationToken ct)
        {
            try
            {
                station.CurrentLoad += 10;

                int cookTime = GetCookTime(item);

                await Task.Delay(
                    TimeSpan.FromSeconds(cookTime),
                    ct);

                station.CurrentLoad -= 10;
            }
            catch
            {
                station.IsOperational = false;
                throw;
            }
        }

        // --------------------------------------

        private int GetCookTime(OrderItem item)
        {
            return item.Category switch
            {
                "Grill" => 120,
                "Fryer" => 180,
                "Salad" => 60,
                "Dessert" => 30,
                _ => 90
            };
        }
    }
}
