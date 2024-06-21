using Microsoft.AspNetCore.Mvc;
using AirportRenovate.Server.Datas;
using AirportRenovate.Server.Utilities;
using AirportRenovate.Server.ViewModels;
using AirportRenovate.Server.Models;
using AirportRenovate.Server.Interfaces.Repositorys;

namespace AirportRenovate.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IGenericRepository<Users> _users;
    private readonly DESEncryptionUtility _dESEncryptionUtility;
    public LoginController(IGenericRepository<Users> users, DESEncryptionUtility dESEncryptionUtility)
    {
        _users = users;
        _dESEncryptionUtility = dESEncryptionUtility;
    }

    [HttpPost]
    public IActionResult LoginTest([FromBody] LoginViewModel loginData)
    {  /* var isAuthenticated = */

        if (!string.IsNullOrEmpty(loginData.Account) && !string.IsNullOrEmpty(loginData.Password))
        {

            return Ok("ok"); // 回傳成功的訊息及用戶相關資訊
        }
        else
        {
            return Unauthorized("登入失敗"); // 登入失敗，回傳未授權的訊息
        }
    }
    [HttpGet]

    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _users.GetAllAsync();
            foreach (var user in users)
            {
                if (user.Password != null)
                {
                    user.Password = _dESEncryptionUtility.DecryptDES(user.Password);
                }
            }
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

    //[HttpPost]

    //public IActionResult Login([FromBody] LoginViewModel loginData)
    //{
    //    if (!string.IsNullOrEmpty(loginData.Account) && !string.IsNullOrEmpty(loginData.Password))
    //    {
    //        var password = _dESEncryptionUtility.EncryptDES(loginData.Password);
    //        var user = _users.GetByCondition(x => x.Account == loginData.Account).FirstOrDefault();
    //        if (user != null && user.Password == password)
    //        {
    //            return Ok(user);
    //        }
    //        else
    //        {

    //            return BadRequest();
    //        }

    //    }
    //    else
    //    {
    //        return Unauthorized("登入失敗"); // 回傳登入失敗
    //    }
    //}
}
