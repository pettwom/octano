<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfAutenticacion.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.Persona.wfAutenticacion" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>OCTANO CALIDAD</title>
    <%--<script src="../../UI/bootstrap/js/bootstrap.min.js"></script>--%>
    <link href="../../UI/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../UI/css/hydro.css" rel="stylesheet" />
    <script src="../../UI/js/jquery-1.10.2.min.js"></script>
    <style type="text/css">
        .top-content .text {
            font-family: 'Century Gothic','anh_font';
            color: #ffffff;
        }

            .top-content .text h1 {
                color: #000;
            }

        .top-content .description {
            margin: 20px 0 10px 0;
        }

            .top-content .description p {
                opacity: 0.8;
            }

            .top-content .description a {
                color: #2d7e3e;
            }

                .top-content .description a:hover,
                .top-content .description a:focus {
                    border-bottom: 1px dotted #2d7e3e;
                }

        .form-box {
            margin-top: 35px;
        }

        .form-top {
            overflow: hidden;
            padding: 0 25px 15px 25px;
            background: #444;
            background: rgba(45, 126, 62, 0.5);
            -moz-border-radius: 4px 4px 0 0;
            -webkit-border-radius: 4px 4px 0 0;
            border-radius: 4px 4px 0 0;
            text-align: left;
        }
        .form-top-left {
            float: left;
            width: 75%;
            padding-top: 25px;
        }
            .form-top-left h3 {
                margin-top: 0;
                color: #ffffff;
            }

            .form-top-left p {
                opacity: 0.8;
                color: #ffffff;
            }

        .form-top-right {
            float: left;
            width: 25%;
            padding-top: 5px;
            font-size: 66px;
            color: #2d7e3e;
            line-height: 100px;
            text-align: right;
            opacity: 0.3;
        }

        .form-bottom {
            padding: 25px 25px 30px 25px;
            background: #444;
            background: rgba(45, 126, 62, 0.5);
            -moz-border-radius: 0 0 4px 4px;
            -webkit-border-radius: 0 0 4px 4px;
            border-radius: 0 0 4px 4px;
            text-align: left;
        }

            .form-bottom form textarea {
                height: 100px;
            }

            .form-bottom form button.btn {
                width: 100%;
            }

            .form-bottom form .input-error {
                border-color: #de615e;
            }

        .social-login {
            margin-top: 35px;
        }

            .social-login h3 {
                color: #ffffff;
            }

        .social-login-buttons {
            margin-top: 25px;
        }

        /* ─── Professional refinements ─── */

        /* Sombra en la tarjeta de login */
        .form-bottom {
            box-shadow: 0 4px 20px rgba(0,0,0,0.18);
        }

        /* Focus en inputs con verde corporativo */
        .form-control:focus {
            border-color: #2D7E3E !important;
            box-shadow: 0 0 0 3px rgba(45,126,62,0.12) !important;
            outline: none !important;
        }

        /* Input group icon consistente */
        .input-group-addon {
            transition: border-color 0.2s ease;
        }
        .has-focus .input-group-addon {
            border-color: #2D7E3E;
        }

        /* Botón de inicio de sesión */
        #loginUsuario_LoginButton {
            padding: 10px 24px !important;
            font-weight: 600 !important;
            letter-spacing: 0.5px !important;
            transition: all 0.2s ease !important;
            border-radius: 4px !important;
            font-size: 15px !important;
        }
        #loginUsuario_LoginButton:hover {
            background-color: #236832 !important;
            border-color: #236832 !important;
            box-shadow: 0 3px 10px rgba(0,0,0,0.2) !important;
        }
        #loginUsuario_LoginButton:active {
            transform: scale(0.97);
        }

        /* Botones de acción social más pulidos */
        .social-login-buttons .btn {
            border-radius: 4px;
            font-weight: 500;
            padding: 9px 22px;
            transition: all 0.2s ease;
        }
        .social-login-buttons .btn-success:hover {
            background-color: #236832 !important;
            border-color: #236832 !important;
            box-shadow: 0 2px 8px rgba(0,0,0,0.15);
        }
        .social-login-buttons .btn-warning:hover {
            box-shadow: 0 2px 8px rgba(0,0,0,0.15);
        }

        /* Título de plataforma más destacado */
        .top-content .text h3 {
            font-weight: 700;
            letter-spacing: 0.3px;
        }
    </style>
