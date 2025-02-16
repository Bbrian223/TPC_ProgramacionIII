using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Manager;

namespace WebApplication1.ViewsManagment
{
    public partial class MesasSettings : System.Web.UI.Page
    {   
        public List<Mesa> listaMesas = new List<Mesa>();
        public List<Salon> listaSalon = new List<Salon>();
        public int idSalon;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenerSalones();
            }
            ObtenerMesas(1);
        }

        protected void BtnMesa_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string mesaSeleccionada = clickedButton.ID;

            if (rdBtnDeshab.Checked == true)
            {
                clickedButton.CssClass = "mesa-cuadrado mesa-cerrada text-decoration-none text-center";
            }
            else
            {
                clickedButton.CssClass = "mesa-cuadrado text-decoration-none text-center";
            }

        }

        protected void ddlSalones_TextChanged(object sender, EventArgs e)
        {
            string salon = ddlSalones.SelectedValue;
            idSalon = int.Parse(salon);
            ObtenerMesas(idSalon);
        }


        //funciones

        public void ObtenerSalones()
        {
            MesasManager manager = new MesasManager();

            try
            {
                listaSalon = manager.ObtenerListaTodosSalones();
                CargarDatosPagina();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

            ObtenerMesas(idSalon);
        }

        public void CargarDatosPagina()
        {

            chkSalon1.Checked = (listaSalon[0] is null) ? false : listaSalon[0].Estado;
            chkSalon2.Checked = (listaSalon[1] is null) ? false : listaSalon[1].Estado;
            chkSalon3.Checked = (listaSalon[2] is null) ? false : listaSalon[2].Estado; 
            chkSalon4.Checked = (listaSalon[3] is null) ? false : listaSalon[3].Estado;
            chkSalon5.Checked = (listaSalon[4] is null) ? false : listaSalon[4].Estado;

            ddlSalones.DataSource = listaSalon;
            ddlSalones.DataTextField = "Nombre";
            ddlSalones.DataValueField = "IdSalon";
            ddlSalones.DataBind();

            idSalon = 1;
        }

        public void ObtenerMesas(int salon)
        {
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

                    if (!item.Habilitado)
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

    }
}