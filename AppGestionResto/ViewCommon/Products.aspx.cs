using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manager;
using Dominio;

namespace WebApplication1.ViewCommon
{
    public partial class Productos : System.Web.UI.Page
    {
        public List<Producto> ListaProductos = new List<Producto>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Seguridad.NivelAcceso != UserType.Gerente) 
            {
                Response.Redirect("~/ViewsStaff/HomeStaff.aspx", false);
            }


            if (!IsPostBack) 
            {
                CargarLista( ObtenerTodosProductos() );
            }
        }

        protected void txtNombreFiltro_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnBuscarNombre_Click(object sender, EventArgs e)
        {

        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ViewCommon/AddProduct.aspx",false);
        }

        //funciones

        public void CargarLista(List<Producto> lista) 
        {
            try
            {
                repeaterProductos.DataSource = lista;
                repeaterProductos.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        public List<Producto> ObtenerTodosProductos() 
        {
            ProductoManager manager = new ProductoManager();
            List<Producto> aux = new List<Producto>();

            try
            {
                aux = manager.ObtnerTodos();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

            return aux;
        }
    }
}