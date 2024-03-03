using AutoMapper;
using Model.Dto.Scores;
using Model.Dto.Studen;
using Model.Entities;

namespace ScoreInquirySystem.Config;

public class AutoMapperConfigs : Profile
{
    public AutoMapperConfigs()
    {
        CreateMap<Student, StudenRes>();
        CreateMap<Score,ScoresRes>();
    }
}