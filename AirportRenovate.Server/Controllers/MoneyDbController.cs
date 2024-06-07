using Microsoft.AspNetCore.Mvc;
using AirportRenovate.Server.Models;
using AirportRenovate.Server.Datas;
using Microsoft.EntityFrameworkCore;
using NPOI.XSSF.UserModel; // NPOI 的 Excel 套件
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NPOI.SS.Formula.Functions;
using AirportRenovate.Server.Mappings;
using AutoMapper.QueryableExtensions;
using AirportRenovate.Server.DTOs;



namespace AirportRenovate.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoneyDbController : ControllerBase
{
    private readonly AirportBudgetDbContext _context;
    private readonly IMapper _mapper;
    public MoneyDbController(AirportBudgetDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    //[HttpPost("ByYear")]
    //public async Task<IActionResult> GetData([FromBody] QueryDto queryDto) // 前端傳Year值,後端回傳符合Year值的工務組資料
    [HttpGet("ByYear")]
    public async Task<IActionResult> GetData(int Year) // 前端傳Year值,後端回傳符合Year值的工務組資料
    {
        try
        {
            //var A = _context.Money.Include(x=>x.Money3DbModels).ToList();
            //A[0].Money3DbModels[0];
            //var yearParameter = Year;

            //var results = await _context.Money
            //    .Where(money => money.Group == "工務組" && money.Year == yearParameter)
            //    .Join(_context.Money3,
            //        money => money.Budget,
            //        money3 => money3.Name,
            //        (money, money3) => new { Money = money, Money3 = money3 })
            //    .Where(joinResult => joinResult.Money3.Group1 == "工務組" &&
            //                         (joinResult.Money3.Status == "O" || joinResult.Money3.Status == "C") &&
            //                         joinResult.Money3.Year == yearParameter)
            //    .GroupBy(joinResult => new { joinResult.Money.Subject6, joinResult.Money.Subject7, joinResult.Money.Subject8, joinResult.Money.BudgetYear, joinResult.Money.Final, joinResult.Money.Budget, joinResult.Money.Group, joinResult.Money3.Year })
            //    .Select(g => new
            //    {
            //        g.Key.Subject6,
            //        g.Key.Subject7,
            //        g.Key.Subject8,
            //        g.Key.BudgetYear,
            //        g.Key.Final,
            //        g.Key.Budget,
            //        g.Key.Group,
            //        Year = g.Key.Year,
            //        General = g.Sum(x => x.Money3.Text == "一般" ? x.Money3.PurchaseMoney : 0),
            //        Out = g.Sum(x => x.Money3.Text == "勻出" ? x.Money3.PurchaseMoney : 0),
            //        UseBudget = g.Key.BudgetYear - g.Sum(x => x.Money3.Text == "勻出" ? x.Money3.PurchaseMoney : 0) - g.Sum(x => x.Money3.Text == "一般" ? x.Money3.PurchaseMoney : 0) + g.Key.Final,
            //        In = g.Sum(x => x.Money3.Text == "勻入" ? x.Money3.PurchaseMoney : 0),
            //        InActual = g.Sum(x => x.Money3.Text == "勻入" ? x.Money3.PayMoney : 0),
            //        InBalance = g.Sum(x => x.Money3.Text == "勻入" ? x.Money3.PurchaseMoney : 0) - g.Sum(x => x.Money3.Text == "勻入" ? x.Money3.PayMoney : 0),
            //        SubjectActual = g.Sum(x => x.Money3.Text == "勻入" ? x.Money3.PayMoney : 0) + g.Sum(x => x.Money3.Text == "一般" ? x.Money3.PayMoney : 0)
            //    })
            //    .ToListAsync();

            //var results = _context.Money.ToList();
            //foreach (var item in results)
            //{
            //    if (!string.IsNullOrEmpty(item.Budget))
            //    {
            //        var a = _context.Money3.Where(x => x.Name == item.Budget);
            //        if (a!= null)
            //        {
            //            item.Money3DbModels = a.ToList();
            //        }
            //    }

            //}


            //var AC = A.ToList();
            //var B = A.Include(m => m.Money3DbModels);
            //var C = B.ToList();

            var A = _context.Money3
               .Where(money3 => money3.Group1 == "工務組"
               && money3.MoneyDbModel != null && money3.MoneyDbModel.Group == "工務組"
               && money3.Year == Year && money3.MoneyDbModel.Year == Year
               && (money3.Status == "O" || money3.Status == "C"))
               .Include(money3 => money3.MoneyDbModel)
               .ToQueryString();

            //var results = _context.Money3
            //    .Where(money3 => money3.Group1 == "工務組"
            //    && money3.MoneyDbModel != null && money3.MoneyDbModel.Group == "工務組"
            //    && money3.Year == Year && money3.MoneyDbModel.Year == Year
            //    && (money3.Status != null && money3.Status.Trim() == "O" || money3.Status != null && money3.Status.Trim() == "C"))
            //    .Include(money3 => money3.MoneyDbModel)
            //    //.ProjectTo<Money3DbModelDto>(_mapper.ConfigurationProvider)
            //    .ToList();

            var results = _context.Money3
    .Where(money3 => money3.Group1 == "工務組"
        && money3.MoneyDbModel != null && money3.MoneyDbModel.Group == "工務組"
        && money3.Year == Year && money3.MoneyDbModel.Year == Year
        && (money3.Status != null && (money3.Status.Trim() == "O" || money3.Status.Trim() == "C")))
    .Include(money3 => money3.MoneyDbModel)
    .Select(money3 => new {
        money3,
        money3.MoneyDbModel,
        Group1 = money3.Group1 != null ? money3.Group1.Trim() : "",
        Status = money3.Status != null ? money3.Status.Trim() : "" // 移除 Status 欄位的多餘空格
    })
    .AsEnumerable() // 轉為本地處理，避免 EF Core 的限制
    .Select(x => {
        x.money3.Group1 = x.Group1;
        x.money3.Status = x.Status; // 更新 Status 欄位值
        return x.money3;
    })
    .ToList();


            return Ok(results);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }



    //[HttpGet("ByBudget")]
    //public async Task<IActionResult> GetDataByBudget(string budget)
    //{
    //    try
    //    {
    //        var results = await _context.Money
    //            .Include(m => m.Money3DbModels)
    //            .Where(m => m.Budget == budget && m.Group == "工務組")
    //            .ToListAsync();

    //        return Ok(results);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, $"Internal server error: {ex}");
    //    }
    //}

    //[HttpGet("ExportToExcel")]
    //public async Task<IActionResult> ExportToExcel(string budget)
    //{
    //    try
    //    {
    //        var results = await _context.Money
    //            .Include(m => m.Money3DbModels)
    //            .Where(m => m.Budget == budget && m.Group == "工務組")
    //            .ToListAsync();

    //        using (var workbook = new XSSFWorkbook())
    //        {
    //            var sheet = workbook.CreateSheet("Budget Data");
    //            var headerRow = sheet.CreateRow(0);
    //            headerRow.CreateCell(0).SetCellValue("Budget");
    //            headerRow.CreateCell(1).SetCellValue("Subject6");
    //            headerRow.CreateCell(2).SetCellValue("Subject7");
    //            headerRow.CreateCell(3).SetCellValue("Subject8");
    //            headerRow.CreateCell(4).SetCellValue("PurchaseMoney");

    //            for (int i = 0; i < results.Count; i++)
    //            {
    //                var row = sheet.CreateRow(i + 1);
    //                row.CreateCell(0).SetCellValue(results[i].Budget);
    //                row.CreateCell(1).SetCellValue(results[i].Subject6);
    //                row.CreateCell(2).SetCellValue(results[i].Subject7);
    //                row.CreateCell(3).SetCellValue(results[i].Subject8);
    //                row.CreateCell(4).SetCellValue(results[i].Money3DbModels.Sum(m => m.PurchaseMoney));
    //            }

    //            using (var stream = new MemoryStream())
    //            {
    //                workbook.Write(stream);
    //                var content = stream.ToArray();
    //                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{budget}.xlsx");
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, $"Internal server error: {ex}");
    //    }
    //}




}
   


