using AutoMapper;
using NtAbcExam.Domain.Models;
using NtAbcExam.Web.Areas.Admin.Models;
using NtAbcExam.Web.Models;

namespace NtAbcExam.Web.DtoMapper.MapProfile
{
    public class ExamQuestionProfile : Profile
    {
        public ExamQuestionProfile()
        {
            CreateMap<exam_database, ExamQuestionViewModel>()
                ;
        }
    }
}
