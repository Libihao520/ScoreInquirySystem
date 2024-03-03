using System.ComponentModel.DataAnnotations;

namespace Model.Dto.GraphValidateCode;

public class GvcRes
{
    /// <summary>
    /// base64的图像验证码
    /// </summary>
    [Required]
    public string base64 { get; set; }

    /// <summary>
    /// 存储到Redis的key，登录请求时需要携带
    /// </summary>
    [Required]
    public string baseKey { get; set; }

    /// <summary>
    /// 失效时间
    /// </summary>
    [Required]
    public DateTime failureTime { get; set; }
}