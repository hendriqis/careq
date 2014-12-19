<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="EventRegistrationDtEntry.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.EventRegistrationDtEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnEventRegistrationDtEntryMemberIdentity" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbnew.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Member Identity")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            $('#<%=btnEventRegistrationDtEntryMemberIdentity.ClientID %>').click(function () {
                var id = $('#<%=hdnMemberID.ClientID %>').val();
                var url = ResolveUrl("~/Program/EventRegistration/MemberIdentityCtl.ascx");
                openUserControlPopup(url, id, 'Member Identity', 1300, 600);
            });

            //#region Member
            $('#<%=lblMember.ClientID %>.lblLink').click(function () {
                var filterExpression = 'MemberID NOT IN (SELECT MemberID FROM EventRegistration WHERE EventID = ' + $('#<%=hdnEventID.ClientID %>').val() + " AND GCRegistrationStatus != 'X010^007') AND IsDeleted = 0";
                openSearchDialog('member', filterExpression, function (value) {
                    onTxtMemberCodeChanged(value);
                });
            });

            function onTxtMemberCodeChanged(value) {
                var filterExpression = "MemberID = " + value + " AND IsDeleted = 0";
                Methods.getObject('GetvMemberList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnMemberID.ClientID %>').val(result.MemberID);
                        $('#<%=txtMemberName.ClientID %>').val(result.MemberName);
                        $('#<%=hdnCompanyID.ClientID %>').val(result.CompanyID);
                        $('#<%=hdnOccupation.ClientID %>').val(result.OccupationID);
                        $('#<%=hdnGCOccupationLevel.ClientID %>').val(result.GCOccupationLevel);
                    }
                    else {
                        $('#<%=hdnMemberID.ClientID %>').val('');
                        $('#<%=txtMemberName.ClientID %>').val('');
                        $('#<%=hdnCompanyID.ClientID %>').val('');
                        $('#<%=hdnOccupation.ClientID %>').val('');
                        $('#<%=hdnGCOccupationLevel.ClientID %>').val('');
                    }
                });
            }
            //#endregion
        }

        function onAfterSaveRightPanelContent(code, value, isAdd) {
            $('#<%=hdnMemberID.ClientID %>').val(value);
            onTxtMemberCodeChanged(value);
        }
    </script>
    <input type="hidden" id="hdnEventID" runat="server" value="" />
    <div class="pageTitle"><%=GetLabel("Event Registration")%></div>
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
                        <td class="tdLabel"><label class="lblLink lblMandatory" runat="server" id="lblMember"><%=GetLabel("Member")%></label></td>
                        <td>
                            <input type="hidden" value="" id="hdnMemberID" runat="server" />
                            <input type="hidden" value="" id="hdnCompanyID" runat="server" />
                            <input type="hidden" value="" id="hdnOccupation" runat="server" />
                            <input type="hidden" value="" id="hdnGCOccupationLevel" runat="server" />
                            <asp:TextBox ID="txtMemberName" Width="100%" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Registration Type")%></label></td>
                        <td><dx:ASPxComboBox ID="cboGCRegistrationType" Width="300px" runat="server"  /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Information Source")%></label></td>
                        <td><dx:ASPxComboBox ID="cboGCInformationSource" Width="300px" runat="server"  /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
