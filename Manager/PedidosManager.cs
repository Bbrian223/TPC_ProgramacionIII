﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Dominio;

namespace Manager
{
    public class PedidosManager
    {
        public List<Pedido> ObtenerTodosPedidos() 
        {
            AccesoDatos datos = new AccesoDatos();
            List<Pedido> lista = new List<Pedido>();

            try
            {
                datos.SetearConsulta("SELECT IDPEDIDO,IDSALON,IDMESA,IDUSUARIO,ESTADO FROM vw_ListaPedidos");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido ped = new Pedido();

                    ped.IdPedido = (long)datos.Lector["IDPEDIDO"];
                    ped.Mesa.IdSalon = (long)datos.Lector["IDSALON"];
                    ped.Mesa.IdMesa = (long)datos.Lector["IDMESA"];
                    ped.Empleado.idusuario = (long)datos.Lector["IDUSUARIO"];
                    ped.Estado = (string)datos.Lector["ESTADO"];

                    lista.Add(ped);
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
        
        public List<DetallePedido> ObtenerProductosDelPedido(int idMesa) 
        {
            AccesoDatos datos = new AccesoDatos();
            List<DetallePedido> lista = new List<DetallePedido>();

            try
            {
                datos.SetearConsulta("SELECT IDDETALLE,IDCATEGORIA,CATEGORIA,NOMBRE,PRECIO,STOCK,DESCRIPCION,ARCHNOMB,CANTIDAD,SUBTOTAL,GUARNICION FROM fn_ObtenerProductos(@IDMESA) ORDER BY IDDETALLE");
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
                    pedidos.Producto.Guarnicion = (bool)datos.Lector["GUARNICION"];
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

        public void CerrarPedido(long idPedido) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_CompletarPedido @IDPEDIDO");
                datos.SetearParametro("@IDPEDIDO",idPedido);
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

        public void AgregarVenta(long idPedido, long idUsuario, decimal total) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_AgregarVenta @IDPEDIDO,@IDUSUARIO,@TOTAL");
                datos.SetearParametro("@IDPEDIDO",idPedido);
                datos.SetearParametro("@IDUSUARIO",idUsuario);
                datos.SetearParametro("@TOTAL",total);
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

        public void AgregarProd(long idPedido, long idUser, int cantidad, string aclaraciones) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_AgregarProdAlPedido @IDPEDIDO,@IDUSUARIO,@CANTIDAD,@ACLARACIONES");
                datos.SetearParametro("@IDPEDIDO",idPedido);
                datos.SetearParametro("@IDUSUARIO", idUser); 
                datos.SetearParametro("@CANTIDAD",cantidad);
                datos.SetearParametro("@ACLARACIONES", aclaraciones);
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

        public void EliminarProd(long idDetalle) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_EliminarProdDePedido @IDDETALLE");
                datos.SetearParametro("@IDDETALLE", idDetalle);
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

        public void ActualizarEstadoMesa(long idMesa, string estado) 
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE Mesas SET ESTADO = @ESTADO WHERE IDMESA = @IDMESA");
                datos.SetearParametro("@ESTADO", estado);
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

        public void CancelarPedido(long idPedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("EXEC sp_CancelarPedido @IDPEDIDO");
                datos.SetearParametro("@IDPEDIDO", idPedido);
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

        public List<int> ObtenerCantidadMesasEstado()
        {
            AccesoDatos datos = new AccesoDatos();
            List<int> lista = new List<int>();

            try
            {
                datos.SetearConsulta("SELECT CANTIDAD FROM vw_PedidosPorEstado\r\nORDER BY \r\nCASE ESTADO \r\nWHEN 'COMPLETADO' THEN 1\r\nWHEN 'EN PROCESO' THEN 2\r\nWHEN 'CANCELADO' THEN 3\r\nEND;\r\n");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    int dato = (int)datos.Lector["CANTIDAD"];

                    lista.Add(dato);
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
