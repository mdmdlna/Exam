using AutoMapper;
using NtAbcExam.Domain.Models;
using NtAbcExam.Web.Areas.Admin.Models;
using NtAbcExam.Web.Models;

namespace NtAbcExam.Web.DtoMapper.MapProfile
{
    public class DeptProfile : Profile
    {
        public DeptProfile()
        {
            CreateMap<department, DeptViewModel>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.PId, opt => opt.MapFrom(s => s.PId))
               .ForMember(d => d.DeptName, opt => opt.MapFrom(s => s.DeptName))
               ;

            CreateMap<DeptViewModel, department>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.PId, opt => opt.MapFrom(s => s.PId))
               .ForMember(d => d.DeptName, opt => opt.MapFrom(s => s.DeptName))
               ;

        }
    }
}
