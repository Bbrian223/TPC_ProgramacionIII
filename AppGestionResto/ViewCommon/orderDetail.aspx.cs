using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Manager;

namespace WebApplication1.ViewCommon
{
    public partial class orderDetail : System.Web.UI.Page
    {
        public List<Producto> listaProduto = new List<Producto>();
        public long IdCategoria;

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

            CargarLista();
        }

        protected void btnCerraVentana_Click(object sender, EventArgs e)
        {
            if (Seguridad.NivelAcceso == UserType.Gerente)
            {
                Response.Redirect("~/ViewsManagment/HomeManagment.aspx", false);
            }
            else 
            { 
                Response.Redirect("~/ViewsStaff/HomeStaff.aspx", false);
            }
        }

        protected void btnAgregarProd_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idProducto = btn.CommandArgument;

            CargarProdSeleccionado(int.Parse(idProducto));


            ClientScript.RegisterStartupScript(this.GetType(), "EjecutarAlgo", "var modal = new bootstrap.Modal(document.getElementById('modalDetalles')); modal.show();", true);

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

        public void CargarProdSeleccionado(int id) 
        {
            List<Producto> ProdSeleccionado = ObtenerTodosProductos().Where(emp => emp.IdProducto == id).ToList();

            repeaterModal.DataSource = ProdSeleccionado;
            repeaterModal.DataBind();

            IdCategoria = ProdSeleccionado[0].Categoria.IdCategoria;
        }

        public void CargarDropDownList() 
        { 
            // 3 ddl --> tipo leche, tamanio, guarnicion
        }
    }
}