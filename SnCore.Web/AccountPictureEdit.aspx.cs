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
using SnCore.SiteMap;

public partial class AccountPictureEdit : AuthenticatedPage
{
    public void Page_Load()
    {
        if (!IsPostBack)
        {
            int id = RequestId;

            if (id > 0)
            {
                TransitAccountPicture tw = SessionManager.AccountService.GetAccountPictureById(SessionManager.Ticket, id);
                this.Title = inputName.Text = Renderer.Render(tw.Name);
                inputDescription.Text = tw.Description;
                inputPictureThumbnail.Src = string.Format("AccountPictureThumbnail.aspx?id={0}&CacheDuration=0", tw.Id);
                inputHidden.Checked = tw.Hidden;

                discussionComments.DiscussionId = SessionManager.GetCount<TransitDiscussion, string, int>(
                    typeof(AccountPicture).Name, id, SessionManager.DiscussionService.GetOrCreateDiscussionId);
                discussionComments.DataBind();

                SiteMapDataAttribute sitemapdata = new SiteMapDataAttribute();
                sitemapdata.Add(new SiteMapDataAttributeNode("Me Me", Request, "AccountManage.aspx"));
                sitemapdata.Add(new SiteMapDataAttributeNode("Pictures", Request, "AccountPicturesManage.aspx"));
                sitemapdata.Add(new SiteMapDataAttributeNode(tw.Name, Request.Url));
                StackSiteMap(sitemapdata);
            }
        }

        SetDefaultButton(manageAdd);
    }

    public void save_Click(object sender, EventArgs e)
    {
        TransitAccountPicture tw = new TransitAccountPicture();
        tw.Name = inputName.Text;
        tw.Description = inputDescription.Text;
        tw.Id = RequestId;
        tw.Hidden = inputHidden.Checked;
        SessionManager.CreateOrUpdate<TransitAccountPicture>(
            tw, SessionManager.AccountService.CreateOrUpdateAccountPicture);
        Redirect("AccountPicturesManage.aspx");
    }
}
