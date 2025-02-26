<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="ProductsList.aspx.cs" Inherits="WebApplication1.ViewsStaff.ProductsList" %>

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

        .error {
            border: 2px solid red;
            background-color: #ffe6e6;
        }
    </style>

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
                        </div>
                    </div>

                    <!--Categoria-->
                    <div class="col-3">
                        <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                            BackColor="white" ForeColor="black" Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged">
                            <asp:ListItem Text="Todos las categorias" Value="0" />
                            <asp:ListItem Text="Cafeteria" Value="1" />
                            <asp:ListItem Text="Entradas" Value="2" />
                            <asp:ListItem Text="Comidas" Value="3" />
                            <asp:ListItem Text="Postres" Value="4" />
                            <asp:ListItem Text="Bebidas" Value="5" />
                        </asp:DropDownList>
                    </div>

                </div>


                <!-- Repeater -->
                <asp:Repeater ID="repeaterProductos" runat="server">
                    <HeaderTemplate>
                        <table class="table">
                            <thead>
                                <tr>
                                    <th scope="col">ID</th>
                                    <th scope="col">Imagen</th>
                                    <th scope="col">Nombre</th>
                                    <th scope="col">Categoria</th>
                                    <th scope="col">Stock</th>
                                    <th scope="col">Estado</th>
                                    <th scope="col">Opciones</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("IdProducto") %></td>
                            <td>
                                <asp:Image ID="imgProducto" runat="server" CssClass="img-thumbnail" Width="80px" Height="80px" ImageUrl='<%# ResolveUrl(Eval("Imagen.DirComp").ToString()) %>' /></td>
                            <td><%# Eval("Nombre") %></td>
                            <td><%# Eval("Categoria.Nombre") %></td>
                            <td><%# Eval("Stock") %></td>
                            <td><%# Eval("Estado") %></td>
                            <td>
                                <asp:Button ID="btnVerProducto" class="btn btn-primary btn-sm" Text="Ver" runat="server"
                                    CommandArgument='<%# Eval("IdProducto") %>' OnClick="btnVerProducto_Click" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                </table>
           
           
                    </FooterTemplate>
                </asp:Repeater>

                <!-- Campo oculto para enviar al servidor -->
                <asp:HiddenField ID="hiddenFieldIdEmpleado" runat="server" />

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

                    <!-- Imagen -->
                    <div class="d-flex justify-content-center">
                        <asp:Image ID="imgPreview" CssClass="image-circle mb-3" runat="server" />
                    </div>

                    <div class="row justify-content-center" style="margin-top: 20px">
                        <!--Categoria-->
                        <div class="col-3">
                            <asp:DropDownList ID="ddlCategoriasModal" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                                BackColor="white" ForeColor="black" Font-Size="Large" Enabled="false" Width="180px" Height="40px">
                                <asp:ListItem Text="Cafeteria" Value="1" />
                                <asp:ListItem Text="Entradas" Value="2" />
                                <asp:ListItem Text="Comidas" Value="3" />
                                <asp:ListItem Text="Postres" Value="4" />
                                <asp:ListItem Text="Bebidas" Value="5" />
                            </asp:DropDownList>
                        </div>

                        <!--Nombre-->
                        <div class="col-3">
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre"
                                Enabled="false"></asp:TextBox>
                        </div>

                        <!--Precio-->
                        <div class="col-3">
                            <asp:TextBox ID="txtPrecio" CssClass="form-control" runat="server"
                                Placeholder="Precio" Enabled="false"></asp:TextBox>
                        </div>

                        <!--Stock-->
                        <div class="col-3">
                            <asp:TextBox ID="txtStock" CssClass="form-control" runat="server"
                                Placeholder="Stock" Enabled="false"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row justify-content-lg-start" style="margin-top: 20px">

                        <!--cafeteria-->
                        <%if (string.Compare(ddlCategoriasModal.SelectedValue, "1") == 0)
                            { %>

                        <div class="col-3">
                            <label style="font-size: 20px">
                                Contiene leche: 
                            <asp:Label ID="lblContieneLeche" runat="server" Font-Size="20px" />
                            </label>
                        </div>
                        <% } %>

                        <!--Entradas-->
                        <%if (string.Compare(ddlCategoriasModal.SelectedValue, "2") == 0)
                            { %>

                        <div class="col-6">
                            <label style="font-size: 20px">
                                Entrada:  
                            <asp:Label ID="lblEntrada" runat="server" Font-Size="20px" />
                            </label>
                        </div>
                        <% } %>
                        <!--Comidas-->
                        <%if (string.Compare(ddlCategoriasModal.SelectedValue, "3") == 0)
                            { %>

                        <div class="col-6">
                            <label style="font-size: 20px">
                                Contiene Guarnicion:  
                            <asp:Label ID="lblContieneGuarnicion" runat="server" Font-Size="20px" />
                            </label>
                        </div>
                        <% } %>

                        <!--Postres-->
                        <%if (string.Compare(ddlCategoriasModal.SelectedValue, "4") == 0)
                            { %>
                        <div class="col-4">
                            <label style="font-size: 20px">
                                Azucar agregada:    
                            <asp:Label ID="lblAzucar" runat="server" Font-Size="20px" />
                            </label>
                        </div>

                        <div class="col-4">
                            <label style="font-size: 20px">
                                Contiene Gluten:    
                            <asp:Label ID="lblGluten" runat="server" Font-Size="20px" />
                            </label>
                        </div>
                        <% } %>

                        <!--Bebidas-->
                        <%if (string.Compare(ddlCategoriasModal.SelectedValue, "5") == 0)
                            { %>

                        <div class="col-3">
                            <asp:TextBox ID="txtVolumen" runat="server" CssClass="form-control" placeholder="Volumen"
                                onkeypress="return soloNumeros(event)" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-6">
                            <label style="font-size: 20px">
                                Contiene Alcohol:  
                            <asp:Label ID="lblAlcohol" runat="server" Font-Size="20px" />
                            </label>
                        </div>

                        <% } %>
                    </div>

                    <div class="row justify-content-center" style="margin-top: 20px">
                        <!--Descripcion-->
                        <div class="input-group">
                            <span class="input-group-text">Descripcion</span>
                            <asp:TextBox ID="txtBxDescripcion" runat="server" CssClass="form-control"
                                Font-Names="Arial" Height="80px" TextMode="MultiLine" Enabled="false"/>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-close" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
