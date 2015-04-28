using AssemblyLine.DAL.Entities;
using AssemblyLine.Models;

namespace AssemblyLine.Mappings
{
    public class ProjectMilestoneMapping : IMapping
    {
        public void CreateMap()
        {
            AutoMapper.Mapper.CreateMap<ProjectCycleMilestone, ProjectLineMilestoneListModel>()
                ;

            AutoMapper.Mapper.CreateMap<ProjectCycleMilestone, ProjectLineMilestoneModel>()
                ;
        }
    }
}