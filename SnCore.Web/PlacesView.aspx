<%@ Page Language="C#" MasterPageFile="~/SnCore.master" AutoEventWireup="true" CodeFile="PlacesView.aspx.cs"
 Inherits="PlacesView" Title="Places" %>

<%@ Register TagPrefix="SnCoreWebControls" Namespace="SnCore.WebControls" Assembly="SnCore.WebControls" %>
<%@ Register TagPrefix="SnCore" TagName="AccountContentGroupLink" Src="AccountContentGroupLinkControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="Title" Src="TitleControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <asp:UpdatePanel ID="panelLinks" UpdateMode="Conditional" RenderMode="Inline" runat="server">
  <ContentTemplate>
   <table cellpadding="0" cellspacing="0" width="784">
    <tr>
     <td>
      <SnCore:Title ID="titlePlaces" Text="Places" runat="server" ExpandedSize="100">  
       <Template>
        <div class="sncore_title_paragraph">
         These are places in your city, contributed by other users. Participate! Click on a place, upload 
         a picture, leave a Mad Lib or write a review. Then, <a href="PlaceEdit.aspx">Suggest a New Place</a>.
        </div>
       </Template>
      </SnCore:Title>
      <div class="sncore_h2sub">
       <asp:LinkButton ID="linkAll" OnClick="linkAll_Click" runat="server" Text="&#187; All Places" />
       <asp:LinkButton ID="linkLocal" OnClick="linkLocal_Click" runat="server" Text="&#187; All Local Places" />
       <a href="PlaceEdit.aspx">&#187; Suggest a Place</a>
       <a href="AccountPlaceQueueManage.aspx">&#187; My Queue</a>
       <a href="PlaceFriendsQueueView.aspx">&#187; My Friends Queue</a>
       <asp:LinkButton ID="linkSearch" OnClick="linkSearch_Click" runat="server" Text="&#187; Search" />
       <SnCore:AccountContentGroupLink ID="linkAddGroup" runat="server" ConfigurationName="SnCore.AddContentGroup.Id" />
      </div>
     </td>
     <td align="right" valign="middle">
      <asp:HyperLink runat="server" ID="linkRss" ImageUrl="images/rss.gif" NavigateUrl="PlacesRss.aspx" />
      <link runat="server" id="linkRelRss" rel="alternate" type="application/rss+xml" title="Rss"
       href="PlacesRss.aspx" />
     </td>
    </tr>
   </table>
  </ContentTemplate>
 </asp:UpdatePanel>
 <asp:UpdatePanel runat="server" ID="panelSearch" UpdateMode="Conditional">
  <ContentTemplate>
   <SnCoreWebControls:PersistentPanel Visible="False" ID="panelSearchInternal" runat="server" EnableViewState="true">
    <table class="sncore_table">
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
       sort by:
      </td>
      <td class="sncore_form_value">
       <asp:DropDownList CssClass="sncore_form_dropdown" ID="listboxSelectSortOrder" runat="server">
        <asp:ListItem Text="Name" Value="Name" />
        <asp:ListItem Text="Date Created" Selected="True" Value="Created" />
       </asp:DropDownList>
      </td>
     </tr>
     <tr>
      <td class="sncore_form_label">
       order by:
      </td>
      <td class="sncore_form_value">
       <asp:DropDownList CssClass="sncore_form_dropdown" ID="listboxSelectOrderBy" runat="server">
        <asp:ListItem Selected="True" Text="Descending" Value="false" />
        <asp:ListItem Text="Ascending" Value="true" />
       </asp:DropDownList>
      </td>
     </tr>
     <tr>
      <td class="sncore_form_label">
       country and state:
      </td>
      <td class="sncore_form_value">
       <asp:UpdatePanel runat="server" ID="panelCountryState" UpdateMode="Conditional">
        <ContentTemplate>
         <asp:DropDownList CssClass="sncore_form_dropdown_small"
          ID="inputCountry" DataTextField="Name" AutoPostBack="true" DataValueField="Name"
          runat="server" />
         <asp:DropDownList CssClass="sncore_form_dropdown_small" ID="inputState"
          AutoPostBack="true" DataTextField="Name" DataValueField="Name" runat="server" /></td>
        </ContentTemplate>
       </asp:UpdatePanel>
     </tr>
     <tr>
      <td class="sncore_form_label">
       city:
      </td>
      <td class="sncore_form_value">
       <asp:UpdatePanel runat="server" ID="panelCity" UpdateMode="Conditional">
        <ContentTemplate>
         <asp:DropDownList CssClass="sncore_form_dropdown" ID="inputCity" DataTextField="Name"
          DataValueField="Name" runat="server" />
        </ContentTemplate>
       </asp:UpdatePanel>
      </td>
     </tr>
     <tr>
      <td class="sncore_form_label">
       type:
      </td>
      <td class="sncore_form_value">
       <asp:DropDownList CssClass="sncore_form_dropdown" ID="inputType" DataTextField="Name"
        DataValueField="Name" runat="server" />
      </td>
     </tr>
     <tr>
      <td class="sncore_form_label">
      </td>
      <td class="sncore_form_value">
       <asp:CheckBox CssClass="sncore_form_checkbox" ID="checkboxPicturesOnly" runat="server"
        Text="show places with pictures only" Checked="true" />
      </td>
     </tr>
     <tr>
      <td class="sncore_form_label">
      </td>
      <td class="sncore_form_value">
       <SnCoreWebControls:Button ID="search" runat="server" Text="Search" CssClass="sncore_form_button"
        OnClick="search_Click" EnableViewState="false" />
      </td>
     </tr>
    </table>
   </SnCoreWebControls:PersistentPanel>
  </ContentTemplate>
 </asp:UpdatePanel>
 <asp:UpdatePanel runat="server" ID="panelGrid" UpdateMode="Conditional" RenderMode="Inline">
  <ContentTemplate>
   <SnCoreWebControls:PagedList CellPadding="4" runat="server" ID="gridManage"
    AllowCustomPaging="true" CssClass="sncore_table"
    ShowHeader="false" RepeatColumns="4" RepeatRows="2" RepeatDirection="Horizontal" 
    OnDataBinding="gridManage_DataBinding">
    <PagerStyle cssclass="sncore_table_pager" position="TopAndBottom" nextpagetext="Next"
     prevpagetext="Prev" horizontalalign="Center" />
    <ItemStyle CssClass="sncore_table_tr_td" HorizontalAlign="Center" />
    <ItemTemplate>
     <div class="sncore_link">
      <a href="PlaceView.aspx?id=<%# Eval("Id") %>">
       <img border="0" src="PlacePictureThumbnail.aspx?id=<%# Eval("PictureId") %>" />
      </a>
     </div>
     <div class="sncore_link">
      <a href="PlaceView.aspx?id=<%# Eval("Id") %>">
       <%# base.Render(Eval("Name")) %>
      </a>
     </div>
     <div class="sncore_link">
      <a href="PlaceView.aspx?id=<%# Eval("Id") %>">
       &#187; read and review
      </a>
     </div>
     <div class="sncore_description">
      <%# base.Render(Eval("City")) %>
      <%# base.Render(Eval("State")) %>
     </div>
     <div class="sncore_description">
      <%# base.Render(Eval("Country")) %>
     </div>
    </ItemTemplate>
   </SnCoreWebControls:PagedList>
  </ContentTemplate>
 </asp:UpdatePanel>
 <SnCore:Title ID="titleFavorites" Text="Favorites" runat="server" ExpandedSize="100">
  <Template>
   <div class="sncore_title_paragraph">
    These are world-wide, all-time favorite places. Click on a picture and then add a place to your own favorites.
    More people add a place to favorites, higher it will be ranked and more chances a place gets to appear
    on this exclusive list.
   </div>
  </Template>
 </SnCore:Title>
 <asp:UpdatePanel runat="server" ID="panelGridFavorites" UpdateMode="Conditional" RenderMode="Inline">
  <ContentTemplate>
   <SnCoreWebControls:PagedList CellPadding="4" runat="server" ID="gridManageFavorites"
    AllowCustomPaging="true" CssClass="sncore_table"
    ShowHeader="false" RepeatColumns="4" RepeatRows="1" RepeatDirection="Horizontal" 
    OnDataBinding="gridManageFavorites_DataBinding">
    <PagerStyle cssclass="sncore_table_pager" position="Bottom" nextpagetext="Next"
     prevpagetext="Prev" horizontalalign="Center" />
    <ItemStyle CssClass="sncore_table_tr_td" HorizontalAlign="Center" />
    <ItemTemplate>
     <div class="sncore_link">
      <a href="PlaceView.aspx?id=<%# Eval("Id") %>">
       <img border="0" src="PlacePictureThumbnail.aspx?id=<%# Eval("PictureId") %>" />
      </a>
     </div>
     <div class="sncore_link">
      <a href="PlaceView.aspx?id=<%# Eval("Id") %>">
       <%# base.Render(Eval("Name")) %>
      </a>
     </div>
     <div class="sncore_link">
      <a href="PlaceView.aspx?id=<%# Eval("Id") %>">
       &#187; read and review
      </a>
     </div>
     <div class="sncore_description">
      <%# base.Render(Eval("City")) %>
      <%# base.Render(Eval("State")) %>
     </div>
     <div class="sncore_description">
      <%# base.Render(Eval("Country")) %>
     </div>
    </ItemTemplate>
   </SnCoreWebControls:PagedList>
  </ContentTemplate>
 </asp:UpdatePanel> 
</asp:Content>
