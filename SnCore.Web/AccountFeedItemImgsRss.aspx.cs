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

public partial class AccountFeedItemImgsRss : AccountPersonPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                TransitAccountFeedItemImgQueryOptions QueryOptions = new TransitAccountFeedItemImgQueryOptions();
                QueryOptions.InterestingOnly = false;
                QueryOptions.VisibleOnly = true;

                ServiceQueryOptions serviceoptions = new ServiceQueryOptions();
                serviceoptions.PageNumber = 0;
                serviceoptions.PageSize = 25;

                rssRepeater.DataSource = SyndicationService.GetAccountFeedItemImgs(QueryOptions, serviceoptions);
                rssRepeater.DataBind();
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        Response.ContentType = "text/xml";
        base.OnPreRender(e);
    }
}