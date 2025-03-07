<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="AdditionalSetting.aspx.cs" Inherits="WebApplication1.ViewsManagment.ProdextraSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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

        .lblModal {
            font-size: 10px;
        }
    </style>

    <h2>Lista de Adicionales</h2>
    <hr class="my-4" />
    <!--Pantalla Principal-->
    <div class="container">
        <div class="row justify-content-center" style="padding-top: 30px">
            <div class="container-fluid">
                <div class="row button-row mb-5 text-center">

                    <!-- Filtro por Nombre -->
                    <div class="col-4">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtNombreFiltro" CssClass="form-control" runat="server" Placeholder="Nombre"
                                OnTextChanged="txtNombreFiltro_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>

                    <!--Categoria-->
                    <div class="col-3">
                        <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                            BackColor="white" ForeColor="black" Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged">
                            <asp:ListItem Text="Todos las categorias" Value="0" />
                            <asp:ListItem Text="Guarniciones" Value="6" />
                            <asp:ListItem Text="Tipo de Leche" Value="7" />
                            <asp:ListItem Text="Tipo de Taza" Value="8" />
                        </asp:DropDownList>
                    </div>

                </div>

                <%if (Dominio.Seguridad.NivelAcceso == Dominio.UserType.Gerente)
                    {  %>
                <!-- Botón de Agregar Producto -->
                <div class="button" style="margin-bottom: 20px;">
                    <asp:Button ID="btnAgregarAdicional" Text="+ Agregar Adicional" runat="server" CssClass="btn btn-primary btn-sm"
                        OnClick="btnAgregarAdicional_Click" />
                </div>
                <%} %>

                <!-- Repeater -->
                <div class="text-center">
                    <asp:Repeater ID="repeaterAdicionales" runat="server">
                        <HeaderTemplate>
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th scope="col">ID</th>
                                        <th scope="col">Categoria</th>
                                        <th scope="col">Nombre</th>
                                        <th scope="col">Estado</th>
                                        <th scope="col">Opciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("IdProducto") %></td>
                                <td><%# Eval("Categoria.Nombre") %></td>
                                <td><%# Eval("Nombre") %></td>
                                <td><%# (bool)Eval("Estado") ? "Activo" : "No Activo" %></td>
                                <td>
                                    <asp:Button ID="btnVerAdicional" class="btn btn-primary btn-sm" Text="Detalles" runat="server"
                                        CommandArgument='<%# Eval("IdProducto") %>' OnClick="btnVerAdicional_Click" />
                                    <%if (Dominio.Seguridad.NivelAcceso == Dominio.UserType.Gerente)
                                        { %>

                                    <asp:Button ID="btnBajaAdicional" class="btn btn-danger btn-sm" Text="Deshabilitar" runat="server"
                                        CommandArgument='<%# Eval("IdProducto") %>' OnClick="btnBajaAdicional_Click" Visible='<%# (bool)Eval("Estado") %>' />

                                    <asp:Button ID="btnHabilitarAd" class="btn btn-success btn-sm" Text="    Habilitar    " runat="server"
                                        CommandArgument='<%# Eval("IdProducto") %>' OnClick="btnHabilitarAd_Click" Visible='<%# !(bool)Eval("Estado") %>' />
                                    <%} %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
