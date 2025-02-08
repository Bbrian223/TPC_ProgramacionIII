<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="MesasSettings.aspx.cs" Inherits="WebApplication1.ViewsManagment.MesasSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        input[type="checkbox"] {
            width: 15px;
            height: 15px;
            transform: scale(1.5);
            cursor: pointer;
            margin: 5px
        }

        input[type="radio"] {
            width: 15px;
            height: 15px;
            transform: scale(1.5); /* Aumenta el tamaño */
            cursor: pointer; /* Cambia el cursor al pasar sobre él */
            margin: 5px; /* Espaciado alrededor */
        }
    </style>


    <h3>Habilitar Salones</h3>

    <hr class="my-4" />

    <div class="container">
        <div class="row row-cols-3">
            <div class="col">
                <!--Salon 1-->
                <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:CheckBox ID="chkSalon1" runat="server" />
                    <label style="font-size: 20px">Salon 1</label>
                </div>
                <!--Salon 2-->
                <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:CheckBox ID="chkSalon2" runat="server" />
                    <label style="font-size: 20px">Salon 2</label>
                </div>
            </div>

            <div class="col">
                <!--Salon 3-->
                <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:CheckBox ID="chkSalon3" runat="server" />
                    <label style="font-size: 20px">Salon 3</label>
                </div>
                <!--Salon 4-->
                <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:CheckBox ID="chkSalon4" runat="server" />
                    <label style="font-size: 20px">Salon 4</label>
                </div>
            </div>

            <div class="col">
                <!--Salon 5-->
                <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:CheckBox ID="chkSalon5" runat="server" />
                    <label style="font-size: 20px">Salon 5</label>
                </div>
            </div>

            <div class="col" style="margin-top: 20px">
                <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server"
                        CssClass="btn btn-primary" Enabled="false" />
                </div>
            </div>

        </div>
    </div>


    <h3 style="margin-top: 40px">Habilitar Mesas</h3>

    <hr class="my-4" />

    <div class="container">
        <div class="row row-cols-3">
            <div class="col">
                <!--Radio button todas las mesas-->
                <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:RadioButton ID="rdbtnTodasMesas" runat="server" GroupName="opccionesMesas" AutoPostBack="true"/>
                    <label style="font-size: 20px">Todas las mesas</label>
                </div>
            </div>

            <div class="col">
                <!--Radio Button secmentos de mesas-->
                <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:RadioButton ID="rdbtnSegmentoMesas" runat="server" GroupName="opccionesMesas" AutoPostBack="true"/>
                    <label style="font-size: 20px">Por Segmento</label>
                </div>
            </div>
        </div>

        <%if (rdbtnTodasMesas.Checked == true) 
            {%> 
            <h3>todas las mesas</h3>  
        <%}else 
            {%> 
            <h3>segmento</h3>
        <%}%>


    </div>
</asp:Content>
