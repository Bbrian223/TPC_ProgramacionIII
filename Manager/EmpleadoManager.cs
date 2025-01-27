using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
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

        public void Agregar(Empleado empl) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_AgregarEmpleado @Nombre,@Apellido,@Documento,@FechaNac,@Calle,@NumDir,@Localidad,@CodPos,@Email,@Telefono,@Rol");
                datos.SetearParametro("@Nombre", empl.Nombre);
                datos.SetearParametro("@Apellido", empl.Apellido);
                datos.SetearParametro("@Documento", empl.Documento);
                datos.SetearParametro("@FechaNac", empl.FechaNac);
                datos.SetearParametro("@Calle", empl.Direccion.Calle);
                datos.SetearParametro("@NumDir", empl.Direccion.Numero);
                datos.SetearParametro("@Localidad", empl.Direccion.Localidad);
                datos.SetearParametro("@CodPos", empl.Direccion.CodPostal);
                datos.SetearParametro("@Email", empl.Email);
                datos.SetearParametro("@Telefono", empl.Telefono);
                datos.SetearParametro("@Rol", (int)empl.rol);
                datos.ejecutarAccion();

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

        public void Baja(int id) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET ESTADO = 0 WHERE IDUSUARIO = @Id");
                datos.SetearParametro("@Id", id);
                datos.ejecutarAccion();
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
