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
using AutoMapper;
using NPOI.SS.Formula.Functions;
using AirportRenovate.Server.Mappings;
using AutoMapper.QueryableExtensions;
using AirportRenovate.Server.DTOs;
using AirportRenovate.Server.ViewModels;
using System.Text.RegularExpressions;
using MyGisMIS.Server.Repositorys;



namespace AirportRenovate.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeletedRecordsController(IGenericRepository<Money3DbModel> money3, IMapper mapper) : ControllerBase
{
    private readonly IGenericRepository<Money3DbModel> _money3 = money3;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public IActionResult SearchDeletedRecords(int Year)
    {   
        var deletedRecords = _money3.GetByCondition(m3 => m3.Year == Year && m3.Status == "X")
            .ToList();

        var cleanedRecords = _mapper.Map<List<DeletedRecordsDto>>(deletedRecords);

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

    [HttpPut("ByRestoreData")]
    public IActionResult RestoreData([FromBody] SoftDeleteViewModel request)
    {
        if (request == null || request.ID1 <= 0)
        {
            return BadRequest("Invalid request.");
        }

        //var entity =  _context.Money3.Find(request.ID1); // 因為find方法是透過主鍵去搜尋,ID1不是主鍵,找不到資料
        var entity = _money3.GetByCondition(e => e.ID1 == request.ID1).FirstOrDefault();

        if (entity == null)
        {
            return NotFound("Record not found.");
        }

        entity.Status = "O";
        _money3.Update(entity);

        return Ok("Status updated to 'O'.");
    }
}
   


