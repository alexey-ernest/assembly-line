namespace AssemblyLine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeeCreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Created");
        }
    }
}
