using AlmacenPorAhi_ASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlmacenPorAhiASP.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
    }
}
