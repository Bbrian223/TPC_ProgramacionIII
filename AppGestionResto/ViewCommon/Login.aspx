<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.ViewCommon.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!-- link de Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            height: 100%;
            margin: 0;
            font-family: Arial, sans-serif;
        }

        .background {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color:darkcyan;
            background-size: cover;
            background-position: center;
            opacity: 0.7;
            z-index: -1;
        }

        .login-container {
            max-width: 400px;
            margin: 100px auto;
            background-color: rgba(255, 255, 255, 0.9);
            border-radius: 10px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            padding: 20px;
        }

        .login-title {
            color: #007bff;
            font-weight: bold;
            margin-bottom: 20px;
            font-style: italic;
        }

        .form-label {
            color: #555;
        }

        .btn-hospital {
            background-color: #007bff;
            border: none;
        }

            .btn-hospital:hover {
                background-color: #00BFFF;
            }

        .forgot-password {
            color: #007bff;
        }

            .forgot-password:hover {
                text-decoration: underline;
            }

        .footer {
            background-color: #f8f9fa;
            text-align: center;
            padding: 10px 0;
        }

        /* Estilo de borde rojo para campo incorrecto */
        .is-invalid {
            border-color: red !important;
        }

        /* Estilo del mensaje de error */
        .error-message {
            color: red;
            display: none;
            font-size: 0.875em;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="background"></div>
        
        <div class="container">
            <div class="login-container">

                <h2 class="text-center login-title">Resto Bar</h2>

                <div class="mb-3">
                    <label for="usuario" class="form-label">Usuario</label>
                    <asp:TextBox ID="txtUsuario" CssClass="form-control" placeholder="Ingrese su usuario" runat="server" />
                    <span id="userErrorMsg" runat="server" class="error-message">Usuario incorrecto. Intente nuevamente.</span>
                </div>

                <div class="mb-3">
                    <label for="password" class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" placeholder="Ingrese su contraseña" runat="server" />
                    <span id="errorMessage" runat="server" class="error-message">Contraseña incorrecta. Intente nuevamente.</span>
                </div>

                <div class="mb-3 form-check">
                    <input type="checkbox" class="form-check-input" id="rememberMe" />
                    <label class="form-check-label" for="rememberMe">Recordarme</label>
                </div>

                <asp:Button ID="btnValidate" CssClass="btn btn-hospital w-100" Text="Iniciar sesión" OnClick="btnValidate_Click" runat="server" />

            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js"></script>

</body>
</html>
