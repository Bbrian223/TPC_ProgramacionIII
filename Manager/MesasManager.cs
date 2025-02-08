using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Manager
{
    public class MesasManager
    {
        public List<Mesa> ObtenerTodos()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Mesa> lista = new List<Mesa>();

            try
            {
                datos.SetearConsulta("SELECT IDSALON,IDMESA,ESTADO,HABILITADA from Mesas ORDER BY IDMESA");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesa mesa = new Mesa();

                    mesa.IdSalon = (long)datos.Lector["IDSALON"];
                    mesa.IdMesa = (long)datos.Lector["IDMESA"];
                    mesa.EstadoMesa = (string)datos.Lector["ESTADO"];
                    mesa.Habilitado = (bool)datos.Lector["HABILITADA"];

                    lista.Add(mesa);
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

        public List<Mesa> ObtenerMesasPorSalon(int idSalon)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Mesa> lista = new List<Mesa>();

            try
            {
                datos.SetearConsulta("SELECT IDSALON,IDMESA,ESTADO,HABILITADA from Mesas WHERE IDSALON = @IDSALON ORDER BY IDMESA");
                datos.SetearParametro("@IDSALON",idSalon);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesa mesa = new Mesa();

                    mesa.IdSalon = (long)datos.Lector["IDSALON"];
                    mesa.IdMesa = (long)datos.Lector["IDMESA"];
                    mesa.EstadoMesa = (string)datos.Lector["ESTADO"];
                    mesa.Habilitado = (bool)datos.Lector["HABILITADA"];

                    lista.Add(mesa);
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
