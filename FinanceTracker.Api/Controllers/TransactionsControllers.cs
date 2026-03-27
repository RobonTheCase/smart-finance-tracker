using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Api.Data;
using FinanceTracker.Api.Models;

namespace FinanceTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/transactions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(
        [FromQuery] string? category,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        var query = _context.Transactions.AsQueryable();

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(t => t.Category.ToLower() == category.ToLower());
        }

        if (startDate.HasValue)
        {
            query = query.Where(t => t.Date >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(t => t.Date <= endDate.Value);
        }

        var transactions = await query
            .OrderByDescending(t => t.Date)
            .ToListAsync();

        return Ok(transactions);
    }

    // POST: api/transactions
    [HttpPost]
    public async Task<ActionResult<Transaction>> CreateTransaction(CreateTransactionDto dto)
    {
      var transaction = new Transaction
      {
          Type = dto.Type,
          SubType = dto.SubType,
          Amount = dto.Amount,
          Category = dto.Category,
          Notes = dto.Notes,
          IsRecurring = dto.IsRecurring,

          Date = DateTime.UtcNow,
          CreatedAt = DateTime.UtcNow
      };

      _context.Transactions.Add(transaction);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetTransactions), new { id = transaction.Id }, transaction);
    }

    // DELETE: api/transactions/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);

        if (transaction == null)
            return NotFound();

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransaction(int id, UpdateTransactionDto dto)
    {
        var transaction = await _context.Transactions.FindAsync(id);

        if (transaction == null)
            return NotFound();

        transaction.Type = dto.Type;
        transaction.Amount = dto.Amount;
        transaction.Category = dto.Category;
        transaction.Notes = dto.Notes;
        transaction.IsRecurring = dto.IsRecurring;

        await _context.SaveChangesAsync();

        return Ok(transaction);
    }
}