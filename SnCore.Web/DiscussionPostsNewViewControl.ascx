<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DiscussionPostsNewViewControl.ascx.cs"
 Inherits="DiscussionPostsNewViewControl" %>
<%@ Import Namespace="SnCore.Tools.Web" %>
<%@ Register TagPrefix="SnCore" TagName="RssLink" Src="RssLinkControl.ascx" %>
<table cellpadding="0" cellspacing="0" class="sncore_half_inner_table">
 <tr>
  <td>
   <div class="sncore_h2">
    <a href="DiscussionsView.aspx">
      New Discussion Posts
     <img src="images/site/right.gif" border="0" />
    </a>
   </div>
  </td>
 </tr>
 <tr>
  <td>
   <div class="sncore_createnew">
    <div class="sncore_link">
     <a href="DiscussionsView.aspx">&#187; post new</a>
     <a href="DiscussionTopOfThreadsView.aspx">&#187; new threads</a>
     <a href="DiscussionThreadsView.aspx">&#187; new posts</a>
     <a href="DiscussionsView.aspx">&#187; all</a>
     <a href="DiscussionThreadsRss.aspx">&#187; rss</a>
    </div>
   </div>
  </td>
 </tr>
</table>
<SnCore:RssLink ID="linkRelThreadsRss" runat="server" NavigateUrl="DiscussionThreadsRss.aspx" 
 Title="New Discussion Posts" ButtonVisible="false" />
<asp:DataGrid CellPadding="4" ShowHeader="false" runat="server" ID="discussionView"
 AutoGenerateColumns="false" CssClass="sncore_half_table">
 <ItemStyle CssClass="sncore_table_tr_td" />
 <Columns>
  <asp:BoundColumn DataField="Id" Visible="false" />
  <asp:TemplateColumn>
   <ItemTemplate>
    <table cellpadding="0" cellspacing="0" width="100%">
     <tr>
      <td width="*">
       <SnCore:RssLink ID="linkRelRss" runat="server" NavigateUrl='<%# string.Format("DiscussionRss.aspx?id={0}", Eval("DiscussionId")) %>' 
        ButtonVisible="false" Title='<%# base.Render(Eval("DiscussionName"))%>' />
       <div class="sncore_title">
        <a href='DiscussionView.aspx?id=<%# Eval("DiscussionId") %>'>
         <%# base.Render(Eval("DiscussionName"))%> 
         <span class="sncore_link">
          <a href='DiscussionView.aspx?id=<%# Eval("DiscussionId") %>'>&#187; read</a>
          <a href="DiscussionPost.aspx?did=<%# Eval("DiscussionId") %>&ReturnUrl=<%# 
           Renderer.UrlEncode(Request.Url.PathAndQuery) %>">&#187; post new</a>
          <a href='DiscussionRss.aspx?id=<%# Eval("DiscussionId") %>'>
           &#187; rss</a>
         </span>
        </a>
       </div>
       <div class="sncore_description">
        <a href='DiscussionThreadView.aspx?did=<%# Eval("DiscussionId") %>&id=<%# Eval("DiscussionThreadId") %>&ReturnUrl=<%# SnCore.Tools.Web.Renderer.UrlEncode(Request.Url.PathAndQuery) %>'>
         <%# base.Render(Eval("Subject"))%>
        </a>
       </div>
       <!--
       <div style="font-size: smaller;">
        by 
        <a href='AccountView.aspx?id=<%# Eval("AccountId") %>'>
         <%# base.Render(Eval("AccountName"))%>
        </a>
        on
        <%# base.Adjust(Eval("Created")).ToString("d") %>
       </div>
       -->
      </td>
     </tr>
    </table>
   </ItemTemplate>
  </asp:TemplateColumn>
 </Columns>
</asp:DataGrid>
