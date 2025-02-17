using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ImagenUser
    {
        public long IdImagen { get; set; }
        public string NombreArch { get; set; }
        public string DirComp
        {
            get { return "~/Database/Imagenes/Perfiles/" + NombreArch + ".jpg"; }
        }
    }
}
