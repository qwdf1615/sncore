<%@ Page Language="C#" MasterPageFile="~/SnCoreAccount.master" AutoEventWireup="true" CodeFile="BugView.aspx.cs"
 Inherits="BugView" Title="Bug" %>

<%@ Register TagPrefix="SnCoreWebControls" Namespace="SnCore.WebControls" Assembly="SnCore.WebControls" %>
<asp:Content ID="Content" ContentPlaceHolderID="AccountContentPlaceHolder" runat="Server">
 <table class="sncore_account_table">
  <tr>
   <td width="100" class="sncore_table_tr_td">
    <asp:Image ID="imageBugType" runat="server" />
    <asp:Label ID="bugType" runat="server" Text="Type" />
   </td>
   <td width="*" class="sncore_table_tr_td">
    <asp:Label ID="bugProject" runat="server" Text="Project" CssClass="sncore_h2left" />
    <asp:Label ID="bugId" runat="server" Text="#Id" CssClass="sncore_h2left" />
    :
    <asp:Label ID="bugSubject" CssClass="sncore_h2left" runat="server" Text="Subject" />
   </td>
  </tr>
 </table>
 <table class="sncore_account_table">
  <tr>
   <td class="sncore_table_tr_td">
    <asp:HyperLink ID="linkProject2" runat="server" Text="Project" />
    |
    <asp:HyperLink ID="linkAddNew" runat="server" Text="New Bug" />
    |
    <asp:HyperLink ID="linkEdit" runat="server" Text="Edit Bug" />
    |
    <asp:HyperLink ID="linkAddNote" runat="server" Text="Add a Note" />
    |
    <asp:LinkButton OnCommand="linkAction_Command" ID="linkAction" runat="server" Text="Do Something" />
   </td>
  </tr>
 </table>
 <table class="sncore_account_table">
  <tr>
   <td class="sncore_form_label">
    reported by:
   </td>
   <td class="sncore_form_value">
    <asp:HyperLink ID="linkAccount" runat="server" />
   </td>
  </tr>
  <tr>
   <td class="sncore_form_label">
    created:
   </td>
   <td class="sncore_form_value">
    <asp:Label ID="bugCreated" runat="server" />
   </td>
  </tr>
  <tr>
   <td class="sncore_form_label">
    updated:
   </td>
   <td class="sncore_form_value">
    <asp:Label ID="bugUpdated" runat="server" />
   </td>
  </tr>
  <tr>
   <td class="sncore_form_label">
    severity:
   </td>
   <td class="sncore_form_value">
    <asp:Label ID="bugSeverity" runat="server" />
   </td>
  </tr>
  <tr>
   <td class="sncore_form_label">
    priority:
   </td>
   <td class="sncore_form_value">
    <asp:Label ID="bugPriority" runat="server" />
   </td>
  </tr>
  <tr>
   <td class="sncore_form_label">
    resolution:
   </td>
   <td class="sncore_form_value">
    <asp:Label ID="bugResolution" runat="server" />
   </td>
  </tr>
  <tr>
   <td class="sncore_form_label">
    status:
   </td>
   <td class="sncore_form_value">
    <asp:Label ID="bugStatus" runat="server" />
   </td>
  </tr>
 </table>
 <div class="sncore_h3">
  Details
 </div>
 <div class="sncore_h2sub" style="font-size: smaller;">
  <asp:LinkButton CommandArgument="Text" ID="linkSwitchMode" Text="&#187; text" runat="server" OnClick="linkSwitchMode_Click" />
 </div>
 <table class="sncore_account_table">
  <tr>
   <td class="sncore_table_tr_td">
    <asp:Label Font-Bold="true" ID="bugDetails" runat="server" Text="Details" />
   </td>
  </tr>
 </table>
 <div class="sncore_h3">
  Notes</div>
 <SnCoreWebControls:PagedGrid CellPadding="4" runat="server" ID="gridNotes" PageSize="15"
  AllowPaging="true" ShowHeader="false" OnItemCommand="gridNotes_ItemCommand" AutoGenerateColumns="false"
  CssClass="sncore_account_table">
  <ItemStyle HorizontalAlign="Center" CssClass="sncore_table_tr_td" />
  <HeaderStyle HorizontalAlign="Center" CssClass="sncore_table_tr_th" />
  <PagerStyle CssClass="sncore_table_pager" Position="TopAndBottom" NextPageText="Next"
   PrevPageText="Prev" HorizontalAlign="Center" />
  <Columns>
   <asp:BoundColumn DataField="Id" Visible="false" />
   <asp:TemplateColumn ItemStyle-HorizontalAlign="Left">
    <itemtemplate>
     <%# base.RenderEx(Eval("Details")) %>
     <div class="sncore_description">
      added by 
      <a href='AccountView.aspx?id=<%# Eval("AccountId") %>'><%# base.Render(Eval("AccountName")) %></a>
      on
      <%# base.Adjust(Eval("Created")).ToString("d") %>
     </div>
    </itemtemplate>
   </asp:TemplateColumn>
   <asp:TemplateColumn>
    <itemtemplate>
     <a href='BugNoteEdit.aspx?bid=<%# Eval("BugId") %>&id=<%# Eval("Id") %>'>Edit</a>
    </itemtemplate>
   </asp:TemplateColumn>
   <asp:ButtonColumn ButtonType="LinkButton" CommandName="Delete" Text="Delete" />
  </Columns>
 </SnCoreWebControls:PagedGrid>
</asp:Content>
