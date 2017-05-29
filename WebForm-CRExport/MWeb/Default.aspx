<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MWeb.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h3>Crystal Report XI R2 and ASP.NET Web Form with export facilities</h3>
        <table style="margin-left:30px;">
            <tr>
                <td>Designation: </td>
                <td>
                    <asp:DropDownList ID="ddlDesignation" runat="server" Width="200px" Height="20px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Export</td>
                <td>
                    <asp:DropDownList ID="ddlExpReport" AppendDataBoundItems="true" runat="server" Width="100px">
                        <asp:ListItem Text="PDF" Value="1" />
                        <asp:ListItem Text="Crystal Preview" Value="2" />
                        <asp:ListItem Text="Excel" Value="3" />
                        <asp:ListItem Text="Doc" Value="4" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <br />
                    <asp:Button ID="btnViewReport" runat="server" Text="View Report" OnClick="btnViewReport_Click" /></td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
