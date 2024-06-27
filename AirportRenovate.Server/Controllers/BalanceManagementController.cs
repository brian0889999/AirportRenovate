using Microsoft.AspNetCore.Mvc;
using AirportRenovate.Server.Interfaces.Repositorys;
using AirportRenovate.Server.Models;
using AutoMapper;
using AirportRenovate.Server.Datas;
using Microsoft.EntityFrameworkCore;
using NPOI.XSSF.UserModel; // NPOI 的 Excel 套件
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;
using AirportRenovate.Server.Mappings;
using AutoMapper.QueryableExtensions;
using AirportRenovate.Server.DTOs;
using AirportRenovate.Server.ViewModels;
using System.Text.RegularExpressions;
using AirportRenovate.Server.Repositorys;



namespace AirportRenovate.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BalanceManagementController(IGenericRepository<Type1> Type1, IGenericRepository<Type2> Type2, IGenericRepository<Type3> Type3,IGenericRepository<Money3DbModel> money3Repository, IMapper mapper) : ControllerBase
{
    private readonly IGenericRepository<Type1> _Type1 = Type1;
    private readonly IGenericRepository<Type2> _Type2 = Type2;
    private readonly IGenericRepository<Type3> _Type3 = Type3;
    private readonly IGenericRepository<Money3DbModel> _money3Repository = money3Repository;
    private readonly IMapper _mapper = mapper;
    [HttpGet("Subjects6")]
    public async Task<IActionResult> GetTypes1(string? group)
    {
        try
        {
            var Subjects6 = await _Type1.GetByCondition(t => t.Group == group).ToListAsync();
            return Ok(Subjects6);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }
    
    [HttpGet("Subjects7")]
    public async Task<IActionResult> GetTypes2(string? group, string? id)
    {
        try
        {
            if(string.IsNullOrEmpty(group) || string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var Subjects7 = await _Type2.GetByCondition(t => t.Group == group && t.ID != null && t.ID.Substring(0, id.Length) == id).ToListAsync();
            return Ok(Subjects7);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }
    
    [HttpGet("Subjects8")]
    public async Task<IActionResult> GetTypes3(string? group, string? id)
    {
        try
        {
            //if(string.IsNullOrEmpty(group) || string.IsNullOrEmpty(id))
            //{
            //    return NotFound();
            //}
            var Subjects8 = await _Type3.GetByCondition(t => t.Group == group && t.ID != null && t.ID == id).ToListAsync();
            return Ok(Subjects8);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("Subjects6_1")]
    public async Task<IActionResult> GetTypes1_1(string? group, string? id)
    {
        try
        {
            if (string.IsNullOrEmpty(group) || string.IsNullOrEmpty(id))
            {
                return Ok("這個組室沒有指定科目!");
            }
            var Subjects6_1 = await _Type1.GetByCondition(t => t.Group == group && t.ID != null && t.ID == id).ToListAsync();
            
            if (Subjects6_1 == null || !Subjects6_1.Any())
            {
                return Ok("這個組室沒有指定科目!");
            }
            return Ok(Subjects6_1);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BalanceFormViewModel balanceForm)
    {
        try
        {
            var results = await _money3Repository.GetByCondition(money3 => money3.Group1 == balanceForm.Group
   && money3.MoneyDbModel != null && money3.MoneyDbModel.Group == balanceForm.Group
   && money3.MoneyDbModel.Budget == balanceForm.Budget
   && money3.Year == balanceForm.Year && money3.MoneyDbModel.Year == balanceForm.Year
   && (money3.Status != null && (money3.Status.Trim() == "O" || money3.Status.Trim() == "C")))
   .Include(money3 => money3.MoneyDbModel)
     .ToListAsync();

            var query = results
            .GroupBy(m3 => new {
                Subject6 = m3.MoneyDbModel != null ? m3.MoneyDbModel.Subject6 : "",
                Subject7 = m3.MoneyDbModel != null ? m3.MoneyDbModel.Subject7 : "",
                Subject8 = m3.MoneyDbModel != null ? m3.MoneyDbModel.Subject8 : "",
                BudgetYear = m3.MoneyDbModel != null ? m3.MoneyDbModel.BudgetYear : 0,
                Final = m3.MoneyDbModel != null && decimal.TryParse(m3.MoneyDbModel.Final, out var final) ? final : 0m,
                Budget = m3.MoneyDbModel != null ? m3.MoneyDbModel.Budget : "",
                Group = m3.MoneyDbModel != null ? m3.MoneyDbModel.Group : "",
                m3.Year
            })
            .Select(g => new BudgetDetailsDto
            {
                Budget = g.Key.Budget ?? "",
                Subject6 = g.Key.Subject6 ?? "",
                Group = g.Key.Group ?? "",
                Subject7 = g.Key.Subject7 ?? "",
                Subject8 = g.Key.Subject8 ?? "",
                BudgetYear = g.Key.BudgetYear,
                Final = g.Key.Final,
                General = g.Sum(x => x.Text == "一般" ? x.PurchaseMoney : 0),
                Out = g.Sum(x => x.Text == "勻出" ? x.PurchaseMoney : 0),
                UseBudget = g.Key.BudgetYear - g.Sum(x => x.Text == "勻出" ? x.PurchaseMoney : 0) - g.Sum(x => x.Text == "一般" ? x.PurchaseMoney : 0) + g.Key.Final,
                In = g.Sum(x => x.Text == "勻入" ? x.PurchaseMoney : 0),
                InActual = g.Sum(x => x.Text == "勻入" ? x.PayMoney : 0),
                InBalance = g.Sum(x => x.Text == "勻入" ? x.PurchaseMoney : 0) - g.Sum(x => x.Text == "勻入" ? x.PayMoney : 0),
                SubjectActual = g.Sum(x => x.Text == "勻入" ? x.PayMoney : 0) + g.Sum(x => x.Text == "一般" ? x.PayMoney : 0)
            }).ToList();

            if (!query.Any())
            {
                return Ok("沒有找到符合條件的資料!");
            }

            return Ok(query);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }
}



