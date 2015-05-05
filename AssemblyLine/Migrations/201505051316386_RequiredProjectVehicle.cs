namespace AssemblyLine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredProjectVehicle : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.Projects", new[] { "Vehicle_Id" });
            AlterColumn("dbo.Projects", "Vehicle_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "Vehicle_Id");
            AddForeignKey("dbo.Projects", "Vehicle_Id", "dbo.Vehicles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.Projects", new[] { "Vehicle_Id" });
            AlterColumn("dbo.Projects", "Vehicle_Id", c => c.Int());
            CreateIndex("dbo.Projects", "Vehicle_Id");
            AddForeignKey("dbo.Projects", "Vehicle_Id", "dbo.Vehicles", "Id");
        }
    }
}
