namespace AlmacenPorAhi_ASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.ProductoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.ClienteId)
                .Index(t => t.ProductoId)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ventas", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Ventas", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.Ventas", "ClienteId", "dbo.Clientes");
            DropIndex("dbo.Ventas", new[] { "UsuarioId" });
            DropIndex("dbo.Ventas", new[] { "ProductoId" });
            DropIndex("dbo.Ventas", new[] { "ClienteId" });
            DropTable("dbo.Ventas");
        }
    }
}
