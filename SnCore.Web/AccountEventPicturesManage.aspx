<%@ Page Language="C#" MasterPageFile="~/SnCore.master" AutoEventWireup="true"
 CodeFile="AccountEventPicturesManage.aspx.cs" Inherits="AccountEventPicturesManage" Title="Pictures" %>

<%@ Register TagPrefix="SnCore" TagName="AccountMenu" Src="AccountMenuControl.ascx" %>
<%@ Register TagPrefix="SnCoreWebControls" Namespace="SnCore.WebControls" Assembly="SnCore.WebControls" %>
<%@ Register TagPrefix="WilcoWebControls" Namespace="Wilco.Web.UI.WebControls" Assembly="Wilco.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <div class="sncore_navigate">
  <asp:HyperLink CssClass="sncore_navigate_item" ID="linkAccountEvents" NavigateUrl="AccountEventsManage.aspx"
   Text="Events" runat="server" />
  <asp:HyperLink CssClass="sncore_navigate_item" ID="linkAccountEventName" Text="AccountEvent"
   runat="server" />
  <asp:Label CssClass="sncore_navigate_item" ID="linkAccountEventPictures" Text="Pictures" runat="server" />
 </div>
 <table class="sncore_inner_table">
  <tr>
   <td valign="top">
    <SnCore:AccountMenu runat="server" ID="menu" />
   </td>
   <td valign="top">
    <div class="sncore_h2">
     Pictures
    </div>
    <table class="sncore_account_table">
     <tr>
      <td style="vertical-align: top;" class="sncore_form_label">
       add photos:
      </td>
      <td class="sncore_table_tr_td">
       <WilcoWebControls:MultiFileUpload ID="files" runat="server" InputCssClass="sncore_form_upload"
        OnFilesPosted="files_FilesPosted" />
       <asp:HyperLink ID="addFile" runat="server" CssClass="sncore_form_label" NavigateUrl="#">+</asp:HyperLink>
       <br />
       <br />
       <SnCoreWebControls:Button ID="save" CssClass="sncore_form_button" runat="server" Text="Upload" />
      </td>
     </tr>
    </table>
    <SnCoreWebControls:PagedGrid CellPadding="4" runat="server" ID="gridManage" PageSize="15"
     AllowPaging="true" OnItemCommand="gridManage_ItemCommand" AutoGenerateColumns="false"
     CssClass="sncore_account_table">
     <PagerStyle CssClass="sncore_table_pager" Position="TopAndBottom" NextPageText="Next"
      PrevPageText="Prev" HorizontalAlign="Center" />
     <ItemStyle HorizontalAlign="Center" CssClass="sncore_table_tr_td" />
     <HeaderStyle HorizontalAlign="Center" CssClass="sncore_table_tr_th" />
     <Columns>
      <asp:BoundColumn DataField="Id" Visible="false" />
      <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
       <itemtemplate>
     <a href='AccountEventPictureView.aspx?id=<%# Eval("Id") %>'><img 
      border="0" src='AccountEventPictureThumbnail.aspx?id=<%# Eval("Id") %>&CacheDuration=0' /></a>
    </itemtemplate>
      </asp:TemplateColumn>
      <asp:TemplateColumn HeaderText="Name">
       <itemtemplate>
        <b><%# base.Render(Eval("Name")) %></b>
        <br /><%# base.Render(Eval("Description")) %>
       </itemtemplate>
      </asp:TemplateColumn>
      <asp:TemplateColumn>
       <itemtemplate>
        <a href='AccountEventPictureEdit.aspx?pid=<%# base.RequestId %>&id=<%# base.Render(Eval("Id")) %>'>Edit</a>
       </itemtemplate>
      </asp:TemplateColumn>
      <asp:ButtonColumn ButtonType="LinkButton" CommandName="Delete" Text="Delete"></asp:ButtonColumn>
     </Columns>
    </SnCoreWebControls:PagedGrid>
   </td>
  </tr>
 </table>
</asp:Content>