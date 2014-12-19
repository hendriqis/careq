<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPBase.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" 
    Inherits="QIS.Careq.Web.SystemSetup.Login" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhMPBase" runat="server">
    <script type="text/javascript">
        var loginData = '';
        $(function () {
            $('#btnLogin').click(function (evt) {
                if (IsValid(evt, 'fsLogin', 'mpLogin'))
                    cbpProcess.PerformCallback('login');
                return false;
            });

            $('#btnGo').live('click', function () {
                setTimeout(function () {
                    if (loginData == '')
                        cbpProcess.PerformCallback('getdata');
                    else {
                        openModule();
                    }
                }, 0);
            });
        });

        function openModule() {
            showLoadingPanel();
            var moduleID = 'SA';
            var link = '~/Libs/Program/RemoteLogon.aspx';
            var url = ResolveUrl(link);

            var mapForm = document.createElement("form");
            mapForm.method = "POST";
            mapForm.action = url;

            var mapInput = document.createElement("input");
            mapInput.type = "hidden";
            mapInput.name = "id";
            mapInput.value = loginData + '|' + $('#<%=ddlUserRole.ClientID %>').val();
            mapForm.appendChild(mapInput);

            document.body.appendChild(mapForm);

            mapForm.submit();

            $(mapForm).remove();
            hideLoadingPanel();
        }

        function onLnkLogoutClientClick() {
            for (var i = 0; i < listWindow.length; ++i) {
                if (!listWindow[i].closed) {
                    listWindow[i].close();
                }
            }
            listWindow = [];
            __doPostBack('<%=lnkLogout.UniqueID%>', '');
        }

        function onLoginSuccess(userName) {
            $('#<%=lblUserLoginInfo.ClientID %>').html(userName);
            cbpSelectUserRole.PerformCallback();
        }

        function onCbpSelectUserRoleEndCallback() {
            if ($('#<%=ddlUserRole.ClientID %> option').length == 1)
                openModule();
            else {
                $('#<%=loginContainerLoginInfo.ClientID %>').hide();
                $('#<%=pnlUserLoginInformation.ClientID %>').show();
                hideLoadingPanel();
            }
        }
    </script>
    <div class="loginBg borderBox"><%=GetApplicationName()%></div>
    <div id="loginContainerLoginInfo" class="loginContainerLoginInfo" runat="server">
        <center>
            <fieldset id="fsLogin">     
                <table cellpadding="2">
                    <tr>
                        <td>User ID</td>
                        <td><asp:TextBox ID="txtUserName" Width="200px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Password</td>
                        <td><asp:TextBox ID="txtPassword" TextMode="Password" Width="200px" runat="server" /></td>
                        <td><input type="submit" value="Log In" id="btnLogin" /></td>
                    </tr>
                </table>
            </fieldset>
        </center>
    </div>
    <div runat="server" id="pnlUserLoginInformation">
        <div class="borderBox" style="float:right; color: #666;">
            <dxcp:ASPxCallbackPanel ID="cbpSelectUserRole" runat="server" Width="80%" ClientInstanceName="cbpSelectUserRole"
                ShowLoadingPanel="false" OnCallback="cbpSelectUserRole_Callback">
                <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                    EndCallback="function(s,e){ onCbpSelectUserRoleEndCallback(); }" />
                <PanelCollection>
                    <dx:PanelContent ID="pnlSelectUserRole" runat="server">    
                        <table>
                            <tr>
                                <td style="width:150px;">User Role</td>
                                <td><asp:DropDownList ID="ddlUserRole" runat="server" Width="200px" /></td>
                                <td></td>
                                <td><input type="button" value="Go" id="btnGo" /></td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dxcp:ASPxCallbackPanel>
        </div>
        <div class="loginBg borderBox pnlUserLoginInformation">
            <asp:LinkButton ID="lnkLogout" CssClass="lnkLogout" Text="[Logout]" OnClick="lnkLogout_Click" OnClientClick="onLnkLogoutClientClick();" runat="server" /> 
            <%=GetLabel("Welcome")%>, <span class="userlogininfo" runat="server" id="lblUserLoginInfo"></span>
        </div>    
        <div style="display:none">        
            <dxcp:ASPxCallbackPanel ID="cbpProcess" runat="server" Width="100%" ClientInstanceName="cbpProcess"
                ShowLoadingPanel="false" OnCallback="cbpProcess_Callback">
                <ClientSideEvents BeginCallback="function(s,e){ showLoadingPanel(); }" 
                    EndCallback="function(s,e){
                        if(s.cpParam == 'login'){
                            var result = s.cpResult.split('|');
                            if(result[0] == 'success'){
                                loginData = s.cpLoginData;
                                onLoginSuccess(result[1]);
                            }
                            else {
                                alert(result[1]);
                                hideLoadingPanel();
                            }
                        }
                        else {
                            loginData = s.cpLoginData;
                            openModule();
                        }
                    }" />
            </dxcp:ASPxCallbackPanel>
        </div>
    </div>  
    <center>
        <div style="margin: 60px 0 100px 0;height:336px;">
            <img src='<%=ResolveUrl("~/Libs/Images/pqm_logo.jpg")%>' alt="" />
        </div>
    </center>
    <div id="loginFooter" class="loginBg borderBox" >
        <div>2013 © PT. Quantum Infra Solusindo</div>
    </div>
</asp:Content>
