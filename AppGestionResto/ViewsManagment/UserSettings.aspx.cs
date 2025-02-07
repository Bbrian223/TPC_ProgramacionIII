using Dominio;
using Manager;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.ViewsManagment
{
    public partial class UserSettings : System.Web.UI.Page
    {
        public List<Empleado> listaEmpleados = new List<Empleado>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Seguridad.NivelAcceso != UserType.Gerente)
            {
                Response.Redirect("~ViewsStaff/HomeStaff.aspx", false);
            }

            if (!IsPostBack)
            {
                CargarListaEnPantalla();
            }
        }

        protected void btnBuscarDni_Click(object sender, EventArgs e)
        {
            string dni = txtDniFiltro.Text;
            txtApellidoFiltro.Text = string.Empty;
            txtNombreFiltro.Text = string.Empty;

            listaEmpleados = ObtenerListaEmpl().Where(empl => empl.Documento == dni).ToList();
            repeaterEmpleados.DataSource = listaEmpleados;
            repeaterEmpleados.DataBind();
        }

        protected void btnBuscarApellido_Click(object sender, EventArgs e)
        {
            string apellido = txtApellidoFiltro.Text;
            txtNombreFiltro.Text = string.Empty;
            txtDniFiltro.Text = string.Empty;

            listaEmpleados = ObtenerListaEmpl().Where(empl => empl.Apellido == apellido).ToList();
            repeaterEmpleados.DataSource = listaEmpleados;
            repeaterEmpleados.DataBind();
        }

        protected void btnBuscarNombre_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreFiltro.Text;
            txtApellidoFiltro.Text = string.Empty;
            txtDniFiltro.Text = string.Empty;

            listaEmpleados = ObtenerListaEmpl().Where(empl => empl.Nombre == nombre).ToList();
            repeaterEmpleados.DataSource = listaEmpleados;
            repeaterEmpleados.DataBind();
        }

        protected void btnEliminarEmpleado_Click(object sender, EventArgs e)
        {
            string idEmpl = hiddenFieldIdEmpleado.Value;

            EmpleadoManager manager = new EmpleadoManager();

            try
            {
                manager.Baja(int.Parse(idEmpl));
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

            CargarListaEnPantalla();
        }

        protected void btnEditarEmpleado_Click(object sender, EventArgs e)
        {
            // editar empleado
        }

        protected void btnAgregarEmpleado_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ViewsManagment/AddUser.aspx", false);
        }

        protected void txtDniFiltro_TextChanged(object sender, EventArgs e)
        {
            string dni = txtDniFiltro.Text.Trim();

            if (string.IsNullOrWhiteSpace(dni))
            {
                CargarListaEnPantalla();
            }
            else 
            {
                btnBuscarDni_Click(sender,e);
            }
        }

        protected void txtApellidoFiltro_TextChanged(object sender, EventArgs e)
        {
            string apellido = txtApellidoFiltro.Text.Trim();

            if (string.IsNullOrWhiteSpace(apellido))
            {
                CargarListaEnPantalla();
            }
            else
            {
                btnBuscarApellido_Click(sender, e);
            }
        }

        protected void txtNombreFiltro_TextChanged(object sender, EventArgs e)
        {
            string nombre = txtNombreFiltro.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                CargarListaEnPantalla();
            }
            else
            {
                btnBuscarNombre_Click(sender, e);
            }
        }

        // Funciones

        private void CargarListaEnPantalla()
        {
            try
            {
                listaEmpleados = ObtenerListaEmpl();
                repeaterEmpleados.DataSource = listaEmpleados;
                repeaterEmpleados.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        private List<Empleado> ObtenerListaEmpl() 
        {
            List<Empleado> listaTodosEmpleados = new List<Empleado>();
            EmpleadoManager manager = new EmpleadoManager();

            try
            {
                listaTodosEmpleados = manager.ObtenerTodos();
            }
            catch (Exception)
            {
                throw;
            }

            return listaTodosEmpleados;
        }

    }
}