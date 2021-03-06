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
using SnCore.Data.Hibernate;

public partial class AccountMadLibEdit : AuthenticatedPage
{
    public void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SiteMapDataAttribute sitemapdata = new SiteMapDataAttribute();
            sitemapdata.Add(new SiteMapDataAttributeNode("Me Me", Request, "AccountManage.aspx"));
            sitemapdata.Add(new SiteMapDataAttributeNode("Syndication", Request, "AccountFeedsManage.aspx"));

            DomainClass cs = SessionManager.GetDomainClass("MadLib");
            inputName.MaxLength = cs["Name"].MaxLengthInChars;

            if (RequestId > 0)
            {
                TransitMadLib t = SessionManager.MadLibService.GetMadLibById(
                    SessionManager.Ticket, RequestId);
                inputTemplate.Text = t.Template;
                inputName.Text = t.Name;
                sitemapdata.Add(new SiteMapDataAttributeNode(t.Name, Request.Url));
            }
            else
            {
                sitemapdata.Add(new SiteMapDataAttributeNode("New MadLib", Request.Url));
            }
            StackSiteMap(sitemapdata);
        }

        SetDefaultButton(save);
    }

    public void save_Click(object sender, EventArgs e)
    {
        TransitMadLib t = new TransitMadLib();
        t.Id = RequestId;
        t.Template = inputTemplate.Text;
        t.Name = inputName.Text;
        SessionManager.CreateOrUpdate<TransitMadLib>(
            t, SessionManager.MadLibService.CreateOrUpdateMadLib);
        Redirect("AccountMadLibsManage.aspx");
    }
}
