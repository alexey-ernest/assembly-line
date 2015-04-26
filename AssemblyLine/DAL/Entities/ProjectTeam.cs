using System.Collections.Generic;

namespace AssemblyLine.DAL.Entities
{
    public class ProjectTeam
    {
        public int Id { get; set; }

        public virtual Employee Manager { get; set; }

        public virtual ICollection<Employee> Engineers { get; set; }
    }
}