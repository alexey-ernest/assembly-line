namespace AssemblyLine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAssemblyLine : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AssemblyLines", newName: "Lines");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Lines", newName: "AssemblyLines");
        }
    }
}
