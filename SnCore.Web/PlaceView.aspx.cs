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
using System.Text;

public partial class PlaceView : Page
{
    private TransitPlace mPlace = null;
    private TransitFeature mPlaceFeature = null;
    private TransitAccount mPlaceAccount = null;

    public TransitAccount PlaceAccount
    {
        get
        {
            try
            {
                if (mPlaceAccount == null && RequestId > 0 && Place != null)
                {
                    object[] args = { Place.AccountId };
                    mPlaceAccount = SessionManager.GetCachedItem<TransitAccount>(
                        AccountService, "GetAccountById", args);
                }
            }
            catch (Exception ex)
            {
                ReportException(ex);
            }

            return mPlaceAccount;
        }
    }

    public TransitPlace Place
    {
        get
        {
            try
            {
                if (mPlace == null && RequestId > 0)
                {
                    try
                    {
                        object[] args = { RequestId };
                        mPlace = SessionManager.GetCachedItem<TransitPlace>(
                            PlaceService, "GetPlaceById", args);
                    }
                    catch (NHibernate.ObjectNotFoundException)
                    {
                        mPlace = null;
                    }
                }
            }
            catch (Exception ex)
            {
                ReportException(ex);
            }

            return mPlace;
        }
    }

    public string Address
    {
        get
        {
            TransitPlace place = Place;

            if (place == null)
                return string.Empty;

            if (string.IsNullOrEmpty(place.Street) &&
                string.IsNullOrEmpty(CityOrStateZip) &&
                string.IsNullOrEmpty(place.Country))
                return string.Empty;

            return Renderer.Render(string.Format("{0}, {1}, {2}",
                place.Street, CityOrStateZip, place.Country));
        }
    }

    public string MarkerText
    {
        get
        {
            TransitPlace place = Place;

            if (place == null)
                return string.Empty;

            StringBuilder b = new StringBuilder();
            b.Append("<b>" + place.Name + "</b><br>");
            b.Append("<font style='font-size: smaller;'>");
            b.Append("<div>" + place.Street + "</div>");
            b.Append("<div>" + place.City + " " + place.Zip + "</div>");
            b.Append("<div>" + place.Country + "</div>");
            b.Append("<div><b>" + place.Phone + "</b></div>");
            b.Append("<br><a target='_blank' href='" + DrivingDirectionsUrl + "'>&#187; get directions</a></div>");
            b.Append("</font>");

            return b.ToString();
        }
    }

    public string CityOrStateZip
    {
        get
        {
            TransitPlace place = Place;

            if (place == null)
                return string.Empty;

            if (! string.IsNullOrEmpty(place.Zip))
                return place.Zip;

            if (string.IsNullOrEmpty(place.City)
                && string.IsNullOrEmpty(place.State))
                return string.Empty;

            return place.City + " " + place.State;
        }
    }

    public string DrivingDirectionsUrl
    {
        get
        {
            TransitPlace place = Place;

            if (place == null)
                return string.Empty;

            return string.Format("http://maps.yahoo.com/dd?taddr={0}&tcsz={1}&tcountry={2}",
               Renderer.UrlEncode(place.Street),
               Renderer.UrlEncode(place.City + ", " + place.State + " " + place.Zip),
               Renderer.UrlEncode(place.Country));
        }
    }

