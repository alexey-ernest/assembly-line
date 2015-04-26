using System.ComponentModel.DataAnnotations;

namespace AssemblyLine.DAL.Entities
{
    public class Line
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Line Name")]
        public string Name { get; set; }
    }
}