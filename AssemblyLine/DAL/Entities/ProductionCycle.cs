using System.Collections.Generic;

namespace AssemblyLine.DAL.Entities
{
    public class ProductionCycle
    {
        public int Id { get; set; }

        public virtual ICollection<CycleMilestone> Milestones { get; set; }
    }
}