using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.User;
using Model.Other;

namespace ScoreInquirySystem.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class LoginController : ControllerBase
{
    private IUserService _userService;

    public LoginController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// 获取图形验证码
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ApiResult> GraphValidateCode()
    {
        return await _userService.GraphValidateCode();
    }

    /// <summary>
    /// 登录，获取token
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<ApiResult> GetToken(UserReq userReq)
    {
        return await _userService.GetToken(userReq);
    }
}