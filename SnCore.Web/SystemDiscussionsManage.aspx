<%@ Page Language="C#" MasterPageFile="~/SnCoreAccount.master" AutoEventWireup="true" CodeFile="SystemDiscussionsManage.aspx.cs"
 Inherits="SystemDiscussionsManage" Title="Discussions" %>

<%@ Register TagPrefix="SnCoreWebControls" Namespace="SnCore.WebControls" Assembly="SnCore.WebControls" %>
<%@ Register TagPrefix="SnCore" TagName="Title" Src="TitleControl.ascx" %>
<asp:Content ID="Content" ContentPlaceHolderID="AccountContentPlaceHolder" runat="Server">
 <SnCore:Title ID="titleDiscussions" Text="Discussions" runat="server">
  <Template>
   <div class="sncore_title_paragraph">
    When <a href="DiscussionsView.aspx">existing discussions</a> get too crowded for an important topic, you can
    <a href="SystemDiscussionEdit.aspx">create your own discussion forum</a>. You will then be able to fully control
    the content on this new discussion board and everyone will see it.
   </div>
  </Template>
 </SnCore:Title>
 <div class="sncore_createnew">
  <a href="SystemDiscussionEdit.aspx">&#187; Create New</a>
  <a href="DiscussionsView.aspx">&#187; I Just Want to Post Something</a>
  <a href="AccountDiscussionThreadsView.aspx">&#187; My Posts</a>
 </div>
 <asp:UpdatePanel id="panelManageUpdate" runat="server" UpdateMode="Always">
  <ContentTemplate>
   <SnCoreWebControls:PagedGrid CellPadding="4" OnItemCommand="gridManage_ItemCommand"
    runat="server" ID="gridManage" AutoGenerateColumns="false" CssClass="sncore_account_table"
    AllowPaging="true" AllowCustomPaging="true" PageSize="10">
    <ItemStyle HorizontalAlign="Center" CssClass="sncore_table_tr_td" />
    <HeaderStyle HorizontalAlign="Center" CssClass="sncore_table_tr_th" />
    <PagerStyle CssClass="sncore_table_pager" Position="TopAndBottom" NextPageText="Next"
     PrevPageText="Prev" HorizontalAlign="Center" />
    <Columns>
     <asp:BoundColumn DataField="Id" Visible="false" />
     <asp:BoundColumn DataField="Name" Visible="false" />
     <asp:TemplateColumn>
      <itemtemplate>
       <img src="images/account/discussions.gif" />
      </itemtemplate>
     </asp:TemplateColumn>
     <asp:TemplateColumn HeaderText="Discussion" ItemStyle-HorizontalAlign="Left">
      <itemtemplate>
       <a href="DiscussionView.aspx?id=<%# Eval("Id") %>">
        <%# base.Render(Eval("Name")) %>
       </a>
       <div class="sncore_description">
        <%# base.Render(Eval("Description")) %>
       </div>
      </itemtemplate>
     </asp:TemplateColumn>
     <asp:TemplateColumn>
      <itemtemplate>
       <a href="SystemDiscussionEdit.aspx?id=<%# Eval("Id") %>">Edit</a>
      </itemtemplate>
     </asp:TemplateColumn>
     <asp:ButtonColumn ButtonType="LinkButton" CommandName="Delete" Text="Delete" />
    </Columns>
   </SnCoreWebControls:PagedGrid>
  </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
