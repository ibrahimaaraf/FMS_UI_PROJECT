﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RPTViewer.aspx.cs" Inherits="FMS_UI_PROJECT.Report.RPTViewer" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
        <!-- Add your ReportViewer control here -->
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="600px" />

    </form>
</body>
</html>
