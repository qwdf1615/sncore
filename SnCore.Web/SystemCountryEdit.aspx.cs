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
using SnCore.SiteMap;

public partial class SystemCountryEdit : AuthenticatedPage
{
    public void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                SiteMapDataAttribute sitemapdata = new SiteMapDataAttribute();
                sitemapdata.Add(new SiteMapDataAttributeNode("Me Me", Request, "AccountPreferencesManage.aspx"));
                sitemapdata.Add(new SiteMapDataAttributeNode("Countries", Request, "SystemCountriesManage.aspx"));

                int id = RequestId;

                if (id > 0)
                {
                    TransitCountry tw = LocationService.GetCountryById(id);
                    inputName.Text = Renderer.Render(tw.Name);
                    sitemapdata.Add(new SiteMapDataAttributeNode(tw.Name, Request.Url));
                }
                else
                {
                    sitemapdata.Add(new SiteMapDataAttributeNode("New Country", Request.Url));
                }

                StackSiteMap(sitemapdata);
            }

            SetDefaultButton(manageAdd);
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
            TransitCountry tw = new TransitCountry();
            tw.Name = inputName.Text;
            tw.Id = RequestId;
            LocationService.AddCountry(SessionManager.Ticket, tw);
            Redirect("SystemCountriesManage.aspx");
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }

    }
}
