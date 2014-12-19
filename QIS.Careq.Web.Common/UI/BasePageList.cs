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
    public abstract class BasePageList : BasePageContent
    {
        public int numberOfPages;

        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack && !Page.IsCallback)
            {
                string filterExpression = "";
                string keyValue = "";
                if (Request.Form["filterExpression"] != null)
                    filterExpression = Request.Form["filterExpression"].ToString();
                if (Request.Form["id"] != null)
                    keyValue = Request.Form["id"].ToString();

                InitializeDataControl(filterExpression, keyValue);                
                SetControlProperties();
            }
        }

        #region Button Event
        public void OnBtnAddClick(ref string result, ref string url)
        {
            result = "add|";
            string errMessage = "";
            if (OnBeforeAddRecord(ref errMessage))
            {
                if (OnAddRecord(ref url, ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else
                result += string.Format("fail|{0}", errMessage);
        }

        public void OnBtnEditClick(ref string result, ref string url)
        {
            result = "edit|";
            string errMessage = "";
            if (OnBeforeEditRecord(ref errMessage))
            {
                if (OnEditRecord(ref url, ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else
                result += string.Format("fail|{0}", errMessage);
        }

        public void OnBtnDeleteClick(ref string result)
        {
            result = "delete|";
            string errMessage = "";
            if (OnBeforeDeleteRecord(ref errMessage))
            {
                if (OnDeleteRecord(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }
            else
                result += string.Format("fail|{0}", errMessage);
        }

        public void OnBtnCustomClick(ref string result, string type)
        {
            result = "customclick|";
            string errMessage = "";
            if (OnCustomButtonClick(type, ref errMessage))
                result += "success";
            else
                result += string.Format("fail|{0}", errMessage);
        }
        #endregion

        #region Virtual Function
        protected virtual void InitializeDataControl()
        {
        }
        protected virtual void InitializeDataControl(string filterExpression, string keyValue)
        {
        }

        protected virtual void SetControlProperties()
        {
        }

        protected virtual void SetDataBinding(int pageIndex, bool isCountPageCount, ref int pageCount)
        { 
        }

        protected virtual bool OnBeforeAddRecord(ref string errMessage)
        {
            return true;
        }

        protected virtual bool OnBeforeEditRecord(ref string errMessage)
        {
            return true;
        }

        protected virtual bool OnBeforeDeleteRecord(ref string errMessage)
        {
            return true;
        }

        protected virtual bool OnAddRecord(ref string url, ref string errMessage)
        {
            return false;
        }

        protected virtual bool OnEditRecord(ref string url, ref string errMessage)
        {
            return false;
        }

        protected virtual bool OnDeleteRecord(ref string errMessage)
        {
            return false;
        }

        protected virtual bool OnCustomButtonClick(string type, ref string errMessage)
        {
            return true;
        }

        public virtual void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = null;
            fieldListValue = null;
        }
        #endregion

        public string OnCallBack(DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = string.Empty;
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    SetDataBinding(Convert.ToInt32(param[1]), false, ref numberOfPages);
                    result = "changepage";
                }
                else // refresh
                {

                    SetDataBinding(1, true, ref numberOfPages);
                    result = "refresh|" + numberOfPages;
                }
            }
            return result;
        }
    }
}
