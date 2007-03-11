<%@ Page Language="C#" MasterPageFile="~/SnCore.master" AutoEventWireup="true" CodeFile="AccountGroupView.aspx.cs"
 Inherits="AccountGroupView" Title="Account Group | View" %>
<%@ Import Namespace="SnCore.Tools.Web" %>

<%@ Register TagPrefix="SnCore" TagName="AccountMenu" Src="AccountMenuControl.ascx" %>
<%@ Register TagPrefix="SnCoreWebControls" Namespace="SnCore.WebControls" Assembly="SnCore.WebControls" %>
<%@ Register TagPrefix="SnCore" TagName="CounterView" Src="CounterViewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="TellAFriend" Src="TellAFriendControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="RedirectView" Src="AccountRedirectViewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="BookmarksView" Src="BookmarksViewControl.ascx" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
 <asp:Panel CssClass="panel" ID="pnlAccountGroup" runat="server">
  <table cellspacing="0" cellpadding="4" class="sncore_table">
   <tr>
    <td class="sncore_table_tr_td" style="text-align: center; vertical-align: top; width: 100px;">
     <asp:Panel CssClass="sncore_nopicture_table" ID="accountNoPicture" runat="server" Visible="false">
      <img border="0" src="images/AccountGroupThumbnail.gif" />
     </asp:Panel>
     <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="panelPictures">
      <ContentTemplate>
       <SnCoreWebControls:PagedList runat="server" ID="picturesView" RepeatColumns="1" RepeatRows="5" AllowCustomPaging="true">
        <PagerStyle cssclass="sncore_table_pager" position="TopAndBottom" nextpagetext="&#187;"
         prevpagetext="&#171;" horizontalalign="Center" />
        <ItemTemplate>
         <a href='<%# string.Format("AccountGroupPictureView.aspx?id={0}", Eval("Id")) %>'>
          <img border="0" src='<%# string.Format("AccountGroupPictureThumbnail.aspx?id={0}", Eval("Id")) %>' alt='<%# base.Render(Eval("Name")) %>' />
          <div style="font-size: smaller;">
           <%# ((int) Eval("CommentCount") >= 1) ? Eval("CommentCount").ToString() + 
            " comment" + (((int) Eval("CommentCount") == 1) ? "" : "s") : "" %>
          </div>
         </a>
        </ItemTemplate>
       </SnCoreWebControls:PagedList>
      </ContentTemplate>
     </asp:UpdatePanel>
    </td>
    <td valign="top" width="*">
     <table class="sncore_inner_table" width="95%">
      <tr>
       <td class="sncore_table_tr_td">
        <asp:Label CssClass="sncore_account_name" ID="accountgroupName" runat="server" />
        <div>
         <SnCore:RedirectView id="redirect" runat="server" />
        </div>
        <div class="sncore_description">
         <asp:Label ID="accountgroupDescription" runat="server" />
        </div>
        <!-- NOEMAIL-START -->
         <div class="sncore_description">
          group views: <SnCore:CounterView ID="counterAccountGroupViews" runat="server" />
         </div>
        <!-- NOEMAIL-END -->
      </tr>
      <tr>
       <td valign="top">
       </td>
       <td class="sncore_table_tr_td" style="text-align: right;">
        <div>
         <asp:LinkButton ID="linkRequest" runat="server" Text="&#187; Join this Group" OnClick="linkRequest_Click" />
        </div>
        <div>
         <SnCore:TellAFriend ID="linkTellAFriend" runat="server" />
        </div>
        <asp:Panel ID="panelAdmin" runat="server">
         <div>
          <asp:HyperLink ID="linkRequests" runat="server" Text="&#187; Membership Requests" 
           NavigateUrl="AccountGroupAccountRequestsManage.aspx" />
         </div>
        </asp:Panel>
       </td>
      </tr>
     </table>
     <table class="sncore_inner_table" width="95%">
      <tr>
       <td class="sncore_table_tr_td" style="font-size: smaller;" align="right">
        socially bookmark this group:
       </td>
       <td class="sncore_table_tr_td">
        <SnCore:BookmarksView ID="bookmarksView" ShowThumbnail="true" runat="server" RepeatColumns="-1" />
       </td>
      </tr>
     </table>
    </td>
   </tr>
  </table>
 </asp:Panel>
</asp:Content>