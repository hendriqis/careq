<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventRegistrationCompanyPICEntryCtl.ascx.cs" 
    Inherits="QIS.Careq.Web.SystemSetup.Program.EventRegistrationCompanyPICEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    $('#lblEntryPopupAddData').live('click', function () {
        $('#<%=txtCompanyCode.ClientID %>').removeAttr('readonly');
        $('#lblCompany').attr('class', 'lblLink lblMandatory');

        $('#<%=hdnIsAdd.ClientID %>').val('1');

        $('#<%=hdnCompanyID.ClientID %>').val('');
        $('#<%=txtCompanyCode.ClientID %>').val('');
        $('#<%=txtCompanyName.ClientID %>').val('');
        $('#<%=hdnContactPersonID.ClientID %>').val('');
        $('#<%=txtContactPersonName.ClientID %>').val('');
        $('#<%=txtPaymentDate.ClientID %>').val('');

        $('#containerPopupEntryData').show();
    });

    setDatePicker('<%=txtPaymentDate.ClientID %>');

    $('#btnEntryPopupCancel').live('click', function () {
        $('#containerPopupEntryData').hide();
    });

    $('#btnEntryPopupSave').click(function (evt) {
        if (IsValid(evt, 'fsEntryPopup', 'mpEntryPopup'))
            if ($('#<%=txtContactPersonName.ClientID %>').val() != '')
                cbpEntryPopupView.PerformCallback('save');
        return false;
    });

    $('.imgDelete.imgLink').die('click');
    $('.imgDelete.imgLink').live('click', function () {
        if (confirm("Are You Sure Want To Delete This Data?")) {
            $row = $(this).closest('tr');
            var id = $row.find('.hdnCompanyID').val();
            $('#<%=hdnCompanyID.ClientID %>').val(id);

            cbpEntryPopupView.PerformCallback('delete');
        }
    });

    $('.imgEdit.imgLink').die('click');
    $('.imgEdit.imgLink').live('click', function () {
        $row = $(this).closest('tr');
        var companyID = $row.find('.hdnCompanyID').val();
        var companyCode = $row.find('.hdnCompanyCode').val();
        var contactPersonID = $row.find('.hdnContactPersonID').val();
        var paymentDate = $row.find('.hdnPaymentDate').val(); 

        var companyName = $row.find('.tdCompanyName').html();
        var contactPersonName = $row.find('.tdContactPersonName').html();

        $('#lblCompany').attr('class', 'lblDisabled');
        $('#<%=txtCompanyCode.ClientID %>').attr('readonly', 'readonly');

        $('#<%=hdnCompanyID.ClientID %>').val(companyID);
        $('#<%=txtCompanyCode.ClientID %>').val(companyCode);
        $('#<%=txtCompanyName.ClientID %>').val(companyName);
        $('#<%=hdnContactPersonID.ClientID %>').val(contactPersonID);
        $('#<%=txtContactPersonName.ClientID %>').val(contactPersonName);
        $('#<%=txtPaymentDate.ClientID %>').val(paymentDate);

        $('#<%=hdnIsAdd.ClientID %>').val('0');

        $('#containerPopupEntryData').show();
    });

    //#region Company
    function getFilterExpressionCompanyPICCompany() {
        var filterExpression = "CompanyID IN (SELECT CompanyID FROM vEventRegistration WHERE EventID = " + $('#<%=hdnEventID.ClientID %>').val() + ") AND CompanyID NOT IN (SELECT CompanyID FROM EventCompanyDt WHERE EventID = " + $('#<%=hdnEventID.ClientID %>').val() + ")";
        return filterExpression;
    }

    $('#lblCompany.lblLink').die('click');
    $('#lblCompany.lblLink').live('click', function () {
        var filterExpression = getFilterExpressionCompanyPICCompany();
        openSearchDialog('company', filterExpression, function (value) {
            $('#<%=txtCompanyCode.ClientID %>').val(value);
            onTxtCompanyCodeChanged(value);
        });
    });

    $('#<%=txtCompanyCode.ClientID %>').die('change');
    $('#<%=txtCompanyCode.ClientID %>').live('change', function () {
        onTxtCompanyCodeChanged($(this).val());
    });

    function onTxtCompanyCodeChanged(value) {
        var filterExpression = "CompanyCode = '" + value + "' AND " + getFilterExpressionCompanyPICCompany() + " AND IsDeleted = 0";
        Methods.getObject('GetCompanyList', filterExpression, function (result) {
            if (result != null) {
                $('#<%=hdnCompanyID.ClientID %>').val(result.CompanyID);
                $('#<%=txtCompanyCode.ClientID %>').val(result.CompanyCode);
                $('#<%=txtCompanyName.ClientID %>').val(result.CompanyName);
            }
            else {
                $('#<%=hdnCompanyID.ClientID %>').val('');
                $('#<%=txtCompanyName.ClientID %>').val('');
            }
            $('#<%=hdnContactPersonID.ClientID %>').val('');
            $('#<%=txtContactPersonName.ClientID %>').val('');
        });
    }
    //#endregion

    //#region Contact Person
    $('#lblContactPerson.lblLink').die('click');
    $('#lblContactPerson.lblLink').live('click', function () {
        if ($('#<%=hdnCompanyID.ClientID %>').val() != '') {
            var filterExpression = 'CompanyID = ' + $('#<%=hdnCompanyID.ClientID %>').val() + " AND MemberID IN (SELECT MemberID FROM EventRegistration WHERE EventID = " + $('#<%=hdnEventID.ClientID %>').val() + " AND GCRegistrationStatus != 'X010^007') AND IsDeleted = 0";
            openSearchDialog('member', filterExpression, function (value) {
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

    function onCbpEntryPopupViewEndCallback(s) {
        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                alert('Save Failed\nError Message : ' + param[2]);
            else {
                var pageCount = parseInt(param[2]);
                setPagingDetailItem(pageCount);
                $('#containerPopupEntryData').hide();
            }
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                alert('Delete Failed\nError Message : ' + param[2]);
            else {
                var pageCount = parseInt(param[2]);
                setPagingDetailItem(pageCount);
            }
        }
        else if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            setPagingDetailItem(pageCount);
        }
        hideLoadingPanel();
    }

    function setPagingDetailItem(pageCount) {
        setPaging($("#pagingPopup"), pageCount, function (page) {
            cbpEntryPopupView.PerformCallback('changepage|' + page);
        }, 8);
    }

    //#region Paging
    var pageCountPopup = parseInt('<%=PageCount %>');
    $(function () {
        setPagingDetailItem(pageCountPopup);
    });
    //#endregion
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnEventID" value="" runat="server" />
    <div class="pageTitle"><%=GetLabel("Company PIC")%></div>
    <table class="tblContentArea">
        <colgroup>
            <col style="width:100%"/>
        </colgroup>
        <tr>            
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:70%">
                    <colgroup>
                        <col style="width:160px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Event")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr> 
                </table>

                <div id="containerPopupEntryData" style="margin-top:10px;display:none;">
                    <input type="hidden" id="hdnIsAdd" runat="server" value="" />
                    <div class="pageTitle"><%=GetLabel("Entry")%></div>
                    <fieldset id="fsEntryPopup" style="margin:0"> 
                        <table class="tblEntryDetail" style="width:100%">
                            <colgroup>
                                <col style="width:150px"/>
                                <col />
                            </colgroup>
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
                                            <td><asp:TextBox ID="txtCompanyCode" CssClass="required" Width="100%" runat="server" /></td>
                                            <td>&nbsp;</td>
                                            <td><asp:TextBox ID="txtCompanyName" ReadOnly="true" Width="100%" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel"><label class="lblLink lblMandatory" id="lblContactPerson"><%=GetLabel("Contact Person")%></label></td>
                                <td>
                                    <input type="hidden" value="" id="hdnContactPersonID" runat="server" />
                                    <asp:TextBox ID="txtContactPersonName" CssClass="required" ReadOnly="true" Width="100%" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Payment Date")%></label></td>
                                <td><asp:TextBox ID="txtPaymentDate" CssClass="required datepicker" Width="120px" runat="server" /></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>
                                                <input type="button" id="btnEntryPopupSave" value='<%= GetLabel("Save")%>' />
                                            </td>
                                            <td>
                                                <input type="button" id="btnEntryPopupCancel" value='<%= GetLabel("Cancel")%>' />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>

                <div style="position:relative;height:297px;overflow-y:auto; overflow-x: hidden;">
                    <dxcp:ASPxCallbackPanel ID="cbpEntryPopupView" runat="server" Width="100%" ClientInstanceName="cbpEntryPopupView"
                        ShowLoadingPanel="false" OnCallback="cbpEntryPopupView_Callback">
                        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                            EndCallback="function(s,e){ onCbpEntryPopupViewEndCallback(s); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                    <asp:GridView ID="grdView" runat="server" CssClass="grdView notAllowSelect" AutoGenerateColumns="false" OnRowDataBound="grdView_RowDataBound" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <img class="imgEdit imgLink" src='<%# ResolveUrl("~/Libs/Images/Button/edit.png")%>' alt="" style="float:left; margin-left:7px" />
                                                    <img class="imgDelete imgLink" src='<%# ResolveUrl("~/Libs/Images/Button/delete.png")%>' alt="" />
                                                
                                                    <input type="hidden" class="hdnCompanyID" value="<%# Eval("CompanyID")%>" />
                                                    <input type="hidden" class="hdnCompanyCode" value="<%# Eval("CompanyCode")%>" />
                                                    <input type="hidden" class="hdnContactPersonID" value="<%# Eval("PICMemberID")%>" />
                                                    <input type="hidden" class="hdnPaymentDate" value="<%# Eval("PaymentDateInDatePickerFormat")%>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderStyle-Width="300px" DataField="CompanyName" ItemStyle-CssClass="tdCompanyName" HeaderText="Company" />
                                            <asp:BoundField DataField="PICMemberName" HeaderText="PIC" ItemStyle-CssClass="tdContactPersonName" />
                                            <asp:BoundField HeaderStyle-Width="150px" DataField="PaymentDateInString" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Payment Date" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <%=GetLabel("No Data To Display")%>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </asp:Panel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>
                    <div class="imgLoadingGrdView" id="containerImgLoadingViewPopup">
                        <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                    </div>
                    <div class="containerPaging">
                        <div class="wrapperPaging">
                            <div id="pagingPopup"></div>
                        </div>
                    </div> 
                </div>
                <div style="width:100%;text-align:center" id="divContainerAddData" runat="server">
                    <span class="lblLink" id="lblEntryPopupAddData"><%= GetLabel("Add Data")%></span>
                </div>
            </td>
        </tr>
    </table>
    <div style="width:100%;text-align:right">
        <input type="button" value='<%= GetLabel("Close")%>' onclick="pcRightPanelContent.Hide();" />
    </div>
</div>

