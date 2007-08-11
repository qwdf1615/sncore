using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Text;
using AjaxControlToolkit;

[assembly: System.Web.UI.WebResource("SnCore.WebControls.DirtyPanelExtenderBehavior.js", "text/javascript")]

namespace SnCore.WebControls
{
    [ClientScriptResource("SnCore.WebControls.DirtyPanelExtenderBehavior", "SnCore.WebControls.DirtyPanelExtenderBehavior.js")]
    [TargetControlType(typeof(Panel))]
    [TargetControlType(typeof(UpdatePanel))]
    public class DirtyPanelExtender : ExtenderControlBase
    {
        [ExtenderControlProperty]
        [DefaultValue("Data has not been saved.")]
        public string OnLeaveMessage
        {
            get
            {
                return GetPropertyValue("OnLeaveMessage", "");
            }
            set
            {
                SetPropertyValue("OnLeaveMessage", value);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            string values_id = string.Format("{0}_Values", TargetControl.ClientID);
            string values = (Page.IsPostBack ? Page.Request.Form[values_id] : String.Join(",", GetValuesArray()));
            ScriptManager.RegisterHiddenField(this, values_id, values);
            base.OnPreRender(e);
        }

        private string[] GetValuesArray()
        {
            return GetValuesArray(TargetControl.Controls);
        }

        private string[] GetValuesArray(ControlCollection coll)
        {
            List<string> values = new List<string>();
            foreach (Control control in coll)
            {
                if (control is DropDownList || control is RadioButtonList)
                {
                    values.Add(string.Format("{0}:{1}", control.ClientID, ((ListControl)control).SelectedIndex));
                }
                else if (control is IEditableTextControl)
                {
                    values.Add(string.Format("{0}:{1}", control.ClientID, Uri.EscapeDataString(((IEditableTextControl)control).Text)));
                }
                else if (control is ICheckBoxControl)
                {
                    values.Add(string.Format("{0}:{1}", control.ClientID, ((ICheckBoxControl)control).Checked.ToString().ToLower()));
                }
                else if (control is FreeTextBoxControls.FreeTextBox)
                {
                    values.Add(string.Format("{0}:{1}", control.ClientID, ((FreeTextBoxControls.FreeTextBox)control).Text));
                }

                values.AddRange(GetValuesArray(control.Controls));
            }
            return values.ToArray();
        }

        public void ResetDirtyFlag()
        {
            ScriptManager.RegisterClientScriptBlock(TargetControl, TargetControl.GetType(),
                string.Format("{0}_Values_Update", TargetControl.ClientID), string.Format("document.getElementById('{0}').value = '{1}';",
                    string.Format("{0}_Values", TargetControl.ClientID), String.Join(",", GetValuesArray())), true);
        }
    }
}
