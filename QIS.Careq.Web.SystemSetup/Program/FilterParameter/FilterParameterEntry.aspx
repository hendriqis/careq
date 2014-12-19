<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
CodeBehind="FilterParameterEntry.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.FilterParameterEntry" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function onCboFilterParameterTypeValueChanged(val) {
            var isEnabled = (val == 'X108^001' || val == 'X108^002');
            if (!isEnabled) {
                $('#<%=txtMethodName.ClientID %>').attr('readonly', 'readonly');
                $('#<%=txtFilterExpression.ClientID %>').attr('readonly', 'readonly');
                $('#<%=txtValueFieldName.ClientID %>').attr('readonly', 'readonly');
                $('#<%=txtTextFieldName.ClientID %>').attr('readonly', 'readonly');
            }
            else {
                $('#<%=txtMethodName.ClientID %>').removeAttr('readonly');
                $('#<%=txtFilterExpression.ClientID %>').removeAttr('readonly');
                $('#<%=txtValueFieldName.ClientID %>').removeAttr('readonly');
                $('#<%=txtTextFieldName.ClientID %>').removeAttr('readonly');
            }

            var isEnabled = (val != 'X108^006');
            if (!isEnabled)
                $('#<%=txtFieldName.ClientID %>').attr('readonly', 'readonly');            
            else
                $('#<%=txtFieldName.ClientID %>').removeAttr('readonly');
            
        }
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <div class="pageTitle"><%=GetLabel("Filter Parameter")%></div>
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Filter Parameter Code")%></label></td>
                        <td><asp:TextBox ID="txtFilterParameterCode" Width="100px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Filter Parameter Name")%></label></td>
                        <td><asp:TextBox ID="txtFilterParameterName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Filter Parameter Caption")%></label></td>
                        <td><asp:TextBox ID="txtFilterParameterCaption" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Filter Parameter Type")%></label></td>
                        <td>
                            <dxe:ASPxComboBox ID="cboFilterParameterType" Width="300px" runat="server">
                                <ClientSideEvents Init="function(s,e){ onCboFilterParameterTypeValueChanged(s.GetValue()); }"
                                    ValueChanged="function(s,e){ onCboFilterParameterTypeValueChanged(s.GetValue()); }" />
                            </dxe:ASPxComboBox>                        
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Field Name")%></label></td>
                        <td><asp:TextBox ID="txtFieldName" Width="300px" runat="server" /></td>
                    </tr>
                </table>
            </td>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Method Name")%></label></td>
                        <td><asp:TextBox ID="txtMethodName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Filter Expression")%></label></td>
                        <td><asp:TextBox ID="txtFilterExpression" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Value Field Name")%></label></td>
                        <td><asp:TextBox ID="txtValueFieldName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Text Field Name")%></label></td>
                        <td><asp:TextBox ID="txtTextFieldName" Width="300px" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
