using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public Mesa Mesa { get; set; }
        public Usuario Empleado { get; set; }
        public List<ItemsPedidos> ListaProductos { get; set; }

    }

    public class ItemsPedidos
    {
        public int IdItemPedidos { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public double subtotal { get; set; }
    }
}
