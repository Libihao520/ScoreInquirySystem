using Model.Dto.Studen;
using Model.Other;

namespace Interface;

public interface IStudentService
{
  public Task<ApiResult>  GetStudentConditionalQuery(StudenReq studenReq);

  public Task<ApiResult> GetPerformanceInName(string name);

  public Task<ApiResult> GetScoreRanking();
}