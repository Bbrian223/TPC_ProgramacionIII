<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="UserSettings.aspx.cs" Inherits="WebApplication1.ViewsManagment.UserSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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
                    <asp:Button class="btn btn-primary btn-sm" Text="+ Nuevo Empleado" runat="server" />
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



            </div>
        </div>
    </div>

</asp:Content>
