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

public partial class AccountContentGroupView : Page
{
    public void Page_Load(object sender, EventArgs e)
    {
        try
        {
            gridManage.OnGetDataSource += new EventHandler(gridManage_OnGetDataSource);

            if (!IsPostBack)
            {
                TransitAccountContentGroup group = ContentService.GetAccountContentGroupById(SessionManager.Ticket, RequestId);

                if (group.Login && !SessionManager.IsLoggedIn)
                {
                    RedirectToLogin();
                }

                Title = labelName.Text = Renderer.Render(group.Name);
                labelDescription.Text = Renderer.Render(group.Description);

                linkRelRss.Attributes["href"] = linkRss.NavigateUrl = string.Format("AccountContentGroupViewRss.aspx?id={0}", RequestId);
                
                GetData(sender, e);
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public void GetData(object sender, EventArgs e)
    {
        gridManage.CurrentPageIndex = 0;
        gridManage.VirtualItemCount = ContentService.GetAccountContentsCountById(
            SessionManager.Ticket, RequestId);
        gridManage_OnGetDataSource(sender, e);
        gridManage.DataBind();
    }

    void gridManage_OnGetDataSource(object sender, EventArgs e)
    {
        try
        {
            ServiceQueryOptions options = new ServiceQueryOptions();
            options.PageNumber = gridManage.CurrentPageIndex;
            options.PageSize = gridManage.PageSize;
            gridManage.DataSource = ContentService.GetAccountContentsById(
                SessionManager.Ticket, RequestId, options);
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }
}
