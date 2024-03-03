namespace Model;

/// <summary>
/// 分页对象入参
/// </summary>
public class PageInput
{
    /// <summary>
    /// 第几页
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// 每页条数
    /// </summary>
    public int PageSize { get; set; } = 10;
}