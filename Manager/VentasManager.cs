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

        public List<decimal> ObtenerVentaSemanal()
        {
            AccesoDatos datos = new AccesoDatos();
            List<decimal> lista = new List<decimal>();

            try
            {
                datos.SetearConsulta("SELECT DIA,TOTAL FROM vw_VentaSemana\r\nORDER BY CASE \r\n    WHEN DIA = 'Sunday' THEN 1\r\n    WHEN DIA = 'Monday' THEN 2\r\n    WHEN DIA = 'Tuesday' THEN 3\r\n    WHEN DIA = 'Wednesday' THEN 4\r\n    WHEN DIA = 'Thursday' THEN 5\r\n    WHEN DIA = 'Friday' THEN 6\r\n    WHEN DIA = 'Saturday' THEN 7\r\nEND;");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    decimal valor = (decimal)datos.Lector["TOTAL"];

                    lista.Add(valor);
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

        public decimal ObtenerRecaudacionDia()
        {
            AccesoDatos datos = new AccesoDatos();
            decimal total = 0;

            try
            {
                datos.SetearConsulta("SELECT TOTAL FROM vs_TotalRecaudadoDia");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    total = (datos.Lector["TOTAL"] is DBNull) ? 0 : Convert.ToDecimal(datos.Lector["TOTAL"]);
                }

                return total;
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
