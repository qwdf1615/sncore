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
using SnCore.WebServices;
using SnCore.BackEndServices;
using SnCore.Services;

public partial class PlacesRss : Page
{
    public string RssTitle
    {
        get
        {
            return Renderer.Render(string.Format("{0} Places",
                SessionManager.GetCachedConfiguration("SnCore.Title", "SnCore")));
        }
    }

    public string SortOrder
    {
        get
        {
            object o = Request.QueryString["sortorder"];
            return (o == null ? "Modified" : o.ToString());
        }
    }

    public bool Ascending
    {
        get
        {
            object o = Request.QueryString["asc"];
            return (o == null ? false : bool.Parse(o.ToString()));
        }
    }

    public string Neighborhood
    {
        get
        {
            object o = Request.QueryString["neighborhood"];
            return (o == null ? string.Empty : o.ToString());
        }
    }

    public string City
    {
        get
        {
            object o = Request.QueryString["city"];
            return (o == null ? string.Empty : o.ToString());
        }
    }

    public string Country
    {
        get
        {
            object o = Request.QueryString["country"];
            return (o == null ? string.Empty : o.ToString());
        }
    }

    public string State
    {
        get
        {
            object o = Request.QueryString["state"];
            return (o == null ? string.Empty : o.ToString());
        }
    }

    public string Type
    {
        get
        {
            object o = Request.QueryString["type"];
            return (o == null ? string.Empty : o.ToString());
        }
    }

    public string Name
    {
        get
        {
            object o = Request.QueryString["name"];
            return (o == null ? string.Empty : o.ToString());
        }
    }

    public bool Pictures
    {
        get
        {
            object o = Request.QueryString["pictures"];
            return (o == null ? true : bool.Parse(o.ToString()));
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TransitPlaceQueryOptions options = new TransitPlaceQueryOptions();
            options.SortAscending = Ascending;
            options.SortOrder = SortOrder;
            options.Neighborhood = Neighborhood;
            options.City = City;
            options.Country = Country;
            options.State = State;
            options.Name = Name;
            options.Type = Type;
            options.PicturesOnly = Pictures;

            ServiceQueryOptions queryoptions = new ServiceQueryOptions();
            queryoptions.PageNumber = 0;
            queryoptions.PageSize = 10;

            rssRepeater.DataSource = SessionManager.PlaceService.GetPlaces(
                SessionManager.Ticket, options, queryoptions);
            rssRepeater.DataBind();
        }
    }

    public string WebsiteUrl
    {
        get
        {
            return SessionManager.WebsiteUrl;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        Response.ContentType = "text/xml";
        base.OnPreRender(e);
    }

    public string Link
    {
        get
        {
            return new Uri(SessionManager.WebsiteUri, "Default.aspx").ToString();
        }
    }
}
