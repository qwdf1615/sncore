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
using SnCore.Services;
using SnCore.WebServices;
using SnCore.SiteMap;

public partial class AccountFriendRequestsSentManage : AuthenticatedPage
{
    public void Page_Load(object sender, EventArgs e)
    {
        try
        {
            listSent.OnGetDataSource += new EventHandler(listSent_OnGetDataSource);
            if (!IsPostBack)
            {
                listSent.VirtualItemCount = SocialService.GetAccountFriendRequestsSentCount(SessionManager.Ticket);                
                GetData(sender, e);

                SiteMapDataAttribute sitemapdata = new SiteMapDataAttribute();
                sitemapdata.Add(new SiteMapDataAttributeNode("Me Me", Request, "AccountPreferencesManage.aspx"));
                sitemapdata.Add(new SiteMapDataAttributeNode("Friend Requests", Request, "AccountFriendRequestsManage.aspx"));
                sitemapdata.Add(new SiteMapDataAttributeNode("Sent", Request.Url));
                StackSiteMap(sitemapdata);
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    void listSent_OnGetDataSource(object sender, EventArgs e)
    {
        ServiceQueryOptions options = new ServiceQueryOptions();
        options.PageNumber = listSent.CurrentPageIndex;
        options.PageSize = listSent.PageSize;
        listSent.DataSource = SocialService.GetAccountFriendRequestsSent(SessionManager.Ticket, options);
    }

    public void GetData(object sender, EventArgs e)
    {
        listSent.CurrentPageIndex = 0;
        listSent_OnGetDataSource(sender, e);
        listSent.DataBind();

        if (listSent.VirtualItemCount == 0)
        {
            noticeManage.Info = "You don't have any pending or outstanding requests.";
        }
    }

    public void listSent_ItemCommand(object sender, DataListCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "Cancel":
                    int id = int.Parse(e.CommandArgument.ToString());
                    SocialService.DeleteAccountFriendRequest(SessionManager.Ticket, id);
                    GetData(sender, e);
                    noticeManage.Info = "Request cancelled.";
                    break;
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }
}
