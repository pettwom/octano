<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucRegistroCertificadoCalidadCarburantes.ascx.cs" Inherits="AnhPresentacionDTEP.Comun.UControl.ucRegistroCertificadoCalidadCarburantes" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<script type="text/javascript">
    function MesageConfirm() {
        $.msgBox({
            title: "Confirmación",
            content: "¿Esta seguro de registrar estos datos?.",
            type: "confirm",
            buttons: [{ value: "Aceptar" }, { value: "Cancelar" }],
            success: function (result) {
                if (result == "Aceptar") {
                    <%--OnClick="btnGuardarRegistroCalidad_Click"--%>
                    $("#btnGuardarRegistroCalidad").trigger("click");
                }
            }
        });
    }
</script>
<script type="text/javascript">
    function upper(ustr) {
        var str = ustr.value;
        ustr.value = str.toUpperCase();
    }
    function tb1_OnKeyUp(s, e) {
        s.SetText(s.GetText().toUpperCase());
    }
    document.onkeydown = function (evt) { return (evt ? evt.which : event.keyCode) != 13; }
</script>
<style>
    .switch
    {
        position: relative;
        display: inline-block;
        width: 60px;
        height: 34px;
    }

        /* Hide default HTML checkbox */
        .switch input
        {
            display: none;
        }

    /* The slider */
    .slider
    {
        position: absolute;
        cursor: pointer;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background-color: #ccc;
        -webkit-transition: .4s;
        transition: .4s;
        box-shadow: 0 1px 2px #888, 0 0px 3px #777, inset 0 -1px 5px #333;
    }

        .slider:before
        {
            position: absolute;
            content: "";
            height: 26px;
            width: 26px;
            left: 4px;
            bottom: 4px;
            background-color: white;
            -webkit-transition: .4s;
            transition: .4s;
            box-shadow: 0 1px 2px #888, 0 0px 3px #777, inset 0 -1px 5px #333;
        }

    input:checked + .slider
    {
        background-color: #0eca0e;
    }

    input:focus + .slider
    {
        box-shadow: 0 0 1px #0eca0e;
    }

    input:checked + .slider:before
    {
        -webkit-transform: translateX(26px);
        -ms-transform: translateX(26px);
        transform: translateX(26px);
    }

    /* Rounded sliders */
    .slider.round
    {
        border-radius: 34px;
    }

        .slider.round:before
        {
            border-radius: 50%;
        }
</style>

