<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberIdentityCtl.ascx.cs" 
    Inherits="QIS.Careq.Web.SystemSetup.Program.MemberIdentityCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<link href="<%= ResolveUrl("~/Libs/Scripts/jquery/rateit/rateit.css")%>" rel="stylesheet" type="text/css">
<script src='<%= ResolveUrl("~/Libs/Scripts/jquery/rateit/jquery.rateit.js")%>' type='text/javascript'></script>
<script type="text/javascript" id="dxss_memberidentityctl">    
    setDatePicker('<%=txtDOB.ClientID %>');
    $('#divRatingLevel').rateit({ max: 5, step: 1, backingfld: '#<%=hdnRatingLevel.ClientID %>' });

    function onGetEntryPopupReturnValue() {
        var result = $('#<%=hdnID.ClientID %>').val();
        return result;
    }

    function onAfterSaveAddRecordEntryPopup(param) {
        return $('#<%=hdnID.ClientID %>').val(param);
    }

    //#region Popup Search
    //#region Company
    $('#lblCompany.lblLink').die('click');
    $('#lblCompany.lblLink').live('click', function () {
        openSearchDialog('company', 'IsDeleted = 0', function (value) {
            $('#<%=txtCompanyCode.ClientID %>').val(value);
            onTxtCompanyCodeChanged(value);
        });
    });

    $('#<%=txtCompanyCode.ClientID %>').die('change');
    $('#<%=txtCompanyCode.ClientID %>').live('change', function () {
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
                $('#<%=txtCompanyName.ClientID %>').val('');
            }
        });
    }
    //#endregion

    //#region Department
    $('#lblDepartment.lblLink').die('click');
    $('#lblDepartment.lblLink').live('click', function () {
        openSearchDialog('companydepartment', '', function (value) {
            $('#<%=txtDepartmentCode.ClientID %>').val(value);
            onTxtDepartmentCodeChanged(value);
        });
    });

    $('#<%=txtDepartmentCode.ClientID %>').die('change');
    $('#<%=txtDepartmentCode.ClientID %>').live('change', function () {
        onTxtDepartmentCodeChanged($(this).val());
    });

    function onTxtDepartmentCodeChanged(value) {
        var filterExpression = "StandardCodeID = 'X003^" + value + "'";
        Methods.getObject('GetStandardCodeList', filterExpression, function (result) {
            if (result != null)
                $('#<%=txtDepartmentName.ClientID %>').val(result.StandardCodeName);
            else
                $('#<%=txtDepartmentName.ClientID %>').val('');
        });
    }
    //#endregion

    //#region Province
    $('#lblProvince.lblLink').die('click');
    $('#lblProvince.lblLink').live('click', function () {
        openSearchDialog('province', '', function (value) {
            $('#<%=txtProvinceCode.ClientID %>').val(value);
            onTxtProvinceCodeChanged(value);
        });
    });

    $('#<%=txtProvinceCode.ClientID %>').die('change');
    $('#<%=txtProvinceCode.ClientID %>').live('change', function () {
        onTxtProvinceCodeChanged($(this).val());
    });

    function onTxtProvinceCodeChanged(value) {
        var filterExpression = "StandardCodeID = '0347^" + value + "'";
        Methods.getObject('GetStandardCodeList', filterExpression, function (result) {
            if (result != null)
                $('#<%=txtProvinceName.ClientID %>').val(result.StandardCodeName);
            else
                $('#<%=txtProvinceName.ClientID %>').val('');
        });
    }
    //#endregion

    //#region Zip Code
    $('#lblZipCode.lblLink').die('click');
    $('#lblZipCode.lblLink').live('click', function () {
        openSearchDialog('zipcodes', '', function (value) {
            $('#<%=txtZipCode.ClientID %>').val(value);
            onTxtZipCodeChanged(value);
        });
    });

    $('#<%=txtZipCode.ClientID %>').die('change');
    $('#<%=txtZipCode.ClientID %>').live('change', function () {
        onTxtZipCodeChanged($(this).val());
    });

    function onTxtZipCodeChanged(value) {
        var filterExpression = "ZipCode = '" + value + "'";
        Methods.getObject('GetZipCodesList', filterExpression, function (result) {
            if (result != null)
                $('#<%=hdnZipCode.ClientID %>').val(result.ID);
            else
                $('#<%=hdnZipCode.ClientID %>').val('');
        });
    }
    //#endregion

    //#region Nationality
    $('#lblNationality.lblLink').die('click');
    $('#lblNationality.lblLink').live('click', function () {
        openSearchDialog('nationality', '', function (value) {
            $('#<%=txtNationalityCode.ClientID %>').val(value);
            onTxtNationalityCodeChanged(value);
        });
    });

    $('#<%=txtNationalityCode.ClientID %>').die('change');
    $('#<%=txtNationalityCode.ClientID %>').live('change', function () {
        onTxtNationalityCodeChanged($(this).val());
    });

    function onTxtNationalityCodeChanged(value) {
        var filterExpression = "StandardCodeID = '0212^" + value + "'";
        Methods.getObject('GetStandardCodeList', filterExpression, function (result) {
            if (result != null)
                $('#<%=txtNationalityName.ClientID %>').val(result.StandardCodeName);
            else
                $('#<%=txtNationalityName.ClientID %>').val('');
        });
    }
    //#endregion
    //#endregion
