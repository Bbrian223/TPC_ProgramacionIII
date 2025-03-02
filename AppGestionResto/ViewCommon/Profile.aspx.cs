using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Manager;

namespace WebApplication1.ViewCommon
{
    public partial class Profile : System.Web.UI.Page
    {
        public bool HabEdicion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            HabilitarEdicion(true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EmpleadoManager manager = new EmpleadoManager();
            Empleado empl = (Empleado)Session["Empleado"];

            if (!ValidarCampos())
            {
                HabilitarEdicion(true);
                MsgError();
                return;
            }

            try
            {
                empl.Direccion.Calle = txtCalle.Text;
                empl.Direccion.Numero = txtNumDir.Text;
                empl.Direccion.Localidad = txtLocalidad.Text;
                empl.Direccion.CodPostal = txtCodPostal.Text;
                empl.Telefono = txtTelefono.Text;
                empl.Email = txtEmail.Text;

                manager.Editar(empl);
                GuardarImagen(empl.idusuario);

                Session["Empleado"] = manager.ObtenerPorId(empl.idusuario);
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }

            CargarDatos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            HabilitarEdicion(false);
            EliminarAlarmas();
            CargarDatos();
        }


        //funciones

        private void CargarDatos()
        {
            EmpleadoManager manager = new EmpleadoManager();

            if (Session["Empleado"] is null) return;

            Empleado empl = (Empleado)Session["Empleado"];

            try
            {
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
                txtOpciones.Text = (empl.rol == UserType.Gerente) ? "Gerente" : "Mozo";
                imgPreview.ImageUrl = empl.Imagen.DirComp;

                HabilitarEdicion(false);
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        private void HabilitarEdicion(bool estado)
        {
            txtCalle.Enabled = estado;
            txtNumDir.Enabled = estado;
            txtLocalidad.Enabled = estado;
            txtCodPostal.Enabled = estado;
            txtTelefono.Enabled = estado;
            txtEmail.Enabled = estado;
            HabEdicion = estado;
            panelMsgLbl.Visible = false;

            if (Session["HabEdicion"] is null)
                Session.Add("HabEdicion", (bool)false);

            Session["HabEdicion"] = estado;
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
        }

        private void MsgError()
        {
            panelMsgLbl.Visible = true;
            lblErrores.Text = "Complete los campos correspondientes...";
            lblErrores.Visible = true;
        }

        public void GuardarImagen(long id)
        {
            string rutaCarpeta, nombreArchivo, rutaCompleta;

            if (!fileUploadImagen.HasFile) return;

            try
            {
                FileUpload file = fileUploadImagen;

                rutaCarpeta = Server.MapPath("~/Database/Imagenes/Perfiles/"); // Carpeta en el servidor
                nombreArchivo = "Usuario-" + id.ToString() + ".jpg";
                rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

                // Guardar la imagen en la carpeta del servidor
                file.SaveAs(rutaCompleta);

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}