</head>
<body>

    <div class="container theme-showcase" role="main">

        <div class="top-content">

            <div class="inner-bg">
                <div class="container">
                    <div class="row">

                        <div class="col-sm-4 col-sm-offset-3 col-lg-3 form-box">
                            <img src="../../UI/img/VolumenesCalidad/Hydro_titulo_small_octano_calidad2.png" width="100%" /><br />
                        </div>

                        <div class="col-xs-12 col-sm-5 col-sm-offset-3 text">

                            <h3><strong>Plataforma Única Informática</strong></h3>

                            <div class="description">
                                <p>
                                  <%-- Sistema Octano Control de Calidad de Carburantes y Lubricantes para el registro de pruebas establecidas en D.S.1499 y D.S.2741.--%>
                                    Sistema Octano Control de Calidad de Carburantes y Lubricantes para el registro de pruebas establecidas en D.S. 5619.
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6 col-sm-offset-3 form-box">
                            <div class="form-top">
                                <div class="form-top-left">
                                    <h3>Inicio de Sesión</h3>
                                    <p>Igrese su usuario (correo personal) y contraseña:</p>
                                    <div class="text-center alert alert-warning" id="divMensaje" runat="server" visible="false">
                                        <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-bottom">
                                <form id="form1" runat="server" role="form" method="post" class="login-form">
                                    <asp:Login ID="loginUsuario" runat="server" OnAuthenticate="loginUsuario_Authenticate" OnLoggedIn="loginUsuario_LoggedIn" Width="100%">
                                        <LayoutTemplate>
                                            <div class="form-group">
                                                <asp:Label ID="Label1" runat="server" CssClass="control-label col-sm-3" ForeColor="White" Text="Usuario"></asp:Label>
                                                <div class="col-sm-9">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><i aria-hidden="true" class="glyphicon glyphicon-user" style="color: #2d7e3e !important"></i></span>
                                                        <asp:TextBox ID="UserName" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" CssClass="text-danger" ErrorMessage="El nombre de usuario es obligatorio." ToolTip="El nombre de usuario es obligatorio." ValidationGroup="loginUsuario">*</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label2" runat="server" CssClass="control-label col-sm-3" ForeColor="White" Text="Contraseña"></asp:Label>
                                                <div class="col-sm-9">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><i aria-hidden="true" class="glyphicon glyphicon-log-in" style="color: #2d7e3e !important"></i></span>
                                                        <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password" Width="100%"></asp:TextBox>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria." ValidationGroup="loginUsuario">*</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label3" runat="server" CssClass="control-label col-sm-3" ForeColor="White" Text="Captcha"></asp:Label>
                                                <div class="col-sm-7">
                                                    <dx:ASPxCaptcha ID="CaptchaLogin" runat="server" CodeLength="6">
                                                        <TextBox Position="Bottom" />
                                                        <ChallengeImage BackgroundColor="#F2F8FF" BorderColor="#AECAF0" BorderWidth="1" ForegroundColor="#426798">
                                                        </ChallengeImage>
                                                        <ValidationSettings ErrorText="Código incorrecto"></ValidationSettings><RefreshButton Text="Mostrar otro código"></RefreshButton><TextBox LabelText="Escriba el código mostrado" Position="Top" ShowLabel="False" /><challengeimage backgroundcolor="#F2F8FF" bordercolor="#AECAF0" borderwidth="1" foregroundcolor="#426798" Width="300"></challengeimage>
                                                    </dx:ASPxCaptcha>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-10 text-center">
                                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="btn btn-success" Text="Inicio de sesión" ValidationGroup="loginUsuario" />
                                                </div>
                                            </div>
                                        </LayoutTemplate>
                                    </asp:Login>
                                </form>


                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3"></div>
                        <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 social-login">
                            <h3>¿Olvidaste tu contraseña?</h3>
                            <div class="social-login-buttons">
                                <a class="btn btn-success" href="https://ppa3vrsger02p.anh.gob.bo:9343/H201Y7070D4/Sitio/Persona/wfRecuperacionCuenta.aspx">Restituir una nueva contraseña
                                </a>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3 social-login">
                            <h3>¿Eres nuevo en el sistema?</h3>
                            <div class="social-login-buttons">
                                <a class="btn btn-warning" href="https://ppa3vrsger02p.anh.gob.bo:9343/H201Y7070D4/Sitio/Persona/wfSolicitarCuenta.aspx">Crear una cuenta nueva
                                </a>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3"></div>
                    </div>
                </div>
            </div>
        </div>

        <div class="backstretch" style="left: 0px; top: 0px; overflow: hidden; margin: 0px; padding: 0px; height: 100%; width: 100%; z-index: -999999; position: fixed;">
            <img src="../../UI/img/bg.jpg" style="position: absolute; margin: 0px; padding: 0px; border: none; width: 100%; height: 100%; max-height: none; max-width: none; z-index: -999999; top: 0px;">
        </div>
    </div>

</body>
<%--<body>
    <div class="container">
        <form id="form1" runat="server" class="form-horizontal">
            <div class="row">
                <div class="col-sm-4 col-sm-offset-3 col-lg-3 form-box">
                    <img src="../../UI/img/hydro_logo.png" width="100%" /><br />
                </div>

                <div class="col-xs-12 col-sm-5 col-sm-offset-3 text">
                    <h3><strong>Plataforma Única Informática</strong></h3>
                    <div class="description">
                        <p>
                            Plataforma única informática de la Agencia Nacional de Hidrocarburos donde se encuentran todos los módulos de administración y control por áreas.
                        </p>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-4 col-sm-offset-3 col-lg-5 form-box">
                    <div class="text-center">
                        <span class="glyphicon glyphicon-user" style="font-size: 70px"></span>
                    </div>
                    <h2 class="text-center login-title">Iniciar sesión</h2>
                    <div class="text-center alert alert-warning" id="divMensaje" runat="server" visible="false">
                        <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
                    </div>
                    <asp:Login ID="loginUsuario" runat="server" OnAuthenticate="loginUsuario_Authenticate" OnLoggedIn="loginUsuario_LoggedIn" Width="100%">
                        <LayoutTemplate>
                            <form role="form" method="post" class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="Usuario" CssClass="control-label col-sm-3"></asp:Label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="UserName" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" CssClass="text-danger" ErrorMessage="El nombre de usuario es obligatorio." ToolTip="El nombre de usuario es obligatorio." ValidationGroup="loginUsuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" Text="Contraseña" CssClass="control-label col-sm-3"></asp:Label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria." ValidationGroup="loginUsuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-10 text-center">
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="btn btn-default" Text="Inicio de sesión" ValidationGroup="loginUsuario" />
                                    </div>
                                </div>
                            </form>
                        </LayoutTemplate>
                    </asp:Login>

                </div>
            </div>
            <div class="backstretch" style="left: 0px; top: 0px; overflow: hidden; margin: 0px; padding: 0px; height: 100%; width: 100%; z-index: -999999; position: fixed;">
                
            </div>
        </form>
    </div>
</body>--%>
</html>


