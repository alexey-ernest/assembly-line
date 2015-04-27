using AssemblyLine.DAL.Entities;
using AssemblyLine.Models;

namespace AssemblyLine.Mappings
{
    public class AssemblyLineTeamMapping : IMapping
    {
        public void CreateMap()
        {
            AutoMapper.Mapper.CreateMap<AssemblyLineTeam, AssemblyLineTeamModel>()
                ;
        }
    }
}