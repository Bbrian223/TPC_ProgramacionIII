using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using Dominio;
using Manager;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.ViewsManagment
{
    public partial class Home : System.Web.UI.Page
    {
        public List<Mesa> listaMesas = new List<Mesa>();
        public List<Salon> listaSalon = new List<Salon>();
        public int idSalon;

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarSalones();
        }

        protected void BtnMesa_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string mesaSeleccionada = clickedButton.ID;

            Response.Redirect("~/ViewCommon/OrderDetail.aspx?mesa="+mesaSeleccionada, false);

        }

        protected void BtnPagina_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string opc = btn.Text;

            int paginaActual = ViewState["PaginaActual"] != null ? (int)ViewState["PaginaActual"] : 1;

            switch (opc)
            {
                case "Atras":
                    if (paginaActual > 1) paginaActual--;
                    break;
                case "Siguiente":
                    if (paginaActual < listaSalon.Count) paginaActual++; // Cambiar 5 por el total de páginas dinámico
                    break;
                default:
                    int numero = int.Parse(opc);

                    paginaActual = (numero > 0) ? numero : 1;
                    break;
            }

            ViewState["PaginaActual"] = paginaActual;
            CargarSalones();
        }

        //funciones

        public void CargarMesas()
        {
            MesasManager manager = new MesasManager();
            //idSalon = 1;

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

        public void CargarSalones() 
        {
            MesasManager manager = new MesasManager();
            try
            {
                listaSalon = manager.ObtenerListaSalonesHabilitados();
                GenerarBotonesPaginacion();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

            CargarMesas();
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

        private void GenerarBotonesPaginacion()
        {
            ContenedorPaginador.Controls.Clear();

            int paginaActual = ViewState["PaginaActual"] != null ? (int)ViewState["PaginaActual"] : 1;

            //Boton pagina anterior 
            LinkButton btnAtras = new LinkButton();
            btnAtras.ID = "Atras";
            btnAtras.Text = "Atras";
            btnAtras.CssClass = "page-link";
            btnAtras.Enabled = paginaActual > 1;
            btnAtras.Click += BtnPagina_Click;
            ContenedorPaginador.Controls.Add(btnAtras);

            // Botones numéricos
            for (int i = 0; i < listaSalon.Count; i++)
            {
                if (!listaSalon[i].Estado)
                    continue;

                string activeClass = (i+1 == paginaActual) ? " active" : "";

                LinkButton btnPagina = new LinkButton();

                btnPagina.ID = "pag_" + listaSalon[i].IdSalon;
                btnPagina.Text = (i+1).ToString();
                btnPagina.CssClass = $"page-link{activeClass}";
                btnPagina.Click += BtnPagina_Click;

                ContenedorPaginador.Controls.Add(btnPagina);
            }

            //Boton de siguiente pagina
            LinkButton btnSte = new LinkButton();
            btnSte.ID = "Siguiente";
            btnSte.Text = "Siguiente";
            btnSte.CssClass = "page-link";
            btnSte.Enabled = paginaActual < listaSalon.Count;
            btnSte.Click += BtnPagina_Click;
            ContenedorPaginador.Controls.Add(btnSte);

            ViewState["PaginaActual"] = paginaActual;
            idSalon = (int)listaSalon[paginaActual-1].IdSalon;
            lblIdSalon.Text = idSalon.ToString();
        }

    }
}