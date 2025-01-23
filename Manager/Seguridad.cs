using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public static class Seguridad
    {
        private static UserType nivelAcceso;


        public static UserType NivelAcceso 
        {
            get { return nivelAcceso; } 
        }
        
        public static bool sesionActiva(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : new Usuario();
            nivelAcceso = usuario.rol;
            
            return usuario.estado;
        }

        public static void CerrarSesion() 
        {
            nivelAcceso = UserType.invalid;
        }


    }
}
