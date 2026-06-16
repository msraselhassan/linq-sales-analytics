namespace SalesAnalytics.Models;

/// <summary>
/// Represents a single sales transaction read from the CSV.
/// Category is nullable so missing/blank categories from the source data can be represented faithfully.
/// </summary>
public sealed record Transaction(
    int TransactionId,
    DateTime Date,
    int CustomerId,
    string CustomerName,
    string? Category,
    string Product,
    int Quantity,
    decimal UnitPrice,
    decimal TotalAmount
);
