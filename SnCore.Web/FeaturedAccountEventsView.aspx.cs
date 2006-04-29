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

public partial class FeaturedAccountEventsView : Page
{
    public void Page_Load(object sender, EventArgs e)
    {
        try
        {
            gridManage.OnGetDataSource += new EventHandler(gridManage_OnGetDataSource);
            if (!IsPostBack)
            {
                GetData();
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    private void GetData()
    {
        gridManage.CurrentPageIndex = 0;
        gridManage.VirtualItemCount = SystemService.GetFeaturesCount("AccountEvent");
        gridManage_OnGetDataSource(this, null);
        gridManage.DataBind();

        if (gridManage.VirtualItemCount == 0)
        {
            labelCount.Text = "No Featured Events";
        }
        else if (gridManage.VirtualItemCount == 1)
        {
            labelCount.Text = "1 Featured Event";
        }
        else
        {
            labelCount.Text = string.Format("{0} Featured Events!", gridManage.VirtualItemCount);
        }
    }

    void gridManage_OnGetDataSource(object sender, EventArgs e)
    {
        try
        {
            ServiceQueryOptions serviceoptions = new ServiceQueryOptions();
            serviceoptions.PageSize = gridManage.PageSize;
            serviceoptions.PageNumber = gridManage.CurrentPageIndex;
            gridManage.DataSource = SystemService.GetFeatures("AccountEvent", serviceoptions);
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public TransitAccountEvent GetAccountEvent(int id)
    {
        TransitAccountEvent a = (TransitAccountEvent)Cache[string.Format("accountevent:{0}", id)];
        if (a == null)
        {
            a = EventService.GetAccountEventById(SessionManager.Ticket, id);
            Cache.Insert(string.Format("accountevent:{0}", id),
                a, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
        }

        return a;
    }
}