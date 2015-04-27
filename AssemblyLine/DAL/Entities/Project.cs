using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AssemblyLine.DAL.Entities
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Vehicle")]
        public virtual Vehicle Vehicle { get; set; }

        [Display(Name = "Vehicle Number")]
        public int? VehicleNumber { get; set; }

        [Display(Name = "Assembly Lines")]
        public int? AssemblyLinesNumber { get; set; }

        [Display(Name = "Assembly Lines")]
        [JsonIgnore]
        public virtual ICollection<ProjectAssemblyLine> AssemblyLines { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Started { get; set; }

        public DateTime? Completed { get; set; }

        public ProjectStatus Status { get; set; }


        #region Delivery Parameters

        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }

        [Display(Name = "Contact Phone")]
        public string DeliveryPhone { get; set; }

        [Display(Name = "Contact Person")]
        public string DeliveryContactPerson { get; set; }

        [Display(Name = "Delivery Date")]
        public DateTime? DeliveryDate { get; set; }

        #endregion
    }
}