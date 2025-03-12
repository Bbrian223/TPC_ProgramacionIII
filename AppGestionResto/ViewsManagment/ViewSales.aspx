<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="ViewSales.aspx.cs" Inherits="WebApplication1.ViewsManagment.ViewSales" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .lblModal {
            font-size: 10px;
        }
    </style>


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
                                            <th scope="col">Estado</th>
                                            <th scope="col">Total</th>
                                            <th scope="col">Opciones</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("IdVenta") %></td>
                                    <td><%# Eval("Pedido.Mesa.IdSalon") %></td>
                                    <td><%# Eval("Pedido.Mesa.IdMesa") %></td>
                                    <td><%# Eval("Fecha_hora") %></td>
                                    <td><%# Eval("Pedido.Estado") %></td>
                                    <td><%# Eval("Total", "{0:F1}") %></td>
                                    <td>
                                        <asp:Button ID="btnVerVenta" class="btn btn-primary btn-sm" Text="Ver Detalles" runat="server"
                                            CommandArgument='<%# Eval("IdVenta") %>' OnClick="btnVerVenta_Click" />
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
                    <asp:Button ID="btnAceptarModal" CssClass="btn btn-danger" runat="server" Text="Eliminar" />
                </div>
            </div>
        </div>
    </div>

    <!-- Campo oculto para enviar al servidor -->
    <asp:HiddenField ID="hiddenFieldIdEmpleado" runat="server" />

    <!-- Modal de ver -->
    <div class="modal fade" id="modalVer" tabindex="-1" aria-labelledby="modalVerLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Nro venta: 
                                    <asp:Label ID="lblModalNroVenta" runat="server" /></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <div class="row justify-content-center" style="margin-top: 20px">
                        <!--Nro de Venta-->
                        <div class="col-3">
                            <label for="txtNroVenta" class="lblModal">ID Venta:</label>
                            <asp:TextBox ID="txtNroVenta" runat="server" CssClass="form-control" placeholder="Nro Venta"
                                Enabled="false"></asp:TextBox>
                        </div>
                        <!--Salon-->
                        <div class="col-3">
                            <label for="txtNroSalon" class="lblModal">Numero Salon:</label>
                            <asp:TextBox ID="txtNroSalon" CssClass="form-control" runat="server"
                                Placeholder="Nro Salon" Enabled="false"></asp:TextBox>
                        </div>
                        <!--Mesa-->
                        <div class="col-3">
                            <label for="txtNroMesa" class="lblModal">Numero Mesa:</label>
                            <asp:TextBox ID="txtNroMesa" CssClass="form-control" runat="server"
                                Placeholder="Nro Mesa" Enabled="false"></asp:TextBox>
                        </div>
                        <!--Fecha-->
                        <div class="col-3">
                            <label for="txtFechaVenta" class="lblModal">Fecha pedido:</label>
                            <asp:TextBox ID="txtFechaVenta" CssClass="form-control" runat="server"
                                Placeholder="Fecha" Enabled="false" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row justify-content-start" style="margin-top: 20px">
                        <!--Estado-->
                        <div class="col-3">
                            <label for="txtEstadoPedido" class="lblModal">Estado Pedido:</label>
                            <asp:TextBox ID="txtEstadoPedido" CssClass="form-control" runat="server"
                                Placeholder="Estado Pedido" Enabled="false"></asp:TextBox>
                        </div>
                        <!--Total-->
                        <div class="col-3">
                            <label for="txtPrecioTotal" class="lblModal">Precio Final:</label>
                            <asp:TextBox ID="txtPrecioTotal" CssClass="form-control" runat="server"
                                Placeholder="Total" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row justify-content-start" style="margin-top: 20px">
                        <h5 style="margin-bottom: 20px; margin-top: 20px;">Detalles:</h5>

                        <asp:GridView ID="gViewProductos" runat="server" CssClass="table table-striped table-bordered text-center"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Producto.IdProducto" HeaderText="ID" ItemStyle-Width="50px" />
                                <asp:BoundField DataField="Producto.Categoria.Nombre" HeaderText="Categoria" ItemStyle-Width="100px" />
                                <asp:BoundField DataField="Producto.Nombre" HeaderText="Nombre" ItemStyle-Width="100px" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-Width="100px" />
                                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:F1}" ItemStyle-Width="100px" />
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Volver</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
