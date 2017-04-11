using AutoMapper;
using NtAbcExam.Domain.Models;
using NtAbcExam.Web.Areas.Admin.Models;
using NtAbcExam.Web.Models;

namespace NtAbcExam.Web.DtoMapper.MapProfile
{
    public class CadreInfoProfile : Profile
    {
        public CadreInfoProfile()
        {
            CreateMap<cadre_info, EmpViewModel>()
               ;

            CreateMap<EmpViewModel, cadre_info>()
               ;
        }
    }
}
