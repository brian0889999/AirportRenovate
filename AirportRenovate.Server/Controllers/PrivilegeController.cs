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
    public ActionResult<IEnumerable<UsersModelDb>> FetchUsers()
    {
        var usersData = _context.user_data1;
        var users = new List<UsersModelDb>();
        foreach (var userData in usersData)
        {
            var user = new UsersModelDb
            {
                No = userData.No,
                Name = userData.Name?.Trim(),
                Account = userData.Account?.Trim(),
                Password = userData.Password?.Trim(),
                Status1 = userData.Status1?.Trim(),
                Status2 = userData.Status2?.Trim(),
                Status3 = userData.Status3?.Trim()
            };
            users.Add(user);
        }
        return Ok(users);
    }
}
