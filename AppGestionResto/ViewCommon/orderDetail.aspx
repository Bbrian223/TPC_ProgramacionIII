<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderDetail.aspx.cs" Inherits="WebApplication1.ViewCommon.orderDetail" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js"></script>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-icons/1.11.3/font/bootstrap-icons.min.css" />

</head>
<body>

    <style>
        .card-container {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            justify-content: flex-start;
            align-items: flex-start;
            padding: 10px;
            margin-top: 30px;
        }

        .card {
            flex: 0 1 calc(33.333% - 20px);
            width: 8rem;
            height: 13rem;
            max-width: 11rem;
            margin: 3px;
        }


        .card-img-top {
            width: 100%;
            height: 100px;
            object-fit: cover; /* Mejor visualización si las imágenes tienen distintas proporciones */
            border-top-left-radius: 8px;
            border-top-right-radius: 8px;
            background-color: #e1e1e1;
        }

        .card-body {
            display: flex;
            flex-direction: column;
            align-items: center;
            text-align: center;
        }

        .card-text {
            margin-bottom: 10px;
            font-size: 1rem;
            font-weight: bold;
        }

        .image-modal {
            width: 70px;
            height: 70px;
            border-radius: 50%;
        }

        .modal-body {
            display: flex;
            justify-content: center; /* Centra los elementos horizontalmente */
            align-items: center; /* Centra los elementos verticalmente */
            gap: 25px; /* Espacio entre los elementos */
        }

        .lblModal {
            font-size: 20px;
            margin-left: 50px;
            margin-right: 50px;
        }

        .txtModal {
            font-size: 18px; /* Tamaño de la fuente */
            width: 100px; /* Ancho del TextBox */
            padding: 8px; /* Espaciado interno */
            border: 1px solid #ced4da; /* Color y grosor del borde */
            border-radius: 5px; /* Bordes redondeados */
            text-align: center; /* Alineación del texto */
        }

        .btnAgregar {
            border-radius: 8px;
            margin-top: 5px;
        }

        .lbldetalle {
            margin-bottom: 20px;
            font-size: 20px;
        }

        .lblDetalleMesa {
            font-size: 20px;
        }

        .table th, .table td {
            text-align: center;
            vertical-align: middle;
        }
    </style>


    <form id="form1" runat="server">

        <nav class="navbar navbar-dark bg-dark">
            <div class="container-fluid">
                <asp:Button ID="btnCerraVentana" runat="server" CssClass="btn-close btn-close-white" OnClick="btnCerraVentana_Click" />

                <h4 style="margin-right: 90px; color: ;">Detalle de pedido Mesa:
                    <asp:Label ID="lblIdMesa" runat="server" /></h4>
            </div>
        </nav>

        <div class="container-fluid vh-100">
            <div class="row h-100">
                <!-- Columna izquierda (70%) con scroll independiente -->
                <div class="col-8 text-white" style="background-color: gray; height: 100%; overflow-y: auto;">
                    <div class="card-container">
                        <asp:Repeater ID="repeaterProductos" runat="server">
                            <ItemTemplate>
                                <asp:Panel CssClass="card" runat="server">
                                    <asp:Image CssClass="card-img-top" runat="server" ImageUrl='<%# Eval("Imagen.DirComp") %>' />
                                    <div class="card-body">
                                        <div class="row">
                                            <asp:Label CssClass="card-text" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                                        </div>
                                        <asp:Button ID="btnVistaPrevia" Text="Agregar" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnVistaPrevia_Click" CommandArgument='<%# Eval("IdProducto") %>' />
                                    </div>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <!-- Columna derecha (30%) con scroll independiente -->
                <div class="col-4 text-black lblDetalleMesa" style="background-color: white; height: 100%; overflow-y: auto;">
                    <h2>Detalle de Mesa</h2>
                    <hr class="my-4" />

                    <!--    Infotmacion mesa    -->
                    <div class="row" style="margin-top: 20px">
                        <div class="col-6">
                            <asp:Label Text="Fecha y hora: " runat="server" />
                        </div>
                        <div class="col-6">
                            <asp:Label ID="lblFecha" runat="server" />
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px">
                        <div class="col-6">
                            <asp:Label Text="Numero de Mesa: " runat="server" />
                        </div>
                        <div class="col-6">
                            <asp:Label ID="lblNumeroMesa" runat="server" />
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px">
                        <div class="col-6">
                            <asp:Label Text="Estado: " runat="server" />
                        </div>
                        <div class="col-6">
                            <asp:Label ID="lblEstadoMesa" runat="server" />
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px; margin-bottom: 20px">
                        <div class="col-6">
                            <asp:Label Text="Nro Empleado: " runat="server" />
                        </div>
                        <div class="col-6">
                            <asp:Label ID="lblNroEmpleado" runat="server" />
                        </div>
                    </div>

                    <asp:Panel ID="pnlMonto" runat="server" CssClass="row">
                        <div class="col-6">
                            <asp:Label Text="Monto Total: " runat="server" />
                        </div>
                        <div class="col-6">
                            <asp:Label ID="lblMontoTotal" runat="server" Text="$ 1563.3" />
                        </div>

                    </asp:Panel>

                    <!--    Boton de Acciones    -->
                    <div class="row" style="margin-top: 20px">
                        <div class="col-4">
                            <asp:Button ID="btnCerrarPedido" Text="Cerrar Pedido" runat="server" CssClass="btn btn-success btn-sm"
                                OnClick="btnCerrarPedido_Click" />
                        </div>
                        <div class="col-4">
                            <asp:Button ID="btnCancelarPedido" Text="Cancelar Pedido" runat="server" CssClass="btn btn-danger btn-sm"
                                OnClick="btnCancelarPedido_Click" />
                        </div>
                        <div class="col-4">
                            <asp:Button ID="btnMesaHabilitada" Text="Habilitar Mesa" runat="server" CssClass="btn btn-primary btn-sm"
                                OnClick="btnMesaHabilitada_Click" />
                        </div>
                    </div>


                    <hr class="my-4" />


                    <div class="row" style="margin-top: 20px">
                        <div class="col-6">
                            <asp:Label Text="Detalle del pedido: " runat="server" />
                        </div>
                    </div>
                    <!--    Repeater detalles Pedidos   -->
                    <div class="row" style="margin-top: 20px">
                        <div class="col-12">
                            <asp:Repeater ID="repeaterDetalles" runat="server">
                                <HeaderTemplate>
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th scope="col">Nombre</th>
                                                <th scope="col">Cantidad</th>
                                                <th scope="col">Subtotal</th>
                                                <th scope="col">Opciones</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("Producto.Nombre") %></td>
                                        <td><%# Eval("Cantidad") %></td>
                                        <td><%# Eval("Subtotal", "{0:F1}") %></td>
                                        <td>
                                            <asp:LinkButton ID="btnEliminarProd" runat="server" CssClass="btn btn-danger btn-sm"
                                                CommandArgument='<%# Eval("IdDetallePedido") %>' OnClick="btnEliminarProd_Click">
                                                <i class="bi bi-trash"></i>
                                            </asp:LinkButton>
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


        <!-- Modal de Productos-->
        <div class="modal fade" id="modalDetalles" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog modal-lg modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Agregar Producto</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                    </div>
                    <div class="modal-body d-flex flex-column gap-3">
                        <!--Repater productos-->
                        <div id="div1" class="d-flex align-items-center gap-3 w-100">
                            <div class="flex-grow-1 table-responsive">

                                <asp:Repeater ID="repeaterModal" runat="server">
                                    <HeaderTemplate>
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th scope="col">Imagen</th>
                                                    <th scope="col">Nombre</th>
                                                    <th scope="col">Precio</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Image ID="imgProdModal" runat="server" CssClass="img-thumbnail" Width="80px" Height="80px" ImageUrl='<%# ResolveUrl(Eval("Imagen.DirComp").ToString()) %>' />
                                            </td>
                                            <td><%# Eval("Nombre") %></td>
                                            <td><%# Eval("Precio", "{0:F1}") %></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                </table>
                           
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>

                            <div>
                                <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number" Text="1" CssClass="txtModal" />
                            </div>

                            <div>
                                <asp:Button ID="btnAgregarProd" Text="Agregar" runat="server" CssClass="btn btn-primary"
                                    OnClick="btnAgregarProd_Click" />
                            </div>
                        </div>

                        <!--Label Error-->
                        <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger gap-3 w-100" Visible="false">
                            <asp:Label ID="lblErrores" Text="" runat="server" Visible="false"></asp:Label>
                        </asp:Panel>


                        <!--Extras-->
                        <div class="d-flex align-items-center gap-3 w-100">
                            <%if (IdCategoria == 1) // CAFETERIA
                                {%>

                            <%if ((bool)Session["GUARNICION"])
                                { %>
                            <div class="col-3">
                                <asp:Label Text="Tipo de Leche" runat="server" CssClass="lbldetalle" />
                                <asp:DropDownList ID="ddlTipoLeche" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                                    BackColor="white" ForeColor="black" Font-Size="Large">
                                </asp:DropDownList>
                            </div>
                            <%} %>

                            <div class="col-3">
                                <asp:Label Text="Tamaño cafe" runat="server" CssClass="lbldetalle" />
                                <asp:DropDownList ID="ddlTamanio" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                                    BackColor="white" ForeColor="black" Font-Size="Large">
                                </asp:DropDownList>
                            </div>

                            <%} %>


                            <%if (IdCategoria == 2) //ENTRADAS
                                { %>
                            <div class="col-2">
                                <asp:Label Text="Detalles:   " runat="server" CssClass="lbldetalle" />
                            </div>
                            <div class="col-6">
                                <asp:Label ID="lblEntradas" runat="server" CssClass="lbldetalle" />
                            </div>

                            <%} %>

                            <%if (IdCategoria == 3 && (bool)Session["GUARNICION"]) // COMIDA

                                { %>
                            <div class="col-3">
                                <asp:Label Text="Guarnicion" runat="server" CssClass="lbldetalle" />
                                <asp:DropDownList ID="ddlGuarniciones" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                                    BackColor="white" ForeColor="black" Font-Size="Large">
                                </asp:DropDownList>
                            </div>

                            <%} %>

                            <%if (IdCategoria == 4) // POSTRES
                                { %>
                            <div class="col-2">
                                <asp:Label Text="Detalles:   " runat="server" CssClass="lbldetalle" />
                            </div>
                            <div class="col-5">
                                <asp:Label ID="lblAzucar" runat="server" CssClass="lbldetalle" />
                            </div>
                            <div class="col-5">
                                <asp:Label ID="lblGluten" runat="server" CssClass="lbldetalle" />
                            </div>

                            <%} %>

                            <%if (IdCategoria == 5) // BEBIDAS
                                { %>
                            <div class="col-2">
                                <asp:Label Text="Detalles:   " runat="server" CssClass="lbldetalle" />
                            </div>
                            <div class="col-5">
                                <asp:Label ID="lblAlcohol" runat="server" CssClass="lbldetalle" />
                            </div>
                            <div class="col-5">
                                <asp:Label ID="lblVolumen" runat="server" CssClass="lbldetalle" />
                            </div>

                            <%} %>
                        </div>

                        <div id="div2" class="d-flex align-items-center gap-3 w-100">
                            <div class="input-group flex-grow-1 table-responsive">
                                <span class="input-group-text">Aclaraciones</span>
                                <asp:TextBox ID="txtBxDescripcion" runat="server" CssClass="form-control"
                                    Font-Names="Arial" Height="80px" TextMode="MultiLine" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>


        <!-- Modal Error-->
        <div class="modal fade" id="modalError" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog modal-lg modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Error</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                    </div>
                    <div class="modal-body d-flex flex-column gap-3 alert alert-danger" role="alert">

                        <asp:Label ID="lblModalError" runat="server" Font-Size="XX-Large" />

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                        <asp:Button ID="btnModalAceptar" Text="Aceptar" runat="server" CssClass="btn btn-primary"
                            OnClick="btnModalAceptar_Click" />
                    </div>

                </div>
            </div>
        </div>

        <!-- Campo oculto para enviar al servidor -->
        <asp:HiddenField ID="hiddenFieldIdEmpleado" runat="server" />

        <script>
            document.getElementById('<%= txtCantidad.ClientID %>').setAttribute('min', '1');
        </script>

    </form>
</body>
</html>
