using Dominio;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                Response.Redirect("~ViewsStaff/HomeStaff.aspx",false);
            }

            if (!IsPostBack) 
            {   
                CargarLista();
            }
        }

        protected void btnBuscarDni_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuscarApellido_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuscarNombre_Click(object sender, EventArgs e)
        {

        }

        protected void btnEditarEmpleado_Click(object sender, EventArgs e)
        {

        }

        protected void btnBajaEmpleado_Click(object sender, EventArgs e)
        {

        }

        // Funciones

        private void CargarLista() 
        {
            EmpleadoManager manager = new EmpleadoManager();

            try
            {
                listaEmpleados = manager.ObtenerTodos();
                repeaterEmpleados.DataSource = listaEmpleados;
                repeaterEmpleados.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

    }
}