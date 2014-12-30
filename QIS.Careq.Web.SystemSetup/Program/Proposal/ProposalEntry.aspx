<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="ProposalEntry.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.ProposalEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtProposalDate.ClientID %>');
            //#region Company
            $('#lblCompany.lblLink').click(function () {
                openSearchDialog('company', 'IsDeleted = 0', function (value) {
                    $('#<%=txtCompanyCode.ClientID %>').val(value);
                    onTxtCompanyCodeChanged(value);
                });
            });

            $('#<%=txtCompanyCode.ClientID %>').change(function () {
                onTxtCompanyCodeChanged($(this).val());
            });

            function onTxtCompanyCodeChanged(value) {
                var filterExpression = "CompanyCode = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetCompanyList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnCompanyID.ClientID %>').val(result.CompanyID);
                        $('#<%=txtCompanyName.ClientID %>').val(result.CompanyName);
                    }
                    else {
                        $('#<%=hdnCompanyID.ClientID %>').val('');
                        $('#<%=txtCompanyCode.ClientID %>').val('');
                        $('#<%=txtCompanyName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

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

            //#region Member
            $('#lblMember.lblLink').click(function () {
                openSearchDialog('member', 'IsDeleted = 0', function (value) {
                    $('#<%=hdnMemberID.ClientID %>').val(value);
                    onTxtMemberCodeChanged(value);
                });
            });

            $('#<%=txtMemberCode.ClientID %>').change(function () {
                var filterExpression = "MemberCode = '" + $(this).val() + "' AND IsDeleted = 0";
                Methods.getObject('GetvMemberList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnMemberID.ClientID %>').val(result.MemberID);
                        $('#<%=txtMemberName.ClientID %>').val(result.LastName);
                    }
                    else {
                        $('#<%=hdnMemberID.ClientID %>').val('');
                        $('#<%=txtMemberCode.ClientID %>').val('');
                        $('#<%=txtMemberName.ClientID %>').val('');
                    }
                });
            });

            function onTxtMemberCodeChanged(value) {
                var filterExpression = "MemberID = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetMemberList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=txtMemberCode.ClientID %>').val(result.MemberCode);
                        $('#<%=txtMemberName.ClientID %>').val(result.LastName);
                    }
                    else {
                        $('#<%=hdnMemberID.ClientID %>').val('');
                        $('#<%=txtMemberCode.ClientID %>').val('');
                        $('#<%=txtMemberName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Trainer
            $('#lblTrainer.lblLink').click(function () {
                openSearchDialog('trainer', 'IsDeleted = 0', function (value) {
                    $('#<%=txtTrainerCode.ClientID %>').val(value);
                    onTxtTrainerCodeChanged(value);
                });
            });

            $('#<%=txtTrainerCode.ClientID %>').change(function () {
                onTxtTrainerCodeChanged($(this).val());
            });

            function onTxtTrainerCodeChanged(value) {
                var filterExpression = "TrainerCode = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetvTrainerList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnTrainerID.ClientID %>').val(result.TrainerID);
                        $('#<%=txtTrainerName.ClientID %>').val(result.TrainerName);
                    }
                    else {
                        $('#<%=hdnTrainerID.ClientID %>').val('');
                        $('#<%=txtTrainerCode.ClientID %>').val('');
                        $('#<%=txtTrainerName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Inquiry
            function getInquiryFilterExpression() {
                var filterExpression = "<%=GetInquiryFilterExpression() %>";
                return filterExpression;
            }

            $('#lblInquiry.lblLink').click(function () {
                openSearchDialog('inquiry', getInquiryFilterExpression(), function (value) {
                    $('#<%=hdnInquiryID.ClientID %>').val(value);
                    onTxtInquiryNoChanged(value);
                });
            });

            function onTxtInquiryNoChanged(value) {
                var filterExpression = "InquiryID = " + value + " AND " + getInquiryFilterExpression();
                Methods.getObject('GetvInquiryList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=txtInquiryNo.ClientID %>').val(result.InquiryNo);
                    }
                    else {
                        $('#<%=hdnInquiryID.ClientID %>').val('');
                        $('#<%=txtInquiryNo.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Training
            $('#lblTraining.lblLink').click(function () {
                openSearchDialog('training', 'IsDeleted = 0', function (value) {
                    $('#<%=txtTrainingCode.ClientID %>').val(value);
                    onTxtTrainingCodeChanged(value);
                });
            });

            $('#<%=txtTrainingCode.ClientID %>').change(function () {
                onTxtTrainingCodeChanged($(this).val());
            });

            function onTxtTrainingCodeChanged(value) {
                var filterExpression = "TrainingCode = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetvTrainingList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnTrainingID.ClientID %>').val(result.TrainingID);
                        $('#<%=txtTrainingName.ClientID %>').val(result.TrainingName);
                    }
                    else {
                        $('#<%=hdnTrainingID.ClientID %>').val('');
                        $('#<%=txtTrainingCode.ClientID %>').val('');
                        $('#<%=txtTrainingName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            $('#lblAddData').click(function (evt) {
                if (IsValid(evt, 'fsMPEntry', 'mpEntry')) {
                    $('#<%=hdnEntryID.ClientID %>').val('');
                    $('#<%=hdnTrainingID.ClientID %>').val('');
                    $('#<%=txtTrainingCode.ClientID %>').val('');
                    $('#<%=txtTrainingName.ClientID %>').val('');
                    $('#<%=txtDuration.ClientID %>').val('');
                    $('#<%=txtNoOfPerson.ClientID %>').val('');
                    $('#<%=txtAmount.ClientID %>').val('');
                    $('#<%=txtMarginPercentage.ClientID %>').val('');
                    $('#<%=txtProposalContent.ClientID %>').val('');
                    $('#<%=chkIsRevision.ClientID %>').prop("checked", false);
                    $('#containerEntry').show();
                }
            });

            $('#btnCancel').click(function () {
                $('#containerEntry').hide();
            });

            $('#btnSave').click(function (evt) {
                if (IsValid(evt, 'fsTrx', 'mpTrx'))
                    cbpProcess.PerformCallback('save');
            });
        }

        function getOtherLeadSourceType()
        {
            return "<%=GetOtherLeadSourceType() %>";
        }

        function proposalTypeChanged() {
            var proposalType = getOtherLeadSourceType();
            if (cboProposalType.GetValue() == leadSourceType) {
                $('#<%=trOtherSourceType.ClientID %>').show();
            } else {
                $('#<%=trOtherSourceType.ClientID %>').hide();
            }
        }

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
            cboGCItemType.SetValue(entity.GCItemType);
            $('#<%=hdnTrainingID.ClientID %>').val(entity.ItemID);
            $('#<%=txtTrainingCode.ClientID %>').val(entity.TrainingCode);
            $('#<%=txtTrainingName.ClientID %>').val(entity.TrainingName);
            $('#<%=txtDuration.ClientID %>').val(entity.Duration);
            $('#<%=txtNoOfPerson.ClientID %>').val(entity.NoOfPerson);
            $('#<%=txtAmount.ClientID %>').val(entity.Amount).trigger('changeValue');
            $('#<%=txtMarginPercentage.ClientID %>').val(entity.MarginPercentage);
            $('#<%=txtProposalContent.ClientID %>').val(entity.ProposalContent);
            $('#<%=chkIsRevision.ClientID %>').prop("checked", entity.IsRevision);
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
    <div class="pageTitle"><%=GetLabel("Proposal")%></div>
    <table class="tblContentArea" style="width:100%">
        <colgroup>
            <col style="width:100%"/>
            <col />
        </colgroup>
        <tr>
            <td>
                <table>
                    <colgroup>
                        <col style="width:50%"/>
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
                                    <td><asp:TextBox ID="txtProposalDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Proposal Type")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboProposalType" ClientInstanceName="cboProposalType" Width="120px" runat="server" /></td>
                                </tr>
                                <tr id="trOtherSourceType" runat="server" style="display:none;" >
                                    <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Other Source Type")%></label></td>
                                    <td><asp:TextBox ID="txtOtherLeadSourceType" Width="300px" runat="server" /></td>                
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Language Type")%></label></td>
                                    <td><dxe:ASPxComboBox ID="cboLanguageType" ClientInstanceName="cboLanguageType" Width="120px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory lblLink" id="lblInquiry"><%=GetLabel("Inquiry")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnInquiryID" runat="server" value="" />
                                        <table cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:97px"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox ID="txtInquiryNo" Width="100%" ReadOnly="true" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory lblLink" id="lblCompany"><%=GetLabel("Company")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnCompanyID" runat="server" value="" />
                                        <table cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:97px"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox ID="txtCompanyCode" Width="100%" runat="server" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtCompanyName" Width="200px" runat="server" ReadOnly="true" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory lblLink" id="lblEmployee"><%=GetLabel("PIC CRO")%></label></td>
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
                                    <td class="tdLabel"><label class="lblMandatory lblLink" id="lblTrainer"><%=GetLabel("Trainer")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnTrainerID" runat="server" value="" />
                                        <table cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:97px"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox ID="txtTrainerCode" Width="100%" runat="server" /></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtTrainerName" Width="200px" runat="server" ReadOnly="true" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory lblLink" id="lblMember"><%=GetLabel("Member")%></label></td>
                                    <td>
                                        <input type="hidden" id="hdnMemberID" runat="server" value="" />
                                        <table cellpadding="0" cellspacing="0">
                                            <colgroup>
                                                <col style="width:97px"/>
                                                <col style="width:3px"/>
                                                <col/>
                                            </colgroup>
                                            <tr>
                                                <td><asp:TextBox ID="txtMemberCode" Width="100%" runat="server"/></td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="txtMemberName" Width="200px" runat="server" ReadOnly="true" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Subject")%></label></td>
                                    <td><asp:TextBox ID="txtSubject" Width="300px" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td class="tdLabel" valign="top"><label class="lblNormal"><%=GetLabel("Remarks")%></label></td>
                                    <td><asp:TextBox ID="txtRemarks" TextMode="MultiLine" Width="300px" runat="server" /></td>
                                </tr>
                            </table>
                        </td>
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
                                            <td class="tdLabel"><label class="lblMandatory lblLink" id="lblTraining"><%=GetLabel("Training")%></label></td>
                                            <td>
                                                <input type="hidden" id="hdnTrainingID" runat="server" value="" />
                                                <table cellpadding="0" cellspacing="0">
                                                    <colgroup>
                                                        <col style="width:97px"/>
                                                        <col style="width:3px"/>
                                                        <col/>
                                                    </colgroup>
                                                    <tr>
                                                        <td><asp:TextBox ID="txtTrainingCode" Width="100%" runat="server" /></td>
                                                        <td>&nbsp;</td>
                                                        <td><asp:TextBox ID="txtTrainingName" Width="200px" runat="server" ReadOnly="true" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Training Type")%></label></td>
                                            <td><dxe:ASPxComboBox ID="cboGCItemType" ClientInstanceName="cboGCItemType" Width="120px" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Duration")%></label></td>
                                            <td><asp:TextBox ID="txtDuration" Width="100px" runat="server" /></td>        
                                        </tr>
                                        <tr>
                                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("No. Of Person")%></label></td>
                                            <td><asp:TextBox ID="txtNoOfPerson" Width="100px" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Amount")%></label></td>
                                            <td><asp:TextBox ID="txtAmount" CssClass="txtCurrency" Width="100px" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Margin Percentage")%></label></td>
                                            <td><asp:TextBox ID="txtMarginPercentage" CssClass="number" Width="100px" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Proposal Content")%></label></td>
                                            <td><asp:TextBox ID="txtProposalContent" Width="100px" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td><asp:CheckBox ID="chkIsRevision" runat="server" Text="Revision" /></td>
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
                                <asp:Panel runat="server" ID="pnlView">
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
                                                    <input type="hidden" value="<%#Eval("GCItemType") %>" bindingfield="GCItemType" />
                                                    <input type="hidden" value="<%#Eval("ItemType") %>" bindingfield="ItemType" />
                                                    <input type="hidden" value="<%#Eval("ItemID") %>" bindingfield="ItemID" />
                                                    <input type="hidden" value="<%#Eval("TrainingCode") %>" bindingfield="TrainingCode" />
                                                    <input type="hidden" value="<%#Eval("TrainingName") %>" bindingfield="TrainingName" />
                                                    <input type="hidden" value="<%#Eval("Duration") %>" bindingfield="Duration" />
                                                    <input type="hidden" value="<%#Eval("NoOfPerson") %>" bindingfield="NoOfPerson" />
                                                    <input type="hidden" value="<%#Eval("Amount") %>" bindingfield="Amount" />
                                                    <input type="hidden" value="<%#Eval("MarginPercentage") %>" bindingfield="MarginPercentage" />
                                                    <input type="hidden" value="<%#Eval("ProposalContent") %>" bindingfield="ProposalContent" />
                                                    <input type="hidden" value="<%#Eval("IsActive") %>" bindingfield="IsActive" />
                                                    <input type="hidden" value="<%#Eval("IsRevision") %>" bindingfield="IsRevision" />
                                                    <input type="hidden" value="<%#Eval("RevisionDate") %>" bindingfield="RevisionDate" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TrainingCode" HeaderText="Training Code" HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="TrainingName" HeaderText="Training Name" />
                                            <asp:BoundField DataField="ItemType" HeaderText="Item Type" HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="Duration" HeaderText="Duration" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundField DataField="NoOfPerson" HeaderText="Person" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundField DataField="ProposalContent" HeaderText="Proposal Content" HeaderStyle-Width="200px" />
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
                <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
                    ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
                    <ClientSideEvents BeginCallback="function(s,e) { showLoadingPanel(); }"
                        EndCallback="function(s,e) { onCbpProcesEndCallback(s); }" />
                </dxcp:ASPxCallbackPanel>
            </td>
        </tr>
    </table>
</asp:Content>
