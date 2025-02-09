using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace WebApplication1.ViewCommon
{
    public partial class orderDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Seguridad.NivelAcceso == UserType.invalid)
            {
                Response.Redirect("~/ViewsManagment/HomeManagment.aspx", false);
            }


            if (!IsPostBack)
            {

                if (Request.QueryString["mesa"] != null) 
                {
                    string mesa = Request.QueryString["mesa"]; 
                    lblIdMesa.Text = mesa;
                }
                else
                {
                    Response.Write("<script>alert('Error: No se recibio ningun numero de mesa');</script>");
                }
            }
        }
    }
}