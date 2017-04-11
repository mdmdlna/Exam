using AutoMapper;
using NtAbcExam.Domain.DataModels;
using NtAbcExam.Domain.Models;
using NtAbcExam.Web.Areas.Admin.Models;
using NtAbcExam.Web.Models;

namespace NtAbcExam.Web.DtoMapper.MapProfile
{
    public class AdminUserProfile : Profile
    {
        public AdminUserProfile()
        {
            CreateMap<AdminUser, AdminUserViewModel>()
               ;

            CreateMap<AdminUserViewModel, AdminUser>()
               ;

        }
    }
}
