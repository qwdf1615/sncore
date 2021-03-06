using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SnCore.WebServices;
using SnCore.SiteMap;
using SnCore.Services;

public partial class SystemCitiesManage : AuthenticatedPage
{
    public void Page_Load(object sender, EventArgs e)
    {
        gridManage.OnGetDataSource += new EventHandler(gridManage_OnGetDataSource);

        if (!IsPostBack)
        {
            GetData(sender, e);

            SiteMapDataAttribute sitemapdata = new SiteMapDataAttribute();
            sitemapdata.Add(new SiteMapDataAttributeNode("System Preferences", Request, "SystemPreferencesManage.aspx"));
            sitemapdata.Add(new SiteMapDataAttributeNode("Cities", Request.Url));
            StackSiteMap(sitemapdata);
        }
    }

    public void GetData(object sender, EventArgs e)
    {
        gridManage.CurrentPageIndex = 0;
        gridManage.VirtualItemCount = SessionManager.LocationService.GetCitiesCount(
            SessionManager.Ticket);
        gridManage_OnGetDataSource(sender, e);
        gridManage.DataBind();
    }

    void gridManage_OnGetDataSource(object sender, EventArgs e)
    {
        ServiceQueryOptions options = new ServiceQueryOptions();
        options.PageNumber = gridManage.CurrentPageIndex;
        options.PageSize = gridManage.PageSize;
        gridManage.DataSource = SessionManager.LocationService.GetCities(
            SessionManager.Ticket, options);
    }

    private enum Cells
    {
        id = 0
    };

    public void gridManage_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        switch (e.Item.ItemType)
        {
            case ListItemType.AlternatingItem:
            case ListItemType.Item:
            case ListItemType.SelectedItem:
            case ListItemType.EditItem:

                int id = int.Parse(e.Item.Cells[(int)Cells.id].Text);
                switch (e.CommandName)
                {
                    case "Delete":                        
                        
                        TransitCity t_city = SessionManager.LocationService.GetCityById(
                            SessionManager.Ticket, id);
                        
                        // deleting a city may need to orphan places
                        TransitPlaceQueryOptions t_placeoptions = new TransitPlaceQueryOptions();
                        t_placeoptions.City = t_city.Name;
                        t_placeoptions.Country = t_city.Country;
                        t_placeoptions.State = t_city.State;
                        t_placeoptions.PicturesOnly = false;
                        int count = SessionManager.PlaceService.GetPlacesCount(SessionManager.Ticket, t_placeoptions);
                        if (count > 0) 
                        {
                            throw new Exception(string.Format("You must orphan {0} place(s) to delete this city.",
                                count));
                        }

                        SessionManager.Delete<TransitCity>(id, SessionManager.LocationService.DeleteCity);
                        ReportInfo("City deleted.");
                        gridManage.CurrentPageIndex = 0;
                        gridManage_OnGetDataSource(source, e);
                        gridManage.DataBind();
                        break;
                }
                break;
        }
    }
}