    public void Page_Load(object sender, EventArgs e)
    {
        try
        {
            picturesView.OnGetDataSource += new EventHandler(picturesView_OnGetDataSource);
            if (!IsPostBack)
            {
                if (RequestId > 0)
                {
                    TransitPlace place = Place;
                    TransitAccount account = PlaceAccount;

                    if (place == null)
                    {
                        ReportWarning("Place does not exist.");
                        panelPlace.Visible = false;
                        return;
                    }

                    groups.PlaceId = RequestId;

                    Title = Renderer.Render(string.Format("{0}, {1}", place.Name, place.City));

                    MetaDescription.Content = string.Format("Get photos, information and reviews on {0} and other places in {1} on {2}.",
                        Renderer.Render(place.Name),
                        Renderer.Render(place.City),
                        Renderer.Render(SessionManager.GetCachedConfiguration("SnCore.Title", "SnCore")));

                    linkType.NavigateUrl = string.Format("PlacesView.aspx?city={0}&state={1}&country={2}&neighborhood={3}&type={4}",
                        Renderer.UrlEncode(place.City),
                        Renderer.UrlEncode(place.State),
                        Renderer.UrlEncode(place.Country),                        
                        Renderer.UrlEncode(place.Neighborhood),
                        Renderer.UrlEncode(place.Type));

                    linkNeighborhood.NavigateUrl = string.Format("PlacesView.aspx?city={0}&state={1}&country={2}&neighborhood={3}",
                        Renderer.UrlEncode(place.City),
                        Renderer.UrlEncode(place.State),
                        Renderer.UrlEncode(place.Country),
                        Renderer.UrlEncode(place.Neighborhood));

                    linkCity.NavigateUrl = string.Format("PlacesView.aspx?city={0}&state={1}&country={2}",
                        Renderer.UrlEncode(place.City),
                        Renderer.UrlEncode(place.State),
                        Renderer.UrlEncode(place.Country));

                    linkState.NavigateUrl = string.Format("PlacesView.aspx?state={0}&country={1}",
                        Renderer.UrlEncode(place.State),
                        Renderer.UrlEncode(place.Country));

                    linkCountry.NavigateUrl = string.Format("PlacesView.aspx?country={0}",
                        Renderer.UrlEncode(place.Country));

                    linkDirections.HRef = DrivingDirectionsUrl;

                    placeAccounts.PlaceId = RequestId;
                    placeFriends.PlaceId = RequestId;
                    attributesView.PlaceId = RequestId;

                    labelDescription.Text = base.RenderEx(place.Description);
                    panelDescription.Visible = !string.IsNullOrEmpty(labelDescription.Text);
                    linkPlace.Text = Renderer.Render(place.Name);
                    linkNeighborhood.Text = placeNeighborhood.Text = Renderer.Render(place.Neighborhood);
                    linkCity.Text = placeCity.Text = Renderer.Render(place.City);
                    linkState.Text = placeState.Text = Renderer.Render(place.State);
                    linkCountry.Text = placeCountry.Text = Renderer.Render(place.Country);
                    linkType.Text = place.Type + "s";
                    placeName.Text = Renderer.Render(place.Name);
                    placeId.Text = "#" + place.Id.ToString();

                    linkManagePictures.NavigateUrl = string.Format("PlacePicturesManage.aspx?id={0}", place.Id);
                    linkClaimOwnership.NavigateUrl = string.Format("AccountPlaceRequestEdit.aspx?pid={0}", place.Id);

                    if (account != null)
                    {
                        linkSuggestedBy.Text = Renderer.Render(account.Name);
                        linkSuggestedBy.NavigateUrl = string.Format("AccountView.aspx?id={0}", account.Id);
                    }
                    else
                    {
                        linkSuggestedBy.Text = "a deleted user";
                    }

                    labelSuggestedOn.Text = Adjust(place.Created).ToString("d");

                    linkAdminEdit.NavigateUrl = string.Format("PlaceEdit.aspx?id={0}", place.Id);
                    linkAdminAttributes.NavigateUrl = string.Format("PlaceAttributesManage.aspx?id={0}", place.Id);

                    placeWebsite.NavigateUrl = place.Website;
                    placeWebsite.Visible = !string.IsNullOrEmpty(place.Website);
                    placeAddress.Text = Renderer.Render(place.Street);
                    placeZip.Text = Renderer.Render(place.Zip);
                    if (! string.IsNullOrEmpty(place.Fax)) placeFax.Text = string.Format("{0} (fax)", Renderer.Render(place.Fax));
                    if (! string.IsNullOrEmpty(place.Phone)) placePhone.Text = string.Format("{0} (phone)", Renderer.Render(place.Phone));
                    if (! string.IsNullOrEmpty(place.CrossStreet)) placeCrossStreet.Text = string.Format("cross-street: {0}", Renderer.Render(place.CrossStreet));
                    placeType.Text = Renderer.Render(place.Type);

                    imageEmail.OnClientClick = string.Format("location.href='mailto:{0}';",
                       Renderer.Render(place.Email));
                    imageEmail.AlternateText = string.Format("E-Mail {0}", Renderer.Render(place.Name));
                    imageEmail.Visible = ! string.IsNullOrEmpty(place.Email);

                    GetPicturesData(sender, e);

                    object[] args = { RequestId };
                    discussionPlaces.DiscussionId = SessionManager.GetCachedCollectionCount(
                        DiscussionService, "GetPlaceDiscussionId", args);
                    discussionPlaces.DataBind();

                    madlibs.ObjectId = RequestId;
                    madlibs.Table = "Place";
                    madlibs.MadLibId = int.Parse(SessionManager.GetCachedConfiguration("SnCore.MadLibs.Place.Id", "0"));
                    madlibs.ReturnUrl = Request.Url.PathAndQuery;
                    madlibs.ObjectAccountId = place.AccountId;
                    madlibs.QueryString = string.Format("&Place.Name={0}", Renderer.UrlEncode(place.Name));

                    if (SessionManager.IsAdministrator)
                    {
                        linkFeature.Text = (LatestPlaceFeature != null)
                            ? string.Format("Feature &#187; Last on {0}", Adjust(LatestPlaceFeature.Created).ToString("d"))
                            : "Feature &#187; Never Featured";
                    }
                }
                else
                {
                    linkPlace.Text = placeName.Text = Renderer.Render(Request.QueryString["name"]);
                    TransitCity city = LocationService.GetCityByTag(Request.QueryString["city"]);
                    if (city != null)
                    {
                        linkCity.Text = placeCity.Text = Renderer.Render(city.Name);
                        linkState.Text = placeState.Text = Renderer.Render(city.State);
                        linkCountry.Text = placeCountry.Text = Renderer.Render(city.Country);

                        linkCity.NavigateUrl = string.Format("PlacesView.aspx?city={0}&state={1}&country={2}",
                            Renderer.UrlEncode(city.Name),
                            Renderer.UrlEncode(city.State),
                            Renderer.UrlEncode(city.Country));

                        linkState.NavigateUrl = string.Format("PlacesView.aspx?state={0}&country={1}",
                            Renderer.UrlEncode(city.State),
                            Renderer.UrlEncode(city.Country));

                        linkCountry.NavigateUrl = string.Format("PlacesView.aspx?country={0}",
                            Renderer.UrlEncode(city.Country));
                    }
                    else
                    {
                        linkCity.Text = Request.QueryString["city"];
                    }

                    panelViews.Visible = false;
                    panelDetails.Visible = false;
                    panelSubmit.Visible = true;
                    panelDescription.Visible = false;
                    panelPropertyGroups.Visible = false;
                    discussionPlaces.Visible = false;
                    madlibs.Visible = false;
                    linkEdit.NavigateUrl = string.Format("PlaceEdit.aspx?{0}", Request.QueryString.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    void GetPicturesData(object sender, EventArgs e)
    {
        object[] p_args = { RequestId };
        picturesView.CurrentPageIndex = 0;
        picturesView.VirtualItemCount = SessionManager.GetCachedCollectionCount(
            PlaceService, "GetPlacePicturesCountById", p_args);
        picturesView_OnGetDataSource(sender, e);
        picturesView.DataBind();
        placeNoPicture.Visible = (picturesView.Items.Count == 0);
    }

    void picturesView_OnGetDataSource(object sender, EventArgs e)
    {
        try
        {
            ServiceQueryOptions options = new ServiceQueryOptions(picturesView.PageSize, picturesView.CurrentPageIndex);
            object[] args = { RequestId, options };
            picturesView.DataSource = SessionManager.GetCachedCollection<TransitPlacePicture>(
                PlaceService, "GetPlacePicturesById", args);
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public string GoogleMapsKey
    {
        get
        {
            return SystemService.GetConfigurationByName("Google.Maps.Key").Value;
        }
    }

    public void linkAddToQueue_Click(object sender, EventArgs e)
    {
        try
        {
            if (!SessionManager.IsLoggedIn)
            {
                RedirectToLogin();
            }

            TransitPlaceQueue tpq = PlaceService.GetOrCreatePlaceQueueByName(SessionManager.Ticket, "My Queue");
            TransitPlaceQueueItem tpqi = new TransitPlaceQueueItem();
            tpqi.PlaceQueueId = tpq.Id;
            tpqi.PlaceId = RequestId;
            PlaceService.CreateOrUpdatePlaceQueueItem(SessionManager.Ticket, tpqi);
            ReportInfo(string.Format("Added {0} to <a href='AccountPlaceQueueManage.aspx'>your queue</a>.", Renderer.Render(Place.Name)));
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }

    }

    public void linkAddToFavorites_Click(object sender, EventArgs e)
    {
        try
        {
            if (!SessionManager.IsLoggedIn)
            {
                RedirectToLogin();
            }

            if (PlaceService.IsAccountPlaceFavorite(SessionManager.Ticket, RequestId))
            {
                throw new Exception("This place is already your favorite.");
            }

            TransitAccountPlaceFavorite apf = new TransitAccountPlaceFavorite();
            apf.PlaceId = RequestId;
            PlaceService.CreateOrUpdateAccountPlaceFavorite(SessionManager.Ticket, apf);

            ReportInfo(string.Format("Added {0} to your favorites.", Renderer.Render(Place.Name)));
            placeFriends.GetData(sender, e);
            panelFriends.Update();
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            panelAdmin.Visible = SessionManager.IsAdministrator;
            linkDeleteFeatures.Visible = (LatestPlaceFeature != null);
            linkMap.Visible = linkDirections.Visible = (Place != null && !string.IsNullOrEmpty(Place.Street));
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
        base.OnPreRender(e);
    }

    public void feature_Click(object sender, EventArgs e)
    {
        try
        {
            if (!SessionManager.IsAdministrator)
            {
                // avoid round-trip
                throw new Exception("You must be an administrator to feature places.");
            }

            TransitFeature t_feature = new TransitFeature();
            t_feature.DataObjectName = "Place";
            t_feature.DataRowId = RequestId;
            SystemService.CreateOrUpdateFeature(SessionManager.Ticket, t_feature);
            Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public void deletefeature_Click(object sender, EventArgs e)
    {
        try
        {
            if (!SessionManager.IsAdministrator)
            {
                // avoid round-trip
                throw new Exception("You must be an administrator to feature places.");
            }

            TransitFeature t_feature = new TransitFeature();
            t_feature.DataObjectName = "Place";
            t_feature.DataRowId = RequestId;
            SystemService.DeleteAllFeatures(SessionManager.Ticket, t_feature);
            Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public TransitFeature LatestPlaceFeature
    {
        get
        {
            if (mPlaceFeature == null)
            {
                mPlaceFeature = SystemService.FindLatestFeature(
                    "Place", RequestId);
            }
            return mPlaceFeature;
        }
    }
}
