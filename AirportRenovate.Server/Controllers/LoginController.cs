using Microsoft.AspNetCore.Mvc;
using AirportRenovate.Server.Models;
using AirportRenovate.Server.Datas;
using AirportRenovate.Server.Utilities;

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
    [HttpGet]

    public IActionResult GetUsers()
    {
        var users = _context.user_data1;
        foreach (var user in users)
        {
            if (user.Password != null)
            {
                user.Password = DESEncryptionUtility.DecryptDES(user.Password);
            }
        }
        return Ok(users);
    }

    [HttpPost]

    public IActionResult Login([FromBody] LoginViewModel loginData)
    {
        //if (!string.IsNullOrEmpty(loginData.Account) && !string.IsNullOrEmpty(loginData.Password))
        //{
        //    var user = _context.user_data1.FirstOrDefault(x => x.Account == loginData.Account);
        //    var password = DESEncryptionUtility.DecryptDES(user.Password);
        //    if (password == loginData.Password)
        //    {
        //        return Ok(user);
        //    }
        //    else
        //    {

        //        return BadRequest();
        //    }

        //}
        if (!string.IsNullOrEmpty(loginData.Account) && !string.IsNullOrEmpty(loginData.Password))
        {
            var password = DESEncryptionUtility.EncryptDES(loginData.Password);
            var user = _context.user_data1.FirstOrDefault(x => x.Account == loginData.Account);
            if (user != null && user.Password == password)
            {
                return Ok(user);
            }
            else
            {

                return BadRequest();
            }

        }
        else
        {
            return Unauthorized("登入失敗"); // 回傳登入失敗
        }
    }
}
