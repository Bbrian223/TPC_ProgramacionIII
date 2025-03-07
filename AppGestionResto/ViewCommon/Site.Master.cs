using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Manager;

namespace WebApplication1
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionActiva(Session["User"]))
            {
                Response.Redirect("~/ViewCommon/Login.aspx", false);
            }
            else
            {
                CargarImagen();
            }

        }

        protected void btnCerrarSesion_OnClick(object sender, EventArgs e)
        {
            CerrarSesion();
        }

        public void CerrarSesion()
        {   
            Seguridad.CerrarSesion();

            Response.Redirect("~/ViewCommon/Login.aspx", false);
        }

        public void CargarImagen()
        {   
            imgUser.ImageUrl = ((Empleado)Session["Empleado"]).Imagen.DirComp;
        }
    }
}