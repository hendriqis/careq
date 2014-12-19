using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace QIS.Careq.Web.Common.UI
{
    public abstract class BasePageTrx : BasePageContent
    {
        #region Session & View State
        public Hashtable ControlEntryList
        {
            get
            {
                if (Session["__ControlEntryList"] == null)
                    Session["__ControlEntryList"] = new Hashtable();

                return (Hashtable)Session["__ControlEntryList"];
            }
            set { Session["__ControlEntryList"] = value; }
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsCallback)
            {
                ControlEntryList.Clear();
                OnControlEntrySetting();
                ReInitControl();
                SetControlEnabled(true);
                InitializeDataControl();
                SetControlProperties();
            }
        }

        #region Button Event
        public void NextPageIndex(int rowCount, ref int pageIndex)
        {
            LoadWords();
            if (rowCount > 0)
            {
                SetControlEnabled(false);
                pageIndex++;
                if (pageIndex == rowCount)
                    pageIndex = 0;
                OnLoadEntity(pageIndex);
                SetControlProperties();
            }
        }

        public void LoadPage(int pageIndex)
        {
            LoadWords();
            SetControlEnabled(false);
            OnLoadEntity(pageIndex);
            SetControlProperties();
        }

        public void PrevPageIndex(int rowCount, ref int pageIndex)
        {
            LoadWords();
            if (rowCount > 0)
            {
                SetControlEnabled(false);
                pageIndex--;
                if (pageIndex < 0)
                    pageIndex = rowCount - 1;
                OnLoadEntity(pageIndex);
                SetControlProperties();
            }
        }

        public void RefreshControl()
        {
            SetControlEnabled(true);
            ReInitControl();
            SetControlProperties();
        }

        public void OnBtnSaveClick(ref string result, ref string retval, bool isAdd)
        {
            result = "save|";
            string errMessage = "";
            if (isAdd)
            {
                if (OnSaveAddRecord(ref errMessage, ref retval))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else
            {
                if (OnSaveEditRecord(ref errMessage, ref retval))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
        }

        public void OnBtnVoidClick(ref string result)
        {
            result = "void|";
            string errMessage = "";
            if (OnVoidRecord(ref errMessage))
                result += "success";
            else
                result += string.Format("fail|{0}", errMessage);
        }
        #endregion

        #region Utility Function
        private void SetControlEnabled(bool isAdd)
        {
            foreach (DictionaryEntry entry in ControlEntryList)
            {
                Control ctrl = (Control)Helper.FindControlRecursive(this, entry.Key.ToString());
                ControlEntrySetting setting = (ControlEntrySetting)entry.Value;
                bool isEnabled = (isAdd ? setting.IsEditAbleInAddMode : setting.IsEditAbleInEditMode);
                SetControlAttribute(ctrl, isEnabled);
            }
        }

        public void ReInitControl()
        {
            LoadWords();
            foreach (DictionaryEntry entry in ControlEntryList)
            {
                Control ctrl = (Control)Helper.FindControlRecursive(this, entry.Key.ToString());
                if (ctrl is WebControl || ctrl is HtmlInputHidden)
                {
                    ControlEntrySetting setting = (ControlEntrySetting)entry.Value;
                    switch (setting.DefaultValue.ToString())
                    {
                        case Constant.DefaultValueEntry.DATE_NOW: SetControlValue(ctrl, DateTime.Now.ToString("dd-MMM-yyyy")); break;
                        case Constant.DefaultValueEntry.TIME_NOW: SetControlValue(ctrl, DateTime.Now.ToString("HH:mm")); break;
                        default: SetControlValue(ctrl, setting.DefaultValue); break;
                    }
                }
            }
        }

        protected void SetControlEntrySetting(Control ctrl, ControlEntrySetting setting)
        {
            ControlEntryList.Add(ctrl.ID, setting);
            if (ctrl is WebControl)
            {
                if (setting.IsRequired)
                    Helper.AddCssClass(((WebControl)ctrl), "required");
                ((WebControl)ctrl).Attributes.Add("validationgroup", "mpEntry");
                if (setting.IsEditAbleInEditMode)
                    ((WebControl)ctrl).Attributes.Add("IsEditAbleInEditMode", "1");
                else
                    ((WebControl)ctrl).Attributes.Add("IsEditAbleInEditMode", "0");
            }
            else if (ctrl is HtmlGenericControl)
            {
                if (setting.IsEditAbleInEditMode)
                    ((HtmlGenericControl)ctrl).Attributes.Add("IsEditAbleInEditMode", "1");
                else
                    ((HtmlGenericControl)ctrl).Attributes.Add("IsEditAbleInEditMode", "0");
            }
        }

        private void SetControlAttribute(Control ctrl, bool isEnabled)
        {
            if (ctrl is TextBox)
            {
                if (isEnabled)
                    ((TextBox)ctrl).ReadOnly = false;
                else
                    ((TextBox)ctrl).ReadOnly = true;
            }
            else if (ctrl is DropDownList)
            {
                ((DropDownList)ctrl).Enabled = isEnabled;
            }
            else if (ctrl is CheckBox)
            {
                ((CheckBox)ctrl).Enabled = isEnabled;
            }
            else if (ctrl is HtmlGenericControl)
            {
                HtmlGenericControl lbl = ctrl as HtmlGenericControl;
                if (!isEnabled)
                    lbl.Attributes.Add("class", "lblDisabled");
            }
        }

        private void SetControlValue(Control ctrl, object value)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = value.ToString();
            else if (ctrl is DropDownList)
                Helper.SetDropDownListValue((DropDownList)ctrl, value.ToString());
            else if (ctrl is CheckBox)
            {
                if (value.ToString() == "")
                    ((CheckBox)ctrl).Checked = false;
                else
                    ((CheckBox)ctrl).Checked = Convert.ToBoolean(value);
            }
            else if (ctrl is HtmlInputHidden)
                ((HtmlInputHidden)ctrl).Value = value.ToString();

        }
        #endregion

        #region Virtual Function
        protected virtual void OnLoadEntity(int PageIndex)
        {
        }

        public virtual int OnGetRowCount()
        {
            return 0;
        }

        public virtual void SetToolbarVisibility(ref bool IsAllowAdd, ref bool IsAllowSave, ref bool IsAllowVoid, ref bool IsAllowNextPrev)
        {

        }

        protected virtual void InitializeDataControl()
        {

        }
        protected virtual void SetControlProperties()
        {
        }

        protected virtual void OnControlEntrySetting()
        {
        }

        protected virtual bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            return false;
        }
        protected virtual bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            return false;
        }
        protected virtual bool OnVoidRecord(ref string errMessage)
        {
            return false;
        }
        #endregion
    }
}
