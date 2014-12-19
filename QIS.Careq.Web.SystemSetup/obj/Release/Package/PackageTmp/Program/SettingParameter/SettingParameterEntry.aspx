<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="SettingParameterEntry.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.SettingParameterEntry" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <input type="hidden" id="hdnID" runat="server" value="" />
    <div class="pageTitle"><%=GetLabel("Setting Variables")%></div>
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Parameter Code")%></label></td>
                        <td><asp:TextBox ID="txtParameterCode" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Parameter Name")%></label></td>
                        <td><asp:TextBox ID="txtParameterName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr class="tdLabel" style="display:none" id="trCboParameterValue" runat="server">
                        <td class="tdLabel"><label class="lblMandatory" id="lblParameterValue"><%=GetLabel("Parameter Value")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboParameterValue" runat="server" Width="300px" /> </td>
                    </tr>
                    <tr id="trTxtParameterValue" runat="server">
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Parameter Value")%></label></td>
                        <td><asp:TextBox ID="txtParameterValue" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" valign="top" style="padding-top:5px"><label><%=GetLabel("Notes")%></label></td>
                        <td><asp:TextBox ID="txtNotes" Width="300px" runat="server" TextMode="MultiLine" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
