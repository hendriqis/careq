<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="PivotReporting.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.PivotReporting" %>

<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    namespace="DevExpress.Web.ASPxPivotGrid.Export" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content3" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnGenerate" crudmode="R" runat="server">
        <img src='<%=ResolveUrl("~/Libs/Images/Icon/tbset.png")%>' alt="" /><br style="clear: both" />
        <div>
            <%=GetLabel("Generate")%>
        </div>
    </li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="plhList" runat="server">
    <script type="text/javascript">
        $(function () {
            setDatePickerElement($('#<%=txtValueDateFrom.ClientID %>'));
            setDatePickerElement($('#<%=txtValueDateTo.ClientID %>'));

            $('#<%=btnGenerate.ClientID %>').click(function () {
                showLoadingPanel();

                cbpView.PerformCallback('refresh');
            });
        });

        function onCbpViewEndCallback(s) {
            $('#divPivot').show();
            hideLoadingPanel();
        }
    </script>
    <style type="text/css">
        .dxpgControl tr td                          { background-color: #FFFFFF; }
        .dxpgControl tr td tr:first-child td        { background-color: #9CC525; }
        
    </style>
    
    <table>
        <colgroup>
            <col style="width: 100px" />
        </colgroup>
        <tr id="trPeriode" runat="server">
            <td class="tdLabel"><label class="tdLabel"><%=GetLabel("Periode")%></label></td>
            <td>
                <asp:TextBox ID="txtValueDateFrom" CssClass="datepicker" runat="server" Width="120px" />
                -
                <asp:TextBox ID="txtValueDateTo" CssClass="datepicker" runat="server" Width="120px" />
            </td>
        </tr>
    </table>
    <hr />
    <div id="divPivot" style="display: none">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <strong><%=GetLabel("Export to")%>:</strong>
                            </td>
                            <td>
                                <dxe:ASPxComboBox ID="cboListExportFormat" runat="server" Style="vertical-align: middle"
                                    SelectedIndex="0" ValueType="System.String" Width="61px">
                                    <Items>
                                        <dxe:ListEditItem Text="Pdf" Value="0" />
                                        <dxe:ListEditItem Text="Excel" Value="1" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSavePivot" OnClick="btnSavePivot_Click" runat="server" Text="Save" />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="3" cellspacing="0">
                        <tr>
                            <td rowspan="5" valign="top" style="width: 106px">
                                <strong><%=GetLabel("Export Options")%>: </strong>
                            </td>
                            <td style="width: 350px">
                                <asp:CheckBox ID="chkPrintHeadersOnEveryPage" runat="server" Text="Print headers on every page" /><br />
                                <asp:CheckBox ID="chkPrintFilterHeaders" runat="server" Text="Print filter headers" Checked="True" /><br />
                                <asp:CheckBox ID="chkPrintColumnHeaders" runat="server" Text="Print column headers" Checked="True" /><br />
                                <asp:CheckBox ID="chkPrintRowHeaders" runat="server" Text="Print row headers" Checked="True" /><br />
                                <asp:CheckBox ID="checkPrintDataHeaders" runat="server" Text="Print data headers" Checked="True" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <dxcp:ASPxCallbackPanel ID="cbpView" runat="server" Width="100%" ClientInstanceName="cbpView"
            ShowLoadingPanel="false" OnCallback="cbpView_Callback">
            <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" EndCallback="function(s,e){ onCbpViewEndCallback(s); }" />
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <asp:Panel runat="server" ID="pnlView" CssClass="pnlContainerGrid" Style="height: 450px;">
                        <dx:ASPxPivotGrid ID="pvRegistration" runat="server" ClientIDMode="AutoID" Width="100%" DataSourceID="odsPivotRegistration" >
                            <Fields>
                                <dx:PivotGridField Area="RowArea" AreaIndex="0" FieldName="CompanyName" Caption="Company"
                                    ID="Company">
                                    <CustomTotals>
                                        <dx:PivotGridCustomTotal SummaryType="Average" />
                                        <dx:PivotGridCustomTotal />
                                        <dx:PivotGridCustomTotal SummaryType="Min" />
                                        <dx:PivotGridCustomTotal SummaryType="Max" />
                                    </CustomTotals>
                                </dx:PivotGridField>
                                <dx:PivotGridField Area="ColumnArea" AreaIndex="0" FieldName="EventDate" ID="fieldEventDate0"
                                    Caption="Year" GroupInterval="DateYear" />
                                <dx:PivotGridField Area="ColumnArea" AreaIndex="1" FieldName="EventDate" ID="fieldEventDate1"
                                    Caption="Month" GroupInterval="DateMonth"  />
                                <dx:PivotGridField Area="DataArea" AreaIndex="3" FieldName="NumberOfTraining" Caption="Number Of Training" CellFormat-FormatType="Numeric"
                                    ID="NumberTraining" />            
                                <dx:PivotGridField Area="FilterArea" AreaIndex="4" FieldName="TrainingName" Caption="Training"
                                    ID="Member" />         
                                <dx:PivotGridField Area="FilterArea" AreaIndex="5" FieldName="OccupationLevel" Caption="Occupation"
                                    ID="Occupation" />
                            </Fields>
                            <OptionsPager RowsPerPage="20" />
                            <OptionsView ShowHorizontalScrollBar="True" />
                        </dx:ASPxPivotGrid>    
                        <asp:HiddenField ID="hdnFilterExpression1" runat="server" Value="1 = 0" />
                        <asp:ObjectDataSource ID="odsPivotRegistration" runat="server" 
                            SelectMethod="GetvEventRegistrationPivotList" 
                            TypeName="QIS.Careq.Data.Service.BusinessLayer">
                            <SelectParameters>
                                <asp:ControlParameter Name="filterExpression" ControlID="hdnFilterExpression1" Type="String" PropertyName="Value" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:Panel>
                </dx:PanelContent>
            </PanelCollection>
        </dxcp:ASPxCallbackPanel>
        <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server" ASPxPivotGridID="pvRegistration"
            Visible="False" />
    </div>
</asp:Content>