</script>

<input type="hidden" id="hdnID" runat="server" value="" />
<div style="overflow-y:scroll;height:450px;">
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <h4><%=GetLabel("Member Data")%></h4>
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:120px"/>
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
                                    <td><asp:TextBox ID="txtFirstName" Style="text-transform:uppercase" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtMiddleName" Style="text-transform:uppercase" Width="100%" runat="server" /></td>
                                    <td>&nbsp;</td>
                                    <td><asp:TextBox ID="txtLastName" Style="text-transform:uppercase" Width="100%" runat="server" /></td>
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
                
                <h4><%=GetLabel("Address Data")%></h4>
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:120px"/>
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
                <h4><%=GetLabel("Contact Data")%></h4>
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:120px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Telephone")%></label></td>
                        <td><asp:TextBox ID="txtTelephoneNo" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Mobile Phone 1")%></label></td>
                        <td><asp:TextBox ID="txtMobilePhone1" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Mobile Phone 2")%></label></td>
                        <td><asp:TextBox ID="txtMobilePhone2" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Business Email")%></label></td>
                        <td><asp:TextBox ID="txtEmail" CssClass="email" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Private Email")%></label></td>
                        <td><asp:TextBox ID="txtEmail2" CssClass="email" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Office Extension No")%></label></td>
                        <td><asp:TextBox ID="txtOfficeExtensionNo" Width="100%" runat="server" /></td>
                    </tr>
                </table>
            </td>
            <td style="padding:5px;vertical-align:top">
                <h4><%=GetLabel("Additional Information")%></h4>
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:130px"/>
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
                        <td class="tdLabel"><label class="lblLink lblMandatory" id="lblDepartment"><%=GetLabel("Department")%></label></td>
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
                <h4><%=GetLabel("Training Information")%></h4>                
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:130px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Last Event Date")%></label></td>
                        <td><asp:TextBox ID="txtLastEventDate" CssClass="datepicker" Width="120px" runat="server" /></td>
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
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No Of Training")%></label></td>
                        <td><asp:TextBox ID="txtNumberOfTraining" CssClass="number" Width="100px" runat="server" /></td>
                    </tr>
                </table>
                <h4><%=GetLabel("Member Status")%></h4>
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
                <h4><%=GetLabel("Other Information")%></h4>
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:130px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Remarks")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="100%" runat="server" TextMode="MultiLine" Rows="2" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>