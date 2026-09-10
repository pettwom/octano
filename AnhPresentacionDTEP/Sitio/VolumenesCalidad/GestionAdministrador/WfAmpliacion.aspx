<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="WfAmpliacion.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.VolumenesCalidad.GestionAdministrador.WfAmpliacion" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxTreeList" Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="100%">
        <Panes>
            <dx:SplitterPane Size="20%" AllowResize="True" AutoHeight="True">
                <ContentCollection>
                    <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxTreeList ID="treeOperadores" ClientInstanceName="treeOperadores" runat="server" AutoGenerateColumns="False" KeyFieldName="HIJO" ParentFieldName="PADRE" Font-Size="Smaller" Theme="Default" Styles="align: left" >
                                        <Columns>
                                            <dx:TreeListTextColumn FieldName="DESCRIPCION" ShowInCustomizationForm="True" VisibleIndex="0" Caption="Entidad">
                                                <CellStyle VerticalAlign="Middle" HorizontalAlign="Left">
                                                    <Paddings PaddingLeft="0px" />
                                                </CellStyle>
                                            </dx:TreeListTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedNode="True" FocusNodeOnExpandButtonClick="False" />
                                        <ClientSideEvents FocusedNodeChanged="function(s, e) { 
                                            var key = treeOperadores.GetFocusedNodeKey();
                                            pnlDatos.PerformCallback(key); 
        }" />
                                    </dx:ASPxTreeList>
                                    

                                </td>
                            </tr>
                        </table>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane ScrollBars="Auto">
                <ContentCollection>
                    <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                        <dx:ASPxCallbackPanel ID="pnlDatos" runat="server" Theme="Youthful" Width="100%" ClientInstanceName="pnlDatos" OnCallback="pnlDatos_OnCallback">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent2" runat="server">
                                    <h3>FORMULARIO DE INSERCION DE AMPLICACIONES</h3>
                                    <p class="descripcion">Gestión de Ampliaciones</p>
                                    <hr />
                                    <fieldset>
                                        <legend style="border: none; float: none; text-align: left">INSERCIÓN AMPLIACIONES</legend>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lblMesAmplia" runat="server" Text="MES/AÑO AMPLIACION:"></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxDateEdit ID="dtAmpliacion" runat="server" Width="90%" Theme="Default">
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties>
                                                                <ClearButton Visibility="Auto"></ClearButton>
                                                            </TimeEditProperties>
                                                        </TimeSectionProperties>

                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                    </dx:ASPxDateEdit>
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="lblApertura" runat="server" Text="APERTURA DESDE:"></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxDateEdit ID="dtApertura" runat="server" Width="90%" Theme="Default">
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties>
                                                                <ClearButton Visibility="Auto"></ClearButton>
                                                            </TimeEditProperties>
                                                        </TimeSectionProperties>

                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                    </dx:ASPxDateEdit>
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="lblCierre" runat="server" Text="APERTURA HASTA:"></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxDateEdit ID="dtCierre" runat="server" Width="90%" Theme="Default">
                                                        <TimeSectionProperties>
                                                            <TimeEditProperties>
                                                                <ClearButton Visibility="Auto"></ClearButton>
                                                            </TimeEditProperties>
                                                        </TimeSectionProperties>

                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                    </dx:ASPxDateEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lblBarcode" runat="server" Text="BARCODE:"></dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox ID="txtBarcode" runat="server" Width="90%" Theme="Office2003Olive"></dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="lblObservaciones" runat="server" Text="OBSERVACIONES:"></dx:ASPxLabel>
                                                </td>
                                                <td colspan="3">
                                                    <dx:ASPxTextBox ID="txtObservaciones" runat="server" Width="95%" Theme="Office2003Olive"></dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <%--<asp:Button ID="btnRegistrar" runat="server" Text="REGISTRAR" OnClick="btnRegistrar_Click" ClientIDMode="Static"/>--%>
                                                    <dx:ASPxButton ID="btnRegistrar" runat="server" Text="REGISTRAR" OnClick="btnRegistrar_Click" ></dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>

                                        <p class="mensaje" id="mensaje_usuario" runat="server"></p>
                                    </fieldset>
                                    <fieldset>
                                        <legend style="border: none; float: none; text-align: left">HISTORICO DE AMPLIACIONES</legend>
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxGridView ID="grvAmpliaciones" runat="server" AutoGenerateColumns="False" KeyFieldName="ID_AMPLIACION" Theme="Default">
                                                        <Settings ShowFilterRow="True" />
                                                        <SettingsDataSecurity AllowEdit="False" AllowInsert="False" />
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ButtonType="Default" ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="8" CellStyle-VerticalAlign="Middle">
<CellStyle VerticalAlign="Middle"></CellStyle>
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn Caption="ID_AMPLIACION" FieldName="ID_AMPLIACION" Name="ID_AMPLIACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="ENTIDAD" FieldName="ENTIDAD" Name="ENTIDAD" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="TIPO_ACTIVIDAD" Name="TIPO_ACTIVIDAD" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="BARCODE" FieldName="BARCODE" Name="BARCODE" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="OBSERVACIONES" FieldName="OBSERVACION" Name="OBSERVACION" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataDateColumn Caption="MES/AÑO AMPLIACIÓN" FieldName="FECHA_OPERACION_FORMAT" Name="FECHA_OPERACION_FORMAT" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                <PropertiesDateEdit  DisplayFormatString="MM/yyyy">
                                                                    <TimeSectionProperties>
                                                                        <TimeEditProperties>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                        </TimeEditProperties>
                                                                    </TimeSectionProperties>
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesDateEdit>
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataDateColumn Caption="APERTURA DESDE" FieldName="FECHA_INICIO_FORMAT" Name="FECHA_INICIO_FORMAT" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                                    <TimeSectionProperties>
                                                                        <TimeEditProperties>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                        </TimeEditProperties>
                                                                    </TimeSectionProperties>
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesDateEdit>
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataDateColumn Caption="APERTURA HASTA" FieldName="FECHA_FIN_FORMAT" Name="FECHA_FIN_FORMAT" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                                    <TimeSectionProperties>
                                                                        <TimeEditProperties>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                        </TimeEditProperties>
                                                                    </TimeSectionProperties>
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesDateEdit>
                                                            </dx:GridViewDataDateColumn>
                                                        </Columns>
                                                        <Styles>
                                                            <Header BackColor="#598024" ForeColor="White">
                                                            </Header>
                                                            <HeaderPanel BackColor="#598024" ForeColor="White">
                                                            </HeaderPanel>
                                                        </Styles>
                                                    </dx:ASPxGridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
    </dx:ASPxSplitter>
</asp:Content>
