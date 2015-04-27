using AssemblyLine.DAL.Entities;
using AssemblyLine.Models;

namespace AssemblyLine.Mappings
{
    public class ProjectMapping : IMapping
    {
        public void CreateMap()
        {
            AutoMapper.Mapper.CreateMap<Project, ProjectListModel>()
                ;
        }
    }
}