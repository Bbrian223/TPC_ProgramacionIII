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
                CargarLista();
            }
        }

        protected void txtNombreFiltro_TextChanged(object sender, EventArgs e)
        {
            string nombre = txtNombreFiltro.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                CargarLista();
            }
            else
            {
                btnBuscarNombre_Click(sender, e);
            }
        }

        protected void btnBuscarNombre_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreFiltro.Text;
            ListaProductos = ObtenerTodosProductos().Where(
                empl => empl.Nombre != null && empl.Nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            CargarLista(ListaProductos);

        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ViewCommon/AddProduct.aspx", false);
        }

        //funciones

        public void CargarLista()
        {
            try
            {
                repeaterProductos.DataSource = ObtenerTodosProductos();
                repeaterProductos.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

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

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoria = int.Parse(ddlCategorias.SelectedValue);

            try
            {
                if (idCategoria != 0)
                {
                    ListaProductos = ObtenerTodosProductos().Where(
                        empl => empl.Categoria.IdCategoria != null && empl.Categoria.IdCategoria == idCategoria).ToList();
                    CargarLista(ListaProductos);
                }
                else
                {
                    CargarLista();
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
    }
}