<asp:Panel ID="Panel1" runat="server" Width="100%">
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true">
    </asp:ScriptManager>--%>
    <div style="display: table; height: 40px; background-color: #ffebcd; border-color: #ffd700; width: 100%; text-align: center;" runat="server" ID="divPanelSwitch" Visible="False">
        <div style="display: table-cell; vertical-align: middle">
            <strong style="color: #2e8b57">REALIZAR DECLARACIÓN SIN MOVIMIENT0 (VOLUMEN CERO)</strong></div>
        
            <label class="switch">
                <asp:CheckBox ID="ckbxVolumenCero" runat="server" OnCheckedChanged="ckbxVolumenCero_CheckedChanged" AutoPostBack="True" Checked="false" />
                <span class="slider round"></span>
            </label>
      
    </div>

    <div runat="server" visible="false" id="cite" class="Hydro_Alerta_Verde" style="background-color: #D3E8A8; border: thin solid #2e8b57; padding: 10px; text-align: center; width: 100%; font-weight: bold">
        <asp:Label ID="TextBoxNumeroCite" Visible="false" runat="server"></asp:Label>
    </div>

    <asp:Panel runat="server" ID="panelVolumenCero" Width="100%" Visible="False">
        <div class="Hydro_Alerta_Rojo" runat="server" visible="false" id="divAlertaVolCero" style="background-color: #ffd7c9; border: thin solid #c30; border-radius: 8px; padding: 10px; text-align: center; width: 90%;">
            <span></span>
            <asp:Label ID="lblAlertaVolumenCero" runat="server" Text=""></asp:Label>
        </div>

        <table align="center">
            <tr>
                <td>Fecha de Importación/Operación:</td>
                <td>
                    <dx:ASPxDateEdit ID="deFechaImportacionVolCero" runat="server">
                        <TimeSectionProperties>
                            <TimeEditProperties>
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                            </TimeEditProperties>
                        </TimeSectionProperties>
                        <ClearButton Visibility="Auto">
                        </ClearButton>
                        <ValidationSettings CausesValidation="True" ErrorDisplayMode="Text" ErrorText="" ErrorTextPosition="Bottom" ValidationGroup="vol0">
                            <RequiredField ErrorText="El campo es obligatorio llenar" IsRequired="True" />
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </td>
                <td>
                    <asp:Button ID="btnRegistrarVolumenCero" runat="server" Text="REGISTRAR" OnClick="btnRegistrarVolumenCero_Click" ValidationGroup="vol0" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="panelRegistroNormal">
        <center>
            <div style="font-weight: bold; text-align: center">
            <h4 style="color: #2D7E3E"><asp:Label ID="TextBoxDescripcionProducto" runat="server" Width="100%" Font-Bold="True"></asp:Label></h4>
            <p></p>
        </div>
        <table cssclass="Hydro_Content_Table" >
            
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="LabelFo" runat="server" Text="Fecha de Muestreo:"></asp:Label>
                </td>
                <td>
                    <dx:ASPxDateEdit ID="txtFechaInicial" runat="server"
                        Theme="DevEx"
                        DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                        EditFormat="Custom" ClientIDMode="Static" ClientInstanceName="txtFechaInicial" Width="100%">
                        <CalendarProperties ShowWeekNumbers="False"></CalendarProperties>
                        <TimeSectionProperties>
                            <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                        </TimeSectionProperties>
                        <ClientSideEvents ValueChanged="function(s, e) {grdCertificadoCalidad.PerformCallback();}" />
                    </dx:ASPxDateEdit>
                </td>
                <td style="text-align: right">
                    <asp:Label ID="LabelLote" runat="server" Text="Nº Lote:" Style="text-align: right"></asp:Label>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxLote" ErrorMessage="Campo Obligatorio" Style="color: #FF0000; font-size: small" ValidationGroup="a"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxLote" name="TextBoxLote" runat="server" Width="100%" MaxLength="30"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBoxLote" ErrorMessage="Formato Incorrecto" Style="color: #FF0000; font-size: 9pt" ValidationExpression="^[a-zA-Z 0-9ñÑ/-]*$" ValidationGroup="a"></asp:RegularExpressionValidator>
                </td>
                <td style="text-align: right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelTag" runat="server" Text="TAG:"></asp:Label>
                </td>
                <td class="auto-style2">
                    <dx:ASPxComboBox ID="cmbTag" runat="server" CssClass="Hydro_Content_Table" Width="100%"></dx:ASPxComboBox>
                </td>
                <td rowspan="5" style="padding:5px">

                    <asp:Panel runat="server" ID="pnlAdjuntaArchivo" Visible="true">
                        <table align="center">
                            <tr>
                                <td align="right">&nbsp;</td>
                                <td align="left">
                                    <asp:Label ID="lblOkArchivo" runat="server" Text="SE HA SELECCIONADO EL ARCHIVO: " Visible="False" ForeColor="#006600"></asp:Label>
                                    <asp:Label ID="lblNoArchivo" runat="server" Text="NO SE HA SELECCIONADO NINGUN ARCHIVO." Visible="False" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right"><b>  <asp:Label ID="Label3" runat="server" Text="Archivo en formato PDF:"></asp:Label></b></td>
                                <td align="left">
                                    <dx:ASPxUploadControl ID="upArchivo" runat="server" OnFileUploadComplete="upArchivo_FileUploadComplete" Width="300px">
                                        <ValidationSettings AllowedFileExtensions=".PDF">
                                        </ValidationSettings>
                                    </dx:ASPxUploadControl>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td align="right"><asp:Label ID="lblObservacion" runat="server" Text="Observación:" Font-Bold="true"></asp:Label></td>
                                <td align="left">
                                    <asp:TextBox ID="txtObservacion" runat="server" Height="64px" TextMode="MultiLine" Width="357px" MaxLength="450" Style="text-transform: uppercase;"></asp:TextBox>                                    
                                   <%-- &nbsp;
                                    <dx:ASPxTokenBox ID="tkblistaObs" ClientInstanceName="tkblistaObs" runat="server" Width="300px" TextField="CODIGO" ValueField="ID_PUNTO_CUSTODIO">                                    
                                    </dx:ASPxTokenBox>--%>
                                </td>
                            </tr>

                        </table>
                    </asp:Panel>


                </td>
            </tr>
             <!-- NUEVA FILA: N° Tanque y Precinto -->
            <asp:Panel runat="server" ID="pnlTanquePrecinto" Visible="false">
            <tr>
                <td class="etiqueta-campo" style="text-align: right">
                    <asp:Label ID="lblNroTanque" runat="server" Text="Placa de Cisterna:"></asp:Label>
                </td>
                <td>                   
                    <asp:TextBox ID="txtPlacaCisterna" runat="server" Width="100%" MaxLength="8" placeholder="9124GXA"></asp:TextBox>
                    <asp:RegularExpressionValidator
                        ID="revPlaca"
                        runat="server"
                        ControlToValidate="txtPlacaCisterna"
                        ValidationExpression="^\d{3,4}[A-Za-z]{3}$"
                        ErrorMessage="Formato inválido. Ejemplo: 784KFD ó 9124XPD"
                        ForeColor="Red"
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                </td>
                <td class="etiqueta-campo" style="text-align: right">                    
                    <asp:Label ID="lblNroPrecinto" runat="server" Text="Nª de Precinto:"></asp:Label>
                </td>
                <td>                    
                    <asp:TextBox ID="txtNroPrecintos" runat="server" Width="100%" MaxLength="60" placeholder="12451;12451;5124;4512;.."></asp:TextBox>
                    <asp:RegularExpressionValidator
                        ID="revPrecintos"
                        runat="server"
                        ControlToValidate="txtNroPrecintos"
                        ErrorMessage="Formato inválido. Ejemplo: 12345;67890"
                        ValidationExpression="^\d+(;\d+)*$"
                        ForeColor="Red"
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                </td>
                <td></td>
                <td></td>
            </tr>
            </asp:Panel>
             <!-- NUEVA FILA: Base A y Base B -->
            <asp:Panel runat="server" ID="pnlProductosBase" Visible="false">
            <tr>
                <td class="etiqueta-campo" style="text-align: right">
                    <asp:Label ID="lblBaseA" runat="server" Text="Nª Tanque DB:" Visible="true"></asp:Label>
                </td>
                <td>                   
                    <dx:ASPxComboBox ID="cmbBaseA" runat="server" Visible="true" Width="100%"></dx:ASPxComboBox>
                </td>
                <td class="etiqueta-campo" style="text-align: right">                    
                    <asp:Label ID="lblBaseB" runat="server" Text="Nª Tanque BD:" Visible="true"></asp:Label>
                </td>
                <td>                    
                    <dx:ASPxComboBox ID="cmbBaseB" runat="server" Visible="true" Width="100%"></dx:ASPxComboBox>
                </td>
                <td></td>
                <td></td>
            </tr>
            </asp:Panel>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label1" runat="server" Text="Volumen del Lote:"></asp:Label><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxVolumen" ErrorMessage="Campo Obligatorio" Style="color: #FF0000; font-size: 9pt" ValidationGroup="a"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxVolumen" runat="server" Width="100%"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator ID="rexpVolNominal" ControlToValidate="TextBoxVolumen" runat="server" ValidationExpression="^[1-9]+\d*(\,\d{1,3})?$" ErrorMessage="Formato Incorrecto (##,###)" Style="color: #FF0000" ValidationGroup="a" Font-Size="9pt"></asp:RegularExpressionValidator>
                    <%--<asp:RegularExpressionValidator ID="rexpVolNominal" ControlToValidate="TextBoxVolumen" runat="server" ValidationExpression="\d*\,?\d*" ErrorMessage="Formato Incorrecto (#,#)" style="color:#FF0000" ValidationGroup="a" Font-Size="9pt"></asp:RegularExpressionValidator>--%>                
                </td>
                <td style="text-align: right">
                    <asp:Label ID="Label2" runat="server" Text="Unidad Medida Volumen:"></asp:Label>
                </td>
                <td>
                    <dx:ASPxComboBox ID="cmbUnidadMedVol" runat="server" CssClass="Hydro_Content_Table" Width="100%">
                        <ClearButton Visibility="Auto">
                        </ClearButton>
                        <ValidationSettings ValidationGroup="a">
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                </td>
                <td style="text-align: right" class="auto-style1">
                    <asp:Label ID="lblEmpresaProveedora" runat="server" Text="Empresa Proveedora:" Visible="false"></asp:Label>
                    <asp:Label ID="LabelMarca" runat="server" Text="Marca:" Visible="false"></asp:Label>
                    <asp:Label ID="lblTipoOperacion" runat="server" Text="Mercado:" Visible="false"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmpresaProveedora" runat="server" Width="100%" MaxLength="40" Visible="false"></asp:TextBox>
                    <dx:ASPxComboBox ID="cmbMarca" runat="server" CssClass="Hydro_Content_Table" Visible="false" Width="100%"></dx:ASPxComboBox>
                    <dx:ASPxComboBox ID="cmbTipoOperacion" runat="server" CssClass="Hydro_Content_Table" Visible="false" Width="100%"></dx:ASPxComboBox>
                </td>
            </tr>            
            <div id="divDinamic" runat="server" visible="false">
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="LabelVolumenMuestra" runat="server" Text="Vol. de Muestra:" Visible="false"></asp:Label><br />
                        <asp:RequiredFieldValidator ID="LabelVolumenMuestraL" runat="server" ControlToValidate="TextBoxVolumenMuestra" ErrorMessage="Campo Obligatorio" Style="color: #FF0000; font-size: 9pt" ValidationGroup="a" Enabled="False"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxVolumenMuestra" runat="server" Visible="false" Width="100%"></asp:TextBox>
                        <br />
                        <asp:RegularExpressionValidator ID="LabelVolumenMuestraT" runat="server" ControlToValidate="TextBoxVolumenMuestra" Style="color: #FF0000; font-size: 9pt" ValidationExpression="^[1-9]+\d*(\,\d{1,3})?$" ErrorMessage="Formato Incorrecto (##,###)" ValidationGroup="a" Enabled="False"></asp:RegularExpressionValidator>
                    </td>
                    <td style="text-align: right">
                        
                        <asp:Label ID="LabelPrecio" runat="server" Text="Precio:" Visible="false"></asp:Label><br />
                        <asp:RequiredFieldValidator ID="TextBoxPrecioL" runat="server" ControlToValidate="TextBoxPrecio" ErrorMessage="Campo Obligatorio" Style="color: #FF0000; font-size: 9pt" ValidationGroup="a" Enabled="False"></asp:RequiredFieldValidator>
                    </td>
                    <td>                        
                        <asp:TextBox ID="TextBoxPrecio" runat="server" Visible="false" Width="100%"></asp:TextBox>                        
                        <br />
                        <asp:RegularExpressionValidator ID="TextBoxPrecioT" runat="server" ControlToValidate="TextBoxPrecio"  Style="color: #FF0000; font-size: 9pt" ValidationExpression="^[1-9]+\d*(\,\d{1,3})?$" ErrorMessage="Formato Incorrecto (##,###)" ValidationGroup="a" Enabled="False"></asp:RegularExpressionValidator>                        
                    </td>
                    <td style="text-align: right">                        
                        <dx:ASPxComboBox ID="cmbMoneda" runat="server" CssClass="Hydro_Content_Table" Width="70px" SelectedIndex="0" Visible="false"></dx:ASPxComboBox>
                       <%-- <br />--%>
                    </td>
                    <td></td>

                </tr>
            </div>
            <div id="divDinamicImp" runat="server" visible="false">                
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblResolucion" runat="server" Text="Resolución:" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtResolucion" runat="server" Visible="false" Width="100%" MaxLength="35"></asp:TextBox>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblFechaMuIm" runat="server" Text="Fecha de Importación:" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="txtFechaMuIm" runat="server" Visible="false"
                            Theme="DevEx"
                            DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                            EditFormat="Custom" ClientIDMode="Static" ClientInstanceName="txtFechaMuIm" Width="100%">
                            <CalendarProperties ShowWeekNumbers="False"></CalendarProperties>
                            <TimeSectionProperties>
                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                            </TimeSectionProperties>
                        </dx:ASPxDateEdit>
                        <dx:ASPxDateEdit ID="txtFechaRA" runat="server" Visible="false"
                            Theme="DevEx"
                            DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"
                            EditFormat="Custom" ClientIDMode="Static" ClientInstanceName="txtFechaMuIm" Width="100%">
                            <CalendarProperties ShowWeekNumbers="False"></CalendarProperties>
                            <TimeSectionProperties>
                                <TimeEditProperties EditFormatString="dd/MM/yyyy" />
                            </TimeSectionProperties>
                        </dx:ASPxDateEdit>
                    </td>
                    <td style="text-align: right">                        
                        <asp:Label ID="lblTanqueOrigenExterno" runat="server" Text="Tanque de Despacho en la Planta de Importacion:" Visible="false"></asp:Label>
                    </td>
                    <td>                        
                        <asp:TextBox ID="txtTanqueOrigenExterno" runat="server" Width="100%" MaxLength="20" Visible="false"></asp:TextBox>
                    </td>
                </tr>                                
            </div>
             <asp:Panel runat="server" ID="pnlEntidadesRelacionados" Visible="false">             
                <tr>
                    <td class="etiqueta-campo" style="text-align: right">
                        <asp:Label ID="lblModalidadTransp" runat="server" Text="Modalidad de Transporte:"></asp:Label>
                    </td>
                    <td>                  
                        <dx:ASPxComboBox ID="cmbModalidadTransporte" runat="server" Width="100%"
                            TextField="COD_TRANSP" ValueField="COD_TRANSP">
                        </dx:ASPxComboBox>                   
                    </td>
                    <td class="etiqueta-campo" style="text-align: right">
                        <asp:Label ID="lbldestino" runat="server" Text="Destino Final del Producto:"></asp:Label>
                    </td>
                    <td>      
                        <dx:ASPxComboBox ID="cmbEnitdadDestino" runat="server" Visible="true" Width="100%"></dx:ASPxComboBox>                               
                    </td>
                    <td class="etiqueta-campo" style="text-align: right"><asp:Label ID="lblRutaInternacion" runat="server" Text="Ruta de Internacion:"></asp:Label></td>
                    <td><asp:TextBox ID="txtRutaInternacion" runat="server" Width="100%" MaxLength="50"></asp:TextBox>    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td style="text-align: right"><asp:Label ID="lblNroLoteVerf" runat="server" Text="N° Lote de Verificacion:" Visible="false"></asp:Label></td>
                <td><asp:TextBox ID="txtNroLoteVerf" runat="server" Width="100%" MaxLength="25" Visible="false"></asp:TextBox></td>
            </tr>
            <div id="divDinamicNomProd" runat="server" visible="false">
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblNombreProducto" runat="server" Text="Nombre del producto:" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombreProducto" runat="server" Visible="false" Width="100%"></asp:TextBox>
                        <dx:ASPxComboBox ID="cmbNombreProducto" runat="server" CssClass="Hydro_Content_Table" Visible="false" Width="100%"></dx:ASPxComboBox>
                    </td>                 
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </div>
        </table>
        <div align="left">
            <asp:Button ID="btnVisualiza" runat="server" Text="Visualizar Parametros" OnClick="btnVisualiza_Click" Visible="False"></asp:Button></div>
        <%--<dx:ASPxGridView  OnHtmlDataCellPrepared="gvImports_HtmlDataCellPrepared"  ID="grdCertificadoCalidad" runat="server" CssClass="Hydro_Content_Table" OnHtmlRowCreated="grdCertificadoCalidad_HtmlRowCreated" AutoGenerateColumns="False" OnCustomColumnDisplayText="grdCertificadoCalidad_CustomColumnDisplayText" ClientInstanceName="grdCertificadoCalidad" OnCustomCallback="grdCertificadoCalidad_CustomCallback" Font-Size="Smaller" Width="100%" ><Columns><dx:GridViewDataTextColumn  Caption="#" Name="Numero" VisibleIndex="1"><Settings HeaderFilterMode="CheckedList"></Settings></dx:GridViewDataTextColumn><dx:GridViewDataTextColumn Caption="decIdPruebaCalidad" FieldName="decIdPruebaCalidad" Visible="false"></dx:GridViewDataTextColumn><dx:GridViewDataTextColumn Caption="Prueba / Ensayo" FieldName="strDescripcion" VisibleIndex="2"><Settings HeaderFilterMode="CheckedList"></Settings></dx:GridViewDataTextColumn><dx:GridViewDataColumn Caption="Método ASTM" VisibleIndex="3" ><Settings AllowAutoFilter="False" /><DataItemTemplate><dx:ASPxComboBox ID="cmbMetodoASTM" runat="server" CssClass="Hydro_Content_Table"></dx:ASPxComboBox></DataItemTemplate></dx:GridViewDataColumn><dx:GridViewDataColumn Caption="Unidad" VisibleIndex="5" ><Settings AllowAutoFilter="False" /><DataItemTemplate><dx:ASPxComboBox ID="cmbUnidad" runat="server" CssClass="Hydro_Content_Table" AutoPostBack="False"></dx:ASPxComboBox></DataItemTemplate></dx:GridViewDataColumn>
        <dx:GridViewDataColumn Caption="Valor reportado" VisibleIndex="6" ><Settings AllowAutoFilter="False" /><DataItemTemplate><dx:ASPxTextBox ID="txtValorReportado" CssClass="allUpper" runat="server" OnTextChanged="TextBox1_Click" EnableDefaultAppearance="False" Border-BorderWidth="1"><ClientSideEvents KeyUp="tb1_OnKeyUp" /></dx:ASPxTextBox></DataItemTemplate></dx:GridViewDataColumn><dx:GridViewDataColumn Caption="ISO/NLGI" VisibleIndex="7"  ><Settings AllowAutoFilter="False" /><DataItemTemplate><dx:ASPxComboBox ID="cmbIsoNlgi"  OnSelectedIndexChanged="VobjIsoNlgi_SelectedIndexChanged" runat="server"  CssClass="Hydro_Content_Table" AutoPostBack="True"></dx:ASPxComboBox></DataItemTemplate></dx:GridViewDataColumn><dx:GridViewDataTextColumn Caption="Espec </br>Mínima" Name="EspecMinima" FieldName="strEspecMinima" VisibleIndex="8"><Settings HeaderFilterMode="CheckedList"></Settings></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Espec </br>Máxima"  Name="EspecMaxima" FieldName="strEspecMaxima" VisibleIndex="9"><Settings HeaderFilterMode="CheckedList"></Settings></dx:GridViewDataTextColumn><dx:GridViewDataColumn Caption="Justificacion" VisibleIndex="10" Visible="False"><Settings AllowAutoFilter="False" /><DataItemTemplate><dx:ASPxTextBox ID="txtJustificacion" runat="server"></dx:ASPxTextBox></DataItemTemplate></dx:GridViewDataColumn><dx:GridViewDataTextColumn Caption="Especifiación</br>Alfanumerica" FieldName="strEspecAlfanumerico" VisibleIndex="11"><Settings HeaderFilterMode="CheckedList"></Settings></dx:GridViewDataTextColumn></Columns><SettingsPager PageSize="30" Visible="False"><PageSizeItemSettings Visible="true" ShowAllItem="true" /></SettingsPager><Settings ShowHeaderFilterButton="True" /><SettingsPopup><HeaderFilter MinHeight="300" MinWidth="300"></HeaderFilter></SettingsPopup><Paddings Padding="5px" /><Styles><Header CssClass="Hydro_Td_Titulo"></Header><Row CssClass="Hydro_Td_Contenido"></Row><Table CssClass="Hydro_Content_Table"></Table></Styles><StylesEditors><TextBox Font-Bold="True"></TextBox></StylesEditors></dx:ASPxGridView>--%>
        <dx:ASPxGridView OnHtmlDataCellPrepared="gvImports_HtmlDataCellPrepared" ID="grdCertificadoCalidad" runat="server" CssClass="Hydro_Content_Table" OnHtmlRowCreated="grdCertificadoCalidad_HtmlRowCreated" AutoGenerateColumns="False" OnCustomColumnDisplayText="grdCertificadoCalidad_CustomColumnDisplayText" ClientInstanceName="grdCertificadoCalidad" OnCustomCallback="grdCertificadoCalidad_CustomCallback" Font-Size="Smaller" Width="100%">
            <Columns>
                <dx:GridViewDataTextColumn Caption="#" Name="Numero" VisibleIndex="1">
                    <Settings HeaderFilterMode="CheckedList"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="decIdPruebaCalidad" FieldName="decIdPruebaCalidad" Visible="false" Name="decIdPruebaCalidad"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Prueba / Ensayo" FieldName="strDescripcion" VisibleIndex="2" Name="PruebaEnsayo">
                    <Settings HeaderFilterMode="CheckedList"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="Método ASTM y/o UOP" VisibleIndex="3" Name="MetodoASTM">
                    <Settings AllowAutoFilter="False" />
                    <DataItemTemplate>
                        <dx:ASPxComboBox ID="cmbMetodoASTM" runat="server" CssClass="Hydro_Content_Table">
                        </dx:ASPxComboBox>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Unidad" VisibleIndex="5" Name="Unidad">
                    <Settings AllowAutoFilter="False" />
                    <DataItemTemplate>
                        <dx:ASPxComboBox ID="cmbUnidad" runat="server" CssClass="Hydro_Content_Table" AutoPostBack="False"></dx:ASPxComboBox>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Valor reportado" VisibleIndex="6" Name="ValorReportado">
                    <Settings AllowAutoFilter="False" />
                    <DataItemTemplate>
                        <asp:TextBox ID="txtValorReportado" runat="server" Height="30" onKeyUp="upper(this);"></asp:TextBox>
                        <%--<dx:ASPxTextBox ID="txtValorReportado"  runat="server" CssClass="allUpper" Border-BorderWidth="1" OnTextChanged="TextBox1_Click">
                        <ClientSideEvents KeyUp="tb1_OnKeyUp" />
                        <ValidationSettings ValidationGroup="a">
                        </ValidationSettings>
                    </dx:ASPxTextBox>--%>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="ISO/NLGI" VisibleIndex="7" Name="IsoNLGI">
                    <Settings AllowAutoFilter="False" />
                    <DataItemTemplate>
                        <dx:ASPxComboBox ID="cmbIsoNlgi" OnSelectedIndexChanged="VobjIsoNlgi_SelectedIndexChanged" runat="server" CssClass="Hydro_Content_Table" AutoPostBack="True"></dx:ASPxComboBox>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Caption="Espec </br>Mínima" Name="EspecMinima" FieldName="strEspecMinima" VisibleIndex="8">
                    <Settings HeaderFilterMode="CheckedList"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Espec </br>Máxima" Name="EspecMaxima" FieldName="strEspecMaxima" VisibleIndex="9">
                    <Settings HeaderFilterMode="CheckedList"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="Justificacion" VisibleIndex="10" Visible="False" Name="Justificacion">
                    <Settings AllowAutoFilter="False" />
                    <DataItemTemplate>
                        <dx:ASPxTextBox ID="txtJustificacion" runat="server">
                        </dx:ASPxTextBox>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Caption="Especifiación</br>Alfanumerica" FieldName="strEspecAlfanumerico" VisibleIndex="11" Name="Especificacion">
                    <Settings HeaderFilterMode="CheckedList"></Settings>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsPager PageSize="30" Visible="False">
                <PageSizeItemSettings Visible="true" ShowAllItem="true" />

            </SettingsPager>
            <Settings ShowHeaderFilterButton="True" />
            <SettingsPopup>
                <HeaderFilter MinHeight="300" MinWidth="300"></HeaderFilter>

            </SettingsPopup>
            <Paddings Padding="5px" />
            <Styles>
                <Header CssClass="Hydro_Td_Titulo" BackColor="#2D7E3E" ForeColor="White">
                </Header>
                <Row CssClass="Hydro_Td_Contenido">
                </Row>
                <Table CssClass="Hydro_Content_Table">
                </Table>
            </Styles>
            <StylesEditors>
                <TextBox Font-Bold="True"></TextBox>

            </StylesEditors>
        </dx:ASPxGridView>
        <table>
            <tr>
                <td>
                    <asp:Label ID="LabelJustificacion" runat="server" Text="Justificación:" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxJustificacion" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>

        </table>

        <div class="Hydro_Alerta_Rojo" runat="server" visible="false" id="alert" style="background-color: #ffd7c9; border: thin solid #c30; border-radius: 8px; padding: 10px; text-align: center; width: 90%;">
            <span></span>
            <asp:Label ID="idmsjError" runat="server" Text=""></asp:Label>
            <p>
                <span></span>
                <asp:Label ID="idmsjErrorNumerico" runat="server" Text=""></asp:Label>
            </p>
            <p>
                <span></span>
                <asp:Label ID="idmsMensajeMaxMin" runat="server" Text=""></asp:Label>
            </p>
        </div>
        <table align="center">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="NumeroRegistro" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="AlmacenamientoAlerta" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="EspecificacionMinima" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LabelTipoAlerta" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LabelEntidad" runat="server" Visible="false"></asp:Label>
                    <asp:Button ID="btnGuardarRegistroCalidad" runat="server" OnClick="btnGuardarRegistroCalidad_Click" Text="REGISTRAR" ValidationGroup="a" Visible="false" BackColor="#2D7E3E" ForeColor="White" />
                    <asp:Button ID="btnEliminarRegistroCalidad" runat="server" OnClick="btnEliminarRegistroCalidad_Click" Text="ELIMINAR" Visible="false" OnClientClick="return confirm('ESTA ACCIÓN ELEMINIRA EL PRESENTE CERTIFICADO. ¿ESTÁ SEGURO DE ELIMINAR EL CERTIFICADO.?');" BackColor="#2D7E3E" ForeColor="White" />
                    <asp:Button ID="btnCancelarRegistroCalidad" runat="server" OnClick="Button1_Click" OnClientClick="return confirm('¿DESEA LIMPIAR TODA LA INFORMACIÓN DEL CERTIFICADO.?');" Text="LIMPIAR" BackColor="#2D7E3E" ForeColor="White"/>
                    <asp:Label ID="LabelIdPruebaCalidad" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="ValidaCalidad" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="EspecificacionMaxima" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="ValorNlgi" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="ValorApi" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="ClasificacionNLGI" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="RegistroApi" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        </center>
    </asp:Panel>
    
</asp:Panel>


<div align="center">
</div>

<asp:Panel ID="Panel2" runat="server">
    <dx:ASPxPopupControl ID="ppMensajeAlerta" ClientInstanceName="ppMensajeAlerta" runat="server" AllowDragging="true"
        HeaderText="MENSAJE DE CONFIRMACIÓN" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="500px" CloseAction="None" ShowCloseButton="false" Modal="True" Theme="Default"><ContentStyle BackColor="White"><Border BorderColor="#2D7E3E" BorderStyle="Solid" /></ContentStyle><HeaderStyle BackColor="#2D7E3E" Font-Bold="True" ForeColor="White"><Border BorderColor="#2D7E3E" BorderStyle="Solid" /></HeaderStyle><ContentCollection><dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server"><h4>DESEA CONTINUAR CON EL REGISTRO?</h4><hr /><table><tr><td><img src="../../../UI/img/VolumenesCalidad/alerta_naranja.png" style="height: 53px; width: 52px" /></td><td class="Hydro_Titulo_Verde_Menor"><p>Verifique que todos datos sean los corectos antes de Guardar</p></td></tr><tr><td colspan="2"><br /><dx:ASPxGridView ID="ASPxGridView1" runat="server" SettingsPager-PageSize="50"><SettingsPager PageSize="50"></SettingsPager></dx:ASPxGridView><br /></td></tr><tr align="right"><td></td><td><table><tr><td><asp:Button ID="btnConfirmar" runat="server" OnClick="btnContinuar_OnClick" Text="SÍ, GUARDAR" /></td><td><asp:Button ID="btnCancelarAlerta" OnClick="btnCancelarAlerta_OnClick" runat="server" Text="CANCELAR" /></td></tr></table></td></tr></table><hr /></dx:PopupControlContentControl></ContentCollection></dx:ASPxPopupControl>
</asp:Panel>
