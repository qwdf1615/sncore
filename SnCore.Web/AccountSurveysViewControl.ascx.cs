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
using Wilco.Web.UI;
using SnCore.Services;
using SnCore.WebServices;

public partial class AccountSurveysViewControl : Control
{
    public int AccountId
    {
        get
        {
            return ViewStateUtility.GetViewStateValue<int>(ViewState, "AccountId", 0);
        }
        set
        {
            ViewState["AccountId"] = value;
        }
    }

    public void Page_Load(object sender, EventArgs e)
    {
        accountSurveys.OnGetDataSource += new EventHandler(accountSurveys_OnGetDataSource);
        if (!IsPostBack)
        {
            accountSurveys_OnGetDataSource(sender, e);
            accountSurveys.DataBind();
            this.Visible = accountSurveys.Items.Count > 0;
        }
    }

    void accountSurveys_OnGetDataSource(object sender, EventArgs e)
    {
        accountSurveys.DataSource = SessionManager.GetCollection<TransitSurvey, int>(
            AccountId, (ServiceQueryOptions) null, SessionManager.AccountService.GetAccountSurveysById);
    }

}
