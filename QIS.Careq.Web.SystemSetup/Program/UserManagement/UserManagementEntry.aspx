<%@ Page Language="C#" MasterPageFile="~/Libs/MasterPage/MPEntry.master" AutoEventWireup="true" 
    CodeBehind="UserManagementEntry.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.UserManagementEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <script type="text/javascript">
        function checkConfirmPassword(value, element) {
            return value == $('#<%=txtPassword.ClientID %>').val();
        }
        function checkConfirmMobilePIN(value, element) {
            return value == $('#<%=txtMobilePIN.ClientID %>').val();
        }

        jQuery.validator.addMethod("confirmpassword", checkConfirmPassword, "");
        jQuery.validator.addMethod("confirmmobilepin", checkConfirmMobilePIN, "");
    </script>
    <input type="hidden" id="hdnID" runat="server" value="" />
    <div class="pageTitle"><%=GetLabel("User")%></div>
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:100%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("User Name")%></label></td>
                        <td><asp:TextBox ID="txtUserName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Email")%></label></td>
                        <td><asp:TextBox ID="txtEmail" Width="100%" CssClass="email" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Email Password")%></label></td>
                        <td><asp:TextBox ID="txtEmailPassword" TextMode="Password" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Password")%></label></td>
                        <td><asp:TextBox ID="txtPassword" Width="100%" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Confirm Password")%></label></td>
                        <td><asp:TextBox ID="txtConfirmPassword" CssClass="confirmpassword" Width="100%" runat="server" /></td>
                    </tr>
                    <tr style="display:none">
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Mobile PIN")%></label></td>
                        <td><asp:TextBox ID="txtMobilePIN" Width="300px" runat="server" /></td>
                    </tr>
                    <tr style="display:none">
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Confirm Mobile PIN")%></label></td>
                        <td><asp:TextBox ID="txtConfirmMobilePIN" CssClass="confirmmobilepin" Width="300px" runat="server" /></td>
                    </tr>
                    <tr style="display:none">
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Security Question")%></label></td>
                        <td><asp:TextBox ID="txtSecurityQuestion" Width="100%" runat="server" /></td>
                    </tr>
                    <tr style="display:none">
                        <td class="tdLabel"><label class="lblNormal"><%=GetLabel("Security Answer")%></label></td>
                        <td><asp:TextBox ID="txtSecurityAnswer" Width="100%" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
