﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Mesa
    {
        public int IdMesa { get; set; }
        public int Numero { get; set; }
        public string Estado { get; set; }
        public Usuario UsuarioAsignado { get; set; }
    }
}
