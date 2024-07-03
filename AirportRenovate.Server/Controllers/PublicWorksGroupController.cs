using Microsoft.AspNetCore.Mvc;
using AirportRenovate.Server.Interfaces.Repositorys;
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
using AirportRenovate.Server.ViewModels;
using System.Text.RegularExpressions;
using Money = AirportRenovate.Server.Models.Money;
using MathNet.Numerics.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;



namespace AirportRenovate.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PublicWorksGroupController(IGenericRepository<Money3> money3Repository, IGenericRepository<Money> moneyRepository,  IMapper mapper) : ControllerBase
{
    private readonly IGenericRepository<Money3> _money3Repository = money3Repository;
    private readonly IGenericRepository<Money> _moneyRepository = moneyRepository;
    private readonly IMapper _mapper = mapper;


    /// <summary>
    /// Groups的預算資料查詢(包含所有欄位)
    /// </summary>
    /// <returns>查詢結果</returns>
    //[HttpGet("ByYear")]
    //public IActionResult GetData(int Year, string Group) // 前端傳Year值,後端回傳符合Year值的工務組資料
    //{
    //    try
    //    {
    //        var results = _money3Repository.GetByCondition(money3 => money3.Group1 == Group
    //                && money3.Money != null && money3.Money.Group == Group
    //                && money3.Year == Year && money3.Money.Year == Year
    //                && (money3.Status != null && (money3.Status.Trim() == "O" || money3.Status.Trim() == "C")))
    //                .Include(money3 => money3.Money)
    //                .AsEnumerable() // 轉為本地處理，避免 EF Core 的限制
    //                .Select(money3 =>
    //                {
    //                    // 移除需要的欄位中的多餘空格
    //                    money3.Group1 = money3.Group1?.Trim() ?? "";
    //                    money3.Status = money3.Status?.Trim() ?? "";
    //                    money3.All = money3.All?.Trim() ?? "";
    //                    money3.True = money3.True?.Trim() ?? "";
    //                    return money3;
    //                })
    //                .ToList();

    //        return Ok(results);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, $"Internal server error: {ex}");
    //    }
    //}

