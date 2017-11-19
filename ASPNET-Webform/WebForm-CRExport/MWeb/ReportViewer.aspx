<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="MWeb.ReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/aspnet_client/System_Web/4_0_30319/crystalreportviewers13/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/crystalreportviewers13/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/crystalreportviewers13/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/crystalreportviewers13/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/crystalreportviewers13/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/crystalreportviewers13/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/crystalreportviewers13/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/4_0_30319/crystalreportviewers13/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnBack" runat="server" Text="Back" Font-Bold="True" OnClick="btnBack_Click" Width="85px" />
        <div>
            <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="True" HasPrintButton="True" HasRefreshButton="True" ReuseParameterValuesOnRefresh="True" Height="50px" Width="350px" OnReportRefresh="CrystalReportViewer1_ReportRefresh" PrintMode="ActiveX" />
        </div>
        <div>
        </div>
    </form>
</body>
</html>
