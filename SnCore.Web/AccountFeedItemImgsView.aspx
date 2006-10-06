<%@ Page Language="C#" MasterPageFile="~/SnCore.master" AutoEventWireup="true" CodeFile="AccountFeedItemImgsView.aspx.cs"
 Inherits="AccountFeedItemImgsView" Title="Pictures" %>

<%@ Import Namespace="SnCore.Services" %>
<%@ Import Namespace="SnCore.Tools.Web" %>
<%@ Register TagPrefix="SnCoreWebControls" Namespace="SnCore.WebControls" Assembly="SnCore.WebControls" %>
<%@ Register TagPrefix="SnCore" TagName="AccountContentGroupLink" Src="AccountContentGroupLinkControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <atlas:UpdatePanel ID="panelLinks" Mode="Conditional" RenderMode="Inline" runat="server">
  <ContentTemplate>
   <table cellpadding="0" cellspacing="0" width="784">
    <tr>
     <td>
      <div class="sncore_h2">
       Pictures
      </div>
      <div class="sncore_h2sub">
       <a href="AccountFeedItemsView.aspx">&#187; Blogs</a>
       <a href="AccountFeedItemsView.aspx">&#187; Content</a>
       <a href="TellAFriend.aspx?Url=<% Response.Write(Renderer.UrlEncode(Request.Url.PathAndQuery)); %>&Subject=<% Response.Write(Renderer.UrlEncode(Title)); %>">&#187; Tell a Friend</a>     
       <SnCore:AccountContentGroupLink ID="linkAddGroup" runat="server" ConfigurationName="SnCore.AddContentGroup.Id" />       
      </div>
     </td>
     <td>
      <asp:Label ID="labelCount" runat="server" CssClass="sncore_h2sub" />
     </td>
     <td align="right" valign="middle">
      <asp:HyperLink runat="server" ID="linkRss" ImageUrl="images/rss.gif" NavigateUrl="AccountFeedItemImgsRss.aspx" />
      <link runat="server" id="linkRelRss" rel="alternate" type="application/rss+xml" title="Rss"
       href="AccountFeedItemImgsRss.aspx" />
     </td>
    </tr>
   </table>
  </ContentTemplate>
 </atlas:UpdatePanel>
 <atlas:UpdatePanel runat="server" ID="panelGrid" Mode="Conditional" RenderMode="Inline">
  <ContentTemplate>
   <SnCoreWebControls:PagedList CellPadding="4" runat="server" ID="gridManage"
    AllowCustomPaging="true" RepeatColumns="4" RepeatRows="4" RepeatDirection="Horizontal"
    CssClass="sncore_table" ShowHeader="false" OnItemCommand="gridManage_ItemCommand"
    OnDataBinding="gridManage_DataBinding">
    <PagerStyle cssclass="sncore_table_pager" position="TopAndBottom" nextpagetext="Next"
     prevpagetext="Prev" horizontalalign="Center" />
    <ItemStyle CssClass="sncore_description" HorizontalAlign="Center" Width="25%" />
    <ItemTemplate>
     <a href="AccountFeedItemView.aspx?id=<%# Eval("AccountFeedItemId") %>">
      <img border="0" src="AccountFeedItemImgThumbnail.aspx?id=<%# Eval("Id") %>" alt='<%# base.Render(Eval("Description")) %>' />
     </a>
     <div>
      x-posted in 
      <a href="AccountFeedView.aspx?id=<%# Eval("AccountFeedId") %>">
       <%# base.Render(Eval("AccountFeedName")) %>
      </a>
     </div>
     <div>
      <a href="AccountFeedItemView.aspx?id=<%# Eval("AccountFeedItemId") %>">
       <%# base.Render(Eval("AccountFeedItemTitle")) %>
      </a>    
     </div>
     <div>
      <atlas:UpdatePanel ID='panelShowHide' Mode="Conditional" runat="Server">
       <ContentTemplate>
        <asp:LinkButton Text='<%# (bool) Eval("Visible") ? "&#187; Hide" : "&#187; Show" %>' ID="linkToggleVisible" runat="server"
         Visible='<%# base.SessionManager.IsAdministrator %>' CommandName="Toggle" CommandArgument='<%# Eval("Id") %>' />
       </ContentTemplate>
      </atlas:UpdatePanel>
     </div>
     <br />
    </ItemTemplate>
   </SnCoreWebControls:PagedList>
  </ContentTemplate>
 </atlas:UpdatePanel>   
</asp:Content>
