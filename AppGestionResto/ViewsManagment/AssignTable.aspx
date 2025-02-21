<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="AssignTable.aspx.cs" Inherits="WebApplication1.ViewsManagment.AssignTable"
    MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        /* Contenedor con scroll vertical */
        .scroll-grid-container {
            max-height: 300px; /* Ajusta la altura según tus necesidades */
            overflow-y: auto;
            overflow-x: hidden;
            border: 1px solid #ccc; /* Borde opcional para distinguir la zona de scroll */
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.2);
        }

            /* Fijar las cabeceras para que no se desplacen con el scroll */
            .scroll-grid-container table thead th {
                position: sticky;
                top: 0;
                background-color: #f3f3f3;
                z-index: 1;
            }

        .mesa-cuadrado {
            display: flex;
            align-items: center;
            justify-content: center;
            width: 60px;
            height: 60px;
            background-color: dodgerblue; /* Verde para disponible */
            color: white;
            font-weight: bold;
            border-radius: 8px;
            transition: transform 0.2s, background-color 0.2s;
            margin: 20px;
        }

        .mesa-cerrada {
            background-color: #6c757d; /* Gris para cerrada */
        }


        input[type="checkbox"] {
            width: 15px;
            height: 15px;
            transform: scale(1.5);
            cursor: pointer;
            margin: 5px
        }
    </style>

    <h2>Asignar mesas Mozos</h2>
    <hr class="my-4" />

    <div class="container">
        <div class="row justify-content-center" style="padding-top: 10px">
            <div class="container-fluid">
                <!--DropDownList Salones-->
                <div class="row button-row mb-5">
                    <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <asp:DropDownList ID="ddlSalones" runat="server" CssClass="btn btn-secondary btn-lg dropdown-toggle"
                            BackColor="white" ForeColor="black" Font-Size="Large" AutoPostBack="true" OnSelectedIndexChanged="ddlSalones_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <!--Botones-->
                    <div class="form-check col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <asp:CheckBox ID="chkAsignarMesa" runat="server" AutoPostBack="true" />
                        <label style="font-size: 20px">Asignar Mesa</label>
                    </div>
                </div>
                <!--Mesas-->
                <div class="row" id="ContenedorMesas" runat="server" style="margin-top: 20px;">
                    <!-- 5 filas con 5 mesas cada una -->
                </div>
                <%if (chkAsignarMesa.Checked)
                    { %>
                <!--GridView Empleados-->
                <div class="row button-row mb-5 text-center">
                    <div class="col-6 scroll-grid-container">
                        <label style="margin-bottom: 20px">Empleados</label>
                        <asp:GridView ID="gViewEmpleados" runat="server" CssClass="table table-striped table-bordered text-center"
                            AutoGenerateColumns="False" DataKeyNames="IdEmpleado" OnRowCommand="gViewEmpleados_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Seleccionar" Text="+"
                                    ControlStyle-CssClass="btn btn-primary btn-sm" ItemStyle-Width="20px" />
                                <asp:BoundField DataField="IdEmpleado" HeaderText="ID" ItemStyle-Width="50px" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-Width="100px" />
                                <asp:BoundField DataField="Apellido" HeaderText="Apellido" ItemStyle-Width="100px" />
                                <asp:BoundField DataField="Documento" HeaderText="DNI" ItemStyle-Width="100px" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <!--Empleados seleccionados-->
                    <div class="col-6 scroll-grid-container">
                        <label style="margin-bottom: 20px">Empleados Seleccionados</label>
                        <asp:GridView ID="gviewEmplSelect" runat="server" CssClass="table table-striped table-bordered text-center"
                            AutoGenerateColumns="False" DataKeyNames="IdEmpleado" OnRowCommand="gviewEmplSelect_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Seleccionar" Text="X"
                                    ControlStyle-CssClass="btn btn-danger btn-sm" ItemStyle-Width="20px" />
                                <asp:BoundField DataField="IdEmpleado" HeaderText="ID" ItemStyle-Width="50px" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-Width="100px" />
                                <asp:BoundField DataField="Apellido" HeaderText="Apellido" ItemStyle-Width="100px" />
                                <asp:BoundField DataField="Documento" HeaderText="DNI" ItemStyle-Width="100px" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <!--Filtros-->
                <div class="row button-row mb-5 text-center">
                    <!-- Filtro por ID -->
                    <div class="col-3">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtIdFiltro" CssClass="form-control" runat="server" Placeholder="ID"
                                OnTextChanged="txtIdFiltro_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                    <!-- Filtro por DOCUMENTO -->
                    <div class="col-3">
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtDniFiltro" CssClass="form-control" runat="server" Placeholder="DNI"
                                OnTextChanged="txtDniFiltro_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <%} %>
        </div>
    </div>

    <div class="modal fade" id="modalEstado" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Empleados asignados</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body d-flex flex-column gap-3" role="alert">

                    <div class="col-6 scroll-grid-container">
                        <label style="margin-bottom: 20px">Empleados Seleccionados</label>
                        <asp:GridView ID="gViewMesaEmpleados" runat="server" CssClass="table table-striped table-bordered text-center"
                            AutoGenerateColumns="False" DataKeyNames="IdEmpleado">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="X" Text="X"
                                    ControlStyle-CssClass="btn btn-danger btn-sm" ItemStyle-Width="20px" />
                                <asp:BoundField DataField="IdEmpleado" HeaderText="ID" ItemStyle-Width="50px" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-Width="100px" />
                                <asp:BoundField DataField="Apellido" HeaderText="Apellido" ItemStyle-Width="100px" />
                                <asp:BoundField DataField="Documento" HeaderText="DNI" ItemStyle-Width="100px" />
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
