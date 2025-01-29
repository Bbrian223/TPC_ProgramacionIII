using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cafeteria : Producto
    {
        public string TipoCafe { get; set; }
        public string Tamaño { get; set; }  //prodia ser un enum
        public bool LecheComun { get; set; }
        public bool LecheAlmendras { get; set; }
    }
}
