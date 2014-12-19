<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="VenueEntry.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.VenueEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
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
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <div class="pageTitle"><%=GetLabel("Venue")%></div>
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <h4><%=GetLabel("Venue Information")%></h4>
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Venue Code")%></label></td>
                        <td><asp:TextBox ID="txtVenueCode" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Venue Name")%></label></td>
                        <td><asp:TextBox ID="txtVenueName" Width="300px" runat="server" /></td>
                    </tr>
                </table>
                <h4><%=GetLabel("Address")%></h4>
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
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
                </table>
                <h4><%=GetLabel("Other Information")%></h4>
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:30%"/>
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
