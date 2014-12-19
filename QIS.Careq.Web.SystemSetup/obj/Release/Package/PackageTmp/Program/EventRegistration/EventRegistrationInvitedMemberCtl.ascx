<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventRegistrationInvitedMemberCtl.ascx.cs" 
    Inherits="QIS.Careq.Web.SystemSetup.Program.EventRegistrationInvitedMemberCtl" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="QIS.Careq.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="QIS.Careq.Web.CustomControl" TagPrefix="qis" %>

<script type="text/javascript" id="dxss_eventinvitationinvitedmemberviewctl">
    //#region Paging
    var pageCountPopup = parseInt('<%=PageCount %>');
    $(function () {
        setPaging($("#pagingPopup"), pageCountPopup, function (page) {
            getCheckedMember();
            cbpPopup.PerformCallback('changepage|' + page);
        });
    });

    function onCbpPopupEndCallback(s) {
        $('#containerImgLoadingViewPopup').hide();

        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCount = parseInt(param[1]);
            if (pageCount > 0)
                $('#<%=grdView.ClientID %> tr:eq(1)').click();

            setPaging($("#pagingPopup"), pageCount, function (page) {
                getCheckedMember();
                cbpPopup.PerformCallback('changepage|' + page);
            });
        }

    }
    //#endregion

    $('#chkSelectAllMember').die('change');
    $('#chkSelectAllMember').live('change', function () {
        var isChecked = $(this).is(":checked");
        $('.chkMember').each(function () {
            $(this).find('input').prop('checked', isChecked);
        });
    });

    function getCheckedMember() {
        var lstSelectedMember = $('#<%=hdnSelectedMember.ClientID %>').val().split(',');
        var result = '';
        $('#<%=grdView.ClientID %> .chkMember input').each(function () {
            if ($(this).is(':checked')) {
                var key = $(this).closest('tr').find('.keyField').html();
                if (lstSelectedMember.indexOf(key) < 0)
                    lstSelectedMember.push(key);
            }
            else {
                var key = $(this).closest('tr').find('.keyField').html();
                if (lstSelectedMember.indexOf(key) > -1)
                    lstSelectedMember.splice(lstSelectedMember.indexOf(key), 1);
            }
        });
        $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedMember.join(','));
    }

    $(function () {
        $('#btnCreateRegistration').click(function () {
            getCheckedMember();
            if ($('#<%=hdnSelectedMember.ClientID %>').val() == '')
                alert('Please Select Member First');
            else {
                if (confirm("Are You Sure?"))
                    cbpCreateRegistrationProcess.PerformCallback('');
            }
        });
    });

    function onRefreshGridView() {
        var filterExpression = txtSearchViewReg.GenerateFilterExpression();
        //if (typeof onRefreshControl == 'function')
        //onRefreshControl(filterExpression);
        $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
        cbpPopup.PerformCallback('refresh');
    }

    function onCboListTypeValueChanged(evt) {
        onRefreshGridView();
    }

    function onTxtSearchViewRegSearchClick(s) {
        setTimeout(function () {
            s.SetBlur();
            onRefreshGridView();
        }, 0);
    }

    function onCbpCreateRegistrationProcessEndCallback(s) {
        var result = s.cpResult.split('|');
        if (result[0] == 'fail')
            alert('Create Registration Failed\nError Message : ' + result[1]);
        else {
            alert('Create Registration Success!');
            onRefreshControl();
            pcRightPanelContent.Hide();
        }
        hideLoadingPanel();
    }
</script>

<input type="hidden" id="hdnID" runat="server" value="" />
<input type="hidden" id="hdnFilterExpression" runat="server" value="" />
<input type="hidden" id="hdnSelectedMember" runat="server" value="" />
<div style="width:100%">
    <table style="margin-top:auto" id="tblFilter" runat="server">
        <tr>
            <td>
                <qis:QISIntellisenseTextBox runat="server" ClientInstanceName="txtSearchViewReg" ID="txtSearchViewReg"
                    Width="300px" Watermark="Search">
                    <ClientSideEvents SearchClick="function(s){ onTxtSearchViewRegSearchClick(s); }" />
                    <IntellisenseHints>
                        <qis:QISIntellisenseHint Text="Member Name" FieldName="FirstName MiddleName LastName" />
                        <qis:QISIntellisenseHint Text="Company Name" FieldName="CompanyName" />
                    </IntellisenseHints>
                </qis:QISIntellisenseTextBox>
            </td>
            <td>
                <dxe:ASPxComboBox ID="cboListType" ClientInstanceName="cboListType" Width="150px" runat="server">
                    <ClientSideEvents ValueChanged="function(s,e) { onCboListTypeValueChanged(e); }" />
                </dxe:ASPxComboBox>
            </td>
        </tr>

    </table>
</div>
<div style="margin-top:10px">
    <dxcp:ASPxCallbackPanel ID="cbpPopup" runat="server" Width="100%" ClientInstanceName="cbpPopup"
        ShowLoadingPanel="false" OnCallback="cbpPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ $('#containerImgLoadingViewPopup').show(); }"
            EndCallback="function(s,e){ onCbpPopupEndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                    <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                        OnRowDataBound="grdView_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <input id="chkSelectAllMember" type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMember" runat="server" CssClass="chkMember" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MemberID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                            <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="150px" />
                            <asp:BoundField DataField="Occupation" HeaderText="Occupation" HeaderStyle-Width="100px" />
                            <asp:BoundField DataField="Department" HeaderText="Department" HeaderStyle-Width="150px" />
                            <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <%=GetLabel("Is Confirmed") %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIsConfirmed" Checked='<%# DataBinder.Eval(Container, "DataItem.IsConfirmed") %>' runat="server" CssClass="chkIsConfirmed" Enabled="false"/>
                                    <input type="hidden" class="hdnIsConfirmed" value="<%#Eval("IsConfirmed") %>" /> 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ConfirmedDateInString" HeaderText="Confirmed Date" HeaderStyle-Width="120px" />
                        </Columns>
                        <EmptyDataTemplate>
                            No Data To Display
                        </EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>
            </dx:PanelContent>
        </PanelCollection>
    </dxcp:ASPxCallbackPanel>    
    <div class="imgLoadingGrdView" id="containerImgLoadingViewPopup" >
        <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
    </div>
    <div class="containerPaging">
        <div class="wrapperPaging">
            <div id="pagingPopup"></div>
        </div>
    </div> 
</div>
<div style="width:100%;text-align:center">
    <table style="margin-left: auto; margin-right: auto; margin-top: 10px;">
        <tr>
            <td><input type="button" value='<%= GetLabel("Create Registration")%>' style="width:120px" id="btnCreateRegistration" /></td>
            <td><input type="button" value='<%= GetLabel("Close")%>' style="width:70px" onclick="pcRightPanelContent.Hide();" /></td>
        </tr>
    </table>
</div>

<dxcp:ASPxCallbackPanel ID="cbpCreateRegistrationProcess" runat="server" Width="100%" ClientInstanceName="cbpCreateRegistrationProcess"
    ShowLoadingPanel="false" OnCallback="cbpCreateRegistrationProcess_Callback">
    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpCreateRegistrationProcessEndCallback(s); }" />
</dxcp:ASPxCallbackPanel>