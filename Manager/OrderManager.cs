using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using Microsoft.SqlServer.Server;

namespace Manager
{
    public class OrderManager
    {
        private PedidosManager manager = new PedidosManager();
        private Pedido pedido = new Pedido();
        public Pedido Pedido 
        { 
            get { return pedido; } 
        }
        public Mesa Mesa
        {
            get { return pedido.Mesa; }
        }

        public string ObtenerEstadoMesa(int idMesa) 
        {   
            MesasManager manager = new MesasManager();

            try
            {
                pedido.Mesa =  manager.ObtenerMesasPorID(idMesa);

                return pedido.Mesa.EstadoMesa;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ObtenerEstadoPedido() 
        {
            try
            {
                pedido = manager.ObtenerPedidoPorID((int)Mesa.IdMesa);
                pedido.ListaProd = manager.ObtenerProductosDelPedido((int)Mesa.IdMesa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal ObtenerMontoTotal() 
        {
            decimal monto = 0.0m;

            if (pedido.ListaProd.Count != 0) 
            {

                foreach (DetallePedido item in pedido.ListaProd)
                {
                    monto += item.Subtotal;
                }
            }

            return Math.Round(monto,1);
        }

        public void AgregarProdAlPedido(int idProd,int cantidad, int idUser) 
        {
            switch (Mesa.EstadoMesa)
            {
                case "DISPONIBLE":

                    GenerarPedidoNuevo(idUser);
                    AgregarProd(idProd, cantidad);
                    break;

                case "OCUPADA":

                    AgregarProd(idProd, cantidad);
                    break;

                default:
                    // cuando hay un error

                    break;
            }
        }

        public void EliminarProdAlPedido(long idDetallePedido)
        {
            try
            {
                manager.EliminarProd(idDetallePedido);
                ObtenerEstadoPedido();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CerrarPedido(long idUser) 
        {
            try
            {
                manager.CerrarPedido(pedido.IdPedido);
                manager.AgregarVenta(pedido.IdPedido,idUser,ObtenerMontoTotal());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void HabilitarMesa()
        {
            try
            {
                manager.ActualizarEstadoMesa(Mesa.IdMesa, "DISPONIBLE");
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PRIVADAS

        private void GenerarPedidoNuevo(int idUser)
        {
            try
            {   
                manager.GenerarPedido(idUser,(int)Mesa.IdMesa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AgregarProd(int idProd,int cantidad) 
        {
            try
            {
                ObtenerEstadoPedido();
                manager.AgregarProd(idProd,(int)Pedido.IdPedido,cantidad);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
