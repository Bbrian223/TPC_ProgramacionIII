using Dominio;
using Manager;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.ViewsManagment
{
    public partial class UserSettings : System.Web.UI.Page
    {
        public List<Empleado> listaEmpleados = new List<Empleado>();
        public bool Editar;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Seguridad.NivelAcceso != UserType.Gerente)
            {
                Response.Redirect("~ViewsStaff/HomeStaff.aspx", false);
            }

            if (!IsPostBack)
            {
                CargarListaEnPantalla();
                if (Session["EditarUsuario"] is null)
                    Session.Add("EditarUsuario", (bool)false);
            }
        }

        protected void btnValidarBaja_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.CommandArgument;

            lblModalIdBaja.Text = id;

            ClientScript.RegisterStartupScript(this.GetType(), "Eliminar", "var modal = new bootstrap.Modal(document.getElementById('modalEliminar')); modal.show();", true);
        }

        protected void btnEliminarEmpleado_Click(object sender, EventArgs e)
        {
            string idEmpl = lblModalIdBaja.Text;

            EmpleadoManager manager = new EmpleadoManager();

            try
            {
                manager.Baja(int.Parse(idEmpl));
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

            CargarListaEnPantalla();
        }

        protected void btnVerEmpleado_Click(object sender, EventArgs e)
        {
            EmpleadoManager manager = new EmpleadoManager();
            Button btn = (Button)sender;
            string idUsuario = (string.IsNullOrWhiteSpace(btn.CommandArgument)) ? (string)Session["IDUSUARIO"] : btn.CommandArgument;
            lblModalNumUsuario.Text = idUsuario;

            if (Session["IDUSUARIO"] is null)
                Session.Add("IDUSUARIO",idUsuario);

            try
            {
                Empleado empl = manager.ObtenerPorId(long.Parse(idUsuario));
            
                txtNombre.Text = empl.Nombre;
                txtApellido.Text = empl.Apellido;
                txtDocumento.Text = empl.Documento;
                txtFechaNac.Text = empl.FechaNac.ToString("yyyy-MM-dd");
                txtCalle.Text = empl.Direccion.Calle;
                txtNumDir.Text = empl.Direccion.Numero;
                txtLocalidad.Text = empl.Direccion.Localidad;
                txtCodPostal.Text = empl.Direccion.CodPostal;
                txtEmail.Text = empl.Email;
                txtTelefono.Text = empl.Telefono;
                ddlOpciones.SelectedValue = (empl.rol == UserType.Gerente) ? "1" : "2";
                imgPreview.ImageUrl = empl.Imagen.DirComp;

                HabilitarEdicionModal(false);
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }

            Session["IDUSUARIO"] = idUsuario;
            ClientScript.RegisterStartupScript(this.GetType(), "Editar", "var modal = new bootstrap.Modal(document.getElementById('modalEditar')); modal.show();", true);
        }

        protected void btnAgregarEmpleado_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ViewsManagment/AddUser.aspx", false);
        }

        protected void txtDniFiltro_TextChanged(object sender, EventArgs e)
        {
            string dni = txtDniFiltro.Text.Trim();

            if (string.IsNullOrWhiteSpace(dni))
            {
                CargarListaEnPantalla();
            }
            else 
            {
                txtApellidoFiltro.Text = string.Empty;
                txtNombreFiltro.Text = string.Empty;

                listaEmpleados = ObtenerListaEmpl().Where(empl => empl.Documento == dni).ToList();
                repeaterEmpleados.DataSource = listaEmpleados;
                repeaterEmpleados.DataBind();
            }
        }

        protected void txtApellidoFiltro_TextChanged(object sender, EventArgs e)
        {
            string apellido = txtApellidoFiltro.Text.Trim();

            if (string.IsNullOrWhiteSpace(apellido))
            {
                CargarListaEnPantalla();
            }
            else
            {
                txtNombreFiltro.Text = string.Empty;
                txtDniFiltro.Text = string.Empty;

                listaEmpleados = ObtenerListaEmpl().Where(empl => empl.Apellido.ToUpper() == apellido.ToUpper()).ToList();
                repeaterEmpleados.DataSource = listaEmpleados;
                repeaterEmpleados.DataBind();
            }
        }

        protected void txtNombreFiltro_TextChanged(object sender, EventArgs e)
        {
            string nombre = txtNombreFiltro.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                CargarListaEnPantalla();
            }
            else
            {
                txtApellidoFiltro.Text = string.Empty;
                txtDniFiltro.Text = string.Empty;

                listaEmpleados = ObtenerListaEmpl().Where(empl => empl.Nombre.ToUpper() == nombre.ToUpper()).ToList();
                repeaterEmpleados.DataSource = listaEmpleados;
                repeaterEmpleados.DataBind();
            }
        }

        protected void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            HabilitarEdicionModal(true);
            ClientScript.RegisterStartupScript(this.GetType(), "Editar", "var modal = new bootstrap.Modal(document.getElementById('modalEditar')); modal.show();", true);
        
        }

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            
            Empleado empl = new Empleado();
            EmpleadoManager manager = new EmpleadoManager();
            string idUsuario = (string)Session["IDUSUARIO"];

            if (!ValidarCampos())
            {
                btnEditarUsuario_Click(sender, e);
                MsgError();
                return;
            }

            try
            {
                empl.idusuario = long.Parse(idUsuario);
                empl.Direccion.Calle = txtCalle.Text;
                empl.Direccion.Numero = txtNumDir.Text;
                empl.Direccion.Localidad = txtLocalidad.Text;
                empl.Direccion.CodPostal = txtCodPostal.Text;
                empl.Telefono = txtTelefono.Text;
                empl.Email = txtEmail.Text;
                empl.rol = (ddlOpciones.SelectedValue == "1") ? UserType.Gerente : UserType.Mozo;

                manager.Editar(empl);
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }

            CargarListaEnPantalla();
            btnVerEmpleado_Click(sender,e);
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            EmpleadoManager manager = new EmpleadoManager();
            Button btn = (Button)sender;
            string id = btn.CommandArgument;

            try
            {
                manager.Alta(int.Parse(id));
                CargarListaEnPantalla();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }

        }

        // Funciones

        private void CargarListaEnPantalla()
        {
            try
            {
                listaEmpleados = ObtenerListaEmpl();
                repeaterEmpleados.DataSource = listaEmpleados;
                repeaterEmpleados.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        private List<Empleado> ObtenerListaEmpl() 
        {
            List<Empleado> listaTodosEmpleados = new List<Empleado>();
            EmpleadoManager manager = new EmpleadoManager();

            try
            {
                listaTodosEmpleados = manager.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }

            return listaTodosEmpleados;
        }

        private void HabilitarEdicionModal(bool editar)
        {
            txtCalle.Enabled = editar;
            txtNumDir.Enabled = editar;
            txtLocalidad.Enabled = editar;
            txtCodPostal.Enabled = editar;
            txtEmail.Enabled = editar;
            txtTelefono.Enabled = editar;
            ddlOpciones.Enabled = editar;
            panelMsgLbl.Visible = false;

            Editar = editar;

            if (Session["EditarUsuario"] is null)
                Session.Add("EditarUsuario", Editar);

            Session["EditarUsuario"] = Editar;
        }

        private bool ValidarCampos()
        {
            bool esValido = true;
            EliminarAlarmas();

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                txtNombre.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                txtApellido.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                txtDocumento.CssClass += " error";
                esValido = false;
            }

            if (!DateTime.TryParse(txtFechaNac.Text, out _))
            {
                txtFechaNac.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtCalle.Text))
            {
                txtCalle.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtNumDir.Text))
            {
                txtNumDir.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtLocalidad.Text))
            {
                txtLocalidad.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtCodPostal.Text))
            {
                txtCodPostal.CssClass += " error";
                esValido = false;
            }

            if (ddlOpciones.SelectedValue == "0")
            {
                ddlOpciones.CssClass += " error";
                esValido = false;
            }

            return esValido;
        }

        private void EliminarAlarmas()
        {
            txtNombre.CssClass = txtNombre.CssClass.Replace("error", "").Trim();
            txtApellido.CssClass = txtApellido.CssClass.Replace("error", "").Trim();
            txtDocumento.CssClass = txtDocumento.CssClass.Replace("error", "").Trim();
            txtFechaNac.CssClass = txtFechaNac.CssClass.Replace("error", "").Trim();
            txtCalle.CssClass = txtCalle.CssClass.Replace("error", "").Trim();
            txtNumDir.CssClass = txtNumDir.CssClass.Replace("error", "").Trim();
            txtLocalidad.CssClass = txtLocalidad.CssClass.Replace("error", "").Trim();
            txtCodPostal.CssClass = txtCodPostal.CssClass.Replace("error", "").Trim();
            txtEmail.CssClass = txtEmail.CssClass.Replace("error", "").Trim();
            txtTelefono.CssClass = txtTelefono.CssClass.Replace("error", "").Trim();
            ddlOpciones.CssClass = ddlOpciones.CssClass.Replace("error", "").Trim();

        }

        private void MsgError()
        {
            panelMsgLbl.Visible = true;
            lblErrores.Text = "Complete los campos correspondientes...";
            lblErrores.Visible = true;
        }


    }
}