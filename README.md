# Sales Analytics Pipeline — Starter

LINQ & Lambda assignment for the **Lumens Academy Full Stack .NET Developer** program.

You will implement three sales-analytics queries against a CSV of transactions, using **LINQ method syntax** only.

---

## Prerequisites

- .NET SDK **8.0** or newer (`dotnet --version` should print `8.x.x` or higher)
- Git
- Any editor — Visual Studio, VS Code, Rider, Notepad++

---

## Project layout

```
starter/
├── Data/
│   └── transactions.csv          ← sample data (92 transactions, Jan–Jun 2025)
├── Models/
│   └── Transaction.cs            ← record type — do not modify
├── Services/
│   ├── TransactionCsvReader.cs   ← CSV parser — do not modify
│   └── SalesAnalyzer.cs          ← 🎯 IMPLEMENT THE THREE METHODS HERE
├── Program.cs                    ← entry point that calls the analyzer
└── SalesAnalytics.Starter.csproj
```

---

## Run the project

From the `starter/` folder:

```bash
dotnet run
```

On a fresh checkout the three queries throw `NotImplementedException` — that is your TODO list.

---

## What to implement

Open `Services/SalesAnalyzer.cs` and implement the three methods:

1. **`GetRevenueByCategory()`** — total revenue per category
   - `GroupBy` + `Sum`
   - Treat `null` or whitespace `Category` as the bucket `"Uncategorized"`

2. **`GetTop5Customers()`** — top 5 customers by total spending, descending
   - `GroupBy` + `Sum` + `OrderByDescending` + `Take(5)`

3. **`GetMonthlyRevenueTrend()`** — revenue grouped by year + month, oldest first
   - `GroupBy` on `Date.Year` + `Date.Month` + `Sum`, then `OrderBy` chronologically

---

## Rules

- ✅ **LINQ method syntax only.** No `foreach` loops for data processing. (You may use `foreach` in `Program.cs` for printing — that is display, not processing.)
- ✅ **Handle empty input** gracefully. Empty CSV ⇒ empty results, no exceptions.
- ✅ **Handle missing categories.** Blank `Category` ⇒ `"Uncategorized"`.
- ✅ **Zero compiler warnings.** `TreatWarningsAsErrors` is enabled in the `.csproj` — the build will fail if you add a warning.

---

## Expected output

When you run `dotnet run` on the supplied CSV, you should see something close to:

```
── Revenue by Category ──────────────────
  Clothing                   81,800.00 BDT
  Electronics                79,450.00 BDT
  Home & Kitchen             52,950.00 BDT
  Books                      52,750.00 BDT
  Sports                     44,660.00 BDT
  Toys                       32,150.00 BDT
  Beauty                     18,480.00 BDT
  Uncategorized               4,580.00 BDT

── Top 5 Customers by Spending ──────────
  #101  Tahsin Rahman            75,930.00 BDT
  #117  Junayed Karim            42,720.00 BDT
  #113  Mehedi Hasan             30,520.00 BDT
  #105  Imran Hossain            30,340.00 BDT
  #120  Rumana Pervin            29,200.00 BDT

── Monthly Revenue Trend ────────────────
  2025-01     105,500.00 BDT
  2025-02      29,430.00 BDT
  2025-03      62,570.00 BDT
  2025-04      59,460.00 BDT
  2025-05      81,690.00 BDT
  2025-06      28,170.00 BDT
```

Your numbers must match exactly when you use the provided CSV.

---

## Submission

1. Push your completed project to a **public GitHub repository** named `linq-sales-analytics`
2. Make sure `dotnet run` works on a clean clone — no missing files, no hardcoded paths
3. Submit the repository link via **Google Classroom** before the deadline

Your final commit should include:
- All source files (your implemented `SalesAnalyzer.cs`)
- `Data/transactions.csv` (do not delete)
- This README (you may extend it with your own notes)
- A `.gitignore` that excludes `bin/` and `obj/`

---

## Tips

- Start with `GetRevenueByCategory` — it is the simplest of the three.
- Use a projection (`Select` into a record) **before** ordering when you want to order by a derived field.
- For the monthly trend, build the group key from `new { t.Date.Year, t.Date.Month }` — anonymous types compare by value, so they group correctly.
- Run early, run often. Print the output and verify against the expected numbers above.

Good luck — and remember: **method syntax, no foreach in data processing.**
