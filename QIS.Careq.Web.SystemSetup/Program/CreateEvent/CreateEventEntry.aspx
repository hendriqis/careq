<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="CreateEventEntry.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.CreateEventEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onLoad() {
            setDatePicker('<%=txtStartDate.ClientID %>');
            setDatePicker('<%=txtEndDate.ClientID %>');

            $('#lblPreviewEmailInvitation').click(function () {
                var templateID = cboTemplate.GetValue();
                if (templateID != null) {
                    var filterExpression = 'TemplateID = ' + templateID;
                    Methods.getObjectValue('GetTemplateTextList', filterExpression, 'TemplateContent', function (result) {
                        $('#divEmailContent').html(result);
                        pcEmailPreview.Show();
                    });
                }
            });

            $('#btnSetEmailInvitation').click(function () {
                var templateID = cboTemplate.GetValue();
                if (templateID != null) {
                    var filterExpression = 'TemplateID = ' + templateID;
                    Methods.getObjectValue('GetTemplateTextList', filterExpression, 'TemplateContent', function (result) {
                        tinyMCE.get(tinymce.editors[0].id).setContent(result);
                        tinymce.triggerSave();
                    });
                }
            });

            $('#lblPreviewEmailConfirmation').click(function () {
                var templateID = cboTemplateConfirmation.GetValue();
                if (templateID != null) {
                    var filterExpression = 'TemplateID = ' + templateID;
                    Methods.getObjectValue('GetTemplateTextList', filterExpression, 'TemplateContent', function (result) {
                        $('#divEmailContent').html(result);
                        pcEmailPreview.Show();
                    });
                }
            });

            $('#btnSetEmailConfirmation').click(function () {
                var templateID = cboTemplateConfirmation.GetValue();
                if (templateID != null) {
                    var filterExpression = 'TemplateID = ' + templateID;
                    Methods.getObjectValue('GetTemplateTextList', filterExpression, 'TemplateContent', function (result) {
                        tinyMCE.get(tinymce.editors[2].id).setContent(result);
                        tinymce.triggerSave();
                    });
                }
            });

            //#region company
            $('#lblPreviewEmailCompanyInvitation').click(function () {
                var templateID = cboTemplateCompany.GetValue();
                if (templateID != null) {
                    var filterExpression = 'TemplateID = ' + templateID;
                    Methods.getObjectValue('GetTemplateTextList', filterExpression, 'TemplateContent', function (result) {
                        $('#divEmailContent').html(result);
                        pcEmailPreview.Show();
                    });
                }
            });

            $('#btnSetEmailCompanyInvitation').click(function () {
                var templateID = cboTemplateCompany.GetValue();
                if (templateID != null) {
                    var filterExpression = 'TemplateID = ' + templateID;
                    Methods.getObjectValue('GetTemplateTextList', filterExpression, 'TemplateContent', function (result) {
                        tinyMCE.get(tinymce.editors[1].id).setContent(result);
                        tinymce.triggerSave();
                    });
                }
            });
            //#endregion

            //#region Popup Search
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
                Methods.getObject('GetTrainingList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnTrainingID.ClientID %>').val(result.TrainingID);
                        $('#<%=txtTrainingName.ClientID %>').val(result.TrainingName);
                        if ($('#<%=txtEventName.ClientID %>').val() == '')
                            $('#<%=txtEventName.ClientID %>').val(result.TrainingName);
                    }
                    else {
                        $('#<%=hdnTrainingID.ClientID %>').val('');
                        $('#<%=txtTrainingCode.ClientID %>').val('');
                        $('#<%=txtTrainingName.ClientID %>').val('');
                    }
                });
            }
            //#endregion

            //#region Venue
            $('#lblVenue.lblLink').click(function () {
                openSearchDialog('venue', 'IsDeleted = 0', function (value) {
                    $('#<%=txtVenueCode.ClientID %>').val(value);
                    onTxtVenueCodeChanged(value);
                });
            });

            $('#<%=txtVenueCode.ClientID %>').change(function () {
                onTxtVenueCodeChanged($(this).val());
            });

            function onTxtVenueCodeChanged(value) {
                var filterExpression = "VenueCode = '" + value + "' AND IsDeleted = 0";
                Methods.getObject('GetvVenueList', filterExpression, function (result) {
                    if (result != null) {
                        $('#<%=hdnVenueID.ClientID %>').val(result.VenueID);
                        $('#<%=txtVenueName.ClientID %>').val(result.VenueName);
                        $('#<%=txtVenueAddress.ClientID %>').val(result.Address);
                    }
                    else {
                        $('#<%=hdnVenueID.ClientID %>').val('');
                        $('#<%=txtVenueCode.ClientID %>').val('');
                        $('#<%=txtVenueName.ClientID %>').val('');
                        $('#<%=txtVenueAddress.ClientID %>').val('');
                    }
                });
            }
            //#endregion
            //#endregion

            registerCollapseExpandHandler();
        }
    </script>

    <input type="hidden" id="hdnID" runat="server" value="" />
    <div class="pageTitle"><%=GetLabel("Event")%></div>
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <h4 class="h4expanded"><%=GetLabel("Event Information")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Event Code")%></label></td>
                            <td><asp:TextBox ID="txtEventCode" Width="100px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Event Name")%></label></td>
                            <td><asp:TextBox ID="txtEventName" Width="300px" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Start Date / Time")%></label></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:140px"/>
                                        <col style="width:5px"/>
                                        <col style="width:80px"/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtStartDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtStartTime" CssClass="time" Width="80px" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("End Date / Time")%></label></td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:140px"/>
                                        <col style="width:5px"/>
                                        <col style="width:80px"/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtEndDate" CssClass="datepicker" Width="120px" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtEndTime" CssClass="time" Width="80px" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" id="lblTraining"><%=GetLabel("Training")%></label></td>
                            <td>
                                <input type="hidden" id="hdnTrainingID" value="" runat="server" />
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:30%"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtTrainingCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtTrainingName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>  
                </div>        
                <h4 class="h4expanded"><%=GetLabel("Event Location")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" id="lblVenue"><%=GetLabel("Venue")%></label></td>
                            <td>
                                <input type="hidden" id="hdnVenueID" value="" runat="server" />
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:30%"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtVenueCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtVenueName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel" valign="top" style="padding-top:5px"><label><%=GetLabel("Address")%></label></td>
                            <td><asp:TextBox Width="100%" runat="server" ID="txtVenueAddress" TextMode="MultiLine" /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Room")%></label></td>
                            <td><asp:TextBox ID="txtRoomName" Width="100%" runat="server"  /></td>
                        </tr>
                    </table> 
                </div>
                <h4 class="h4expanded"><%=GetLabel("Email Invitation")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                            <col style="width:320px"/>
                            <col style="width:50px"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Template Personal")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboTemplate" ClientInstanceName="cboTemplate" Width="300px" runat="server" /></td>
                            <td><label class="lblLink" id="lblPreviewEmailInvitation">Preview</label></td>
                            <td><input type="button" value="Set" id="btnSetEmailInvitation" /></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <input type="hidden" id="hdnEmailInvitation" value="" />
                                <%=GetLabel("Email Invitation") %><br />
                                <asp:TextBox TextMode="MultiLine" Width="100%" Height="300px" ID="txtEmailInvitation" runat="server" CssClass="htmlEditor" />  
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Template Company")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboTemplateCompany" ClientInstanceName="cboTemplateCompany" Width="300px" runat="server" /></td>
                            <td><label class="lblLink" id="lblPreviewEmailCompanyInvitation">Preview</label></td>
                            <td><input type="button" value="Set" id="btnSetEmailCompanyInvitation" /></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <input type="hidden" id="hdnEmailCompanyInvitation" value="" />
                                <%=GetLabel("Email Invitation") %><br />
                                <asp:TextBox TextMode="MultiLine" Width="100%" Height="300px" ID="txtEmailCompanyInvitation" runat="server" CssClass="htmlEditor" />  
                            </td>
                        </tr>
                    </table> 
                </div>
            </td>
            <td style="padding:5px;vertical-align:top">      
                <h4 class="h4expanded"><%=GetLabel("Trainer Data")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblMandatory lblLink" id="lblTrainer"><%=GetLabel("Trainer")%></label></td>
                            <td>
                                <input type="hidden" id="hdnTrainerID" value="" runat="server" />
                                <table style="width:100%" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col style="width:30%"/>
                                        <col style="width:3px"/>
                                        <col/>
                                    </colgroup>
                                    <tr>
                                        <td><asp:TextBox ID="txtTrainerCode" Width="100%" runat="server" /></td>
                                        <td>&nbsp;</td>
                                        <td><asp:TextBox ID="txtTrainerName" Width="100%" runat="server" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLabel" valign="top" style="padding-top:5px"><label class="lblNormal"><%=GetLabel("Assistant Trainer")%></label></td>
                            <td><asp:TextBox ID="txtAssistantTrainer" TextMode="MultiLine" Width="100%" runat="server"  /></td>
                        </tr>
                    </table> 
                </div> 
                <h4 class="h4expanded"><%=GetLabel("Other Information")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Price")%></label></td>
                            <td><asp:TextBox ID="txtPrice" CssClass="txtCurrency" Width="150px" runat="server"  /></td>
                        </tr>
                        <tr>
                            <td class="tdLabel" valign="top" style="padding-top:5px"><label><%=GetLabel("Remarks")%></label></td>
                            <td><asp:TextBox Width="100%" runat="server" ID="txtRemarks" TextMode="MultiLine" /></td>
                        </tr>
                    </table>
                </div>
                <h4 class="h4expanded"><%=GetLabel("Email Confirmation")%></h4>
                <div class="containerTblEntryContent">
                    <table class="tblEntryContent" style="width:100%">
                        <colgroup>
                            <col style="width:30%"/>
                            <col style="width:320px"/>
                            <col style="width:50px"/>
                        </colgroup>
                        <tr>
                            <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Template")%></label></td>
                            <td><dxe:ASPxComboBox ID="cboTemplateConfirmation" ClientInstanceName="cboTemplateConfirmation" Width="300px" runat="server" /></td>
                            <td><label class="lblLink" id="lblPreviewEmailConfirmation">Preview</label></td>
                            <td><input type="button" value="Set" id="btnSetEmailConfirmation" /></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <input type="hidden" id="hdnEmailConfirmation" value="" />
                                <%=GetLabel("Email Confirmation") %><br />
                                <asp:TextBox TextMode="MultiLine" Width="100%" Height="300px" ID="txtEmailConfirmation" runat="server" CssClass="htmlEditor" />  
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>

    <dxpc:ASPxPopupControl ID="pcEmailPreview" runat="server" ClientInstanceName="pcEmailPreview" EnableHierarchyRecreation="True"
        FooterText="" HeaderText="Email Invitation" Modal="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" Width="700px"
        PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" Height="600px">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div id="divEmailContent" style="width:100%;height:100%">></div>        
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>   
</asp:Content>