</table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>

            </div>
        </div>
    </div>

    <!-- Modal de confirmación baja-->
    <div class="modal fade" id="modalEliminar" tabindex="-1" aria-labelledby="modalEliminarLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalEliminarLabel">Confirmar Eliminación Producto:
                        <asp:Label ID="lblIdProdBaja" runat="server" /></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body alert alert-danger">

                    <h4>Esta seguro que desea dar de baja:
                        <asp:Label ID="lblModalBajaAdicional" runat="server" /></h4>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarBaja" CssClass="btn btn-danger" runat="server" Text="Eliminar" OnClick="btnConfirmarBaja_Click" />
                </div>
            </div>
        </div>
    </div>


    <!-- Modal de Editar / ver-->
    <div class="modal fade" id="modalEditar" tabindex="-1" aria-labelledby="modalEditarLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Ver Adicional: 
                                <asp:Label ID="lblModalNumAdicional" runat="server" /></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <asp:Panel ID="panelMsgLbl" runat="server" CssClass="alert alert-danger">
                        <asp:Label ID="lblErrores" runat="server" Visible="false"></asp:Label>
                    </asp:Panel>

                    <div class="row justify-content-center" style="margin-top: 20px">
                        <!--Categoria-->
                        <div class="col-3">
                            <label for="ddlCategoriasModalVer" class="lblModal">Categoria:</label>
                            <asp:DropDownList ID="ddlCategoriasModalVer" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                                BackColor="white" ForeColor="black" Font-Size="Large" Enabled="false" Width="180px" Height="40px">
                                <asp:ListItem Text="Guarniciones" Value="6" />
                                <asp:ListItem Text="Tipo de Leche" Value="7" />
                                <asp:ListItem Text="Tipo de Taza" Value="8" />
                            </asp:DropDownList>
                        </div>

                        <!--Nombre-->
                        <div class="col-5">
                            <label for="txtNombre" class="lblModal">Nombre:</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre"
                                Enabled="false"></asp:TextBox>
                        </div>

                        <!--Precio-->
                        <div class="col-4">
                            <label for="txtPrecio" class="lblModal">Precio:</label>
                            <asp:TextBox ID="txtPrecio" CssClass="form-control" runat="server"
                                Placeholder="Precio" onkeypress="return soloNumeros(event);"></asp:TextBox>
                        </div>

                    </div>

                    <div class="row justify-content-center" style="margin-top: 20px">
                        <!--Descripcion-->
                        <div class="input-group">
                            <span class="input-group-text">Descripcion</span>
                            <asp:TextBox ID="txtBxDescripcion" runat="server" CssClass="form-control"
                                Font-Names="Arial" Height="80px" TextMode="MultiLine" />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <%if (!EditarAdicional)
                        {%>
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Volver</button>
                    <%if (Dominio.Seguridad.NivelAcceso == Dominio.UserType.Gerente)
                        {  %>
                    <asp:Button ID="btnEditarAdicional" CssClass="btn btn-primary" runat="server" Text="Editar" OnClick="btnEditarAdicional_Click" />
                    <%} %>

                    <%}
                        else
                        {  %>
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarEdicion" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="btnGuardarEdicion_Click" />
                    <%} %>
                </div>
            </div>
        </div>
    </div>



    <!-- Modal NUEVO -->
    <div class="modal fade" id="modalNuevoAd" tabindex="-1" aria-labelledby="modalNuevoAdLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Agregar Adicional: </h5>
                </div>
                <div class="modal-body">

                    <asp:Panel ID="panleModalAgregar" runat="server" CssClass="alert alert-danger">
                        <asp:Label ID="lblModalAgregar" runat="server" Visible="false"></asp:Label>
                    </asp:Panel>

                    <div class="row justify-content-center" style="margin-top: 20px">
                        <!--Categoria-->
                        <div class="col-4">
                            <asp:DropDownList ID="ddlModalAgregar" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                                BackColor="white" ForeColor="black" Font-Size="Large" Width="180px" Height="40px">
                                <asp:ListItem Text="Seleccione una opcion" Value="0" />
                                <asp:ListItem Text="Guarniciones" Value="6" />
                                <asp:ListItem Text="Tipo de Leche" Value="7" />
                                <asp:ListItem Text="Tipo de Taza" Value="8" />
                            </asp:DropDownList>
                        </div>

                        <!--Nombre-->
                        <div class="col-4">
                            <asp:TextBox ID="txtNombreModalAgregar" runat="server" CssClass="form-control"
                                placeholder="Nombre"></asp:TextBox>
                        </div>

                        <!--Precio-->
                        <div class="col-4">
                            <asp:TextBox ID="txtPrecioModalAgregar" CssClass="form-control" runat="server"
                                Placeholder="Precio" onkeypress="return soloNumeros(event);"></asp:TextBox>
                        </div>

                    </div>

                    <div class="row justify-content-center" style="margin-top: 20px">
                        <!--Descripcion-->
                        <div class="input-group">
                            <span class="input-group-text">Descripcion</span>
                            <asp:TextBox ID="txtDescripcionModalAgregar" runat="server" CssClass="form-control"
                                Font-Names="Arial" Height="80px" TextMode="MultiLine" />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarNuevoAdicional" CssClass="btn btn-primary" runat="server" Text="Guardar"
                        OnClick="btnGuardarNuevoAdicional_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
