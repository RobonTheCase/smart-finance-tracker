namespace FinanceTracker.Api.Models;

public class CreateTransactionDto
{
    public string Type { get; set; } = "";
    public string? SubType { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; } = "";
    public string? Notes { get; set; }
    public bool IsRecurring { get; set; }
}