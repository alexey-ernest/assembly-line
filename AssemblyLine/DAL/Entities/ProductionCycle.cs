using System;
using System.Collections.Generic;

namespace AssemblyLine.DAL.Entities
{
    public class ProductionCycle
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public DateTime? Started { get; set; }

        public DateTime? Completed { get; set; }

        public CycleStatus Status { get; set; }

        public virtual ICollection<CycleMilestone> Milestones { get; set; }

        public virtual CycleMilestone Milestone { get; set; }
    }
}