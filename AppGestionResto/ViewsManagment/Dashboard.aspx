<%@ Page Title="" Language="C#" MasterPageFile="~/ViewCommon/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WebApplication1.ViewsManagment.Dashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .custom-card {
            width: 300px; /* Ajusta el ancho de la carta */
            height: 120px; /* Ajusta la altura de la carta */
            max-width: 18rem;
            color:black;
            border:3px solid gray
            
        }

            .custom-card .card-body {
                display: flex;
                flex-direction: column;
                justify-content: center;
                align-items: center;
                text-align: center;
            }

            .custom-card .card-body span.custom-label {
                font-size: 30px; /* Ajusta el tamaño del texto dentro del Label */
            }

        .label-card {
            font-size: 20px; /* Ajusta el tamaño del encabezado */
            font-weight: bold;
        }

        .charts-wrapper {
            display: flex;
            justify-content: center; /* Centra los gráficos horizontalmente */
            gap: 20px; /* Espacio entre los gráficos */
        }

        .chart-box {
            width: 600px;
            height: 250px;
            border: 3px solid gray;
            border-radius: 15px;
        }

        .gView-box {
            width: 600px;
            height: 200px;
            border: 3px solid gray;
            border-radius: 15px;
            overflow: auto;
            margin-top: 15px;
            text-align: center;
        }
    </style>



    <div class="container d-flex justify-content-between">
        <!--Recaudacion Diaria-->
        <div class="card mb-3 custom-card" style="background-color:#9bd8e4">
            <div class="card-body text-center">
                <label class="label-card">Recaudacion Diaria</label>
                <asp:Label ID="lblRecaudacionDiaria" runat="server" CssClass="custom-label" />
            </div>
        </div>
        <!--Pedidos en Curso-->
        <div class="card mb-3 custom-card" style="background-color:#fef83f">
            <div class="card-body text-center">
                <label class="label-card">Pedidos en curso</label>
                <asp:Label ID="lblPedidosEnCurso" Text="20" runat="server" CssClass="custom-label" />
            </div>
        </div>

        <!--Pedidos Completados-->
        <div class="card mb-3 custom-card" style="background-color:#60eb57">
            <div class="card-body text-center">
                <label class="label-card">Pedidos completados</label>
                <asp:Label ID="lblPedidosCompletadosDia" Text="42" runat="server" CssClass="custom-label" />
            </div>
        </div>
        <!--Pedidos Cancelados-->
        <div class="card mb-3 custom-card" style="background-color:#ff7575">
            <div class="card-body text-center">
                <label class="label-card">Pedidos cancelados</label>
                <asp:Label ID="lblPedidosCancelados" Text="3" runat="server" CssClass="custom-label" />
            </div>
        </div>

    </div>

    <div class="charts-wrapper">
        <div class="chart-box">
            <canvas id="chartBarras"></canvas>
        </div>
        <div class="chart-box">
            <canvas id="chartDona"></canvas>
        </div>
    </div>

    <div class="charts-wrapper">
        <!--gView Pedidos por empleados-->
        <div class="gView-box">
            <h7>Pedidos por empleados</h7>
            <asp:GridView ID="gViewEmpleados" runat="server" CssClass="table table-striped table-bordered text-center"
                AutoGenerateColumns="False" DataKeyNames="IdEmpleado">
                <Columns>
                    <asp:BoundField DataField="IdEmpleado" HeaderText="ID" ItemStyle-Width="50px" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="PedidosGenerados" HeaderText="Pedidos" ItemStyle-Width="100px" />
                </Columns>
            </asp:GridView>
        </div>
        <!--gView Productos bajo stock-->
        <div class="gView-box">
            <h7>Productos con bajo stock</h7>
            <asp:GridView ID="gViewProductos" runat="server" CssClass="table table-striped table-bordered text-center"
                AutoGenerateColumns="False" DataKeyNames="IdProducto">
                <Columns>
                    <asp:BoundField DataField="IdProducto" HeaderText="ID" ItemStyle-Width="50px" />
                    <asp:BoundField DataField="Categoria.Nombre" HeaderText="Categoria" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="Stock" HeaderText="Stock" ItemStyle-Width="100px" />
                </Columns>
            </asp:GridView>
        </div>
    </div>


    <asp:HiddenField ID="VentasJson" runat="server" />
    <asp:HiddenField ID="MesasJson" runat="server" />





    <script type="text/javascript">
        document.addEventListener("DOMContentLoaded", function () {
            // Obtener datos del HiddenField
            var jsonData = document.getElementById('<%= VentasJson.ClientID %>').value;
            var ventas = JSON.parse(jsonData);

            // Extraer categorías y valores
            var labels = Object.keys(ventas);
            var data = Object.values(ventas);

            // Configuración del gráfico
            var ctx = document.getElementById('chartBarras').getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Ventas',
                        data: data,
                        backgroundColor: 'rgba(54, 162, 235, 0.6)',
                        borderColor: 'rgba(54, 162, 235, 1)',
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    scales: {
                        y: {
                            beginAtZero: true,
                            suggestedMax: 100 // 🔹 Ajusta este valor según lo que necesites
                        }
                    }
                }
            });

            /********************************* Grafico dona ********************************/

            // Obtener datos del HiddenField
            var jsonData2 = document.getElementById('<%= MesasJson.ClientID %>').value;
            var mesas = JSON.parse(jsonData2);

            // Extraer categorías y valores
            var labels = Object.keys(mesas);
            var data = Object.values(mesas);

            // Configuración del gráfico
            var ctx = document.getElementById('chartDona').getContext('2d');
            var myChart2 = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: labels,
                    datasets: [{
                        data: data,
                        backgroundColor: [
                            'rgba(75, 192, 192, 0.6)',  //DISPONIBLE
                            'rgba(255, 206, 86, 0.6)',   //OCUPADO
                            'rgba(54, 162, 235, 0.6)',  //PENDIENTE
                            'rgba(255, 99, 132, 0.6)',  //CERRADA
                            'rgba(153, 102, 255, 0.6)'
                        ],
                        borderColor: [
                            'rgba(75, 192, 192, 1)',    //DISPONIBLE
                            'rgba(255, 206, 86, 1)',    //OCUPADO
                            'rgba(54, 162, 235, 1)',    //PENDIENTE
                            'rgba(255, 99, 132, 1)',    //CERRADA
                            'rgba(153, 102, 255, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false
                }
            });

        });
    </script>


</asp:Content>
