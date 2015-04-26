using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssemblyLine.DAL.Entities
{
    public class AssemblyLineTeam
    {
        public int Id { get; set; }

        [Display(Name = "Team Manager")]
        public virtual Employee Manager { get; set; }

        [Display(Name = "Team Engineers")]
        public virtual ICollection<Employee> Engineers { get; set; }
    }
}