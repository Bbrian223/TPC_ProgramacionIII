using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Manager;

namespace WebApplication1.ViewsStaff
{
    public partial class ProductsList : System.Web.UI.Page
    {
        public List<Producto> ListaProductos = new List<Producto>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Seguridad.NivelAcceso != UserType.Mozo)
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
                nombre = txtNombreFiltro.Text;
                ListaProductos = ObtenerTodosProductos().Where(
                    empl => empl.Nombre != null && empl.Nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                CargarLista(ListaProductos);
            }
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

        protected void btnVerProducto_Click(object sender, EventArgs e)
        {
            ProductoManager manager = new ProductoManager();
            Button btn = (Button)sender;
            string idProd = (string.IsNullOrWhiteSpace(btn.CommandArgument)) ? (string)Session["IdProducto"] : btn.CommandArgument;
            lblModalNumProd.Text = idProd;

            if (Session["IdProducto"] is null)
                Session.Add("IdProducto", idProd);

            try
            {
                Producto prod = new Producto();
                prod = manager.Obtener(long.Parse(idProd));
                CargarDatosProducto(prod);

                ClientScript.RegisterStartupScript(this.GetType(), "Editar", "var modal = new bootstrap.Modal(document.getElementById('modalEditar')); modal.show();", true);
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }

            Session["IdProducto"] = idProd;
            Session["EditarProducto"] = false;
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

        public void CargarDatosProducto(Producto prod)
        {
            ProductoManager manager = new ProductoManager();

            try
            {
                ddlCategoriasModal.SelectedValue = prod.Categoria.IdCategoria.ToString();
                txtNombre.Text = prod.Nombre;
                txtPrecio.Text = prod.Precio.ToString("0.0");
                txtStock.Text = prod.stock.ToString();
                txtBxDescripcion.Text = prod.Descripcion;
                imgPreview.ImageUrl = prod.Imagen.DirComp;

                switch (ddlCategoriasModal.SelectedValue)
                {
                    case "1":   //cafeteria
                        if (prod.Guarnicion)
                            lblContieneLeche.Text = "si";
                        else
                            lblContieneLeche.Text = "no";

                        break;
                    case "2":   // Entradas
                        Entradas entrada = manager.ObtenerEntrada(prod.IdProducto);
                        if (entrada.Individual)
                            lblEntrada.Text = "Individual";
                        else
                            lblEntrada.Text = "Para compartir";

                        break;

                    case "3":   //Comidas
                        if (prod.Guarnicion)
                            lblContieneGuarnicion.Text = "si";
                        else
                            lblContieneGuarnicion.Text = "no";

                        break;

                    case "4":
                        Postres postre = manager.ObtenerPostre(prod.IdProducto);
                        if (postre.ContieneAzucar)
                            lblAzucar.Text = "si";
                        else
                            lblAzucar.Text = "no";

                        if (postre.ContieneGluten)
                            lblGluten.Text = "si";
                        else
                            lblGluten.Text = "no";

                        break;

                    case "5":
                        Bebidas bebida = manager.ObtenerBebida(prod.IdProducto);
                        if (bebida.Alcohol)
                            lblAlcohol.Text = "si";
                        else
                            lblAlcohol.Text = "no";

                        txtVolumen.Text = bebida.Volumen.ToString();

                        break;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}