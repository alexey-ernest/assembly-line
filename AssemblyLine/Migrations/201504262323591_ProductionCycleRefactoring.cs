namespace AssemblyLine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductionCycleRefactoring : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskPoints", "MilestoneTask_Id", "dbo.MilestoneTasks");
            DropForeignKey("dbo.TaskPoints", "TaskPerformer_Id", "dbo.TaskPerformers");
            DropForeignKey("dbo.TaskPerformers", "Task_Id", "dbo.MilestoneTasks");
            DropForeignKey("dbo.TaskPerformers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.MilestoneTasks", "CycleMilestone_Id", "dbo.CycleMilestones");
            DropForeignKey("dbo.ProductionCycles", "Milestone_Id", "dbo.CycleMilestones");
            DropForeignKey("dbo.CycleMilestones", "ProductionCycle_Id", "dbo.ProductionCycles");
            DropForeignKey("dbo.Projects", "Cycle_Id", "dbo.ProductionCycles");
            DropForeignKey("dbo.ProductionCycles", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Projects", new[] { "Cycle_Id" });
            DropIndex("dbo.ProductionCycles", new[] { "Milestone_Id" });
            DropIndex("dbo.ProductionCycles", new[] { "Project_Id" });
            DropIndex("dbo.CycleMilestones", new[] { "ProductionCycle_Id" });
            DropIndex("dbo.MilestoneTasks", new[] { "CycleMilestone_Id" });
            DropIndex("dbo.TaskPoints", new[] { "MilestoneTask_Id" });
            DropIndex("dbo.TaskPoints", new[] { "TaskPerformer_Id" });
            DropIndex("dbo.TaskPerformers", new[] { "Task_Id" });
            DropIndex("dbo.TaskPerformers", new[] { "User_Id" });
            DropColumn("dbo.Projects", "Cycle_Id");
            DropTable("dbo.ProductionCycles");
            DropTable("dbo.CycleMilestones");
            DropTable("dbo.MilestoneTasks");
            DropTable("dbo.TaskPoints");
            DropTable("dbo.TaskPerformers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TaskPerformers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Task_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Projects", "Cycle_Id", c => c.Int());
            CreateIndex("dbo.TaskPerformers", "User_Id");
            CreateIndex("dbo.TaskPerformers", "Task_Id");
            CreateIndex("dbo.TaskPoints", "TaskPerformer_Id");
            CreateIndex("dbo.TaskPoints", "MilestoneTask_Id");
            CreateIndex("dbo.MilestoneTasks", "CycleMilestone_Id");
            CreateIndex("dbo.CycleMilestones", "ProductionCycle_Id");
            CreateIndex("dbo.ProductionCycles", "Project_Id");
            CreateIndex("dbo.ProductionCycles", "Milestone_Id");
            CreateIndex("dbo.Projects", "Cycle_Id");
            AddForeignKey("dbo.ProductionCycles", "Project_Id", "dbo.Projects", "Id");
            AddForeignKey("dbo.Projects", "Cycle_Id", "dbo.ProductionCycles", "Id");
            AddForeignKey("dbo.CycleMilestones", "ProductionCycle_Id", "dbo.ProductionCycles", "Id");
            AddForeignKey("dbo.ProductionCycles", "Milestone_Id", "dbo.CycleMilestones", "Id");
            AddForeignKey("dbo.MilestoneTasks", "CycleMilestone_Id", "dbo.CycleMilestones", "Id");
            AddForeignKey("dbo.TaskPerformers", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.TaskPerformers", "Task_Id", "dbo.MilestoneTasks", "Id");
            AddForeignKey("dbo.TaskPoints", "TaskPerformer_Id", "dbo.TaskPerformers", "Id");
            AddForeignKey("dbo.TaskPoints", "MilestoneTask_Id", "dbo.MilestoneTasks", "Id");
        }
    }
}
