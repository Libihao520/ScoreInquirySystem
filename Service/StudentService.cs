using AutoMapper;
using Dapper;
using Interface;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Dto;
using Model.Dto.Scores;
using Model.Dto.Studen;
using Model.Other;
using ScoreInquirySystem.Data;

namespace Service;

public class StudentService : IStudentService
{
    private TestContext _context;
    private readonly IMapper _mapper;
    private readonly DapperContext _dapperContext;


    public StudentService(TestContext context, IMapper mapper, DapperContext dapperContext)
    {
        _context = context;
        _mapper = mapper;
        _dapperContext = dapperContext;
    }


    public async Task<ApiResult> GetStudentConditionalQuery(StudenReq studenReq)
    {
        var query = _context.Students.AsNoTracking().ToList();


        if (!string.IsNullOrEmpty(studenReq.Class))
        {
            var Class = _context.Classinfos.AsNoTracking().Where(c => c.Name == studenReq.Class).FirstOrDefault();
            if (Class != null)
            {
                var studentclasses = _context.Studentclasses.AsNoTracking().Where(p => p.Classid == Class.Id).ToList();
                query = (from t1 in query
                    join t2 in studentclasses on t1.Id equals t2.Studentid
                    select t1).ToList();
            }
            else
            {
                return ResultHelper.Success("没有数据", new PageDto<StudenRes>());
            }
        }

        if (!string.IsNullOrEmpty(studenReq.Name))
        {
            query = query.Where(p => p.Name.Contains(studenReq.Name)).ToList();

            //或者
            // query = query.Where(p => EF.Functions.Like(p.Name, $"%{studenReq.Name}%")).ToList();  
        }

        if (studenReq.Sex != null)
        {
            query = query.Where(p => p.Sex == studenReq.Sex).ToList();
        }

        query = query.Skip((studenReq.PageIndex - 1) * studenReq.PageSize)
            .Take(studenReq.PageSize).ToList();

        var studenReqs = _mapper.Map<List<StudenRes>>(query);

        var req = new StudenReq();

        PageDto<StudenRes> result = new()
        {
            Total = query.Count(),
            Data = studenReqs
        };

        return ResultHelper.Success("成功", result);
    }

    public async Task<ApiResult> GetPerformanceInName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return ResultHelper.Error("名称不能为空！");
        }

        var Student = _context.Students.AsNoTracking().Where(p => p.Name == name).FirstOrDefault();
        if (Student == null)
        {
            return ResultHelper.Error("该学生不存在！");
        }

        var examinations = _context.Examinations.AsNoTracking().ToList();
        var scores = _context.Scores.AsNoTracking().Where(p => p.Studentid == Student.Id).ToList();

        var scoresResList = _mapper.Map<List<ScoresRes>>(scores);
        foreach (var scoresRes in scoresResList)
        {
            scoresRes.name = Student.Name;
            scoresRes.SubjectName = scoresRes.Subject switch  
            {  
                0 => "语文",  
                1 => "数学",  
                2 => "英语",  
                _ => "未知科目" 
            };  
            var exam = examinations.FirstOrDefault(e => e.Id == scoresRes.Examinationid);  
            if (exam != null)  
            {  
                scoresRes.Examination = exam.Title;  
            }  
        }
        

        return ResultHelper.Success("成绩！", scoresResList);
    }



    public async Task<ApiResult> GetScoreRanking()
    {
        using (var connection = _dapperContext.DbConnection)
        {
            var enumerable = connection.Query<dynamic>(@"
WITH 
q as( SELECT SCORES.id,
SCORES.VALUE as 分数,
SCORES.SUBJECT as 科目,
SCORES.STUDENTID as 学生id,
STUDENTCLASS.CLASSID as 班级id 
FROM SCORES  
LEFT JOIN STUDENTCLASS ON  SCORES.STUDENTID =  STUDENTCLASS.STUDENTID WHERE EXAMINATIONID = (SELECT ID from EXAMINATION ORDER BY CREATEDATETIME desc LIMIT 1)),

q2 as ( SELECT CLASSINFO.GRADE as 年级,CLASSINFO.name as 班级名称,q.科目,q.分数,q.学生id,STUDENT.NAME as 姓名 from q 
LEFT JOIN  CLASSINFO ON q.班级id = CLASSINFO.id 
LEFT JOIN STUDENT ON q.学生id = STUDENT.id
WHERE  STATUS != 3 AND STATUS != 4),

q3 as (SELECT q2.班级名称,q2.学生id,q2.姓名,
       sum(CASE WHEN q2.科目 = 0 THEN q2.分数 ELSE 0 END) AS 语文,
       sum(CASE WHEN q2.科目 = 1 THEN q2.分数 ELSE 0 END) AS 数学,
       sum(CASE WHEN q2.科目 = 2 THEN q2.分数 ELSE 0 END) AS 英语
FROM q2
GROUP BY 1,2,3
),

ranked_data AS (
  SELECT *, ROW_NUMBER() OVER (PARTITION BY 班级名称 ORDER BY 总分 DESC) as cou
  FROM (
    SELECT q3.*, 语文 + 数学 + 英语 as 总分
    FROM q3
  ) subquery
)
SELECT *
FROM ranked_data
WHERE cou <= 5
ORDER BY 班级名称, cou;
");
        return ResultHelper.Success("最近一次考试的成绩！",enumerable);
        }

    }
    
}