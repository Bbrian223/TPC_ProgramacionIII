using Dominio;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.ViewCommon
{
    public partial class Login : System.Web.UI.Page
    {
        Usuario nuevoUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            UsuarioManager manager = new UsuarioManager();

            try
            {
                nuevoUsuario = manager.ObtenerUsuario(txtUsuario.Text, txtPassword.Text);

                switch (nuevoUsuario.rol)
                {
                    case UserType.invalid:

                        InputError();
                        break;
                    case UserType.Gerente:
                        
                        Response.Redirect("~/ViewsManagment/Dashboard.aspx", false);
                        break;
                    case UserType.Mozo:

                        Response.Redirect("~/ViewsStaff/HomeStaff.aspx", false);
                        break;
                }

                Session.Add("User", nuevoUsuario);
                GuardarEmpleado((int)nuevoUsuario.idusuario);
            }
            catch (Exception ex)
            {
                // colocar pop up con el error
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

            
        }

        private void InputError()
        {
            txtUsuario.CssClass += " is-invalid";
            userErrorMsg.Style["display"] = "block";
            txtPassword.CssClass += " is-invalid";
            errorMessage.Style["display"] = "block";
        }

        public void GuardarEmpleado(int idusuario)
        {
            EmpleadoManager manager = new EmpleadoManager();
            Empleado emplActual = new Empleado();

            try
            {
                emplActual = manager.ObtenerPorId(idusuario);
                Session.Add("Empleado", emplActual);
                Seguridad.sesionActiva(Session["User"]);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}