using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dominio;

namespace Manager
{
    public class UsuarioManager
    {
        public Usuario ObtenerUsuario(string user, string pass)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario aux = new Usuario();
            try
            {
                datos.SetearConsulta("SELECT IDUSUARIO,NOMBRE,CONTRASENIA,IDROL FROM Usuarios WHERE NOMBRE = @NOM AND CONTRASENIA = @CONTRA AND ESTADO = 1");
                datos.SetearParametro("@NOM", user);
                datos.SetearParametro("@CONTRA", pass);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    aux.idusuario = (long)datos.Lector["IDUSUARIO"];
                    aux.nombreusuario = (string)datos.Lector["NOMBRE"];
                    aux.clave = (string)datos.Lector["CONTRASENIA"];
                    aux.rol = (UserType)datos.Lector["IDROL"];
                    aux.estado = true;
                }

                return aux;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public void CambiarClave(Usuario user, string NuevaPass) 
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("EXEC sp_CambiarPass @IDUSUARIO,@ACTUALPASS,@NUEVAPASS");
                datos.SetearParametro("@IDUSUARIO", user.idusuario);
                datos.SetearParametro("@ACTUALPASS", user.clave);
                datos.SetearParametro("@NUEVAPASS", NuevaPass);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConeccion();
            }
        }

        public void BajaUsuario(int id) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET ESTADO = 0 WHERE IDUSUARIO = @ID");
                datos.SetearParametro("@ID", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                datos.CerrarConeccion();
            }
        }

    }
}
