﻿<%@ Page Title="" Language="C#" MasterPageFile="~/libs/MasterPage/MPEntry.master" AutoEventWireup="true" CodeBehind="StandardCodeEntry.aspx.cs" Inherits="QIS.Careq.Web.SystemSetup.Program.StandardCodeEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plhEntry" runat="server">
    <input type="hidden" id="hdnID" runat="server" value="" />
    <div class="pageTitle"><%=GetLabel("Standard Code")%></div>
    
    <table class="tblContentArea">
        <colgroup>
            <col style="width:50%"/>
        </colgroup>
        <tr>
            <td style="padding:5px;vertical-align:top">
                <table class="tblEntryContent" style="width:50%">
                    <colgroup>
                        <col style="width:30%"/>
                    </colgroup>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Standard Code ID")%></label></td>
                        <td>
                            <asp:TextBox ID="txtStandardCodeParentID" Width="100px" runat="server" />
                            ^
                            <asp:TextBox ID="txtStandardCodeChildID" Width="100px" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label class="lblMandatory"><%=GetLabel("Standard Code Name")%></label></td>
                        <td><asp:TextBox ID="txtStandardCodeName" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel"><label><%=GetLabel("Tag Property")%></label></td>
                        <td><asp:TextBox ID="txtTagProperty" Width="300px" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="tdLabel" valign="top" style="margin-top:5px"><label><%=GetLabel("Remarks")%></label></td>
                        <td><asp:TextBox ID="txtRemarks" Width="300px" runat="server" TextMode="MultiLine" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>