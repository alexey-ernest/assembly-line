using System;
using System.Data.Entity.Migrations;
using AssemblyLine.DAL;
using AssemblyLine.DAL.Entities;

namespace AssemblyLine.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // This method will be called after migrating to the latest version.

            context.Employees.AddOrUpdate(
                e => new {e.FirstName, e.LastName},
                new Employee
                {
                    FirstName = "Alexey",
                    LastName = "Melnikov",
                    Post = "CTO",
                    Created = DateTime.Now.AddDays(-7)
                },
                new Employee
                {
                    FirstName = "Brogan",
                    LastName = "Bembrogan",
                    Post = "Engineer",
                    Created = DateTime.Now.AddDays(-2)
                },
                new Employee
                {
                    FirstName = "Bob",
                    LastName = "Dilan",
                    Post = "Engineer",
                    Created = DateTime.Now.AddDays(-3)
                },
                new Employee
                {
                    FirstName = "Niko",
                    LastName = "Williams",
                    Post = "Engineer",
                    Created = DateTime.Now.AddDays(-4)
                },
                new Employee
                {
                    FirstName = "James",
                    LastName = "Moriarty",
                    Post = "Engineer",
                    Created = DateTime.Now.AddDays(-2)
                },
                new Employee
                {
                    FirstName = "Elon",
                    LastName = "Musk",
                    Post = "CEO",
                    Created = DateTime.Now.AddDays(-7)
                }
                );

            context.Vehicles.AddOrUpdate(
                v => v.Name,
                new Vehicle {Name = "M1135 nuclear"},
                new Vehicle {Name = "MEV"},
                new Vehicle {Name = "M1126 infantry carrier vehicle"},
                new Vehicle {Name = "M1128 mobile gun system"},
                new Vehicle {Name = "M1134 anti-tank "}
                );

            context.Lines.AddOrUpdate(
                l => l.Name,
                new Line {Name = "Line 1"},
                new Line {Name = "Line 2"},
                new Line {Name = "Line 3"},
                new Line {Name = "Line 4"},
                new Line {Name = "Line 5"},
                new Line {Name = "Line 6"},
                new Line {Name = "Line 7"},
                new Line {Name = "Line 8"},
                new Line {Name = "Line 9"},
                new Line {Name = "Line 10"}
                );

            context.ProductionCycles.AddOrUpdate(
                c => c.Id,
                new ProductionCycle
                {
                    Id = 1,
                    Milestones = new[]
                    {
                        new CycleMilestone
                        {
                            Id = 1,
                            Name = "Planning",
                            Position = 0,
                            Tasks =
                                new[]
                                {
                                    new MilestoneTask
                                    {
                                        Name = "Resources",
                                        CheckPoints = new[]
                                        {
                                            new TaskPoint {Title = "Allocate Financial Resources", Position = 0},
                                            new TaskPoint {Title = "Allocate Human Resources", Position = 1},
                                            new TaskPoint {Title = "Make Working Scheduler", Position = 2}
                                        }
                                    },
                                    new MilestoneTask
                                    {
                                        Name = "Assembly Line",
                                        CheckPoints = new[]
                                        {
                                            new TaskPoint {Title = "Service Maintenance", Position = 0},
                                            new TaskPoint {Title = "Check & Reapair", Position = 1},
                                            new TaskPoint {Title = "Test Run", Position = 2}
                                        }
                                    },
                                    new MilestoneTask
                                    {
                                        Name = "Assets",
                                        CheckPoints = new[]
                                        {
                                            new TaskPoint {Title = "Allocate Assets", Position = 0},
                                            new TaskPoint {Title = "Buy Assets if Required", Position = 1},
                                            new TaskPoint {Title = "Store and Account Resources", Position = 2}
                                        }
                                    }
                                }
                        },
                        new CycleMilestone
                        {
                            Id = 2, 
                            Name = "Manufacturing", 
                            Position = 1,
                            Tasks =
                                new[]
                                {
                                    new MilestoneTask
                                    {
                                        Name = "Vehicle",
                                        CheckPoints = new[]
                                        {
                                            new TaskPoint {Title = "Produce Parts", Position = 0},
                                            new TaskPoint {Title = "Put Together", Position = 1},
                                            new TaskPoint {Title = "Smoke Test", Position = 2},
                                        }
                                    },
                                    new MilestoneTask
                                    {
                                        Name = "Body",
                                        CheckPoints = new[]
                                        {
                                            new TaskPoint {Title = "Produce Body", Position = 0},
                                            new TaskPoint {Title = "Put Vehicle", Position = 1},
                                            new TaskPoint {Title = "Add Doors", Position = 2},
                                            new TaskPoint {Title = "Add Interior", Position = 3},
                                            new TaskPoint {Title = "Add Glass", Position = 4}
                                        }
                                    },
                                    new MilestoneTask
                                    {
                                        Name = "Chassis",
                                        CheckPoints = new[]
                                        {
                                            new TaskPoint {Title = "Produce Chassis", Position = 0},
                                            new TaskPoint {Title = "Put to the Body", Position = 1},
                                            new TaskPoint {Title = "Connect with Vehicle", Position = 2},
                                            new TaskPoint {Title = "Test Connection", Position = 3},
                                            new TaskPoint {Title = "Put wheels", Position = 4}
                                        }
                                    }
                                }
                        },
                        new CycleMilestone
                        {
                            Id = 3, 
                            Name = "Validating", 
                            Position = 2,
                            Tasks =
                                new[]
                                {
                                    new MilestoneTask
                                    {
                                        Name = "Laboratory Tests",
                                        CheckPoints = new[]
                                        {
                                            new TaskPoint {Title = "Engine Off Test", Position = 0},
                                            new TaskPoint {Title = "Engine Onn Test", Position = 1}
                                        }
                                    },
                                    new MilestoneTask
                                    {
                                        Name = "Test Drive",
                                        CheckPoints = new[]
                                        {
                                            new TaskPoint {Title = "Soft Test", Position = 0},
                                            new TaskPoint {Title = "Normal Test", Position = 1},
                                            new TaskPoint {Title = "Extreme Test", Position = 2}
                                        }
                                    },
                                    new MilestoneTask
                                    {
                                        Name = "Documents",
                                        CheckPoints = new[]
                                        {
                                            new TaskPoint {Title = "Vehicle Registration", Position = 0},
                                            new TaskPoint {Title = "Vehicle Passport", Position = 1}
                                        }
                                    }
                                }
                        }
                    }
                }
                );
        }
    }
}