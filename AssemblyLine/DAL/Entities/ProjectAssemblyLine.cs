using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AssemblyLine.DAL.Entities
{
    public class ProjectAssemblyLine
    {
        public int Id { get; set; }

        [JsonIgnore]
        public virtual Project Project { get; set; }

        public virtual Line Line { get; set; }

        [Display(Name = "Production Team")]
        public virtual AssemblyLineTeam ProductionTeam { get; set; }

        [Display(Name = "Procurement Team")]
        public virtual AssemblyLineTeam ProcurementTeam { get; set; }
    }
}