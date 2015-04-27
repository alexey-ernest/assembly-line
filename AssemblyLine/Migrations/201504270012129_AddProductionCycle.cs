namespace AssemblyLine.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddProductionCycle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskPerformers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Employee_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.ProjectMilestoneTasks", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.TaskPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Position = c.Int(nullable: false),
                        TaskPerformer_Id = c.Int(),
                        TaskPerformer_Id1 = c.Int(),
                        MilestoneTask_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskPerformers", t => t.TaskPerformer_Id)
                .ForeignKey("dbo.TaskPerformers", t => t.TaskPerformer_Id1)
                .ForeignKey("dbo.MilestoneTasks", t => t.MilestoneTask_Id)
                .Index(t => t.TaskPerformer_Id)
                .Index(t => t.TaskPerformer_Id1)
                .Index(t => t.MilestoneTask_Id);
            
            CreateTable(
                "dbo.ProjectMilestoneTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Started = c.DateTime(),
                        Completed = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Milestone_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectCycleMilestones", t => t.Milestone_Id)
                .Index(t => t.Milestone_Id);
            
            CreateTable(
                "dbo.ProjectCycleMilestones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Position = c.Int(nullable: false),
                        Started = c.DateTime(),
                        Completed = c.DateTime(),
                        Status = c.Int(nullable: false),
                        ProjectProductionCycle_Id = c.Int(),
                        Cycle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectProductionCycles", t => t.ProjectProductionCycle_Id)
                .ForeignKey("dbo.ProjectProductionCycles", t => t.Cycle_Id)
                .Index(t => t.ProjectProductionCycle_Id)
                .Index(t => t.Cycle_Id);
            
            CreateTable(
                "dbo.ProjectProductionCycles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Started = c.DateTime(),
                        Completed = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Milestone_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectCycleMilestones", t => t.Milestone_Id)
                .ForeignKey("dbo.Projects", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Milestone_Id);
            
            CreateTable(
                "dbo.CycleMilestones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Position = c.Int(nullable: false),
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
                        CycleMilestone_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CycleMilestones", t => t.CycleMilestone_Id)
                .Index(t => t.CycleMilestone_Id);
            
            CreateTable(
                "dbo.ProductionCycles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CycleMilestones", "ProductionCycle_Id", "dbo.ProductionCycles");
            DropForeignKey("dbo.MilestoneTasks", "CycleMilestone_Id", "dbo.CycleMilestones");
            DropForeignKey("dbo.TaskPoints", "MilestoneTask_Id", "dbo.MilestoneTasks");
            DropForeignKey("dbo.TaskPerformers", "Id", "dbo.ProjectMilestoneTasks");
            DropForeignKey("dbo.ProjectMilestoneTasks", "Milestone_Id", "dbo.ProjectCycleMilestones");
            DropForeignKey("dbo.ProjectCycleMilestones", "Cycle_Id", "dbo.ProjectProductionCycles");
            DropForeignKey("dbo.ProjectProductionCycles", "Id", "dbo.Projects");
            DropForeignKey("dbo.ProjectCycleMilestones", "ProjectProductionCycle_Id", "dbo.ProjectProductionCycles");
            DropForeignKey("dbo.ProjectProductionCycles", "Milestone_Id", "dbo.ProjectCycleMilestones");
            DropForeignKey("dbo.TaskPerformers", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.TaskPoints", "TaskPerformer_Id1", "dbo.TaskPerformers");
            DropForeignKey("dbo.TaskPoints", "TaskPerformer_Id", "dbo.TaskPerformers");
            DropIndex("dbo.MilestoneTasks", new[] { "CycleMilestone_Id" });
            DropIndex("dbo.CycleMilestones", new[] { "ProductionCycle_Id" });
            DropIndex("dbo.ProjectProductionCycles", new[] { "Milestone_Id" });
            DropIndex("dbo.ProjectProductionCycles", new[] { "Id" });
            DropIndex("dbo.ProjectCycleMilestones", new[] { "Cycle_Id" });
            DropIndex("dbo.ProjectCycleMilestones", new[] { "ProjectProductionCycle_Id" });
            DropIndex("dbo.ProjectMilestoneTasks", new[] { "Milestone_Id" });
            DropIndex("dbo.TaskPoints", new[] { "MilestoneTask_Id" });
            DropIndex("dbo.TaskPoints", new[] { "TaskPerformer_Id1" });
            DropIndex("dbo.TaskPoints", new[] { "TaskPerformer_Id" });
            DropIndex("dbo.TaskPerformers", new[] { "Employee_Id" });
            DropIndex("dbo.TaskPerformers", new[] { "Id" });
            DropTable("dbo.ProductionCycles");
            DropTable("dbo.MilestoneTasks");
            DropTable("dbo.CycleMilestones");
            DropTable("dbo.ProjectProductionCycles");
            DropTable("dbo.ProjectCycleMilestones");
            DropTable("dbo.ProjectMilestoneTasks");
            DropTable("dbo.TaskPoints");
            DropTable("dbo.TaskPerformers");
        }
    }
}
