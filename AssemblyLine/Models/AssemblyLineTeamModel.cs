using System.Collections.Generic;

namespace AssemblyLine.Models
{
    public class AssemblyLineTeamModel
    {
        public int Id { get; set; }

        public virtual EmployeeListModel Manager { get; set; }

        public virtual IEnumerable<EmployeeListModel> Engineers { get; set; }
    }
}