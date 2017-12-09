namespace DatacenterMap.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "DatacenterMap.Andar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroAndar = c.Int(nullable: false),
                        QuantidadeMaximaSalas = c.Int(nullable: false),
                        Edificacao_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("DatacenterMap.Edificacao", t => t.Edificacao_Id)
                .Index(t => t.Edificacao_Id);
            
            CreateTable(
                "DatacenterMap.Edificacao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 64, unicode: false),
                        NumeroAndares = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DatacenterMap.Sala",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroSala = c.String(nullable: false, maxLength: 80, unicode: false),
                        QuantidadeMaximaSlots = c.Int(nullable: false),
                        Largura = c.Double(nullable: false),
                        Comprimento = c.Double(nullable: false),
                        Andar_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("DatacenterMap.Andar", t => t.Andar_Id)
                .Index(t => t.Andar_Id);
            
            CreateTable(
                "DatacenterMap.Slot",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ocupado = c.Boolean(nullable: false),
                        Sala_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("DatacenterMap.Sala", t => t.Sala_Id)
                .Index(t => t.Sala_Id);
            
            CreateTable(
                "DatacenterMap.Equipamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 255, unicode: false),
                        Tamanho = c.Int(nullable: false),
                        Tensao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DatacenterMap.Gaveta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ocupado = c.Boolean(nullable: false),
                        Posicao = c.Int(nullable: false),
                        Equipamento_Id = c.Int(),
                        Rack_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("DatacenterMap.Equipamento", t => t.Equipamento_Id)
                .ForeignKey("DatacenterMap.Rack", t => t.Rack_Id)
                .Index(t => t.Equipamento_Id)
                .Index(t => t.Rack_Id);
            
            CreateTable(
                "DatacenterMap.Rack",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuantidadeGavetas = c.Int(nullable: false),
                        Tensao = c.Int(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 255, unicode: false),
                        Slot_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("DatacenterMap.Slot", t => t.Slot_Id)
                .Index(t => t.Slot_Id);
            
            CreateTable(
                "DatacenterMap.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 64, unicode: false),
                        Email = c.String(nullable: false, maxLength: 128, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("DatacenterMap.Rack", "Slot_Id", "DatacenterMap.Slot");
            DropForeignKey("DatacenterMap.Gaveta", "Rack_Id", "DatacenterMap.Rack");
            DropForeignKey("DatacenterMap.Gaveta", "Equipamento_Id", "DatacenterMap.Equipamento");
            DropForeignKey("DatacenterMap.Slot", "Sala_Id", "DatacenterMap.Sala");
            DropForeignKey("DatacenterMap.Sala", "Andar_Id", "DatacenterMap.Andar");
            DropForeignKey("DatacenterMap.Andar", "Edificacao_Id", "DatacenterMap.Edificacao");
            DropIndex("DatacenterMap.Rack", new[] { "Slot_Id" });
            DropIndex("DatacenterMap.Gaveta", new[] { "Rack_Id" });
            DropIndex("DatacenterMap.Gaveta", new[] { "Equipamento_Id" });
            DropIndex("DatacenterMap.Slot", new[] { "Sala_Id" });
            DropIndex("DatacenterMap.Sala", new[] { "Andar_Id" });
            DropIndex("DatacenterMap.Andar", new[] { "Edificacao_Id" });
            DropTable("DatacenterMap.Usuario");
            DropTable("DatacenterMap.Rack");
            DropTable("DatacenterMap.Gaveta");
            DropTable("DatacenterMap.Equipamento");
            DropTable("DatacenterMap.Slot");
            DropTable("DatacenterMap.Sala");
            DropTable("DatacenterMap.Edificacao");
            DropTable("DatacenterMap.Andar");
        }
    }
}
