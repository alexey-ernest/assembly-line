using System.Collections.Generic;

namespace AssemblyLine.DAL.Entities
{
    public class AssemblyLineTeam
    {
        public int Id { get; set; }

        public virtual Employee Manager { get; set; }

        public virtual ICollection<Employee> Engineers { get; set; }
    }
}