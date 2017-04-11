using AutoMapper;
using NtAbcExam.Domain.Models;
using NtAbcExam.Web.Areas.Admin.Models;
using NtAbcExam.Web.Models;

namespace NtAbcExam.Web.DtoMapper.MapProfile
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<exam_subject, SubjectViewModel>()
             ;

            CreateMap<SubjectViewModel, exam_subject>()
            ;

        }
    }
}
