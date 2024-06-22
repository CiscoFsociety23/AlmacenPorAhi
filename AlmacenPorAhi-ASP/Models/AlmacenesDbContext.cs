using AlmacenPorAhi_ASP.Models;
using AlmacenPorAhiASP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AlmacenPorAhiASP.Models
{
    public class AlmacenesDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Devolucion> Devoluciones { get; set; }
    }
}
