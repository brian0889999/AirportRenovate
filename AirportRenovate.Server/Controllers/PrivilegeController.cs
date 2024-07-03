using Microsoft.AspNetCore.Mvc;
using AirportRenovate.Server.Interfaces.Repositorys;
using AirportRenovate.Server.Models;
using AirportRenovate.Server.Datas;
using AirportRenovate.Server.Utilities;
using AutoMapper;
using System.Net.Sockets;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using AirportRenovate.Server.ViewModels;

namespace AirportRenovate.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PrivilegeController : ControllerBase
{
    private readonly IGenericRepository<User> _users;
    private readonly IMapper _mapper;
    private readonly DESEncryptionUtility _dESEncryptionUtility;
    public PrivilegeController(IGenericRepository<User> users, IMapper mapper, DESEncryptionUtility dESEncryptionUtility)
    {
        _users = users;
        _mapper = mapper;
        _dESEncryptionUtility = dESEncryptionUtility;
    }

    /// <summary>
    /// 取得 使用者
    /// </summary>
    /// <returns>查詢結果</returns>
    [HttpGet]
    public ActionResult<IEnumerable<UserDataViewModel>> FetchUsers()
    {
        var usersData = _users.GetAll().ToList();
        List<UserDataViewModel> users = _mapper.Map<List<UserDataViewModel>>(usersData);

        foreach (var user in users)
        {
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = _dESEncryptionUtility.DecryptDES(user.Password.Trim());
            }
        }

        return Ok(users);
    }

    /// <summary>
    /// 新增 使用者
    /// </summary>
    /// <returns>新增結果</returns>
    [HttpPost]
    public IActionResult AddUser([FromBody] UserDataViewModel request)
    {
        try
        {
            if (request == null)
            {
                return BadRequest("沒有user資料");
            }
            else
            {
                if (!string.IsNullOrEmpty(request.Password))
                {
                    request.Password = _dESEncryptionUtility.EncryptDES(request.Password.Trim());
                }

                User newUser = _mapper.Map<User>(request);

                _users.Add(newUser);
                return Ok(newUser);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 更新 使用者
    /// </summary>
    /// <returns>更新結果</returns>
    [HttpPut]

    public IActionResult UpdateUser([FromBody] UserDataViewModel currentItem)
    {
        try
        {
            if (currentItem == null)
            {
                return BadRequest("沒有user資料");
            }
            else
            {
                // 根據No欄位去找資料
                var existingUser = _users.GetByCondition(user => user.No == currentItem.No).FirstOrDefault();

                if (existingUser == null)
                {
                    return NotFound("沒有找到user");
                }

                if (!string.IsNullOrEmpty(currentItem.Password))
                {
                    currentItem.Password = _dESEncryptionUtility.EncryptDES(currentItem.Password.Trim());
                }

                _mapper.Map(currentItem, existingUser);
                _users.Update(existingUser);

                return Ok(existingUser);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
