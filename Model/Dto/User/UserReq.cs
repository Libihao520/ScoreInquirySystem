using System.ComponentModel.DataAnnotations;

namespace Model.Dto.User;

public class UserReq
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required]
    public string username { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// 验证码的缓存key
    /// </summary>
    [Required]
    public string AuthcodeKey { get; set; }

    /// <summary>
    /// 验证码
    /// </summary>
    [Required]
    public string Authcode { get; set; }
}