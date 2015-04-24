using System;
using System.Collections.Generic;

namespace AssemblyLine.DAL.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public int? VehicleNumber { get; set; }

        public virtual ICollection<ProjectAssemblyLine> AssemblyLines { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Started { get; set; }

        public DateTime? Completed { get; set; }

        public ProjectStatus Status { get; set; }

        public virtual ICollection<ProductionCycle> Cycles { get; set; }

        public virtual ProductionCycle Cycle { get; set; }


        #region delivery info

        public string DeliveryAddress { get; set; }

        public string DeliveryPhone { get; set; }

        public string DeliveryContactPerson { get; set; }

        public DateTime DeliveryDate { get; set; }

        #endregion
    }
}