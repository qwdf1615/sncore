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

public partial class AccountStoriesRss : Page
{
    public string Name
    {
        get
        {
            return Renderer.Render(string.Format("{0} Stories",
                SessionManager.GetCachedConfiguration("SnCore.Title", "SnCore")));
        }
    }

    public string WebsiteUrl
    {
        get
        {
            return SessionManager.WebsiteUrl;
        }
    }

    public string Link
    {
        get
        {
            return new Uri(SessionManager.WebsiteUri, "Default.aspx").ToString();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ServiceQueryOptions options = new ServiceQueryOptions();
            options.PageNumber = 0;
            options.PageSize = 10;
            rssRepeater.DataSource = SessionManager.StoryService.GetAllAccountStories(
                SessionManager.Ticket, options);
            rssRepeater.DataBind();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        Response.ContentType = "text/xml";
        base.OnPreRender(e);
    }
}
