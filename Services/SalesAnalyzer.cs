using SalesAnalytics.Models;

namespace SalesAnalytics.Services;

/// <summary>
/// 🎯 ASSIGNMENT: Implement the three methods below using LINQ method syntax.
///
/// Rules:
///   • Use LINQ method syntax (.GroupBy, .Select, .Sum, .OrderByDescending, .Take, .ToList, ...)
///   • NO `foreach` loops for data processing — only for printing in Program.cs
///   • Handle empty input gracefully (return empty result, do not throw)
///   • Treat null/blank Category as the bucket "Uncategorized"
///   • Zero compiler warnings — the project has TreatWarningsAsErrors enabled
/// </summary>
public sealed class SalesAnalyzer
{
    private readonly IReadOnlyList<Transaction> _transactions;

    public SalesAnalyzer(IEnumerable<Transaction> transactions)
    {
        _transactions = transactions?.ToList() ?? new List<Transaction>();
    }

    /// <summary>
    /// Total revenue per category.
    ///
    /// Hint: GroupBy(category) + Sum(TotalAmount).
    /// Edge case: rows where Category is null or whitespace should be grouped as "Uncategorized".
    /// </summary>
    /// <returns>
    /// Dictionary keyed by category name, value = total revenue for that category.
    /// Return an empty dictionary if there are no transactions.
    /// </returns>
    public IReadOnlyDictionary<string, decimal> GetRevenueByCategory()
    {
        // TODO: implement using LINQ method syntax.
        // throw new NotImplementedException("GetRevenueByCategory is not implemented yet.");

      return  _transactions.GroupBy(g => string.IsNullOrWhiteSpace(g.Category) ? "Uncategorized" : g.Category.Trim()).ToDictionary(x => x.Key, x => x.Sum(t => t.TotalAmount));


    }

    /// <summary>
    /// Top 5 customers by total spending, descending.
    ///
    /// Hint: GroupBy(CustomerId) + Sum + OrderByDescending + Take(5).
    /// </summary>
    /// <returns>
    /// Up to 5 customers ordered by TotalSpent descending.
    /// Returns fewer entries if there are fewer than 5 unique customers.
    /// </returns>
    public IReadOnlyList<CustomerSpending> GetTop5Customers()
    {
        // TODO: implement using LINQ method syntax.
        //throw new NotImplementedException("GetTop5Customers is not implemented yet.");

       return _transactions.GroupBy(c=>c.CustomerId).
            Select(cs=> new CustomerSpending(
            cs.Key,
            cs.First().CustomerName,
            cs.Sum(x=>x.TotalAmount)
            ))
            .OrderByDescending(t=>t.TotalSpent)
            .Take(5).ToList();
    }

    /// <summary>
    /// Month-over-month revenue trend, ordered chronologically (oldest first).
    ///
    /// Hint: GroupBy a key built from Date.Year + Date.Month, then Sum(TotalAmount),
    ///       then OrderBy year, then month.
    /// </summary>
    /// <returns>
    /// One entry per (year, month) that has at least one transaction,
    /// ordered chronologically from the earliest month.
    /// </returns>
    public IReadOnlyList<MonthlyRevenue> GetMonthlyRevenueTrend()
    {
        // TODO: implement using LINQ method syntax.
        //throw new NotImplementedException("GetMonthlyRevenueTrend is not implemented yet.");


      return  _transactions.GroupBy(x => new { x.Date.Year, x.Date.Month })
                     .Select(mr => new MonthlyRevenue(mr.Key.Year, mr.Key.Month, mr.Sum(x => x.TotalAmount)))
                     .OrderBy(m => m.Year)
                     .ThenBy(n => n.Month)
                     .ToList();
    }
}

/// <summary>One row of the "top customers" report.</summary>
public sealed record CustomerSpending(int CustomerId, string CustomerName, decimal TotalSpent);

/// <summary>One row of the monthly revenue trend.</summary>
public sealed record MonthlyRevenue(int Year, int Month, decimal Revenue);
