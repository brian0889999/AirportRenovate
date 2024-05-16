using Microsoft.AspNetCore.Mvc;
using AirportRenovate.Server.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using AirportRenovate.Server.Datas;

namespace AirportRenovate.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly AirportBudgetDbContext _context;
    public LoginController(AirportBudgetDbContext context)
    {
        _context = context;
    }


    //[HttpPost]
    //public IActionResult LoginTest([FromBody] LoginViewModel loginData)
    //{  /* var isAuthenticated = */

    //    if (!string.IsNullOrEmpty(loginData.Account) && !string.IsNullOrEmpty(loginData.Password))
    //    {
            
    //        return Ok("ok"); // 回傳成功的訊息及用戶相關資訊
    //    }
    //    else
    //    {
    //        return Unauthorized("登入失敗"); // 登入失敗，回傳未授權的訊息
    //    }
    //}

    [HttpPost]

    public IActionResult Login([FromBody] LoginViewModel loginData) 
    {
    if(!string.IsNullOrEmpty(loginData.Account) && !string.IsNullOrEmpty(loginData.Password))
        {
            var user = _context.user_data1.FirstOrDefault(x => x.Account == loginData.Account);
            return Ok(user);
        }
    else
        {
            return Unauthorized("登入失敗"); // 回傳登入失敗
        }
    }
}
