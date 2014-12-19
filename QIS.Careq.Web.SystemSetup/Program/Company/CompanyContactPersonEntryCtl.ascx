<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyContactPersonEntryCtl.ascx.cs" 
    Inherits="QIS.Careq.Web.SystemSetup.Program.CompanyContactPersonEntryCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    $('#lblEntryPopupAddData').live('click', function () {
        $('#<%=txtDepartmentCode.ClientID %>').removeAttr('readonly');

        $('#<%=txtDepartmentCode.ClientID %>').val('');
        $('#<%=txtDepartmentName.ClientID %>').val('');
        $('#<%=hdnContactPersonID.ClientID %>').val('');
        $('#<%=txtContactPersonName.ClientID %>').val('');
        $('#<%=hdnEntryID.ClientID %>').val('');        

        $('#containerPopupEntryData').show();
    });

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
            var id = $row.find('.hdnID').val();
            $('#<%=hdnEntryID.ClientID %>').val(id);

            cbpEntryPopupView.PerformCallback('delete');
        }
    });

    $('.imgEdit.imgLink').die('click');
    $('.imgEdit.imgLink').live('click', function () {
        $row = $(this).closest('tr');
        var id = $row.find('.hdnID').val();

        var departmentCode = $row.find('.hdnDepartmentID').val().split('^')[1];
        var contactPersonID = $row.find('.hdnContactPersonID').val();

        var departmentName = $row.find('.tdDepartmentName').html();
        var contactPersonName = $row.find('.tdContactPersonName').html();

        $('#<%=txtDepartmentCode.ClientID %>').attr('readonly', 'readonly');

        $('#<%=txtDepartmentCode.ClientID %>').val(departmentCode);
        $('#<%=txtDepartmentName.ClientID %>').val(departmentName);
        $('#<%=hdnContactPersonID.ClientID %>').val(contactPersonID);
        $('#<%=txtContactPersonName.ClientID %>').val(contactPersonName);

        $('#<%=hdnEntryID.ClientID %>').val(id);

        $('#containerPopupEntryData').show();
    });

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
            else {
                $('#<%=txtDepartmentCode.ClientID %>').val('');
                $('#<%=txtDepartmentName.ClientID %>').val('');
            }
            $('#<%=hdnContactPersonID.ClientID %>').val('');
            $('#<%=txtContactPersonName.ClientID %>').val('');
        });
    }
    //#endregion

    //#region Contact Person
    $('#lblContactPerson.lblLink').die('click');
    $('#lblContactPerson.lblLink').live('click', function () {
        if ($('#<%=txtDepartmentCode.ClientID %>').val() != '') {
            var filterExpression = 'CompanyID = ' + $('#<%=hdnCompanyID.ClientID %>').val();
            filterExpression += " AND GCDepartment = 'X003^" + $('#<%=txtDepartmentCode.ClientID %>').val() + "'";
            filterExpression += " AND IsDeleted = 0";
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

    function onCbpEntryPopupViewEndCallback(s) {
        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                alert('Save Failed\nError Message : ' + param[2]);
            else
                $('#containerPopupEntryData').hide();
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                alert('Delete Failed\nError Message : ' + param[2]);
        }
        hideLoadingPanel();
    }
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnCompanyID" value="" runat="server" />
    <div class="pageTitle"><%=GetLabel("Contact Person")%></div>
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
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Company")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr> 
                </table>

                <div id="containerPopupEntryData" style="margin-top:10px;display:none;">
                    <input type="hidden" id="hdnEntryID" runat="server" value="" />
                    <div class="pageTitle"><%=GetLabel("Entry")%></div>
                    <fieldset id="fsEntryPopup" style="margin:0"> 
                        <table class="tblEntryDetail" style="width:100%">
                            <colgroup>
                                <col style="width:150px"/>
                                <col />
                            </colgroup>
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
                                            <td><asp:TextBox ID="txtDepartmentCode" CssClass="required" Width="100%" runat="server" /></td>
                                            <td>&nbsp;</td>
                                            <td><asp:TextBox ID="txtDepartmentName" ReadOnly="true" Width="100%" runat="server" /></td>
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
                                                
                                                <input type="hidden" class="hdnID" value="<%# Eval("ID")%>" />
                                                <input type="hidden" class="hdnDepartmentID" value="<%# Eval("GCDepartment")%>" />
                                                <input type="hidden" class="hdnContactPersonID" value="<%# Eval("MemberID")%>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderStyle-Width="150px" DataField="Department" ItemStyle-CssClass="tdDepartmentName" HeaderText="Department" />
                                        <asp:BoundField DataField="ContactPersonName" HeaderText="Contact Person" ItemStyle-CssClass="tdContactPersonName" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Data To Display
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
                <div class="imgLoadingGrdView" id="containerImgLoadingViewPopup">
                    <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                </div>
                <div style="width:100%;text-align:center" id="divContainerAddData" runat="server">
                    <span class="lblLink" id="lblEntryPopupAddData"><%= GetLabel("AddData")%></span>
                </div>
            </td>
        </tr>
    </table>
    <div style="width:100%;text-align:right">
        <input type="button" value='<%= GetLabel("Close")%>' onclick="pcRightPanelContent.Hide();" />
    </div>
</div>

