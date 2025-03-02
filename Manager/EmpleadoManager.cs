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
                datos.SetearConsulta("SELECT IDEMPLEADO,IDUSUARIO,NOMBRE,APELLIDO,DOCUMENTO,FECHA_NACIMIENTO,FECHA_INGRESO,IDDIRECCION,CALLE,NUM_DIRECCION,LOCALIDAD,COD_POSTAL,EMAIL,TELEFONO,IDROL,ESTADO,ARCHIMAGEN FROM vw_ListaEmpleados");
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
                    empleado.Imagen.NombreArch = (string)datos.Lector["ARCHIMAGEN"];

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

        public List<Empleado> ObtenerListaEmplPorRol(int idRol)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Empleado> lista = new List<Empleado>();

            try
            {
                datos.SetearConsulta("SELECT IDEMPLEADO,IDUSUARIO,NOMBRE,APELLIDO,DOCUMENTO,FECHA_NACIMIENTO,FECHA_INGRESO,IDDIRECCION,CALLE,NUM_DIRECCION,LOCALIDAD,COD_POSTAL,EMAIL,TELEFONO,IDROL,ESTADO,ARCHIMAGEN FROM vw_ListaEmpleados WHERE IDROL = @IDROL");
                datos.SetearParametro("@IDROL",idRol);
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
                    empleado.Imagen.NombreArch = (string)datos.Lector["ARCHIMAGEN"];

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

        public List<Empleado> ObtenerEmpleadosPorMesa(long mesa) 
        {
            AccesoDatos datos = new AccesoDatos();
            List<Empleado> lista = new List<Empleado>();    
            
            try
            {
                datos.SetearConsulta("EXEC sp_ListaEmpleadosPorMesa @IDMESA");
                datos.SetearParametro("@IDMESA", mesa);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Empleado empl = new Empleado();

                    empl.IdEmpleado = (long)datos.Lector["IDEMPLEADO"];
                    empl.idusuario = (long)datos.Lector["IDUSUARIO"];
                    empl.Nombre = (string)datos.Lector["NOMBRE"];
                    empl.Apellido = (string)datos.Lector["APELLIDO"];
                    empl.Documento = (string)datos.Lector["DOCUMENTO"];
                    empl.FechaNac = (DateTime)datos.Lector["FECHA_NACIMIENTO"];
                    empl.FechaIng = (DateTime)datos.Lector["FECHA_INGRESO"];
                    empl.rol = (UserType)datos.Lector["IDROL"];
                    empl.estado = (bool)datos.Lector["ESTADO"];

                    lista.Add(empl);
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

        public List<Empleado> ObtenerCantidadPedidosPorEmpleado()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Empleado> lista = new List<Empleado>();

            try
            {
                datos.SetearConsulta("SELECT IDEMPLEADO,NOMBRE,APELLIDO,CANTIDAD FROM vw_ListaPedidosPorEmpleado ORDER BY CANTIDAD DESC");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Empleado emp = new Empleado();

                    emp.IdEmpleado = (long)datos.Lector["IDEMPLEADO"];
                    emp.Nombre = (string)datos.Lector["NOMBRE"];
                    emp.Apellido = (string)datos.Lector["APELLIDO"];
                    emp.PedidosGenerados = (int)datos.Lector["CANTIDAD"];

                    lista.Add(emp);
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

        public Empleado ObtenerPorId(long idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            Empleado empleado = new Empleado();

            try
            {
                datos.SetearConsulta("SELECT IDEMPLEADO,IDUSUARIO,NOMBRE,APELLIDO,DOCUMENTO,FECHA_NACIMIENTO,FECHA_INGRESO,IDDIRECCION,CALLE,NUM_DIRECCION,LOCALIDAD,COD_POSTAL,EMAIL,TELEFONO,IDROL,ESTADO,ARCHIMAGEN FROM vw_ListaEmpleados WHERE IDUSUARIO = @IDUSUARIO");
                datos.SetearParametro("@IDUSUARIO",idUsuario);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
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
                    empleado.Imagen.NombreArch = (string)datos.Lector["ARCHIMAGEN"];
                }

                return empleado;
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
                datos.SetearConsulta("EXEC sp_AgregarEmpleado @Nombre,@Apellido,@Documento,@FechaNac,@Calle,@NumDir,@Localidad,@CodPos,@Email,@Telefono,@Rol,@User");
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
                datos.SetearParametro("@User", (string)empl.nombreusuario);
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

        public void Editar(Empleado empl)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_EditarUsuarios @IDUSUARIO,@CALLE,@NUMDIR,@LOCALIDAD,@CODPOSTAL,@EMAIL,@TELEFONO,@IDROL");
                datos.SetearParametro("@IDUSUARIO", empl.idusuario);
                datos.SetearParametro("@CALLE", empl.Direccion.Calle);
                datos.SetearParametro("@NUMDIR", empl.Direccion.Numero);
                datos.SetearParametro("@LOCALIDAD", empl.Direccion.Localidad);
                datos.SetearParametro("@CODPOSTAL", empl.Direccion.CodPostal);
                datos.SetearParametro("@EMAIL", empl.Email);
                datos.SetearParametro("@TELEFONO", empl.Telefono);
                datos.SetearParametro("@IDROL", (int)empl.rol);
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

        public long UltimoId()
        {
            AccesoDatos datos = new AccesoDatos();
            long id = -1;

            try
            {
                datos.SetearConsulta("SELECT MAX(IDUSUARIO) AS ID FROM Usuarios");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    id = (long)datos.Lector["ID"];
                }

                return id;
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
