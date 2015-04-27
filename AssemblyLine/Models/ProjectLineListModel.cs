using System.Collections.Generic;

namespace AssemblyLine.Models
{
    public class ProjectLineListModel
    {
        public int Id { get; set; }

        public AssemblyLineModel Line { get; set; }

        public IEnumerable<ProjectLineMilestoneModel> Milestones { get; set; }
    }
}