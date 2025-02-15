<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="HomeManagment.aspx.cs" Inherits="WebApplication1.ViewsManagment.Home" %>

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
            color:whitesmoke;
        }

        /* Ejemplo de estados */
        .mesa-ocupada {
            background-color: #f5ed35; /* Amarillo para ocupada */
        }

        .mesa-ocupada:hover {
            transform: scale(1.1);
            background-color: #e4db17;
            color:whitesmoke;
        }

        .mesa-pendiente {
            background-color: #dc3545; /* Rojo para pendiente */
        }

        .mesa-pendiente:hover {
            transform: scale(1.1);
            background-color: #da1b2e; 
            color:whitesmoke;
        }

        .mesa-cerrada {
            background-color: #6c757d; /* Gris para cerrada */
        }

        .mesa-cerrada:hover {
            transform: scale(1.1);
            background-color: #6a757d;
            color:whitesmoke;
        }
    </style>


    <h2>Mesas</h2>

    <hr class="my-4" />

    <div class="container mt-4">
        <div class="row" id="ContenedorMesas" runat="server">
                    
            <!-- 5 filas con 5 mesas cada una -->

        </div>
    </div>


</asp:Content>
