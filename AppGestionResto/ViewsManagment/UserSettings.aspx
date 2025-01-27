<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="UserSettings.aspx.cs" Inherits="WebApplication1.ViewsManagment.UserSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script>
        $(document).on('click', '.btnEliminar', function () {
            // Obtener el valor del atributo data-id
            var idEmpleado = $(this).data('id');

            // Abrir el modal y mostrar el valor
            $('#modalEliminar').modal('show');
            $('#idEmpleadoSeleccionado').text(idEmpleado);

            // Almacenar el valor en el campo oculto
            $('#<%= hiddenFieldIdEmpleado.ClientID %>').val(idEmpleado);
        });
    </script>

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
                            <div class="input-group-append">
                                <asp:Button ID="btnBuscarDni" CssClass="btn btn-outline-secondary" runat="server" Text="Buscar" OnClick="btnBuscarDni_Click" />
                            </div>
                        </div>
                    </div>

                    <!-- Filtro por Apellido -->
                    <div class="col-4">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtApellidoFiltro" CssClass="form-control" runat="server" Placeholder="Apellido" 
                                OnTextChanged="txtApellidoFiltro_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <div class="input-group-append">
                                <asp:Button ID="btnBuscarApellido" CssClass="btn btn-outline-secondary" runat="server" Text="Buscar" OnClick="btnBuscarApellido_Click" />
                            </div>
                        </div>
                    </div>

                    <!-- Filtro por Nombre -->
                    <div class="col-4">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtNombreFiltro" CssClass="form-control" runat="server" Placeholder="Nombre" 
                                OnTextChanged="txtNombreFiltro_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <div class="input-group-append">
                                <asp:Button ID="btnBuscarNombre" CssClass="btn btn-outline-secondary" runat="server" Text="Buscar" OnClick="btnBuscarNombre_Click" />
                            </div>
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
                                <button type="button" class="btn btn-danger btn-sm btnEliminar"
                                    data-id='<%# Eval("IdEmpleado") %>'>
                                    Eliminar
                               
                                </button>
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
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="modalEliminarLabel">Confirmar Eliminación</h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                ¿Está seguro de que desea eliminar al empleado con ID 
                               
                                <span id="idEmpleadoSeleccionado"></span>?
                           
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                                <asp:Button ID="btnEliminarEmpleado" CssClass="btn btn-danger" runat="server" Text="Eliminar" OnClick="btnEliminarEmpleado_Click"/>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Campo oculto para enviar al servidor -->
                <asp:HiddenField ID="hiddenFieldIdEmpleado" runat="server" />

            </div>
        </div>
    </div>
</asp:Content>
