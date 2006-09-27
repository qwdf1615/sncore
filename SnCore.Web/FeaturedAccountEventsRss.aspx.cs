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
using SnCore.WebServices;
using SnCore.BackEndServices;
using SnCore.Services;
using System.Collections.Generic;

public partial class FeaturedAccountEventsRss : Page
{
    public string Name
    {
        get
        {
            return Renderer.Render(string.Format("{0} Featured Events",
                SessionManager.GetCachedConfiguration("SnCore.Title", "SnCore")));
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ServiceQueryOptions queryoptions = new ServiceQueryOptions();
                queryoptions.PageNumber = 0;
                queryoptions.PageSize = 25;

                object[] args = { "AccountEvent", queryoptions };
                rssRepeater.DataSource = SessionManager.GetCachedCollection<TransitFeature>(
                    SystemService, "GetFeatures", args);

                rssRepeater.DataBind();
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public string WebsiteUrl
    {
        get
        {
            return SessionManager.WebsiteUrl;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        Response.ContentType = "text/xml";
        base.OnPreRender(e);
    }

    public string Link
    {
        get
        {
            return WebsiteUrl.TrimEnd('/') + "/FeaturedAccountEventsView.aspx";
        }
    }

    public TransitAccountEvent GetAccountEvent(int id)
    {
        object[] args = { SessionManager.Ticket, id, SessionManager.UtcOffset };
        return SessionManager.GetCachedItem<TransitAccountEvent>(
            EventService, "GetAccountEventById", args);
    }
}
