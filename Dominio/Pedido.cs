using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pedido
    {
        public long IdPedido { get; set; }
        public Mesa Mesa { get; set; }
        public Usuario Empleado { get; set; }
        public string Estado { get; set; }
        public List<DetallePedido> ListaProd { get; set; }

        public Pedido() 
        { 
            Mesa = new Mesa();
            Empleado = new Usuario();
            ListaProd = new List<DetallePedido>();
        }

    }

    public class DetallePedido
    {
        public long IdDetallePedido { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public string Aclaraciones { get; set; }

        public DetallePedido()
        {
            Producto = new Producto();
        }
    }
}
