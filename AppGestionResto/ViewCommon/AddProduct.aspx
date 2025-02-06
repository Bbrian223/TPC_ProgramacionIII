<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="WebApplication1.ViewCommon.AddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <<style>
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




    <h2>+ Agregar Producto</h2>

    <hr class="my-4" />

    <div class="container">

        <!-- Imagen -->
        <asp:Panel ID="panelMsgLbl" runat="server">
            <asp:Label ID="lblErrores" runat="server" Visible="false"></asp:Label>
        </asp:Panel>

        <div id="imagePreview" class="image-circle mx-auto mb-3"></div>
        <asp:FileUpload ID="fileUploadImagen" runat="server" CssClass="form-control-file mx-auto d-block" />

        <div class="row justify-content-center" style="margin-top: 20px">
            <!--Categoria-->
            <div class="col-3">
                <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                    BackColor="white" ForeColor="black" Font-Size="Large" AutoPostBack="true">
                    <asp:ListItem Text="Seleccione una categoría" Value="0" />
                    <asp:ListItem Text="Cafeteria" Value="1" />
                    <asp:ListItem Text="Entradas" Value="2" />
                    <asp:ListItem Text="Comidas" Value="3" />
                    <asp:ListItem Text="Postres" Value="4" />
                    <asp:ListItem Text="Bebidas" Value="5" />
                </asp:DropDownList>
            </div>

            <!--Nombre-->
            <div class="col-3">
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
            </div>

            <!--Precio-->
            <div class="col-3">
                <asp:TextBox ID="txtPrecio" CssClass="form-control" runat="server"
                    Placeholder="Precio" onkeypress="return soloNumeros(event);"></asp:TextBox>
            </div>

            <!--Stock-->
            <div class="col-3">
                <asp:TextBox ID="txtStock" CssClass="form-control" runat="server"
                    Placeholder="Stock" onkeypress="return soloNumeros(event);"></asp:TextBox>
            </div>
        </div>


        <div class="row justify-content-lg-start" style="margin-top: 20px">
            <!--Cafeteria-->
            <%if (string.Compare(ddlCategorias.SelectedValue, "1") == 0)
                { %>

            <div class="col-3">
                <asp:TextBox ID="txtTipoCafe" runat="server" CssClass="form-control" placeholder="Tipo Cafe"></asp:TextBox>
            </div>
            <div class="col-3">
                <asp:TextBox ID="txtTamano" runat="server" CssClass="form-control" placeholder="Tamaño"></asp:TextBox>
                <!--Podria ser un ddl-->
            </div>

            <% } %>


            <!--Entradas-->
            <%if (string.Compare(ddlCategorias.SelectedValue, "2") == 0)
                { %>

            <div class="col-3">
                <asp:TextBox ID="txtPorciones" runat="server" CssClass="form-control" placeholder="Porciones"></asp:TextBox>
            </div>

            <% } %>

            <!--Comidas-->
            <%if (string.Compare(ddlCategorias.SelectedValue, "3") == 0)
                { %>

            <div class="col-3">
                <asp:TextBox ID="txtGuarnicion" runat="server" CssClass="form-control" placeholder="Guarnicion"></asp:TextBox>
            </div>

            <% } %>

            <!--Postres-->
            <%if (string.Compare(ddlCategorias.SelectedValue, "4") == 0)
                { %>
            <div class="col-3">
                <asp:CheckBox id="chkAzucar" runat="server" Text=" " CssClass="form-check-input form-control-lg" />
                <label style="font-size: 20px">Azucar agregada</label>
            </div>

            <div class="col-3">
                <asp:CheckBox id="chkGluten" runat="server" Text=" " CssClass="form-check-input form-control-lg" />
                <label style="font-size: 20px">Gluten</label>
            </div>
            <% } %>

            <!--Bebidas-->
            <%if (string.Compare(ddlCategorias.SelectedValue, "5") == 0)
                { %>

            <div class="col-3">
                <asp:TextBox ID="txtVolumen" runat="server" CssClass="form-control" placeholder="Volumen"
                    onkeypress="return soloNumeros(event)" ></asp:TextBox>
            </div>
            <div class="col-3">
                <asp:CheckBox id="chkAlcohol" runat="server" Text=" " CssClass="form-check-input form-control-lg" />
                <label style="font-size: 20px">Alcohol</label>

            </div>

            <% } %>
        </div>

        <div class="row justify-content-center" style="margin-top: 20px">
            <!--Descripcion-->
            <div class="input-group">
                <span class="input-group-text">Descripcion</span>
                <asp:TextBox id="txtBxDescripcion" runat="server" CssClass="form-control" 
                    Font-Names="Arial" Height="80px" TextMode="MultiLine"/>
            </div>
        </div>



        <div class="row" style="margin-top: 40px;">
            <div class="col-6">
                <asp:Button ID="btnCancelar" Text="Cancelar" runat="server"
                    CssClass="btn btn-danger w-100" OnClick="btnCancelar_Click"/>
            </div>
            <div class="col-6">
                <asp:Button ID="btnGuardar" Text="Guardar" runat="server"
                    CssClass="btn btn-primary w-100" OnClick="btnGuardar_Click"/>
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
