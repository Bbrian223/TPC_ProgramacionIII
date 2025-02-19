using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Venta
    {
        public long IdVenta { get; set; }
        public long IdPedido { get; set; }
        public Mesa Mesa { get; set; }
        public long IdUsuario { get; set; }
        public DateTime Fecha_hora { get; set; }
        public decimal Total { get; set; }

        public Venta()
        {
            Mesa = new Mesa();
        }
    }
}
