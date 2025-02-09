<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderDetail.aspx.cs" Inherits="WebApplication1.ViewCommon.orderDetail" %>

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
    <form id="form1" runat="server">

        <nav class="navbar navbar-light bg-light">
            <div class="container-fluid">
                <asp:Button ID="btnCerraVentana" runat="server" CssClass="btn-close"/>

                <h4 style="margin-right:90px">Detalle de pedido Mesa:
                    <asp:Label ID="lblIdMesa" runat="server" /></h4>
            </div>
        </nav>

        <div class="container-fluid vh-100">
            <div class="row h-100">
                <!-- Columna izquierda (30%) -->
                <div class="col-8 bg-primary text-white d-flex align-items-center justify-content-center">
                    <h2>Sección Izquierda</h2>
                </div>

                <!-- Columna derecha (70%) -->
                <div class="col-4 bg-success text-white d-flex align-items-center justify-content-center">
                    <h2>Sección Derecha</h2>
                </div>
            </div>
        </div>





    </form>
</body>
</html>
