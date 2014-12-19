<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="CompanyEntry.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.CompanyEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">    
    <link href="<%= ResolveUrl("~/Libs/Scripts/jquery/rateit/rateit.css")%>" rel="stylesheet" type="text/css">
    <script src='<%= ResolveUrl("~/Libs/Scripts/jquery/rateit/jquery.rateit.js")%>' type='text/javascript'></script>
    <script type="text/javascript">
        function onLoad () {
            //#region LOB Class
            $('#lblLOBClass.lblLink').click(function () {
                var filterExpression = 'IsDeleted = 0';
                openSearchDialog('lobclass', filterExpression, function (value) {
                    $('#<%=txtLOBClassCode.ClientID %>').val(value);
                    onTxtLOBClassCodeChanged(value);
                });
            });

            $('#<%=txtLOBClassCode.ClientID %>').change(function () {
                onTxtLOBClassCodeChanged($(this).val());
            });

            function onTxtLOBClassCodeChanged(value) {
                var filterExpression = "LOBClassCode = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetLOBClassificationList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnLOBClassID.ClientID %>').val(result.LOBClassID);
                        $('#<%=txtLOBClassName.ClientID %>').val(result.LOBClassName);
                    }
                    else {
                        $('#<%=hdnLOBClassID.ClientID %>').val('');
                        $('#<%=txtLOBClassCode.ClientID %>').val('');
                        $('#<%=txtLOBClassName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Region
            $('#lblRegion.lblLink').click(function () {
                var filterExpression = 'IsDeleted = 0';
                openSearchDialog('region', filterExpression, function (value) {
                    $('#<%=txtRegionCode.ClientID %>').val(value);
                    onTxtRegionCodeChanged(value);
                });
            });

            $('#<%=txtRegionCode.ClientID %>').change(function () {
                onTxtRegionCodeChanged($(this).val());
            });

            function onTxtRegionCodeChanged(value) {
                var filterExpression = "RegionCode = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetRegionList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnRegionID.ClientID %>').val(result.RegionID);
                        $('#<%=txtRegionName.ClientID %>').val(result.RegionName);
                    }
                    else {
                        $('#<%=hdnRegionID.ClientID %>').val('');
                        $('#<%=txtRegionCode.ClientID %>').val('');
                        $('#<%=txtRegionName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Holding Company
            $('#lblHoldingCompany.lblLink').click(function () {
                var filterExpression = 'IsDeleted = 0';
                if ($('#<%=hdnID.ClientID %>').val() != '') {
                    filterExpression = 'CompanyID != ' + $('#<%=hdnID.ClientID %>').val() + ' AND IsDeleted = 0';
                }
                openSearchDialog('company', filterExpression, function (value) {
                    $('#<%=txtHoldingCompanyCode.ClientID %>').val(value);
                    onTxtHoldingCompanyCodeChanged(value);
                });
            });

            $('#<%=txtHoldingCompanyCode.ClientID %>').change(function () {
                onTxtHoldingCompanyCodeChanged($(this).val());
            });

            function onTxtHoldingCompanyCodeChanged(value) {
                var filterExpression = "CompanyCode = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetCompanyList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnHoldingCompanyID.ClientID %>').val(result.CompanyID);
                        $('#<%=txtHoldingCompanyName.ClientID %>').val(result.CompanyName);
                    }
                    else {
                        $('#<%=hdnHoldingCompanyID.ClientID %>').val('');
                        $('#<%=txtHoldingCompanyCode.ClientID %>').val('');
                        $('#<%=txtHoldingCompanyName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Contact Person
            $('#<%=lblContactPerson.ClientID %>.lblLink').click(function () {
                if ($('#<%=hdnID.ClientID %>').val() != '') {
                    var filterExpression = 'CompanyID = ' + $('#<%=hdnID.ClientID %>').val() + ' AND IsDeleted = 0';
                    openSearchDialog('membercontactperson', filterExpression, function (value) {
                        onTxtContactPersonCodeChanged(value);
                    });
                }
            });

            function onTxtContactPersonCodeChanged(value) {
                var filterExpression = "MemberID = " + value + " AND IsDeleted = 0";
                Methods.getObject('GetvMemberList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnContactPersonID.ClientID %>').val(result.MemberID);
                        $('#<%=txtContactPersonName.ClientID %>').val(result.MemberName);
                    }
                    else {
                        $('#<%=hdnContactPersonID.ClientID %>').val('');
                        $('#<%=txtContactPersonName.ClientID %>').val('');
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
                openSearchDialog('zipcodes', 'IsDeleted = 0', function (value) {
                    $('#<%=txtZipCode.ClientID %>').val(value);
                    onTxtZipCodeChanged(value);
                });
            });

            $('#<%=txtZipCode.ClientID %>').change(function () {
                onTxtZipCodeChanged($(this).val());
            });

            function onTxtZipCodeChanged(value) {
                var filterExpression = "ZipCode = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetvZipCodesList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnZipCode.ClientID %>').val(result.ID);
                        $('#<%=txtCity.ClientID %>').val(result.City);
                        $('#<%=txtProvinceCode.ClientID %>').val(result.cfGCProvince);
                        $('#<%=txtProvinceName.ClientID %>').val(result.Province);
                    }
                    else {
                        $('#<%=hdnZipCode.ClientID %>').val('');
                        $('#<%=txtZipCode.ClientID %>').val('');
                        $('#<%=txtCity.ClientID %>').val('');
                        $('#<%=txtProvinceCode.ClientID %>').val('');
                        $('#<%=txtProvinceName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            $('#divRatingLevel').rateit({ max: 5, step: 1, backingfld: '#<%=hdnRatingLevel.ClientID %>' });

            registerCollapseExpandHandler();
        }

        function onCboGCCompanyTypeChanged(s) {
            //if (s.GetValue() == 'X011^003') {
            //    cboGCCountryOfOrigin.SetEnabled(true);
            //}
            //else {
            //    cboGCCountryOfOrigin.SetEnabled(false);
            //    cboGCCountryOfOrigin.SetValue('');

                //alert($(cboGCCountryOfOrigin.GetInputElement()).parent().html());
                //$elm = $(cboGCCountryOfOrigin.GetInputElement()).parent();
                //alert($elm.find('input').attr('class'));
                //$elm.find('input').attr('class', 'dxeDisabled');
                //dxeEditArea dxeEditAreaSys
                //$elm.find('input').attr('class', 'dxeEditArea dxeDisabled');
            //}
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnRatingStar" runat="server" value="" />
    <div class="pageTitle"><%=GetLabel("Company")%></div>
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <h4 class="h4expanded"><%=GetLabel("General Information")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Company Code")%></label></td>
                            <td><asp:TextBox ID="txtCompanyCode" Style="text-transform:uppercase" Width="150px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Company Name")%></label></td>
                            <td><asp:TextBox ID="txtCompanyName" Style="text-transform:uppercase" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Short Name")%></label></td>
                            <td><asp:TextBox ID="txtShortName" Style="text-transform:uppercase" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" id="lblLOBClass"><%=GetLabel("LOB Type")%></label></td>
                            <td>
                                <input type="hidden" value="" id="hdnLOBClassID" runat="server" />
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:30%"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtLOBClassCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtLOBClassName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Company Type")%></label></td>
                            <td>
                                <dxe:ASPxComboBox ID="cboGCCompanyType" Width="200px" runat="server">
                                    <ClientSideEvents Init="function(s){ onCboGCCompanyTypeChanged(s); }"
                                        ValueChanged="function(s){ onCboGCCompanyTypeChanged(s); }" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Country Of Origin")%></label></td>
                            <td>
                                <dxe:ASPxComboBox ID="cboGCCountryOfOrigin" ClientInstanceName="cboGCCountryOfOrigin" Width="200px" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("VAT Registration No")%></label></td>
                            <td><asp:TextBox ID="txtVATRegistrationNo" Width="300px" runat="server" /></td>
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
                            <td class="tdLabel"><label class="lblLink" id="lblHoldingCompany"><%=GetLabel("Holding Company")%></label></td>
                            <td>
                                <input type="hidden" value="" id="hdnHoldingCompanyID" runat="server" />
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:30%"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtHoldingCompanyCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtHoldingCompanyName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" runat="server" id="lblContactPerson"><%=GetLabel("Contact Person")%></label></td>
                            <td>
                                <input type="hidden" value="" id="hdnContactPersonID" runat="server" />
                                <asp:TextBox ID="txtContactPersonName" Width="100%" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                <h4 class="h4expanded"><%=GetLabel("Company Status")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:10px"/>
                        </colgroup>
                        <tr>
                            <td><asp:CheckBox ID="chkIsTaxable" runat="server" /></td>
                            <td><%=GetLabel("Is Taxable")%></td>
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="chkIsGoPublicCompany" runat="server" /></td>
                            <td><%=GetLabel("Is Go Public Company")%></td>
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
                <h4 class="h4expanded"><%=GetLabel("Address")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Website URL")%></label></td>
                            <td><asp:TextBox ID="txtWebsiteUrl" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblRegion"><%=GetLabel("Region")%></label></td>
                            <td>
                                <input type="hidden" value="" id="hdnRegionID" runat="server" />
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:30%"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtRegionCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtRegionName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblMandatory"><%=GetLabel("Address")%></label></td>
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
                            <td class="tdLabel"><label class="lblLink" id="lblZipCode"><%=GetLabel("ZipCode")%></label></td>
                            <td>
                                <input type="hidden" runat="server" id="hdnZipCode" value="" />
                                <asp:TextBox ID="txtZipCode" Width="100%" runat="server" />
                            </td>
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
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Telephone")%></label></td>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="width:100%">
                                    <tr>
                                        <td style="width:50%"><asp:TextBox ID="txtTelephoneNo" Width="100%" runat="server" /></td>
                                        <td style="width:5px">&nbsp;</td>
                                        <td style="width:50%"><asp:TextBox ID="txtTelephoneNo2" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>                            
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Fax No")%></label></td>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="width:100%">
                                    <tr>
                                        <td style="width:50%"><asp:TextBox ID="txtFaxNo" Width="100%" runat="server" /></td>
                                        <td style="width:5px">&nbsp;</td>
                                        <td style="width:50%"><asp:TextBox ID="txtFaxNo2" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>      
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Email")%></label></td>
                            <td><asp:TextBox ID="txtEmail" CssClass="email" Width="100%" runat="server" /></td>
                        </tr>
                    </table>
                </div>
                <h4 class="h4expanded"><%=GetLabel("Other Information")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
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
