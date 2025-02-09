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
                    <asp:CheckBox ID="chkSalon1" runat="server" AutoPostBack="true"/>
                    <label style="font-size: 20px">Salon 1</label>
                </div>
                <!--Salon 2-->
                <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:CheckBox ID="chkSalon2" runat="server" AutoPostBack="true"/>
                    <label style="font-size: 20px">Salon 2</label>
                </div>
            </div>

            <div class="col">
                <!--Salon 3-->
                <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:CheckBox ID="chkSalon3" runat="server" AutoPostBack="true"/>
                    <label style="font-size: 20px">Salon 3</label>
                </div>
                <!--Salon 4-->
                <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:CheckBox ID="chkSalon4" runat="server" AutoPostBack="true"/>
                    <label style="font-size: 20px">Salon 4</label>
                </div>
            </div>

            <div class="col">
                <!--Salon 5-->
                <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:CheckBox ID="chkSalon5" runat="server" AutoPostBack="true"/>
                    <label style="font-size: 20px">Salon 5</label>
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
                    <asp:RadioButton ID="rdbtnTodasMesas" runat="server" GroupName="opccionesMesas" AutoPostBack="true" />
                    <label style="font-size: 20px">Habilitar todas</label>
                </div>
            </div>

            <div class="col" style="margin-bottom: 40px">
                <!--Radio Button secmentos de mesas-->
                <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:RadioButton ID="rdbtnSeleccionar" runat="server" GroupName="opccionesMesas" AutoPostBack="true" />
                    <label style="font-size: 20px">Seleccionar</label>
                </div>
            </div>
        </div>

        <%if (rdbtnSeleccionar.Checked == true)
            {%>

            <%if (chkSalon1.Checked)
                { %>
                <!--Salon 1-->
                <div class="col" style="margin-bottom: 20px">
                    <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <label style="font-size: 20px; margin-right: 10px">Salon 1</label>
                        <asp:TextBox ID="txtInicioS1" runat="server" CssClass="form-control-sm" placeholder="1"></asp:TextBox>
                        <label style="font-size: 20px; margin-right: 10px; margin-left: 10px">A</label>
                        <asp:TextBox ID="txtfinS1" runat="server" CssClass="form-control-sm" placeholder="24"></asp:TextBox>
                    </div>
                </div>
            <%} %>

            <%if (chkSalon2.Checked)
                { %>
                <!--Salon 2-->
                <div class="col" style="margin-bottom: 20px">
                    <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <label style="font-size: 20px; margin-right: 10px">Salon 2</label>
                        <asp:TextBox ID="txtInicioS2" runat="server" CssClass="form-control-sm" placeholder="25"></asp:TextBox>
                        <label style="font-size: 20px; margin-right: 10px; margin-left: 10px">A</label>
                        <asp:TextBox ID="txtFinS2" runat="server" CssClass="form-control-sm" placeholder="48"></asp:TextBox>
                    </div>
                </div>
            <%} %>

            <%if (chkSalon3.Checked)
                { %>
                <!--Salon 3-->
                <div class="col" style="margin-bottom: 20px">
                    <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <label style="font-size: 20px; margin-right: 10px">Salon 3</label>
                        <asp:TextBox ID="txtInicioS3" runat="server" CssClass="form-control-sm" placeholder="49"></asp:TextBox>
                        <label style="font-size: 20px; margin-right: 10px; margin-left: 10px">A</label>
                        <asp:TextBox ID="txtfinS3" runat="server" CssClass="form-control-sm" placeholder="72"></asp:TextBox>
                    </div>
                </div>
            <%} %>

            <%if (chkSalon4.Checked)
                { %>
                <!--Salon 4-->
                <div class="col" style="margin-bottom: 20px">
                    <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <label style="font-size: 20px; margin-right: 10px">Salon 4</label>
                        <asp:TextBox ID="txtInicioS4" runat="server" CssClass="form-control-sm" placeholder="73"></asp:TextBox>
                        <label style="font-size: 20px; margin-right: 10px; margin-left: 10px">A</label>
                        <asp:TextBox ID="txtfinS4" runat="server" CssClass="form-control-sm" placeholder="96"></asp:TextBox>
                    </div>
                </div>
            <%} %>

            <%if (chkSalon5.Checked)
                { %>
                <!--Salon 5-->
                <div class="col" style="margin-bottom: 20px">
                    <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <label style="font-size: 20px; margin-right: 10px">Salon 5</label>
                        <asp:TextBox ID="txtInicioS5" runat="server" CssClass="form-control-sm" placeholder="97"></asp:TextBox>
                        <label style="font-size: 20px; margin-right: 10px; margin-left: 10px">A</label>
                        <asp:TextBox ID="txtfinS5" runat="server" CssClass="form-control-sm" placeholder="120"></asp:TextBox>
                    </div>
                </div>
            <%} %>

        <%}%>
    </div>

</asp:Content>
