using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Bebidas : Producto
    {
        public bool Alcohol { get; set; }
        public int Volumen { get; set; }
    }
}
