using AssemblyLine.DAL.Entities;
using AssemblyLine.Models;

namespace AssemblyLine.Mappings
{
    public class ProjectLineMapping : IMapping
    {
        public void CreateMap()
        {
            AutoMapper.Mapper.CreateMap<ProjectAssemblyLine, ProjectLineListModel>()
                .ForMember(x => x.Milestones, o => o.MapFrom(s => s.Cycle != null ? s.Cycle.Milestones : null))
                ;

            AutoMapper.Mapper.CreateMap<ProjectAssemblyLine, ProjectLineModel>()
                ;
        }
    }
}