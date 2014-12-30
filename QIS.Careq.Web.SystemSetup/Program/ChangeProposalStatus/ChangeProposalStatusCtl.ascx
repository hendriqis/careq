<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangeProposalStatusCtl.ascx.cs" 
    Inherits="QIS.Careq.Web.SystemSetup.Program.ChangeProposalStatusCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">

    function onCboLeadProcessTypeTypeChanged() {
        if (cboLeadProcessType.GetValue() == "<%=GetLeadProcessTypeOther() %>") {
            $('#<%=txtOtherLeadProcessType.ClientID %>').show();
        } else {
            $('#<%=txtOtherLeadProcessType.ClientID %>').hide();
        }
    }
</script>

<div>
    <input type="hidden" id="hdnLeadID" value="" runat="server" />
    <div class="pageTitle"><%=GetLabel("Proposal Process")%></div>
    <table class="tblContentArea">
        <colgroup>
            <col style="width:100%"/>
        </colgroup>
        <tr>            
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:160px"/>
                        <col style="width:150px"/>
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Process Type")%></label></td>
                        <td>
                            <dxe:ASPxComboBox ID="cboLeadProcessType" ClientInstanceName="cboLeadProcessType" Width="100%" runat="server">
                                <ClientSideEvents ValueChanged="function() { onCboLeadProcessTypeTypeChanged() }" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td><asp:TextBox ID="txtOtherLeadProcessType" Style="display:none" Width="100%" runat="server" /></td>
                    </tr> 
                </table>
            </td>
        </tr>
    </table>
    <div style="width:100%;text-align:right">
        <input type="button" value='<%= GetLabel("Close")%>' onclick="pcRightPanelContent.Hide();" />
    </div>
</div>

