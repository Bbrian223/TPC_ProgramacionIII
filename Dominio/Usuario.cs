using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{   
    public enum UserType 
    { 
        invalid = 0,
        Gerente = 1,
        Mozo = 2
    }

    public class Usuario
    {
        public long idusuario { get; set; }
        public string nombreusuario { get; set; }
        public string clave { get; set; }
        public UserType rol { get; set; }
        public bool estado { get; set; }

        public Usuario() 
        { 
            idusuario = 0;
            nombreusuario = string.Empty;
            clave = string.Empty;
            estado = false;
            rol = UserType.invalid;
        }

    }
}
