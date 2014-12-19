<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPList.master" AutoEventWireup="true" 
CodeBehind="EventInvitationDt.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.EventInvitationDt" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnEventInvitationBack" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/back.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("View")%></div></li>
    <li id="btnEventInvitationProcess" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbsendmail.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Send Mail")%></div></li>
    <li id="btnEventInvitationInvitedView" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbnew.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Invited Member")%></div></li>
    <li id="btnEventInvitationSearch" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbsearch.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Search Member")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=btnEventInvitationProcess.ClientID %>').click(function () {
                getCheckedMember();
                if ($('#<%=hdnSelectedMember.ClientID %>').val() == '')
                    alert('Please Select Member First');
                else {
                    var id = $('#<%=hdnID.ClientID %>').val() + '|' + $('#<%=hdnSelectedMember.ClientID %>').val();
                    var url = ResolveUrl("~/Program/EventInvitation/EventInvitationSendEmailCtl.ascx");
                    openUserControlPopup(url, id, 'Send Email', 700, 500);
                }
            });

            $('#<%=btnEventInvitationBack.ClientID %>').click(function () {
                document.location = ResolveUrl('~/Program/EventInvitation/EventInvitationList.aspx');
            });

            $('#<%=btnEventInvitationInvitedView.ClientID %>').click(function () {
                var id = $('#<%=hdnID.ClientID %>').val();
                var url = ResolveUrl("~/Program/EventInvitation/EventInvitationInvitedMemberViewCtl.ascx");
                openUserControlPopup(url, id, 'Invited Member', 750, 500);
            });

            $('#<%=btnEventInvitationSearch.ClientID %>').click(function () {
                pcSearchMember.Show();
            });

            $('#btnClearFilter').click(function () {
                $('#<%=txtMemberName.ClientID %>').val('');
                $('#<%=txtCompany.ClientID %>').val('');
                $('#<%=txtRatingLevel.ClientID %>').val('');
                $('.chkOccupationLevel').each(function () {
                    $(this).prop('checked', false);
                });
                ddeOccupationLevel.SetText('');
                $('.chkCertificate').each(function () {
                    $(this).prop('checked', false);
                });
                ddeCertificate.SetText('');
                $('.chkDepartment').each(function () {
                    $(this).prop('checked', false);
                });
                ddeDepartment.SetText('');
                onSearchClick('');
                pcSearchMember.Hide();
            });

            $('#btnSearchMember').click(function (evt) {
                if (IsValid(evt, 'fsSearchMember', 'mpEntryPopup')) {
                    var memberName = $('#<%=txtMemberName.ClientID %>').val();
                    var company = $('#<%=txtCompany.ClientID %>').val();
                    var ratingLevel = $('#<%=txtRatingLevel.ClientID %>').val();
                    var occupationLevel = getCheckedOccupationLevelValue();
                    var certificate = getCheckedCertificateValue();
                    var department = getCheckedDepartmentValue();
                    var memberStatus = getCheckedMemberStatusValue();

                    var filterExpression = "";
                    if (memberName != '')
                        filterExpression += "(FirstName LIKE '%" + memberName + "%' OR MiddleName LIKE '%" + memberName + "%' OR LastName LIKE '%" + memberName + "%')";
                    if (company != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "CompanyName LIKE '%" + company + "%'";
                    }
                    if (occupationLevel != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "GCOccupationLevel IN (" + occupationLevel + ")";
                    }
                    if (department != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "GCDepartment IN (" + department + ")";
                    }
                    if (memberStatus != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "GCMemberStatus IN (" + memberStatus + ")";
                    }
                    if (certificate != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "CompanyID IN (SELECT CompanyID FROM CompanyCertification WHERE GCCompanyCertification IN (" + certificate + "))";
                    }
                    if (ratingLevel != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "RatingLevel >= " + ratingLevel;
                    }
                    if ($('#chkIsCSClub').is(':checked')) {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "IsCSClub = 1";
                    }
                    if ($('#chkIsHRDClub').is(':checked')) {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "IsHRDClub = 1";
                    }
                    if ($('#chkIsISOClub').is(':checked')) {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "IsISOClub = 1";
                    }
                    if ($('#chkIsCompanyContactPerson').is(':checked')) {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "IsCompanyContactPerson = 1";
                    }

                    var LOBClass = $('#<%=hdnLOBClassID.ClientID %>').val();
                    if (LOBClass != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "(LOBClassID IN (" + LOBClass + "))";
                    }

                    onSearchClick(filterExpression);
                    pcSearchMember.Hide();
                }
                return false;
            });

            $('.chkOccupationLevel').change(function () {
                ddeOccupationLevel.SetText(getCheckedOccupationLevel());
            });

            $('.chkDepartment').change(function () {
                ddeDepartment.SetText(getCheckedDepartment());
            });

            $('.chkCertificate').change(function () {
                ddeCertificate.SetText(getCheckedCertificate());
            });

            $('.chkMemberStatus').change(function () {
                ddeMemberStatus.SetText(getCheckedMemberStatus());
            });

            //#region LOB Class
            $('#lblLOBClass.lblLink').click(function () {
                var LOBClass = $('#<%=hdnLOBClassID.ClientID %>').val();
                var url = ResolveUrl("~/Program/ReportMember/LOBSearchCtl.ascx");
                openUserControlPopup(url, LOBClass, 'LOB Class', 1000, 600);
            });
            //#endregion
        });

        function setLOBClass(lobClassID, lobClassCode) {
            $('#<%=hdnLOBClassID.ClientID %>').val(lobClassID);
            $('#<%=txtLOBClassCode.ClientID %>').val(lobClassCode);
        }

        /* #region Certificate */
        function getCheckedCertificateValue() {
            var result = '';
            $('.chkCertificate').each(function () {
                if ($(this).is(':checked')) {
                    if (result != "")
                        result += ",";
                    result += "'" + $(this).val() + "'";
                }
            });
            return result;
        }

        function getCheckedCertificate() {
            var result = '';
            $('.chkCertificate').each(function () {
                if ($(this).is(':checked')) {
                    if (result != "")
                        result += ", ";
                    result += $(this).next('span').html();
                }
            });
            return result;
        }
        /* #endregion */

        /* #region Occupation Level */
        function getCheckedOccupationLevelValue() {
            var result = '';
            $('.chkOccupationLevel').each(function () {
                if ($(this).is(':checked')) {
                    if (result != "")
                        result += ",";
                    result += "'" + $(this).val() + "'";
                }
            });
            return result;
        }

        function getCheckedOccupationLevel() {
            var result = '';
            $('.chkOccupationLevel').each(function () {
                if ($(this).is(':checked')) {
                    if (result != "")
                        result += ", ";
                    result += $(this).next('span').html();
                }
            });
            return result;
        }
        /* #endregion */

        /* #region Department */
        function getCheckedDepartmentValue() {
            var result = '';
            $('.chkDepartment').each(function () {
                if ($(this).is(':checked')) {
                    if (result != "")
                        result += ",";
                    result += "'" + $(this).val() + "'";
                }
            });
            return result;
        }

        function getCheckedDepartment() {
            var result = '';
            $('.chkDepartment').each(function () {
                if ($(this).is(':checked')) {
                    if (result != "")
                        result += ", ";
                    result += $(this).next('span').html();
                }
            });
            return result;
        }
        /* #endregion */

        /* #region Member Status */
        function getCheckedMemberStatusValue() {
            var result = '';
            $('.chkMemberStatus').each(function () {
                if ($(this).is(':checked')) {
                    if (result != "")
                        result += ",";
                    result += "'" + $(this).val() + "'";
                }
            });
            return result;
        }

        function getCheckedMemberStatus() {
            var result = '';
            $('.chkMemberStatus').each(function () {
                if ($(this).is(':checked')) {
                    if (result != "")
                        result += ", ";
                    result += $(this).next('span').html();
                }
            });
            return result;
        }
        /* #endregion */

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

        function onGetCurrID() {
            return $('#<%=hdnID.ClientID %>').val();
        }

        function onGetFilterExpression() {
            return $().val('#<%=hdnFilterExpression.ClientID %>');
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

        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var currPage = parseInt('<%=CurrPage %>');
        $(function () {
            setPaging($("#paging"), pageCount, function (page) {
                getCheckedMember();
                cbpView.PerformCallback('changepage|' + page);
            }, null, currPage);
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
                    getCheckedMember();
                    cbpView.PerformCallback('changepage|' + page);
                }, null, currPage);
            }
            else
                $('#<%=grdView.ClientID %> tr:eq(1)').click();
        }
        //#endregion
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
                    <h5>Please try to enter at least one these items:</h5>
                    <table class="tblEntryContent" style="width:100%;margin-bottom:15px;">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Member Name")%></label></td>
                            <td><asp:TextBox ID="txtMemberName" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Certificate")%></label></td>
                            <td>
                                <dxe:ASPxDropDownEdit ClientInstanceName="ddeCertificate" ID="ddeCertificate"
                                    Width="300px" runat="server" EnableAnimation="False">
                                    <DropDownWindowTemplate>
                                        <div style="overflow-y: auto; max-height: 150px;">
                                            <asp:Repeater ID="rptCertificate" runat="server" OnItemDataBound="rptCertificate_ItemDataBound">
                                                <ItemTemplate>
                                                    <div><input type="checkbox" id="chkCertificate" runat="server" class="chkCertificate" value='<%#Eval("StandardCodeID") %>' /><span><%#Eval("StandardCodeName") %></span></div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </DropDownWindowTemplate>
                                </dxe:ASPxDropDownEdit>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Company")%></label></td>
                            <td><asp:TextBox ID="txtCompany" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Occupation Level")%></label></td>
                            <td>
                                <dxe:ASPxDropDownEdit ClientInstanceName="ddeOccupationLevel" ID="ddeOccupationLevel"
                                    Width="300px" runat="server" EnableAnimation="False">
                                    <DropDownWindowTemplate>
                                        <div style="overflow-y: auto; max-height: 150px;">
                                            <asp:Repeater ID="rptOccupationLevel" runat="server" OnItemDataBound="rptOccupationLevel_ItemDataBound">
                                                <ItemTemplate>
                                                    <div><input type="checkbox" id="chkOccupationLevel" runat="server" class="chkOccupationLevel" value='<%#Eval("StandardCodeID") %>' /><span><%#Eval("StandardCodeName") %></span></div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </DropDownWindowTemplate>
                                </dxe:ASPxDropDownEdit>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Department")%></label></td>
                            <td>
                                <dxe:ASPxDropDownEdit ClientInstanceName="ddeDepartment" ID="ddeDepartment"
                                    Width="300px" runat="server" EnableAnimation="False">
                                    <DropDownWindowTemplate>
                                        <div style="overflow-y: auto; max-height: 150px;">
                                            <asp:Repeater ID="rptDepartment" runat="server" OnItemDataBound="rptDepartment_ItemDataBound">
                                                <ItemTemplate>
                                                    <div><input type="checkbox" id="chkDepartment" runat="server" class="chkDepartment" value='<%#Eval("StandardCodeID") %>' /><span><%#Eval("StandardCodeName") %></span></div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </DropDownWindowTemplate>
                                </dxe:ASPxDropDownEdit>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Member Status")%></label></td>
                            <td>
                                <dxe:ASPxDropDownEdit ClientInstanceName="ddeMemberStatus" ID="ddeMemberStatus"
                                    Width="300px" runat="server" EnableAnimation="False">
                                    <DropDownWindowTemplate>
                                        <div style="overflow-y: auto; max-height: 150px;">
                                            <asp:Repeater ID="rptMemberStatus" runat="server">
                                                <ItemTemplate>
                                                    <div><input type="checkbox" id="chkMemberStatus" runat="server" class="chkMemberStatus" value='<%#Eval("StandardCodeID") %>' /><span><%#Eval("StandardCodeName") %></span></div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </DropDownWindowTemplate>
                                </dxe:ASPxDropDownEdit>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Rating Level")%></label></td>
                            <td>> &nbsp;<asp:TextBox ID="txtRatingLevel" CssClass="number" Width="100px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblLOBClass"><%=GetLabel("LOB Type")%></label></td>
                            <td>
                                <input type="hidden" value="" id="hdnLOBClassID" runat="server" />
                                <asp:TextBox ID="txtLOBClassCode" ReadOnly="true" Width="100%" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Other Information")%></label></td>
                            <td>
                                <dxe:ASPxDropDownEdit ClientInstanceName="ddeOtherInformation" ID="ddeOtherInformation"
                                    Width="300px" runat="server" EnableAnimation="False">
                                    <DropDownWindowTemplate>
                                        <div style="overflow-y: auto; max-height: 150px;">
                                            <div><input type="checkbox" id="chkIsCSClub" /><span><%=GetLabel("Is CS Club") %></span></div>
                                            <div><input type="checkbox" id="chkIsHRDClub" /><span><%=GetLabel("Is HRD Club") %></span></div>
                                            <div><input type="checkbox" id="chkIsISOClub" /><span><%=GetLabel("Is ISO Club") %></span></div>
                                            <div><input type="checkbox" id="chkIsCompanyContactPerson" /><span><%=GetLabel("Is Company Contact Person")%></span></div>
                                        </div>
                                    </DropDownWindowTemplate>
                                </dxe:ASPxDropDownEdit>
                            </td>
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
</asp:Content>
