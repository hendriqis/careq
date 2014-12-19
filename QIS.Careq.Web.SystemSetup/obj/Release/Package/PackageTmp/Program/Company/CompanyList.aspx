<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="CompanyList.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.CompanyList" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnReportMemberListSearch" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbsearch.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Search")%></div></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/CustomGridViewList.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            setDatePicker('<%=txtFromCreatedDate.ClientID %>');
            $('#<%=txtFromCreatedDate.ClientID %>').datepicker('option', 'maxDate', '0');

            setDatePicker('<%=txtToCreatedDate.ClientID %>');
            $('#<%=txtToCreatedDate.ClientID %>').datepicker('option', 'maxDate', '0');

            $('#<%=btnReportMemberListSearch.ClientID %>').click(function () {
                pcSearchMember.Show();
            });

            $('#<%=txtAddress.ClientID %>').keypress(function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) { // enter
                    $('#btnSearchMember').click();
                }
            });

            $('#<%=txtCompanyName.ClientID %>').keypress(function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) { // enter
                    $('#btnSearchMember').click();
                }
            });

            $('#btnClearFilter').click(function () {
                $('#<%=txtCompanyName.ClientID %>').val('');
                $('#<%=txtAddress.ClientID %>').val('');
                $('#<%=hdnLOBClassID.ClientID %>').val('');
                $('#<%=txtLOBClassCode.ClientID %>').val('');
                $('#<%=txtFromCreatedDate.ClientID %>').val('');
                $('#<%=txtToCreatedDate.ClientID %>').val(''); 
                $('.chkCompanyType').each(function () {
                    $(this).prop('checked', false);
                });
                ddeCompanyType.SetText('');
                $('.chkCertificate').each(function () {
                    $(this).prop('checked', false);
                });
                ddeCertificate.SetText('');
                $('.chkCountryOfOrigin').each(function () {
                    $(this).prop('checked', false);
                });
                ddeCountryOfOrigin.SetText('');
                onSearchClick('');
                pcSearchMember.Hide();
            });

            $('#btnSearchMember').click(function (evt) {
                if (IsValid(evt, 'fsSearchMember', 'mpEntryPopup')) {
                    var companyName = $('#<%=txtCompanyName.ClientID %>').val();
                    var address = $('#<%=txtAddress.ClientID %>').val();
                    var companyType = getCheckedCompanyTypeValue();
                    var certificate = getCheckedCertificateValue();
                    var countryOfOrigin = getCheckedCountryOfOriginValue();

                    var filterExpression = "";
                    if (companyName != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "CompanyName LIKE '%" + companyName + "%'";
                    }
                    if (companyType != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "GCCompanyType IN (" + companyType + ")";
                    }
                    if (certificate != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "CompanyID IN (SELECT CompanyID FROM CompanyCertification WHERE GCCompanyCertification IN (" + certificate + "))";
                    }
                    if (countryOfOrigin != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "GCCountryOfOrigin IN (" + countryOfOrigin + ")";
                    }
                    if (address != '') {
                        if (filterExpression != "")
                            filterExpression += " AND ";
                        filterExpression += "(StreetName LIKE '%" + address + "%' OR County LIKE '%" + address + "%' OR District LIKE '%" + address + "%' OR Province LIKE '%" + address + "%' OR City LIKE '%" + address + "%' OR ZipCode LIKE '%" + address + "%')";
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

            function onSearchClick(filterExpression) {
                $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
                cbpView.PerformCallback('refresh');
            }

            $('.chkCompanyType').change(function () {
                ddeCompanyType.SetText(getCheckedCompanyType());
            });

            $('.chkCertificate').change(function () {
                ddeCertificate.SetText(getCheckedCertificate());
            });

            $('.chkCountryOfOrigin').change(function () {
                ddeCountryOfOrigin.SetText(getCheckedCountryOfOrigin());
            });

            var grd = new customGridView();
            grd.init('<%=grdView.ClientID %>', '<%=hdnID.ClientID %>', '<%=pnlView.ClientID %>', cbpView, 'paging');

            //#region LOB Class
            $('#lblLOBClass.lblLink').click(function () {
                var LOBClass = $('#<%=hdnLOBClassID.ClientID %>').val();
                var url = ResolveUrl("~/Program/ReportMember/LOBSearchCtl.ascx");
                openUserControlPopup(url, LOBClass, 'LOB Class', 1000, 600);
            });
            //#endregion
        });

        /* #region Company Type */
        function getCheckedCompanyTypeValue() {
            var result = '';
            $('.chkCompanyType').each(function () {
                if ($(this).is(':checked')) {
                    if (result != "")
                        result += ",";
                    result += "'" + $(this).val() + "'";
                }
            });
            return result;
        }

        function getCheckedCompanyType() {
            var result = '';
            $('.chkCompanyType').each(function () {
                if ($(this).is(':checked')) {
                    if (result != "")
                        result += ", ";
                    result += $(this).next('span').html();
                }
            });
            return result;
        }
        /* #endregion */

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

        /* #region Country Of Origin */
        function getCheckedCountryOfOriginValue() {
            var result = '';
            $('.chkCountryOfOrigin').each(function () {
                if ($(this).is(':checked')) {
                    if (result != "")
                        result += ",";
                    result += "'" + $(this).val() + "'";
                }
            });
            return result;
        }

        function getCheckedCountryOfOrigin() {
            var result = '';
            $('.chkCountryOfOrigin').each(function () {
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

        function onRefreshControl(filterExpression) {
            $('#<%=hdnFilterExpression.ClientID %>').val(filterExpression);
            cbpView.PerformCallback('refresh');
        }

        function onGetCurrID() {
            return $('#<%=hdnID.ClientID %>').val();
        }

        function onGetFilterExpression() {
            return $('#<%=hdnFilterExpression.ClientID %>').val();
        }

        $('.lnkCertification a').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            openMatrixControl('CompanyCertification', id, 'Company Certification');
        });

        $('.lnkContactPerson a').live('click', function () {
            var id = $(this).closest('tr').find('.keyField').html();
            var url = ResolveUrl("~/Program/Company/CompanyContactPersonEntryCtl.ascx");
            openUserControlPopup(url, id, 'Company Contact Person', 600, 500);
        });


        //#region Paging
        var pageCount = parseInt('<%=PageCount %>');
        var currPage = parseInt('<%=CurrPage %>');
        var rowCount = parseInt('<%=RowCount %>');
        $(function () {
            $('#spnTotalEntity').html(rowCount);
            setPaging($("#paging"), pageCount, function (page) {
                cbpView.PerformCallback('changepage|' + page);
            }, null, currPage);
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                var rowCount = parseInt(param[2]);
                $('#spnTotalEntity').html(rowCount);
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

        $('.lblWebsiteUrl').live('click', function () {
            var url = $(this).html();
            if (url.indexOf('http') < 0)
                url = 'http://' + url;
            window.open(url, '_blank');
        });
    </script>
    <style type="text/css">
        .grdSelected td         { vertical-align:top; }
        .ulRating li            { list-style-type: none; display: inline-block; }
        .ulRating               { margin: 0; padding: 0; }
        .starnotselected        { width: 16px; height: 16px; background:url(<%=ResolveUrl("~/Libs/Scripts/jquery/rating/star.gif")%>) no-repeat 0 0px; }
        .starselected           { width: 16px; height: 16px; background:url(<%=ResolveUrl("~/Libs/Scripts/jquery/rating/star.gif")%>) no-repeat 0 -32px; }
    </style>
    <input type="hidden" value="" id="hdnID" runat="server" />
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
                                <asp:BoundField DataField="CompanyID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                <asp:BoundField DataField="CompanyCode" HeaderText="Code" HeaderStyle-Width="80px" />
                                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-Width="200px" />
                                <asp:TemplateField HeaderStyle-Width="300px" ItemStyle-VerticalAlign="Top">
                                    <HeaderTemplate><%=GetLabel("Company Type")%></HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="padding:0 3px 3px 3px">
                                            <div><%# Eval("cfCompanyType")%></div>
                                            <div style="color:#5200FD"><%# Eval("LOBClassName")%></div>
                                            <div><%# Eval("cfHoldingCompanyName")%></div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate><%=GetLabel("Address")%></HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="padding:0 3px 3px 3px">
                                            <div><%# Eval("Address")%></div>
                                            <div style="float:left; margin-left:20px;" /><%=GetLabel("Phone No")%></div><div style="margin-left:100px;color:#5200FD">: <%# Eval("cfPhoneNo")%></div>
                                            <div style="float:left; margin-left:20px;" /><%=GetLabel("Website URL")%></div><div style="margin-left:100px">: <label class="lblLink lblWebsiteUrl"><%# Eval("WebsiteUrl")%></label></div>                                                  
                                        </div>
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
                                <asp:HyperLinkField HeaderText="Certification" Text="Certification" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkCertification" HeaderStyle-Width="100px" />
                                <asp:HyperLinkField HeaderText="Contact Person" Text="Contact Person" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkContactPerson" HeaderStyle-Width="100px" />
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
        <%=GetLabel("Total Record")%> : <span id="spnTotalEntity"></span>
    </div>

    <dxpc:ASPxPopupControl ID="pcSearchMember" runat="server" ClientInstanceName="pcSearchMember" EnableHierarchyRecreation="True"
        FooterText="" HeaderText="Search Member" Modal="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" Width="600px"
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" Height="450px">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <h2 style="margin-bottom: 5px; margin-top: 0px;">Find Company</h2>
                <fieldset id="fsSearchMember" style="margin:0"> 
                    <h5>Please try to enter at least one these items:</h5>
                    <table class="tblEntryContent" style="width:100%;margin-bottom:15px;">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Company Name")%></label></td>
                            <td><asp:TextBox ID="txtCompanyName" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Company Type")%></label></td>
                            <td>
                                <dxe:ASPxDropDownEdit ClientInstanceName="ddeCompanyType" ID="ddeCompanyType"
                                    Width="300px" runat="server" EnableAnimation="False">
                                    <DropDownWindowTemplate>
                                        <div style="overflow-y: auto; max-height: 150px;">
                                            <asp:Repeater ID="rptCompanyType" runat="server">
                                                <ItemTemplate>
                                                    <div><input type="checkbox" id="chkCompanyType" runat="server" class="chkCompanyType" value='<%#Eval("StandardCodeID") %>' /><span><%#Eval("StandardCodeName") %></span></div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </DropDownWindowTemplate>
                                </dxe:ASPxDropDownEdit>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Country Of Origin")%></label></td>
                            <td>
                                <dxe:ASPxDropDownEdit ClientInstanceName="ddeCountryOfOrigin" ID="ddeCountryOfOrigin"
                                    Width="300px" runat="server" EnableAnimation="False">
                                    <DropDownWindowTemplate>
                                        <div style="overflow-y: auto; max-height: 150px;">
                                            <asp:Repeater ID="rptCountryOfOrigin" runat="server">
                                                <ItemTemplate>
                                                    <div><input type="checkbox" id="chkCountryOfOrigin" runat="server" class="chkCountryOfOrigin" value='<%#Eval("StandardCodeID") %>' /><span><%#Eval("StandardCodeName") %></span></div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </DropDownWindowTemplate>
                                </dxe:ASPxDropDownEdit>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblLink" id="lblLOBClass"><%=GetLabel("LOB Type")%></label></td>
                            <td>
                                <input type="hidden" value="" id="hdnLOBClassID" runat="server" />
                                <asp:TextBox ID="txtLOBClassCode" ReadOnly="true" Width="100%" runat="server" />
                            </td>
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
                            <td class="tdLabel" valign="top" style="padding-top:5px"><label class="lblNormal"><%=GetLabel("Address")%></label></td>
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