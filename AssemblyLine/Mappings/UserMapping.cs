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
                ;
        }
    }
}