using System;
using System.Collections.Generic;
using System.Globalization;
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
                decimal precio = Decimal.Parse(txtPrecio.Text, new CultureInfo("es-ES"));

                nuevoProd.Nombre = txtNombre.Text;
                nuevoProd.Precio = precio;
                nuevoProd.stock = int.Parse(txtStock.Text); //validar
                nuevoProd.Descripcion = txtBxDescripcion.Text;
                nuevoProd.Categoria.IdCategoria = int.Parse(ddlCategorias.SelectedValue);

                switch (ddlCategorias.SelectedValue)
                {
                    case "2":   //entradas
                        manager.AgregarEntrada(nuevoProd,chkIndividual.Checked);
                        break;

                    case "4":   //postre
                        manager.AgregarPostre(nuevoProd,chkAzucar.Checked,chkGluten.Checked);    
                        break;
                    
                    case "5":   //bebida
                        manager.AgregarBebida(nuevoProd,int.Parse(txtVolumen.Text),chkAlcohol.Checked);
                        break;

                    default:    // general
                        manager.AgregarProd(nuevoProd);
                        break;
                }

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
            txtVolumen.Text = string.Empty;

            chkIndividual.Checked = false;
            chkAzucar.Checked = false;
            chkGluten.Checked = false;
            chkAlcohol.Checked = false;

            ddlCategorias.SelectedValue = "0";
            lblErrores.Visible = false;
            txtBxDescripcion.Text = string.Empty;
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
            txtVolumen.CssClass = txtVolumen.CssClass.Replace("error", "").Trim();
            chkIndividual.CssClass = chkIndividual.CssClass.Replace("error", "").Trim();
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