using NtAbcExam.Web.DtoMapper.MapProfile;

namespace NtAbcExam.Web.DtoMapper
{
    public class AutoMapperProfile
    {
        public static void InitAllProfile()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ExamProfile>();
                cfg.AddProfile<QuestionProfile>();
                cfg.AddProfile<DeptProfile>();
                cfg.AddProfile<SubjectProfile>();
                cfg.AddProfile<AdminUserProfile>();
                cfg.AddProfile<CadreInfoProfile>();
                cfg.AddProfile<ExamQuestionProfile>();
            });
        }
      
    }
}
