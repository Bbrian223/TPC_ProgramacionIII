using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manager;

namespace WebApplication1.ViewsManagment
{
    public partial class ViewSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarLista();
        }

        protected void txtNroVenta_TextChanged(object sender, EventArgs e)
        {
            VentasManager manager = new VentasManager();  
            string idVenta = txtNroVentaFiltro.Text;
            txtFechaFiltro.Text = string.Empty;
            txtMesaFiltro.Text = string.Empty;
            ddlSalones.SelectedValue = "TODOS";

            try
            {
                if (idVenta != string.Empty)
                {
                    repeaterVentas.DataSource = manager.ObtenerTodas().Where(vent => vent.IdVenta == long.Parse(idVenta)).ToList();
                    repeaterVentas.DataBind();
                }
                else
                {
                    CargarLista();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void txtFechaFiltro_TextChanged(object sender, EventArgs e)
        {
            VentasManager manager = new VentasManager();  
            string fecha = txtNroVentaFiltro.Text;
            txtNroVentaFiltro.Text = string.Empty;
            txtMesaFiltro.Text = string.Empty;
            ddlSalones.SelectedValue = "TODOS";

            try
            {
                if (fecha != string.Empty)
                {
                    repeaterVentas.DataSource = manager.ObtenerTodas().Where(vent => vent.Fecha_hora == DateTime.Parse(fecha)).ToList();
                    repeaterVentas.DataBind();
                }
                else
                {
                    CargarLista();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void txtMesaFiltro_TextChanged(object sender, EventArgs e)
        {
            VentasManager manager = new VentasManager();
            string mesa = txtMesaFiltro.Text;
            txtNroVentaFiltro.Text = string.Empty;
            txtFechaFiltro.Text = string.Empty;
            ddlSalones.SelectedValue = "TODOS";

            try
            {
                if (mesa != string.Empty)
                {
                    repeaterVentas.DataSource = manager.ObtenerTodas().Where(vent => vent.Mesa.IdMesa == long.Parse(mesa)).ToList();
                    repeaterVentas.DataBind();
                }
                else
                {
                    CargarLista();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void ddlSalones_SelectedIndexChanged(object sender, EventArgs e)
        {
            VentasManager manager = new VentasManager();
            string salon = ddlSalones.SelectedValue;
            txtNroVentaFiltro.Text = string.Empty;
            txtMesaFiltro.Text = string.Empty;
            txtFechaFiltro.Text = string.Empty;

            try
            {
                if (salon != "TODOS")
                {
                    repeaterVentas.DataSource = manager.ObtenerTodas().Where(venta => venta.Mesa.IdSalon == long.Parse(salon)).ToList();
                    repeaterVentas.DataBind();
                }
                else
                {
                    CargarLista();
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

        //Funciones

        public void CargarLista()
        {
            VentasManager manager = new VentasManager();

            try
            {
                repeaterVentas.DataSource = manager.ObtenerTodas();
                repeaterVentas.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message +"')</Script>");
            }
        }

    }
}