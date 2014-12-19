<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberPastTrainingCtl.ascx.cs" 
    Inherits="QIS.Careq.Web.SystemSetup.Program.MemberPastTrainingCtl" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
    <%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript" id="dxss_serviceunithealthcareentryctl">
    $('#lblEntryPopupAddData').live('click', function () {
        $('#<%=hdnEntryID.ClientID %>').val('');

        $('#<%=hdnEventID.ClientID %>').val('');
        $('#<%=txtEventCode.ClientID %>').val('');
        $('#<%=txtEventName.ClientID %>').val('');
        $('#<%=hdnTrainingID.ClientID %>').val('');
        $('#<%=txtTrainingCode.ClientID %>').val('');
        $('#<%=txtTrainingName.ClientID %>').val('');
        $('#<%=txtTrainer.ClientID %>').val('');
        $('#<%=txtVenueName.ClientID %>').val('');
        $('#<%=txtVenueLocation.ClientID %>').val('');
        $('#<%=txtRemarks.ClientID %>').val('');
        $('#<%=txtTrainingDuration.ClientID %>').val(0);

        cboTrainingYear.SetSelectedIndex(0);
        cboTrainingMonth.SetSelectedIndex(0);
        cboTrainingDate.SetSelectedIndex(0);

        cboTrainingYear.SetEnabled(true);
        cboTrainingMonth.SetEnabled(true);
        cboTrainingDate.SetEnabled(true);
        $('#<%=txtTrainingDuration.ClientID %>').removeAttr('readonly');
        $('#<%=txtTrainingCode.ClientID %>').removeAttr('readonly');
        $('#<%=txtTrainingName.ClientID %>').removeAttr('readonly');
        $('#<%=txtTrainer.ClientID %>').removeAttr('readonly');
        $('#<%=txtVenueName.ClientID %>').removeAttr('readonly');
        $('#<%=txtVenueLocation.ClientID %>').removeAttr('readonly');
        $('#lblTraining').attr('class', 'lblLink lblMandatory');

        $('#containerPopupEntryData').show();
    });

    $('#btnEntryPopupCancel').live('click', function () {
        $('#containerPopupEntryData').hide();
    });

    $('#btnEntryPopupSave').click(function (evt) {
        if (IsValid(evt, 'fsEntryPopup', 'mpEntryPopup'))
            cbpEntryPopupView.PerformCallback('save');
        return false;
    });

    $('.imgDelete.imgLink').die('click');
    $('.imgDelete.imgLink').live('click', function () {
        if (confirm("Are You Sure Want To Delete This Data?")) {
            $row = $(this).closest('tr');
            var id = $row.find('.hdnID').val();
            $('#<%=hdnEntryID.ClientID %>').val(id);

            cbpEntryPopupView.PerformCallback('delete');
        }
    });

    $('.imgEdit.imgLink').die('click');
    $('.imgEdit.imgLink').live('click', function () {
        $row = $(this).closest('tr');
        var id = $row.find('.hdnID').val();

        var eventID = $row.find('.hdnEventID').val();
        var eventCode = $row.find('.hdnEventCode').val();
        var eventName = $row.find('.hdnEventName').val();
        var trainingID = $row.find('.hdnTrainingID').val();
        var trainingCode = $row.find('.hdnTrainingCode').val();
        var remarks = $row.find('.hdnRemarks').val();
        var trainingYear = $row.find('.hdnTrainingYear').val();
        var trainingMonth = $row.find('.hdnTrainingMonth').val();
        var trainingDate = $row.find('.hdnTrainingDate').val();
        var trainingDuration = $row.find('.hdnTrainingDuration').val();

        var trainingName = $row.find('.tdTrainingName').html();
        var trainerName = $row.find('.tdTrainerName').html();
        var venueName = $row.find('.tdVenueName').html();
        var venueLocation = $row.find('.tdVenueLocation').html();

        $('#<%=txtTrainingCode.ClientID %>').attr('readonly', 'readonly');

        cboTrainingYear.SetValue(trainingYear);
        if (trainingMonth != '00')
            cboTrainingMonth.SetValue(parseInt(trainingMonth));
        else
            cboTrainingMonth.SetSelectedIndex(0);
        if (trainingDate != '00')
            cboTrainingDate.SetValue(parseInt(trainingDate));
        else
            cboTrainingDate.SetSelectedIndex(0);


        $('#<%=txtTrainingDuration.ClientID %>').val(trainingDuration);
        $('#<%=hdnEventID.ClientID %>').val(eventID);
        $('#<%=txtEventCode.ClientID %>').val(eventCode);
        $('#<%=txtEventName.ClientID %>').val(eventName);
        $('#<%=hdnTrainingID.ClientID %>').val(trainingID);
        $('#<%=txtTrainingCode.ClientID %>').val(trainingCode);
        $('#<%=txtTrainingName.ClientID %>').val(trainingName);
        $('#<%=txtTrainer.ClientID %>').val(trainerName);
        $('#<%=txtVenueName.ClientID %>').val(venueName);
        $('#<%=txtVenueLocation.ClientID %>').val(venueLocation);
        $('#<%=txtRemarks.ClientID %>').val(remarks);

        $('#<%=hdnEntryID.ClientID %>').val(id);
        if (eventID != '') {
            cboTrainingYear.SetEnabled(false);
            cboTrainingMonth.SetEnabled(false);
            cboTrainingDate.SetEnabled(false);
            $('#<%=txtTrainingDuration.ClientID %>').attr('readonly', 'readonly');
            $('#<%=txtTrainingCode.ClientID %>').attr('readonly', 'readonly');
            $('#<%=txtTrainingName.ClientID %>').attr('readonly', 'readonly');
            $('#<%=txtTrainer.ClientID %>').attr('readonly', 'readonly');
            $('#<%=txtVenueName.ClientID %>').attr('readonly', 'readonly');
            $('#<%=txtVenueLocation.ClientID %>').attr('readonly', 'readonly');

            $('#lblTraining').attr('class', 'lblDisabled lblMandatory');
        }
        else {
            cboTrainingYear.SetEnabled(true);
            cboTrainingMonth.SetEnabled(true);
            cboTrainingDate.SetEnabled(true);
            $('#<%=txtTrainingDuration.ClientID %>').removeAttr('readonly');

            $('#<%=txtTrainingCode.ClientID %>').removeAttr('readonly');
            $('#<%=txtTrainingName.ClientID %>').removeAttr('readonly');
            $('#<%=txtTrainer.ClientID %>').removeAttr('readonly');
            $('#<%=txtVenueName.ClientID %>').removeAttr('readonly');
            $('#<%=txtVenueLocation.ClientID %>').removeAttr('readonly');
            $('#lblTraining').attr('class', 'lblLink lblMandatory');

            if (trainingID != '') {
                $('#<%=txtTrainingName.ClientID %>').attr('readonly', 'readonly');
            }
        }

        $('#containerPopupEntryData').show();
    });

    //#region Event
    $('#lblEvent.lblLink').die('click');
    $('#lblEvent.lblLink').live('click', function () {
        var filterExpression = "EventID NOT IN (SELECT EventID FROM MemberPastTraining WHERE MemberID = " + $('#<%=hdnMemberID.ClientID %>').val() + ")";
        openSearchDialog('event', filterExpression, function (value) {
            $('#<%=txtEventCode.ClientID %>').val(value);
            onTxtEventCodeChanged(value);
        });
    });

    $('#<%=txtEventCode.ClientID %>').die('change');
    $('#<%=txtEventCode.ClientID %>').live('change', function () {
        onTxtEventCodeChanged($(this).val());
    });

    function onTxtEventCodeChanged(value) {
        var filterExpression = "EventCode = '" + value + "'";
        Methods.getObject('GetvEventList', filterExpression, function (result) {
            if (result != null) {
                $('#<%=txtTrainingCode.ClientID %>').attr('readonly', 'readonly');
                $('#<%=txtTrainingName.ClientID %>').attr('readonly', 'readonly');
                $('#<%=txtTrainer.ClientID %>').attr('readonly', 'readonly');
                $('#<%=txtVenueName.ClientID %>').attr('readonly', 'readonly');
                $('#<%=txtVenueLocation.ClientID %>').attr('readonly', 'readonly');
                $('#<%=txtTrainingDuration.ClientID %>').attr('readonly', 'readonly');

                $('#lblTraining').attr('class', 'lblDisabled lblMandatory');

                cboTrainingYear.SetEnabled(false);
                cboTrainingMonth.SetEnabled(false);
                cboTrainingDate.SetEnabled(false);

                cboTrainingYear.SetValue(result.StartDateInYear);
                cboTrainingMonth.SetValue(result.StartDateInMonth);
                cboTrainingDate.SetValue(result.StartDateInDate);
                $('#<%=txtTrainingDuration.ClientID %>').val(result.TrainingDuration);
                $('#<%=hdnEventID.ClientID %>').val(result.EventID);
                $('#<%=txtEventName.ClientID %>').val(result.EventName);
                $('#<%=txtTrainingCode.ClientID %>').val(result.TrainingCode);
                $('#<%=txtTrainingName.ClientID %>').val(result.TrainingName);
                $('#<%=txtTrainer.ClientID %>').val(result.TrainerName);
                $('#<%=txtVenueName.ClientID %>').val(result.VenueName);
                $('#<%=txtVenueLocation.ClientID %>').val(result.Address);
            }
            else {
                cboTrainingYear.SetEnabled(true);
                cboTrainingMonth.SetEnabled(true);
                cboTrainingDate.SetEnabled(true);

                $('#<%=txtTrainingCode.ClientID %>').removeAttr('readonly');
                $('#<%=txtTrainingName.ClientID %>').removeAttr('readonly');
                $('#<%=txtTrainer.ClientID %>').removeAttr('readonly');
                $('#<%=txtVenueName.ClientID %>').removeAttr('readonly');
                $('#<%=txtVenueLocation.ClientID %>').removeAttr('readonly');
                $('#<%=txtTrainingDuration.ClientID %>').removeAttr('readonly');
                $('#lblTraining').attr('class', 'lblLink lblMandatory');

                cboTrainingYear.SetSelectedIndex(0);
                cboTrainingMonth.SetSelectedIndex(0);
                cboTrainingDate.SetSelectedIndex(0);
                $('#<%=txtTrainingDuration.ClientID %>').val('');
                $('#<%=hdnEventID.ClientID %>').val('');
                $('#<%=txtEventCode.ClientID %>').val('');
                $('#<%=txtEventName.ClientID %>').val('');
                $('#<%=txtTrainingCode.ClientID %>').val('');
                $('#<%=txtTrainingName.ClientID %>').val('');
                $('#<%=txtTrainer.ClientID %>').val('');
                $('#<%=txtVenueName.ClientID %>').val('');
                $('#<%=txtVenueLocation.ClientID %>').val('');
            }
        });
    }
    //#endregion

    //#region Training
    $('#lblTraining.lblLink').die('click');
    $('#lblTraining.lblLink').live('click', function () {
        openSearchDialog('training', 'IsDeleted = 0', function (value) {
            $('#<%=txtTrainingCode.ClientID %>').val(value);
            onTxtTrainingCodeChanged(value);
        });
    });

    $('#<%=txtTrainingCode.ClientID %>').die('change');
    $('#<%=txtTrainingCode.ClientID %>').live('change', function () {
        onTxtTrainingCodeChanged($(this).val());
    });

    function onTxtTrainingCodeChanged(value) {
        var filterExpression = "TrainingCode = '" + value + "' AND IsDeleted = 0";
        Methods.getObject('GetvTrainingList', filterExpression, function (result) {
            if (result != null) {
                $('#<%=hdnTrainingID.ClientID %>').val(result.TrainingID);
                $('#<%=txtTrainingName.ClientID %>').val(result.TrainingName);

                $('#<%=txtTrainingName.ClientID %>').attr('readonly', 'readonly');
            }
            else {
                $('#<%=hdnTrainingID.ClientID %>').val('');
                $('#<%=txtTrainingCode.ClientID %>').val('');
                $('#<%=txtTrainingName.ClientID %>').val('');

                $('#<%=txtTrainingName.ClientID %>').removeAttr('readonly');
            }
        });
    }
    //#endregion

    function onCbpEntryPopupViewEndCallback(s) {
        var param = s.cpResult.split('|');
        if (param[0] == 'save') {
            if (param[1] == 'fail')
                alert('Save Failed\nError Message : ' + param[2]);
            else
                $('#containerPopupEntryData').hide();
        }
        else if (param[0] == 'delete') {
            if (param[1] == 'fail')
                alert('Delete Failed\nError Message : ' + param[2]);
        }
        hideLoadingPanel();
    }
