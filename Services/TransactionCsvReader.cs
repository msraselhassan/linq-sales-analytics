using System.Globalization;
using SalesAnalytics.Models;

namespace SalesAnalytics.Services;

/// <summary>
/// Lightweight CSV reader for the assignment.
/// Provided as-is — participants should NOT need to modify this file.
/// Malformed rows are skipped silently so the analyzer can still run.
/// </summary>
public static class TransactionCsvReader
{
    public static IReadOnlyList<Transaction> Read(string path)
    {
        if (!File.Exists(path))
        {
            return Array.Empty<Transaction>();
        }

        var lines = File.ReadAllLines(path);
        if (lines.Length <= 1)
        {
            return Array.Empty<Transaction>();
        }

        return lines
            .Skip(1) // header
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(TryParse)
            .Where(t => t is not null)
            .Select(t => t!)
            .ToList();
    }

    private static Transaction? TryParse(string line)
    {
        try
        {
            var parts = line.Split(',');
            if (parts.Length < 9)
            {
                return null;
            }

            var culture = CultureInfo.InvariantCulture;
            var category = string.IsNullOrWhiteSpace(parts[4]) ? null : parts[4].Trim();

            return new Transaction(
                TransactionId: int.Parse(parts[0], culture),
                Date: DateTime.Parse(parts[1], culture),
                CustomerId: int.Parse(parts[2], culture),
                CustomerName: parts[3].Trim(),
                Category: category,
                Product: parts[5].Trim(),
                Quantity: int.Parse(parts[6], culture),
                UnitPrice: decimal.Parse(parts[7], culture),
                TotalAmount: decimal.Parse(parts[8], culture)
            );
        }
        catch
        {
            return null;
        }
    }
}
