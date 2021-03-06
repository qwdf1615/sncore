<%@ Page Language="C#" MasterPageFile="~/SnCore.master" AutoEventWireup="true" CodeFile="SiteMap.aspx.cs"
 Inherits="SiteMap2" Title="Site Map" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
 <div class="sncore_h2">
  Site Map
 </div>
 <table cellpadding="0" cellspacing="0" class="sncore_table">
  <tr>
   <td valign="top" width="50%">
    <ul>
     <li><a href="Default.aspx">Home</a></li>
     <ul>
      <li><a id="linkBlog" runat="server">Blog</a></li>
      <li><a href="AccountsView.aspx">People</a></li>
      <ul><li>
      <asp:DataList RepeatLayout="Flow" RepeatDirection="Horizontal" ID="listAccounts" runat="server">
       <ItemStyle CssClass="sncore_link" />
       <ItemTemplate>
        <a href='AccountsViewList.aspx?PageNumber=<%# Eval("PageNumber") %>&PageSize=<%# Eval("PageSize") %>&PageCount=<%# Eval("PageCount") %>'>
         <%# Eval("Start") %>-<%# (int) Eval("Start") + (int) Eval("Count") - 1 %>
        </a>
       </ItemTemplate>
      </asp:DataList>
      </li></ul>
      <li><a href="AccountFeedItemsView.aspx">Blogs</a></li>
      <ul><li>
      <asp:DataList RepeatLayout="Flow" RepeatDirection="Horizontal" ID="listFeeds" runat="server">
       <ItemStyle CssClass="sncore_link" />
       <ItemTemplate>
        <a href='AccountFeedsViewList.aspx?PageNumber=<%# Eval("PageNumber") %>&PageSize=<%# Eval("PageSize") %>&PageCount=<%# Eval("PageCount") %>'>
         <%# Eval("Start") %>-<%# (int) Eval("Start") + (int) Eval("Count") - 1 %>
        </a>
       </ItemTemplate>
      </asp:DataList>
      </li></ul>
      <li><a href="AccountFeedItemImgsView.aspx">Feed Pictures</a></li>
      <li><a href="AccountFeedItemMediasView.aspx">Feed Media</a></li>
      <li><a href="PlacesView.aspx">Places</a></li>
      <ul><li>
      <asp:DataList RepeatLayout="Flow" RepeatDirection="Horizontal" ID="listPlaces" runat="server">
       <ItemStyle CssClass="sncore_link" />
       <ItemTemplate>
        <a href='PlacesViewList.aspx?PageNumber=<%# Eval("PageNumber") %>&PageSize=<%# Eval("PageSize") %>&PageCount=<%# Eval("PageCount") %>'>
         <%# Eval("Start") %>-<%# (int) Eval("Start") + (int) Eval("Count") - 1 %>
        </a>
       </ItemTemplate>
      </asp:DataList>
      </li></ul>
      <li><a href="PlacesFavoritesView.aspx">Favorite Places</a></li>
      <li><a href="AccountEventsToday.aspx">Today's Events</a></li>
      <li><a href="AccountEventsView.aspx">Events</a></li>
      <ul><li>
      <asp:DataList RepeatLayout="Flow" RepeatDirection="Horizontal" ID="listAccountEvents" runat="server">
       <ItemStyle CssClass="sncore_link" />
       <ItemTemplate>
        <a href='AccountEventsViewList.aspx?PageNumber=<%# Eval("PageNumber") %>&PageSize=<%# Eval("PageSize") %>&PageCount=<%# Eval("PageCount") %>'>
         <%# Eval("Start") %>-<%# (int) Eval("Start") + (int) Eval("Count") - 1 %>
        </a>
       </ItemTemplate>
      </asp:DataList>
      </li></ul>
      <li><a href="AccountStoriesView.aspx">Stories</a></li>
      <ul><li>
      <asp:DataList RepeatLayout="Flow" RepeatDirection="Horizontal" ID="listStories" runat="server">
       <ItemStyle CssClass="sncore_link" />
       <ItemTemplate>
        <a href='AccountStoriesViewList.aspx?PageNumber=<%# Eval("PageNumber") %>&PageSize=<%# Eval("PageSize") %>&PageCount=<%# Eval("PageCount") %>'>
         <%# Eval("Start") %>-<%# (int) Eval("Start") + (int) Eval("Count") - 1 %>
        </a>
       </ItemTemplate>
      </asp:DataList>
      </li></ul>
      <li><a href="DiscussionsView.aspx">Discussions</a></li>
      <li><a href="DiscussionTopOfThreadsView.aspx">Latest Discussion Threads</a></li>
      <li><a href="DiscussionThreadsView.aspx">Latest Discussion Posts</a></li>
      <ul>
       <asp:Repeater ID="listDiscussions" runat="server">
        <ItemTemplate>
         <a href='DiscussionView.aspx?id=<%# Eval("Id") %>'>
          <li><%# base.Render(Eval("Name")) %></li>
         </a>
        </ItemTemplate>
       </asp:Repeater>
      </ul>
      <li><a href="AccountGroupsView.aspx">Groups</a></li>
      <ul><li>
      <asp:DataList RepeatLayout="Flow" RepeatDirection="Horizontal" ID="listAccountGroups" runat="server">
       <ItemStyle CssClass="sncore_link" />
       <ItemTemplate>
        <a href='AccountGroupsViewList.aspx?PageNumber=<%# Eval("PageNumber") %>&PageSize=<%# Eval("PageSize") %>&PageCount=<%# Eval("PageCount") %>'>
         <%# Eval("Start") %>-<%# (int) Eval("Start") + (int) Eval("Count") - 1 %>
        </a>
       </ItemTemplate>
      </asp:DataList>
      </li></ul>      
      <li><b>Surveys</b></li>
      <ul>
       <asp:DataList RepeatLayout="Flow" RepeatDirection="Horizontal" ID="listSurveys" runat="server">
        <ItemTemplate>
         <li>
          <a href='AccountSurveyView.aspx?id=<%# Eval("Id") %>'>
           <%# base.Render(Eval("Name")) %>
          </a>
        </ItemTemplate>
       </asp:DataList>
      </ul>
      <li><b>Featured</b></li>
      <ul>
       <li><a href="NewContent.aspx">New Content</a></li>
       <li><a href="FeaturedAccountFeedsView.aspx">Blogs</a></li>
       <li><a href="FeaturedAccountFeedItemsView.aspx">Blog Posts</a></li>
       <li><a href="FeaturedAccountsView.aspx">People</a></li>
       <li><a href="FeaturedPlacesView.aspx">Places</a></li>
       <li><a href="FeaturedAccountEventsView.aspx">Events</a></li>
       <li><a href="FeaturedAccountGroupsView.aspx">Groups</a></li>
      </ul>
      <li><b>RSS Syndication</b></li>
      <ul>
       <li><a href="AccountsRss.aspx">People</a></li>
       <li><a href="PlacesRss.aspx">Places</a></li>
       <li><a href="AccountFeedsRss.aspx">Blogs</a></li>
       <li><a href="AccountStoriesRss.aspx">Stories</a></li>
       <li><a href="AccountEventsRss.aspx">Events</a></li>
       <li><a href="AccountGroupsRss.aspx">Public Groups</a></li>
       <li><a href="DiscussionTopOfThreadsRss.aspx">Discussion Threads</a></li>
       <li><a href="DiscussionThreadsRss.aspx">Discussion Posts</a></li>
       <li><a href="AccountFeedItemsRss.aspx">Aggregated Blogs</a></li>
       <li><a href="AccountFeedItemImgsRss.aspx">Aggregated Blog Pictures</a></li>
       <li><a href="AccountFeedItemMediasRss.aspx">Aggregated Blog Media</a></li>
       <li><a href="FeaturedAccountsRss.aspx">Featured People</a></li>
       <li><a href="FeaturedPlacesRss.aspx">Featured Places</a></li>
       <li><a href="FeaturedAccountFeedsRss.aspx">Featured Blogs</a></li>
       <li><a href="FeaturedAccountFeedItemsRss.aspx">Featured Blog Posts</a></li>
       <li><a href="FeaturedAccountEventsRss.aspx">Featured Events</a></li>
       <li><a href="FeaturedAccountGroupsRss.aspx">Featured Groups</a></li>
      </ul>
      <li><a href="Search.aspx">Search</a></li>
     </ul>
    </ul>
   </td>
   <td valign="top" width="50%">
    <ul>    
     <li><b>Member Services</b></li>
     <ul>
      <li><a href="AccountCreate.aspx">Join</a></li>
      <li><a href="AccountLogin.aspx">Login</a></li>
      <li><a href="AccountManage.aspx">Me Me</a></li>
      <li><a href="AccountPreferencesManage.aspx">Preferences</a></li>
      <li><a href="AccountChangePassword.aspx">Change Password</a></li>
      <li><a href="AccountView.aspx">Preview Me</a></li>
      <li><a href="AccountPicturesManage.aspx">Pictures</a></li>
      <li><a href="AccountEmailsManage.aspx">E-Mails</a></li>
      <li><a href="AccountOpenIdsManage.aspx">Open Id</a></li>
      <li><a href="AccountWebSitesManage.aspx">Websites</a></li>
      <li><a href="AccountLicenseEdit.aspx">License</a></li>
      <li><a href="AccountPlaceFavoritesManage.aspx">Favorite Places</a></li>
      <li><a href="AccountPlacesManage.aspx">Places</a></li>
      <li><a href="AccountPlaceChangeRequestsManage.aspx">Change Requests</a></li>
      <li><a href="AccountPlaceQueueManage.aspx">Queue</a></li>
      <li><a href="PlaceFriendsQueueView.aspx">Friends Queue</a></li>
      <li><a href="AccountFriendsManage.aspx">Friends</a></li>
      <li><a href="AccountInvitationsManage.aspx">Invite Friends</a></li>
      <li><a href="AccountAuditEntriesManage.aspx">Broadcasts</a></li>
      <li><a href="AccountFriendAuditEntriesView.aspx">Friends Activity</a></li>
      <li><a href="AccountFriendRequestsManage.aspx">Pending Friend Requests</a></li>
      <li><a href="AccountFriendRequestsSentManage.aspx">Sent Friend Requests</a></li>
      <li><a href="AccountStoriesManage.aspx">Stories</a></li>
      <li><a href="AccountSurveysManage.aspx">Surveys</a></li>
      <li><a href="AccountFeedsManage.aspx">Syndication</a></li>
      <li><a href="AccountBlogsManage.aspx">Blogs</a></li>
      <li><a href="AccountMessageFoldersManage.aspx?folder=inbox">Communications</a></li>
      <ul>
       <li><a href="AccountMessageFoldersManage.aspx?folder=inbox">Inbox</a></li>
       <li><a href="AccountMessageFoldersManage.aspx?folder=sent">Sent</a></li>
       <li><a href="AccountMessageFoldersManage.aspx?folder=trash">Trash</a></li>
      </ul>
      <li><a href="AccountRedirectsManage.aspx">Redirects</a></li>
      <li><a href="AccountFlagsManage.aspx">Reported Abuse</a></li>
      <li><a href="AccountDelete.aspx">Leave</a></li>
     </ul>
     <li><a href="Help.aspx">Help</a></li>
      <ul>
       <li><a href="docs/html/faq.html">Frequently Asked Questions</a></li>
       <li><a runat="server" id="linkSiteDiscussion" href="DiscussionView.aspx?id=0">Site Discussion</a></li>
       <li><a runat="server" id="linkReportBug" href="BugEdit.aspx?pid=0&type=Bug">Report a Bug</a></li>
       <li><a runat="server" id="linkSuggestFeature" href="BugEdit.aspx?pid=0&type=Suggestion">Suggest a Feature</a></li>
       <li><a href="Services.htm">Web Services</a></li>
       <li><a href="docs/html/index.html">Developer Documentation</a></li>
       <li><a href="About.aspx">About</a></li>
      </ul>
     <li><a href="SystemStatsHits.aspx">Statistics</a></li>
      <ul>
       <li><a href="RefererAccountsView.aspx">Top Traffickers</a></li>
       <li><a href="AccountCitiesView.aspx">Top Cities</a></li>
       <li><a href="SystemStatsHits.aspx">Page Hits</a></li>
       <li><a href="SystemRefererHosts.aspx">Referer Hosts</a></li>
       <li><a href="SystemRefererQueries.aspx">Referer Queries</a></li>
       <li><a href="SystemStatsCache.aspx">Front-End Cache</a></li>
      </ul>
      <li><a href="TermsOfUse.aspx">Terms of Use</a></li>
      <li><a href="PrivacyPolicy.aspx">Privacy Policy</a></li>
      <li><a href="CodeOfConduct.aspx">Code of Conduct</a></li>
    </ul>
   </td>
  </tr>
 </table>
</asp:Content>
