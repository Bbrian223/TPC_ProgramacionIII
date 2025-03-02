using System;
using System.Collections.Generic;
using System.Globalization;
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
            string fecha = txtFechaFiltro.Text;
            txtNroVentaFiltro.Text = string.Empty;
            txtMesaFiltro.Text = string.Empty;
            ddlSalones.SelectedValue = "TODOS";

            try
            {
                if (fecha != string.Empty)
                {
                    if (DateTime.TryParseExact(fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime aux))
                    {
                        repeaterVentas.DataSource = obtenerListaPedidos()
                            .Where(vent => vent.Fecha_hora.Date == aux.Date) // Comparación solo por fecha
                            .ToList();
                        repeaterVentas.DataBind();
                    }
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

        //funciones

        private void MostrarModal()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Visualizar", "var modal = new bootstrap.Modal(document.getElementById('modalVer')); modal.show();", true);
        }

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