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
using SnCore.Tools.Web;
using System.Text;
using SnCore.Services;
using SnCore.WebServices;
using System.Reflection;
using System.Collections.Generic;
using SnCore.SiteMap;

public partial class FeaturedAccountFeedsView : Page
{
    public void Page_Load(object sender, EventArgs e)
    {
        gridManage.OnGetDataSource += new EventHandler(gridManage_OnGetDataSource);
        if (!IsPostBack)
        {
            GetData();

            SiteMapDataAttribute sitemapdata = new SiteMapDataAttribute();
            sitemapdata.Add(new SiteMapDataAttributeNode("Blogs", Request, "AccountFeedItemsView.aspx"));
            sitemapdata.Add(new SiteMapDataAttributeNode("Featured", Request.Url));
            StackSiteMap(sitemapdata);
        }
    }

    private void GetData()
    {
        gridManage.CurrentPageIndex = 0;
        gridManage.VirtualItemCount = SessionManager.ObjectService.GetFeaturesCount(
            SessionManager.Ticket, "AccountFeed");
        gridManage_OnGetDataSource(this, null);
        gridManage.DataBind();

        if (gridManage.VirtualItemCount == 0)
        {
            labelCount.Text = "No Featured Feeds";
        }
        else if (gridManage.VirtualItemCount == 1)
        {
            labelCount.Text = "1 Featured Feed";
        }
        else
        {
            labelCount.Text = string.Format("{0} Featured Feeds!", gridManage.VirtualItemCount);
        }
    }

    void gridManage_OnGetDataSource(object sender, EventArgs e)
    {
        ServiceQueryOptions serviceoptions = new ServiceQueryOptions();
        serviceoptions.PageSize = gridManage.PageSize;
        serviceoptions.PageNumber = gridManage.CurrentPageIndex;

        object[] args = { SessionManager.Ticket, "AccountFeed", serviceoptions };
        gridManage.DataSource = SessionManager.GetCachedCollection<TransitFeature>(
            SessionManager.ObjectService, "GetFeatures", args);
    }

    public TransitAccountFeed GetAccountFeed(int id)
    {
        object[] args = { SessionManager.Ticket, id };
        return SessionManager.GetCachedItem<TransitAccountFeed>(
            SessionManager.SyndicationService, "GetAccountFeedById", args);
    }
}
