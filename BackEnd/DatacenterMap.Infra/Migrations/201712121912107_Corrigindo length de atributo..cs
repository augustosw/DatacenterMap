namespace DatacenterMap.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Corrigindolengthdeatributo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("DatacenterMap.Sala", "NumeroSala", c => c.String(nullable: false, maxLength: 80, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("DatacenterMap.Sala", "NumeroSala", c => c.String(nullable: false, maxLength: 8000, unicode: false));
        }
    }
}
