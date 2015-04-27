using AssemblyLine.DAL.Entities;
using AssemblyLine.Models;

namespace AssemblyLine.Mappings
{
    public class ProjectLineMapping : IMapping
    {
        public void CreateMap()
        {
            AutoMapper.Mapper.CreateMap<ProjectAssemblyLine, ProjectLineListModel>()
                ;

            AutoMapper.Mapper.CreateMap<ProjectAssemblyLine, ProjectLineModel>()
                ;
        }
    }
}