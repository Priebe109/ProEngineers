namespace Opgave2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class string1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jokes", "Tags", c => c.String());
        }
        
        public override void Down()
        {
        }
    }
}
