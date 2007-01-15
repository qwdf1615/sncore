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

public partial class AccountsViewList : Page
{
    public void Page_Load(object sender, EventArgs e)
    {
            if (!IsPostBack)
            {
                int pagenumber = int.Parse(Request["PageNumber"]);
                int pagecount = int.Parse(Request["PageCount"]);
                int pagesize = int.Parse(Request["PageSize"]);
                
                AccountActivityQueryOptions options = new AccountActivityQueryOptions();
                ServiceQueryOptions serviceoptions = new ServiceQueryOptions();
                serviceoptions.PageSize = pagesize;
                serviceoptions.PageNumber = pagenumber;
                gridManage.DataSource = SessionManager.SocialService.GetAccountActivity(
                    SessionManager.Ticket, options, serviceoptions);
                gridManage.DataBind();

                if (pagenumber > 0)
                    linkPrev.HRef = string.Format("AccountsViewList.aspx?PageNumber={0}&PageSize={1}&PageCount={2}",
                        pagenumber - 1, pagesize, pagecount);

                if (pagenumber < pagecount - 1)
                    linkNext.HRef = string.Format("AccountsViewList.aspx?PageNumber={0}&PageSize={1}&PageCount={2}",
                        pagenumber + 1, pagesize, pagecount);
            }
    }
}
