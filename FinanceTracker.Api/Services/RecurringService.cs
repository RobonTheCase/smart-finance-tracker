using FinanceTracker.Api.Data;
using FinanceTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Api.Services;

public class RecurringService
{
    private readonly AppDbContext _context;

    public RecurringService(AppDbContext context)
    {
        _context = context;
    }
    public async Task GenerateRecurringTransactions()
    {
      Console.WriteLine("RECURRING SERVICE HIT");

        var now = DateTime.UtcNow;

        var templates = await _context.Transactions
            .Where(t => t.IsRecurring && t.RecurringId == null)
            .ToListAsync();

        foreach (var t in templates)
        {
            if (t.Frequency != "monthly") continue;

            var exists = await _context.Transactions.AnyAsync(x =>
                x.RecurringId == t.Id &&
                x.Date.Month == now.Month &&
                x.Date.Year == now.Year
            );

            if (!exists)
            {
                var newTransaction = new Transaction
                {
                    Type = t.Type,
                    Amount = t.Amount,
                    Category = t.Category,
                    Notes = t.Notes,
                    IsRecurring = true,
                    Frequency = t.Frequency,
                    SubType = t.SubType,
                    RecurringId = t.Id,
                    Date = now,
                    CreatedAt = now
                };

                _context.Transactions.Add(newTransaction);
            }
        }

        await _context.SaveChangesAsync();
    }
}