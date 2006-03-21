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

public partial class AccountBlogView : Page
{
    private TransitFeature mAccountBlogFeature = null;
    private TransitAccountBlog mAccountBlog = null;

    public TransitAccountBlog AccountBlog
    {
        get
        {
            if (mAccountBlog == null)
            {
                mAccountBlog = BlogService.GetAccountBlogById(SessionManager.Ticket, RequestId);
            }
            return mAccountBlog;
        }
    }

    public TransitFeature LatestAccountBlogFeature
    {
        get
        {
            if (mAccountBlogFeature == null)
            {
                mAccountBlogFeature = SystemService.FindLatestFeature(
                    "AccountBlog", RequestId);
            }
            return mAccountBlogFeature;
        }
    }

    public void Page_Load()
    {
        try
        {
            gridManage.OnGetDataSource += new EventHandler(gridManage_OnGetDataSource);
            if (!IsPostBack)
            {
                TransitAccountBlog f = AccountBlog;
                labelBlog.Text = Renderer.Render(f.Name);
                labelBlogDescription.Text = Renderer.Render(f.Description);

                TransitAccount a = AccountService.GetAccountById(f.AccountId);
                labelAccountName.Text = Renderer.Render(a.Name);
                linkAccount.HRef = string.Format("AccountView.aspx?id={0}", a.Id);
                imageAccount.Src = string.Format("AccountPictureThumbnail.aspx?id={0}", a.PictureId);

                this.Title = string.Format("{0}'s {1}", Renderer.Render(a.Name), Renderer.Render(f.Name));

                gridManage.VirtualItemCount = BlogService.GetAccountBlogPostsCountById(RequestId);
                gridManage_OnGetDataSource(this, null);
                gridManage.DataBind();

                if (SessionManager.IsAdministrator)
                {
                    linkFeature.Text = (LatestAccountBlogFeature != null)
                        ? string.Format("Feature &#187; Last on {0}", Adjust(LatestAccountBlogFeature.Created).ToString("d"))
                        : "Feature &#187; Never Featured";
                }

                linkRelRss.Attributes["href"] = linkRss.NavigateUrl = string.Format("AccountBlogRss.aspx?id={0}", RequestId);

                linkEdit.NavigateUrl = string.Format("AccountBlogEdit.aspx?id={0}", RequestId);
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    void gridManage_OnGetDataSource(object sender, EventArgs e)
    {
        try
        {
            ServiceQueryOptions options = new ServiceQueryOptions();
            options.PageNumber = gridManage.CurrentPage;
            options.PageSize = gridManage.PageSize;
            gridManage.DataSource = BlogService.GetAccountBlogPostsById(RequestId, options);
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public string GetComments(int count)
    {
        if (count == 0) return "post a comment";
        else if (count == 1) return "1 comment";
        else return string.Format("{0} comments", count);
    }

    protected override void OnPreRender(EventArgs e)
    {
        panelAdmin.Visible = SessionManager.IsAdministrator;
        if (panelAdmin.Visible)
        {
            linkDeleteFeatures.Visible = (LatestAccountBlogFeature != null);
        }

        panelOwner.Visible = SessionManager.IsAdministrator
            || ((SessionManager.Account != null) && (SessionManager.Account.Id == AccountBlog.AccountId));

        base.OnPreRender(e);
    }

    public void feature_Click(object sender, EventArgs e)
    {
        try
        {
            if (!SessionManager.IsAdministrator)
            {
                // avoid round-trip
                throw new Exception("You must be an administrator to feature blogs.");
            }

            TransitFeature t_feature = new TransitFeature();
            t_feature.DataObjectName = "AccountBlog";
            t_feature.DataRowId = RequestId;
            SystemService.CreateOrUpdateFeature(SessionManager.Ticket, t_feature);
            Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public void deletefeature_Click(object sender, EventArgs e)
    {
        try
        {
            if (!SessionManager.IsAdministrator)
            {
                // avoid round-trip
                throw new Exception("You must be an administrator to feature blogs.");
            }

            TransitFeature t_feature = new TransitFeature();
            t_feature.DataObjectName = "AccountBlog";
            t_feature.DataRowId = RequestId;
            SystemService.DeleteAllFeatures(SessionManager.Ticket, t_feature);
            Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }


}