</script>

<div style="height:440px; overflow-y:auto">
    <input type="hidden" id="hdnMemberID" value="" runat="server" />
    <div class="pageTitle"><%=GetLabel("Past Training")%></div>
    <table class="tblContentArea">
        <colgroup>
            <col style="width:100%"/>
        </colgroup>
        <tr>            
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:70%">
                    <colgroup>
                        <col style="width:160px"/>
                        <col/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Member")%></label></td>
                        <td colspan="2"><asp:TextBox ID="txtHeaderText" ReadOnly="true" Width="100%" runat="server" /></td>
                    </tr> 
                </table>

                <div id="containerPopupEntryData" style="margin-top:10px;display:none;">
                    <input type="hidden" id="hdnEntryID" runat="server" value="" />
                    <div class="pageTitle"><%=GetLabel("Entry")%></div>
                    <fieldset id="fsEntryPopup" style="margin:0"> 
                        <table class="tblEntryDetail" style="width:100%">
                            <colgroup>
                                <col style="width:150px"/>
                                <col />
                            </colgroup>
                            <tr>
                                <td class="tdLabel"><label class="lblLink lblMandatory" id="lblEvent"><%=GetLabel("Event")%></label></td>
                                <td>
                                    <input type="hidden" value="" id="hdnEventID" runat="server" />
                                    <table style="width:100%" cellpadding="0" cellspacing="0">
                                        <colgroup>
                                            <col style="width:30%"/>
                                            <col style="width:3px"/>
                                            <col/>
                                        </colgroup>
                                        <tr>
                                            <td><asp:TextBox ID="txtEventCode" CssClass="required" Width="100%" runat="server" /></td>
                                            <td>&nbsp;</td>
                                            <td><asp:TextBox ID="txtEventName" ReadOnly="true" Width="100%" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Training Date")%></label></td>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <colgroup>
                                            <col style="width:80px"/>
                                            <col style="width:5px"/>
                                            <col style="width:100px"/>
                                            <col style="width:5px"/>
                                            <col style="width:80px"/>
                                        </colgroup>
                                        <tr>
                                            <td><dxe:ASPxComboBox runat="server" ID="cboTrainingYear" ClientInstanceName="cboTrainingYear" Width="80px" /></td>
                                            <td>&nbsp;</td>
                                            <td><dxe:ASPxComboBox runat="server" ID="cboTrainingMonth" ClientInstanceName="cboTrainingMonth" Width="100px" /></td>
                                            <td>&nbsp;</td>
                                            <td><dxe:ASPxComboBox runat="server" ID="cboTrainingDate" ClientInstanceName="cboTrainingDate" Width="80px" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Duration")%></label></td>
                                <td><asp:TextBox ID="txtTrainingDuration" Width="80px" CssClass="number" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="tdLabel"><label class="lblLink lblMandatory" id="lblTraining"><%=GetLabel("Training")%></label></td>
                                <td>
                                    <input type="hidden" value="" id="hdnTrainingID" runat="server" />
                                    <table style="width:100%" cellpadding="0" cellspacing="0">
                                        <colgroup>
                                            <col style="width:30%"/>
                                            <col style="width:3px"/>
                                            <col/>
                                        </colgroup>
                                        <tr>
                                            <td><asp:TextBox ID="txtTrainingCode" CssClass="required" Width="100%" runat="server" /></td>
                                            <td>&nbsp;</td>
                                            <td><asp:TextBox ID="txtTrainingName" ReadOnly="true" Width="100%" runat="server" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Trainer")%></label></td>
                                <td><asp:TextBox ID="txtTrainer" Width="100%" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Venue Name")%></label></td>
                                <td><asp:TextBox ID="txtVenueName" Width="100%" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Venue Location")%></label></td>
                                <td><asp:TextBox ID="txtVenueLocation" Width="100%" runat="server" /></td>
                            </tr>
                            <tr>
                                <td class="tdLabel" style="vertical-align: top; padding-top: 5px;"><label class="lblNormal"><%=GetLabel("Remarks")%></label></td>
                                <td colspan="2"><asp:TextBox ID="txtRemarks" Width="300px" runat="server" TextMode="MultiLine" /></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td>
                                                <input type="button" id="btnEntryPopupSave" value='<%= GetLabel("Save")%>' />
                                            </td>
                                            <td>
                                                <input type="button" id="btnEntryPopupCancel" value='<%= GetLabel("Cancel")%>' />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>

                <dxcp:ASPxCallbackPanel ID="cbpEntryPopupView" runat="server" Width="100%" ClientInstanceName="cbpEntryPopupView"
                    ShowLoadingPanel="false" OnCallback="cbpEntryPopupView_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }"
                        EndCallback="function(s,e){ onCbpEntryPopupViewEndCallback(s); }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <asp:Panel runat="server" ID="pnlPatientVisitTransHdGrdView" Style="width: 100%; margin-left: auto; margin-right: auto; position: relative;font-size:0.95em;">
                                <asp:GridView ID="grdView" runat="server" CssClass="grdView notAllowSelect" AutoGenerateColumns="false" OnRowDataBound="grdView_RowDataBound" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="trEmpty">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <img class='imgEdit imgLink' title='<%=GetLabel("Edit")%>' 
                                                    src='<%=ResolveUrl("~/Libs/Images/Button/edit.png")%>' alt="" style="float:left;margin-right: 2px;" />
                                                <img class='imgDelete imgLink' title='<%=GetLabel("Delete")%>' 
                                                    src='<%=ResolveUrl("~/Libs/Images/Button/delete.png")%>' alt="" />

                                                <input type="hidden" class="hdnID" value="<%# Eval("ID")%>" />
                                                <input type="hidden" class="hdnEventID" value="<%# Eval("EventID")%>" />
                                                <input type="hidden" class="hdnEventCode" value="<%# Eval("EventCode")%>" />
                                                <input type="hidden" class="hdnEventName" value="<%# Eval("EventName")%>" />
                                                <input type="hidden" class="hdnTrainingID" value="<%# Eval("TrainingID")%>" />
                                                <input type="hidden" class="hdnTrainingCode" value="<%# Eval("TrainingCode")%>" />
                                                <input type="hidden" class="hdnRemarks" value="<%# Eval("Remarks")%>" />
                                                <input type="hidden" class="hdnTrainingYear" value="<%# Eval("DisplayTrainingInYear")%>" />
                                                <input type="hidden" class="hdnTrainingMonth" value="<%# Eval("DisplayTrainingInMonth")%>" />
                                                <input type="hidden" class="hdnTrainingDate" value="<%# Eval("DisplayTrainingInDate")%>" />
                                                <input type="hidden" class="hdnTrainingDuration" value="<%# Eval("DisplayTrainingDuration")%>" />                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataField="DisplayTrainingDate" HeaderText="Training Date" ItemStyle-CssClass="tdTrainingDate" />
                                        <asp:BoundField DataField="TrainingName" HeaderText="Training Name" ItemStyle-CssClass="tdTrainingName" />
                                        <asp:BoundField HeaderStyle-Width="100px" DataField="DisplayTrainerName" ItemStyle-CssClass="tdTrainerName" HeaderText="Trainer" />
                                        <asp:BoundField HeaderStyle-Width="100px" DataField="VenueName" ItemStyle-CssClass="tdVenueName" HeaderText="Venue Name" />
                                        <asp:BoundField HeaderStyle-Width="150px" DataField="Address" ItemStyle-CssClass="tdVenueLocation" HeaderText="Venue Location" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Data To Display
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:Panel>
                        </dx:PanelContent>
                    </PanelCollection>
                </dxcp:ASPxCallbackPanel>
                <div class="imgLoadingGrdView" id="containerImgLoadingViewPopup">
                    <img src='<%= ResolveUrl("~/Libs/Images/loading_small.gif")%>' alt='' />
                </div>
                <div style="width:100%;text-align:center" id="divContainerAddData" runat="server">
                    <span class="lblLink" id="lblEntryPopupAddData"><%= GetLabel("AddData")%></span>
                </div>
            </td>
        </tr>
    </table>
    <div style="width:100%;text-align:right">
        <input type="button" value='<%= GetLabel("Close")%>' onclick="pcRightPanelContent.Hide();" />
    </div>
</div>

