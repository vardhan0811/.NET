using System;
using System.Collections.Generic;
using System.Linq;
using M1.Practice.Domain.Q09_ParallelAggregation;

namespace M1.Practice.Application.Q09_ParallelAggregation
{
    public class SalesAggregator
    {
        public List<RegionSalesReport>
            AggregateByRegion(List<Sale> sales)
        {
            var result =
                sales
                .AsParallel()
                .GroupBy(s => s.Region)
                .Select(g =>
                {
                    var topCategory =
                        g.GroupBy(x => x.Category)
                         .OrderByDescending(c =>
                             c.Sum(x => x.Amount))
                         .First()
                         .Key;

                    return new RegionSalesReport
                    {
                        Region = g.Key,
                        TotalSales = g.Sum(x => x.Amount),
                        TopCategory = topCategory
                    };
                })
                // Make output deterministic
                .OrderBy(r => r.Region)
                .ToList();

            return result;
        }

        // ---------------------------------------

        public BestDayReport
            GetBestSalesDay(List<Sale> sales)
        {
            var best =
                sales
                .AsParallel()
                .GroupBy(s => s.Date.Date)
                .Select(g =>
                    new BestDayReport
                    {
                        Date = g.Key,
                        TotalSales = g.Sum(x => x.Amount)
                    })
                .OrderByDescending(x => x.TotalSales)
                .ThenBy(x => x.Date)
                .First();

            return best;
        }
    }
}
