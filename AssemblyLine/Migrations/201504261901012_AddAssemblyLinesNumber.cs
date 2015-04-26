namespace AssemblyLine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssemblyLinesNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "AssemblyLinesNumber", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "AssemblyLinesNumber");
        }
    }
}
