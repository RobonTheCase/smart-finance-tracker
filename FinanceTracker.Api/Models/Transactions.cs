namespace FinanceTracker.Api.Models;

public class Transaction
{
    public int Id { get; set; }
    public string Type { get; set; } = ""; 
    public string? SubType { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; } = "";
    public DateTime Date { get; set; }
    public string? Notes { get; set; }
    public bool IsRecurring { get; set; }
    public string? Frequency { get; set; } 
    public int? RecurringId { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}