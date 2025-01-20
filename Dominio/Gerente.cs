using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Gerente : Usuario
    {
        public int IdGerente { get; set; }
        public string Nombre { get; set; }
        public string apellido { get; set; }
    }
}
