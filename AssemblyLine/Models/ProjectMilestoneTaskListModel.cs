using AssemblyLine.DAL.Entities;

namespace AssemblyLine.Models
{
    public class ProjectMilestoneTaskListModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TaskStatus Status { get; set; }
    }
}