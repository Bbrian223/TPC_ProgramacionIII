using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class EmpleadoManager
    {
        public List<Empleado> ObtenerTodos()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Empleado> lista = new List<Empleado>();

            try
            {
                datos.SetearConsulta("SELECT IDEMPLEADO,IDUSUARIO,NOMBRE,APELLIDO,DOCUMENTO,FECHA_NACIMIENTO,FECHA_INGRESO,IDDIRECCION,CALLE,NUM_DIRECCION,LOCALIDAD,COD_POSTAL,EMAIL,TELEFONO,IDROL,ESTADO FROM vw_ListaEmpleados\r\n");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Empleado empleado = new Empleado();

                    empleado.IdEmpleado = (long)datos.Lector["IDEMPLEADO"];
                    empleado.idusuario = (long)datos.Lector["IDUSUARIO"];
                    empleado.Nombre = (string)datos.Lector["NOMBRE"];
                    empleado.Apellido = (string)datos.Lector["APELLIDO"];
                    empleado.Documento = (string)datos.Lector["DOCUMENTO"];
                    empleado.FechaNac = (DateTime)datos.Lector["FECHA_NACIMIENTO"];
                    empleado.FechaIng = (DateTime)datos.Lector["FECHA_INGRESO"];
                    empleado.Direccion.IdDireccion = (long)datos.Lector["IDDIRECCION"];
                    empleado.Direccion.Calle = (string)datos.Lector["CALLE"];
                    empleado.Direccion.Numero = (string)datos.Lector["NUM_DIRECCION"];
                    empleado.Direccion.Localidad = (string)datos.Lector["LOCALIDAD"];
                    empleado.Direccion.CodPostal = (string)datos.Lector["COD_POSTAL"];
                    empleado.Email = !(datos.Lector["EMAIL"] is DBNull) ? (string)datos.Lector["EMAIL"] : string.Empty;
                    empleado.Telefono = !(datos.Lector["TELEFONO"] is DBNull) ? (string)datos.Lector["TELEFONO"] : string.Empty;
                    empleado.rol = (UserType)datos.Lector["IDROL"];
                    empleado.estado = (bool)datos.Lector["ESTADO"];

                    lista.Add(empleado);
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally 
            {
                datos.CerrarConeccion();
            }
        }



    }
}
