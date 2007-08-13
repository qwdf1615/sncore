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

public partial class PlaceChangeRequestEdit : AuthenticatedPage
{
    private int PlaceId
    {
        get
        {
            return GetId("pid");
        }
    }
               
    private LocationSelectorCountryStateCityNeighborhoodText mLocationSelector = null;

    public LocationSelectorCountryStateCityNeighborhoodText LocationSelector
    {
        get
        {
            if (mLocationSelector == null)
            {
                mLocationSelector = new LocationSelectorCountryStateCityNeighborhoodText(
                    this, false, inputCountry, inputState, inputCity, inputNeighborhood);
            }

            return mLocationSelector;
        }
    }

    public void Page_Load(object sender, EventArgs e)
    {
        if (RequestId <= 0 && GetId("pid") == 0)
        {
            throw new Exception("Missing Place");
        }

        LocationSelector.CountryChanged += new EventHandler(LocationSelector_CountryChanged);
        LocationSelector.StateChanged += new EventHandler(LocationSelector_StateChanged);

        if (!IsPostBack)
        {
            DomainClass cs = SessionManager.GetDomainClass("PlaceChangeRequest");
            inputName.MaxLength = cs["Name"].MaxLengthInChars;
            inputStreet.MaxLength = cs["Street"].MaxLengthInChars;
            inputZip.MaxLength = cs["Zip"].MaxLengthInChars;
            inputCrossStreet.MaxLength = cs["CrossStreet"].MaxLengthInChars;
            inputPhone.MaxLength = cs["Phone"].MaxLengthInChars;
            inputFax.MaxLength = cs["Fax"].MaxLengthInChars;
            inputEmail.MaxLength = cs["Email"].MaxLengthInChars;
            inputWebsite.MaxLength = cs["Website"].MaxLengthInChars;

            ArrayList types = new ArrayList();
            types.Add(new TransitAccountPlaceType());
            types.AddRange(SessionManager.PlaceService.GetPlaceTypes(SessionManager.Ticket, null));
            selectType.DataSource = types;
            selectType.DataBind();

            TransitPlace place = null;

            if (RequestId > 0)
            {
                TransitPlaceChangeRequest request = SessionManager.PlaceService.GetPlaceChangeRequestById(
                    SessionManager.Ticket, RequestId);
                labelName.Text = Renderer.Render(request.Name);
                inputName.Text = request.Name;
                inputDescription.Text = request.Description;
                inputCrossStreet.Text = request.CrossStreet;
                inputEmail.Text = request.Email;
                inputFax.Text = request.Fax;
                inputPhone.Text = request.Phone;
                inputStreet.Text = request.Street;
                inputWebsite.Text = request.Website;
                inputZip.Text = request.Zip;
                selectType.Items.FindByValue(request.Type).Selected = true;
                LocationSelector.SelectLocation(sender, new LocationEventArgs(request));

                place = SessionManager.PlaceService.GetPlaceById(
                    SessionManager.Ticket, request.PlaceId);
            }
            else
            {
                place = SessionManager.PlaceService.GetPlaceById(
                    SessionManager.Ticket, PlaceId);
                labelName.Text = Renderer.Render(place.Name);
                inputName.Text = place.Name;
                inputDescription.Text = place.Description;
                inputCrossStreet.Text = place.CrossStreet;
                inputEmail.Text = place.Email;
                inputFax.Text = place.Fax;
                inputPhone.Text = place.Phone;
                inputStreet.Text = place.Street;
                inputWebsite.Text = place.Website;
                inputZip.Text = place.Zip;
                selectType.Items.FindByValue(place.Type).Selected = true;
                LocationSelector.SelectLocation(sender, new LocationEventArgs(place));
            }

            SiteMapDataAttribute sitemapdata = new SiteMapDataAttribute();
            sitemapdata.Add(new SiteMapDataAttributeNode("Places", Request, "PlacesView.aspx"));
            sitemapdata.AddRange(SiteMapDataAttribute.GetLocationAttributeNodes(Request, "PlacesView.aspx", place.Country, place.State, place.City, place.Neighborhood, place.Type));
            sitemapdata.Add(new SiteMapDataAttributeNode(place.Name, Request, string.Format("PlaceView.aspx?id={0}", place.Id)));
            sitemapdata.Add(new SiteMapDataAttributeNode("Request Changes", Request.Url));
            StackSiteMap(sitemapdata);

            linkBack.NavigateUrl = (RequestId > 0 ? 
                string.Format("AccountPlaceChangeRequestsManage.aspx?id={0}", RequestId)
                : string.Format("PlaceView.aspx?id={0}", place.Id));
        }

        if (!SessionManager.AccountService.HasVerifiedEmail(SessionManager.Ticket, SessionManager.AccountId))
        {
            ReportWarning("You don't have any verified e-mail addresses.\n" +
                "You must add/confirm a valid e-mail address before submitting place changes.");

            save.Enabled = false;
        }

        SetDefaultButton(save);
    }

    public void save_Click(object sender, EventArgs e)
    {
        TransitPlaceChangeRequest t = (RequestId > 0) ? SessionManager.PlaceService.GetPlaceChangeRequestById(
            SessionManager.Ticket, RequestId) : new TransitPlaceChangeRequest();

        if (PlaceId > 0) t.PlaceId = PlaceId;
        t.Name = inputName.Text;
        t.Type = selectType.SelectedValue;
        t.Id = RequestId;
        t.Description = inputDescription.Text;
        t.CrossStreet = inputCrossStreet.Text;
        t.Email = inputEmail.Text;
        t.Fax = inputFax.Text;
        t.Phone = inputPhone.Text;
        t.Street = inputStreet.Text;
        t.Website = inputWebsite.Text;
        t.Zip = inputZip.Text;
        t.City = inputCity.Text;
        t.Neighborhood = inputNeighborhood.Text;
        t.State = inputState.SelectedValue;
        t.Country = inputCountry.SelectedValue;

        SessionManager.CreateOrUpdate<TransitPlaceChangeRequest>(
            t, SessionManager.PlaceService.CreateOrUpdatePlaceChangeRequest);

        Redirect(linkBack.NavigateUrl);
    }

    void LocationSelector_StateChanged(object sender, EventArgs e)
    {

    }

    void LocationSelector_CountryChanged(object sender, EventArgs e)
    {
        panelCountryState.Update();
    }
}