namespace WpfApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cars", "Moc", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "LiczbaMiejsc", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "Avaliable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cars", "Rent_Id", c => c.Guid());
            CreateIndex("dbo.Cars", "Rent_Id");
            AddForeignKey("dbo.Cars", "Rent_Id", "dbo.Rents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "Rent_Id", "dbo.Rents");
            DropIndex("dbo.Cars", new[] { "Rent_Id" });
            DropColumn("dbo.Cars", "Rent_Id");
            DropColumn("dbo.Cars", "Avaliable");
            DropColumn("dbo.Cars", "LiczbaMiejsc");
            DropColumn("dbo.Cars", "Moc");
            DropTable("dbo.Rents");
        }
    }
}
