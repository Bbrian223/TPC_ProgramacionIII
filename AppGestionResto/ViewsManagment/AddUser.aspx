<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="WebApplication1.ViewsManagment.AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .image-circle {
            width: 150px;
            height: 150px;
            border-radius: 50%;
            border: 2px solid #ccc;
            background-color: #f8f9fa;
            background-size: cover;
            background-position: center;
        }

        .error {
            border: 2px solid red;
            background-color: #ffe6e6;
        }

        .MsgSucces {
            text-align: center;
            background-color: lightgreen;
            color: green;
            font-size: 20px;
            height: 40px;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .MsgError {
            text-align: center;
            background-color: #ff6a6a;
            color: white;
            font-size: 20px;
            height: 40px;
            margin-top: 10px;
            margin-bottom: 10px;
        }
    </style>




    <h2>+ Agregar Empleado</h2>

    <hr class="my-4" />

    <div class="container">

        <asp:Panel ID="panelMsgLbl" runat="server">
            <asp:Label ID="lblErrores" runat="server" Visible="false"></asp:Label>
        </asp:Panel>

        <div class="d-flex justify-content-center">
            <asp:Image ID="imgPreview" CssClass="image-circle mb-3" runat="server" />
        </div>

        <asp:FileUpload ID="fileUploadImagen" runat="server" CssClass="form-control" onchange="javascript:__doPostBack('<%= fileUploadImagen.ClientID %>', '');" />


        <div class="row justify-content-center" style="margin-top: 20px">
            <div class="col-3">
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
            </div>
            <div class="col-3">
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Apellido"></asp:TextBox>
            </div>
            <div class="col-3">
                <asp:TextBox ID="txtDocumento" CssClass="form-control" runat="server"
                    Placeholder="DNI" onkeypress="return soloNumeros(event);"></asp:TextBox>
            </div>
            <div class="col-3">
                <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control" placeholder="Fecha Nac"
                    type="date"></asp:TextBox>
            </div>
        </div>

        <div class="row justify-content-center" style="margin-top: 20px">
            <div class="col-3">
                <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" placeholder="Calle"></asp:TextBox>
            </div>
            <div class="col-3">
                <asp:TextBox ID="txtNumDir" CssClass="form-control" runat="server"
                    Placeholder="Numero" onkeypress="return soloNumeros(event);"></asp:TextBox>
            </div>
            <div class="col-3">
                <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" placeholder="Localidad"></asp:TextBox>
            </div>
            <div class="col-3">
                <asp:TextBox ID="txtCodPostal" runat="server" CssClass="form-control" placeholder="Cod Postal"></asp:TextBox>
            </div>
        </div>


        <div class="row justify-content-center" style="margin-top: 20px">
            <div class="col-4">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" TextMode="Email"></asp:TextBox>
            </div>
            <div class="col-4">
                <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server"
                    Placeholder="Telefono" onkeypress="return soloNumeros(event);"></asp:TextBox>
            </div>
            <div class="col-4">
                <asp:DropDownList ID="ddlOpciones" runat="server" CssClass="form-select" AutoPostBack="true">
                    <asp:ListItem Text="Seleccione una opcion" Value="0" />
                    <asp:ListItem Text="Gerente" Value="1" />
                    <asp:ListItem Text="Mozo" Value="2" />
                </asp:DropDownList>
            </div>
        </div>

        <div class="row" style="margin-top: 40px;">
            <div class="col-6">
                <asp:Button ID="btnCancelar" Text="Cancelar" runat="server"
                    CssClass="btn btn-danger w-100"
                    OnClick="btnCancelar_Click" />
            </div>
            <div class="col-6">
                <asp:Button ID="btnGuardar" Text="Guardar" runat="server"
                    CssClass="btn btn-primary w-100"
                    OnClick="btnGuardar_Click" />
            </div>
        </div>


    </div>

    <script>
        function soloNumeros(e) {
            var key = e.which || e.keyCode;
            // Permitir teclas como Backspace (8), Tab (9), Enter (13), Delete (46), etc.
            if (key === 8 || key === 9 || key === 13 || key === 46 || (key >= 37 && key <= 40)) {
                return true;
            }
            // Validar si es un número (teclas 0-9)
            if (key >= 48 && key <= 57) {
                return true;
            }
            // Evitar cualquier otro carácter
            return false;
        }
    </script>


</asp:Content>
