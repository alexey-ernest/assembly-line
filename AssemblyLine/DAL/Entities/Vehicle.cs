using System.ComponentModel.DataAnnotations;

namespace AssemblyLine.DAL.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}