using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{   
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public bool Estado { get; set; }
        //momentaneo
        public string Rol { get; set; }


        public void CambiarClave(string contra)
        {
            //cambio de contrasenia del usuario
        }
    }
}
