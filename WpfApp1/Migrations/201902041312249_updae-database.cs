namespace WpfApp1.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Updaedatabase : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Cars");
            DropColumn("dbo.Cars", "Id");
            AddColumn("dbo.Cars", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Cars", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Cars");
            DropColumn("dbo.Cars", "Id");
            AddColumn("dbo.Cars", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Cars", "Id");
        }
    }
}
