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
            //MODIFICAR PARA SABER SI MUESTRA GUARNICION

            try
            {
                if (ordManager.ObtenerEstadoMesa((int)Session["idMesa"]) != "PENDIENTE")
                {
                    CargarModal(int.Parse(idProducto),false);
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
            Usuario user = (Usuario)Session["User"];


            try
            {
                int stock = ordManager.VerificarStock(idProd);

                if (stock >= cantidad)
                {
                    ordManager.AgregarProdAlPedido(idProd, cantidad, (int)user.idusuario);
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


    }
}