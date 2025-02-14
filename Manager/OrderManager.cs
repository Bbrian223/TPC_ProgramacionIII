using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dominio;

namespace Manager
{
    public class OrderManager
    {
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
            PedidosManager manager = new PedidosManager();

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


        // PRIVADAS

        private void GenerarPedidoNuevo(int idUser)
        {
            PedidosManager manager = new PedidosManager();

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
            PedidosManager manager = new PedidosManager();

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
