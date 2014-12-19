<%@ Page Title="" Language="C#" MasterPageFile="~/Libs/MasterPage/MPList.master" AutoEventWireup="true" 
    CodeBehind="UploadEventParticipantDt.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.UploadEventParticipantDt" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="plhCustomButtonToolbar" runat="server">
    <li id="btnEventInvitationConfirmationBack" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/back.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("View")%></div></li>
    <li id="btnUploadEventParticipantChooseFile" runat="server"><img src='<%=ResolveUrl("~/Libs/Images/Icon/back.png")%>' alt="" /><br style="clear:both"/><div><%=GetLabel("Choose File")%></div></li>
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
        });

        function onCbpViewEndCallback(s) {
            hideLoadingPanel();
        }

    </script>
    <input type="hidden" id="hdnUploadedFile1" runat="server" value="" />
    <div style="display:none">
        <asp:FileUpload ID="FileUpload1" runat="server" />
    </div>
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
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                            <asp:BoundField DataField="EmailAddress" HeaderText="Email" HeaderStyle-Width="120px" />
                            <asp:BoundField DataField="MobilePhoneNo1" HeaderText="Mobile Phone No 1" HeaderStyle-Width="120px" />
                            <asp:HyperLinkField HeaderText="Member" Text="Member" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="lnkMember" HeaderStyle-Width="100px" />
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
</asp:Content>