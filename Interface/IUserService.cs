using Model.Dto.User;
using Model.Other;

namespace Interface;

public interface IUserService
{
    public Task<ApiResult> GraphValidateCode();

    public Task<ApiResult> GetToken( UserReq userReq);
}