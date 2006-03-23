<%@ Page Language="C#" MasterPageFile="~/SnCore.master" AutoEventWireup="true"
 CodeFile="AccountPictureEdit.aspx.cs" Inherits="AccountPictureEdit" Title="Account | Pictures | Edit" %>

<%@ Register TagPrefix="SnCore" TagName="AccountMenu" Src="AccountMenuControl.ascx" %>
<%@ Register TagPrefix="SnCoreWebControls" Namespace="SnCore.WebControls" Assembly="SnCore.WebControls" %>
<%@ Register TagPrefix="SnCore" TagName="DiscussionFullView" Src="DiscussionFullViewControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="AccountReminder" Src="AccountReminderControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <table class="sncore_table">
  <tr>
   <td valign="top" width="150">
    <SnCore:AccountMenu runat="server" ID="menu" />
   </td>
   <td valign="top" width="*">
    <SnCore:AccountReminder ID="accountReminder" runat="server" />
    <div class="sncore_h2">
     Account Picture
    </div>
    <asp:HyperLink ID="linkBack" Text="&#187; Cancel" CssClass="sncore_cancel" NavigateUrl="AccountPicturesManage.aspx"
     runat="server" />
    <asp:ValidationSummary runat="server" ID="manageValidationSummary" CssClass="sncore_form_validator"
     ShowSummary="true" />
    <table class="sncore_account_table">
     <tr>
      <td class="sncore_form_label">
       <a href='AccountPictureView.aspx?id=<% Response.Write(base.RequestId); %>'>
        <img border="0" runat="server" id="inputPictureThumbnail" src="AccountPictureThumbnail.aspx?id=0" />
       </a>
      </td>
      <td class="sncore_form_tr_td">
       <table>
        <tr>
         <td class="sncore_form_label">
          name:
         </td>
         <td class="sncore_form_value">
          <asp:TextBox CssClass="sncore_form_textbox" ID="inputName" runat="server" />
         </td>
        </tr>
        <tr>
         <td class="sncore_form_label">
          description:
         </td>
         <td class="sncore_form_value">
          <asp:TextBox CssClass="sncore_form_textbox" ID="inputDescription" runat="server"
           TextMode="MultiLine" Rows="10" />
         </td>
        </tr>
        <tr>
         <td>
         </td>
         <td class="sncore_form_value">
          <SnCoreWebControls:Button ID="manageAdd" runat="server" Text="Save" CausesValidation="true" CssClass="sncore_form_button"
           OnClick="save_Click" />
         </td>
        </tr>
       </table>
      </td>
     </tr>
    </table>
    <SnCore:DiscussionFullView runat="server" ID="discussionComments" PostNewText="&#187; Post a Comment" />
   </td>
  </tr>
 </table>
</asp:Content>
