<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="ViewSales.aspx.cs" Inherits="WebApplication1.ViewsManagment.ViewSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Lista de Ventas</h2>

    <hr class="my-4" />

    <div class="container">
        <div class="row justify-content-center" style="padding-top: 30px">
            <div class="container-fluid">
                <div class="row button-row mb-5 text-center">
                    <!-- Filtro por NroVenta -->
                    <div class="col-3">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtNroVentaFiltro" CssClass="form-control" runat="server" Placeholder="Nro Venta"
                                OnTextChanged="txtNroVenta_TextChanged" AutoPostBack="true">
                            </asp:TextBox>
                        </div>
                    </div>
                    <!-- Filtro Mesa -->
                    <div class="col-3">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtMesaFiltro" CssClass="form-control" runat="server" Placeholder="Nro Mesa" 
                                OnTextChanged="txtMesaFiltro_TextChanged" AutoPostBack="true">
                            </asp:TextBox>
                        </div>
                    </div>
                    <!-- Filtro Fecha -->
                    <div class="col-3">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtFechaFiltro" CssClass="form-control" runat="server" TextMode="Date"
                                OnTextChanged="txtFechaFiltro_TextChanged" AutoPostBack="true">
                            </asp:TextBox>
                        </div>
                    </div>
                    <!-- DropDownList Salones -->
                    <div class="col-3">
                        <div class="input-group mb-3">
                            <asp:DropDownList ID="ddlSalones" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                                BackColor="white" ForeColor="black" Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlSalones_SelectedIndexChanged">
                                <asp:ListItem Text="Todos los salones" Value="TODOS" />
                                <asp:ListItem Text="Salon 1" Value="1" />
                                <asp:ListItem Text="Salon 2" Value="2" />
                                <asp:ListItem Text="Salon 3" Value="3" />
                                <asp:ListItem Text="Salon 4" Value="4" />
                                <asp:ListItem Text="Salon 5" Value="5" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <!-- Repeater Pedido-->
                <div class="row row-cols-1 justify-content-center">
                    <div class="col-11 text-center">
                        <asp:Repeater ID="repeaterVentas" runat="server">
                            <HeaderTemplate>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th scope="col">Nro Venta</th>
                                            <th scope="col">Salon</th>
                                            <th scope="col">Mesa</th>
                                            <th scope="col">Fecha</th>
                                            <th scope="col">Total</th>
                                            <th scope="col">Opciones</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("IdVenta") %></td>
                                    <td><%# Eval("Mesa.IdSalon") %></td>
                                    <td><%# Eval("Mesa.IdMesa") %></td>
                                    <td><%# Eval("Fecha_hora") %></td>
                                    <td><%# Eval("Total") %></td>
                                    <td>
                                        <asp:Button ID="btnVerVenta" class="btn btn-primary btn-sm" Text="Ver Detalles" runat="server"
                                            CommandArgument='<%# Eval("IdVenta") %>' OnClick="btnVerVenta_Click"/>
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
                    <asp:Button ID="btnAceptarModal" CssClass="btn btn-danger" runat="server" Text="Eliminar"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Campo oculto para enviar al servidor -->
    <asp:HiddenField ID="hiddenFieldIdEmpleado" runat="server" />

</asp:Content>
