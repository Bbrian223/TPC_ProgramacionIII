using Dominio;
using Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.ViewsManagment
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Seguridad.NivelAcceso != UserType.Gerente) 
            {
                Response.Redirect("~/ViewsStaff/HomeStaff.aspx",false);
            }

            CargarImagen();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            BorrarDatos();
            Response.Redirect("~/ViewsManagment/UserSettings.aspx",false);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EmpleadoManager manager = new EmpleadoManager();
            Empleado nuevoEmpleado = new Empleado();

            if (!ValidarCampos())
            {
                MsgError();
                return;
            }

            try
            {
                nuevoEmpleado.Nombre = txtNombre.Text;
                nuevoEmpleado.Apellido = txtApellido.Text;
                nuevoEmpleado.Documento = txtDocumento.Text;
                nuevoEmpleado.FechaNac = DateTime.Parse(txtFechaNac.Text);
                nuevoEmpleado.Direccion.Calle = txtCalle.Text;
                nuevoEmpleado.Direccion.Numero = txtNumDir.Text;
                nuevoEmpleado.Direccion.Localidad = txtLocalidad.Text;
                nuevoEmpleado.Direccion.CodPostal = txtCodPostal.Text;
                nuevoEmpleado.Email = !(txtEmail.Text == string.Empty) ? txtEmail.Text : string.Empty;
                nuevoEmpleado.Telefono = !(txtTelefono.Text == string.Empty) ? txtTelefono.Text : string.Empty;
                nuevoEmpleado.rol = (UserType)ddlOpciones.SelectedIndex;
                nuevoEmpleado.nombreusuario = ObtenerNombreUsuario();

                manager.Agregar(nuevoEmpleado);

                GuardarImagen(manager.UltimoId());
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }


            BorrarDatos();
            MsgSucces();
        }

        //funciones

        private void BorrarDatos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtFechaNac.Text = string.Empty;
            txtCalle.Text = string.Empty;
            txtNumDir.Text = string.Empty;
            txtLocalidad.Text = string.Empty;
            txtCodPostal.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            ddlOpciones.SelectedValue = "0";

            Session["fileUser"] = null;
            imgPreview.ImageUrl = string.Empty;
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
            panelMsgLbl.CssClass = "MsgError";
            lblErrores.Text = "Complete los campos correspondientes...";
            lblErrores.Visible = true;
        }

        private void MsgSucces() 
        {
            panelMsgLbl.CssClass = "MsgSucces";
            lblErrores.Text = "Usuario Guardado con Exito !!!";
            lblErrores.Visible = true;
        }

        private void CargarImagen()
        {

            try
            {
                if (fileUploadImagen.HasFile)
                {
                    Session.Add("fileUser", fileUploadImagen);
                }

                if (Session["fileUser"] != null)
                {
                    FileUpload file = (FileUpload)Session["fileUser"];

                    byte[] imagenBytes = file.FileBytes;
                    string base64String = Convert.ToBase64String(imagenBytes);
                    imgPreview.ImageUrl = "data:image/png;base64," + base64String;
                }

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }

        }

        public void GuardarImagen(long id)
        {
            string rutaCarpeta, nombreArchivo, rutaCompleta;

            try
            {
                FileUpload file = (FileUpload)Session["fileUser"];

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

        public string ObtenerNombreUsuario()
        {
            string nombre = txtNombre.Text; ;
            string[] aux = nombre.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            nombre = aux[0];

            string apellido = txtApellido.Text; ;
            string[] aux2 = apellido.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            apellido = aux2[0];

            char primerCarac = nombre[0];

            string nombreUser = primerCarac + apellido;

            return nombreUser.ToLower();
        }


    }
}