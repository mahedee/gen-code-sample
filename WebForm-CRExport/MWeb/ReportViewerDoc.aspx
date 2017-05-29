<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerDoc.aspx.cs" Inherits="MWeb.ReportViewerDoc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
            <table>
                <tr>
                    <td style="height: 6px">
                        <asp:Button ID="btnBack" runat="server" Text="Back" Font-Bold="True" OnClick="btnBack_Click" Width="85px" />

                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td style="height: 26px">
                        <iframe id="frame_view_report" runat="server" style="height: 700px; width: 950px; overflow: scroll;" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
