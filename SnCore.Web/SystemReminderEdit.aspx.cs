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

public partial class SystemReminderEdit : AuthenticatedPage
{
    public void Page_Load(object sender, EventArgs e)
    {
        try
        {
            gridManageReminderAccountProperties.OnGetDataSource += new EventHandler(gridManageReminderAccountProperties_OnGetDataSource);

            if (!IsPostBack)
            {
                SiteMapDataAttribute sitemapdata = new SiteMapDataAttribute();
                sitemapdata.Add(new SiteMapDataAttributeNode("Me Me", Request, "AccountPreferencesManage.aspx"));
                sitemapdata.Add(new SiteMapDataAttributeNode("Reminders", Request, "SystemRemindersManage.aspx"));

                inputDataObject.DataSource = SystemService.GetDataObjects();
                inputDataObject.DataBind();

                inputDataObject_SelectedIndexChanged(sender, e);

                if (RequestId > 0)
                {
                    TransitReminder t = SystemService.GetReminderById(RequestId);
                    inputUrl.Text = t.Url;

                    ListItem deltaHoursItem = inputDeltaHours.Items.FindByValue(t.DeltaHours.ToString());
                    if (deltaHoursItem == null)
                    {
                        deltaHoursItem = new ListItem(string.Format("Every {0} Hours", t.DeltaHours), t.DeltaHours.ToString());
                        inputDeltaHours.Items.Add(deltaHoursItem);
                    }

                    inputDataObjectField.Items.FindByValue(t.DataObjectField).Selected = true;

                    inputDeltaHours.ClearSelection();
                    deltaHoursItem.Selected = true;
                    inputEnabled.Checked = t.Enabled;
                    inputRecurrent.Checked = t.Recurrent;

                    GetAccountPropertiesData(sender, e);
                    GetAccountProperties(sender, e);

                    sitemapdata.Add(new SiteMapDataAttributeNode(t.Url, Request.Url));
                }
                else
                {
                    panelAccountProperties.Visible = false;
                    sitemapdata.Add(new SiteMapDataAttributeNode("New Reminder", Request.Url));
                }

                StackSiteMap(sitemapdata);
            }

            SetDefaultButton(manageAdd);
            PageManager.SetDefaultButton(addAccountProperty, panelAccountProperties.Controls);
            PageManager.SetDefaultButton(inputTest, inputTestAccountId);
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    void GetAccountProperties(object sender, EventArgs e)
    {
        inputAccountPropertyGroup.DataSource = AccountService.GetAccountPropertyGroups();
        inputAccountPropertyGroup.DataBind();
        inputAccountPropertyGroup_SelectedIndexChanged(sender, e);
    }

    void GetAccountPropertiesData(object sender, EventArgs e)
    {
        gridManageReminderAccountProperties.CurrentPageIndex = 0;
        gridManageReminderAccountProperties.VirtualItemCount = SystemService.GetReminderAccountPropertiesCountById(RequestId);
        gridManageReminderAccountProperties_OnGetDataSource(sender, e);
        gridManageReminderAccountProperties.DataBind();
    }

    public void inputAccountPropertyGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            inputAccountProperty.DataSource = AccountService.GetAccountProperties(
                int.Parse(inputAccountPropertyGroup.SelectedValue));
            inputAccountProperty.DataBind();
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    private enum Cells
    {
        id = 0
    };

    void gridManageReminderAccountProperties_OnGetDataSource(object sender, EventArgs e)
    {
        try
        {
            ServiceQueryOptions options = new ServiceQueryOptions();
            options.PageNumber = gridManageReminderAccountProperties.CurrentPageIndex;
            options.PageSize = gridManageReminderAccountProperties.PageSize;
            gridManageReminderAccountProperties.DataSource = SystemService.GetReminderAccountPropertiesById(RequestId, options);
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public void inputDataObject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            inputDataObjectField.DataSource = SystemService.GetDataObjectFieldsById(
                int.Parse(inputDataObject.SelectedValue));
            inputDataObjectField.DataBind();
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
            TransitReminder t = new TransitReminder();
            t.Id = RequestId;
            t.Url = inputUrl.Text;
            t.DeltaHours = int.Parse(inputDeltaHours.SelectedValue);
            t.Enabled = inputEnabled.Checked;
            t.Recurrent = inputRecurrent.Checked;
            t.DataObject_Id = int.Parse(inputDataObject.SelectedValue);
            t.DataObjectField = inputDataObjectField.SelectedValue;
            t.LastRun = DateTime.UtcNow;
            SystemService.CreateOrUpdateReminder(SessionManager.Ticket, t);
            Redirect("SystemRemindersManage.aspx");
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public void gridManageReminderAccountProperties_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.AlternatingItem:
                case ListItemType.Item:
                case ListItemType.SelectedItem:
                case ListItemType.EditItem:
                    int id = int.Parse(e.Item.Cells[(int)Cells.id].Text);
                    switch (e.CommandName)
                    {
                        case "Delete":
                            SystemService.DeleteReminderAccountProperty(SessionManager.Ticket, id);
                            ReportInfo("Reminder Account Property deleted.");
                            GetAccountPropertiesData(source, e);
                            break;
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public void addAccountProperty_Click(object sender, EventArgs e)
    {
        try
        {
            TransitReminderAccountProperty trap = new TransitReminderAccountProperty();
            trap.AccountPropertyId = int.Parse(inputAccountProperty.SelectedValue);
            trap.ReminderId = RequestId;
            trap.Value = inputAccountPropertyValue.Text;
            trap.Unset = inputAccountPropertyEmpty.Checked;
            SystemService.CreateOrUpdateReminderAccountProperty(SessionManager.Ticket, trap);
            GetAccountPropertiesData(sender, e);
            ReportInfo("Property Added");
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public void inputTest_Click(object sender, EventArgs e)
    {
        try
        {
            bool result = SystemService.CanSendReminder(RequestId, int.Parse(inputTestAccountId.Text));
            ReportInfo(string.Format("Reminder will {0} be sent.", result ? string.Empty : "NOT"));
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

}
