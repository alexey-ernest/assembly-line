using System;
using System.Collections.Generic;

namespace AssemblyLine.DAL.Entities
{
    public class MilestoneTask
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Started { get; set; }

        public DateTime? Completed { get; set; }

        public TaskStatus Status { get; set; }

        public virtual ICollection<TaskPerformer> Performers { get; set; }

        public virtual ICollection<TaskPoint> CheckPoints { get; set; }
    }
}