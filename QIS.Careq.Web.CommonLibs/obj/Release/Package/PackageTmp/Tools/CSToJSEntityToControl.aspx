﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CSToJSEntityToControl.aspx.cs" Inherits="QIS.Careq.Web.CommonLibs.Tools.CSToJSEntityToControl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtCS" TextMode="MultiLine" Rows="10" Width="800px" runat="server" />
        <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" />
        <br />
        
        <asp:TextBox ID="txtJS" TextMode="MultiLine" Rows="10" Width="800px" runat="server" />
    </div>
    </form>
</body>
</html>
