using System;
using System.Collections.Generic;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ProjectStatus Status { get; set; }

        public DateTime Created { get; set; }

        public List<ProjectLineListModel> AssemblyLines { get; set; }
    }
}