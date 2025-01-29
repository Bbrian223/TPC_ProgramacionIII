using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Postres : Producto
    {
        public bool ContieneAzucar { get; set; }
        public bool ContieneGluten { get; set; }
    }
}
