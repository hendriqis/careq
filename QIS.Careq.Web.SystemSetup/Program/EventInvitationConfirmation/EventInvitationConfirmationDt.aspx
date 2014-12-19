<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPList.master" AutoEventWireup="true" 
CodeBehind="EventInvitationConfirmationDt.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.EventInvitationConfirmationDt" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnEventInvitationConfirmationBack" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/back.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("View")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        $(function () {
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
            });
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                if (pageCount > 0)
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();

                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();

            $('#<%=grdView.ClientID %> .txtConfirmDate').each(function () {
                setDatePickerElement($(this));
                $chkIsConfirmed = $(this).closest('tr').find('.chkIsConfirmed input');
                if ($chkIsConfirmed.is(':checked'))
                    $(this).datepicker('enable');
                else
                    $(this).datepicker('disable');
            });

        }
        //#endregion

        $btnSave = null;
        $('.btnSave').live('click', function () {
            if ($(this).attr('enabled') != 'false') {
                var memberID = $(this).closest('tr').find('.keyField').html();
                var confirmDate = $(this).closest('tr').find('.txtConfirmDate').val();
                var isConfirm = '0';
                if ($(this).closest('tr').find('.chkIsConfirmed input').is(':checked'))
                    isConfirm = '1';
                var param = memberID + '|' + isConfirm + '|' + confirmDate;

                $btnSave = $(this);
                cbpSaveConfirm.PerformCallback(param);
            }
        });

        $('.chkIsConfirmed input').live('change', function () {
            $txtConfirmDate = $(this).closest('tr').find('.txtConfirmDate');
            var isConfirmDB = ($(this).closest('tr').find('.hdnIsConfirmed').val() == 'True');
            if ($(this).is(':checked'))
                $txtConfirmDate.datepicker('enable');
            else
                $txtConfirmDate.datepicker('disable');

            if ($(this).is(':checked') == isConfirmDB)
                $(this).closest('tr').find('.btnSave').attr('enabled', 'false');
            else
                $(this).closest('tr').find('.btnSave').removeAttr('enabled');
        });

        $(function () {
            var today = Methods.dateToDatePickerFormat(todayDate);
            $('#<%=grdView.ClientID %> .txtConfirmDate').each(function () {
                setDatePickerElement($(this));
                $chkIsConfirmed = $(this).closest('tr').find('.chkIsConfirmed input');
                if ($chkIsConfirmed.is(':checked'))
                    $(this).datepicker('enable');
                else {
                    $(this).datepicker('disable');
                    $(this).val(today);
                }
            });

            $('#<%=btnEventInvitationConfirmationBack.ClientID %>').click(function () {
                document.location = ResolveUrl('~/Program/EventInvitationConfirmation/EventInvitationConfirmationList.aspx');
            });
        });

        function onCbpSaveConfirmEndCallback(s) {
            var result = s.cpResult.split('|');
            if (result[0] == 'success') {
                $hdnIsConfirmed = $btnSave.closest('tr').find('.hdnIsConfirmed');
                if ($hdnIsConfirmed.val() == 'True')
                    $hdnIsConfirmed.val('False');
                else
                    $hdnIsConfirmed.val('True');
                $btnSave.attr('enabled', 'false');
            }
            else {
                if (result[1] != '')
                    alert('Save Failed\nError Message : ' + result[1]);
                else
                    alert('Save Failed');
            }
            hideLoadingPanel();
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                            OnRowDataBound="grdView_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="MemberID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                                <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="Occupation" HeaderText="Occupation" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="Department" HeaderText="Department" HeaderStyle-Width="150px" />
                                <asp:TemplateField HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <%=GetLabel("Registration") %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsConfirmed" Checked='<%# DataBinder.Eval(Container, "DataItem.IsConfirmed") %>' runat="server" CssClass="chkIsConfirmed" />
                                        <input type="hidden" class="hdnIsConfirmed" value="<%#Eval("IsConfirmed") %>" /> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="145px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <%=GetLabel("Confirm Date") %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input type="text" class="datepicker txtConfirmDate" id="txtConfirmDate" runat="server" style="width:120px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <%=GetLabel("Save") %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <input type="button" class="btnSave" enabled="false" value="Save" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No Data To Display
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>    
        <div class="imgLoadingGrdView" id="containerImgLoadingView" >
            <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
        </div>
        <div class="containerPaging">
            <div class="wrapperPaging">
                <div id="paging"></div>
            </div>
        </div> 
    </div>

    <dxcp:ASPxCallbackPanel ID="cbpSaveConfirm" runat="server" Width="100%" ClientInstanceName="cbpSaveConfirm"
        ShowLoadingPanel="false" OnCallback="cbpSaveConfirm_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpSaveConfirmEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>
