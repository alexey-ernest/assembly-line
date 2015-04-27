using System.Collections.Generic;

namespace AssemblyLine.DAL.Entities
{
    public class MilestoneTask
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<TaskPoint> CheckPoints { get; set; }
    }
}