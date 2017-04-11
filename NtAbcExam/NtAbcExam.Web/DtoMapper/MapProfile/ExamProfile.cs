using AutoMapper;
using NtAbcExam.Domain.Models;
using NtAbcExam.Web.Areas.Admin.Models;
using NtAbcExam.Web.Models;

namespace NtAbcExam.Web.DtoMapper.MapProfile
{
    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            CreateMap<exam_test, ExamViewModel>();

            CreateMap<exam_test, ExamTestViewModel>();

            CreateMap<ExamTestViewModel, exam_test>();

            CreateMap<exam_score, ExamScoreViewModel>();
        }
    }
}
