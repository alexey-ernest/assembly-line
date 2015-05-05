using AssemblyLine.DAL.Entities;
using AssemblyLine.Models;

namespace AssemblyLine.Mappings
{
    public class ProjectLineMapping : IMapping
    {
        public void CreateMap()
        {
            AutoMapper.Mapper.CreateMap<ProjectLine, ProjectLineListModel>()
                ;

            AutoMapper.Mapper.CreateMap<ProjectLine, ProjectLineModel>()
                ;
        }
    }
}