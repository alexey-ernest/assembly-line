using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssemblyLine.DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public User User { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Post { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection<TaskPerformer> Tasks { get; set; }
    }
}