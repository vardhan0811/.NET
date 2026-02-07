using System;

namespace M1.Practice.Domain.Q09_ParallelAggregation
{
    public class RegionSalesReport
    {
        public string Region { get; set; }

        public decimal TotalSales { get; set; }

        public string TopCategory { get; set; }
    }

    public class BestDayReport
    {
        public DateTime Date { get; set; }

        public decimal TotalSales { get; set; }
    }
}
