<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VisualWebPart1UserControl.ascx.cs" Inherits="DolbyShuttle.VisualWebPart1.VisualWebPart1UserControl" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<style type="text/css">
    .style1
    {
        width: 188px;
    }
    .style6
    {
        width: 80px;
        color: #FFFFFF;
    }
    .style8
    {
        font-family: Arial, Helvetica, sans-serif;
        font-size: small;
        color: #0072BC;
    }

</style>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div id="Layer1" style="position:static; z-index:1000" >
                    <img alt = "Please wait..." src ="/_layouts/images/DolbyShuttle/spinner.gif" style="position: absolute; z-index: 10000" align="middle" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <table bgcolor="#E9E9E9" style="height:25px;width:25px">
            <tr>

                <td class="style6">
                    <span class="style8"><strong dir="ltr">Depart </strong>
                    <asp:ImageButton ID="ImageButton1" runat="server" Height="16px" 
                        ImageUrl="~/_layouts/images/DolbyShuttle/exchange.gif" Width="16px" 
                        onclick="ImageButton1_Click"/>
                    </span><br />
                    <asp:DropDownList ID="ddlStart" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="startListBox_SelectedIndexChanged" Width="85px" 
                        Font-Size="Smaller">
                        <asp:ListItem>999 Brannan</asp:ListItem>
                        <asp:ListItem>475 Brannan</asp:ListItem>
                        <asp:ListItem>100 Potrero</asp:ListItem>
                        <asp:ListItem>BART</asp:ListItem>
                        <asp:ListItem>CalTrain</asp:ListItem>
                        <asp:ListItem>One Market</asp:ListItem>
                        <asp:ListItem>Transbay</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <span class="style8"><strong>Arrive</strong></span><br />
                    <asp:DropDownList ID="ddlEnd" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="endtListBox_SelectedIndexChanged" Width="85px" 
                        Font-Size="Smaller">
                        <asp:ListItem>999 Brannan</asp:ListItem>
                        <asp:ListItem>475 Brannan</asp:ListItem>
                        <asp:ListItem>100 Potrero</asp:ListItem>
                        <asp:ListItem>BART</asp:ListItem>
                        <asp:ListItem>CalTrain</asp:ListItem>
                        <asp:ListItem>One Market</asp:ListItem>
                        <asp:ListItem>Transbay</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    <asp:TextBox ID="StartTB1" runat="server" BackColor="White" ReadOnly="True" 
                        Font-Bold="True" Font-Size="Smaller" Width="89px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="EndTB1" runat="server" BackColor="White" ReadOnly="True" 
                        Font-Bold="True" Font-Size="Smaller" Width="89px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    <asp:TextBox ID="StartTB2" runat="server" BackColor="White" ReadOnly="True" 
                        Font-Bold="True" Font-Size="Smaller" Width="89px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="EndTB2" runat="server" BackColor="White" ReadOnly="True" 
                        Font-Bold="True" Font-Size="Smaller" Width="89px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    <asp:TextBox ID="StartTB3" runat="server" BackColor="White" ReadOnly="True" 
                        Font-Bold="True" Font-Size="Smaller" Width="89px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="EndTB3" runat="server" BackColor="White" ReadOnly="True" 
                        Font-Bold="True" Font-Size="Smaller" Width="89px"></asp:TextBox>
                </td>
            </tr>
        </table>
        
    </ContentTemplate>
</asp:UpdatePanel>



