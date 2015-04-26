namespace AssemblyLine.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AssemblyLines : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssemblyLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ProjectAssemblyLines", "Line_Id", c => c.Int());
            CreateIndex("dbo.ProjectAssemblyLines", "Line_Id");
            AddForeignKey("dbo.ProjectAssemblyLines", "Line_Id", "dbo.AssemblyLines", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectAssemblyLines", "Line_Id", "dbo.AssemblyLines");
            DropIndex("dbo.ProjectAssemblyLines", new[] { "Line_Id" });
            DropColumn("dbo.ProjectAssemblyLines", "Line_Id");
            DropTable("dbo.AssemblyLines");
        }
    }
}
