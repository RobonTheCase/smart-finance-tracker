namespace FinanceTracker.Api.Models;

public class DashboardSummaryDto
{
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal Balance { get; set; }
    public decimal Savings { get; set; }
    public decimal Investments { get; set; }
}

public class CategoryBreakdownDto
{
    public string Category { get; set; } = "";
    public decimal Total { get; set; }
}

public class MonthlyComparisonDto
{
    public decimal ThisMonth { get; set; }
    public decimal LastMonth { get; set; }
    public decimal Difference { get; set; }
}