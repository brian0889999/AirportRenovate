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
        var usersData = _context.user_data1;
        var users = new List<UserModelDb>();
        foreach (var userData in usersData)
        {
            var user = new UserModelDb
            {
                No = userData.No,
                Name = userData.Name?.Trim(),
                Account = userData.Account?.Trim(),
                Password = DESEncryptionUtility.DecryptDES(userData.Password?.Trim()),
                Status1 = userData.Status1?.Trim(),
                Status2 = userData.Status2?.Trim(),
                Status3 = userData.Status3?.Trim()
            };
            users.Add(user);
        }
        return Ok(users);
    }


    [HttpPut]

    public IActionResult UpdateUser([FromBody] UserModelDb currentItem)
    {
        if (currentItem == null)
        {
            return BadRequest("沒有user資料");
        }

        // 根據No欄位去找資料
        var existingUser = _context.user_data1.FirstOrDefault(user => user.No == currentItem.No);
        if (existingUser == null)
        {
            return NotFound("沒有找到user");
        }

        existingUser.Name = currentItem.Name;
        existingUser.Account = currentItem.Account;
        existingUser.Password = DESEncryptionUtility.EncryptDES(currentItem?.Password);
        existingUser.Status1 = currentItem.Status1?.Trim();
        existingUser.Status2 = currentItem.Status2?.Trim();
        existingUser.Status3 = currentItem. Status3?.Trim();
        _context.SaveChanges();

        return Ok(existingUser);
    }
}
