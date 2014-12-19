<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="UploadEventParticipantDt2.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.UploadEventParticipantDt2" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnUploadEventParticipantBack" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/back.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("View")%></div></li>
    <li id="btnUploadEventParticipantChooseFile" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/back.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Choose File")%></div></li>
    <li id="btnUploadEventParticipantSave" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/tbsave.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Save")%></div></li>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">   
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/ui/jquery.ui.core.js")%>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/ui/jquery.ui.widget2.js")%>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/ui/jquery.ui.mouse.js")%>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/ui/jquery.ui.draggable.js")%>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/ui/jquery.ui.droppable.js")%>'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~/Libs/Scripts/jquery/ui/jquery.effects.core.js")%>'></script>
    <script type="text/javascript">
        $(function () {
            $('#<%=btnUploadEventParticipantChooseFile.ClientID %>').click(function () {
                document.getElementById('<%= FileUpload1.ClientID %>').click();
            });

            $('#<%=FileUpload1.ClientID %>').change(function () {
                if (this.files && this.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('#<%=hdnUploadedFile1.ClientID %>').val(e.target.result);
                        $('#previewImage').attr('src', e.target.result);
                        cbpView.PerformCallback();
                    }

                    reader.readAsDataURL(this.files[0]);
                }
            });

            $('#<%=btnUploadEventParticipantBack.ClientID %>').click(function () {
                document.location = ResolveUrl('~/Program/UploadEventParticipant/UploadEventParticipantList.aspx');
            });

            $('#<%=btnUploadEventParticipantSave.ClientID %>').click(function () {
                $('#<%=hdnSelectedMember.ClientID %>').val(arrayMatrixRegisteredMember.join('|'));
                //alert($('#<%=hdnSelectedMember.ClientID %>').val());
                cbpProcess.PerformCallback('save');
            });
        });

        function addFilterRow() {
            $trHeader = $('#<%=grdViewDt.ClientID %> tr:eq(0)');
            $trFilter = $("<tr><td></td><td></td><td></td><td></td><td></td></tr>");
            $input1 = $("<input type='text' id='txtDtFilterCode' style='width:100%' />").val($('#<%=hdnSearchTextCode.ClientID %>').val());
            $input2 = $("<input type='text' id='txtDtFilterName' style='width:100%' />").val($('#<%=hdnSearchTextName.ClientID %>').val());
            $input3 = $("<input type='text' id='txtDtFilterEmail' style='width:100%' />").val($('#<%=hdnSearchTextEmail.ClientID %>').val());
            $input4 = $("<input type='text' id='txtDtFilterMobilePhone' style='width:100%' />").val($('#<%=hdnSearchTextMobilePhone.ClientID %>').val());
            $trFilter.find('td:eq(0)').append($input1);
            $trFilter.find('td:eq(1)').append($input2);
            $trFilter.find('td:eq(2)').append($input3);
            $trFilter.find('td:eq(3)').append($input4);


            $trFilter.insertAfter($trHeader);

            $('#txtDtFilterCode').keypress(function (e) {
                onKeyPress(this, e, '<%=hdnSearchTextCode.ClientID %>');
            });
            $('#txtDtFilterName').keypress(function (e) {
                onKeyPress(this, e, '<%=hdnSearchTextName.ClientID %>');
            });
            $('#txtDtFilterEmail').keypress(function (e) {
                onKeyPress(this, e, '<%=hdnSearchTextEmail.ClientID %>');
            });
            $('#txtDtFilterMobilePhone').keypress(function (e) {
                onKeyPress(this, e, '<%=hdnSearchTextMobilePhone.ClientID %>');
            });
        }

        function onBeforeChangePage() {
            $('#<%=hdnSearchTextCode.ClientID %>').val($('#txtDtFilterCode').val());
            $('#<%=hdnSearchTextName.ClientID %>').val($('#txtDtFilterName').val());
            $('#<%=hdnSearchTextEmail.ClientID %>').val($('#txtDtFilterEmail').val());
            $('#<%=hdnSearchTextMobilePhone.ClientID %>').val($('#txtDtFilterMobilePhone').val());
        }

        function onKeyPress(elm, e, elmHdnID) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13) {
                $('#' + elmHdnID).val($(elm).val());
                e.preventDefault();
                cbpViewDt.PerformCallback('refresh');
            }
        }

        //#region Uploaded Member
        var pageCount = parseInt('<%=PageCount %>');
        $(function () {
            setPaging($("#pagingDt"), pageCount, function (page) {
                cbpViewDt.PerformCallback('changepage|' + page);
            });
            setTrDtDroppable();
        });

        function onCbpViewEndCallback(s) {
            $('.trDraggable').each(function () {
                $(this).width($(this).parent().width());
            });
            $('.trDraggable').draggable({
                helper: 'clone',
                drag: function (event, ui) {
                    //insideDropZone = false;
                }
            });
            hideLoadingPanel();
            //arrayMatrixRegisteredMember[$('.trDraggable').length - 1] = undefined;
            arrayMatrixRegisteredMember = new Array($('.trDraggable').length);
            cbpProcess.PerformCallback('map');
        }
        //#endregion

        var arrayMatrixRegisteredMember = [];

        //#region Paging Dt
        function onCbpViewDtEndCallback(s) {
            $('#containerImgLoadingViewDt').hide();

            var param = s.cpResult.split('|');
            if (param[0] == 'refresh') {
                var pageCount = parseInt(param[1]);
                if (pageCount > 0)
                    $('#<%=grdViewDt.ClientID %> tr:eq(1)').click();

                setPaging($("#pagingDt"), pageCount, function (page) {
                    onBeforeChangePage();
                    cbpViewDt.PerformCallback('changepage|' + page);
                });

                if (isCompanyChanged) {
                    isCompanyChanged = false;
                    if ($('.trDraggable').length > 0)
                        cbpProcess.PerformCallback('map');
                }
            }
            else
                $('#<%=grdViewDt.ClientID %> tr:eq(1)').click();
            setTrDtDroppable();
            addFilterRow();
            setTimeout(function () {
                mapArrayToGrid();
            }, 0);
        }

        function mapArrayToGrid() {
            for (var i = 0; i < arrayMatrixRegisteredMember.length; ++i) {
                if (arrayMatrixRegisteredMember[i] !== undefined) {
                    $td = null;

                    $('#<%=grdViewDt.ClientID %> .keyField:gt(0)').each(function () {
                        if ($(this).html() == arrayMatrixRegisteredMember[i])
                            $td = $(this);
                    });
                    if ($td != null) {
                        $lastTd = $td.parent().find('td').last();

                        $tr = $('.trDraggable:eq(' + i + ')');

                        $lastTd.html($tr.find('td:eq(2)').html());
                        $lastTd.append($('<img class="imgDeleteRegisteredMember"></img>').attr('src', ResolveUrl("~/Libs/Images/Button/delete.png")));
                        $lastTd.append($('<input type="hidden"></input').val(i + 1));

                        $lastTd.draggable({
                            helper: 'clone',
                            drag: function (event, ui) {
                                //insideDropZone = false;
                            }
                        });
                        $lastTd.draggable('enable');
                    }
                }
            }
        }

        function setTrDtDroppable() {
            $('#<%=grdViewDt.ClientID %> tr').droppable({
                drop: function (event, ui) {
                    var $elmn = ui.helper.clone();
                    if ($elmn.html().replace(' ', '') != '') {
                        if ($elmn[0].nodeName == 'TR') {
                            $tr = $elmn;
                            $lastTd = $(this).find('td').last();

                            var keyField = $tr.find('td').first().html();
                            $input = $('#<%=grdViewDt.ClientID %> input[value=' + keyField + ']');
                            if ($input.length > 0) {
                                $input.closest('td').empty();
                            }

                            $lastTd.html($tr.find('td:eq(2)').html());
                            $lastTd.append($('<img class="imgDeleteRegisteredMember"></img>').attr('src', ResolveUrl("~/Libs/Images/Button/delete.png")));
                            $lastTd.append($('<input type="hidden"></input').val(keyField));

                            arrayMatrixRegisteredMember[parseInt(keyField) - 1] = $(this).find('td').first().html();

                            $lastTd.draggable({
                                helper: 'clone',
                                drag: function (event, ui) {
                                    //insideDropZone = false;
                                }
                            });
                            $lastTd.draggable('enable');
                        }
                        else if ($elmn[0].nodeName == 'TD') {
                            $lastTd = $(this).find('td').last();
                            $(ui.draggable).empty();
                            $(ui.draggable).html($lastTd.html());
                            if ($lastTd.html() == '&nbsp;') {
                                $(ui.draggable).draggable('disable');
                            }
                            else {
                                var tempKeyField = $(ui.draggable).find('input').val();
                                arrayMatrixRegisteredMember[parseInt(tempKeyField) - 1] = $(ui.draggable).closest('tr').find('td').first().html();
                            }

                            var keyField = $elmn.find('input').val();
                            arrayMatrixRegisteredMember[parseInt(keyField) - 1] = $(this).find('td').first().html();
                            $lastTd.html($elmn.html());

                            $lastTd.draggable({
                                helper: 'clone',
                                drag: function (event, ui) {
                                    //insideDropZone = false;
                                }
                            });
                            $lastTd.draggable('enable');

                        }
                    }
                }
            });
        }
        //#endregion

        $('.imgDeleteRegisteredMember').live('click', function () {
            if (confirm("Are You Sure?")) {
                $td = $(this).closest('td');

                var keyField = $td.find('input').val();
                arrayMatrixRegisteredMember[parseInt(keyField) - 1] = '';
                $td.empty();
            }
        });

        function onCbpProcessEndCallback(s) {
            var result = s.cpResult.split('|');
            if (result[0] == 'save') {
                if (result[1] == 'fail')
                    alert('Registration Failed\nError Message : ' + result[2]);
                else {
                    alert('Registration Success!');
                    $('#<%=btnUploadEventParticipantBack.ClientID %>').click();
                }
            }
            else {
                arrayMatrixRegisteredMember = result[1].split(';');
                mapArrayToGrid();
            }
            hideLoadingPanel();
        }

        //#region Company
        $('#lblCompany.lblLink').live('click', function () {
            openSearchDialog('company', 'IsDeleted = 0', function (value) {
                $('#<%=txtCompanyCode.ClientID %>').val(value);
                onTxtCompanyCodeChanged(value);
            });
        });

        $('#<%=txtCompanyCode.ClientID %>').live('change', function () {
            onTxtCompanyCodeChanged($(this).val());
        });
        var isCompanyChanged = false;
        function onTxtCompanyCodeChanged(value) {
            var filterExpression = "CompanyCode = '" + value + "' AND IsDeleted = 0";
            Methods.getObject('GetCompanyList', filterExpression, function (result) {
                if (result != null) {
                    $('#<%=hdnCompanyID.ClientID %>').val(result.CompanyID);
                    $('#<%=txtCompanyName.ClientID %>').val(result.CompanyName);
                }
                else {
                    $('#<%=hdnCompanyID.ClientID %>').val('');
                    $('#<%=txtCompanyName.ClientID %>').val('');
                }
                isCompanyChanged = true;

                $('#<%=hdnSearchTextCode.ClientID %>').val('');
                $('#<%=hdnSearchTextName.ClientID %>').val('');
                $('#<%=hdnSearchTextEmail.ClientID %>').val('');
                $('#<%=hdnSearchTextMobilePhone.ClientID %>').val('');
                arrayMatrixRegisteredMember = new Array($('.trDraggable').length);
                cbpViewDt.PerformCallback('refresh');
            });
        }
        //#endregion
    </script>
    <style type="text/css">
        .imgDeleteRegisteredMember
        {
            float: right;
            height: 16px;
            cursor: pointer;
        }
        .grdSelected td     { padding: 2px; }
    </style>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnSelectedMember" runat="server" value="" />
    <input type="hidden" id="hdnUploadedFile1" runat="server" value="" />

    <input type="hidden" id="hdnSearchTextName" runat="server" value="" />
    <input type="hidden" id="hdnSearchTextEmail" runat="server" value="" />
    <input type="hidden" id="hdnSearchTextMobilePhone" runat="server" value="" />
    <input type="hidden" id="hdnSearchTextCode" runat="server" value="" />

    <div style="display:none">
        <asp:FileUpload ID="FileUpload1" runat="server" />
    </div>

    <table cellpadding="0" cellspacing="0">
        <colgroup>
            <col style="width:100px"/>
            <col style="width:5px" />
            <col style="width:500px"/>
        </colgroup>
        <tr>
            <td><label class="lblLink lblMandatory" id="lblCompany"><%=GetLabel("Company")%></label></td>
            <td>&nbsp;</td>
            <td>
                <input type="hidden" value="" id="hdnCompanyID" runat="server" />
                <table style="width:100%" cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:30%"/>
                        <col style="width:3px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td><asp:TextBox ID="txtCompanyCode" Width="100%" runat="server" /></td>
                        <td>&nbsp;</td>
                        <td><asp:TextBox ID="txtCompanyName" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <table style="width:100%">
        <colgroup>
            <col style="width:44%"/>
            <col />
            <col style="width:54%"/>
        </colgroup>
        <tr>
            <td><h4><%=GetLabel("Registered")%></h4></td>
            <td>&nbsp;</td>
            <td><h4><%=GetLabel("Member")%></h4></td>
        </tr>
        <tr>
            <td valign="top">
                <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                    ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ onCbpViewEndCallback(s);}" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid">
                                <asp:GridView ID="grdView" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" RowStyle-CssClass="trDraggable" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:BoundField DataField="MemberID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:BoundField DataField="MemberCode" HeaderText="Code" HeaderStyle-Width="80px" />
                                        <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                                        <asp:BoundField DataField="EmailAddress1" HeaderText="Email" HeaderStyle-Width="120px" />
                                        <asp:BoundField DataField="MobilePhoneNo1" HeaderText="Mobile Phone No 1" HeaderStyle-Width="120px" />
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
            </td>
            <td>&nbsp;</td>
            <td>
                <dxcp:ASPxCallbackPanel ID="cbpViewDt" runat="server" Width="100%" ClientInstanceName="cbpViewDt"
                    ShowLoadingPanel="false" OnCallback="cbpViewDt_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ $('#containerImgLoadingViewDt').show(); }"
                        EndCallback="function(s,e){ onCbpViewDtEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent2" runat="server">
                            <asp:Panel runat="server" ID="Panel1" CssClass="pnlContainerGridPatientPage">
                                <asp:GridView ID="grdViewDt" runat="server" CssClass="grdSelected" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:BoundField DataField="MemberID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                        <asp:BoundField DataField="MemberCode" HeaderText="Code" HeaderStyle-Width="80px" />
                                        <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                                        <asp:BoundField DataField="EmailAddress1" HeaderText="Email" HeaderStyle-Width="150px" />
                                        <asp:BoundField DataField="MobilePhoneNo1" HeaderText="Mobile Phone" HeaderStyle-Width="130px" />
                                        <asp:TemplateField HeaderStyle-Width="170px">
                                        
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
                <div class="imgLoadingGrdView" id="containerImgLoadingViewDt" >
                    <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                </div>
                <div class="containerPaging">
                    <div class="wrapperPaging">
                        <div id="pagingDt"></div>
                    </div>
                </div> 
            </td>
        </tr>
    </table>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
            EndCallback="function(s,e){ onCbpProcessEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>