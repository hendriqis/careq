<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventConfirmationSendEmailCtl.ascx.cs" 
    Inherits="QIS.Careq.Web.SystemSetup.Program.EventConfirmationSendEmailCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>

<script type="text/javascript" id="dxss_eventconfirmationsendemailctl">
    $(function () {
        $('#btnSendEmail').click(function () {
            if (confirm("Are You Sure Want To Send Mail?"))
                cbpSendEmailProcess.PerformCallback('');
        });
    });

    function onCbpSendEmailProcessEndCallback(s) {
        var result = s.cpResult.split('|');
        if (result[0] == 'fail')
            alert('Send Email Failed\nError Message : ' + result[1]);
        else {
            alert('Email Sent!');
            onRefreshControl();
            pcRightPanelContent.Hide();
        }
        hideLoadingPanel();
    }
</script>
<input type="hidden" runat="server" id="hdnEventID" />
<input type="hidden" runat="server" id="hdnSelectedMember" />
<table class="tblEntryContent" style="width:100%">
    <colgroup>
        <col style="width:100px"/>
    </colgroup>
    <tr>
        <td class="tdLabel" valign="top" style="margin-top:5px"><label><%=GetLabel("To")%></label></td>
        <td><asp:TextBox ID="txtTo" Width="500px" ReadOnly="true" runat="server" TextMode="MultiLine" /></td>
    </tr>
    <tr>
        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Subject")%></label></td>
        <td><asp:TextBox ID="txtSubject" ReadOnly="true" Width="500px" runat="server" /></td>
    </tr>
    <tr>
        <td class="tdLabel" valign="top" style="margin-top:5px"><label><%=GetLabel("Content")%></label></td>
        <td>
            <div style="width:500px; height:300px; overflow-y:scroll; border:1px solid #AAA;" id="divContent" runat="server">
            </div>
        </td>
    </tr>
</table>
<div style="width:100%;text-align:center">
    <table style="margin-left: auto; margin-right: auto; margin-top: 10px;">
        <tr>
            <td><input type="button" value='<%= GetLabel("Send")%>' style="width:70px" id="btnSendEmail" /></td>
            <td><input type="button" value='<%= GetLabel("Close")%>' style="width:70px" onclick="pcRightPanelContent.Hide();" /></td>
        </tr>
    </table>
</div>

<dxcp:ASPxCallbackPanel ID="cbpSendEmailProcess" runat="server" Width="100%" ClientInstanceName="cbpSendEmailProcess"
    ShowLoadingPanel="false" OnCallback="cbpSendEmailProcess_Callback">
    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpSendEmailProcessEndCallback(s); }" />
</dxcp:ASPxCallbackPanel>