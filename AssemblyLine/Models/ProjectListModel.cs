using System;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.Models
{
    public class ProjectListModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ProjectStatus Status { get; set; }

        public DateTime Created { get; set; }
    }
}