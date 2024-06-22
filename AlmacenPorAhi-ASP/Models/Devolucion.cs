using AlmacenPorAhiASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlmacenPorAhi_ASP.Models
{
    public class Devolucion
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public Venta Venta { get; set; }
        public String NombreCliente { get; set; }
        public String NombreProducto { get; set; }
        public DateTime Fecha { get; set; }
        public int TotalDevolucion { get; set; }
        public EstadoDevolucion Estado { get; set; }
    }

    public enum EstadoDevolucion
    {
        Nuevo,
        Aceptada,
        Rechazada,
        Completada
    }
}
