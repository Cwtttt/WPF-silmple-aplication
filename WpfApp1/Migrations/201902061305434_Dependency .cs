namespace WpfApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dependency : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "Rent_Id", "dbo.Rents");
            DropIndex("dbo.Cars", new[] { "Rent_Id" });
            AddColumn("dbo.Rents", "Car_Id", c => c.Guid());
            AlterColumn("dbo.Rents", "ReturnDate", c => c.DateTime());
            CreateIndex("dbo.Rents", "Car_Id");
            AddForeignKey("dbo.Rents", "Car_Id", "dbo.Cars", "Id");
            DropColumn("dbo.Cars", "Rent_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "Rent_Id", c => c.Guid());
            DropForeignKey("dbo.Rents", "Car_Id", "dbo.Cars");
            DropIndex("dbo.Rents", new[] { "Car_Id" });
            AlterColumn("dbo.Rents", "ReturnDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Rents", "Car_Id");
            CreateIndex("dbo.Cars", "Rent_Id");
            AddForeignKey("dbo.Cars", "Rent_Id", "dbo.Rents", "Id");
        }
    }
}
