using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Consts;
using Model.Dto.GraphValidateCode;
using Model.Dto.User;
using Model.Other;
using ScoreInquirySystem.Data;
using Service.Utils;
using Service.Utils.RedisUtil;

namespace Service;

public class UserService : IUserService
{
    private TestContext _context;
    private readonly JWTTokenOptions _jwtTokenOptions;

    public UserService(TestContext context, IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
    {
        _context = context;
        _jwtTokenOptions = jwtTokenOptions.CurrentValue;
    }

    public async Task<ApiResult> GraphValidateCode()
    {
        // 生成随机验证码
        Random random = new Random();

        string captchaText = "";
        for (int i = 0; i < 4; i++)
        {
            captchaText += (char)random.Next('A', 'Z' + 1);
        }

        string base64Captcha = CaptchaGenerator.GenerateCaptcha(150, 50, captchaText);

        Guid newGuid = Guid.NewGuid();
        var dateTime = DateTime.Now.AddMinutes(3);

        //写入缓存
        CacheManager.Set(string.Format(RedisKey.GraphValidateCode, newGuid), captchaText, TimeSpan.FromMinutes(3));

        var gvcRes = new GvcRes()
        {
            base64 = "data:image/png;base64," + base64Captcha,
            baseKey = newGuid.ToString(),
            failureTime = dateTime
        };

        return ResultHelper.Success("请求成功！图形验证码有效期三分钟", gvcRes);
    }

    public async Task<ApiResult> GetToken(UserReq userReq)
    {
        if (string.IsNullOrEmpty(userReq.username) || string.IsNullOrEmpty(userReq.Password))
        {
            return ResultHelper.Error("参数不能为空");
        }
        
        var s = CacheManager.Get<string>(string.Format(RedisKey.GraphValidateCode, userReq.AuthcodeKey));
        if (string.IsNullOrEmpty(s))
        {
            return ResultHelper.Error("验证码已失效!");
        }

        if (s != userReq.Authcode)

        {
            return ResultHelper.Error("验证码错误!");
        }
        
        var users = _context.Users.Where(u => u.Name == userReq.username && u.Password == userReq.Password).FirstOrDefault();

        if (users == null )
        {
            return ResultHelper.Error("账号不存在，用户名或密码错误！");
        }
        #region 
        var claims = new[]
        {
            new Claim("Id",users.Id.ToString()),
            new Claim("Name",users.Name),
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenOptions.SecurityKey));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _jwtTokenOptions.Issuer,
            audience: _jwtTokenOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),//Token10分钟有效期
            signingCredentials: creds);

        string returnToken = new JwtSecurityTokenHandler().WriteToken(token);
        return ResultHelper.Success("身份验证成功，返回token","token: Bearer "+returnToken);
        #endregion
        
    }
}