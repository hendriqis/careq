<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPList.master" AutoEventWireup="true" 
CodeBehind="EventRegistrationDtList.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.EventRegistrationDtList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnEventInvitationConfirmationBack" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/back.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("View")%></div></li>
    <li id="btnSendMail" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbsendmail.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Send Mail")%></div></li>
    <li id="btnEventInvitationInvitedView" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbnew.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Invited Member")%></div></li>
    <li id="btnEventRegistrationCompanyPIC" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbset.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Company PIC")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=grdView.ClientID %> tr:gt(0):not(.trEmpty)').live('click', function () {
                $('#<%=grdView.ClientID %> tr.selected').removeClass('selected');
                $(this).addClass('selected');
                $('#<%=hdnMemberID.ClientID %>').val($(this).find('.keyField').html());
            });
            $('#<%=grdView.ClientID %> tr:eq(1)').click();

            $('#<%=btnEventInvitationInvitedView.ClientID %>').click(function () {
                var id = $('#<%=hdnEventID.ClientID %>').val();
                var url = ResolveUrl("~/Program/EventRegistration/EventRegistrationInvitedMemberCtl.ascx");
                openUserControlPopup(url, id, 'Invited Member', 950, 500);
            });

            $('#<%=btnEventRegistrationCompanyPIC.ClientID %>').click(function () {
                var id = $('#<%=hdnEventID.ClientID %>').val();
                var url = ResolveUrl("~/Program/EventRegistration/EventRegistrationCompanyPICEntryCtl.ascx");
                openUserControlPopup(url, id, 'Company PIC', 750, 500);
            });

            $('#<%=btnSendMail.ClientID %>').click(function () {
                getCheckedMember();
                if ($('#<%=hdnSelectedMember.ClientID %>').val() == '')
                    alert('Please Select Member First');
                else {
                    var id = $('#<%=hdnEventID.ClientID %>').val() + '|' + $('#<%=hdnSelectedMember.ClientID %>').val();
                    var url = ResolveUrl("~/Program/EventRegistration/EventConfirmationSendEmailCtl.ascx");
                    openUserControlPopup(url, id, 'Send Email', 700, 500);
                }
            });
        });

        function onBeforeLoadRightPanelContent(code) {
            return $('#<%=hdnEventID.ClientID %>').val();
        }

        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        $(function () {
            setPaging($("#paging"), pageCount, function (page) {
                getCheckedMember();
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
                    getCheckedMember();
                    cbpView.PerformCallback('changepage|' + page);
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();

        }
        //#endregion

        $(function () {
            $('#<%=btnEventInvitationConfirmationBack.ClientID %>').click(function () {
                document.location = ResolveUrl('~/Program/EventRegistration/EventRegistrationList.aspx');
            });
        });

        function onCustomClickQuickMenuPrint(code) {
            if (code == 'daftarhadir') {
                openReportViewer('F000003', $('#<%=hdnEventID.ClientID %>').val());
            }
        }

        $('#chkSelectAllMember').die('change');
        $('#chkSelectAllMember').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkMember').each(function () {
                $(this).find('input').prop('checked', isChecked);
            });
        });

        $("#<%=rblCheckUncheckAll.ClientID %>").die('change');
        $("#<%=rblCheckUncheckAll.ClientID %>").live('change', function () {
            var value = $("#<%=rblCheckUncheckAll.ClientID %> input:checked").val();
            if (value == '1') {
                $('#chkSelectAllMember').prop('checked', true);
                $('#chkSelectAllMember').change();
                $('#<%=hdnSelectedMember.ClientID %>').val($('#<%=hdnListAllMemberID.ClientID %>').val());
            }
            else {
                $('#chkSelectAllMember').prop('checked', false);
                $('.chkMember').each(function () {
                    $(this).find('input').prop('checked', false);
                });
                $('#<%=hdnSelectedMember.ClientID %>').val('');
            }
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
    </script>
    <input type="hidden" id="hdnEventID" runat="server" value="" />
    <input type="hidden" id="hdnMemberID" runat="server" value="" />
    <input type="hidden" id="hdnSelectedMember" runat="server" value="" />
    <input type="hidden" id="hdnFilterExpression" runat="server" value="" />
    <asp:RadioButtonList ID="rblCheckUncheckAll" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Text="Check All" Value="1" />
        <asp:ListItem Text="Uncheck All" Value="2" />
    </asp:RadioButtonList>
    <div style="position: relative;">
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                        <input type="hidden" id="hdnListAllMemberID" runat="server" value="" />
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
                                <asp:BoundField DataField="cfEmailAddress" HeaderText="Email Address" />
                                <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="Occupation" HeaderText="Occupation" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="Department" HeaderText="Department" HeaderStyle-Width="200px" />
                                <asp:BoundField DataField="RegistrationType" HeaderText="Registration Type" HeaderStyle-Width="120px" />
                                <asp:BoundField DataField="RegistrationStatus" HeaderText="Registration Status" HeaderStyle-Width="120px" />
                                <asp:BoundField DataField="InformationSource" HeaderText="Information Source" HeaderStyle-Width="120px" />
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
</asp:Content>
