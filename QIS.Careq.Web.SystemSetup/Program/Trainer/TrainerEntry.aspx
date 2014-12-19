<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="TrainerEntry.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.TrainerEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <input type="hidden" id="hdnID" runat="server" value="" />
    <div class="pageTitle"><%=GetLabel("Trainer")%></div>
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <h4><%=GetLabel("Trainer Data")%></h4>
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:35%"/>
                        <col style="width:65%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Trainer Code")%></label></td>
                        <td><asp:TextBox ID="txtTrainerCode" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Salutation")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboSalutation" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Title")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboTitle" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Trainer Name")%></label></td>
                        <td>
                            <table style="width:100%" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:49%"/>
                                    <col style="width:3px"/>
                                    <col/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtFirstName" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtMiddleName" Width="100%" runat="server" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Last Name")%></label></td>
                        <td><asp:TextBox ID="txtLastName" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Suffix")%></label></td>
                        <td><dxe:ASPxComboBox ID="cboSuffix" Width="100%" runat="server" /></td>
                    </tr>
                </table>
            </td>
            <td style="padding:5px;vertical-align:top">
                <h4><%=GetLabel("Contact Information")%></h4>
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:35%"/>
                        <col style="width:65%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Email")%></label></td>
                        <td><asp:TextBox ID="txtEmail" CssClass="email" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Mobile Phone 1")%></label></td>
                        <td><asp:TextBox ID="txtMobilePhone1" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Mobile Phone 2")%></label></td>
                        <td><asp:TextBox ID="txtMobilePhone2" Width="100%" runat="server" /></td>
                    </tr>
                </table>
                <h4><%=GetLabel("OtherInformation")%></h4>
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:35%"/>
                        <col style="width:65%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Remarks")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
