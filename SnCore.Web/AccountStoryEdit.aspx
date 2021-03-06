<%@ Page Language="C#" MasterPageFile="~/SnCore.master" AutoEventWireup="true" CodeFile="AccountStoryEdit.aspx.cs"
 Inherits="AccountStoryEdit" Title="Account Story" %>

<%@ Register TagPrefix="SnCore" TagName="AccountMenu" Src="AccountMenuControl.ascx" %>
<%@ Register TagPrefix="SnCoreWebControls" Namespace="SnCore.WebControls" Assembly="SnCore.WebControls" %>
<%@ Register TagPrefix="WilcoWebControls" Namespace="Wilco.Web.UI.WebControls" Assembly="Wilco.Web" %>
<%@ Register TagPrefix="SnCore" TagName="AccountReminder" Src="AccountReminderControl.ascx" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
 <div class="sncore_h2">
  <asp:Label ID="labelTitle" runat="server" Text="Tell a Story" />
 </div>
 <div class="sncore_h2sub">
  <asp:HyperLink ID="linkBack" runat="server" Text="&#187; Cancel" NavigateUrl="AccountStoriesView.aspx" />
  <asp:HyperLink ID="linkAddPictures" runat="server" Text="&#187; Add Photos" />
  <asp:HyperLink ID="linkView" runat="server" Text="&#187; Preview" Target="_blank" />
  <asp:LinkButton runat="server" id="LinkButton1" Text="&#187; Summarize" OnClick="linkSummarize_Click"
   OnClientClick="DirtyPanelExtender_SuppressDirtyCheck(); WebForm_OnSubmit();" CausesValidation="false" />
 </div>
 <SnCoreWebControls:DirtyPanelExtender ID="panelPostExtender" runat="server" TargetControlID="panelEdit"
  OnLeaveMessage="Your story has not been saved!" />
 <asp:Panel id="panelEdit" runat="server">
  <table class="sncore_table">
   <tr>
    <td class="sncore_form_label">
     title:
    </td>
    <td class="sncore_form_value">
     <asp:TextBox ID="inputName" runat="server" CssClass="sncore_form_textbox" />
    </td>
   </tr>
   <tr>
    <td valign="top" class="sncore_form_label">
     story:
    </td>
    <td class="sncore_form_value">
     <ajaxToolkitHTMLEditor:Editor id="inputSummary" runat="server" Height="400px" InitialCleanUp="true" />
    </td>
   </tr>
   <tr>
    <td>
    </td>
    <td class="sncore_form_value">
     <asp:CheckBox ID="inputPublish" runat="server" Checked="true" Text="Publish" />
     <div class="sncore_description">
      uncheck to save as draft
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
     <sncorewebcontrols:button id="linkSave" cssclass="sncore_form_button" onclick="save"
      runat="server" text="Save" OnClientClick="DirtyPanelExtender_SuppressDirtyCheck(); WebForm_OnSubmit();" />
     <sncorewebcontrols:button id="linkSaveAndContinue" cssclass="sncore_form_button" onclick="saveAndContinue"
      runat="server" text="Save and Continue Editing" Width="200" OnClientClick="DirtyPanelExtender_SuppressDirtyCheck(); WebForm_OnSubmit();" />
     <sncorewebcontrols:button id="linkClean" cssclass="sncore_form_button" onclick="clean"
      runat="server" text="Clean" OnClientClick="DirtyPanelExtender_SuppressDirtyCheck(); WebForm_OnSubmit();" />
    </td>
   </tr>
   <tr>
    <td>
    </td>
    <td>
     <asp:Label ID="labelSummary" runat="server" />
    </td>
   </tr>
  </table>
 </asp:Panel>
 <asp:DataList ItemStyle-HorizontalAlign="center" ItemStyle-Width="200px" RepeatColumns="4"
  runat="server" ID="gridManage" ShowHeader="false" CssClass="sncore_table"
  OnItemCommand="gridManage_ItemCommand">
  <ItemTemplate>
   <a href='AccountStoryPictureView.aspx?id=<%# Eval("Id") %>'>
    <img border="0" alt='<%# Eval("Name") %>' src='AccountStoryPictureThumbnail.aspx?id=<%# Eval("Id") %>' />
   </a>
   <div>
    <asp:LinkButton CssClass="sncore_link" ID="linkDelete" runat="server" CommandArgument='<%# Eval("Id") %>'
     CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this picture?');"
     Text="Delete" />
    <asp:LinkButton CssClass="sncore_link" ID="linkInsert" runat="server" CommandArgument='<%# Eval("Id") %>'
     CommandName="Insert" Text="Insert" />
   </div>
   <div>
    <asp:LinkButton CssClass="sncore_link" ID="linkUp" runat="server" CommandArgument='<%# Eval("Id") %>'
     CommandName="Up" Text="<" />
    <asp:LinkButton CssClass="sncore_link" ID="linkDown" runat="server" CommandArgument='<%# Eval("Id") %>'
     CommandName="Down" Text=">" />
   </div>
  </ItemTemplate>
 </asp:DataList>
 <sncore:accountreminder id="accountReminder" runat="server" style="width: 582px;" />
</asp:Content>
