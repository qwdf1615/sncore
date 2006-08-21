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

public partial class SearchAccountBlogPostsControl : Control
{
    public void Page_Load()
    {
        try
        {
            gridResults.OnGetDataSource += new EventHandler(gridResults_OnGetDataSource);

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SearchQuery))
                {
                    GetResults();
                }
                else
                {
                    this.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public void GetResults()
    {
        gridResults.CurrentPageIndex = 0;
        object[] args = { SearchQuery };
        gridResults.VirtualItemCount = SessionManager.GetCachedCollectionCount(
            BlogService, "SearchAccountBlogPostsCount", args);

        labelResults.Text = string.Format("{0} result{1}",
            gridResults.VirtualItemCount, gridResults.VirtualItemCount != 1 ? "s" : string.Empty);

        gridResults_OnGetDataSource(this, null);
        gridResults.DataBind();
    }

    public string SearchQuery
    {
        get
        {
            object query = Request.QueryString["q"];
            return query == null ? string.Empty : query.ToString();
        }
    }

    public int ResultsCount
    {
        get
        {
            return gridResults.VirtualItemCount;
        }
    }

    public string GetComments(int count)
    {
        if (count == 0) return "post a comment";
        else if (count == 1) return "1 comment";
        else return string.Format("{0} comments", count);
    }

    void gridResults_OnGetDataSource(object sender, EventArgs e)
    {
        try
        {
            ServiceQueryOptions options = new ServiceQueryOptions();
            options.PageNumber = gridResults.CurrentPageIndex;
            options.PageSize = gridResults.PageSize;
            object[] args = { SearchQuery, options };
            gridResults.DataSource = SessionManager.GetCachedCollection<TransitAccountBlogPost>(
                BlogService, "SearchAccountBlogPosts", args);
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }
}
