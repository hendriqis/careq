<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPList.master" AutoEventWireup="true" 
CodeBehind="ProposalTrackingLogEntry.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.ProposalTrackingLogEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        $(function () {
            setDatePicker('<%=txtLogDate.ClientID %>');

            $('#lblAddData').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    $('#<%=hdnEntryID.ClientID %>').val('');
                    var time = new Date();
                    $('#<%=txtLogTime.ClientID %>').val(time.getHours() + ':' + time.getMinutes());
                    $('#<%=txtRemarks.ClientID %>').val('');
                    $('#containerEntry').show();

                    $('#<%=hdnEmployeeID.ClientID %>').val($('#<%=hdnDefaultEmployeeID.ClientID %>').val());
                    $('#<%=txtEmployeeCode.ClientID %>').val($('#<%=hdnDefaultEmployeeCode.ClientID %>').val());
                    $('#<%=txtEmployeeName.ClientID %>').val($('#<%=hdnDefaultEmployeeName.ClientID %>').val());
                }
            });
                        
            $('#btnCancel').click(function () {
                $('#containerEntry').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });

            //#region Employee
            $('#lblEmployee.lblLink').click(function () {
                openSearchDialog('employee', 'IsDeleted = 0', function (value) {
                    $('#<%=txtEmployeeCode.ClientID %>').val(value);
                    onTxtEmployeeCodeChanged(value);
                });
            });

            $('#<%=txtEmployeeCode.ClientID %>').change(function () {
                onTxtEmployeeCodeChanged($(this).val());
            });

            function onTxtEmployeeCodeChanged(value) {
                var filterExpression = "EmployeeCode = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetvEmployeeList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnEmployeeID.ClientID %>').val(result.EmployeeID);
                        $('#<%=txtEmployeeName.ClientID %>').val(result.EmployeeName);
                    }
                    else {
                        $('#<%=hdnEmployeeID.ClientID %>').val('');
                        $('#<%=txtEmployeeCode.ClientID %>').val('');
                        $('#<%=txtEmployeeName.ClientID %>').val('');
                    }
                });
            }
            //#endregion
        });

        function onCbpProcesEndCallback(s) {
            hideLoadingPanel();
            var param = s.cpResult.split('|');
            if (param[0] == 'save') {
                if (param[1] == 'fail')
                    alert('Save Failed\nError Message : ' + param[2]);   
                else {
                    cbpView.PerformCallback('refresh');
                    $('#containerEntry').hide();
                }
            }
            else if (param[0] == 'delete') {
                if (param[1] == 'fail')
                    alert('Delete Failed\nError Message : ' + param[2]);   
                else
                    cbpView.PerformCallback('refresh');
            }
        }

        $('.imgEdit.imgLink').die('click');
        $('.imgEdit.imgLink').live('click', function () {
            $row = $(this).closest('tr').parent().closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.ID);
            $('#<%=txtLogDate.ClientID %>').val(entity.LogDateInDatePicker);
            $('#<%=txtLogTime.ClientID %>').val(entity.LogTime);
            cboActivityType.SetValue(entity.GCActivityType);
            $('#<%=txtRemarks.ClientID %>').val(entity.Remarks);
            $('#<%=hdnEmployeeID.ClientID %>').val(entity.CRO);
            $('#<%=txtEmployeeCode.ClientID %>').val(entity.CROCode);
            $('#<%=txtEmployeeName.ClientID %>').val(entity.CROName);
            $('#containerEntry').show();
        });

        $('.imgDelete.imgLink').die('click');
        $('.imgDelete.imgLink').live('click', function () {
            $row = $(this).closest('tr').parent().closest('tr');
            var entity = rowToObject($row);
            $('#<%=hdnEntryID.ClientID %>').val(entity.ID);
            cbpProcess.PerformCallback('delete');
        });
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <input type="hidden" id="hdnDefaultEmployeeID" runat="server" value="" />
    <input type="hidden" id="hdnDefaultEmployeeCode" runat="server" value="" />
    <input type="hidden" id="hdnDefaultEmployeeName" runat="server" value="" />
    <table style="width:100%">
        <colgroup>
            <col style="width:100%"/>
            <col />
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent">
                    <colgroup>
                        <col style="width:150px"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Proposal No.")%></label></td>
                        <td><asp:TextBox ID="txtProposalNo" Width="200px" ReadOnly="true" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Proposal Date")%></label></td>
                        <td><asp:TextBox ID="txtProposalDate" CssClass="datepicker" Width="120px" runat="server" ReadOnly="true" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Subject")%></label></td>
                        <td><asp:TextBox ID="txtSubject" Width="300px" runat="server" ReadOnly="true" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td> 
                <div id="containerEntry" style="margin-top:4px;display:none;">
                    <div class="pageTitle"><%=GetLabel("Entry")%></div>
                    <fieldset id="fsTrx" style="margin:0"> 
                        <input type="hidden" value="" id="hdnEntryID" runat="server" />
                        <table class="tblEntryDetail">
                            <tr>
                                <td>
                                    <table>
                                        <colgroup>
                                            <col width="130px" />
                                            <col />
                                        </colgroup>
                                        <tr>
                                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Log Date")%></label></td>
                                            <td><asp:TextBox ID="txtLogDate" CssClass="datepicker" Width="120px" runat="server" /></td>        
                                        </tr>
                                        <tr>
                                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Log Time")%></label></td>
                                            <td><asp:TextBox ID="txtLogTime" CssClass="datepicker" Width="100px" runat="server" /></td>        
                                        </tr>
                                        <tr>
                                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Activity Type")%></label></td>
                                            <td><dxe:ASPxComboBox ID="cboActivityType" ClientInstanceName="cboActivityType" Width="120px" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td class="tdLabel"><label class="lblMandatory lblLink" id="lblEmployee"><%=GetLabel("CRO")%></label></td>
                                            <td>
                                                <input type="hidden" id="hdnEmployeeID" runat="server" value="" />
                                                <table cellpadding="0" cellspacing="0">
                                                    <colgroup>
                                                        <col style="width:97px"/>
                                                        <col style="width:3px"/>
                                                        <col/>
                                                    </colgroup>
                                                    <tr>
                                                        <td><asp:TextBox ID="txtEmployeeCode" Width="100%" runat="server" /></td>
                                                        <td>&nbsp;</td>
                                                        <td><asp:TextBox ID="txtEmployeeName" Width="200px" runat="server" ReadOnly="true" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdLabel" valign="top"><label class="lblNormal"><%=GetLabel("Remarks")%></label></td>
                                            <td><asp:TextBox ID="txtRemarks" TextMode="MultiLine" Width="300px" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <input type="button" id="btnSave" value='<%= GetLabel("Save")%>' />
                                                        </td>
                                                        <td>
                                                            <input type="button" id="btnCancel" value='<%= GetLabel("Cancel")%>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>    
                <div style="position: relative;">
                    <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
                        ShowLoadingPanel="false" OnCallback="cbpView_Callback">
                        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }" 
                            EndCallback="function(s,e) { hideLoadingPanel(); }" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="height:300px">
                                    <asp:GridView ID="grdView" runat="server" CssClass="grdSelected grdPatientPage" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderStyle-CssClass="keyField" ItemStyle-CssClass="keyField" />
                                            <asp:TemplateField  HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <img class="imgEdit <%# IsEditable.ToString() == "False" ? "imgDisabled" : "imgLink"%>" title='<%=GetLabel("Edit")%>' 
                                                                    src='<%# IsEditable.ToString() == "False" ? ResolveUrl("~/Libs/Images/Button/edit_disabled.png") : ResolveUrl("~/Libs/Images/Button/edit.png")%>' alt="" />
                                                            </td>
                                                            <td style="width:1px">&nbsp;</td>
                                                            <td>
                                                                <img class="imgDelete <%# IsEditable.ToString() == "False" ? "imgDisabled" : "imgLink"%>" title='<%=GetLabel("Delete")%>' 
                                                                    src='<%# IsEditable.ToString() == "False" ? ResolveUrl("~/Libs/Images/Button/delete_disabled.png") : ResolveUrl("~/Libs/Images/Button/delete.png")%>' alt="" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <input type="hidden" value="<%#Eval("ID") %>" bindingfield="ID" />
                                                    <input type="hidden" value="<%#Eval("LogDateInDatePicker") %>" bindingfield="LogDateInDatePicker" />
                                                    <input type="hidden" value="<%#Eval("LogTime") %>" bindingfield="LogTime" />
                                                    <input type="hidden" value="<%#Eval("GCActivityType") %>" bindingfield="GCActivityType" />
                                                    <input type="hidden" value="<%#Eval("CRO") %>" bindingfield="CRO" />
                                                    <input type="hidden" value="<%#Eval("CROCode") %>" bindingfield="CROCode" />
                                                    <input type="hidden" value="<%#Eval("CROName") %>" bindingfield="CROName" />
                                                    <input type="hidden" value="<%#Eval("Remarks") %>" bindingfield="Remarks" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="LogDateInString" HeaderText="Date" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="LogTime" HeaderText="Time" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="CROName" HeaderText="CRO" HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="ActivityType" HeaderText="Activity Type" HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <%=GetLabel("No Data To Display")%>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <div class="imgLoadingGrdView" id="containerImgLoadingView" >
                                        <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                                    </div>     
                                    <div style='width:100%;text-align:center;' >
                                        <span class="lblLink" style="text-align:center" id="lblAddData"><%= GetLabel("Add Data")%></span>
                                    </div>
                                </asp:Panel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dxcp:ASPxCallbackPanel>    
                </div>
            </td>
        </tr>
    </table>
    <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
        ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
        <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }"
            EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
    </dxcp:ASPxCallbackPanel>
</asp:Content>
