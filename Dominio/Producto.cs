using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Producto
    {
        public long IdProducto { get; set; }
        public CategoriaProducto Categoria { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int stock { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public Producto()
        {
            Categoria = new CategoriaProducto();
        }
    }
}
