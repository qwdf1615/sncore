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
using SnCore.SiteMap;
using SnCore.Services;

public partial class BugResolve : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        linkBack.NavigateUrl = string.Format("BugView.aspx?id={0}", RequestId);

        if (!IsPostBack)
        {
            selectResolution.DataSource = SessionManager.BugService.GetBugResolutions(
                SessionManager.Ticket, null);
            selectResolution.DataBind();

            TransitBug bug = SessionManager.BugService.GetBugById(
                SessionManager.Ticket, RequestId);
            TransitBugProject project = SessionManager.BugService.GetBugProjectById(
                SessionManager.Ticket, bug.ProjectId);

            SiteMapDataAttribute sitemapdata = new SiteMapDataAttribute();
            sitemapdata.Add(new SiteMapDataAttributeNode("Bugs", Request, "BugProjectsManage.aspx"));
            sitemapdata.Add(new SiteMapDataAttributeNode(project.Name, Request, string.Format("BugProjectBugsManage.aspx?id={0}", project.Id)));
            sitemapdata.Add(new SiteMapDataAttributeNode(string.Format("#{0}: {1}", bug.Id, bug.Subject), Request, string.Format("BugView.aspx?id={0}", bug.Id)));
            sitemapdata.Add(new SiteMapDataAttributeNode("Resolve", Request.Url));
            StackSiteMap(sitemapdata);
        }

        SetDefaultButton(manageAdd);
    }

    public void save_Click(object sender, EventArgs e)
    {
        SessionManager.BugService.ResolveBug(SessionManager.Ticket, RequestId, selectResolution.SelectedValue, inputNote.Text);
        Redirect(string.Format("BugView.aspx?id={0}", RequestId));
    }
}
