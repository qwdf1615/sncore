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

public partial class AccountView : Page
{
    public AccountView()
    {
        mIsMobileEnabled = true;
    }

    private int mAccountId = -1;
    private TransitAccount mAccount = null;
    private TransitFeature mAccountFeature = null;

    public int AccountId
    {
        get
        {
            if (mAccountId < 0)
            {
                mAccountId = RequestId;
                if (mAccountId == 0)
                {
                    mAccountId = SessionManager.Account.Id;
                }
            }

            return mAccountId;
        }
    }

    public TransitAccount Account
    {
        get
        {
            if (mAccount == null)
            {
                mAccount = SessionManager.GetInstance<TransitAccount, int>(
                    AccountId, SessionManager.AccountService.GetAccountById);
            }
            return mAccount;
        }
    }
    public TransitFeature LatestAccountFeature
    {
        get
        {
            if (mAccountFeature == null)
            {
                mAccountFeature = SessionManager.GetInstance<TransitFeature, string, int>(
                    "Account", RequestId, SessionManager.ObjectService.FindLatestFeature);
            }
            return mAccountFeature;
        }
    }

    public void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (RequestId == 0)
            {
                counterProfileViews.Uri = string.Format("{0}?id={1}", Request.Url, AccountId);
            }
        }
    }

    public void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            linkDelete.NavigateUrl = string.Format("AccountDelete.aspx?id={0}", AccountId);
            linkResetPassword.NavigateUrl = string.Format("AccountChangePassword.aspx?id={0}", AccountId);
            linkAttributes.NavigateUrl = string.Format("AccountAttributesManage.aspx?id={0}", AccountId);
            linkManageQuotas.NavigateUrl = string.Format("SystemAccountQuotasManage.aspx?id={0}", AccountId);

            picturesView.AccountId = AccountId;
            attributesView.AccountId = AccountId;
            placeFavoritesView.AccountId = AccountId;
            placesView.AccountId = AccountId;
            groupsView.AccountId = AccountId;
            propertygroupsView.AccountId = AccountId;
            friendsView.AccountId = AccountId;
            surveysView.AccountId = AccountId;
            websitesView.AccountId = AccountId;
            storiesView.AccountId = AccountId;
            feedsView.AccountId = AccountId;
            blogsView.AccountId = AccountId;
            licenseView.AccountId = AccountId;
            eventsView.AccountId = AccountId;

            if (Account == null)
            {
                ReportWarning("Account does not exist.");
                pnlAccount.Visible = false;
                return;
            }

            SiteMapDataAttribute sitemapdata = new SiteMapDataAttribute();
            sitemapdata.Add(new SiteMapDataAttributeNode("People", Request, "AccountsView.aspx"));
            sitemapdata.Add(new SiteMapDataAttributeNode(Account.Name, Request.Url));
            StackSiteMap(sitemapdata);

            accountReminder.Visible = (RequestId == 0);

            this.Title = Renderer.Render(Account.Name);

            accountLastLogin.Text = Adjust(Account.LastLogin).ToString("d");
            accountCity.Text = Renderer.Render(Account.City);
            accountState.Text = Renderer.Render(Account.State);
            accountCountry.Text = Renderer.Render(Account.Country);
            accountName.Text = Renderer.Render(Account.Name);
            accountId.Text = "#" + Account.Id.ToString();

            string returnurl = Renderer.UrlEncode(string.Format("AccountView.aspx?id={0}", Account.Id));

            linkNewMessage.NavigateUrl = string.Format("AccountMessageEdit.aspx?id={0}&ReturnUrl={1}",
                Account.Id.ToString(), returnurl);

            linkAddToFriends.NavigateUrl = string.Format("AccountFriendRequestEdit.aspx?pid={0}&ReturnUrl={1}",
                Account.Id.ToString(), returnurl);

            if (! Account.IsVerified)
            {
                panelLinks.Visible = false;
                panelDetails.Visible = false;
                ReportWarning(string.Format("{0}'s e-mail address has not been verified yet. Profile hidden.",
                    Account.Name));
                return;
            }

            discussionTags.DiscussionId = SessionManager.GetCount<TransitDiscussion, string, int>(
                typeof(Account).Name, Account.Id, SessionManager.DiscussionService.GetOrCreateDiscussionId);

            linkLeaveTestimonial.NavigateUrl = string.Format("DiscussionPost.aspx?did={0}&ReturnUrl={1}",
                discussionTags.DiscussionId, returnurl);

            redirect.AccountId = AccountId;
            redirect.TargetUri = string.Format("AccountView.aspx?id={0}", AccountId);

            websitesView.DataBind();
            surveysView.DataBind();
            storiesView.DataBind();
            discussionTags.DataBind();

            if (SessionManager.IsAdministrator)
            {
                linkFeature.Text = (LatestAccountFeature != null)
                    ? string.Format("Feature &#187; Last on {0}", Adjust(LatestAccountFeature.Created).ToString("d"))
                    : "Feature &#187; Never Featured";
            }

            panelAdmin.Visible = SessionManager.IsAdministrator && (AccountId != SessionManager.Account.Id);
            if (panelAdmin.Visible)
            {
                linkPromoteAdmin.Visible = (! Account.IsAdministrator) && SessionManager.IsAdministrator;
                linkDemoteAdmin.Visible = Account.IsAdministrator && SessionManager.IsAdministrator;
                linkDeleteFeatures.Visible = (LatestAccountFeature != null);
            }
        }
    }

    public void promoteAdmin_Click(object sender, EventArgs e)
    {
        if (RequestId == 0)
        {
            throw new Exception("You cannot make yourself administrator.");
        }

        if (!SessionManager.IsAdministrator)
        {
            // avoid round-trip
            throw new Exception("You must be an administrator to promote other users.");
        }

        SessionManager.AccountService.PromoteAdministrator(SessionManager.Ticket, AccountId);
        Redirect(Request.Url.PathAndQuery);
    }

    public void feature_Click(object sender, EventArgs e)
    {
        if (!SessionManager.IsAdministrator)
        {
            // avoid round-trip
            throw new Exception("You must be an administrator to feature other users.");
        }

        TransitFeature t_feature = new TransitFeature();
        t_feature.DataObjectName = "Account";
        t_feature.DataRowId = AccountId;
        SessionManager.CreateOrUpdate<TransitFeature>(
            t_feature, SessionManager.ObjectService.CreateOrUpdateFeature);
        Redirect(Request.Url.PathAndQuery);
    }

    public void demoteAdmin_Click(object sender, EventArgs e)
    {
        if (RequestId == 0)
        {
            throw new Exception("You cannot make yourself administrator.");
        }

        if (!SessionManager.IsAdministrator)
        {
            // avoid round-trip
            throw new Exception("You must be an administrator to demote other users.");
        }

        SessionManager.AccountService.DemoteAdministrator(SessionManager.Ticket, AccountId);
        Redirect(Request.Url.PathAndQuery);
    }


    public void impersonate_Click(object sender, EventArgs e)
    {
        if (RequestId == 0)
        {
            throw new Exception("You cannot impersonate self.");
        }

        if (!SessionManager.IsAdministrator)
        {
            // avoid round-trip
            throw new Exception("You must be an administrator to impersonate users.");
        }

        if (SessionManager.IsImpersonating)
        {
            throw new Exception("You're already impersonating a user.");
        }

        SessionManager.Impersonate(SessionManager.AccountService.Impersonate(SessionManager.Ticket, AccountId));
        Response.Redirect("AccountView.aspx");
    }

    public void deletefeature_Click(object sender, EventArgs e)
    {
        if (!SessionManager.IsAdministrator)
        {
            // avoid round-trip
            throw new Exception("You must be an administrator to feature accounts.");
        }

        TransitFeature t_feature = new TransitFeature();
        t_feature.DataObjectName = "Account";
        t_feature.DataRowId = RequestId;
        SessionManager.ObjectService.DeleteAllFeatures(SessionManager.Ticket, t_feature);
        SessionManager.InvalidateCache<TransitFeature>();
        Redirect(Request.Url.PathAndQuery);
    }
}
