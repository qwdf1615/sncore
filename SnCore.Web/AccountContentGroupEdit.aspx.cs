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
using Wilco.Web.UI.WebControls;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using SnCore.Tools.Drawing;
using SnCore.Tools.Web;
using SnCore.WebServices;
using SnCore.Services;
using SnCore.SiteMap;

public partial class AccountContentGroupEdit : AuthenticatedPage
{
    public void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                SiteMapDataAttribute sitemapdata = new SiteMapDataAttribute();
                sitemapdata.Add(new SiteMapDataAttributeNode("Me Me", Request, "AccountPreferencesManage.aspx"));
                sitemapdata.Add(new SiteMapDataAttributeNode("Content", Request, "AccountContentGroupsManage.aspx"));

                if (RequestId > 0)
                {
                    gridManageContent.OnGetDataSource += new EventHandler(gridManageContent_OnGetDataSource);
                    GetData(sender, e);

                    TransitAccountContentGroup tf = ContentService.GetAccountContentGroupById(
                        SessionManager.Ticket, RequestId);

                    sitemapdata.Add(new SiteMapDataAttributeNode(tf.Name, Request.Url));

                    inputName.Text = tf.Name;
                    inputDescription.Text = tf.Description;
                    inputTrusted.Checked = tf.Trusted;
                    inputLogin.Checked = tf.Login;

                    linkNew.NavigateUrl = string.Format("AccountContentEdit.aspx?gid={0}", RequestId);
                    linkView.NavigateUrl = string.Format("AccountContentGroupView.aspx?id={0}", RequestId);
                }
                else
                {
                    sitemapdata.Add(new SiteMapDataAttributeNode("New Group", Request.Url));

                    linkNew.Visible = false;
                    linkView.Visible = false;
                }

                inputTrusted.Enabled = SessionManager.IsAdministrator;
                StackSiteMap(sitemapdata);
            }

            SetDefaultButton(linkSave);
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    void gridManageContent_OnGetDataSource(object sender, EventArgs e)
    {
        ServiceQueryOptions options = new ServiceQueryOptions();
        options.PageSize = gridManageContent.PageSize;
        options.PageNumber = gridManageContent.CurrentPageIndex;
        gridManageContent.DataSource = ContentService.GetAccountContentsById(
            SessionManager.Ticket, RequestId, options);
    }

    public void GetData(object sender, EventArgs e)
    {
        gridManageContent.CurrentPageIndex = 0;
        gridManageContent.VirtualItemCount = ContentService.GetAccountContentsCountById(
            SessionManager.Ticket, RequestId);
        gridManageContent_OnGetDataSource(this, null);
        gridManageContent.DataBind();
    }

    public void save(object sender, EventArgs e)
    {
        try
        {
            TransitAccountContentGroup s = new TransitAccountContentGroup();
            s.Id = RequestId;
            s.Name = inputName.Text;
            s.Description = inputDescription.Text;
            s.Trusted = inputTrusted.Checked && SessionManager.IsAdministrator;
            s.Login = inputLogin.Checked;
            ContentService.CreateOrUpdateAccountContentGroup(SessionManager.Ticket, s);
            Redirect("AccountContentGroupsManage.aspx");
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

    public void gridManageContent_ItemCommand(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "Delete":
                    int id = int.Parse(e.CommandArgument.ToString());
                    ContentService.DeleteAccountContent(SessionManager.Ticket, id);
                    ReportInfo("Content deleted.");
                    GetData(sender, e);
                    break;
            }
        }
        catch (Exception ex)
        {
            ReportException(ex);
        }
    }

}
