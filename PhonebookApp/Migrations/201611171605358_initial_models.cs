namespace PhonebookApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.contact_person",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        last_name = c.String(maxLength: 100, unicode: false),
                        first_name = c.String(maxLength: 100, unicode: false),
                        address = c.String(maxLength: 100, unicode: false),
                        birthday = c.DateTime(nullable: false),
                        email = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.phonenumber",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        phonenumber = c.String(maxLength: 100, unicode: false),
                        contact_person_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.contact_person", t => t.contact_person_id)
                .Index(t => t.contact_person_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.phonenumber", "contact_person_id", "dbo.contact_person");
            DropIndex("dbo.phonenumber", new[] { "contact_person_id" });
            DropTable("dbo.phonenumber");
            DropTable("dbo.contact_person");
        }
    }
}
