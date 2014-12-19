<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPList.master" AutoEventWireup="true" 
CodeBehind="UpdateRegistrationStatusDtList.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.UpdateRegistrationStatusDtList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnUpdateRegistrationStatusDtBack" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/back.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("View")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        function onRefreshControl(filterExpression) {
            getCheckedMember();
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        function onBeforeLoadRightPanelContent(code) {
            return $('#<%=hdnEventID.ClientID %>').val();
        }

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var pageCountPerMember = parseInt('<%=PageCountPerMember %>');

        $(function () {
            $('#ulTabUpdateRegistration li').click(function () {
                var idx = $('#ulTabUpdateRegistration li').index($(this));
                $('#ulTabUpdateRegistration li.selected').removeAttr('class');
                $('.containerUpdateRegistration').filter(':visible').hide();
                $contentID = $(this).attr('contentid');
                $('#' + $contentID).show();
                $(this).addClass('selected');
                if (idx > 0)
                    cbpViewPerMember.PerformCallback('refresh');
                else
                    cbpView.PerformCallback('refresh');

            });

            setPaging($("#paging"), pageCount, function (page) {
                getCheckedMember();
                cbpView.PerformCallback('changepage|' + page);
            });

            setPaging($("#pagingPerMember"), pageCountPerMember, function (page) {
                cbpViewPerMember.PerformCallback('changepage|' + page);
            });
            $('#containerUpdatePerMember').hide();

            $('#btnUpdateStatusToProcess').click(function () {
                getCheckedMember();
                if ($('#<%=hdnSelectedMember.ClientID %>').val() == '')
                    alert('Please Select Member First');
                else {
                    cbpProcessAll.PerformCallback();
                }
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
                    getCheckedMember();
                    cbpView.PerformCallback('changepage|' + page);
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();

        }

        function onCbpViewPerMemberEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                if (pageCount > 0)
                    $('#<%=grdViewPerMember.ClientID %> tr:eq(1)').click();

                setPaging($("#pagingPerMember"), pageCount, function (page) {
                    cbpViewPerMember.PerformCallback('changepage|' + page);
                });
            }
            else
                $('#<%=grdViewPerMember.ClientID %> tr:eq(1)').click();

        }
        //#endregion

        $(function () {
            $('#<%=btnUpdateRegistrationStatusDtBack.ClientID %>').click(function () {
                document.location = ResolveUrl('~/Program/UpdateRegistrationStatus/UpdateRegistrationStatusList.aspx');
            });
        });

        function onCboRegistrationStatusChanged(s) {
            $tr = $(s.GetInputElement()).closest('tr').parent().closest('tr');
            var GCRegistrationStatus = $tr.find('.divRegistrationStatus').html();
            if (s.GetValue() == GCRegistrationStatus)
                $tr.find('.btnSave').attr('enabled', 'false');
            else
                $tr.find('.btnSave').removeAttr('enabled');

            $tr.find('.divRegistrationStatusCurrValue').html(s.GetValue());
        }

        $btnSave = null;
        $('.btnSave').live('click', function () {
            if ($(this).attr('enabled') != 'false') {
                var memberID = $(this).closest('tr').find('.keyField').html();
                var registrationStatus = $(this).closest('tr').find('.divRegistrationStatusCurrValue').html();
                    
                var param = memberID + '|' + registrationStatus;

                $btnSave = $(this);
                cbpSaveRegistrationStatus.PerformCallback(param);
            }
        });

        function onCbpSaveRegistrationStatusEndCallback(s) {
            var result = s.cpResult.split('|');
            if (result[0] == 'success') {
                $tr = $btnSave.closest('tr');
                $tr.find('.divRegistrationStatus').html($tr.find('.divRegistrationStatusCurrValue').html());
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

        function onCbpProcessAllEndCallback(s) {
            var result = s.cpResult.split('|');
            if (result[0] == 'success') {
                $('#<%=hdnSelectedMember.ClientID %>').val('');
                cbpView.PerformCallback('refresh');
            }
            else {
                if (result[1] != '')
                    alert('Save Failed\nError Message : ' + result[1]);
                else
                    alert('Save Failed');
            }
            hideLoadingPanel();
        }

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

        $('#chkSelectAllMember').die('change');
        $('#chkSelectAllMember').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkMember').each(function () {
                $(this).find('input').prop('checked', isChecked);
            });
        });

        function onCboUpdateStatusToValueChanged() {
            $('#<%=hdnSelectedMember.ClientID %>').val('');
            cbpView.PerformCallback('refresh');
        }

        function onCustomClickQuickMenuPrint(code) {
            if (code == 'laporanevent') {
                openReportViewer('F000005', $('#<%=hdnEventID.ClientID %>').val() + "|GCRegistrationStatus = 'X010^003'");
            }
        }
    </script>
    <input type="hidden" id="hdnEventID" runat="server" value="" />
    <input type="hidden" id="hdnMemberID" runat="server" value="" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <input type="hidden" id="hdnSelectedMember" runat="server" value="" />

    <div class="containerUlTabPage">
        <ul class="ulTabPage" id="ulTabUpdateRegistration">
            <li class="selected" contentid="containerUpdateAll"><%=GetLabel("All Member") %></li>
            <li contentid="containerUpdatePerMember"><%=GetLabel("Per Member") %></li>
        </ul>
    </div>

    <div id="containerUpdateAll" class="containerUpdateRegistration">
        <div style="float:right">
            <table cellpadding="0" cellspacing="0" style="margin:3px 0;">
                <tr>
                    <td style="width:130px"><%=GetLabel("Update Status To") %></td>
                    <td style="width:210px">
                        <dxe:ASPxComboBox ID="cboUpdateStatusTo" runat="server" Width="200px">
                            <ClientSideEvents ValueChanged="function(s,e){
                                onCboUpdateStatusToValueChanged();
                            }" />
                        </dxe:ASPxComboBox>
                    </td>
                    <td><input type="button" id="btnUpdateStatusToProcess" value="<%=GetLabel("Process") %>" /></td>
                </tr>                        
            </table>
        </div>
        <br style="clear:both" />
        <div style="position: relative;">
            <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                    EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">
                        <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="height:378px">
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
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="200px" />
                                    <asp:BoundField DataField="LOBClassName" HeaderText="LOB" HeaderStyle-Width="150px" />
                                    <asp:BoundField DataField="Occupation" HeaderText="Occupation" HeaderStyle-Width="200px" />
                                    <asp:BoundField DataField="Department" HeaderText="Department" HeaderStyle-Width="150px" />
                                    <asp:BoundField DataField="RegistrationType" HeaderText="Registration Type" HeaderStyle-Width="150px" />                            
                                    <asp:BoundField DataField="RegistrationStatus" HeaderText="Registration Status" HeaderStyle-Width="150px" />                              
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
    </div>
    <div id="containerUpdatePerMember" class="containerUpdateRegistration">
        <div style="position: relative;">
            <dxcp:ASPxCallbackPanel ID="cbpViewPerMember" runat="server" Width="100%" ClientInstanceName="cbpViewPerMember"
                ShowLoadingPanel="false" OnCallback="cbpViewPerMember_Callback">
                <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                    EndCallback="function(s,e){ onCbpViewPerMemberEndCallback(s); }" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent2" runat="server">
                        <asp:Panel runat="server" ID="Panel1" CssClass="pnlContainerGrid">
                            <asp:GridView ID="grdViewPerMember" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty"
                                OnRowDataBound="grdViewPerMember_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="MemberID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                    <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                                    <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="200px" />
                                    <asp:BoundField DataField="LOBClassName" HeaderText="LOB" HeaderStyle-Width="150px" />
                                    <asp:BoundField DataField="Occupation" HeaderText="Occupation" HeaderStyle-Width="200px" />
                                    <asp:BoundField DataField="Department" HeaderText="Department" HeaderStyle-Width="150px" />
                                    <asp:BoundField DataField="RegistrationType" HeaderText="Registration Type" HeaderStyle-Width="150px" />                              
                                    <asp:TemplateField HeaderStyle-Width="145px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <%=GetLabel("Registration Status") %>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="display:none" class="divRegistrationStatus"><%#Eval("GCRegistrationStatus") %></div>
                                            <div style="display:none" class="divRegistrationStatusCurrValue"><%#Eval("GCRegistrationStatus") %></div>
                                            <dxe:ASPxComboBox ID="cboRegistrationStatus" runat="server" Width="90%">
                                                <ClientSideEvents ValueChanged="function(s,e){ onCboRegistrationStatusChanged(s); }" />
                                            </dxe:ASPxComboBox>
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
            <div class="imgLoadingGrdView" id="Div1" >
                <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
            </div>
            <div class="containerPaging">
                <div class="wrapperPaging">
                    <div id="pagingPerMember"></div>
                </div>
            </div> 
        </div>
    </div>

    <div style="display:none">
        <dxcp:ASPxCallbackPanel ID="cbpSaveRegistrationStatus" runat="server" Width="100%" ClientInstanceName="cbpSaveRegistrationStatus"
            ShowLoadingPanel="false" OnCallback="cbpSaveRegistrationStatus_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpSaveRegistrationStatusEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel>

        <dxcp:ASPxCallbackPanel ID="cbpProcessAll" runat="server" Width="100%" ClientInstanceName="cbpProcessAll"
            ShowLoadingPanel="false" OnCallback="cbpProcessAll_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpProcessAllEndCallback(s); }" />
        </dxcp:ASPxCallbackPanel>
    </div>
</asp:Content>
