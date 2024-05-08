using Microsoft.AspNetCore.Mvc;
using AirportRenovate.Server.Models;

namespace AirportRenovate.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginViewModel loginData)
    {  /* var isAuthenticated = */

        if (1==1)
        {
            
            return Ok("ok"); // 回傳成功的訊息及用戶相關資訊
        }
        else
        {
            return Unauthorized(); // 登入失敗，回傳未授權的訊息
        }
    }
}
