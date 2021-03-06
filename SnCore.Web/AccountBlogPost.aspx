<%@ Page Language="C#" MasterPageFile="~/SnCore.master" AutoEventWireup="true" CodeFile="AccountBlogPost.aspx.cs"
 Inherits="AccountBlogPostNew" Title="Blog | Post" %>

<%@ Register TagPrefix="SnCore" TagName="AccountMenu" Src="AccountMenuControl.ascx" %>
<%@ Register TagPrefix="SnCoreWebControls" Namespace="SnCore.WebControls" Assembly="SnCore.WebControls" %>
<%@ Register TagPrefix="WilcoWebControls" Namespace="Wilco.Web.UI.WebControls" Assembly="Wilco.Web" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
 <div class="sncore_h2">
  Blog Post
 </div>
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
     <asp:Label ID="labelBlog" runat="server" Text="Blog" />
    </div>
    <div class="sncore_h2sub">
     <asp:Label ID="labelBlogDescription" runat="server" />
    </div>
   </td>
  </tr>
 </table>
 <div class="sncore_cancel">
  <asp:HyperLink ID="linkBack" Text="&#187; Cancel" runat="server" />
  <asp:HyperLink ID="linkPreview" Text="&#187; View" Target="_blank" runat="server" />
 </div>
 <asp:ValidationSummary runat="server" ID="manageValidationSummary" CssClass="sncore_form_validator"
  ShowSummary="true" />
 <table class="sncore_table">
  <tr>
   <td class="sncore_form_label">
    title:
   </td>
   <td class="sncore_form_value">
    <asp:TextBox CssClass="sncore_form_textbox" ID="inputTitle" runat="server" />
   </td>
  </tr>
  <tr>
   <td style="vertical-align: top;" class="sncore_form_label">
    add photos:
   </td>
   <td>
    <WilcoWebControls:MultiFileUpload id="files" runat="server" inputcssclass="sncore_form_upload"
     onfilesposted="files_FilesPosted" />
    <asp:HyperLink ID="addFile" runat="server" CssClass="sncore_form_label" NavigateUrl="#">+</asp:HyperLink>
    <br />
    <SnCoreWebControls:Button id="picturesAdd" cssclass="sncore_form_button" runat="server"
     text="Upload" OnClientClick="WebForm_OnSubmit();" />   
   </td>
  </tr>
  <tr>
   <td class="sncore_form_label">
    post:
   </td>
   <td class="sncore_form_value">   
    <ajaxToolkitHTMLEditor:Editor ID="inputBody" runat="server" Height="400px" InitialCleanUp="true" />
   </td>
  </tr>
  <tr>
   <td class="sncore_form_label">
   </td>
   <td class="sncore_form_value">
    <asp:CheckBox CssClass="sncore_form_checkbox" ID="enableComments" runat="server" 
     Text="Enable Comments" Checked="true" />
   </td>
  </tr>
  <tr>
   <td>
   </td>
   <td class="sncore_form_value">
    <asp:CheckBox ID="inputPublish" runat="server" Checked="true" Text="Publish Blog Post" />
    <div class="sncore_description">
     uncheck to save as draft
    </div>
   </td>
  </tr>
  <tr>
   <td>
   </td>
   <td class="sncore_form_value">
    <asp:CheckBox ID="inputSticky" runat="server" Checked="false" Text="Sticky" />
    <div class="sncore_description">
     post will stick to the top of the blog when checked
    </div>
   </td>
  </tr>
  <tr>
   <td>
   </td>
   <td class="sncore_form_value">
    <div class="sncore_description">
     <asp:UpdatePanel id="panelLastSaved" UpdateMode="Always" runat="server">
      <ContentTemplate>
       <asp:Label id="labelLastSaved" runat="server" />
      </ContentTemplate>
     </asp:UpdatePanel>
    </div>
   </td>
  </tr>
  <tr>
   <td>
   </td>
   <td>
    <asp:UpdatePanel id="panelEdit" runat="server">
     <ContentTemplate>
      <sncorewebcontrols:button id="linkSave" cssclass="sncore_form_button" onclick="save"
       runat="server" text="Save" OnClientClick="WebForm_OnSubmit();" />
      <sncorewebcontrols:button id="linkSaveAndContinue" cssclass="sncore_form_button" onclick="saveAndContinue"
       runat="server" text="Save and Continue Editing" Width="200" OnClientClick="WebForm_OnSubmit();" />
     </ContentTemplate>
    </asp:UpdatePanel>
   </td>
  </tr>
 </table>
</asp:Content>
