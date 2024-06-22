namespace AlmacenPorAhi_ASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDevoluciones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devolucions", "NombreCliente", c => c.String());
            AddColumn("dbo.Devolucions", "NombreProducto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devolucions", "NombreProducto");
            DropColumn("dbo.Devolucions", "NombreCliente");
        }
    }
}
