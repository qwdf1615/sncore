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
using SnCore.Services;
using SnCore.WebServices;

public partial class DiscussionRss : Page
{
    private TransitDiscussion mDiscussion = null;

    public string WebsiteUrl
    {
        get
        {
            return SessionManager.WebsiteUrl;
        }
    }

    private TransitDiscussion Discussion
    {
        get
        {
            if (mDiscussion == null)
            {
                mDiscussion = SessionManager.DiscussionService.GetDiscussionById(
                    SessionManager.Ticket, RequestId);
            }
            return mDiscussion;
        }
    }

    public string Description
    {
        get
        {
            return Renderer.Render(Discussion.Description);
        }
    }

    public string RssTitle
    {
        get
        {
            return Renderer.Render(string.Format("{0} {1}",
                SessionManager.GetCachedConfiguration("SnCore.Title", "SnCore"), Discussion.Name));
        }
    }

    public string Link
    {
        get
        {
            return new Uri(SessionManager.WebsiteUri, string.Format("DiscussionView.aspx?id={0}", 
                RequestId)).ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TransitDiscussion discussion = Discussion;
            if (discussion == null)
            {
                Response.StatusCode = 404;
                Response.End();
                return;
            }

            discussionRss.DiscussionId = RequestId;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        Response.ContentType = "text/xml";
        base.OnPreRender(e);
    }
}
