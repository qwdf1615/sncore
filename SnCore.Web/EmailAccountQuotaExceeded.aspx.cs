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
using SnCore.Tools.Web;

public partial class EmailAccountQuotaExceeded : AuthenticatedPage
{
    private TransitAccount mAccount;

    public TransitAccount Account
    {
        get
        {
            if (mAccount == null)
            {
                mAccount = SessionManager.AccountService.GetAccountById(
                    SessionManager.Ticket, RequestId);
            }
            return mAccount;
        }
    }

    public void Page_Load(object sender, EventArgs e)
    {
        Title = string.Format("{0} exceeded a quota", 
            Renderer.Render(Account.Name));
    }
}
