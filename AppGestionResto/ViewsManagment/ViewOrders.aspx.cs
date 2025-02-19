using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Manager;

namespace WebApplication1.ViewsManagment
{
    public partial class ViewOrders : System.Web.UI.Page
    {
        public List<Pedido> listaPedidos = new List<Pedido>();

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarLista();
        }

        protected void txtIdPedidoFiltro_TextChanged(object sender, EventArgs e)
        {
            PedidosManager manager = new PedidosManager();
            string id = txtIdPedidoFiltro.Text;
            txtMesaFiltro.Text = string.Empty;

            try
            {
                if (id != string.Empty)
                {
                    repeaterPedidos.DataSource = manager.ObtenerTodosPedidos().Where(ped => ped.IdPedido == long.Parse(id)).ToList();
                    repeaterPedidos.DataBind();
                }
                else 
                {
                    CargarLista();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "');</Script>");
            }
        }

        protected void ddlSalones_SelectedIndexChanged(object sender, EventArgs e)
        {
            PedidosManager manager = new PedidosManager();
            string salon = ddlSalones.SelectedValue;
            ddlEstadoPedido.SelectedValue = "TODOS";

            try
            {
                if (salon != "TODOS")
                {
                    repeaterPedidos.DataSource = manager.ObtenerTodosPedidos().Where(ped => ped.Mesa.IdSalon == long.Parse(salon)).ToList();
                    repeaterPedidos.DataBind();
                }
                else
                {
                    CargarLista();
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        protected void txtMesaFiltro_TextChanged(object sender, EventArgs e)
        {
            PedidosManager manager = new PedidosManager();
            string mesa = txtMesaFiltro.Text;
            txtIdPedidoFiltro.Text = string.Empty;
            try
            {
                if (mesa != string.Empty)
                {
                    repeaterPedidos.DataSource = manager.ObtenerTodosPedidos().Where(ped => ped.Mesa.IdMesa == long.Parse(mesa)).ToList();
                    repeaterPedidos.DataBind();
                }
                else
                {
                    CargarLista();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "');</Script>");
            }
        }

        protected void ddlEstadoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            PedidosManager manager = new PedidosManager();
            string estado = ddlEstadoPedido.SelectedValue;
            ddlSalones.SelectedValue = "TODOS";

            try
            {
                if (estado == "TODOS")
                {
                    CargarLista();
                }
                else
                {
                    repeaterPedidos.DataSource = manager.ObtenerTodosPedidos().Where(ped => ped.Estado == estado).ToList();
                    repeaterPedidos.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

        }

        protected void btnVerPedido_Click(object sender, EventArgs e)
        {

        }

        protected void btnAceptarModal_Click(object sender, EventArgs e)
        {

        }

        
        //funciones

        public void CargarLista() 
        {
            PedidosManager manager = new PedidosManager();

            try
            {
                listaPedidos = manager.ObtenerTodosPedidos();
                repeaterPedidos.DataSource = listaPedidos;
                repeaterPedidos.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }


    }
}