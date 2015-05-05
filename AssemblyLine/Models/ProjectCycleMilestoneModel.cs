using System.Collections.Generic;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.Models
{
    public class ProjectCycleMilestoneModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public MilestoneStatus Status { get; set; }

        public List<ProjectMilestoneTaskListModel> Tasks { get; set; }
    }
}