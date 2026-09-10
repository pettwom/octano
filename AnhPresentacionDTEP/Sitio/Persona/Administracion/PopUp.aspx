<%@ Page Title="" Language="C#"  MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" AutoEventWireup="true" CodeBehind="PopUp.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.Persona.Administracion.PopUp" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="PopUp" ContentPlaceHolderID="MainContent" runat="server">

    <dx:ASPxPopupControl ID="pcLogin" runat="server" Width="720" Height="420" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcLogin"
        HeaderText="Login" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" AutoUpdatePosition="true">
        <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

                <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                           <dx:ASPxSplitter ID="ASPxSplitter1" runat="server"  Height="100%" FullscreenMode="true" >
                                <Panes>

                                    <dx:SplitterPane MaxSize="200px">
                                        <ContentCollection>   
                                           <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                               <dx:ASPxTreeView  runat="server" ID="arbolMenu" Theme="Office2003Olive" AllowSelectNode="True" Font-Size="XX-Small" AutoPostBack="True"  ForeColor="Black" ShowLines="true">
                                                   <Nodes>
                                                       <dx:TreeViewNode Text="Padre" NavigateUrl="~/Employee.aspx" Target="_blank" Expanded="true">
                                                          <Nodes>
                                                               <dx:TreeViewNode Text="Hijo_1" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                                <dx:TreeViewNode Text="Hijo_2" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                                <dx:TreeViewNode Text="Hijo_3" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                          </Nodes>
                                                        </dx:TreeViewNode>
                                                       <dx:TreeViewNode Text="Padre_2" NavigateUrl="~/Employee.aspx" Target="_blank" Expanded="true">
                                                          <Nodes>
                                                                <dx:TreeViewNode Text="Hijo_1" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                                <dx:TreeViewNode Text="Hijo_2" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                                <dx:TreeViewNode Text="Hijo_3" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                          </Nodes>
                                                        </dx:TreeViewNode>
                                                       <dx:TreeViewNode Text="Padre_3" NavigateUrl="~/Employee.aspx" Target="_blank" Expanded="true">
                                                          <Nodes>
                                                                <dx:TreeViewNode Text="Hijo_1" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                                <dx:TreeViewNode Text="Hijo_2" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                                <dx:TreeViewNode Text="Hijo_3" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                          </Nodes>
                                                        </dx:TreeViewNode>
                                                       <dx:TreeViewNode Text="Padre_4" NavigateUrl="~/Employee.aspx" Target="_blank" Expanded="true">
                                                          <Nodes>
                                                                <dx:TreeViewNode Text="Hijo_1" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                                <dx:TreeViewNode Text="Hijo_2" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                                <dx:TreeViewNode Text="Hijo_3" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                          </Nodes>
                                                        </dx:TreeViewNode>
                                                       <dx:TreeViewNode Text="Padre_2" NavigateUrl="~/Employee.aspx" Target="_blank">
                                                          <Nodes>
                                                                <dx:TreeViewNode Text="Hijo_1" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                                <dx:TreeViewNode Text="Hijo_2" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                                <dx:TreeViewNode Text="Hijo_3" NavigateUrl="~/Employee.aspx" Target="_blank"/>
                                                          </Nodes>
                                                        </dx:TreeViewNode>
                                                   </Nodes>
                                               </dx:ASPxTreeView>  
                                            </dx:SplitterContentControl>
                                        </ContentCollection>
                                    </dx:SplitterPane>

                                    <dx:SplitterPane>
                                        <ContentCollection>
                                            <dx:SplitterContentControl  Height="100%">
                                            </dx:SplitterContentControl>
                                        </ContentCollection>
                                    </dx:SplitterPane>

                                </Panes>
                            </dx:ASPxSplitter>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>

                <div>
                    <a href="javascript:ShowCreateAccountWindow();" id="hl1" style="float: right; margin: 14px 0 10px 0;">Create Account</a>
                </div>

            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>