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
using SnCore.SiteMap;

[SiteMapDataAttribute("Statistics")]
public partial class SystemStatsHits : AuthenticatedPage
{
    public enum ChartType
    {
        Hourly,
        Daily,
        DailyUnique,
        Weekly,
        Monthly,
        Yearly,
        AccountDaily,
        AccountWeekly,
        AccountMonthly,
        AccountYearly
    }

    public void Page_Load()
    {
        if (!SessionManager.IsAdministrator)
        {
            ReportWarning("This page is only available to the system administrator.");
            imageStats.Visible = false;
            return;
        }

        if (!IsPostBack)
        {
            SetChartType(ChartType.Daily);
        }
    }

    void SetChartType(ChartType type)
    {
        imageStats.Src = string.Format("SystemStatsChart.aspx?type={0}&CacheDuration=300", type);
        switch (type)
        {
            case ChartType.DailyUnique:
                labelChartType.Text = "Unique Visitors (Returning/New)";
                break;
            case ChartType.AccountDaily:
            case ChartType.AccountMonthly:
            case ChartType.AccountWeekly:
            case ChartType.AccountYearly:
                labelChartType.Text = type.ToString().Replace("Account", "New Accounts ");
                break;
            default:
                labelChartType.Text = type.ToString();
                break;
        }

        linkDaily.Enabled = (type != ChartType.Daily);
        linkHourly.Enabled = (type != ChartType.Hourly);
        linkMonthly.Enabled = (type != ChartType.Monthly);
        linkYearly.Enabled = (type != ChartType.Yearly);
        linkWeekly.Enabled = (type != ChartType.Weekly);

        linkAccountDaily.Enabled = (type != ChartType.AccountDaily);
        linkAccountMonthly.Enabled = (type != ChartType.AccountMonthly);
        linkAccountYearly.Enabled = (type != ChartType.AccountYearly);
        linkAccountWeekly.Enabled = (type != ChartType.AccountWeekly);

        linkDailyUnique.Enabled = (type != ChartType.DailyUnique);
    }

    public void linkDailyUnique_Click(object sender, EventArgs e)
    {
        SetChartType(ChartType.DailyUnique);
    }

    public void linkYearly_Click(object sender, EventArgs e)
    {
        SetChartType(ChartType.Yearly);
    }

    public void linkHourly_Click(object sender, EventArgs e)
    {
        SetChartType(ChartType.Hourly);
    }

    public void linkMonthly_Click(object sender, EventArgs e)
    {
        SetChartType(ChartType.Monthly);
    }

    public void linkWeekly_Click(object sender, EventArgs e)
    {
        SetChartType(ChartType.Weekly);
    }

    public void linkDaily_Click(object sender, EventArgs e)
    {
        SetChartType(ChartType.Daily);
    }

    public void linkAccountYearly_Click(object sender, EventArgs e)
    {
        SetChartType(ChartType.AccountYearly);
    }

    public void linkAccountMonthly_Click(object sender, EventArgs e)
    {
        SetChartType(ChartType.AccountMonthly);
    }

    public void linkAccountWeekly_Click(object sender, EventArgs e)
    {
        SetChartType(ChartType.AccountWeekly);
    }

    public void linkAccountDaily_Click(object sender, EventArgs e)
    {
        SetChartType(ChartType.AccountDaily);
    }

}
