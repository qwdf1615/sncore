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

public partial class AccountBlogPostView : Page
{
    public void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                object[] args = { SessionManager.Ticket, RequestId };
                TransitAccountBlogPost tfi = SessionManager.GetCachedItem<TransitAccountBlogPost>(
                    BlogService, "GetAccountBlogPostById", args);

                labelAccountName.Text = Renderer.Render(tfi.AccountName);
                imageAccount.Src = string.Format("AccountPictureThumbnail.aspx?id={0}", tfi.AccountPictureId);

                this.Title = string.Format("{0}'s {1} in {2}", 
                    Renderer.Render(tfi.AccountName), 
                    Renderer.Render(tfi.Title), 
                    Renderer.Render(tfi.AccountBlogName));

                licenseView.AccountId = tfi.AccountId;

                linkAccountBlogPost.Text = Renderer.Render(tfi.Title);
                BlogTitle.Text = linkAccountBlog.Text = Renderer.Render(tfi.AccountBlogName);
                linkAccount.Text = Renderer.Render(tfi.AccountName);
                BlogPostCreated.Text = base.Adjust(tfi.Created).ToString();

                linkEdit.Visible = tfi.CanEdit;
                linkEdit.NavigateUrl = string.Format("AccountBlogPost.aspx?bid={0}&id={1}&ReturnUrl={2}", 
                    tfi.AccountBlogId, tfi.Id, Renderer.UrlEncode(Request.Url.PathAndQuery));
                linkDelete.Visible = tfi.CanDelete;

                linkAccountView.HRef = linkAccount.NavigateUrl = string.Format("AccountView.aspx?id={0}", tfi.AccountId);
                BlogTitle.NavigateUrl = linkAccountBlog.NavigateUrl = string.Format("AccountBlogView.aspx?id={0}", tfi.AccountBlogId);

                BlogPostTitle.Text = Renderer.Render(string.IsNullOrEmpty(tfi.Title) ? "Untitled" : tfi.Title);
                BlogPostBody.Text = RenderEx(tfi.Body);

                object[] d_args = { RequestId };
                BlogPostComments.DiscussionId = SessionManager.GetCachedCollectionCount(
                    DiscussionService, "GetAccountBlogPostDiscussionId", d_args);
                BlogPostComments.DataBind();
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public void linkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            object[] args = { SessionManager.Ticket, RequestId };
            TransitAccountBlogPost tfi = SessionManager.GetCachedItem<TransitAccountBlogPost>(
                BlogService, "GetAccountBlogPostById", args);
            BlogService.DeleteAccountBlogPost(SessionManager.Ticket, tfi.Id);
            Redirect(string.Format("AccountBlogView.aspx?id={0}", tfi.AccountBlogId));
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }
}
