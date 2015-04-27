﻿using AssemblyLine.DAL.Entities;

namespace AssemblyLine.Models
{
    public class ProjectLineMilestoneModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MilestoneStatus Status { get; set; }
    }
}