    /// <summary>
    /// Groups的預算資料查詢
    /// </summary>
    /// <returns>查詢結果</returns>
    [HttpGet]
    public IActionResult GetGroupData(int Year, string Group) // 前端傳Year值,後端回傳符合Year值的工務組資料
    {
        Expression<Func<Money3, bool>> condition = item => true;
        condition = money3 => money3.Group1 == Group;
        try
        {
            var results = _money3Repository.GetByCondition(money3 => money3.Group1 == Group
    && money3.Money != null && money3.Money.Group == Group
    && money3.Year == Year && money3.Money.Year == Year
    && (money3.Status != null && (money3.Status.Trim() == "O" || money3.Status.Trim() == "C")))
    .Include(money3 => money3.Money)
    .AsEnumerable()
    .Select(money3 =>
    {
        money3.Group1 = money3.Group1?.Trim() ?? "";
        money3.Status = money3.Status?.Trim() ?? "";
        money3.All = money3.All?.Trim() ?? "";
        money3.True = money3.True?.Trim() ?? "";
        return new
        {
            money3.Money?.Subject6,
            money3.Money?.Subject7,
            money3.Money?.Subject8,
            money3.Money?.BudgetYear,
            money3.Money?.Final,
            money3.Money?.Budget,
            money3.Money?.Group,
            money3.Money?.Year,
            money3.Text,
            money3.PurchaseMoney,
            money3.PayMoney
        };
    })
    .ToList();


            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }


    /// <summary>
    /// Groups的預算詳細資料查詢
    /// </summary>
    /// <returns>查詢結果</returns>
    [HttpGet("SelectedDetail")]
    public async Task<IActionResult> GetDetailData(string Budget, int Year, string Group, string? Note = null, int? PurchaseMoney = null) // 前端傳Year值,後端回傳符合Year值的工務組資料
    {
        try
        {
            var query = _money3Repository.GetByCondition(money3 =>
           money3.Money!.Budget == Budget &&
           money3.Group1 == Group &&
           money3.Money.Group == Group &&
           money3.Year == Year &&
           money3.Money.Year == Year &&
           money3.Status == "O"
       );

            if (!string.IsNullOrEmpty(Note))
            {
                query = query.Where(money3 => money3.Note != null && money3.Note.Contains(Note));
            }

            if (PurchaseMoney.HasValue)
            {
                query = query.Where(money3 => money3.PurchaseMoney == PurchaseMoney.Value);
            }

            var results = await query.Include(money3 => money3.Money).ToListAsync();

            return Ok(results);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    //[HttpGet("ExportToExcel")]
    //public async Task<IActionResult> ExportToExcel(string budget)
    //{
    //    try
    //    {
    //        var results = await _context.Money
    //            .Include(m => m.Money3s)
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
    //                row.CreateCell(4).SetCellValue(results[i].Money3s.Sum(m => m.PurchaseMoney));
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

    /// <summary>
    /// 新增資料
    /// </summary>
    /// <returns>新增結果</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SoftDeleteViewModel request)
    {
        try
        {   
            // 取得資料庫中ID1欄位的最大值並遞增
            int maxID1 = await _money3Repository.GetAll().MaxAsync(m => (int?)m.ID1) ?? 0;
            request.ID1 = maxID1 + 1;

            // 取得當年民國年分
            //var currentYear = DateTime.Now.Year - 1911;
            //request.Year = currentYear;
            // 使用 AutoMapper 將 ViewModel 映射到 Model
            Money3 money3 = _mapper.Map<Money3>(request);
            // 檢查 People 和 People1 欄位，若為 null 則存空值
            money3.People = request.People == "無" ? string.Empty : request.People;
            money3.People1 = request.People1 == "無" ? string.Empty : request.People1;
            _money3Repository.Add(money3);

            return Ok("Record added successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    /// <summary>
    /// 更新細項
    /// </summary>
    /// <returns>更新結果</returns>
    [HttpPut]
    public IActionResult DoUpdate([FromBody] SoftDeleteViewModel request)
    {
        try
        {
            //request.Money = null;
            var money3 = _money3Repository.GetByCondition(m => m.ID1 == request.ID1).AsNoTracking().FirstOrDefault(); // 這邊不能用find(ID1不是PK)
            if (money3 == null)
            {
                return NotFound("Record not found");
            }
            // 將 ViewModel 映射到實體模型
            Money3 updatedMoney3 = _mapper.Map<SoftDeleteViewModel, Money3>(request, money3);
            // 檢查 People 和 People1 欄位，若為 null 則存空值
            money3.People = request.People == "無" ? string.Empty : request.People;
            money3.People1 = request.People1 == "無" ? string.Empty : request.People1;
            _money3Repository.Update(updatedMoney3);
            return Ok("Record updated successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    /// <summary>
    /// 軟刪除
    /// </summary>
    /// <returns>刪除結果</returns>
    [HttpPut("SoftDelete")]
    public IActionResult DoSoftDelete([FromBody] SoftDeleteViewModel request)
    {
        try
        {
            var money3 = _money3Repository.GetByCondition(m => m.ID1 == request.ID1).FirstOrDefault();
            if (money3 == null)
            {
                return NotFound("not exist");
            }
            // 更新Status欄位
            money3.Status = "X";
            _money3Repository.Update(money3);
            return Ok("ok");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    /// <summary>
    /// 軟刪除
    /// </summary>
    /// <returns>刪除結果</returns>
    [HttpGet("ByDeletedRecords")]
    public IActionResult SearchDeletedRecords(int Year, string? Note)
    {
        try
        {
            var deletedRecords = _money3Repository.GetByCondition(m3 => m3.Year == Year && m3.Status == "X").ToList();

            if (!string.IsNullOrEmpty(Note))
            {
                deletedRecords = deletedRecords.Where(m3 => m3.Note != null && m3.Note.Contains(Note)).ToList();
            }


            var cleanedRecords = _mapper.Map<List<DeletedRecordsViewModel>>(deletedRecords);

            //var cleanedRecords = deletedRecords.Select(record => new
            //{
            //    record.ID,
            //    Purchasedate = record.Purchasedate,
            //    Text = record.Text?.Trim(),
            //    Note = record.Note?.Trim(),
            //    PurchaseMoney = record.PurchaseMoney,
            //    PayDate = record.PayDate,
            //    PayMoney = record.PayMoney,
            //    People = record.People?.Trim(),
            //    Name = record.Name?.Trim(),
            //    Remarks = record.Remarks?.Trim(),
            //    People1 = record.People1?.Trim(),
            //    ID1 = record.ID1,
            //    Status = record.Status?.Trim(),
            //    Group1 = record.Group1?.Trim(),
            //    All = record.All?.Trim(),
            //    True = record.True?.Trim(),
            //    Year = record.Year,
            //    Year1 = record.Year1?.Trim()
            //}).ToList();
            return Ok(cleanedRecords);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 軟刪除
    /// </summary>
    /// <returns>刪除結果</returns>
    [HttpPut("ByRestoreData")]
    public IActionResult RestoreData([FromBody] SoftDeleteViewModel request)
    {
        try
        {
            if (request == null || request.ID1 <= 0)
            {
                return BadRequest("Invalid request.");
            }

            //var entity =  _context.Money3.Find(request.ID1); // 因為find方法是透過主鍵去搜尋,ID1不是主鍵,找不到資料
            var entity = _money3Repository.GetByCondition(e => e.ID1 == request.ID1).FirstOrDefault();

            if (entity == null)
            {
                return NotFound("Record not found.");
            }

            entity.Status = "O";
            _money3Repository.Update(entity);

            return Ok("Status updated to 'O'.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
   


