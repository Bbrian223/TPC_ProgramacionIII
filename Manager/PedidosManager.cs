using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Dominio;

namespace Manager
{
    public class PedidosManager
    {
        public List<DetallePedido> ObtenerProductosDelPedido(int idMesa) 
        {
            AccesoDatos datos = new AccesoDatos();
            List<DetallePedido> lista = new List<DetallePedido>();

            try
            {
                datos.SetearConsulta("SELECT IDDETALLE,IDCATEGORIA,CATEGORIA,NOMBRE,PRECIO,STOCK,DESCRIPCION,ARCHNOMB,CANTIDAD,SUBTOTAL FROM fn_ObtenerProductos(@IDMESA)");
                datos.SetearParametro("@IDMESA",idMesa);
                datos.EjecutarLectura();

                while (datos.Lector.Read()) 
                { 
                    DetallePedido pedidos = new DetallePedido();

                    pedidos.IdDetallePedido = datos.Lector["IDDETALLE"] is DBNull ? 0 : (long)datos.Lector["IDDETALLE"];
                    pedidos.Producto.Categoria.IdCategoria = (long)datos.Lector["IDCATEGORIA"];
                    pedidos.Producto.Categoria.Nombre = (string)datos.Lector["CATEGORIA"];
                    pedidos.Producto.Nombre = (string)datos.Lector["NOMBRE"];
                    pedidos.Producto.Precio = (decimal)datos.Lector["PRECIO"];
                    pedidos.Producto.stock = (int)datos.Lector["STOCK"];
                    pedidos.Producto.Descripcion = (string)datos.Lector["DESCRIPCION"];
                    pedidos.Producto.Imagen.NombreArch = (string)datos.Lector["ARCHNOMB"];
                    pedidos.Cantidad = (int)datos.Lector["CANTIDAD"];
                    pedidos.Subtotal = (decimal)datos.Lector["SUBTOTAL"];

                    lista.Add(pedidos);
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

        public Pedido ObtenerPedidoPorID(int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();
            Pedido ped = new Pedido();

            try
            {
                datos.SetearConsulta("SELECT IDPEDIDO,IDMESA,IDSALON,ESTADOMESA,HABILITADA,ESTADOPEDIDO FROM fn_obtenerPedido(@IDMESA)");
                datos.SetearParametro("@IDMESA", idMesa);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    ped.IdPedido = (long)datos.Lector["IDPEDIDO"];
                    ped.Mesa.IdMesa = (long)datos.Lector["IDMESA"];
                    ped.Mesa.IdSalon = (long)datos.Lector["IDSALON"];
                    ped.Mesa.EstadoMesa = (string)datos.Lector["ESTADOMESA"];
                    ped.Mesa.Habilitado = (bool)datos.Lector["HABILITADA"];
                    ped.Estado = (string)datos.Lector["ESTADOPEDIDO"];
                }

                return ped;
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

        public void GenerarPedido(int idUser, int idMesa)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_GenerarPedido @IDUSER,@IDMESA");
                datos.SetearParametro("@IDUSER", idUser);
                datos.SetearParametro("@IDMESA", idMesa);
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

        public void AgregarProd(int idProd,int idPedido, int cantidad) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_AgregarProdAlPedido @IDPRODUCTO,@IDPEDIDO,@CANTIDAD");
                datos.SetearParametro("@IDPRODUCTO",idProd);
                datos.SetearParametro("@IDPEDIDO",idPedido);
                datos.SetearParametro("@CANTIDAD",cantidad);
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
