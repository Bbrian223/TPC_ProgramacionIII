<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="UserSettings.aspx.cs" Inherits="WebApplication1.ViewsManagment.UserSettings" %>

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
                            <asp:TextBox ID="txtDniFiltro" CssClass="form-control" runat="server" Placeholder="DNI"></asp:TextBox>

                            <div class="input-group-append">
                                <asp:Button ID="btnBuscarDni" CssClass="btn btn-outline-secondary" runat="server" Text="Buscar" OnClick="btnBuscarDni_Click" />
                            </div>
                        </div>
                    </div>

                    <!-- Filtro por Apellido -->
                    <div class="col-4">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtApellidoFiltro" CssClass="form-control" runat="server" Placeholder="Apellido"></asp:TextBox>
                            <div class="input-group-append">
                                <asp:Button ID="btnBuscarApellido" CssClass="btn btn-outline-secondary" runat="server" Text="Buscar" OnClick="btnBuscarApellido_Click" />
                            </div>
                        </div>
                    </div>

                    <!-- Filtro por Nombre -->
                    <div class="col-4">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtNombreFiltro" CssClass="form-control" runat="server" Placeholder="Nombre"></asp:TextBox>
                            <div class="input-group-append">
                                <asp:Button ID="btnBuscarNombre" CssClass="btn btn-outline-secondary" runat="server" Text="Buscar" OnClick="btnBuscarNombre_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <!--Boton de Alta Empleado-->
                <div class="button" style="margin-bottom: 20px;">
                    <button type="button" class="btn btn-primary btn-sm" data-bs-toggle="modal" data-bs-target="#nuevoUsuario">
                        + Agregar Empleado
                    </button>
                </div>

                <asp:Repeater ID="repeaterEmpleados" runat="server">
                    <HeaderTemplate>
                        <table class="table">
                            <thead>
                                <tr>
                                    <!--<th scope="col">Foto</th>-->
                                    <th scope="col">ID</th>
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
                            <td><%# Eval("Nombre") %></td>
                            <td><%# Eval("Apellido") %></td>
                            <td><%# Eval("Documento") %></td>
                            <td><%# Eval("FechaIng", "{0:dd/MM/yyyy}") %></td>
                            <td><%# Eval("rol") %></td>
                            <td><%# Eval("estado") %></td>
                            <td>
                                <asp:Button ID="btnEditarEmpleado" class="btn btn-primary btn-sm" Text="Editar" runat="server"
                                    CommandArgument='<%# Eval("IdEmpleado") %>' OnClick="btnEditarEmpleado_Click" />
                                <asp:Button ID="btnBajaEmpleado" class="btn btn-danger btn-sm" Text="Eliminar" runat="server"
                                    CommandArgument='<%# Eval("IdEmpleado") %>' OnClick="btnBajaEmpleado_Click" />
                            </td>

                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
   
                    </FooterTemplate>
                </asp:Repeater>


                <!--Fin tabla-->


                <!-- Inicio Pop Up -->
                <div class="modal fade" id="nuevoUsuario" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Nuevo Empleado</h5>
                            </div>
                            <div class="modal-body text-center">

                                <div id="imagePreview" class="image-circle mx-auto mb-3"></div>
                                <asp:FileUpload ID="fileUploadImagen" runat="server" CssClass="form-control-file mx-auto d-block" />

                                <div class="row justify-content-center" style="margin-top: 20px">
                                    <div class="col-3">
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Apellido"></asp:TextBox>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" placeholder="Documento"></asp:TextBox>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control" placeholder="Fecha Nac"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row justify-content-center" style="margin-top: 20px">
                                    <div class="col-3">
                                        <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" placeholder="Calle"></asp:TextBox>
                                    </div>
                                    <div class="col-3">
                                        <asp:TextBox ID="txtNumDir" runat="server" CssClass="form-control" placeholder="Numero"></asp:TextBox>
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
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                    </div>
                                    <div class="col-4">
                                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Telefono"></asp:TextBox>
                                    </div>
                                    <div class="col-4">
                                        <asp:DropDownList ID="ddlOpciones" runat="server" CssClass="form-select">
                                            <asp:ListItem Text="Seleccione una opcion" Value="0" />
                                            <asp:ListItem Text="Gerente" Value="1" />
                                            <asp:ListItem Text="Mozo" Value="2" />
                                        </asp:DropDownList>
                                    </div>
                                </div>


                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cancelar</button>
                                <button type="button" class="btn btn-primary">Guardar</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Fin Pop Up -->



            </div>
        </div>
    </div>

</asp:Content>
