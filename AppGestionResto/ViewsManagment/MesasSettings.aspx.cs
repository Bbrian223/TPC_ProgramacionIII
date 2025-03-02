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
                CargarDatosPagina();
                ObtenerMesas(idSalon);
            }
            else 
            { 
                ObtenerMesas( Session["idSalon"] == null ? 1 : (int)Session["idSalon"]  );
            }
        }

        protected void BtnMesa_Click(object sender, EventArgs e)
        {
            MesasManager manager = new MesasManager();
            Button clickedButton = (Button)sender;
            string mesaSeleccionada = clickedButton.ID;

            try
            {
                manager.ActualizarEstadoMesa(int.Parse(mesaSeleccionada),rdBtnHab.Checked);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

            ObtenerMesas(Session["idSalon"] == null ? 1 : (int)Session["idSalon"]);
        }

        protected void ddlSalones_TextChanged(object sender, EventArgs e)
        {
            string salon = ddlSalones.SelectedValue;
            idSalon = int.Parse(salon);

            ObtenerMesas(idSalon);

            Session["idSalon"] = idSalon;
        }

        protected void btnGuardarSalonesHab_Click(object sender, EventArgs e)
        {
            List<Salon> listaSalonesChk = new List<Salon>();
            string Salones = string.Empty;

            listaSalonesChk.Add(new Salon(1,chkSalon1.Checked));
            listaSalonesChk.Add(new Salon(2, chkSalon2.Checked));
            listaSalonesChk.Add(new Salon(3, chkSalon3.Checked));
            listaSalonesChk.Add(new Salon(4, chkSalon4.Checked));
            listaSalonesChk.Add(new Salon(5, chkSalon5.Checked));

            for (int i = 0; i < listaSalonesChk.Count; i++)
            {
                if (listaSalonesChk[i].Estado == false) 
                {
                    
                    Salones += listaSalonesChk[i].IdSalon.ToString();
                    Salones += "  ";
                }
            }

            CargarModalError(Salones, true);
            Session.Add("ListaSalonesChk",listaSalonesChk);
        }

        protected void btnModalAceptar_Click(object sender, EventArgs e)
        {
            MesasManager manager = new MesasManager();
            List<Salon> lista = (List<Salon>)Session["ListaSalonesChk"];

            try
            {
                foreach (var item in lista)
                {
                    manager.ActualizarEstadoSalon(item.IdSalon,item.Estado);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

        }

        //funciones

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

            ObtenerMesas(Session["idSalon"] == null ? 1 : (int)Session["idSalon"]);
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

            rdBtnHab.Checked = true;

            idSalon = 1;
            Session.Add("idSalon", idSalon);
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

        public void CargarModalError(string msg, bool btnEstado)
        {
            btnModalAceptar.Visible = btnEstado;
            
            if(msg == string.Empty)
                lblModalError.Text = "Salones a deshabilitar: Ninguno";
            else 
                lblModalError.Text = "Salones a deshabilitar: " + msg;

            ClientScript.RegisterStartupScript(this.GetType(), "Error", "var modal = new bootstrap.Modal(document.getElementById('modalError')); modal.show();", true);
        }
    }
}