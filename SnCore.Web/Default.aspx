<%@ Page Language="C#" MasterPageFile="~/SnCore.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
 Inherits="_Default" Title="Welcome" %>

<%@ Import Namespace="SnCore.Tools.Web" %>
<%@ Register TagPrefix="SnCore" TagName="Title" Src="TitleControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="AccountMenu" Src="AccountMenuControl.ascx" %>
<%@ Register TagPrefix="SnCoreWebControls" Namespace="SnCore.WebControls" Assembly="SnCore.WebControls" %>
<%@ Register TagPrefix="SnCore" TagName="AccountsNewView" Src="AccountsNewViewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="PlacesNewView" Src="PlacesNewViewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="AccountsActiveView" Src="AccountsActiveViewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="DiscussionPostsNewView" Src="DiscussionPostsNewViewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="AccountFeedItemsNewView" Src="AccountFeedItemsNewViewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="AccountReminder" Src="AccountReminderControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="FeedPreview" Src="AccountFeedPreviewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="SearchDefault" Src="SearchDefaultControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="BookmarksView" Src="BookmarksViewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="Featured" Src="FeaturedViewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="BlogView" Src="AccountBlogViewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="AccountFeedItemsFeaturedView" Src="AccountFeedItemsFeaturedViewControl.ascx" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
 <table width="100%" cellpadding="0" cellspacing="0">
  <tr>
   <td valign="top" width="*">
    <SnCore:BlogView ID="websiteBlog" runat="server">
     <ContentLinkIds>
      <asp:ListItem>SnCore.PressContentGroup.Id</asp:ListItem>
      <asp:ListItem>SnCore.AddContentGroup.Id</asp:ListItem>
     </ContentLinkIds>
    </SnCore:BlogView>
    <SnCore:AccountFeedItemsFeaturedView id="featuredAccountFeedItems" runat="server" />
    <SnCore:Featured id="featured" runat="server" />
   </td>
   <td valign="top" width="333">
    <div id="panelRightFront">
     <!-- NOEMAIL-START -->
     <SnCore:SearchDefault runat="server" ID="searchDefault" />
     <!-- NOEMAIL-END -->
     <SnCore:PlacesNewView ID="placesNewMain" runat="server" Count="2" />
     <SnCore:AccountsNewView ID="accountsNewMain" runat="server" Count="2" />
     <SnCore:DiscussionPostsNewView ID="discussionsNewMain" runat="server" />
    </div>
   </td>
  </tr>
 </table>
</asp:Content>
