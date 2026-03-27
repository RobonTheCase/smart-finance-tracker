using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Api.Data;
using FinanceTracker.Api.Models;
using FinanceTracker.Api.Services;

namespace FinanceTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly RecurringService _recurringService;


    public DashboardController(AppDbContext context, RecurringService recurringService)
    {
        _context = context;
        _recurringService = recurringService;
    }

[HttpGet("summary")]
public async Task<ActionResult<DashboardSummaryDto>> GetSummary()
{
    await _recurringService.GenerateRecurringTransactions();
    var transactions = await _context.Transactions.ToListAsync();
    

    var income = transactions
        .Where(t => t.Type == "income")
        .Sum(t => t.Amount);

    var expense = transactions
        .Where(t => t.Type == "expense")
        .Sum(t => t.Amount);

    var savings = transactions
        .Where(t => t.Type == "transfer" && t.SubType == "savings")
        .Sum(t => t.Amount);  

    var investments = transactions
        .Where(t => t.Type == "transfer" && t.SubType == "investment")
        .Sum(t => t.Amount); 

    var result = new DashboardSummaryDto
    {
        TotalIncome = income,
        TotalExpense = expense,
        Balance = income - expense,
        Savings = savings,
        Investments = investments
    };

    return Ok(result);
}

[HttpGet("categories")]
public async Task<ActionResult<IEnumerable<CategoryBreakdownDto>>> GetCategoryBreakdown()
{

   var transactions = await _context.Transactions
        .Where(t => t.Type == "expense")
        .ToListAsync();

    var result = transactions
        .GroupBy(t => t.Category)
        .Select(g => new CategoryBreakdownDto
        {
            Category = g.Key,
            Total = g.Sum(t => t.Amount)
        })
        .OrderByDescending(x => x.Total)
        .ToList();

    return Ok(result);
}

[HttpGet("monthly-comparison")]
public async Task<ActionResult<MonthlyComparisonDto>> GetMonthlyComparison()
{
    var now = DateTime.UtcNow;

    var startOfThisMonth = new DateTime(now.Year, now.Month, 1);
    var startOfLastMonth = startOfThisMonth.AddMonths(-1);
    var endOfLastMonth = startOfThisMonth.AddDays(-1);

    var thisMonthTransactions = await _context.Transactions
    .Where(t => t.Type == "expense" && t.Date >= startOfThisMonth)
    .ToListAsync();

    var thisMonthTotal = thisMonthTransactions.Sum(t => t.Amount);

    var lastMonthTransactions = await _context.Transactions
    .Where(t => t.Type == "expense" &&
                t.Date >= startOfLastMonth &&
                t.Date <= endOfLastMonth)
    .ToListAsync();

    var lastMonthTotal = lastMonthTransactions.Sum(t => t.Amount);

    var result = new MonthlyComparisonDto
    {
        ThisMonth = thisMonthTotal,
        LastMonth = lastMonthTotal,
        Difference = thisMonthTotal - lastMonthTotal
    };

    return Ok(result);
}

[HttpGet("insights")]
public async Task<ActionResult<IEnumerable<string>>> GetInsights()
{
    var now = DateTime.UtcNow;
    var startOfThisMonth = new DateTime(now.Year, now.Month, 1);
    var startOfLastMonth = startOfThisMonth.AddMonths(-1);
    var endOfLastMonth = startOfThisMonth.AddDays(-1);

    var thisMonth = await _context.Transactions
        .Where(t => t.Type == "expense" && t.Date >= startOfThisMonth)
        .ToListAsync();

    var lastMonth = await _context.Transactions
        .Where(t => t.Type == "expense" &&
                    t.Date >= startOfLastMonth &&
                    t.Date <= endOfLastMonth)
        .ToListAsync();

    var insights = new List<string>();

    var thisTotal = thisMonth.Sum(t => t.Amount);
    var lastTotal = lastMonth.Sum(t => t.Amount);

    if (lastTotal > 0)
    {
        var change = ((thisTotal - lastTotal) / lastTotal) * 100;

        if (change > 0)
            insights.Add($"You spent {Math.Round(change)}% more this month");
        else
            insights.Add($"You spent {Math.Abs(Math.Round(change))}% less this month");
    }

    var topCategory = thisMonth
        .GroupBy(t => t.Category)
        .OrderByDescending(g => g.Sum(x => x.Amount))
        .FirstOrDefault();

    if (topCategory != null)
    {
        insights.Add($"Top spending category: {topCategory.Key}");
    }

    return Ok(insights);
}
}