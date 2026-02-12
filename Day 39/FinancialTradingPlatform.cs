using System;
using System.Collections.Generic;
using System.Linq;

namespace Day39
{
    // ==========================================
    // Base Interface & Enums
    // ==========================================
    public interface IFinancialInstrument
    {
        string? Symbol { get; }
        decimal CurrentPrice { get; set; }
        InstrumentType Type { get; }
    }

    public enum InstrumentType { Stock, Bond, Option, Future }
    public enum Trend { Upward, Downward, Sideways }

    // ==========================================
    // Generic Portfolio
    // ==========================================
    public class Portfolio<T> where T : IFinancialInstrument
    {
        private readonly Dictionary<T, int> _holdings = new();

        public void Buy(T instrument, int quantity, decimal executionPrice)
        {
            if (quantity <= 0 || executionPrice <= 0)
                throw new ArgumentException("Quantity and price must be positive.");

            instrument.CurrentPrice = executionPrice;

            if (_holdings.ContainsKey(instrument))
                _holdings[instrument] += quantity;
            else
                _holdings[instrument] = quantity;
        }

        public decimal? Sell(T instrument, int quantity, decimal executionPrice)
        {
            if (!_holdings.ContainsKey(instrument))
                return null;

            if (_holdings[instrument] < quantity)
                throw new InvalidOperationException("Insufficient quantity to sell.");

            instrument.CurrentPrice = executionPrice;
            _holdings[instrument] -= quantity;

            if (_holdings[instrument] == 0)
                _holdings.Remove(instrument);

            return quantity * executionPrice;
        }

        public decimal CalculateTotalValue()
        {
            return _holdings.Sum(position =>
                position.Key.CurrentPrice * position.Value);
        }

        public Dictionary<T, int> GetHoldings() => _holdings;
    }

    // ==========================================
    // Specialized Instrument
    // ==========================================
    public class Stock : IFinancialInstrument
    {
        public string? Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public InstrumentType Type => InstrumentType.Stock;
        public string? CompanyName { get; set; }
        public decimal DividendYield { get; set; }
    }

