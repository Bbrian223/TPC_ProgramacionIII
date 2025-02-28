using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Empleado: Usuario
    {
        public long IdEmpleado { get; set; }
        public Direccion Direccion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public DateTime FechaNac { get; set; }
        public DateTime FechaIng { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int PedidosGenerados { get; set; }
        public Empleado() 
        {
            Direccion = new Direccion();
        }

    }
}
