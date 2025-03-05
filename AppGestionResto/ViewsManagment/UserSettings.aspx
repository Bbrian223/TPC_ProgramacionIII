<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="UserSettings.aspx.cs" Inherits="WebApplication1.ViewsManagment.UserSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
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
    </style>

    <h2>Lista de Usuarios</h2>

    <hr class="my-4" />

    <div class="container">
        <div class="row justify-content-center" style="padding-top: 30px">
            <div class="container-fluid">
                <div class="row button-row mb-5 text-center">

                    <!-- Filtro por DNI -->
                    <div class="col-4">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtDniFiltro" CssClass="form-control" runat="server" Placeholder="DNI"
                                OnTextChanged="txtDniFiltro_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Filtro por Apellido -->
                    <div class="col-4">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtApellidoFiltro" CssClass="form-control" runat="server" Placeholder="Apellido"
                                OnTextChanged="txtApellidoFiltro_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Filtro por Nombre -->
                    <div class="col-4">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtNombreFiltro" CssClass="form-control" runat="server" Placeholder="Nombre"
                                OnTextChanged="txtNombreFiltro_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <!-- Botón de Alta Empleado -->
                <div class="button" style="margin-bottom: 20px;">
                    <asp:Button ID="btnAgregarEmpleado" Text="+ Agregar Empleado" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnAgregarEmpleado_Click" />
                </div>

                <!-- Repeater -->
                <asp:Repeater ID="repeaterEmpleados" runat="server">
                    <HeaderTemplate>
                        <table class="table">
                            <thead>
                                <tr>
                                    <th scope="col">ID</th>
                                    <th scope="col">Imagen</th>
                                    <th scope="col">Nombre</th>
                                    <th scope="col">Apellido</th>
                                    <th scope="col">Documento</th>
                                    <th scope="col">Fecha Ingreso</th>
                                    <th scope="col">Cargo</th>
                                    <th scope="col">Estado</th>
                                    <th scope="col">Opciones</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("IdEmpleado") %></td>
                            <td>
                                <asp:Image ID="imgUser" runat="server" CssClass="img-thumbnail" Width="80px" Height="80px" ImageUrl='<%# ResolveUrl(Eval("Imagen.DirComp").ToString()) %>' onerror="this.onerror=null; this.src='/Database/Imagenes/Perfiles/sin-imagen.jpg';"/></td>
                            <td><%# Eval("Nombre") %></td>
                            <td><%# Eval("Apellido") %></td>
                            <td><%# Eval("Documento") %></td>
                            <td><%# Eval("FechaIng", "{0:dd/MM/yyyy}") %></td>
                            <td><%# Eval("rol") %></td>
                            <td><%# (bool)Eval("estado") ? "Activo" : "No Activo" %></td>
                            <td>
                                <asp:Button ID="btnVerEmpleado" class="btn btn-primary btn-sm" Text="Detalles" runat="server"
                                    CommandArgument='<%# Eval("IdUsuario") %>' OnClick="btnVerEmpleado_Click" />

                                <asp:Button ID="btnValidarBaja" class="btn btn-danger btn-sm" Text="Deshabiltar" runat="server"
                                    CommandArgument='<%# Eval("IdUsuario") %>' OnClick="btnValidarBaja_Click" Visible='<%# (bool)Eval("Estado") %>'/>

                                <asp:Button ID="btnHabilitar" class="btn btn-success btn-sm" Text="    Habilitar    " runat="server"
                                    CommandArgument='<%# Eval("IdUsuario") %>' OnClick="btnHabilitar_Click" Visible='<%# !(bool)Eval("Estado") %>'/>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                   
                   
                    </FooterTemplate>
                </asp:Repeater>

                <!-- Modal de confirmación -->
                <div class="modal fade" id="modalEliminar" tabindex="-1" aria-labelledby="modalEliminarLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="modalEliminarLabel">Confirmar Eliminación</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body alert alert-danger">
                                
                                <h3>¿Está seguro de que desea dar de baja al empleado con ID: <asp:Label ID="lblModalIdBaja" runat="server" /> </h3>
                           
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                                <asp:Button ID="btnEliminarEmpleado" CssClass="btn btn-danger" runat="server" Text="Eliminar" OnClick="btnEliminarEmpleado_Click" />
                            </div>
                        </div>
                    </div>
                </div>


                <!-- Modal de ver -->
                <div class="modal fade" id="modalEditar" tabindex="-1" aria-labelledby="modalEditarLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg modal-dialog-centered">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Ver Usuario
                                    <asp:Label ID="lblModalNumUsuario" runat="server" /></h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">

                                <asp:Panel ID="panelMsgLbl" runat="server" CssClass="alert alert-danger">
                                    <asp:Label ID="lblErrores" runat="server" Visible="false"></asp:Label>
                                </asp:Panel>

                                <div class="d-flex justify-content-center">
                                    <asp:Image ID="imgPreview" CssClass="image-circle mb-3" runat="server" onerror="this.onerror=null; this.src='/Database/Imagenes/Perfiles/sin-imagen.jpg';" />
                                </div>

                                <div class="row justify-content-center" style="margin-top: 20px">
                                    <div class="col-3">
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Apellido" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox ID="txtDocumento" CssClass="form-control" runat="server"
                                            Placeholder="DNI" onkeypress="return soloNumeros(event);" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control" placeholder="Fecha Nac"
                                            type="date" Enabled="false"></asp:TextBox>
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
                                        <asp:DropDownList ID="ddlOpciones" runat="server" CssClass="form-select" Enabled="false">
                                            <asp:ListItem Text="Gerente" Value="1" />
                                            <asp:ListItem Text="Mozo" Value="2" />
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                            <div class="modal-footer">

                                <%if ((bool)Session["EditarUsuario"] == false)
                                    { %>
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Volver</button>
                                <asp:Button ID="btnEditarUsuario" CssClass="btn btn-primary" runat="server" Text="Editar" OnClick="btnEditarUsuario_Click" />
                                <%
                                    }
                                    else
                                    { %>
                                <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cancelar</button>
                                <asp:Button ID="btnGuardarEdicion" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="btnGuardarEdicion_Click" />
                                <%} %>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>
</asp:Content>
