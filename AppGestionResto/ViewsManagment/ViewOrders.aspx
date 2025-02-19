<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="ViewOrders.aspx.cs" Inherits="WebApplication1.ViewsManagment.ViewOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Lista de Pedidos </h2>

    <hr class="my-4" />

    <div class="container">
        <div class="row justify-content-center" style="padding-top: 30px">
            <div class="container-fluid">
                <div class="row button-row mb-5 text-center">
                    <!-- Filtro por NroPedido -->
                    <div class="col-3">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtIdPedidoFiltro" CssClass="form-control" runat="server" Placeholder="Nro Pedido"
                                OnTextChanged="txtIdPedidoFiltro_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                    <!-- Filtro Mesa -->
                    <div class="col-3">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtMesaFiltro" CssClass="form-control" runat="server" Placeholder="Nro Mesa"
                                OnTextChanged="txtMesaFiltro_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                    <!-- DropDownList Salones -->
                    <div class="col-3">
                        <div class="input-group mb-3">
                            <asp:DropDownList ID="ddlSalones" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                                BackColor="white" ForeColor="black" Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlSalones_SelectedIndexChanged">
                                <asp:ListItem Text="Todos los salones" Value="TODOS"/>
                                <asp:ListItem Text="Salon 1" Value="1" />
                                <asp:ListItem Text="Salon 2" Value="2" />
                                <asp:ListItem Text="Salon 3" Value="3" />
                                <asp:ListItem Text="Salon 4" Value="4" />
                                <asp:ListItem Text="Salon 5" Value="5" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <!-- DropDownList estado -->
                    <div class="col-3">
                        <div class="input-group mb-3">
                            <asp:DropDownList ID="ddlEstadoPedido" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                                BackColor="white" ForeColor="black" Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlEstadoPedido_SelectedIndexChanged">
                                <asp:ListItem Text="Todos los estados" Value="TODOS" />
                                <asp:ListItem Text="En Proceso" Value="EN PROCESO" />
                                <asp:ListItem Text="Completado" Value="COMPLETADO" />
                                <asp:ListItem Text="Cancelado" Value="CANCELADO" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <!-- Repeater Pedido-->
                <div class="row row-cols-1 justify-content-center">
                    <div class="col-11 text-center">
                        <asp:Repeater ID="repeaterPedidos" runat="server">
                            <HeaderTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th scope="col">Nro Pedido</th>
                                            <th scope="col">Salon</th>
                                            <th scope="col">Mesa</th>
                                            <th scope="col">Empleado</th>
                                            <th scope="col">Estado</th>
                                            <th scope="col">Opciones</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("IdPedido") %></td>
                                    <td><%# Eval("Mesa.IdSalon") %></td>
                                    <td><%# Eval("Mesa.IdMesa") %></td>
                                    <td><%# Eval("Empleado.idusuario") %></td>
                                    <td><%# Eval("Estado") %></td>
                                    <td>
                                        <asp:Button ID="btnVerPedido" class="btn btn-primary btn-sm" Text="Ver Detalles" runat="server"
                                            CommandArgument='<%# Eval("IdPedido") %>' OnClick="btnVerPedido_Click" />
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
    </div>

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
                    <asp:Button ID="btnAceptarModal" CssClass="btn btn-danger" runat="server" Text="Eliminar"
                        OnClick="btnAceptarModal_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Campo oculto para enviar al servidor -->
    <asp:HiddenField ID="hiddenFieldIdEmpleado" runat="server" />


</asp:Content>
