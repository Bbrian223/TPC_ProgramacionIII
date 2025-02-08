using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Manager;

namespace WebApplication1.ViewsManagment
{
    public partial class Home : System.Web.UI.Page
    {
        public List<Mesa> listaMesas = new List<Mesa>(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarMesas();   
        }

        protected void BtnMesa_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string mesaSeleccionada = clickedButton.ID;

            // Cambia la clase del botón seleccionado
            //clickedButton.CssClass = "mesa-cuadrado mesa-ocupada";

            Response.Write("<script>alert(' MESA SELECCIONADA: " + mesaSeleccionada + "');</script>");

        }

        public void CargarMesas()
        {
            MesasManager manager = new MesasManager();
            int idSalon = 1;

            try
            {
                listaMesas = manager.ObtenerMesasPorSalon(idSalon);
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

            try
            {
                foreach (Mesa item in listaMesas)
                {
                    Button btnMesa = new Button();
                    int idMesa = (int)item.IdMesa;

                    btnMesa.ID = idMesa.ToString() ;
                    btnMesa.Text = "Mesa " + (int)item.IdMesa;
                    btnMesa.Click += BtnMesa_Click; // manejar evento

                    switch (item.EstadoMesa)
                    {
                        case "DISPONIBLE":
                            btnMesa.CssClass = "mesa-cuadrado text-decoration-none text-center";
                            break;

                        case "OCUPADA":
                            btnMesa.CssClass = "mesa-cuadrado mesa-ocupada text-decoration-none text-center";
                            break;

                        case "PENDIENTE":
                            btnMesa.CssClass = "mesa-cuadrado mesa-pendiente text-decoration-none text-center";
                            break;
                    }

                    if (!item.Habilitado)
                        btnMesa.CssClass = "mesa-cuadrado mesa-cerrada text-decoration-none text-center";

                    ContenedorMesas.Controls.Add(btnMesa);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}