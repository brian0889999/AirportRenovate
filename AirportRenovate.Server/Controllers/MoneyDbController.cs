using Microsoft.AspNetCore.Mvc;
using AirportRenovate.Server.Models;
using AirportRenovate.Server.Datas;
using Microsoft.EntityFrameworkCore;

namespace AirportRenovate.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoneyDbController : ControllerBase
{
    private readonly AirportBudgetDbContext _context;
    public MoneyDbController(AirportBudgetDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> GetData([FromBody] QueryDto queryDto)
    {
        try
        {
            var yearParameter = queryDto.Year;

            var results = await _context.Money
                .Where(money => money.Group == "工務組" && money.Year == yearParameter)
                .Join(_context.Money3,
                    money => money.Budget,
                    money3 => money3.Name,
                    (money, money3) => new { Money = money, Money3 = money3 })
                .Where(joinResult => joinResult.Money3.Group1 == "工務組" &&
                                     (joinResult.Money3.status == "O" || joinResult.Money3.status == "C") &&
                                     joinResult.Money3.year == yearParameter)
                .GroupBy(joinResult => new { joinResult.Money.Subject6, joinResult.Money.Subject7, joinResult.Money.Subject8, joinResult.Money.BudgetYear, joinResult.Money.Final, joinResult.Money.Budget, joinResult.Money.Group, joinResult.Money3.year })
                .Select(g => new
                {
                    g.Key.Subject6,
                    g.Key.Subject7,
                    g.Key.Subject8,
                    g.Key.BudgetYear,
                    g.Key.Final,
                    g.Key.Budget,
                    g.Key.Group,
                    Year = g.Key.year,
                    General = g.Sum(x => x.Money3.Text == "一般" ? x.Money3.PurchaseMoney : 0),
                    Out = g.Sum(x => x.Money3.Text == "勻出" ? x.Money3.PurchaseMoney : 0),
                    UseBudget = g.Key.BudgetYear - g.Sum(x => x.Money3.Text == "勻出" ? x.Money3.PurchaseMoney : 0) - g.Sum(x => x.Money3.Text == "一般" ? x.Money3.PurchaseMoney : 0) + g.Key.Final,
                    In = g.Sum(x => x.Money3.Text == "勻入" ? x.Money3.PurchaseMoney : 0),
                    InActual = g.Sum(x => x.Money3.Text == "勻入" ? x.Money3.PayMoney : 0),
                    InBalance = g.Sum(x => x.Money3.Text == "勻入" ? x.Money3.PurchaseMoney : 0) - g.Sum(x => x.Money3.Text == "勻入" ? x.Money3.PayMoney : 0),
                    SubjectActual = g.Sum(x => x.Money3.Text == "勻入" ? x.Money3.PayMoney : 0) + g.Sum(x => x.Money3.Text == "一般" ? x.Money3.PayMoney : 0)
                })
                .ToListAsync();

            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }
}
    public class QueryDto
{
    public int Year { get; set; }
}


