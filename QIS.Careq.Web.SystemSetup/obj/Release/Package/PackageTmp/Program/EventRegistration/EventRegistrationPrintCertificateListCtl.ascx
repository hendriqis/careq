<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventRegistrationPrintCertificateListCtl.ascx.cs" 
    Inherits="QIS.Careq.Web.SystemSetup.Program.EventRegistrationPrintCertificateListCtl" %>

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
            $('#<%=chkCheckAll.ClientID %>').prop('checked', false);
            $('#<%=chkUncheckAll.ClientID %>').prop('checked', false);
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

    function onRefreshControlPrintCertificate(filterExpression) {
        $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
        getCheckedMember();
        cbpPopup.PerformCallback('refresh');
    }

    $('#btnEventRegistrationPrintCertificationPrint').click(function () {
        getCheckedMember();
        var lstCheckedMember = $('#<%=hdnSelectedMember.ClientID %>').val().substring(1);
        if (lstCheckedMember == '')
            alert('Please Select Member First');
        else
            cbpGenerateCertificationNo.PerformCallback();
    });

    function getCheckedMember() {
        var lstSelectedMember = $('#<%=hdnSelectedMember.ClientID %>').val().split(';');
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
        $('#<%=hdnSelectedMember.ClientID %>').val(lstSelectedMember.join(';'));
    }

    $('#chkSelectAllMember').die('change');
    $('#chkSelectAllMember').live('change', function () {
        var isChecked = $(this).is(":checked");
        $('.chkMember').each(function () {
            $(this).find('input').prop('checked', isChecked);
        });
    });

    $('.txtRevisionNo').die('change');
    $('.txtRevisionNo').live('change', function () {
        var lstRevisionNoKey = $('#<%=hdnRevisionNoKey.ClientID %>').val().split(';');
        var lstRevisionNo = $('#<%=hdnRevisionNo.ClientID %>').val().split(';');

        var key = $(this).closest('tr').find('.keyField').html();
        var idx = lstRevisionNoKey.indexOf(key);
        if (idx > 0)
            lstRevisionNo[idx] = $(this).val();
        else {
            lstRevisionNoKey.push(key);
            lstRevisionNo.push($(this).val());
        }
        $('#<%=hdnRevisionNoKey.ClientID %>').val(lstRevisionNoKey.join(';'));
        $('#<%=hdnRevisionNo.ClientID %>').val(lstRevisionNo.join(';'));
    });
    

    function onCbpGenerateCertificationNoEndCallback(s) {
        var result = s.cpResult.split('|');
        if (result[0] == 'fail')
            alert('Generate Certification No Failed\nError Message : ' + result[1]);
        else {
            var lstCheckedMember = $('#<%=hdnSelectedMember.ClientID %>').val().substring(1);
            openReportViewer('F000001', $('#<%=hdnID.ClientID %>').val() + '|' + lstCheckedMember);
        }
        hideLoadingPanel();
    }

    $('.chkMember').die('change');
    $('.chkMember').live('change', function () {
        $('#<%=chkCheckAll.ClientID %>').prop('checked', false);
        $('#<%=chkUncheckAll.ClientID %>').prop('checked', false);
        $('#chkSelectAllMember').prop('checked', false);
    });

    $('#<%=chkCheckAll.ClientID %>').change(function () {
        var isChecked = $(this).is(":checked");
        if (isChecked) {
            $('#<%=hdnSelectedMember.ClientID %>').val($('#<%=hdnListAllMemberID.ClientID %>').val());
            $('.chkMember').each(function () {
                $(this).find('input').prop('checked', true);
            });
            $('#<%=chkUncheckAll.ClientID %>').prop('checked', false);
        }
    });

    $('#<%=chkUncheckAll.ClientID %>').change(function () {
        var isChecked = $(this).is(":checked");
        if (isChecked) {
            $('#<%=hdnSelectedMember.ClientID %>').val('');
            $('.chkMember').each(function () {
                $(this).find('input').prop('checked', false);
            });
            $('#<%=chkCheckAll.ClientID %>').prop('checked', false);
            $('#chkSelectAllMember').prop('checked', false);
        }
    });
</script>

<input type="hidden" id="hdnID" runat="server" value="" />
<input type="hidden" id="hdnFilterExpression" runat="server" value="" />
<input type="hidden" id="hdnSelectedMember" runat="server" value="" />
<input type="hidden" id="hdnRevisionNoKey" runat="server" value="" />
<input type="hidden" id="hdnRevisionNo" runat="server" value="" />
<input type="hidden" id="hdnIsRevisionNoChanged" runat="server" value="0" />

<div class="toolbarArea">
    <table style="float:right;margin-top:20px;" id="tblFilter" runat="server">
        <tr>
            <td>
                <qis:QISIntellisenseTextBox runat="server" ClientInstanceName="txtSearchViewPrintCertificate" ID="txtSearchViewPrintCertificate" Width="300px" Watermark="Search">
                    <ClientSideEvents SearchClick="function(s){ s.SetBlur(); onRefreshControlPrintCertificate(s.GenerateFilterExpression()); }" />
                    <IntellisenseHints>
                        <qis:QISIntellisenseHint FieldName="FirstName MiddleName LastName" Text="Name" />
                        <qis:QISIntellisenseHint FieldName="CompanyName" Text="Company" />
                        <qis:QISIntellisenseHint FieldName="OccupationLevel" Text="Occupation Level" />
                    </IntellisenseHints>
                </qis:QISIntellisenseTextBox>
            </td>
        </tr>
    </table>
    <ul>
        <li id="btnEventRegistrationPrintCertificationPrint"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbprint.png")%>' alt="" /><div><%=GetLabel("Print")%></div></li>
    </ul>
</div>
<div align="center">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td><asp:CheckBox ID="chkCheckAll" runat="server" Text="Check All"/></td>
            <td style="width:10px">&nbsp;</td>
            <td><asp:CheckBox ID="chkUncheckAll" runat="server" Text="Uncheck All"/></td>
        </tr>
    </table>
    
</div>
<div style="position: relative;">
    <dxcp:ASPxCallbackPanel ID="cbpPopup" runat="server" Width="100%" ClientInstanceName="cbpPopup"
        ShowLoadingPanel="false" OnCallback="cbpPopup_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ $('#containerImgLoadingViewPopup').show(); }"
            EndCallback="function(s,e){ onCbpPopupEndCallback(s); }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server">
                <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="height: 275px;">
                    <input type="hidden" id="hdnListAllMemberID" runat="server" value="0" />
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
                            <asp:BoundField DataField="OccupationLevel" HeaderText="Occupation" HeaderStyle-Width="120px" />
                            <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate><%=GetLabel("Revision No") %></HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRevisionNo" runat="server" Width="90%" CssClass="txtRevisionNo number" />
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
    <div class="imgLoadingGrdView" id="containerImgLoadingViewPopup" >
        <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
    </div>
    <div class="containerPaging">
        <div class="wrapperPaging">
            <div id="pagingPopup"></div>
        </div>
    </div> 
</div>

<dxcp:ASPxCallbackPanel ID="cbpGenerateCertificationNo" runat="server" Width="100%" ClientInstanceName="cbpGenerateCertificationNo"
    ShowLoadingPanel="false" OnCallback="cbpGenerateCertificationNo_Callback">
    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpGenerateCertificationNoEndCallback(s); }" />
</dxcp:ASPxCallbackPanel>