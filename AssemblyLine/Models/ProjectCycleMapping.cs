using AssemblyLine.DAL.Entities;
using AssemblyLine.Mappings;
using Mapper = AutoMapper.Mapper;

namespace AssemblyLine.Models
{
    public class ProjectCycleMapping : IMapping
    {
        public void CreateMap()
        {
            Mapper.CreateMap<ProjectProductionCycle, ProjectLineCycleModel>()
                ;
        }
    }
}