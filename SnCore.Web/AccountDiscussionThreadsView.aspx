<%@ Page Language="C#" MasterPageFile="~/SnCore.master" AutoEventWireup="true" CodeFile="AccountDiscussionThreadsView.aspx.cs"
 Inherits="AcountDiscussionThreadsView" Title="Discussion Threads" %>

<%@ Import Namespace="SnCore.Tools.Web" %>
<%@ Register TagPrefix="SnCoreWebControls" Namespace="SnCore.WebControls" Assembly="SnCore.WebControls" %>
<%@ Register TagPrefix="SnCore" TagName="DiscussionView" Src="DiscussionViewControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <link rel="alternate" type="application/rss+xml" title="Rss" href="DiscussionRss.aspx?id=<% Response.Write(RequestId); %>">
 <div class="sncore_navigate">
  <asp:HyperLink NavigateUrl="AccountView.aspx" CssClass="sncore_navigate_item" ID="linkAccount"
   Text="Account" runat="server" />
 </div>
 <table cellpadding="0" cellspacing="0" width="784">
  <tr>
   <td>
    <asp:Label ID="labelHeader" runat="server" Text="Discussion Posts" CssClass="sncore_h2" />
   </td>
   <td align="right">
    <asp:HyperLink ImageUrl="images/rss.gif" runat="server" ToolTip="Rss" ID="linkRss" />
   </td>
  </tr>
 </table>
 <atlas:UpdatePanel runat="server" ID="panelThreads" Mode="Always" RenderMode="Inline">
  <ContentTemplate>
   <table class="sncore_table">
    <tr>
     <td>
      <asp:CheckBox AutoPostBack="true" OnCheckedChanged="showTopLevel_CheckedChanged"
       ID="showTopLevel" Checked="False" runat="server" Text="only show threads started by this user" />
     </td>
    </tr>
   </table>
   <SnCoreWebControls:PagedGrid CellPadding="4" ShowHeader="false" runat="server" ID="discussionThreadView"
    AutoGenerateColumns="false" CssClass="sncore_table" BorderWidth="0" PageSize="5"
    AllowPaging="true" AllowCustomPaging="true">
    <ItemStyle HorizontalAlign="Center" CssClass="sncore_table_tr_td" />
    <HeaderStyle HorizontalAlign="Center" CssClass="sncore_table_tr_th" />
    <PagerStyle CssClass="sncore_table_pager" Position="TopAndBottom" NextPageText="Next"
     PrevPageText="Prev" HorizontalAlign="Center" />
    <Columns>
     <asp:BoundColumn DataField="Id" Visible="false" />
     <asp:TemplateColumn>
      <itemtemplate>
       <table class="sncore_message_table" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
         <td align="left" valign="top" width="*">
          <div class="sncore_message_subject">
           <%# base.Render(Eval("Subject"))%>
          </div>
          <div class="sncore_description">
           posted on <%# base.Adjust(Eval("Created")).ToString() %>
           in <a href='DiscussionView.aspx?id=<%# Eval("DiscussionId") %>'><%# Renderer.Render(Eval("DiscussionName")) %></a>
          </div>
          <div class="sncore_description">
           <a href="DiscussionPost.aspx?did=<%# Eval("DiscussionId") %>&pid=<%# Eval("Id") %>&ReturnUrl=<%# Renderer.UrlEncode(Request.Url.PathAndQuery) %>&#edit">
            &#187; reply</a>
           <a href="DiscussionPost.aspx?did=<%# Eval("DiscussionId") %>&pid=<%# Eval("Id") %>&ReturnUrl=<%# Renderer.UrlEncode(Request.Url.PathAndQuery) %>&Quote=true&#edit">
            &#187; quote</a>
          </div>
          <div class="sncore_message_body">
           <%# base.RenderEx(Eval("Body"))%>
          </div>
         </td>
        </tr>
      </table>
     </itemtemplate>
     </asp:TemplateColumn>
    </Columns>
   </SnCoreWebControls:PagedGrid>
  </ContentTemplate>
 </atlas:UpdatePanel>
</asp:Content>
