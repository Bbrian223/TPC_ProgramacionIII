<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="WebApplication1.ViewCommon.Productos" %>

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

    <h2>Lista de Productos</h2>

    <hr class="my-4" />

    <div class="container">
        <div class="row justify-content-center" style="padding-top: 30px">
            <div class="container-fluid">
                <div class="row button-row mb-5 text-center">

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

                <!-- Botón de Agregar Producto -->
                <div class="button" style="margin-bottom: 20px;">
                    <asp:Button ID="btnAgregarProducto" Text="+ Agregar Producto" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnAgregarProducto_Click" />
                </div>

                <!-- Repeater -->
                <asp:Repeater ID="repeaterProductos" runat="server">
                    <HeaderTemplate>
                        <table class="table">
                            <thead>
                                <tr>
                                    <th scope="col">ID</th>
                                    <th scope="col">Foto</th>
                                    <th scope="col">Nombre</th>
                                    <th scope="col">Categoria</th>
                                    <th scope="col">Stock</th>
                                    <th scope="col">Opciones</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("IdProducto") %></td>
                            <td><%# Eval("Nombre") %></td>
                            <td><%# Eval("Nombre") %></td>
                            <td><%# Eval("Categoria") %></td>
                            <td><%# Eval("Stock") %></td>
                            <td>
                                <asp:Button ID="btnEditarEmpleado" class="btn btn-primary btn-sm" Text="Editar" runat="server"
                                    CommandArgument='<%# Eval("IdProducto") %>' />
                                <button type="button" class="btn btn-danger btn-sm btnEliminar"
                                    data-id='<%# Eval("IdProducto") %>'>
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
                                ¿Está seguro de que desea eliminar al Producto con ID 
                           
                            <span id="idEmpleadoSeleccionado"></span>?
                       
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                                <asp:Button ID="btnEliminarEmpleado" CssClass="btn btn-danger" runat="server" Text="Eliminar" />
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
