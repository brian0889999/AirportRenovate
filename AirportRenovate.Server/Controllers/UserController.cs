using Microsoft.AspNetCore.Mvc;
using AirportRenovate.Server.Datas;
using AirportRenovate.Server.Utilities;
using AirportRenovate.Server.ViewModels;
using AirportRenovate.Server.Models;
using AirportRenovate.Server.Interfaces.Repositorys;
using Microsoft.AspNetCore.Authorization;
using AirportRenovate.Server.DTOs;
using AirportRenovate.Server.Services;

namespace AirportRenovate.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{   
    private readonly TokenService _tokenService;
    private readonly IGenericRepository<User> _userRepository;
    private readonly DESEncryptionUtility _dESEncryptionUtility;
    public UserController(TokenService tokenService, IGenericRepository<User> userRepository, DESEncryptionUtility dESEncryptionUtility)
    {   
        _tokenService = tokenService;
        _userRepository = userRepository;
        _dESEncryptionUtility = dESEncryptionUtility;
    }

    [HttpGet("Current")]
    public IActionResult GetCurrent()
    {
        try
        {
            UserDTO userDTO = _tokenService.GetUser(User);

            var userInfo =  _userRepository.GetByCondition(item => item.No == userDTO.UserId).FirstOrDefault();

                //if (user.Password != null)
                //{
                //    user.Password = _dESEncryptionUtility.DecryptDES(user.Password);
                //}

            return Ok(userInfo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

   
}
