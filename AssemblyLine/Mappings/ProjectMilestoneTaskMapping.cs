using AssemblyLine.DAL.Entities;
using AssemblyLine.Models;

namespace AssemblyLine.Mappings
{
    public class ProjectMilestoneTaskMapping : IMapping
    {
        public void CreateMap()
        {
            AutoMapper.Mapper.CreateMap<ProjectMilestoneTask, ProjectMilestoneTaskListModel>()
                ;
        }
    }
}