<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="InquiryEntry.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.InquiryEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtInquiryDate.ClientID %>');
            //$('#trOtherSourceType').hide();
            //#region Company
            $('#lblCompany.lblLink').click(function () {
                openSearchDialog('company', 'IsDeleted = 0', function (value) {
                    $('#<%=txtCompanyCode.ClientID %>').val(value);
                    onTxtCompanyCodeChanged(value);
                });
            });

            $('#<%=txtCompanyCode.ClientID %>').change(function () {
                onTxtCompanyCodeChanged($(this).val());
            });

            function onTxtCompanyCodeChanged(value) {
                var filterExpression = "CompanyCode = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetCompanyList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnCompanyID.ClientID %>').val(result.CompanyID);
                        $('#<%=txtCompanyName.ClientID %>').val(result.CompanyName);
                    }
                    else {
                        $('#<%=hdnCompanyID.ClientID %>').val('');
                        $('#<%=txtCompanyCode.ClientID %>').val('');
                        $('#<%=txtCompanyName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Employee
            $('#lblEmployee.lblLink').click(function () {
                openSearchDialog('employee', 'IsDeleted = 0', function (value) {
                    $('#<%=txtEmployeeCode.ClientID %>').val(value);
                    onTxtEmployeeCodeChanged(value);
                });
            });

            $('#<%=txtEmployeeCode.ClientID %>').change(function () {
                onTxtEmployeeCodeChanged($(this).val());
            });

            function onTxtEmployeeCodeChanged(value) {
                var filterExpression = "EmployeeCode = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetvEmployeeList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnEmployeeID.ClientID %>').val(result.EmployeeID);
                        $('#<%=txtEmployeeName.ClientID %>').val(result.EmployeeName);
                    }
                    else {
                        $('#<%=hdnEmployeeID.ClientID %>').val('');
                        $('#<%=txtEmployeeCode.ClientID %>').val('');
                        $('#<%=txtEmployeeName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Member
            $('#lblMember.lblLink').click(function () {
                openSearchDialog('member', 'IsDeleted = 0', function (value) {
                    $('#<%=hdnMemberID.ClientID %>').val(value);
                    onTxtMemberCodeChanged(value);
                });
            });

            $('#<%=txtMemberCode.ClientID %>').change(function () {
                var filterExpression = "MemberCode = '" + $(this).val() + "' AND IsDeleted = 0";
                Methods.getObject('GetvMemberList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnMemberID.ClientID %>').val(result.MemberID);
                        $('#<%=txtMemberName.ClientID %>').val(result.LastName);
                    }
                    else {
                        $('#<%=hdnMemberID.ClientID %>').val('');
                        $('#<%=txtMemberCode.ClientID %>').val('');
                        $('#<%=txtMemberName.ClientID %>').val('');
                    }
                });
            });

            function onTxtMemberCodeChanged(value) {
                var filterExpression = "MemberID = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetMemberList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=txtMemberCode.ClientID %>').val(result.MemberCode);
                        $('#<%=txtMemberName.ClientID %>').val(result.LastName);
                    }
                    else {
                        $('#<%=hdnMemberID.ClientID %>').val('');
                        $('#<%=txtMemberCode.ClientID %>').val('');
                        $('#<%=txtMemberName.ClientID %>').val('');
                    }
                });
            }
            //#endregion
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <div class="pageTitle"><%=GetLabel("Inquiry")%></div>
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
            <col />
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent">
                    <colgroup>
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Inquiry No.")%></label></td>
                        <td><asp:TextBox ID="txtInquiryNo" Width="200px" ReadOnly="true" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Inquiry Date")%></label></td>
                        <td><asp:TextBox ID="txtInquiryDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory lblLink" id="lblCompany"><%=GetLabel("Company")%></label></td>
                        <td>
                            <input type="hidden" id="hdnCompanyID" runat="server" value="" />
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:97px"/>
                                    <col style="width:3px"/>
                                    <col/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtCompanyCode" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtCompanyName" Width="200px" runat="server" ReadOnly="true" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory lblLink" id="lblEmployee"><%=GetLabel("PIC CRO")%></label></td>
                        <td>
                            <input type="hidden" id="hdnEmployeeID" runat="server" value="" />
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:97px"/>
                                    <col style="width:3px"/>
                                    <col/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtEmployeeCode" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtEmployeeName" Width="200px" runat="server" ReadOnly="true" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory lblLink" id="lblMember"><%=GetLabel("Member")%></label></td>
                        <td>
                            <input type="hidden" id="hdnMemberID" runat="server" value="" />
                            <table cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col style="width:97px"/>
                                    <col style="width:3px"/>
                                    <col/>
                                </colgroup>
                                <tr>
                                    <td><asp:TextBox ID="txtMemberCode" Width="100%" runat="server"/></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtMemberName" Width="200px" runat="server" ReadOnly="true" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Subject")%></label></td>
                        <td><asp:TextBox ID="txtSubject" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" valign="top"><label class="lblNormal"><%=GetLabel("Remarks")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" TextMode="MultiLine" Width="300px" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
