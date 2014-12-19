<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataMigrationProcessedDataCtl.ascx.cs" 
    Inherits="QIS.Careq.Web.CommonLibs.Tools.DataMigrationProcessedDataCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="QIS.Careq.Web.CustomControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" 
    Namespace="QIS.Careq.Web.CustomControl" TagPrefix="qis" %>

<script type="text/javascript" id="dxss_datamigrationprocesseddata">
    //#region Paging
    var pageCountPopup = parseInt('<%=PageCount %>');
    $(function () {
        setPaging($("#pagingPopup"), pageCountPopup, function (page) {
            getCheckedRestore();
            cbpViewPopup.PerformCallback('changepage|' + page);
        });
    });

    function onCbpViewPopupEndCallback(s) {
        hideLoadingPanel();

        var param = s.cpResult.split('|');
        if (param[0] == 'refresh') {
            var pageCountPopup = parseInt(param[1]);
            if (pageCountPopup > 0)
                $('#<%=grdViewPopup.ClientID %> tr:eq(1)').click();

            setPaging($("#pagingPopup"), pageCountPopup, function (page) {
                getCheckedRestore();
                cbpViewPopup.PerformCallback('changepage|' + page);
            });
        }
        else
            $('#<%=grdViewPopup.ClientID %> tr:eq(1)').click();
    }
    //#endregion

    $('#chkSelectAllRestore').die('change');
    $('#chkSelectAllRestore').live('change', function () {
        var isChecked = $(this).is(":checked");
        $('.chkRestore').each(function () {
            $(this).find('input').prop('checked', isChecked);
        });
    });

    function getCheckedRestore() {
        var lstSelectedRestore = $('#<%=hdnSelectedRestore.ClientID %>').val().split(',');
        var result = '';
        $('#<%=grdViewPopup.ClientID %> .chkRestore input').each(function () {
            if ($(this).is(':checked')) {
                var key = $(this).closest('tr').find('.keyField').html();
                if (lstSelectedRestore.indexOf(key) < 0)
                    lstSelectedRestore.push(key);
            }
            else {
                var key = $(this).closest('tr').find('.keyField').html();
                if (lstSelectedRestore.indexOf(key) > -1)
                    lstSelectedRestore.splice(lstSelectedRestore.indexOf(key), 1);
            }
        });
        $('#<%=hdnSelectedRestore.ClientID %>').val(lstSelectedRestore.join(','));
    }

    function onCboMigrationStatusValueChanged(s) {
        $('#<%=hdnSelectedRestore.ClientID %>').val('');
        cbpViewPopup.PerformCallback('refresh');
    }

    $(function () {
        $('#<%=btnMPListRestore.ClientID %>').click(function () {
            getCheckedRestore();
            cbpViewPopupRestore.PerformCallback('restore');
        });
    });

    function onCbpViewPopupRestoreEndCallback(s) {
        var param = s.cpResult.split('|');
        if (param[1] == 'success') {
            $('#<%=hdnSelectedRestore.ClientID %>').val('');
            cbpViewPopup.PerformCallback('refresh');
            cbpView.PerformCallback('refresh');
        }
        else 
            alert('Restore Failed\nError Message : ' + param[2]);
    }

    function onRefreshControlPopup(filterExpression) {
        $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
        cbpViewPopup.PerformCallback('refresh');
    }

    function onTxtSearchViewPopupSearchClick(s) {
        setTimeout(function () {
            s.SetBlur();
            var filterExpression = s.GenerateFilterExpression();
            onRefreshControlPopup(filterExpression);
        }, 0);
    }
</script>

<input type="hidden" id="hdnFilterExpression" runat="server" value="" />
<input type="hidden" id="hdnSelectedRestore" runat="server" value="" />
<div class="toolbarArea" style="height:50px">
    <table style="float:right;margin-top:20px;" id="tblFilter" runat="server">
        <tr>
            <td>
                <qis:QISIntellisenseTextBox runat="server" ClientInstanceName="txtSearchViewPopup" ID="txtSearchViewPopup" Width="300px" Watermark="Search">
                    <ClientSideEvents SearchClick="function(s){ onTxtSearchViewPopupSearchClick(s); }" />
                </qis:QISIntellisenseTextBox>
            </td>
            <td>
                <dxe:ASPxComboBox ID="cboMigrationStatus" runat="server" Width="200px">
                    <ClientSideEvents ValueChanged="function(s,e){ onCboMigrationStatusValueChanged(s); }" />
                    <Items>
                        <dxe:ListEditItem Text="All" Value="-1" />
                        <dxe:ListEditItem Text="Transferred" Value="1" />
                        <dxe:ListEditItem Text="Trashed" Value="2" />
                        <dxe:ListEditItem Text="Failed" Value="3" />
                    </Items>
                </dxe:ASPxComboBox>
            </td>
        </tr>
    </table>
    <ul>
        <li id="btnMPListRestore" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbedit.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Restore")%></div></li>
    </ul>
</div>

<div style="position: relative;">
    <dxcp:ASPxCallbackPanel ID="cbpViewPopup" runat="server" Width="100%" ClientInstanceName="cbpViewPopup"
        ShowLoadingPanel="false" OnCallback="cbpViewPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ onCbpViewPopupEndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="height:300px">
                    <asp:GridView ID="grdViewPopup" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty" OnRowDataBound="grdViewPopup_RowDataBound">
                        <EmptyDataTemplate>
                            <%=GetLabel("No Data To Display")%>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <input id="chkSelectAllRestore" type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRestore" runat="server" CssClass="chkRestore" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
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

    <dxcp:ASPxCallbackPanel ID="cbpViewPopupRestore" runat="server" Width="100%" ClientInstanceName="cbpViewPopupRestore"
        ShowLoadingPanel="false" OnCallback="cbpViewPopupRestore_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
            EndCallback="function(s,e){ onCbpViewPopupRestoreEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</div>