using Microsoft.AspNetCore.Mvc;
using AirportRenovate.Server.Models;
using AirportRenovate.Server.Datas;
using AirportRenovate.Server.Utilities;
using System.Net.Sockets;
using System.Linq.Expressions;

namespace AirportRenovate.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrivilegeController : ControllerBase
{
    private readonly AirportBudgetDbContext _context;
    public PrivilegeController(AirportBudgetDbContext context)
    {
        _context = context;
    }


    [HttpGet]    
    public ActionResult<IEnumerable<UserModelDb>> FetchUsers()
    {
        var usersData = _context.User_data1;
        var users = new List<UserModelDb>();
        foreach (var userData in usersData)
        {
            var user = new UserModelDb
            {
                No = userData.No,
                Name = userData.Name?.Trim(),
                Account = userData.Account?.Trim(),
                Password = string.IsNullOrEmpty(userData?.Password) ? null : DESEncryptionUtility.DecryptDES(userData.Password.Trim()),
                Auth = userData?.Auth?.Trim(),
                Status1 = userData?.Status1?.Trim(),
                Status2 = userData?.Status2?.Trim(),
                Status3 = userData?.Status3?.Trim()
            };
            users.Add(user);
        }
        return Ok(users);
    }

    [HttpPost]
    public IActionResult AddUser([FromBody] UserModelDb currentItem)
    {
        if(currentItem == null)
        {
            return BadRequest("沒有user資料");
        }
        else
        {
            // 創建新的 LoginModelDb 物件
            var newUser = new LoginModelDb
            {
                //No = currentItem.No,
                Name = currentItem.Name,
                Account = currentItem.Account,
                Password = string.IsNullOrEmpty(currentItem?.Password) ? null : DESEncryptionUtility.EncryptDES(currentItem.Password),
                Email = "",
                Auth = currentItem?.Auth,
                Reason = "",
                Status1 = currentItem?.Status1,
                Status2 = "O", // 預設值
                Memo = "",
                Status3 = currentItem?.Status3?.Trim(),
                Account_Open = "y", // 預設值
                Time = new DateTime(1990, 1, 1, 0, 0, 0), // 預設值
                Time1 = DateTime.Now, // 當下時間
                Count = 0, // 預設值
                Unit_No = "M" // 預設值
            };

            // 添加到資料庫
            _context.User_data1.Add(newUser);
            _context.SaveChanges();

            return Ok(newUser);
        }
    }


    [HttpPut]

    public IActionResult UpdateUser([FromBody] UserModelDb currentItem)
    {
        if (currentItem == null)
        {
            return BadRequest("沒有user資料");
        } 
        else
        {
            // 根據No欄位去找資料
            var existingUser = _context.User_data1.FirstOrDefault(user => user.No == currentItem.No);
            if (existingUser == null)
            {
                return NotFound("沒有找到user");
            }
            existingUser.Name = currentItem.Name;
            existingUser.Account = currentItem.Account;
            existingUser.Password = string.IsNullOrEmpty(currentItem?.Password) ? null : DESEncryptionUtility.EncryptDES(currentItem.Password);
            existingUser.Auth = currentItem?.Auth;
            existingUser.Status1 = currentItem?.Status1?.Trim();
            existingUser.Status2 = currentItem?.Status2?.Trim();
            existingUser.Status3 = currentItem?.Status3?.Trim();
            _context.SaveChanges();

            return Ok(existingUser);
        }
        

        
    }
}
