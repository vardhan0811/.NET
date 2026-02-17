using System;
using System.Collections.Generic;
using System.Text;


    public interface IFinancialInstrument
    {
        string Symbol { get; }
        decimal CurrentPrice { get; }
        InstrumentType Type { get; }
    }

    public enum InstrumentType { Stock, Bond, Option, Future }
    public enum Trend { Upward, Downward, Sideways }


    public class Portfolio<T> where T : IFinancialInstrument
    {
        private readonly Dictionary<T, int> _holdings = new();

        public void Buy(T instrument, int quantity, decimal price)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be > 0");

            if (price <= 0)
                throw new ArgumentException("Price must be > 0");

            if (_holdings.ContainsKey(instrument))
                _holdings[instrument] += quantity;
            else
                _holdings[instrument] = quantity;
        }

        public decimal? Sell(T instrument, int quantity, decimal currentPrice)
        {
            if (!_holdings.ContainsKey(instrument) ||
                _holdings[instrument] < quantity)
                return null;

            _holdings[instrument] -= quantity;

            if (_holdings[instrument] == 0)
                _holdings.Remove(instrument);

            return quantity * currentPrice;
        }

        public decimal CalculateTotalValue()
        {
            return _holdings.Sum(h => h.Key.CurrentPrice * h.Value);
        }

        public (T instrument, decimal returnPercentage)? GetTopPerformer(
            Dictionary<T, decimal> purchasePrices)
        {
            var performances = _holdings
                .Where(h => purchasePrices.ContainsKey(h.Key))
                .Select(h =>
                {
                    var buyPrice = purchasePrices[h.Key];
                    var current = h.Key.CurrentPrice;
                    var returnPct = ((current - buyPrice) / buyPrice) * 100;
                    return (instrument: h.Key, returnPct);
                })
                .OrderByDescending(p => p.returnPct)
                .FirstOrDefault();

            if (performances.instrument == null)
                return null;

            return performances;
        }

        public Dictionary<T, int> GetHoldings() => _holdings;
    }

    public class Stock : IFinancialInstrument
    {
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public InstrumentType Type => InstrumentType.Stock;
        public string CompanyName { get; set; }
        public decimal DividendYield { get; set; }

        public override string ToString() => $"{Symbol} (Stock)";
    }

    public class Bond : IFinancialInstrument
    {
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public InstrumentType Type => InstrumentType.Bond;
        public DateTime MaturityDate { get; set; }
        public decimal CouponRate { get; set; }

        public override string ToString() => $"{Symbol} (Bond)";
    }


    public class TradingStrategy<T> where T : IFinancialInstrument
    {
        public void Execute(
            Portfolio<T> portfolio,
            IEnumerable<T> marketData,
            Func<T, bool> buyCondition,
            Func<T, bool> sellCondition)
        {
            foreach (var instrument in marketData)
            {
                if (buyCondition(instrument))
                    portfolio.Buy(instrument, 10, instrument.CurrentPrice);

                if (sellCondition(instrument))
                    portfolio.Sell(instrument, 5, instrument.CurrentPrice);
            }
        }

        public Dictionary<string, decimal> CalculateRiskMetrics(IEnumerable<T> instruments)
        {
            var prices = instruments.Select(i => i.CurrentPrice).ToList();

            if (!prices.Any())
                return new Dictionary<string, decimal>();

            var avg = prices.Average();
            var variance = prices.Average(p => (p - avg) * (p - avg));
            var volatility = (decimal)Math.Sqrt((double)variance);

            return new Dictionary<string, decimal>
            {
                { "Volatility", volatility },
                { "Beta", 1.1m },           // simplified
                { "SharpeRatio", avg / (volatility == 0 ? 1 : volatility) }
            };
        }
    }

    public class PriceHistory<T> where T : IFinancialInstrument
    {
        private readonly Dictionary<T, List<(DateTime, decimal)>> _history = new();

        public void AddPrice(T instrument, DateTime timestamp, decimal price)
        {
            if (!_history.ContainsKey(instrument))
                _history[instrument] = new List<(DateTime, decimal)>();

            _history[instrument].Add((timestamp, price));
        }

        public decimal? GetMovingAverage(T instrument, int days)
        {
            if (!_history.ContainsKey(instrument))
                return null;

            var prices = _history[instrument]
                .OrderByDescending(p => p.Item1)
                .Take(days)
                .Select(p => p.Item2)
                .ToList();

            if (!prices.Any())
                return null;

            return prices.Average();
        }

        public Trend DetectTrend(T instrument, int period)
        {
            if (!_history.ContainsKey(instrument))
                return Trend.Sideways;

            var prices = _history[instrument]
                .OrderByDescending(p => p.Item1)
                .Take(period)
                .Select(p => p.Item2)
                .ToList();

            if (prices.Count < 2)
                return Trend.Sideways;

            if (prices.First() > prices.Last())
                return Trend.Upward;
            if (prices.First() < prices.Last())
                return Trend.Downward;

            return Trend.Sideways;
        }
    }


    public class FinanceSystem
{
        public static void Main()
        {
            var portfolio = new Portfolio<IFinancialInstrument>();

            var stock1 = new Stock { Symbol = "AAPL", CurrentPrice = 180 };
            var stock2 = new Stock { Symbol = "TSLA", CurrentPrice = 250 };
            var bond1 = new Bond { Symbol = "GOVT10Y", CurrentPrice = 100 };

            // Buy instruments
            portfolio.Buy(stock1, 10, 150);
            portfolio.Buy(stock2, 5, 200);
            portfolio.Buy(bond1, 20, 95);

            Console.WriteLine("Total Portfolio Value: " +
                portfolio.CalculateTotalValue());

            // Trading Strategy
            var strategy = new TradingStrategy<IFinancialInstrument>();

            strategy.Execute(
                portfolio,
                new List<IFinancialInstrument> { stock1, stock2, bond1 },
                buyCondition: i => i.CurrentPrice < 200,
                sellCondition: i => i.CurrentPrice > 240);

            Console.WriteLine("After Strategy Execution Value: " +
                portfolio.CalculateTotalValue());

            // Price History
            var history = new PriceHistory<IFinancialInstrument>();

            history.AddPrice(stock1, DateTime.Now.AddDays(-3), 170);
            history.AddPrice(stock1, DateTime.Now.AddDays(-2), 175);
            history.AddPrice(stock1, DateTime.Now.AddDays(-1), 180);

            Console.WriteLine("Moving Average (2 days): " +
                history.GetMovingAverage(stock1, 2));

            Console.WriteLine("Trend: " +
                history.DetectTrend(stock1, 3));

            // Risk Metrics
            var risk = strategy.CalculateRiskMetrics(
                new List<IFinancialInstrument> { stock1, stock2, bond1 });

            Console.WriteLine("\nRisk Metrics:");
            foreach (var metric in risk)
                Console.WriteLine($"{metric.Key}: {metric.Value}");
        }
    }

