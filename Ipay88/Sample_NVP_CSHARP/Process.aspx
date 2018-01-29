<%@ Page Language="C#" AutoEventWireup="True" CodeFile="Process.aspx.cs" Inherits="_NVP.Process" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>API Example Code - Results</title>
    <link rel="stylesheet" type="text/css" href="assets/paymentstyle.css" />
</head>

<body>
    <h1>DirectApi C# ASP.NET NVP Example</h1>
    <h3>Receipt Page</h3>
    <p><a href="index.html">Return to the Home Page</a></p>

    <form id="ReciptForm" runat="server">
        <div align="center">
            <asp:Panel ID="pnlError" runat="server" Visible="false">
                <table width="60%" align="center" cellpadding="5" border="0">
                    <tbody>
                        <tr class="title">
                            <td colspan="2"><p><strong>Error Response</strong></p></td>
                        </tr>
                        <tr>
                            <td align="right" width="50%"><strong><i><asp:Label id="lblErrorCode" runat="server"/>: </i></strong></td>
                            <td width="50%"><asp:Label id="lblErrorMessage" runat="server"/></td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </div>

        <div align="center">
            <asp:Panel ID="pnlResults" runat="server">
                <table width="60%" align="center" cellpadding="5" border="0">
                    <tbody>
                        <tr class="title">
                            <td colspan="2"><p><strong><asp:Label id="lblGateWayCode" runat="server"/></strong></p></td>
                        </tr>
                        <tr>
                            <td align="right" width="50%"><strong><i>Result: </i></strong></td>
                            <td width="50%"><asp:Label id="lblResult" runat="server"/></td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </div>

        <div align="center">
            <table width="60%" align="center" cellpadding="5" border="0">
                <tbody>
                    <tr class="title">
                        <td colspan="2"><p><strong>All Response Fields</strong></p></td>
                    </tr>
        
                    <tr class="title">
                        <td colspan="2">
                            <asp:Panel ID="pnlFieldResults" runat="server">
                            </asp:Panel>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        
        <div align="center">
            <table width="60%" align="center" cellpadding="5" border="0">
                <tbody>
                    <tr class="title">
                        <td colspan="2"><p><strong>Raw Response</strong></p></td>
                    </tr>
                    <tr>
                        <td  colspan="2" align="center" width="100%">
                            <asp:TextBox ID="txtRawResponse" runat="server" Width="95%" Height="209px" 
                                ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
