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
using System.Collections.Specialized;
using System.Text;
using Wilco.Web.UI;
using SnCore.SiteMap;

public partial class AccountFeedItemImgsView : Page
{
    public bool IsEditing
    {
        get
        {
            return SessionManager.IsAdministrator &&
                ViewStateUtility.GetViewStateValue<bool>(
                    ViewState, "IsEditing", false);
        }
        set
        {
            ViewState["IsEditing"] = value;
        }
    }

    public void Page_Load(object sender, EventArgs e)
    {
        gridManage.OnGetDataSource += new EventHandler(gridManage_OnGetDataSource);
        if (!IsPostBack)
        {
            GetData();

            SiteMapDataAttribute sitemapdata = new SiteMapDataAttribute();
            sitemapdata.Add(new SiteMapDataAttributeNode("Blogs", Request, "AccountFeedItemsView.aspx"));
            sitemapdata.Add(new SiteMapDataAttributeNode("Pictures", Request.Url));
            StackSiteMap(sitemapdata);
        }
    }

    private SyndicationService.TransitAccountFeedItemImgQueryOptions mQueryOptions = null;

    SyndicationService.TransitAccountFeedItemImgQueryOptions QueryOptions
    {
        get
        {
            if (mQueryOptions == null)
            {
                mQueryOptions = new SyndicationService.TransitAccountFeedItemImgQueryOptions();
                mQueryOptions.InterestingOnly = false;
                mQueryOptions.VisibleOnly = (IsEditing ? false : true);
            }

            return mQueryOptions;
        }
    }

    public void linkEdit_Click(object sender, EventArgs e)
    {
        IsEditing = !IsEditing;
        GetData();
    }

    private void GetData()
    {
        gridManage.CurrentPageIndex = 0;
        gridManage.VirtualItemCount = SessionManager.GetCount<
            SyndicationService.TransitAccountFeedItemImg,
            SyndicationService.ServiceQueryOptions,
            SyndicationService.TransitAccountFeedItemImgQueryOptions>(
            QueryOptions, SessionManager.SyndicationService.GetAccountFeedItemImgsCount);
        gridManage_OnGetDataSource(this, null);
        gridManage.DataBind();

        SyndicationService.TransitAccountFeedQueryOptions options = new SyndicationService.TransitAccountFeedQueryOptions();
        options.PublishedOnly = false;
        options.PicturesOnly = false;
        int feedsCount = SessionManager.GetCount<
            SyndicationService.TransitAccountFeed,
            SyndicationService.ServiceQueryOptions,
            SyndicationService.TransitAccountFeedQueryOptions>(
            options, SessionManager.SyndicationService.GetAccountFeedsCount);

        labelCount.Text = string.Format("{0} picture{1} from <a href='AccountFeedsView.aspx'>{2} blog{3}</a>",
            gridManage.VirtualItemCount, gridManage.VirtualItemCount == 1 ? string.Empty : "s",
            feedsCount, feedsCount == 1 ? string.Empty : "s");
    }

    void gridManage_OnGetDataSource(object sender, EventArgs e)
    {
        SyndicationService.ServiceQueryOptions serviceoptions = new SyndicationService.ServiceQueryOptions();
        serviceoptions.PageSize = gridManage.PageSize;
        serviceoptions.PageNumber = gridManage.CurrentPageIndex;
        gridManage.DataSource = SessionManager.GetCollection<
            SyndicationService.TransitAccountFeedItemImg,
            SyndicationService.ServiceQueryOptions,
            SyndicationService.TransitAccountFeedItemImgQueryOptions>(
            QueryOptions, serviceoptions, SessionManager.SyndicationService.GetAccountFeedItemImgs);
    }

    public void gridManage_DataBinding(object sender, EventArgs e)
    {
        panelGrid.Update();
    }
}
