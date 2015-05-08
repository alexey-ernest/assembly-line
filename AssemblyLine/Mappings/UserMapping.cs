using System.Web.Security;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.Mappings
{
    public class UserMapping : IMapping
    {
        public void CreateMap()
        {
            AutoMapper.Mapper.CreateMap<MembershipUser, User>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.UserName))
                .ForMember(x => x.UserName, o => o.MapFrom(s => s.UserName))
                .ForMember(x => x.Email, o => o.MapFrom(s => s.Email))
                .ForMember(x => x.AccessFailedCount, o => o.Ignore())
                .ForMember(x => x.Claims, o => o.Ignore())
                .ForMember(x => x.EmailConfirmed, o => o.Ignore())
                .ForMember(x => x.LockoutEnabled, o => o.Ignore())
                .ForMember(x => x.LockoutEndDateUtc, o => o.Ignore())
                .ForMember(x => x.Logins, o => o.Ignore())
                .ForMember(x => x.PasswordHash, o => o.Ignore())
                .ForMember(x => x.PhoneNumber, o => o.Ignore())
                .ForMember(x => x.PhoneNumberConfirmed, o => o.Ignore())
                .ForMember(x => x.Roles, o => o.Ignore())
                .ForMember(x => x.SecurityStamp, o => o.Ignore())
                .ForMember(x => x.TwoFactorEnabled, o => o.Ignore())
                ;
        }
    }
}