namespace AssemblyLine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Projects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        VehicleNumber = c.Int(),
                        Created = c.DateTime(nullable: false),
                        Started = c.DateTime(),
                        Completed = c.DateTime(),
                        Status = c.Int(nullable: false),
                        DeliveryAddress = c.String(),
                        DeliveryPhone = c.String(),
                        DeliveryContactPerson = c.String(),
                        DeliveryDate = c.DateTime(nullable: false),
                        Cycle_Id = c.Int(),
                        Vehicle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductionCycles", t => t.Cycle_Id)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_Id)
                .Index(t => t.Cycle_Id)
                .Index(t => t.Vehicle_Id);
            
            CreateTable(
                "dbo.ProjectAssemblyLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcurementTeam_Id = c.Int(),
                        ProductionTeam_Id = c.Int(),
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssemblyLineTeams", t => t.ProcurementTeam_Id)
                .ForeignKey("dbo.AssemblyLineTeams", t => t.ProductionTeam_Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.ProcurementTeam_Id)
                .Index(t => t.ProductionTeam_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.AssemblyLineTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Manager_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Manager_Id)
                .Index(t => t.Manager_Id);
            
            CreateTable(
                "dbo.ProductionCycles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Position = c.Int(nullable: false),
                        Started = c.DateTime(),
                        Completed = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Milestone_Id = c.Int(),
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CycleMilestones", t => t.Milestone_Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Milestone_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.CycleMilestones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Position = c.Int(nullable: false),
                        Started = c.DateTime(),
                        Completed = c.DateTime(),
                        Status = c.Int(nullable: false),
                        ProductionCycle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductionCycles", t => t.ProductionCycle_Id)
                .Index(t => t.ProductionCycle_Id);
            
            CreateTable(
                "dbo.MilestoneTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Started = c.DateTime(),
                        Completed = c.DateTime(),
                        Status = c.Int(nullable: false),
                        CycleMilestone_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CycleMilestones", t => t.CycleMilestone_Id)
                .Index(t => t.CycleMilestone_Id);
            
            CreateTable(
                "dbo.TaskPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Position = c.Int(nullable: false),
                        MilestoneTask_Id = c.Int(),
                        TaskPerformer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MilestoneTasks", t => t.MilestoneTask_Id)
                .ForeignKey("dbo.TaskPerformers", t => t.TaskPerformer_Id)
                .Index(t => t.MilestoneTask_Id)
                .Index(t => t.TaskPerformer_Id);
            
            CreateTable(
                "dbo.TaskPerformers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Task_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MilestoneTasks", t => t.Task_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Task_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Employees", "AssemblyLineTeam_Id", c => c.Int());
            AlterColumn("dbo.Vehicles", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Employees", "AssemblyLineTeam_Id");
            AddForeignKey("dbo.Employees", "AssemblyLineTeam_Id", "dbo.AssemblyLineTeams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Vehicle_Id", "dbo.Vehicles");
            DropForeignKey("dbo.ProductionCycles", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Projects", "Cycle_Id", "dbo.ProductionCycles");
            DropForeignKey("dbo.CycleMilestones", "ProductionCycle_Id", "dbo.ProductionCycles");
            DropForeignKey("dbo.ProductionCycles", "Milestone_Id", "dbo.CycleMilestones");
            DropForeignKey("dbo.MilestoneTasks", "CycleMilestone_Id", "dbo.CycleMilestones");
            DropForeignKey("dbo.TaskPerformers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TaskPerformers", "Task_Id", "dbo.MilestoneTasks");
            DropForeignKey("dbo.TaskPoints", "TaskPerformer_Id", "dbo.TaskPerformers");
            DropForeignKey("dbo.TaskPoints", "MilestoneTask_Id", "dbo.MilestoneTasks");
            DropForeignKey("dbo.ProjectAssemblyLines", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.ProjectAssemblyLines", "ProductionTeam_Id", "dbo.AssemblyLineTeams");
            DropForeignKey("dbo.ProjectAssemblyLines", "ProcurementTeam_Id", "dbo.AssemblyLineTeams");
            DropForeignKey("dbo.AssemblyLineTeams", "Manager_Id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "AssemblyLineTeam_Id", "dbo.AssemblyLineTeams");
            DropIndex("dbo.TaskPerformers", new[] { "User_Id" });
            DropIndex("dbo.TaskPerformers", new[] { "Task_Id" });
            DropIndex("dbo.TaskPoints", new[] { "TaskPerformer_Id" });
            DropIndex("dbo.TaskPoints", new[] { "MilestoneTask_Id" });
            DropIndex("dbo.MilestoneTasks", new[] { "CycleMilestone_Id" });
            DropIndex("dbo.CycleMilestones", new[] { "ProductionCycle_Id" });
            DropIndex("dbo.ProductionCycles", new[] { "Project_Id" });
            DropIndex("dbo.ProductionCycles", new[] { "Milestone_Id" });
            DropIndex("dbo.AssemblyLineTeams", new[] { "Manager_Id" });
            DropIndex("dbo.ProjectAssemblyLines", new[] { "Project_Id" });
            DropIndex("dbo.ProjectAssemblyLines", new[] { "ProductionTeam_Id" });
            DropIndex("dbo.ProjectAssemblyLines", new[] { "ProcurementTeam_Id" });
            DropIndex("dbo.Projects", new[] { "Vehicle_Id" });
            DropIndex("dbo.Projects", new[] { "Cycle_Id" });
            DropIndex("dbo.Employees", new[] { "AssemblyLineTeam_Id" });
            AlterColumn("dbo.Vehicles", "Name", c => c.String());
            DropColumn("dbo.Employees", "AssemblyLineTeam_Id");
            DropTable("dbo.TaskPerformers");
            DropTable("dbo.TaskPoints");
            DropTable("dbo.MilestoneTasks");
            DropTable("dbo.CycleMilestones");
            DropTable("dbo.ProductionCycles");
            DropTable("dbo.AssemblyLineTeams");
            DropTable("dbo.ProjectAssemblyLines");
            DropTable("dbo.Projects");
        }
    }
}
