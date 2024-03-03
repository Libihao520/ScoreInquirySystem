using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Studen;
using Model.Other;
using ScoreInquirySystem.Data;

namespace ScoreInquirySystem.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class GetDataController : ControllerBase
{
    private TestContext _testContext;
    private IStudentService _studentService;

    public GetDataController(TestContext testContext, IStudentService studentService)
    {
        _testContext = testContext;
        _studentService = studentService;
    }

    /// <summary>
    /// 根据班级名称性别查询
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<ApiResult> getStudentData([FromBody] StudenReq req)
    {
        return
            await _studentService.GetStudentConditionalQuery(req);
    }

    /// <summary>
    /// 根据姓名获取成绩
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<ApiResult> GetPerformance([FromQuery] String name)
    {
        return
            await _studentService.GetPerformanceInName(name);
    }

    /// <summary>
    /// 获取最近的一次考试成绩排名
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<ApiResult> GetScoreRanking()
    {
        return await _studentService.GetScoreRanking();
    }
    
}