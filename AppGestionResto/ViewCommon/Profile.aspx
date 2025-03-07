<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WebApplication1.ViewCommon.Profile" %>

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

        .MsgSucces {
            text-align: center;
            background-color: lightgreen;
            color: green;
            font-size: 20px;
            height: 40px;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .MsgError {
            text-align: center;
            background-color: #ff6a6a;
            color: white;
            font-size: 20px;
            height: 40px;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .contenedor-info {
            border: 2px solid lightGray;
            border-radius: 10px;
            padding-top: 30px;
            padding-bottom: 50px;
        }

        .lblModal {
            font-size: 10px;
        }
    </style>


    <h2>Mi perfil</h2>

    <hr class="my-4" />

    <div class="container-fluid">
        <div class="row vh-90">
            <!-- Sección Izquierda (80%) -->
            <div class="col-10 contenedor-info">

                <asp:Panel ID="panelMsgLbl" runat="server" CssClass="alert alert-danger">
                    <asp:Label ID="lblErrores" runat="server" Visible="false"></asp:Label>
                </asp:Panel>

                <div class="d-flex justify-content-center">
                    <asp:Image ID="imgPreview" CssClass="image-circle mb-3" runat="server" onerror="this.onerror=null; this.src='/Database/Imagenes/Perfiles/sin-imagen.jpg';" />
                </div>

                <%if (HabEdicion)
                    { %>

                <asp:FileUpload ID="fileUploadImagen" runat="server" CssClass="form-control" onchange="javascript:__doPostBack('<%= fileUploadImagen.ClientID %>', '');" />

                <%}%>

                <div class="row justify-content-center" style="margin-top: 20px">
                    <div class="col-3">
                        <label for="txtNombre" class="lblModal">Nombre:</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-3">
                        <label for="txtApellido" class="lblModal">Apellido:</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Apellido" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-3">
                        <label for="txtDocumento" class="lblModal">Documento:</label>
                        <asp:TextBox ID="txtDocumento" CssClass="form-control" runat="server"
                            Placeholder="DNI" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-3">
                        <label for="txtFechaNac" class="lblModal">Fecha Nacimiento:</label>
                        <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control" placeholder="Fecha Nac"
                            type="date" Enabled="false"></asp:TextBox>
                    </div>
                </div>

                <div class="row justify-content-center" style="margin-top: 20px">
                    <div class="col-3">
                        <label for="txtCalle" class="lblModal">Calle:</label>
                        <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" placeholder="Calle"></asp:TextBox>
                    </div>
                    <div class="col-3">
                        <label for="txtNumDir" class="lblModal">Numero:</label>
                        <asp:TextBox ID="txtNumDir" CssClass="form-control" runat="server"
                            Placeholder="Numero" onkeypress="return soloNumeros(event);"></asp:TextBox>
                    </div>
                    <div class="col-3">
                        <label for="txtLocalidad" class="lblModal">Localidad:</label>
                        <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" placeholder="Localidad"></asp:TextBox>
                    </div>
                    <div class="col-3">
                        <label for="txtCodPostal" class="lblModal">Codigo Postal:</label>
                        <asp:TextBox ID="txtCodPostal" runat="server" CssClass="form-control" placeholder="Cod Postal"></asp:TextBox>
                    </div>
                </div>


                <div class="row justify-content-center" style="margin-top: 20px">
                    <div class="col-4">
                        <label for="txtEmail" class="lblModal">Email:</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" TextMode="Email"></asp:TextBox>
                    </div>
                    <div class="col-4">
                        <label for="txtTelefono" class="lblModal">Telefono:</label>
                        <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server"
                            Placeholder="Telefono" onkeypress="return soloNumeros(event);"></asp:TextBox>
                    </div>
                    <div class="col-4">
                        <label for="txtOpciones" class="lblModal">Categoria:</label>
                        <asp:TextBox ID="txtOpciones" CssClass="form-control" runat="server"
                            Placeholder="Categoria" Enabled="false"></asp:TextBox>
                    </div>
                </div>

            </div>
            <!-- Sección Derecha (20%) -->
            <div class="col-2">
                <%if (!HabEdicion)
                    {%>
                <div style="margin-bottom: 20px">
                    <asp:Button ID="btnEditar" Text="Editar" runat="server"
                        CssClass="btn btn-secondary w-100" OnClick="btnEditar_Click" />
                </div>
                <div style="margin-bottom: 20px">
                    <asp:Button ID="btnCambiarPass" Text="Cambiar Pass" runat="server"
                        CssClass="btn btn-secondary w-100" OnClick="btnCambiarPass_Click" />
                </div>
                <%}
                    else
                    { %>
                <div style="margin-bottom: 20px">
                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server"
                        CssClass="btn btn-primary w-100" OnClick="btnGuardar_Click" />
                </div>
                <div style="margin-bottom: 20px">
                    <asp:Button ID="btnCancelar" Text="Cancelar" runat="server"
                        CssClass="btn btn-danger w-100" OnClick="btnCancelar_Click" />
                </div>
                <%} %>
            </div>


        </div>
    </div>


    <!-- Modal de ver -->
    <div class="modal fade" id="modalEditarPass" tabindex="-1" aria-labelledby="modalEditarPassLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                </div>
                <div class="modal-body">

                    <asp:Panel ID="panelModal" runat="server" CssClass="alert alert-danger">
                        <asp:Label ID="lblErrorModal" runat="server" Visible="false"></asp:Label>
                    </asp:Panel>

                    <!--pass actual-->
                    <div class="row justify-content-center" style="margin-top: 20px">
                        <div class="col-3">
                            <label>Contraseña Actual</label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox ID="txtPassActual" runat="server" CssClass="form-control"
                                placeholder="Contraseña Actual"></asp:TextBox>
                        </div>
                    </div>
                    <!--nuva pass-->
                    <div class="row justify-content-center" style="margin-top: 20px">
                        <div class="col-3">
                            <label>Nueva Contraseña</label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox ID="txtNuevaPass" runat="server" CssClass="form-control"
                                placeholder="Nueva Contraseña"></asp:TextBox>
                        </div>
                    </div>
                    <!--confirmar pass-->
                    <div class="row justify-content-center" style="margin-top: 20px">
                        <div class="col-3">
                            <label>Confirmar Contraseña</label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox ID="txtConfirmarPass" runat="server" CssClass="form-control"
                                placeholder="Confirmar Contraseña"></asp:TextBox>
                        </div>
                    </div>


                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarEdicion" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="btnGuardarEdicion_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal de Confirmacion -->
    <div class="modal fade" id="modalConfirmarCambio" tabindex="-1" aria-labelledby="modalConfirmarCambioLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                </div>
                <div class="modal-body">
                    <asp:Panel ID="panel1" runat="server" CssClass="alert alert-success">
                        <h4>Cambio Exitoso!!</h4>
                    </asp:Panel>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>



    <script>
        function soloNumeros(e) {
            var key = e.which || e.keyCode;
            // Permitir teclas como Backspace (8), Tab (9), Enter (13), Delete (46), etc.
            if (key === 8 || key === 9 || key === 13 || key === 46 || (key >= 37 && key <= 40)) {
                return true;
            }
            // Validar si es un número (teclas 0-9)
            if (key >= 48 && key <= 57) {
                return true;
            }
            // Evitar cualquier otro carácter
            return false;
        }
    </script>
</asp:Content>
