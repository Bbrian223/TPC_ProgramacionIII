<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="HomeStaff.aspx.cs" Inherits="WebApplication1.ViewsStaff.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .mesa-cuadrado {
            display: flex;
            align-items: center;
            justify-content: center;
            width: 100px;
            height: 100px;
            background-color: #28a745; /* Verde para disponible */
            color: white;
            font-weight: bold;
            border-radius: 8px;
            transition: transform 0.2s, background-color 0.2s;
            margin: 20px;
        }

            .mesa-cuadrado:hover {
                transform: scale(1.1);
                background-color: #218838;
                color: whitesmoke;
            }

        /* Ejemplo de estados */
        .mesa-ocupada {
            background-color: #f5ed35; /* Amarillo para ocupada */
        }

            .mesa-ocupada:hover {
                transform: scale(1.1);
                background-color: #e4db17;
                color: whitesmoke;
            }

        .mesa-pendiente {
            background-color: #dc3545; /* Rojo para pendiente */
        }

            .mesa-pendiente:hover {
                transform: scale(1.1);
                background-color: #da1b2e;
                color: whitesmoke;
            }

        .mesa-cerrada {
            background-color: #6c757d; /* Gris para cerrada */
        }

            .mesa-cerrada:hover {
                transform: scale(1.1);
                background-color: #6a757d;
                color: whitesmoke;
            }
    </style>


    <h2>Salon
        <asp:Label ID="lblIdSalon" runat="server" />: </h2>

    <hr class="my-4" />

    <!--Contenedor para las mesas-->
    <div class="container mt-4">
        <div class="row" id="ContenedorMesas" runat="server">

            <!-- 5 filas con 5 mesas cada una -->

        </div>
    </div>

    <!--Contenedor etiquetas y botones-->
    <div class="container" style="margin-top: 50px">

        <!--Etiqutas indicador de colores-->
        <div class="pagination justify-content-start">
            <div class="row">
                <div class="col d-flex align-items-center">
                    <div class="me-2" style="width: 20px; height: 20px; background-color: #28a745;"></div>
                    <asp:Label runat="server" Text="Disponible"></asp:Label>
                </div>
                <div class="col d-flex align-items-center">
                    <div class="me-2" style="width: 20px; height: 20px; background-color: #f5ed35;"></div>
                    <asp:Label runat="server" Text="Ocupada"></asp:Label>
                </div>
                <div class="col d-flex align-items-center">
                    <div class="me-2" style="width: 20px; height: 20px; background-color: #dc3545;"></div>
                    <asp:Label runat="server" Text="Pendiente"></asp:Label>
                </div>
                <div class="col d-flex align-items-center">
                    <div class="me-2" style="width: 20px; height: 20px; background-color: #6c757d;"></div>
                    <asp:Label runat="server" Text="Cerrada"></asp:Label>
                </div>
            </div>
        </div>

        <!--Botones de pagina-->
        <nav aria-label="Page navigation example">
            <div id="ContenedorPaginador" class="pagination justify-content-end" runat="server">
            </div>
        </nav>
    </div>


</asp:Content>
