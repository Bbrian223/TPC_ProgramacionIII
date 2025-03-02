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
                    repeaterVentas.DataSource = manager.ObtenerTodas().Where(vent => vent.Pedido.Mesa.IdMesa == long.Parse(mesa)).ToList();
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
                    repeaterVentas.DataSource = manager.ObtenerTodas().Where(venta => venta.Pedido.Mesa.IdSalon == long.Parse(salon)).ToList();
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
            Button btn = (Button)sender;
            string idVenta = btn.CommandArgument;
            lblModalNroVenta.Text = idVenta;

            try
            {
                CargarDatosModal(long.Parse(idVenta));
                MostrarModal();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
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

        private void MostrarModal()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Visualizar", "var modal = new bootstrap.Modal(document.getElementById('modalVer')); modal.show();", true);
        }

        public void CargarDatosModal(long idVenta)
        {
            VentasManager manager = new VentasManager();

            try
            {
                Venta venta = manager.ObtenerVentaPorId(idVenta);
                venta.Pedido.ListaProd = manager.ObtenerProductos(idVenta);

                txtNroVenta.Text = venta.IdVenta.ToString();
                txtNroSalon.Text = venta.Pedido.Mesa.IdSalon.ToString();
                txtNroMesa.Text = venta.Pedido.Mesa.IdMesa.ToString();
                txtFechaVenta.Text = venta.Fecha_hora.ToString("yyyy-MM-dd");
                txtEstadoPedido.Text = venta.Pedido.Estado;
                txtPrecioTotal.Text = venta.Total.ToString("0.0");

                gViewProductos.DataSource = venta.Pedido.ListaProd;
                gViewProductos.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}