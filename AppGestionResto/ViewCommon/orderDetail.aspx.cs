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
        public OrderManager ordManager = new OrderManager();
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
                    Session.Add("idMesa",int.Parse(mesa));
                }

            }

            VerificarEstadoMesa();
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

            Session["idMesa"] = 0;
        }

        protected void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idProducto = btn.CommandArgument;

            if (Session["GUARNICION"] is null)
                Session.Add("GUARNICION", (bool)false);

            try
            {
                if (ordManager.ObtenerEstadoMesa((int)Session["idMesa"]) != "PENDIENTE")
                {
                    CargarModal(int.Parse(idProducto),false);
                    CargarDatosLabel(idProducto);
                    Session["GUARNICION"] = ordManager.ObtenerGuarnicion(int.Parse(idProducto));
                }
                else 
                {
                    CargarModalError("Habilite la mesa antes de registrar un pedido", false);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

            Session.Add("idProducto",int.Parse(idProducto));
        }

        protected void btnAgregarProd_Click(object sender, EventArgs e)
        {
            int idProd = (int)Session["idProducto"];
            int cantidad = int.Parse(txtCantidad.Text);

            try
            {
                int stock = ordManager.VerificarStock(idProd);

                if (stock >= cantidad)
                {
                    GuardarProductoSeleccionado(idProd, cantidad);
                    VerificarEstadoMesa();
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
                else
                {
                    lblErrores.Text = "Stock insuficiente, cantidad disponible: " + stock;
                    CargarModal(idProd,true);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        protected void btnEliminarProd_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string idDetalle = btn.CommandArgument;

            try
            {
                ordManager.EliminarProdAlPedido(long.Parse(idDetalle));
                MostrarDetallePedido();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnCerrarPedido_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["User"];

            try
            {
                ordManager.CerrarPedido(usuario.idusuario);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnCancelarPedido_Click(object sender, EventArgs e)
        {
            // CANCELAR PEDIDO
            // SOLICITA CONFIRMACION POR CARTEL MODAL
            try
            {
                CargarModalError("Esta seguro que desea Cancelar este Pedido?",true);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

        }

        protected void btnMesaHabilitada_Click(object sender, EventArgs e)
        {
            try
            {
                ordManager.HabilitarMesa();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void btnModalAceptar_Click(object sender, EventArgs e)
        {
            // unica funcion --> cancelar pedido actual

            try
            {
                ordManager.CancelarPedido();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

            Response.Redirect(Request.Url.AbsoluteUri);
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
            ProductoManager manager = new ProductoManager();

            try
            {
                ddlGuarniciones.DataSource = manager.ObtnerGuarniciones();
                ddlTipoLeche.DataSource = manager.ObtnerTipoLeche();
                ddlTamanio.DataSource = manager.ObtnerTamanioTaza();

                ddlGuarniciones.DataTextField = "Nombre";
                ddlGuarniciones.DataValueField = "IdProducto";
                ddlGuarniciones.DataBind();

                ddlTipoLeche.DataTextField = "Nombre";
                ddlTipoLeche.DataValueField = "IdProducto";
                ddlTipoLeche.DataBind();

                ddlTamanio.DataTextField = "Nombre";
                ddlTamanio.DataValueField = "IdProducto";
                ddlTamanio.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        public void VerificarEstadoMesa() 
        {
            if (Session["idMesa"] is null)
                return;

            try
            {
                switch (ordManager.ObtenerEstadoMesa((int)Session["idMesa"]))
                {
                    case "DISPONIBLE":
                        // Mesa abierta, Sin Pedidos asignados
                        MostrarDatosMesa();
                        HabilitarBotonesEstado(false);
                        break;

                    case "OCUPADA":
                        // Pedido asignado
                        ordManager.ObtenerEstadoPedido();
                        MostrarDetallePedido();
                        HabilitarBotonesEstado(true);
                        break;

                    case "PENDIENTE":
                        // Mesa En espera
                        MostrarDatosMesa();
                        HabilitarBotonesEstado(false);
                        if(ordManager.Mesa.Habilitado == true)
                            btnMesaHabilitada.Visible = true;
                        else
                            btnMesaHabilitada.Visible = false;
                        break;

                    default:
                        // Mesa Cerrada
                        break;

                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void MostrarDatosMesa() 
        {
            Empleado empl = (Empleado)Session["Empleado"];

            try
            {
                lblFecha.Text = DateTime.Now.ToString();
                lblNumeroMesa.Text = ordManager.Mesa.IdMesa.ToString();
                lblEstadoMesa.Text = ordManager.Mesa.EstadoMesa;
                lblNroEmpleado.Text = empl.IdEmpleado.ToString();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }

        public void MostrarDetallePedido() 
        {
            MostrarDatosMesa();

            try
            {
                repeaterDetalles.DataSource = ordManager.Pedido.ListaProd;
                repeaterDetalles.DataBind();
                lblMontoTotal.Text = "$ " + ordManager.ObtenerMontoTotal().ToString();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        public void HabilitarBotonesEstado(bool estado)
        {
            btnCerrarPedido.Visible = estado;
            btnCancelarPedido.Visible = estado;
            pnlMonto.Visible = estado;
            btnMesaHabilitada.Visible = false;
        }

        public void CargarModal(int idProducto, bool error)
        {
            CargarProdSeleccionado(idProducto);
            CargarDropDownList();
            btnModalAceptar.Visible = false;
            pnlError.Visible = error; 
            lblErrores.Visible = error;
            txtCantidad.Text = "1";

            ClientScript.RegisterStartupScript(this.GetType(), "VistaPrevia", "var modal = new bootstrap.Modal(document.getElementById('modalDetalles')); modal.show();", true);
        }

        public void CargarModalError(string msg,bool btnEstado)
        {
            btnModalAceptar.Visible = btnEstado;
            lblModalError.Text = msg;   
            ClientScript.RegisterStartupScript(this.GetType(), "Error", "var modal = new bootstrap.Modal(document.getElementById('modalError')); modal.show();", true);
        }

        public void CargarDatosLabel(string idProd)
        {
            ProductoManager manager = new ProductoManager();

            try
            {
                switch (IdCategoria)
                {
                    case 2:
                        if (manager.EntradaIndividual(long.Parse(idProd)))
                            lblEntradas.Text = "Entrada Individual";
                        else
                            lblEntradas.Text = "Entrada para compartir";

                        break;

                    case 4:
                        Postres postre = manager.ObtenerPostre(long.Parse(idProd));

                        if (postre.ContieneAzucar)
                            lblAzucar.Text = "Contiene azucar agregada";
                        else
                            lblAzucar.Text = "No contiene azucar agregada";

                        if (postre.ContieneGluten)
                            lblGluten.Text = "Contiene gluten";
                        else
                            lblGluten.Text = "Sin Gluten";

                        break;
                    case 5:
                        Bebidas bebida = manager.ObtenerBebida(long.Parse(idProd));

                        if (bebida.Alcohol)
                            lblAlcohol.Text = "Contiene Alcohol";
                        else
                            lblAlcohol.Text = "No contiene Alcohol";

                        lblVolumen.Text = "Volumen: " + bebida.Volumen + " mL";

                        break;

                    default:
                        lblEntradas.Text = string.Empty;
                        lblAzucar.Text = string.Empty;
                        lblGluten.Text = string.Empty;
                        lblAlcohol.Text = string.Empty;
                        lblVolumen.Text = string.Empty;

                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (Session["IDCATEGORIA"] is null)
                Session.Add("IDCATEGORIA", IdCategoria);

            Session["IDCATEGORIA"] = IdCategoria;
        }

        public void GuardarProductoSeleccionado(int idProd, int cantidad)
        {
            Usuario user = (Usuario)Session["User"];
            string aclaraciones = txtBxDescripcion.Text;
            int idUser = (int)user.idusuario;
            IdCategoria = (long)Session["IDCATEGORIA"];

            try
            {
                switch (IdCategoria)
                {
                    case 1:     //CAFETERIA
                        ordManager.AgregarProdAlPedido(idProd, cantidad, idUser, aclaraciones);

                        if ((bool)Session["GUARNICION"] && ddlTipoLeche.SelectedIndex != 0)
                            ordManager.AgregarProdAlPedido(int.Parse(ddlTipoLeche.SelectedValue), 1, idUser, string.Empty);

                        if (ddlTamanio.SelectedIndex != 0)
                            ordManager.AgregarProdAlPedido(int.Parse(ddlTamanio.SelectedValue), 1, idUser, string.Empty);

                    break;  
                    
                    case 3:     //COMIDAS
                        if ((bool)Session["GUARNICION"])
                        {
                            if (string.IsNullOrWhiteSpace(txtBxDescripcion.Text))
                                aclaraciones = "Guarnicion: " + ddlGuarniciones.SelectedItem.Text;
                            else
                                aclaraciones += "; Guarnicion: " + ddlGuarniciones.SelectedItem.Text;
                        }

                        ordManager.AgregarProdAlPedido(idProd, cantidad, idUser, aclaraciones);

                    break;
                    
                    default:    //ENTRADAS,POSTRES,BEBIDAS
                        ordManager.AgregarProdAlPedido(idProd, cantidad, idUser, aclaraciones);

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