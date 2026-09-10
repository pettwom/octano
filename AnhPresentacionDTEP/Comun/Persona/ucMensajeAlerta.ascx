<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMensajeAlerta.ascx.cs" Inherits="AnhHydroTalleresOpeGarrafasPresentacion.Controles.ucMensajeAlerta" %>
<div class="alert alert-warning alert-dismissible fade in" role="alert" id="divAlertas" runat="server">
    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
        <span aria-hidden="true">&times;</span>
    </button>
    <strong>
        <asp:Label ID="lblTipo" runat="server" Text="Ninguno"></asp:Label>
    </strong>
    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
</div>
