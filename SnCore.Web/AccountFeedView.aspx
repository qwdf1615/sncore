<%@ Page Language="C#" MasterPageFile="~/SnCore.master" AutoEventWireup="true"
 CodeFile="AccountFeedView.aspx.cs" Inherits="AccountFeedView" Title="Feed" %>

<%@ Register TagPrefix="SnCore" TagName="AccountMenu" Src="AccountMenuControl.ascx" %>
<%@ Register TagPrefix="SnCoreWebControls" Namespace="SnCore.WebControls" Assembly="SnCore.WebControls" %>
<%@ Register TagPrefix="SnCore" TagName="BookmarksView" Src="BookmarksViewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="LicenseView" Src="AccountLicenseViewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="AutoScroll" Src="AutoScrollControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="RedirectView" Src="AccountRedirectViewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="RssLink" Src="RssLinkControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="FacebookLike" Src="FacebookLikeControl.ascx" %>
<%@ Import Namespace="SnCore.Tools.Web" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
 <table cellspacing="0" cellpadding="4" class="sncore_table">
  <tr>
   <td align="center" width="150">
    <a runat="server" id="linkAccount" href="AccountView.aspx">
     <img border="0" src="AccountPictureThumbnail.aspx" runat="server" id="imageAccount" />
     <div>
      <asp:Label ID="labelAccountName" runat="server" />
     </div>
    </a>
   </td>
   <td valign="top" width="*">
    <div class="sncore_h2">
     <asp:HyperLink Target="_blank" ID="labelFeed" runat="server" Text="Feed" />
    </div>
    <div class="sncore_h2sub">
     <asp:Label ID="labelFeedDescription" runat="server" />
     <div>
      <SnCore:RedirectView id="redirect" runat="server" />
     </div>
    </div>
   </td>
   <td align="right">   
    <SnCore:RssLink ID="linkRelRss" runat="server" NavigateUrl="AccountFeedRss.aspx" />
    <asp:UpdatePanel runat="server" ID="panelAdminUpdate" UpdateMode="Conditional">
     <ContentTemplate>
      <asp:Panel ID="panelAdmin" runat="server" HorizontalAlign="Right">
       <div>
        <asp:LinkButton OnClick="feature_Click" runat="server" ID="linkFeature" Text="Feature" />
       </div>
       <div>
        <asp:LinkButton OnClick="deletefeature_Click" runat="server" ID="linkDeleteFeatures" Text="&#187; Delete Features" />
       </div>
       <div>
        <asp:LinkButton OnClick="publish_Click" runat="server" ID="linkPublish" Text="Publish" />
       </div>
       <div>
        <asp:LinkButton OnClick="publishImgs_Click" runat="server" ID="linkPublishImgs" Text="Publish Pictures" />
       </div>
       <div>
        <asp:LinkButton OnClick="publishMedia_Click" runat="server" ID="linkPublishMedia" Text="Publish Media" />
       </div>
      </asp:Panel>
     </ContentTemplate>
    </asp:UpdatePanel>
   </td>
  </tr>
 </table>
 <table class="sncore_table">
  <tr>
   <td>
    <SnCore:LicenseView runat="server" ID="licenseView" />       
   </td>
   <td class="sncore_table_tr_td" style="font-size: smaller;" align="right">
    bookmark:
   </td>
   <td class="sncore_table_tr_td">
    <SnCore:BookmarksView ID="bookmarksView" ShowThumbnail="true" runat="server" RepeatColumns="-1" />
   </td>
   <td class="sncore_table_tr_td" style="font-size: smaller;" align="right">
    <SnCore:FacebookLike ID="facebookLike" runat="server" />
   </td>
  </tr>
 </table>
 <asp:UpdatePanel runat="server" UpdateMode="Always" ID="panelGrid">
  <ContentTemplate>
   <SnCoreWebControls:PagedList CellPadding="4" runat="server" ID="gridManage" RepeatColumns="2"
    RepeatRows="3" CssClass="sncore_table" BorderWidth="0" ShowHeader="false" AllowCustomPaging="true">
    <PagerStyle cssclass="sncore_table_pager" position="TopAndBottom" nextpagetext="Next"
     prevpagetext="Prev" horizontalalign="Center" />
    <ItemTemplate>
     <div class="sncore_h2left">
      <a href='AccountFeedItemView.aspx?id=<%# Eval("Id") %>'>
       <%# base.Render(GetValue(Eval("Title"), "Untitled")) %>
      </a>
     </div>
     <div class="sncore_h2sub" style="font-size: smaller;">
      &#187; <%# base.Adjust(Eval("Created")) %>
      <a href='<%# base.Render(Eval("Link")) %>' target="_blank">
       &#187; x-posted
      </a>
      <a href='AccountFeedItemView.aspx?id=<%# Eval("Id") %>'>
       &#187; <%# GetComments((int) Eval("CommentCount"))%>
      </a>
     </div>
     <div>
      <%# GetDescription((string) Eval("Description")) %>
     </div>
    </ItemTemplate>
   </SnCoreWebControls:PagedList>
  </ContentTemplate>
 </asp:UpdatePanel>
 <SnCore:AutoScroll runat="server" ID="autoScrollToForm" />
</asp:Content>
