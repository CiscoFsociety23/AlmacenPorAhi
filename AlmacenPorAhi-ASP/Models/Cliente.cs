using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlmacenPorAhi_ASP.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
    }
}