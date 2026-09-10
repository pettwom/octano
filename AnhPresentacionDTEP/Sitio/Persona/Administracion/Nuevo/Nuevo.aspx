<%@ Page Title=""  MasterPageFile="~/Sitio/VolumenesCalidad/CantidadesMaster.Master" Language="C#" AutoEventWireup="true" CodeBehind="Nuevo.aspx.cs" Inherits="AnhPresentacionDTEP.Sitio.Persona.Administracion.Nuevo.Nuevo" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    
    <h1>Nueva ventana</h1>
    <asp:Button ID="newTabButton2" runat="server" Text="Nuevo" OnClick="newTabButton2_Click"> 

    </asp:Button>
    <asp:DataGrid ID="listadoNombresRoles" runat="server" >
    </asp:DataGrid>
    <h1>Listado 2</h1>
    <asp:GridView ID="Listado2" AutoGenerateColumns="false" runat="server">
        <Columns>
            <asp:BoundField DataField="Nro"/>
            <asp:BoundField DataField="Nombre"/>
            <asp:BoundField DataField="Id Usuario"/>
            <asp:BoundField DataField="Rol"/>  
            <asp:BoundField DataField="Edicion"/>  
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="editButton" Text="Edit" runat="server" ></asp:Button>         
                    <asp:Button ID="deleteButton" Text="Delete" runat="server"></asp:Button>
                    <asp:Button ID="updateButton" Text="Update" runat="server"></asp:Button>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <h1>LISTADO 3</h1>
    <dx:ASPxGridView ID="listado3" runat="server">
        <Columns>  
            
                              
            <dx:GridViewDataTextColumn Caption="Nombre" Width="250px">
                
            </dx:GridViewDataTextColumn>
             <dx:GridViewDataTextColumn Caption="Id Usuario" Width="150px" >
                 
            </dx:GridViewDataTextColumn>
             <dx:GridViewDataTextColumn Caption="Rol" Width="100px">
                 
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewCommandColumn VisibleIndex="3" Width="150px" ButtonType="Image" ShowDeleteButton="true" ShowEditButton="true" ShowSelectButton="true" Caption="Valores">
               
                
            </dx:GridViewCommandColumn>        
            
        </Columns>
        <SettingsCommandButton>
            <EditButton>
                <Image Url="../../../../UI/img/iconos/editar.png">

                </Image>
            </EditButton>
                
            <DeleteButton>
                <Image  Url="../../../../UI/img/iconos/eliminar.png">

                </Image>
            </DeleteButton>
            <SelectButton>
                <Image Url="../../../../UI/img/iconos/verDoc.png">

                </Image>
            </SelectButton>
            
        </SettingsCommandButton>
        <SettingsEditing>

        </SettingsEditing>
        

    </dx:ASPxGridView>
    
</asp:Content>



