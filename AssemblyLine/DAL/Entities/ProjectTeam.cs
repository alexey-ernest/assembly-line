using System.Collections.Generic;

namespace AssemblyLine.DAL.Entities
{
    public class ProjectTeam
    {
        public int Id { get; set; }

        public virtual User Manager { get; set; }

        public virtual ICollection<User> Engineers { get; set; }
    }
}