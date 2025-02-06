using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Manager;
using Microsoft.Ajax.Utilities;

namespace WebApplication1.ViewCommon
{
    public partial class AddProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Seguridad.NivelAcceso != UserType.Gerente)
            {
                Response.Redirect("~/ViewCommon/HomeStaff.aspx",false);
            }

            if (!IsPostBack)
            {
                BorrarDatos();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ViewCommon/Products.aspx",false);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ProductoManager manager = new ProductoManager();
            Producto nuevoProd = new Producto();

            if (!ValidarCampos())
            {
                MsgError();
                return;
            }

            try
            {
                //obtener datos

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

            BorrarDatos();
            MsgSucces();
        }


        //funciones

        public void BorrarDatos() 
        {
            txtNombre.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtTipoCafe.Text = string.Empty;
            txtTamano.Text = string.Empty;
            txtPorciones.Text = string.Empty;
            txtGuarnicion.Text = string.Empty;
            chkAzucar.Checked = false;
            chkGluten.Checked = false;
            txtVolumen.Text = string.Empty;
            chkAlcohol.Checked = false;
            ddlCategorias.SelectedValue = "0";
            lblErrores.Visible = false;
        }

        private bool ValidarCampos()
        {
            bool esValido = true;
            string categoria = ddlCategorias.SelectedValue;
            EliminarAlarmas();

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                txtNombre.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                txtPrecio.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtStock.Text))
            {
                txtStock.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtTipoCafe.Text) && int.Parse(categoria) == 1)
            {
                txtTipoCafe.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtTamano.Text) && int.Parse(categoria) == 1)
            {
                txtTamano.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtPorciones.Text) && int.Parse(categoria) == 2)
            {
                txtPorciones.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtGuarnicion.Text) && int.Parse(categoria) == 3)
            {
                txtGuarnicion.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtVolumen.Text) && int.Parse(categoria) == 5)
            {
                txtVolumen.CssClass += " error";
                esValido = false;
            }

            if (ddlCategorias.SelectedValue == "0")
            {
                ddlCategorias.CssClass += " error";
                esValido = false;
            }

            return esValido;
        }

        private void EliminarAlarmas()
        {
            txtNombre.CssClass = txtNombre.CssClass.Replace("error", "").Trim();
            txtPrecio.CssClass = txtPrecio.CssClass.Replace("error", "").Trim();
            txtStock.CssClass = txtStock.CssClass.Replace("error", "").Trim();
            txtTipoCafe.CssClass = txtTipoCafe.CssClass.Replace("error", "").Trim();
            txtTamano.CssClass = txtTamano.CssClass.Replace("error", "").Trim();
            txtPorciones.CssClass = txtPorciones.CssClass.Replace("error", "").Trim();
            txtGuarnicion.CssClass = txtGuarnicion.CssClass.Replace("error", "").Trim();
            txtVolumen.CssClass = txtVolumen.CssClass.Replace("error", "").Trim();
            ddlCategorias.CssClass = ddlCategorias.CssClass.Replace("error", "").Trim();
        }

        private void MsgError()
        {
            panelMsgLbl.CssClass = "MsgError";
            lblErrores.Text = "Complete los campos correspondientes...";
            lblErrores.Visible = true;
        }

        private void MsgSucces()
        {
            panelMsgLbl.CssClass = "MsgSucces";
            lblErrores.Text = "Producto Guardado con Exito !!!";
            lblErrores.Visible = true;
        }

    }
}