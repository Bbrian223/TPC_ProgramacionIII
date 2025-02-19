using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Manager
{
    public class VentasManager
    {
        public List<Venta> ObtenerTodas()
        { 
            AccesoDatos datos = new AccesoDatos();
            List<Venta> lista = new List<Venta>();

            try
            {
                datos.SetearConsulta("SELECT IDVENTA,IDPEDIDO,IDSALON,IDMESA,IDUSUARIO,FECHA,TOTAL FROM vw_ListaVentas");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta venta = new Venta();

                    venta.IdVenta = (long)datos.Lector["IDVENTA"];
                    venta.IdPedido = (long)datos.Lector["IDPEDIDO"];
                    venta.Mesa.IdSalon = (long)datos.Lector["IDSALON"];
                    venta.Mesa.IdMesa = (long)datos.Lector["IDMESA"];
                    venta.IdUsuario = (long)datos.Lector["IDUSUARIO"];
                    venta.Fecha_hora = (DateTime)datos.Lector["FECHA"];
                    venta.Total = (decimal)datos.Lector["TOTAL"];

                    lista.Add(venta);
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
