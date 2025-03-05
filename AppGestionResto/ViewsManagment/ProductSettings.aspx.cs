using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Manager;
using Dominio;
using System.Runtime.Versioning;
using System.IO;

namespace WebApplication1.ViewCommon
{
    public partial class Productos : System.Web.UI.Page
    {
        public List<Producto> ListaProductos = new List<Producto>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Seguridad.NivelAcceso != UserType.Gerente)
            {
                Response.Redirect("~/ViewsManagment/HomeManagment.aspx", false);
            }


            if (!IsPostBack)
            {
                CargarLista();
            }

            if (Session["EditarProducto"] is null)
                Session.Add("EditarProducto", (bool)false);
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

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ViewsManagment/AddProduct.aspx", false);
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

        protected void btnConfirmarBaja_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.CommandArgument;

            lblModalIdBaja.Text = id;

            ClientScript.RegisterStartupScript(this.GetType(), "Baja", "var modal = new bootstrap.Modal(document.getElementById('modalEliminar')); modal.show();", true);
        }

        protected void btnEliminarProd_Click(object sender, EventArgs e)
        {
            ProductoManager manager = new ProductoManager();
            string idProd = lblModalIdBaja.Text;
            try
            {
                manager.Baja(int.Parse(idProd));
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

            CargarLista();
        }

        protected void btnVerProducto_Click(object sender, EventArgs e)
        {
            ProductoManager manager = new ProductoManager();
            Button btn = (Button)sender;
            string idProd = (string.IsNullOrWhiteSpace(btn.CommandArgument)) ? (string)Session["IdProducto"] : btn.CommandArgument;
            lblModalNumUsuario.Text = idProd;

            if (Session["IdProducto"] is null)
                Session.Add("IdProducto", idProd);

            try
            {
                Producto prod = new Producto();
                prod = manager.Obtener(long.Parse(idProd));
                CargarDatosProducto(prod);
                HabilitarEdicion(false);

                ClientScript.RegisterStartupScript(this.GetType(), "Editar", "var modal = new bootstrap.Modal(document.getElementById('modalEditar')); modal.show();", true);
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }

            Session["IdProducto"] = idProd;
            Session["EditarProducto"] = false;
        }

        protected void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            HabilitarEdicion(true);
            Session["EditarProducto"] = true;
            ClientScript.RegisterStartupScript(this.GetType(), "Editar", "var modal = new bootstrap.Modal(document.getElementById('modalEditar')); modal.show();", true);
        }

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            ProductoManager manager = new ProductoManager();
            string idProd = (string)Session["IdProducto"];

            if (!ValidarCampos())
            {
                MsgError();
                ClientScript.RegisterStartupScript(this.GetType(), "Editar", "var modal = new bootstrap.Modal(document.getElementById('modalEditar')); modal.show();", true);
                return;
            }

            try
            {
                Producto prod = new Producto();
                prod.IdProducto = long.Parse(idProd);
                prod.Nombre = txtNombre.Text;
                prod.Precio = decimal.Parse(txtPrecio.Text);
                prod.stock = int.Parse(txtStock.Text);
                prod.Descripcion = txtBxDescripcion.Text;

                manager.Editar(prod);
                GuardarImagen(prod.IdProducto);
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')<Script>");
            }

            CargarLista();
            btnVerProducto_Click(sender, e);
        }

        protected void btnHabilitarProd_Click(object sender, EventArgs e)
        {
            ProductoManager manager = new ProductoManager();
            Button btn = (Button)sender;
            string id = btn.CommandArgument;

            try
            {
                manager.Alta(long.Parse(id));
                CargarLista();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
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
                txtPrecio.Text = prod.Precio.ToString();
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

        public void HabilitarEdicion(bool status)
        {
            txtPrecio.Enabled = status;
            txtStock.Enabled = status;
            txtBxDescripcion.Enabled = status;
            panelMsgLbl.Visible = false;
        }   

        private bool ValidarCampos()
        {
            bool esValido = true;
            string categoria = ddlCategoriasModal.SelectedValue;
            EliminarAlarmas();

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                txtNombre.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                txtPrecio.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtStock.Text))
            {
                txtStock.CssClass += " error";
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtVolumen.Text) && int.Parse(categoria) == 5)
            {
                txtVolumen.CssClass += " error";
                esValido = false;
            }

            if (ddlCategoriasModal.SelectedValue == "0")
            {
                ddlCategorias.CssClass += " error";
                esValido = false;
            }

            return esValido;
        }

        private void EliminarAlarmas()
        {
            txtNombre.CssClass = txtNombre.CssClass.Replace("error", "").Trim();
            txtPrecio.CssClass = txtPrecio.CssClass.Replace("error", "").Trim();
            txtStock.CssClass = txtStock.CssClass.Replace("error", "").Trim();
            txtVolumen.CssClass = txtVolumen.CssClass.Replace("error", "").Trim();
            ddlCategoriasModal.CssClass = ddlCategorias.CssClass.Replace("error", "").Trim();
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

                rutaCarpeta = Server.MapPath("~/Database/Imagenes/Productos/"); // Carpeta en el servidor
                nombreArchivo = "producto-" + id.ToString() + ".jpg";
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