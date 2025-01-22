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
        public static UserType ObtenerNivelAcceso(string user, string pass)
        {
            AccesoDatos datos = new AccesoDatos();
            int id_usuario = 0;
            try
            {
                datos.SetearConsulta("SELECT IDUSUARIO FROM Usuarios WHERE NOMBRE = @NOM AND CONTRASENIA = @CONTRA AND ESTADO = 1");
                datos.SetearParametro("@NOM", user);
                datos.SetearParametro("@CONTRA", pass);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    id_usuario = (int)datos.Lector["IDUSUARIO"];
                }

                return (UserType)id_usuario;
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

        public void CambiarClave(string pass,int id) 
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET CONTRASENIA = @CONTRA WHERE IDUSUARIO = @ID");
                datos.SetearParametro("@CONTRA", pass);
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
