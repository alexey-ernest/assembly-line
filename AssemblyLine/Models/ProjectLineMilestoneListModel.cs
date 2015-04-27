using AssemblyLine.DAL.Entities;

namespace AssemblyLine.Models
{
    public class ProjectLineMilestoneListModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MilestoneStatus Status { get; set; }
    }
}