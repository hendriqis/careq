<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="MemberEntry.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.MemberEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <link href="<%= ResolveUrl("~/Libs/Scripts/jquery/rateit/rateit.css")%>" rel="stylesheet" type="text/css">
    <script src='<%= ResolveUrl("~/Libs/Scripts/jquery/rateit/jquery.rateit.js")%>' type='text/javascript'></script>
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtDOB.ClientID %>');

            $('#divRatingLevel').rateit({ max: 5, step: 1, backingfld: '#<%=hdnRatingLevel.ClientID %>' });

            //#region Popup Search
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

            //#region Department
            $('#lblDepartment.lblLink').click(function () {
                openSearchDialog('companydepartment', '', function (value) {
                    $('#<%=txtDepartmentCode.ClientID %>').val(value);
                    onTxtDepartmentCodeChanged(value);
                });
            });

            $('#<%=txtDepartmentCode.ClientID %>').change(function () {
                onTxtDepartmentCodeChanged($(this).val());
            });

            function onTxtDepartmentCodeChanged(value) {
                var filterExpression = "StandardCodeID = 'X003^" + value + "'";
                Methods.getObject('GetStandardCodeList', filterExpression, function (result) {
                    if (result != null)
                        $('#<%=txtDepartmentName.ClientID %>').val(result.StandardCodeName);
                    else {
                        $('#<%=txtDepartmentCode.ClientID %>').val('');
                        $('#<%=txtDepartmentName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Province
            $('#lblProvince.lblLink').click(function () {
                openSearchDialog('province', '', function (value) {
                    $('#<%=txtProvinceCode.ClientID %>').val(value);
                    onTxtProvinceCodeChanged(value);
                });
            });

            $('#<%=txtProvinceCode.ClientID %>').change(function () {
                onTxtProvinceCodeChanged($(this).val());
            });

            function onTxtProvinceCodeChanged(value) {
                var filterExpression = "StandardCodeID = '0347^" + value + "'";
                Methods.getObject('GetStandardCodeList', filterExpression, function (result) {
                    if (result != null)
                        $('#<%=txtProvinceName.ClientID %>').val(result.StandardCodeName);
                    else {
                        $('#<%=txtProvinceCode.ClientID %>').val('');
                        $('#<%=txtProvinceName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Zip Code
            $('#lblZipCode.lblLink').click(function () {
                openSearchDialog('zipcodes', '', function (value) {
                    $('#<%=txtZipCode.ClientID %>').val(value);
                    onTxtZipCodeChanged(value);
                });
            });

            $('#<%=txtZipCode.ClientID %>').change(function () {
                onTxtZipCodeChanged($(this).val());
            });

            function onTxtZipCodeChanged(value) {
                var filterExpression = "ZipCode = '" + value + "'";
                Methods.getObject('GetZipCodesList', filterExpression, function (result) {
                    if (result != null)
                        $('#<%=hdnZipCode.ClientID %>').val(result.ID);
                    else {
                        $('#<%=hdnZipCode.ClientID %>').val('');
                        $('#<%=txtZipCode.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Nationality
            $('#lblNationality.lblLink').click(function () {
                openSearchDialog('nationality', '', function (value) {
                    $('#<%=txtNationalityCode.ClientID %>').val(value);
                    onTxtNationalityCodeChanged(value);
                });
            });

            $('#<%=txtNationalityCode.ClientID %>').change(function () {
                onTxtNationalityCodeChanged($(this).val());
            });

            function onTxtNationalityCodeChanged(value) {
                var filterExpression = "StandardCodeID = '0212^" + value + "'";
                Methods.getObject('GetStandardCodeList', filterExpression, function (result) {
                    if (result != null)
                        $('#<%=txtNationalityName.ClientID %>').val(result.StandardCodeName);
                    else {
                        $('#<%=txtNationalityCode.ClientID %>').val('');
                        $('#<%=txtNationalityName.ClientID %>').val('');
                    }
                });
            }
            //#endregion
            //#endregion

            registerCollapseExpandHandler();
        }
    </script>

    <input type="hidden" id="hdnID" runat="server" value="" />
    <div class="pageTitle"><%=GetLabel("Member")%></div>
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <h4 class="h4expanded"><%=GetLabel("Member Data")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:170px"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Member Code")%></label></td>
                            <td><asp:TextBox ID="txtMemberCode" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Member Name")%></label></td>
                            <td>
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:50px"/>
                                        <col style="width:3px"/>
                                        <col style="width:50px"/>
                                        <col style="width:3px"/>
                                        <col style="width:100px"/>
                                        <col style="width:3px"/>
                                        <col style="width:80px"/>
                                        <col style="width:3px"/>
                                        <col/>
                                        <col style="width:3px"/>
                                        <col style="width:50px"/>
                                    </colgroup>
                                    <tr>
                                        <td><dxe:ASPxComboBox ID="cboSalutation" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><dxe:ASPxComboBox ID="cboTitle" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtFirstName" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtMiddleName" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtLastName" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><dxe:ASPxComboBox ID="cboSuffix" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Preferred Name")%></label></td>
                            <td><asp:TextBox ID="txtPreferredName" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("City Of Birth")%></label></td>
                            <td><asp:TextBox ID="txtBirthPlace" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Date Of Birth")%></label></td>
                            <td><asp:TextBox ID="txtDOB" Width="120px" runat="server" CssClass="datepicker" /></td>
                        </tr>
                    </table>
                </div>

                <h4 class="h4expanded"><%=GetLabel("Address")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:170px"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Address")%></label></td>
                            <td><asp:TextBox ID="txtAddress" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("County")%></label></td>
                            <td><asp:TextBox ID="txtCounty" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("District")%></label></td>
                            <td><asp:TextBox ID="txtDistrict" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("City")%></label></td>
                            <td><asp:TextBox ID="txtCity" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblProvince"><%=GetLabel("Province")%></label></td>
                            <td>
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:30%"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtProvinceCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtProvinceName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblZipCode"><%=GetLabel("ZipCode")%></label></td>
                            <td>
                                <input type="hidden" runat="server" id="hdnZipCode" value="" />
                                <asp:TextBox ID="txtZipCode" Width="100%" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>

                <h4 class="h4expanded"><%=GetLabel("Contact Data")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:170px"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Telephone")%></label></td>
                            <td><asp:TextBox ID="txtTelephoneNo" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label><%=GetLabel("Mobile Phone 1")%></label></td>
                            <td><asp:TextBox ID="txtMobilePhone1" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Mobile Phone 2")%></label></td>
                            <td><asp:TextBox ID="txtMobilePhone2" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Business Email")%></label></td>
                            <td><asp:TextBox ID="txtEmail" CssClass="email" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Private Email")%></label></td>
                            <td><asp:TextBox ID="txtEmail2" CssClass="email" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Office Extension No")%></label></td>
                            <td><asp:TextBox ID="txtOfficeExtensionNo" Width="100%" runat="server" /></td>
                        </tr>
                    </table>
                </div>

                <h4 class="h4expanded"><%=GetLabel("Informasi Update Record")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Dibuat Oleh")%></label></td>
                            <td><asp:TextBox ID="txtCreatedBy" Width="200px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Dibuat Pada")%></label></td>
                            <td><asp:TextBox ID="txtCreatedDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Terakhir Diubah Oleh")%></label></td>
                            <td><asp:TextBox ID="txtLastUpdatedBy" Width="200px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Terakhir Diubah Pada")%></label></td>
                            <td><asp:TextBox ID="txtLastUpdatedDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                        </tr>
                    </table>
                </div>
            </td>
            <td style="padding:5px;vertical-align:top">
                <h4 class="h4expanded"><%=GetLabel("Additional Information")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:170px"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Member Status")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboMemberStatus" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblNationality"><%=GetLabel("Nationality")%></label></td>
                            <td>
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:30%"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtNationalityCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtNationalityName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink lblMandatory" id="lblCompany"><%=GetLabel("Company")%></label></td>
                            <td>
                                <input type="hidden" value="" id="hdnCompanyID" runat="server" />
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:30%"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtCompanyCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtCompanyName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblDepartment"><%=GetLabel("Department")%></label></td>
                            <td>
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:30%"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtDepartmentCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtDepartmentName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Occupation")%></label></td>
                            <td><asp:TextBox ID="txtOccupation" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Occupation Level")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboOccupationLevel" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Rating Level")%></label></td>
                            <td>
                                <input type="hidden" id="hdnRatingLevel" runat="server" />
                                <div id="divRatingLevel">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("VAT Registration No")%></label></td>
                            <td><asp:TextBox ID="txtVATRegistrationNo" Width="100%" runat="server" /></td>
                        </tr>
                    </table>
                </div>
                <h4 class="h4expanded"><%=GetLabel("Training Information")%></h4>                
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:170px"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Last Event Date")%></label></td>
                            <td>
                                <asp:TextBox ID="txtLastEventStartDate" CssClass="datepicker" Width="120px" runat="server" />
                                &nbsp;<%=GetLabel("-")%>&nbsp;
                                <asp:TextBox ID="txtLastEventEndDate" CssClass="datepicker" Width="120px" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Last Event")%></label></td>
                            <td><asp:TextBox ID="txtLastEvent" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Last Company")%></label></td>
                            <td><asp:TextBox ID="txtLastCompany" Width="100%" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Number Of Training")%></label></td>
                            <td><asp:TextBox ID="txtNumberOfTraining" CssClass="number" Width="100px" runat="server" /></td>
                        </tr>
                    </table>
                </div>
                <h4 class="h4expanded"><%=GetLabel("Member Status")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:10px"/>
                        </colgroup>
                        <tr>
                            <td><asp:CheckBox ID="chkIsCSClub" Width="100%" runat="server" /></td>
                            <td><%=GetLabel("Is CS Club")%></td>
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="chkIsHRDClub" Width="100%" runat="server" /></td>
                            <td><%=GetLabel("Is HRD Club")%></td>
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="chkIsISOClub" Width="100%" runat="server" /></td>
                            <td><%=GetLabel("Is ISO Club")%></td>
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="chkIsCompanyContactPerson" Width="100%" runat="server" /></td>
                            <td><%=GetLabel("Is Company Contact Person")%></td>
                        </tr>
                    </table>
                </div>
                <h4 class="h4expanded"><%=GetLabel("Other Information")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:170px"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Remarks")%></label></td>
                            <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
