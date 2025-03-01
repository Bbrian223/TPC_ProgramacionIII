using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Manager;

namespace WebApplication1.ViewsStaff
{
    public partial class OrderStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarListaVentas();
        }

        protected void txtNroVentaFiltro_TextChanged(object sender, EventArgs e)
        {
            string idVenta = txtNroVentaFiltro.Text;
            txtFechaFiltro.Text = string.Empty;
            txtMesaFiltro.Text = string.Empty;
            ddlSalones.SelectedValue = "TODOS";

            try
            {
                if (idVenta != string.Empty)
                {
                    repeaterVentas.DataSource = obtenerListaPedidos().Where(vent => vent.IdVenta == long.Parse(idVenta)).ToList();
                    repeaterVentas.DataBind();
                }
                else
                {
                    CargarListaVentas();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void txtFechaFiltro_TextChanged(object sender, EventArgs e)
        {
            string fecha = txtNroVentaFiltro.Text;
            txtNroVentaFiltro.Text = string.Empty;
            txtMesaFiltro.Text = string.Empty;
            ddlSalones.SelectedValue = "TODOS";

            try
            {
                if (fecha != string.Empty)
                {
                    repeaterVentas.DataSource = obtenerListaPedidos().Where(vent => vent.Fecha_hora == DateTime.Parse(fecha)).ToList();
                    repeaterVentas.DataBind();
                }
                else
                {
                    CargarListaVentas();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void txtMesaFiltro_TextChanged(object sender, EventArgs e)
        {
            string mesa = txtMesaFiltro.Text;
            txtNroVentaFiltro.Text = string.Empty;
            txtFechaFiltro.Text = string.Empty;
            ddlSalones.SelectedValue = "TODOS";

            try
            {
                if (mesa != string.Empty)
                {
                    repeaterVentas.DataSource = obtenerListaPedidos().Where(vent => vent.Pedido.Mesa.IdMesa == long.Parse(mesa)).ToList();
                    repeaterVentas.DataBind();
                }
                else
                {
                    CargarListaVentas();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void ddlSalones_SelectedIndexChanged(object sender, EventArgs e)
        {
            string salon = ddlSalones.SelectedValue;
            txtNroVentaFiltro.Text = string.Empty;
            txtMesaFiltro.Text = string.Empty;
            txtFechaFiltro.Text = string.Empty;

            try
            {
                if (salon != "TODOS")
                {
                    repeaterVentas.DataSource = obtenerListaPedidos().Where(venta => venta.Pedido.Mesa.IdSalon == long.Parse(salon)).ToList();
                    repeaterVentas.DataBind();
                }
                else
                {
                    CargarListaVentas();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void btnVerVenta_Click(object sender, EventArgs e)
        {

        }

        //funciones

        private void CargarListaVentas()
        {
            VentasManager manager = new VentasManager();
            Empleado empl = (Empleado)Session["Empleado"];

            if (empl is null) return;

            try
            {
                repeaterVentas.DataSource = obtenerListaPedidos();
                repeaterVentas.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        public List<Venta> obtenerListaPedidos()
        {
            VentasManager manager = new VentasManager();
            Empleado empl = (Empleado)Session["Empleado"];
            List<Venta> lista = new List<Venta>();

            if (empl is null) return lista;

            try
            {
                lista = manager.ObtenerVentasPorEmpleado(empl.idusuario);
                
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}