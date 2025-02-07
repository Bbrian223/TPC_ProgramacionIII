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
        }

        .mesa-cuadrado:hover {
            transform: scale(1.1);
            background-color: #218838; /* Verde más oscuro en hover */
            color:gray;
        }

        /* Ejemplo de estados */
        .mesa-ocupada {
            background-color: #dc3545; /* Rojo para ocupada */
        }

        .mesa-cerrada {
            background-color: #6c757d; /* Gris para cerrada */
        }
    </style>

    <script>

        document.querySelectorAll('.mesa-cuadrado').forEach(mesa => {
            mesa.addEventListener('click', () => {
                const idMesa = mesa.getAttribute('id').split('-')[1];
                window.location.href = `MesaDetalle.aspx?id=${idMesa}`;
            });
        });
    </script>


    <h2>Mesas</h2>

    <hr class="my-4" />

    <div class="container mt-4">
        <div class="row">
            <!-- 5 filas con 5 mesas cada una -->
            <% for (int i = 1; i <= 24; i++)

                { %>
            <div class="col-2 d-flex justify-content-center mb-3">
                <a href="MesaDetalle.aspx?id=<%= i %>"
                    class="mesa-cuadrado text-decoration-none text-center"
                    id="mesa-<%= i %>">Mesa <%= i %>
            </a>
            </div>
            <% } %>
        </div>
    </div>


</asp:Content>
