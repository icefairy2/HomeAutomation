namespace webapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lamps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsTurnedOn = c.Boolean(nullable: false),
                        DateRecorded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Lamps");
        }
    }
}
