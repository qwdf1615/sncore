<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PlaceFeaturedViewControl.ascx.cs"
 Inherits="PlaceFeaturedViewControl" %>
<%@ Register TagPrefix="SnCore" TagName="Notice" Src="NoticeControl.ascx" %>
<%@ Register TagPrefix="SnCore" TagName="RssLink" Src="RssLinkControl.ascx" %>
<SnCore:RssLink ID="linkRelRss" runat="server" NavigateUrl="FeaturedPlacesRss.aspx" 
 Title="Featured Places" ButtonVisible="false" />
<asp:Panel ID="panelFeatured" runat="server">
 <table cellpadding="2" cellspacing="0" class="sncore_featured_table">
  <tr>
   <td width="*" valign="top" class="sncore_featured_tr_td">
    <a runat="server" id="linkFeature2">
     <img border="0" id="imgFeature" runat="server" />
    </a>
    <div class="sncore_description">
     <a runat="server" id="linkFeature3">
      <asp:Label ID="labelFeatureName" runat="server" />
     </a>
    </div>
    <div class="sncore_description">
     <asp:Label ID="labelFeatureCity" runat="server" />
    </div>
   </td>
  </tr>
 </table>
</asp:Panel>
