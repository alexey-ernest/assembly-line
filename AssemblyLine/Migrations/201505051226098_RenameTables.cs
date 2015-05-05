namespace AssemblyLine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProjectProductionCycles", newName: "ProjectLineCycles");
            RenameTable(name: "dbo.ProjectAssemblyLines", newName: "ProjectLines");
            RenameColumn(table: "dbo.ProjectCycleMilestones", name: "ProjectProductionCycle_Id", newName: "ProjectLineCycle_Id");
            RenameIndex(table: "dbo.ProjectCycleMilestones", name: "IX_ProjectProductionCycle_Id", newName: "IX_ProjectLineCycle_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProjectCycleMilestones", name: "IX_ProjectLineCycle_Id", newName: "IX_ProjectProductionCycle_Id");
            RenameColumn(table: "dbo.ProjectCycleMilestones", name: "ProjectLineCycle_Id", newName: "ProjectProductionCycle_Id");
            RenameTable(name: "dbo.ProjectLines", newName: "ProjectAssemblyLines");
            RenameTable(name: "dbo.ProjectLineCycles", newName: "ProjectProductionCycles");
        }
    }
}
