using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using Dominio;
using Manager;
using Microsoft.Win32;

namespace WebApplication1.ViewsManagment
{
    public partial class ProdextraSetting : System.Web.UI.Page
    {
        public bool EditarAdicional;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatonPantalla();
            }
        }

        protected void txtNombreFiltro_TextChanged(object sender, EventArgs e)
        {
            string nombre = txtNombreFiltro.Text.Trim();

            try
            {
                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    repeaterAdicionales.DataSource = ObtenerLista().Where(prod => prod.Nombre.ToLower() == nombre.ToLower()).ToList();
                    repeaterAdicionales.DataBind();
                }
                else
                {
                    CargarDatonPantalla();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idCategoria = ddlCategorias.SelectedValue;

            try
            {
                if (idCategoria != "0")
                {
                    repeaterAdicionales.DataSource = ObtenerLista().Where(prod => prod.Categoria.IdCategoria == int.Parse(idCategoria)).ToList();
                    repeaterAdicionales.DataBind();
                }
                else
                {
                    CargarDatonPantalla();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void btnAgregarAdicional_Click(object sender, EventArgs e)
        {
            LimpiarEntradas();
            CargarModalAgregar();
        }

        protected void btnVerAdicional_Click(object sender, EventArgs e)
        {
            ProductoManager manager = new ProductoManager();
            Button btn = (Button)sender;
            string id = btn.CommandArgument;
            EditarAdicional = false;

            lblModalNumAdicional.Text = id;

            try
            {
                Producto prod = manager.Obtener(long.Parse(id));
                txtNombre.Text = prod.Nombre;
                txtPrecio.Text = prod.Precio.ToString("0.0");
                txtBxDescripcion.Text = prod.Descripcion;
                ddlCategoriasModalVer.SelectedValue = prod.Categoria.IdCategoria.ToString();

                HabilitarEdicion(false);
                CargarModalDetalles();

                Session["EditarAdicional"] = EditarAdicional;
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void btnBajaAdicional_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idProd = btn.CommandArgument;

            lblIdProdBaja.Text = idProd;

            try
            {
                lblModalBajaAdicional.Text = ObtenerNombreProd(long.Parse(idProd));
                CargarModalEliminar();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void btnConfirmarBaja_Click(object sender, EventArgs e)
        {
            ProductoManager manager = new ProductoManager();
            string id = lblIdProdBaja.Text;

            try
            {
                manager.Baja(long.Parse(id));
                CargarDatonPantalla();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void btnEditarAdicional_Click(object sender, EventArgs e)
        {
            EditarAdicional = true;
            HabilitarEdicion(true);
            CargarModalDetalles();

            Session["EditarAdicional"] = EditarAdicional;
        }

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            ProductoManager manager = new ProductoManager();

            if (!ValidarCampos()) 
            {
                btnEditarAdicional_Click(sender, e);
                MsgError();
                return;
            }

            try
            {
                Producto prod = new Producto();

                prod.IdProducto = long.Parse(lblModalNumAdicional.Text);
                prod.Nombre = txtNombre.Text;
                prod.Precio = decimal.Parse(txtPrecio.Text);
                prod.Descripcion = txtBxDescripcion.Text;
                prod.Categoria.IdCategoria = long.Parse(ddlCategoriasModalVer.SelectedValue);

                manager.Editar(prod); 

                HabilitarEdicion(false);
                CargarDatonPantalla();
                CargarModalDetalles();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void btnHabilitarAd_Click(object sender, EventArgs e)
        {
            ProductoManager manager = new ProductoManager();
            Button btn = (Button)sender;
            string id = btn.CommandArgument;

            try
            {
                manager.Alta(long.Parse(id));
                CargarDatonPantalla();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void btnGuardarNuevoAdicional_Click(object sender, EventArgs e)
        {
            ProductoManager manager = new ProductoManager();
            string idCategoria = ddlModalAgregar.SelectedValue;

            if (!ValidarCamposModalAgregar())
            {
                MsgErrorModalAgregar();
                CargarModalAgregar();
                return;
            }


            try
            {
                Producto prod = new Producto();

                prod.Categoria.IdCategoria = long.Parse(idCategoria);
                prod.Nombre = txtNombreModalAgregar.Text;
                prod.Precio = decimal.Parse(txtPrecioModalAgregar.Text);
                prod.Descripcion = txtDescripcionModalAgregar.Text;

                manager.AgregarProdExtra(prod);

                CargarDatonPantalla();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        //funciones 

        public void CargarDatonPantalla() 
        {
            EditarAdicional = false;

            if (Session["EditarAdicional"] is null)
                Session.Add("EditarAdicional",EditarAdicional);

            ddlCategorias.SelectedValue = "0";

            try
            {
                repeaterAdicionales.DataSource = ObtenerLista();
                repeaterAdicionales.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }

            Session["EditarAdicional"] = EditarAdicional;
        }

        public List<Producto> ObtenerLista()
        {
            ProductoManager manager = new ProductoManager();

            try
            {
                return manager.ObtenerAdicionales();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CargarModalDetalles()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "ver", "var modal = new bootstrap.Modal(document.getElementById('modalEditar')); modal.show();", true);
        }

        public void CargarModalEliminar()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "eliminar", "var modal = new bootstrap.Modal(document.getElementById('modalEliminar')); modal.show();", true);
        }

        public void CargarModalAgregar()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "agregar", "var modal = new bootstrap.Modal(document.getElementById('modalNuevoAd')); modal.show();", true);
        }

        public string ObtenerNombreProd(long idprod)
        {
            try
            {
                Producto prod = ObtenerLista().Find(aux => aux.IdProducto == idprod);
                return prod.Nombre;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void HabilitarEdicion(bool estado) 
        {
            txtNombre.Enabled = estado;
            txtPrecio.Enabled = estado;
            txtBxDescripcion.Enabled = estado;
            panelMsgLbl.Visible = false;
        }

        private void EliminarAlarmas()
        {
            txtNombre.CssClass = txtNombre.CssClass.Replace("error", "").Trim();
            txtPrecio.CssClass = txtPrecio.CssClass.Replace("error", "").Trim();
            ddlCategoriasModalVer.CssClass = ddlCategorias.CssClass.Replace("error", "").Trim();

            //eliminar alarmas modal agregar
            txtNombreModalAgregar.CssClass = txtNombreModalAgregar.CssClass.Replace("error", "").Trim();
            txtPrecioModalAgregar.CssClass = txtPrecioModalAgregar.CssClass.Replace("error", "").Trim();
            ddlModalAgregar.CssClass = ddlModalAgregar.CssClass.Replace("error", "").Trim();
        }

        private void MsgError()
        {
            panelMsgLbl.Visible = true;
            lblErrores.Text = "Complete los campos correspondientes...";
            lblErrores.Visible = true;
        }

        public bool ValidarCampos()
        {
            bool estaodo = true;

            EliminarAlarmas();

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                txtNombre.CssClass += " error";
                estaodo = false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                txtPrecio.CssClass += " error";
                estaodo = false;
            }

            if (ddlCategoriasModalVer.SelectedValue == "0")
            {
                ddlCategoriasModalVer.CssClass += " error";
                estaodo = false;
            }


            return estaodo;
        }

        public bool ValidarCamposModalAgregar()
        {
            bool estaodo = true;

            EliminarAlarmas();

            if (string.IsNullOrWhiteSpace(txtNombreModalAgregar.Text))
            {
                txtNombreModalAgregar.CssClass += " error";
                estaodo = false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecioModalAgregar.Text))
            {
                txtPrecioModalAgregar.CssClass += " error";
                estaodo = false;
            }

            if (ddlModalAgregar.SelectedValue == "0")
            {
                ddlModalAgregar.CssClass += " error";
                estaodo = false;
            }

            return estaodo;
        }

        public void LimpiarEntradas()
        {
            txtNombreModalAgregar.Text = string.Empty;
            txtPrecioModalAgregar.Text = string.Empty;
            txtDescripcionModalAgregar.Text = string.Empty;
            ddlModalAgregar.SelectedValue = "0";
            panleModalAgregar.Visible = false;
        }

        private void MsgErrorModalAgregar()
        {
            panleModalAgregar.Visible = true;
            lblModalAgregar.Text = "Complete los campos correspondientes...";
            lblModalAgregar.Visible = true;
        }
    }
}