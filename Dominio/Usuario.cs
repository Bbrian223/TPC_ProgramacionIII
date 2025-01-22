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
        private int idusuario;
        private string nombreusuario;
        private string clave;
        private bool estado;
        private UserType rol;

        public int IdUsuario 
        {
            get { return idusuario;} 
        }
        public string NombreUsuario 
        {
            get { return nombreusuario; } 
        }
        public string Clave 
        {
            get { return clave; } 
        }
        public bool Estado 
        {
            get { return estado; } 
        }
        public UserType Rol 
        {
            get { return rol; } 
        }

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
