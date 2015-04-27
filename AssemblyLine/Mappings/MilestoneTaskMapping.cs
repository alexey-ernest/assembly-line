using AssemblyLine.DAL.Entities;
using AssemblyLine.Models;

namespace AssemblyLine.Mappings
{
    public class MilestoneTaskMapping : IMapping
    {
        public void CreateMap()
        {
            AutoMapper.Mapper.CreateMap<MilestoneTask, MilestoneTaskListModel>()
                ;
        }
    }
}