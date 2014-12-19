<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventInvitationInvitedMemberViewCtl.ascx.cs" 
    Inherits="QIS.Careq.Web.SystemSetup.Program.EventInvitationInvitedMemberViewCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<script type="text/javascript" id="dxss_eventinvitationinvitedmemberviewctl">
    //#region Paging
    var pageCountPopup = parseInt('<%=PageCount %>');
    $(function () {
        setPaging($("#pagingPopup"), pageCountPopup, function (page) {
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
                cbpPopup.PerformCallback('changepage|' + page);
            });
        }

    }
    //#endregion
</script>

<input type="hidden" id="hdnID" runat="server" value="" />
<input type="hidden" id="hdnFilterExpression" runat="server" value="" />
<div style="position: relative;">
    <dxcp:ASPxCallbackPanel ID="cbpPopup" runat="server" Width="100%" ClientInstanceName="cbpPopup"
        ShowLoadingPanel="false" OnCallback="cbpPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ $('#containerImgLoadingViewPopup').show(); }"
            EndCallback="function(s,e){ onCbpPopupEndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                    <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                        <Columns>
                            <asp:BoundField DataField="MemberID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                            <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="150px" />
                            <asp:BoundField DataField="Occupation" HeaderText="Occupation" HeaderStyle-Width="100px" />
                            <asp:BoundField DataField="Department" HeaderText="Department" HeaderStyle-Width="150px" />
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