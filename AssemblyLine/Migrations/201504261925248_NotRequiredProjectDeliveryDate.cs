namespace AssemblyLine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotRequiredProjectDeliveryDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "DeliveryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "DeliveryDate", c => c.DateTime(nullable: false));
        }
    }
}
