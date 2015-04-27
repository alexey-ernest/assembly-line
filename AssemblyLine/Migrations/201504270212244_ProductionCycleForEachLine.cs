namespace AssemblyLine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductionCycleForEachLine : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectProductionCycles", "Id", "dbo.Projects");
            AddForeignKey("dbo.ProjectProductionCycles", "Id", "dbo.ProjectAssemblyLines", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectProductionCycles", "Id", "dbo.ProjectAssemblyLines");
            AddForeignKey("dbo.ProjectProductionCycles", "Id", "dbo.Projects", "Id");
        }
    }
}
