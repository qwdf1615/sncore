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

public partial class AccountMenuControl : Control
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (SessionManager.IsLoggedIn)
            {
                TransitAccountMessageFolder inbox = SessionManager.GetInstance<TransitAccountMessageFolder, int, string>(
                    SessionManager.AccountId, "inbox", SessionManager.AccountService.GetAccountMessageSystemFolder);

                if (inbox != null)
                {
                    linkRequestsStar.Visible = (inbox.UnReadMessageCount > 0);
                    linkInbox.InnerText = string.Format("Inbox ({0})", inbox.UnReadMessageCount);
                }

                linkRequests.InnerText = string.Format("Requests ({0})",
                    SessionManager.GetCount<TransitAccountFriendRequest, int>(
                        SessionManager.AccountId, SessionManager.SocialService.GetAccountFriendRequestsCount));

                linkInvitations.InnerText = string.Format("Invitations ({0})",
                    SessionManager.GetCount<TransitAccountGroupAccountInvitation, int>(
                        SessionManager.AccountId, SessionManager.GroupService.GetAccountGroupAccountInvitationsByAccountIdCount));

                linkAccountPlaceRequests.InnerText = string.Format("Requests ({0})",
                    SessionManager.GetCount<TransitAccountPlaceRequest, int>(
                        SessionManager.AccountId, SessionManager.PlaceService.GetAccountPlaceRequestsByAccountIdCount));
            }
        }
    }
}
