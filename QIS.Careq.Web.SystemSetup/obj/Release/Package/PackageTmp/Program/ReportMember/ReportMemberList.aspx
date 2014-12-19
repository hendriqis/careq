<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPList.master" AutoEventWireup="true" 
CodeBehind="ReportMemberList.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.ReportMemberList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnReportMemberListSearch" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbsearch.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Search Member")%></div></li>
    <li id="btnReportMemberListPrint" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbprint.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Print")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        $(function () {
            setDatePicker('<%=txtFromCreatedDate.ClientID %>');
            $('#<%=txtFromCreatedDate.ClientID %>').datepicker('option', 'maxDate', '0');

            setDatePicker('<%=txtToCreatedDate.ClientID %>');
            $('#<%=txtToCreatedDate.ClientID %>').datepicker('option', 'maxDate', '0');

            $('#<%=btnReportMemberListSearch.ClientID %>').click(function () {
                pcSearchMember.Show();
            });

            $('#<%=btnReportMemberListPrint.ClientID %>').click(function () {
                openReportViewer('F000004', $('#<%=hdnFilterExpression.ClientID %>').val());
            });

            $('#<%=txtAddress.ClientID %>').keypress(function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) { // enter
                    $('#btnSearchMember').click();
                }
            });

            $('#<%=txtMemberName.ClientID %>').keypress(function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) { // enter
                    $('#btnSearchMember').click();
                }
            });

            $('#<%=txtCompany.ClientID %>').keypress(function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) { // enter
                    $('#btnSearchMember').click();
                }
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
                $('#<%=txtAddress.ClientID %>').val('');
                $('#<%=hdnLOBClassID.ClientID %>').val('');
                $('#<%=txtLOBClassCode.ClientID %>').val('');
                
                $('#<%=txtFromCreatedDate.ClientID %>').val('');
                $('#<%=txtToCreatedDate.ClientID %>').val(''); 
                onSearchClick('');
                pcSearchMember.Hide();
            });

            $('#btnSearchMember').click(function (evt) {
                if (IsValid(evt, 'fsSearchMember', 'mpEntryPopup')) {
                    var memberName = $('#<%=txtMemberName.ClientID %>').val();
                    var address = $('#<%=txtAddress.ClientID %>').val();
                    var company = $('#<%=txtCompany.ClientID %>').val();
                    var ratingLevel = $('#<%=txtRatingLevel.ClientID %>').val();
                    var occupationLevel = getCheckedOccupationLevelValue();
                    var certificate = getCheckedCertificateValue();
                    var department = getCheckedDepartmentValue();
                    var memberStatus = getCheckedMemberStatusValue();
                    var memberGreetings = getCheckedMemberGreetingsValue();

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
                    if (memberGreetings != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "MemberID IN (SELECT MemberID FROM MemberGreetings WHERE GCGreetingType IN (" + memberGreetings + "))";
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
                    if (address != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "(CompanyStreetName LIKE '%" + address + "%' OR CompanyCounty LIKE '%" + address + "%' OR CompanyDistrict LIKE '%" + address + "%' OR CompanyProvince LIKE '%" + address + "%' OR CompanyCity LIKE '%" + address + "%' OR CompanyZipCode LIKE '%" + address + "%')";
                    }
                    var LOBClass = $('#<%=hdnLOBClassID.ClientID %>').val();
                    if (LOBClass != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "(LOBClassID IN (" + LOBClass + "))";
                    }

                    var txtFromCreatedDateText = $('#<%=txtFromCreatedDate.ClientID %>').val();
                    var txtToCreatedDateText = $('#<%=txtToCreatedDate.ClientID %>').val();
                    if (txtFromCreatedDateText != '' && txtToCreatedDateText != '') {
                        var fromCreatedDate = Methods.getDatePickerDate(txtFromCreatedDateText);
                        var toCreatedDate = Methods.getDatePickerDate(txtToCreatedDateText);

                        filterExpression += "CreatedDate BETWEEN '" + Methods.dateToString(fromCreatedDate) + "' AND '" + Methods.dateToString(toCreatedDate) + "'";
                    }
                    onSearchClick(filterExpression);
                    pcSearchMember.Hide();
                }
                return false;
            });

            $('.chkOccupationLevel').change(function () {
                ddeOccupationLevel.SetText(getCheckedOccupationLevel());
            });

            $('.chkCertificate').change(function () {
                ddeCertificate.SetText(getCheckedCertificate());
            });

            $('.chkDepartment').change(function () {
                ddeDepartment.SetText(getCheckedDepartment());
            });

            $('.chkMemberStatus').change(function () {
                ddeMemberStatus.SetText(getCheckedMemberStatus());
            });

            $('.chkMemberGreetings').change(function () {
                ddeMemberGreetings.SetText(getCheckedMemberGreetings());
            });

            //#region LOB Class
            $('#lblLOBClass.lblLink').click(function () {
                var LOBClass = $('#<%=hdnLOBClassID.ClientID %>').val();
                var url = ResolveUrl("~/Program/ReportMember/LOBSearchCtl.ascx");
                openUserControlPopup(url, LOBClass, 'LOB Class', 1000, 600);
            });
            //#endregion
        });

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

        /* #region Member Greetings */
        function getCheckedMemberGreetingsValue() {
            var result = '';
            $('.chkMemberGreetings').each(function () {
                if ($(this).is(':checked')) {
                    if (result != "")
                        result += ",";
                    result += "'" + $(this).val() + "'";
                }
            });
            return result;
        }

        function getCheckedMemberGreetings() {
            var result = '';
            $('.chkMemberGreetings').each(function () {
                if ($(this).is(':checked')) {
                    if (result != "")
                        result += ", ";
                    result += $(this).next('span').html();
                }
            });
            return result;
        }
        /* #endregion */

        function setLOBClass(lobClassID, lobClassCode) {
            $('#<%=hdnLOBClassID.ClientID %>').val(lobClassID);
            $('#<%=txtLOBClassCode.ClientID %>').val(lobClassCode);
        }

        function onSearchClick(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

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
                                <asp:BoundField DataField="MemberNameWithTitle" HeaderText="Member Name" />
                                <asp:TemplateField HeaderStyle-Width="350px">
                                    <HeaderTemplate><%=GetLabel("Contact Information")%></HeaderTemplate>
                                    <ItemTemplate>  
                                        <table cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:130px"/>
                                                <col style="width:8px"/>
                                                <col style="width:200px"/>
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
                                            <tr>
                                                <td><%=GetLabel("Company Phone No")%></td>
                                                <td>:</td>
                                                <td class="tdValue"><%#Eval("CompanyPhoneNo1")%></td>
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
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" Height="400px">
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
                                            <asp:Repeater ID="rptCertificate" runat="server">
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
                                            <asp:Repeater ID="rptOccupationLevel" runat="server">
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
                                            <asp:Repeater ID="rptDepartment" runat="server">
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
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Member Greetings")%></label></td>
                            <td>
                                <dxe:ASPxDropDownEdit ClientInstanceName="ddeMemberGreetings" ID="ddeMemberGreetings"
                                    Width="300px" runat="server" EnableAnimation="False">
                                    <DropDownWindowTemplate>
                                        <div style="overflow-y: auto; max-height: 150px;">
                                            <asp:Repeater ID="rptMemberGreetings" runat="server">
                                                <ItemTemplate>
                                                    <div><input type="checkbox" id="chkMemberGreetings" runat="server" class="chkMemberGreetings" value='<%#Eval("StandardCodeID") %>' /><span><%#Eval("StandardCodeName") %></span></div>
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
                        <tr>
                            <td class="tdLabel" valign="top" style="padding-top:5px"><label class="lblNormal"><%=GetLabel("Company Address")%></label></td>
                            <td><asp:TextBox ID="txtAddress" Rows="2" TextMode="MultiLine" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Created Date")%></label></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:140px"/>
                                        <col style="width:3px"/>
                                        <col style="width:140px"/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtFromCreatedDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                                        <td><%=GetLabel("s/d") %></td>
                                        <td><asp:TextBox ID="txtToCreatedDate" Width="120px" CssClass="datepicker" runat="server" /></td>
                                    </tr>
                                </table>
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
