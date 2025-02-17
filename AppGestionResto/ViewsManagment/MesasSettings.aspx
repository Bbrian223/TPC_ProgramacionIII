<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="MesasSettings.aspx.cs" Inherits="WebApplication1.ViewsManagment.MesasSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .mesa-cuadrado {
            display: flex;
            align-items: center;
            justify-content: center;
            width: 60px;
            height: 60px;
            background-color: #28a745; /* Verde para disponible */
            color: white;
            font-weight: bold;
            border-radius: 8px;
            transition: transform 0.2s, background-color 0.2s;
            margin: 20px;
        }

        .mesa-cerrada {
            background-color: #6c757d; /* Gris para cerrada */
        }

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

    <!--Switch para habilitar salones-->
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

        </div>

        <!--Boton Guardar Salones-->
        <div style="margin-top: 30px; margin-left: 23px">
            <asp:Button ID="btnGuardarSalonesHab" Text="Guardar" runat="server" CssClass="btn btn-primary btn-sm"
                OnClick="btnGuardarSalonesHab_Click" />
        </div>
    </div>


    <h3 style="margin-top: 30px">Habilitar Mesas</h3>

    <hr class="my-4" />

    <!--Habilitaciones de mesas-->
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
                <!--Radio Button segmentos de mesas-->
                <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                    <asp:RadioButton ID="rdbtnSeleccionar" runat="server" GroupName="opccionesMesas" AutoPostBack="true" />
                    <label style="font-size: 20px">Seleccionar</label>
                </div>
            </div>
        </div>

        <%if (rdbtnSeleccionar.Checked)
            { %>
        <div class="container mt-4">
            <div class="row row-cols-3">
                <!--DropDownList 1-->
                <div class="col">
                    <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <asp:DropDownList ID="ddlSalones" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                            BackColor="white" ForeColor="black" Font-Size="Large" OnTextChanged="ddlSalones_TextChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <!--Habilitar 1-->
                <div class="col">
                    <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <asp:RadioButton ID="rdBtnHab" runat="server" GroupName="opcHabMesa" />
                        <label style="font-size: 20px">Habilitar</label>

                    </div>
                </div>
                <!--Deshabilitar 1-->
                <div class="col">
                    <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <asp:RadioButton ID="rdBtnDeshab" runat="server" GroupName="opcHabMesa" />
                        <label style="font-size: 20px">Deshabilitar</label>
                    </div>
                </div>
            </div>
            <div class="row" id="ContenedorMesas" runat="server" style="margin-top: 20px;">
                <!-- 5 filas con 5 mesas cada una -->
            </div>
        </div>
        <%} %>
    </div>



    <!-- Modal Error-->
    <div class="modal fade" id="modalError" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Confirmacion Salones</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body d-flex flex-column gap-3" role="alert">

                    <asp:Label Text="Esta seguro que desea guardar los cambios?" runat="server" Font-Size="Large"/>
                    <asp:Label Text="Se cerraran las mesas que se encuentren en salones deshabilitados" runat="server" Font-Size="Large" ForeColor="red"/>
                    <asp:Label ID="lblModalError" runat="server" Font-Size="Large" />

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnModalAceptar" Text="Aceptar" runat="server" CssClass="btn btn-primary" 
                        OnClick="btnModalAceptar_Click"/>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
