namespace AssemblyLine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LineStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lines", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lines", "Status");
        }
    }
}
