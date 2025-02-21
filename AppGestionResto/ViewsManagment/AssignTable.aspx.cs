using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Manager;

namespace WebApplication1.ViewsManagment
{
    public partial class AssignTable : System.Web.UI.Page
    {
        List<string> listaIdSelect = new List<string>();
        List<Salon> listaSalon = new List<Salon>();
        List<Mesa> listaMesas = new List<Mesa>();
        
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

            try
            {
                if (!chkAsignarMesa.Checked)
                {
                    MostrarModal(mesaSeleccionada);
                }
                else
                {
                    AsignarMesaEmpleado(mesaSeleccionada);
                    ObtenerMesas();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }

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
            string idEmpleado = gViewEmpleados.DataKeys[index].Value.ToString();

            if (Session["ListIdEmpl"] == null) 
                Session.Add("ListIdEmpl", new List<string>());

            listaIdSelect = (List<string>)Session["ListIdEmpl"];

            try
            {
                if (!listaIdSelect.Contains(idEmpleado)) 
                    listaIdSelect.Add(idEmpleado);

                gviewEmplSelect.DataSource = ObtenerEmp(listaIdSelect);
                gviewEmplSelect.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }

            Session["ListIdEmpl"] = listaIdSelect;
        }

        protected void gviewEmplSelect_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string idEmpleado = gviewEmplSelect.DataKeys[index].Value.ToString();

            listaIdSelect = (List<string>)Session["ListIdEmpl"];

            try
            {
                listaIdSelect.Remove(idEmpleado);
                gviewEmplSelect.DataSource = ObtenerEmp(listaIdSelect);
                gviewEmplSelect.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }

            Session["ListIdEmpl"] = listaIdSelect;
        }

        protected void gViewMesaEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MesasManager manager = new MesasManager();
            int index = Convert.ToInt32(e.CommandArgument);
            string idEmpleado = gViewMesaEmpleados.DataKeys[index].Value.ToString();
            string mesa = lblModalMesa.Text;

            try
            {
                manager.desasignarEmpleadoMesa( long.Parse(idEmpleado), long.Parse(mesa));
                MostrarModal(mesa);
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message +"')</Script>");
            }
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

                    if (item.EmplAsignados == 0)
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

            ddlSalones.SelectedValue = "1";
            Session.Add("IDSALON", int.Parse(ddlSalones.SelectedValue));
        }

        public List<Empleado> ObtenerEmp(List<string> lista)
        {
            EmpleadoManager manager = new EmpleadoManager();
            List<Empleado> aux = new List<Empleado>();

            try
            {
                foreach (var item in lista)
                {
                    aux.Add( manager.ObtenerPorId(long.Parse(item)) );
                }
            }
            catch (Exception)
            {
                throw;
            }

            return aux;
        }

        public void MostrarModal(string mesa)
        {
            EmpleadoManager manager = new EmpleadoManager();
            List<Empleado> lista = new List<Empleado>();
            lblModalError.Text = string.Empty;

            try
            {
                lista = manager.ObtenerEmpleadosPorMesa(long.Parse(mesa));

                gViewMesaEmpleados.DataSource = lista;
                gViewMesaEmpleados.DataBind();
                lblModalMesa.Text = mesa;

                if (lista.Count == 0) lblModalError.Text = "SIN MESAS ASIGNADAS";

                ObtenerMesas();
            }
            catch (Exception)
            {
                throw;
            }

            ClientScript.RegisterStartupScript(this.GetType(), "Error", "var modal = new bootstrap.Modal(document.getElementById('modalEstado')); modal.show();", true);
        }

        public void AsignarMesaEmpleado(string mesa)
        {
            MesasManager manager = new MesasManager();
            List<string> lista = (List<string>)Session["ListIdEmpl"];

            if (lista is null)
                return;

            try
            {
                foreach (var item in lista)
                {
                    manager.AsignarMesa( long.Parse(item), long.Parse(mesa));
                } 
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}