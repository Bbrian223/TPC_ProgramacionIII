using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha_hora { get; set; }
        public double Total { get; set; }
    }
}
