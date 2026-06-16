using System.Globalization;
using SalesAnalytics.Services;

// Bangladeshi Taka formatting — fall back gracefully if culture is not installed.
var moneyCulture = TryGetCulture("bn-BD") ?? CultureInfo.InvariantCulture;
var moneyFormat = (decimal v) => v.ToString("N2", moneyCulture);

var csvPath = Path.Combine(AppContext.BaseDirectory, "Data", "transactions.csv");
var transactions = TransactionCsvReader.Read(csvPath);

Console.WriteLine("=========================================");
Console.WriteLine("  Sales Analytics Pipeline");
Console.WriteLine("=========================================");
Console.WriteLine($"Loaded {transactions.Count} transactions from {csvPath}");
Console.WriteLine();

if (transactions.Count == 0)
{
    Console.WriteLine("No transactions found. Make sure Data/transactions.csv exists next to the executable.");
    return;
}

var analyzer = new SalesAnalyzer(transactions);

// ── 1. Revenue by category ───────────────────────────────────────────────
Console.WriteLine("── Revenue by Category ──────────────────");
var byCategory = analyzer.GetRevenueByCategory()
    .OrderByDescending(kv => kv.Value)
    .ToList();
foreach (var (category, revenue) in byCategory)
{
    Console.WriteLine($"  {category,-20} {moneyFormat(revenue),15} BDT");
}
Console.WriteLine();

// ── 2. Top 5 customers ───────────────────────────────────────────────────
Console.WriteLine("── Top 5 Customers by Spending ──────────");
var top5 = analyzer.GetTop5Customers();
foreach (var c in top5)
{
    Console.WriteLine($"  #{c.CustomerId}  {c.CustomerName,-22} {moneyFormat(c.TotalSpent),15} BDT");
}
Console.WriteLine();

// ── 3. Month-over-month trend ────────────────────────────────────────────
Console.WriteLine("── Monthly Revenue Trend ────────────────");
var monthly = analyzer.GetMonthlyRevenueTrend();
foreach (var m in monthly)
{
    Console.WriteLine($"  {m.Year}-{m.Month:D2}  {moneyFormat(m.Revenue),15} BDT");
}
Console.WriteLine();

static CultureInfo? TryGetCulture(string name)
{
    try { return CultureInfo.GetCultureInfo(name); }
    catch (CultureNotFoundException) { return null; }
}
