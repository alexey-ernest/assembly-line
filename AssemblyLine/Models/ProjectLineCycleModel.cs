using System.Collections.Generic;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.Models
{
    public class ProjectLineCycleModel
    {
        public int Id { get; set; }

        public CycleStatus Status { get; set; }

        public List<ProjectCycleMilestoneListModel> Milestones { get; set; }
    }
}