using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Web.Script.Serialization;
using Manager;


namespace WebApplication1.ViewsManagment
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGraficoBarras();
                CargarGraficoDona();
                CargarEmpleados();
                CargarProductos();
                CargarTarjetas();
            }
        }

        private void CargarGraficoBarras()
        {
            VentasManager manager = new VentasManager();

            try
            {
                List<decimal> lista = manager.ObtenerVentaSemanal();

                // Simulación de datos desde la base de datos
                var datosVentas = new Dictionary<string, int>
                {
                    { "Domingo", (int)lista[0] },
                    { "Lunes", (int)lista[1] },
                    { "Martes", (int)lista[2] },
                    { "Miercoles", (int)lista[3] },
                    { "Jueves", (int)lista[4] },
                    { "Viernes", (int)lista[5] },
                    { "Sabado", (int)lista[6] }
                };

                // Serializar los datos en formato JSON
                var json = new JavaScriptSerializer().Serialize(datosVentas);

                // Pasar la variable al frontend
                VentasJson.Value = json;
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        private void CargarGraficoDona()
        {
            MesasManager manager = new MesasManager();

            try
            {
                List<int> lista = manager.CantidadMesasPorEstado();
                int cerradas = 120 - (lista[0] + lista[1] + lista[2]);

                // Simulación de datos desde la base de datos
                var datosMesa = new Dictionary<string, int>
            {
                { "Disponible", lista[0] },
                { "Ocupadas", lista[1] },
                { "Pendientes", lista[2] },
                { "Cerradas", cerradas }
            };

                // Serializar los datos en formato JSON
                var json = new JavaScriptSerializer().Serialize(datosMesa);

                // Pasar la variable al frontend
                MesasJson.Value = json;

            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }

        }

        private void CargarEmpleados()
        {
            EmpleadoManager manager = new EmpleadoManager();

            try
            {
                gViewEmpleados.DataSource = manager.ObtenerCantidadPedidosPorEmpleado();
                gViewEmpleados.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        private void CargarProductos()
        {
            ProductoManager manager = new ProductoManager();
            int min = 10;
            try
            {
                gViewProductos.DataSource = manager.ObtenerProductosConBajoStock(min);
                gViewProductos.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }

        private void CargarTarjetas()
        {
            VentasManager manager = new VentasManager();
            PedidosManager managerPed = new PedidosManager();

            try
            {
                decimal total = manager.ObtenerRecaudacionDia();
                List<int> lista = managerPed.ObtenerCantidadMesasEstado();

                lblRecaudacionDiaria.Text = "$ " + total.ToString("0.0");
                lblPedidosCompletadosDia.Text = lista[0].ToString();
                lblPedidosEnCurso.Text = lista[1].ToString();
                lblPedidosCancelados.Text = lista[2].ToString();
            }
            catch (Exception ex)
            {
                Response.Write("<Script>alert('Error: " + ex.Message + "')</Script>");
            }
        }
    }
}
