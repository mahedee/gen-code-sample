<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VS13CR11._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <h3>Crystal Report XI R2 with ASP.NET Web Form</h3>
        <table>
            <tr>
                <td>Designation: </td>
                <td>
                    <asp:DropDownList ID="ddlDesignation" runat="server" Width="200px" Height="20px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td></td>
                <td>
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
