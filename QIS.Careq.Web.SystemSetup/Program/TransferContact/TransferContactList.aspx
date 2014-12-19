<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPList.master" AutoEventWireup="true" 
CodeBehind="TransferContactList.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.TransferContactList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnEventInvitationProcess" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbnew.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Transfer Contact")%></div></li>
    <li id="btnEventInvitationSearch" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbnew.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Search Member")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=btnEventInvitationProcess.ClientID %>').click(function () {
                getCheckedMember();
                if ($('#<%=hdnSelectedMember.ClientID %>').val() == '')
                    alert('Please Select Member First');
                else {
                    getCheckedMember();
                    $('#<%=btnExport.ClientID%>').click();
                    //cbpTransferContactProcess.PerformCallback('');
                }
            });

            $('#<%=btnEventInvitationSearch.ClientID %>').click(function () {
                pcSearchMember.Show();
            });

            $('#btnClearFilter').click(function () {
                $('#<%=txtLastName.ClientID %>').val('');
                $('#<%=txtFirstName.ClientID %>').val('');
                $('#<%=txtMiddleName.ClientID %>').val('');
                $('#<%=txtCompany.ClientID %>').val('');
                $('#<%=txtOccupation.ClientID %>').val('');
                $('#<%=txtRatingLevel.ClientID %>').val('');
                onSearchClick('');
                pcSearchMember.Hide();
            });

            $('#btnSearchMember').click(function (evt) {
                if (IsValid(evt, 'fsSearchMember', 'mpEntryPopup')) {
                    var firstName = $('#<%=txtFirstName.ClientID %>').val();
                    var middleName = $('#<%=txtMiddleName.ClientID %>').val();
                    var company = $('#<%=txtCompany.ClientID %>').val();
                    var occupation = $('#<%=txtOccupation.ClientID %>').val();
                    var ratingLevel = $('#<%=txtRatingLevel.ClientID %>').val();

                    var filterExpression = "LastName LIKE '%" + $('#<%=txtLastName.ClientID %>').val() + "%'";
                    if (firstName != '')
                        filterExpression += " AND FirstName LIKE '%" + firstName + "%'";
                    if (middleName != '')
                        filterExpression += " AND MiddleName LIKE '%" + middleName + "%'";
                    if (company != '')
                        filterExpression += " AND CompanyName LIKE '%" + company + "%'";
                    if (occupation != '')
                        filterExpression += " AND OccupationLevel LIKE '%" + occupation + "%'";
                    if (ratingLevel != '')
                        filterExpression += " AND RatingLevel >= " + ratingLevel;

                    onSearchClick(filterExpression);
                    pcSearchMember.Hide();
                }
                return false;
            });
        });

        function onSearchClick(filterExpression) {
            getCheckedMember();
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        function onRefreshControl(filterExpression) {
            $('#<%=hdnSelectedMember.ClientID %>').val('');
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        $('#chkSelectAllMember').die('change');
        $('#chkSelectAllMember').live('change', function () {
            var isChecked = $(this).is(":checked");
            $('.chkMember').each(function () {
                $(this).find('input').prop('checked', isChecked);
            });
        });

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        $(function () {
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
                getCheckedMember();
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

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                if (pageCount > 0)
                    $('#<%=grdView.ClientID %> tr:eq(1)').click();

                setPaging($("#paging"), pageCount, function (page) {
                    cbpView.PerformCallback('changepage|' + page);
                    getCheckedMember();
                });
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion

        function onCbpTransferContactProcessEndCallback(s) {
            var result = s.cpResult.split('|');
            if (result[0] == 'fail')
                alert('Transfer Contact Failed\nError Message : ' + result[1]);
            else {
                alert('Transfer Contact Success');
                onRefreshControl();
            }
            hideLoadingPanel();
        }
    </script>
    <style type="text/css">
        h5          { margin: 0; border-bottom: 1px solid #AAAAAA; font-weight: normal; font-size: 14px; }
        .ulRating li            { list-style-type: none; display: inline-block; }
        .ulRating               { margin: 0; padding: 0; }
        .starnotselected        { width: 16px; height: 16px; background:url(<%=ResolveUrl("~/Libs/Scripts/jquery/rating/star.gif")%>) no-repeat 0 0px; }
        .starselected           { width: 16px; height: 16px; background:url(<%=ResolveUrl("~/Libs/Scripts/jquery/rating/star.gif")%>) no-repeat 0 -32px; }
    </style>

    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnSelectedMember" runat="server" value="" />
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
                                <asp:TemplateField HeaderStyle-Width="350px">
                                    <HeaderTemplate><%=GetLabel("Contact Information")%></HeaderTemplate>
                                    <ItemTemplate>  
                                        <table cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:100px"/>
                                                <col style="width:8px"/>
                                            </colgroup>
                                            <tr>
                                                <td><%=GetLabel("Email Address")%></td>
                                                <td>:</td>
                                                <td class="tdValue"><%#Eval("cfEmailAddress")%></td>
                                            </tr>
                                            <tr>
                                                <td><%=GetLabel("Mobile No")%></td>
                                                <td>:</td>
                                                <td class="tdValue"><%#Eval("cfMobilePhoneNo")%></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="350px">
                                    <HeaderTemplate><%=GetLabel("Contact Information")%></HeaderTemplate>
                                    <ItemTemplate>  
                                        <%#Eval("CompanyName")%>
                                        <table cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:100px"/>
                                                <col style="width:8px"/>
                                            </colgroup>
                                            <tr>
                                                <td><%=GetLabel("Department")%></td>
                                                <td>:</td>
                                                <td class="tdValue"><%#Eval("Department")%></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="200px">
                                    <HeaderTemplate><%=GetLabel("Occupation")%></HeaderTemplate>
                                    <ItemTemplate>
                                        <div><%#Eval("Occupation") %></div>
                                        <table cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:100px"/>
                                                <col style="width:8px"/>
                                            </colgroup>
                                            <tr>
                                                <td><%=GetLabel("Occupation Level")%></td>
                                                <td>:</td>
                                                <td class="tdValue"><%#Eval("OccupationLevel")%></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                    <HeaderTemplate><%=GetLabel("Rating Level")%></HeaderTemplate>
                                    <ItemTemplate>
                                        <ul class="ulRating">
                                            <li><div id="divRating1" runat="server" class="starnotselected"></div></li>
                                            <li><div id="divRating2" runat="server" class="starnotselected"></div></li>
                                            <li><div id="divRating3" runat="server" class="starnotselected"></div></li>
                                            <li><div id="divRating4" runat="server" class="starnotselected"></div></li>
                                            <li><div id="divRating5" runat="server" class="starnotselected"></div></li>
                                        </ul>
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
     <dxpc:ASPxPopupControl ID="pcSearchMember" runat="server" ClientInstanceName="pcSearchMember" EnableHierarchyRecreation="True"
        FooterText="" HeaderText="Search Member" Modal="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" Width="600px"
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" Height="370px">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <h2 style="margin-bottom: 5px; margin-top: 0px;">Find Member</h2>
                <fieldset id="fsSearchMember" style="margin:0"> 
                    <h5>You must enter all of this member information:</h5>
                    <table class="tblEntryContent" style="width:100%;margin-bottom:15px;">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Last Name")%></label></td>
                            <td><asp:TextBox ID="txtLastName" CssClass="required" Width="300px" runat="server" /></td>
                        </tr>
                    </table>
                    <h5>Please try to enter at least one these items:</h5>
                    <table class="tblEntryContent" style="width:100%;margin-bottom:15px;">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("First Name")%></label></td>
                            <td><asp:TextBox ID="txtFirstName" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Middle Name")%></label></td>
                            <td><asp:TextBox ID="txtMiddleName" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Company")%></label></td>
                            <td><asp:TextBox ID="txtCompany" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Occupation Level")%></label></td>
                            <td><asp:TextBox ID="txtOccupation" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Rating Level")%></label></td>
                            <td>> &nbsp;<asp:TextBox ID="txtRatingLevel" CssClass="number" Width="100px" runat="server" /></td>
                        </tr>
                    </table>
                </fieldset>
                <div style="width:100%;text-align:center">
                    <table style="margin-left: auto; margin-right: auto; margin-top: 10px;">
                        <tr>
                            <td><input type="button" value='<%= GetLabel("Search")%>' style="width:70px" id="btnSearchMember" /></td>
                            <td><input type="button" value='<%= GetLabel("Close")%>' style="width:70px" onclick="pcSearchMember.Hide();" /></td>
                            <td><input type="button" value='<%= GetLabel("Clear Filter")%>' style="width:70px" id="btnClearFilter" /></td>
                        </tr>
                    </table>
                </div>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    
    <div style="display:none;">
        <asp:Button ID="btnExport" Visible="true" runat="server" OnClientClick="getCheckedMember();" OnClick="btnExport_Click" Text="Export" />
    </div>
</asp:Content>
