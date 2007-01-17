<%@ Page Language="C#" MasterPageFile="~/SnCoreAccount.master" AutoEventWireup="true" CodeFile="SystemPreferencesManage.aspx.cs"
 Inherits="SystemPreferencesManage" Title="System Preferences" %>
 
<asp:Content ID="Content" ContentPlaceHolderID="AccountContentPlaceHolder" runat="Server">
 <table class="sncore_account_table">
  <tr>
   <td valign="top" width="50%">
    <div class="sncore_h2">
     System Settings
    </div>
    <table>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemConfigurationsManage.aspx">Settings</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemSurveysManage.aspx">Surveys</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemPicturesManage.aspx">Pictures</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemPictureTypesManage.aspx">Picture Types</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemAccountEventTypesManage.aspx">Event&nbsp;Types</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemRemindersManage.aspx">Reminders</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="FeedTypesManage.aspx">Feed Types</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemBookmarksManage.aspx">Bookmarks</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="AccountMadLibsManage.aspx">Mad Libs</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="TagWordsManage.aspx">Tag&nbsp;Words</a></td></tr>
    </table>
    <table>
     <tr><td width="30" align="center"><img src="images/account/contentgroups.gif" /></td><td width="*"><a href="AccountContentGroupsManage.aspx">Content</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/property.gif" /></td><td width="*"><a href="MarketingCampaignsManage.aspx">Campaigns</a></td></tr>
    </table>
    <table>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemInvitationsManage.aspx">Invitations</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemAccountPropertyGroupsManage.aspx">Property Groups</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemAttributesManage.aspx">Attributes</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemAccountRedirectsManage.aspx">Redirects</a></td></tr>
    </table>
    <div class="sncore_h2">
     Geography
    </div>
    <table>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemCountriesManage.aspx">Countries</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemStatesManage.aspx">States</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemCitiesManage.aspx">Cities</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemNeighborhoodsManage.aspx">Neighborhoods</a></td></tr>
    </table>
   </td>
   <td valign="top" width="50%">
    <div class="sncore_h2">
     Places
    </div>
    <table>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="PlacesManage.aspx">Places</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="PlaceTypesManage.aspx">Place Types</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemAccountPlaceTypesManage.aspx">Property Types</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemAccountPlaceRequestsManage.aspx">Property Requests</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/settings.gif" /></td><td width="*"><a href="SystemPlacePropertyGroupsManage.aspx">Property Groups</a></td></tr>
    </table>
    <div class="sncore_h2">
     Bugs
    </div>
    <table>
     <tr><td width="30" align="center"><img src="images/account/prefs.gif" /></td><td width="*"><a href="BugProjectsManage.aspx">Bug Projects</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/star.gif" /></td><td width="*"><a href="BugPrioritiesManage.aspx">Priorities</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/star.gif" /></td><td width="*"><a href="BugSeveritiesManage.aspx">Severities</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/star.gif" /></td><td width="*"><a href="BugResolutionsManage.aspx">Resolutions</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/star.gif" /></td><td width="*"><a href="BugStatusesManage.aspx">Statuses</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/star.gif" /></td><td width="*"><a href="BugTypesManage.aspx">Types</a></td></tr>
    </table>
    <div class="sncore_h2">
     Statistics
    </div>
    <table>
     <tr><td width="30" align="center"><img src="images/account/star.gif" /></td><td width="*"><a href="SystemStatsHits.aspx">Hit Stats</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/star.gif" /></td><td width="*"><a href="SystemRefererHosts.aspx">Referer Hosts</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/star.gif" /></td><td width="*"><a href="SystemRefererQueries.aspx">Referer Queries</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/star.gif" /></td><td width="*"><a href="SystemRefererHostDupsManage.aspx">Host Dups</a></td></tr>
     <tr><td width="30" align="center"><img src="images/account/star.gif" /></td><td width="*"><a href="SystemRefererAccountsManage.aspx">Referer Accounts</a></td></tr>
    </table>
   </td>
  </tr>
 </table>
</asp:Content>