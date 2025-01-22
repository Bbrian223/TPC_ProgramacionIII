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

        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {


        }

        private void InputError()
        {
            txtUsuario.CssClass += " is-invalid";
            userErrorMsg.Style["display"] = "block";
            txtPassword.CssClass += " is-invalid";
            errorMessage.Style["display"] = "block";
        }

        private void ObtenerDatosUsuario()
        {

        }
    }
}