using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AssemblyLine.DAL.Entities
{
    public class Line
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Line Name")]
        public string Name { get; set; }

        [Display(Name = "Line Status")]
        public LineStatus Status { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProjectLine> ProjectLines { get; set; }
    }
}