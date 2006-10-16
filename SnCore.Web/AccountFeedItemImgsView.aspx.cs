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
using Microsoft.Web.UI;
using Wilco.Web.UI;

public partial class AccountFeedItemImgsView : AccountPersonPage
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
        try
        {
            gridManage.OnGetDataSource += new EventHandler(gridManage_OnGetDataSource);
            if (!IsPostBack)
            {
                linkEdit.Visible = SessionManager.IsAdministrator;
                GetData();
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    private TransitAccountFeedItemImgQueryOptions mQueryOptions = null;

    TransitAccountFeedItemImgQueryOptions QueryOptions
    {
        get
        {
            if (mQueryOptions == null)
            {
                mQueryOptions = new TransitAccountFeedItemImgQueryOptions();
                mQueryOptions.InterestingOnly = false;
                mQueryOptions.VisibleOnly = (IsEditing ? false : true);
            }

            return mQueryOptions;
        }
    }

    public void linkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            IsEditing = !IsEditing;
            linkEdit.Text = IsEditing ? "&#187; Preview" : "&#187; Edit";
            GetData();
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    private void GetData()
    {
        gridManage.CurrentPageIndex = 0;
        gridManage.VirtualItemCount = SyndicationService.GetAccountFeedItemImgsCount(QueryOptions);
        gridManage_OnGetDataSource(this, null);
        gridManage.DataBind();

        int feedsCount = SyndicationService.GetUpdatedAccountFeedsCount();

        labelCount.Text = string.Format("{0} picture{1} from <a href='AccountFeedsView.aspx'>{2} blog{3}</a>",
            gridManage.VirtualItemCount, gridManage.VirtualItemCount == 1 ? string.Empty : "s",
            feedsCount, feedsCount == 1 ? string.Empty : "s");
    }

    void gridManage_OnGetDataSource(object sender, EventArgs e)
    {
        try
        {
            ServiceQueryOptions serviceoptions = new ServiceQueryOptions();
            serviceoptions.PageSize = gridManage.PageSize;
            serviceoptions.PageNumber = gridManage.CurrentPageIndex;
            gridManage.DataSource = SyndicationService.GetAccountFeedItemImgs(QueryOptions, serviceoptions);
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public void gridManage_ItemCommand(object sender, DataListCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "Toggle":
                    TransitAccountFeedItemImg img = SyndicationService.GetAccountFeedItemImgById(
                        SessionManager.Ticket, int.Parse(e.CommandArgument.ToString()));
                    img.Visible = !img.Visible;
                    if (!img.Visible) img.Interesting = false;
                    SyndicationService.CreateOrUpdateAccountFeedItemImg(SessionManager.Ticket, img);
                    LinkButton lb = (LinkButton)e.Item.FindControl("linkToggleVisible");
                    lb.Text = img.Visible ? "&#187; Hide" : "&#187; Show";
                    UpdatePanel up = (UpdatePanel)e.Item.FindControl("panelShowHide");
                    up.Update();
                    break;
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public void gridManage_DataBinding(object sender, EventArgs e)
    {
        panelGrid.Update();
    }
}
