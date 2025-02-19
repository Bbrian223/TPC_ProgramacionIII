using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Manager;

namespace WebApplication1.ViewsManagment
{
    public partial class AssignTable : System.Web.UI.Page
    {
        List<Salon> listaSalon = new List<Salon>();
        List<Mesa> listaMesas = new List<Mesa>();
        public bool EmplSeleccionado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Seguridad.NivelAcceso != UserType.Gerente)
            {
                Response.Redirect("~/ViewsStaff/HomeStaff.aspx", false);
            }

            if (!IsPostBack)
            {
                CargarGridView();
                ObtenerSalones();
                CargarSalones();
            }

            ObtenerMesas();
        }

        protected void ddlSalones_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSalon = int.Parse(ddlSalones.SelectedValue);
            Session["IDSALON"] = idSalon;
            ObtenerMesas();
        }

        protected void BtnMesa_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string mesaSeleccionada = clickedButton.ID;




        }

        protected void txtIdFiltro_TextChanged(object sender, EventArgs e)
        {
            string id = txtIdFiltro.Text.Trim();

            if (string.IsNullOrWhiteSpace(id))
            {
                CargarGridView();
            }
            else
            {
                BuscarMozoPorID(int.Parse(id));
            }
        }

        protected void txtDniFiltro_TextChanged(object sender, EventArgs e)
        {
            string dni = txtDniFiltro.Text.Trim();

            if (string.IsNullOrWhiteSpace(dni))
            {
                CargarGridView();
            }
            else
            {
                BuscarMozoPorDNI(dni);
            }
        }

        protected void gViewEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int idCliente = Convert.ToInt32(gViewEmpleados.DataKeys[index].Value);

            // Mostrar el ID seleccionado
            lblResultado.Text = $"Empleado seleccionado: {idCliente}";

            Session["emplSeleccionado"] = true;
        }

        //Funciones
        private void CargarGridView()
        {
            EmpleadoManager manager = new EmpleadoManager();
            List<Empleado> listaMozos = new List<Empleado>();

            try
            {
                //listaMozos = manager.ObtenerListaEmplPorRol((int)UserType.Mozo);
                listaMozos = manager.ObtenerListaEmplPorRol((int)UserType.Mozo);
                gViewEmpleados.DataSource = listaMozos; ;
                gViewEmpleados.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

        }

        private void BuscarMozoPorID(int id)
        {
            EmpleadoManager manager = new EmpleadoManager();

            try
            {
                List<Empleado> aux = manager.ObtenerListaEmplPorRol((int)UserType.Mozo).Where(emp => emp.IdEmpleado == id).ToList();
                gViewEmpleados.DataSource = aux;
                gViewEmpleados.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void BuscarMozoPorDNI(string dni)
        {
            EmpleadoManager manager = new EmpleadoManager();

            try
            {
                List<Empleado> aux = manager.ObtenerListaEmplPorRol((int)UserType.Mozo).Where(emp => emp.Documento.ToLower().Contains(dni.ToLower())).ToList();

                gViewEmpleados.DataSource = aux;
                gViewEmpleados.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        public void ObtenerSalones()
        {
            MesasManager manager = new MesasManager();

            try
            {
                listaSalon = manager.ObtenerListaTodosSalones();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

        }

        public void ObtenerMesas()
        {
            int salon = (Session["IDSALON"] == null) ? 1 : (int)Session["idSalon"];

            MesasManager manager = new MesasManager();

            try
            {
                listaMesas = manager.ObtenerMesasPorSalon(salon);
                AsignarDatosMesas();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

        }

        public void AsignarDatosMesas()
        {
            int inicio = (int)listaMesas[0].IdMesa;
            int fin = listaMesas.Count();
            ContenedorMesas.Controls.Clear();

            try
            {
                foreach (Mesa item in listaMesas)
                {
                    Button btnMesa = new Button();
                    int idMesa = (int)item.IdMesa;

                    btnMesa.ID = idMesa.ToString();
                    btnMesa.Text = item.IdMesa.ToString();
                    btnMesa.Click += BtnMesa_Click; // manejar evento

                    if (item.UsuarioAsignado.idusuario == -1) 
                    {
                        btnMesa.CssClass = "mesa-cuadrado mesa-cerrada text-decoration-none text-center";
                    }
                    else
                    {
                        btnMesa.CssClass = "mesa-cuadrado text-decoration-none text-center";
                    }

                    ContenedorMesas.Controls.Add(btnMesa);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CargarSalones() 
        {
            ddlSalones.DataSource = listaSalon;
            ddlSalones.DataValueField = "IdSalon";
            ddlSalones.DataTextField = "Nombre";
            ddlSalones.DataBind();

            EmplSeleccionado = false;
            ddlSalones.SelectedValue = "1";
            Session.Add("IDSALON",int.Parse(ddlSalones.SelectedValue));
            Session.Add("emplSeleccionado", EmplSeleccionado);
        }

    }
}