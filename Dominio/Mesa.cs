﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Mesa
    {
        public long IdMesa { get; set; }
        public long IdSalon { get; set; }
        public string EstadoMesa { get; set; }
        public Usuario UsuarioAsignado { get; set; }
        public bool Habilitado { get; set; }

        public Mesa()
        { 
            UsuarioAsignado = new Usuario();
            EstadoMesa = string.Empty;
        }
    }

    public class Salon
    {
        public long IdSalon { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }

        public Salon() 
        {
            IdSalon = 0;
            Nombre = string.Empty;
            Estado = false;
        }
    }
}
