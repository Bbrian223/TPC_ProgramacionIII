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

</head>
<body>

    <style>
        .card-container {
            display: flex;
            flex-wrap: wrap; /* Permite que las cartas se acomoden en varias filas */
            gap: 10px; /* Espacio entre cartas */
            justify-content: flex-start; /* Alinea las cartas de izquierda a derecha */
            padding: 10px; /* Espaciado interno */
            margin-top: 30px;
        }

        .card {
            width: 11rem; /* Mantiene el tamaño de las cartas */
            height: 13rem;
            margin: 0; /* Elimina márgenes innecesarios */
            flex: 1 1 calc(33.333% - 20px); /* Hace que se distribuyan en 3 columnas */
            max-width: 11rem; /* Evita que crezcan más del tamaño deseado */
            margin-left: 3px;
            margin-right: 3px;
            margin-bottom: 5px;
        }


        .card-img-top {
            width: 100%; /* Ocupar todo el ancho del contenedor */
            height: 100px; /* Altura fija */
            object-fit: contain; /* Ajusta la imagen dentro sin recortar */
            display: block; /* Evita espacios extra debajo de la imagen */
            background-color: #f8f9fa; /* Color de fondo opcional para imágenes más pequeñas */
            border-top-left-radius: 8px;
            border-top-right-radius: 8px;
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
        }
    </style>


    <form id="form1" runat="server">

        <nav class="navbar navbar-light bg-light">
            <div class="container-fluid">
                <asp:Button ID="btnCerraVentana" runat="server" CssClass="btn-close" OnClick="btnCerraVentana_Click" />

                <h4 style="margin-right: 90px">Detalle de pedido Mesa:
                    <asp:Label ID="lblIdMesa" runat="server" /></h4>
            </div>
        </nav>

        <div class="container-fluid vh-100">
            <div class="row h-100">

                <!-- Columna izquierda (70%) -->
                <div class="col-8 text-white" style="background-color: gray">
                    <div class="card-container">
                        <asp:Repeater ID="repeaterProductos" runat="server">
                            <ItemTemplate>

                                <asp:Panel CssClass="card" runat="server">
                                    <asp:Image CssClass="card-img-top" runat="server" ImageUrl='<%# Eval("Imagen.DirComp") %>' />

                                    <div class="card-body">
                                        <div class="row">
                                            <asp:Label CssClass="card-text" runat="server" Text='<%# Eval("Nombre") %>'></asp:Label>
                                        </div>
                                        <asp:Button ID="btnAgregarProd" Text="Agregar" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnAgregarProd_Click" CommandArgument='<%# Eval("IdProducto") %>' />
                                    </div>

                                </asp:Panel>

                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <!-- Columna derecha (30%) -->
                <div class="col-4 bg-success text-white ">
                    <h2>Detalles del pedido</h2>
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
                        <!-- Alineación vertical -->

                        <!-- div1: Alineación horizontal -->
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
                                            <td><%# Eval("Precio") %></td>
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
                                <asp:Button Text="Agregar" runat="server" CssClass="btn btn-primary" />
                            </div>
                        </div>

                        <div class="d-flex align-items-center gap-3 w-100">
                            <%if (IdCategoria == 1) // cafeteria
                                { %>
                            <div class="col-3">
                                <asp:DropDownList ID="ddlTipoLeche" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                                    BackColor="white" ForeColor="black" Font-Size="Large">
                                </asp:DropDownList>
                            </div>

                            <div class="col-3">
                                <asp:DropDownList ID="ddlTamanio" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                                    BackColor="white" ForeColor="black" Font-Size="Large">
                                </asp:DropDownList>
                            </div>

                            <%} %>


                            <%if (IdCategoria == 3) // comida

                                { %>
                            <div class="col-3">
                                <asp:DropDownList ID="ddlGuarniciones" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                                    BackColor="white" ForeColor="black" Font-Size="Large">
                                </asp:DropDownList>
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



        <!-- Campo oculto para enviar al servidor -->
        <asp:HiddenField ID="hiddenFieldIdEmpleado" runat="server" />

        <script>
            document.getElementById('<%= txtCantidad.ClientID %>').setAttribute('min', '0');
        </script>

    </form>
</body>
</html>
