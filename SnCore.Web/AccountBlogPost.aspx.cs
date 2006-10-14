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
using Wilco.Web.UI.WebControls;
using SnCore.Tools.Drawing;
using System.IO;
using System.Drawing;

public partial class AccountBlogPostNew : AuthenticatedPage
{
    public int BlogId
    {
        get
        {
            return GetId("bid");
        }
    }

    public string ReturnUrl
    {
        get
        {
            object o = Request.QueryString["ReturnUrl"];
            return (o == null ? string.Format("AccountBlogView.aspx?id={0}", BlogId) : o.ToString());
        }
    }

    public void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SetDefaultButton(manageAdd);
            if (!IsPostBack)
            {
                this.addFile.Attributes["onclick"] = this.files.GetAddFileScriptReference() + "return false;";

                linkBack.NavigateUrl = ReturnUrl;

                TransitAccountBlog blog = BlogService.GetAccountBlogById(SessionManager.Ticket, BlogId);
                labelAccountName.Text = Renderer.Render(blog.AccountName);
                labelBlog.Text = Renderer.Render(blog.Name);
                labelBlogDescription.Text = Renderer.Render(blog.Description);
                linkAccount.HRef = string.Format("AccountView.aspx?id={0}", blog.AccountId);
                imageAccount.Src = string.Format("AccountPictureThumbnail.aspx?id={0}", blog.AccountPictureId);

                if (RequestId != 0)
                {
                    TransitAccountBlogPost post = BlogService.GetAccountBlogPostById(SessionManager.Ticket, RequestId);
                    inputBody.Text = post.Body;
                    inputTitle.Text = post.Title;                    
                }
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public void save_Click(object sender, EventArgs e)
    {
        try
        {
            TransitAccountBlogPost tp = new TransitAccountBlogPost();
            tp.Title = inputTitle.Text;
            tp.Body = inputBody.Text;
            tp.AccountBlogId = BlogId;
            tp.Id = RequestId;
            BlogService.CreateOrUpdateAccountBlogPost(SessionManager.Ticket, tp);
            Redirect(ReturnUrl);
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    protected void files_FilesPosted(object sender, FilesPostedEventArgs e)
    {
        try
        {
            if (e.PostedFiles.Count == 0)
                return;

            foreach (HttpPostedFile file in e.PostedFiles)
            {
                TransitAccountPictureWithBitmap p = new TransitAccountPictureWithBitmap();

                ThumbnailBitmap t = new ThumbnailBitmap(file.InputStream);
                p.Bitmap = t.Bitmap;
                p.Name = Path.GetFileName(file.FileName);
                p.Description = string.Empty;

                int id = AccountService.AddAccountPicture(SessionManager.Ticket, p);

                Size size = t.GetNewSize(new Size(200, 200));

                inputBody.Text = string.Format("<a href=AccountPictureView.aspx?id={2}><img border=0 width={0} height={1} src=AccountPicture.aspx?id={2}></a>\n{3}",
                    size.Width, size.Height, id, inputBody.Text);
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }
}