    // ==========================================
    // Trading Strategy
    // ==========================================
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
                {
                    portfolio.Buy(instrument, 1, instrument.CurrentPrice);
                    Console.WriteLine($"Bought {instrument.Symbol}");
                }
                else if (sellCondition(instrument))
                {
                    var proceeds = portfolio.Sell(instrument, 1, instrument.CurrentPrice);
                    if (proceeds.HasValue)
                        Console.WriteLine($"Sold {instrument.Symbol} for {proceeds.Value}");
                }
            }
        }
    }

    // ==========================================
    // Price History
    // ==========================================
    public class PriceHistory<T> where T : IFinancialInstrument
    {
        private readonly Dictionary<T, List<(DateTime Timestamp, decimal Price)>> _priceHistory = new();

        public void AddPrice(T instrument, DateTime timestamp, decimal price)
        {
            if (!_priceHistory.ContainsKey(instrument))
                _priceHistory[instrument] = new List<(DateTime, decimal)>();

            _priceHistory[instrument].Add((timestamp, price));
        }

        public Trend DetectTrend(T instrument, int period)
        {
            if (!_priceHistory.ContainsKey(instrument))
                return Trend.Sideways;

            var recentPrices = _priceHistory[instrument]
                .OrderByDescending(p => p.Timestamp)
                .Take(period)
                .Select(p => p.Price)
                .ToList();

            if (recentPrices.Count < 2)
                return Trend.Sideways;

            if (recentPrices.First() > recentPrices.Last())
                return Trend.Upward;

            if (recentPrices.First() < recentPrices.Last())
                return Trend.Downward;

            return Trend.Sideways;
        }
    }

    // ==========================================
    // Financial Trading Platform
    // ==========================================
    public class FinancialTradingPlatform
    {
        public static void Run()
        {
            var stockPortfolio = new Portfolio<Stock>();
            var tradingStrategy = new TradingStrategy<Stock>();
            var priceHistory = new PriceHistory<Stock>();
            var marketStocks = new List<Stock>();

            bool exitRequested = false;

            while (!exitRequested)
            {
                Console.WriteLine("\n====== FINANCIAL TRADING PLATFORM ======");
                Console.WriteLine("1. Add Stock to Market");
                Console.WriteLine("2. Buy Stock");
                Console.WriteLine("3. Sell Stock");
                Console.WriteLine("4. View Portfolio Value");
                Console.WriteLine("5. Execute Trading Strategy");
                Console.WriteLine("6. Add Price History");
                Console.WriteLine("7. Detect Stock Trend");
                Console.WriteLine("8. Exit");

                Console.Write("Select an option: ");
                string selectedOption = Console.ReadLine() ?? string.Empty;

                try
                {
                    switch (selectedOption)
                    {
                        case "1":

                            Console.Write("Enter Stock Symbol: ");
                            string newStockSymbol = Console.ReadLine() ?? string.Empty;

                            Console.Write("Enter Company Name: ");
                            string newCompanyName = Console.ReadLine() ?? string.Empty;

                            Console.Write("Enter Current Price: ");
                            string? priceInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(priceInput) || !decimal.TryParse(priceInput, out decimal newStockPrice))
                            {
                                Console.WriteLine("Invalid price entered.");
                                break;
                            }

                            var newStock = new Stock
                            {
                                Symbol = newStockSymbol,
                                CompanyName = newCompanyName,
                                CurrentPrice = newStockPrice
                            };

                            marketStocks.Add(newStock);
                            Console.WriteLine("Stock added to market successfully.");
                            break;

                        case "2":

                            if (!marketStocks.Any())
                            {
                                Console.WriteLine("No stocks available in market.");
                                break;
                            }

                            Console.WriteLine("\nAvailable Stocks:");
                            foreach (var stock in marketStocks)
                                Console.WriteLine($"{stock.Symbol} - {stock.CompanyName} - {stock.CurrentPrice}");

                            Console.Write("Enter Stock Symbol to Buy: ");
                            string? buySymbolInput = Console.ReadLine();
                            string buySymbol = buySymbolInput ?? string.Empty;

                            Console.Write("Enter Quantity: ");
                            string? buyQuantityInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(buyQuantityInput) || !int.TryParse(buyQuantityInput, out int buyQuantity))
                            {
                                Console.WriteLine("Invalid quantity entered.");
                                break;
                            }

                            var stockToBuy =
                                marketStocks.FirstOrDefault(s => s.Symbol == buySymbol);

                            if (stockToBuy != null)
                                stockPortfolio.Buy(stockToBuy, buyQuantity, stockToBuy.CurrentPrice);
                            else
                                Console.WriteLine("Stock not found.");

                            break;

                        case "3":

                            Console.WriteLine("\nCurrent Holdings:");
                            foreach (var holding in stockPortfolio.GetHoldings())
                                Console.WriteLine($"{holding.Key.Symbol} - Quantity: {holding.Value}");

                            Console.Write("Enter Stock Symbol to Sell: ");
                            string? sellSymbolInput = Console.ReadLine();
                            string sellSymbol = sellSymbolInput ?? string.Empty;

                            Console.Write("Enter Quantity: ");
                            string? sellQuantityInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(sellQuantityInput) || !int.TryParse(sellQuantityInput, out int sellQuantity))
                            {
                                Console.WriteLine("Invalid quantity entered.");
                                break;
                            }

                            var stockToSell =
                                marketStocks.FirstOrDefault(s => s.Symbol == sellSymbol);

                            if (stockToSell != null)
                            {
                                var saleProceeds =
                                    stockPortfolio.Sell(stockToSell, sellQuantity, stockToSell.CurrentPrice);

                                if (saleProceeds.HasValue)
                                    Console.WriteLine($"Sold for {saleProceeds.Value}");
                                else
                                    Console.WriteLine("Stock not in portfolio.");
                            }
                            else
                            {
                                Console.WriteLine("Stock not found.");
                            }

                            break;

                        case "4":

                            decimal totalValue = stockPortfolio.CalculateTotalValue();
                            Console.WriteLine($"Total Portfolio Value: {totalValue}");
                            break;

                        case "5":

                            tradingStrategy.Execute(
                                stockPortfolio,
                                marketStocks,
                                stock => stock.CurrentPrice < 100,
                                stock => stock.CurrentPrice > 200);

                            break;

                        case "6":

                            Console.Write("Enter Stock Symbol: ");
                            string? historySymbolInput = Console.ReadLine();
                            string historySymbol = historySymbolInput ?? string.Empty;

                            Console.Write("Enter New Price: ");
                            string? historyPriceInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(historyPriceInput) || !decimal.TryParse(historyPriceInput, out decimal historyPrice))
                            {
                                Console.WriteLine("Invalid price entered.");
                                break;
                            }

                            var historyStock =
                                marketStocks.FirstOrDefault(s => s.Symbol == historySymbol);

                            if (historyStock != null)
                            {
                                priceHistory.AddPrice(historyStock, DateTime.Now, historyPrice);
                                Console.WriteLine("Price history updated.");
                            }
                            else
                            {
                                Console.WriteLine("Stock not found.");
                            }

                            break;

                        case "7":

                            Console.Write("Enter Stock Symbol: ");
                            string trendSymbol = Console.ReadLine() ?? string.Empty;

                            var stockForTrend =
                                marketStocks.FirstOrDefault(s => s.Symbol == trendSymbol);

                            if (stockForTrend != null)
                            {
                                Trend detectedTrend =
                                    priceHistory.DetectTrend(stockForTrend, 3);

                                Console.WriteLine($"Detected Trend: {detectedTrend}");
                            }
                            else
                            {
                                Console.WriteLine("Stock not found.");
                            }

                            break;

                        case "8":
                            exitRequested = true;
                            Console.WriteLine("Exiting Trading Platform...");
                            break;

                        default:
                            Console.WriteLine("Invalid menu option selected.");
                            break;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Error: {exception.Message}");
                }
            }
        }
    }
}
