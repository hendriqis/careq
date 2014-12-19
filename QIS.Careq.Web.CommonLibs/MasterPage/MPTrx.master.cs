using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;

namespace QIS.Careq.Web.CommonLibs.MasterPage
{
    public partial class MPTrx : BaseMP
    {
        private BasePageTrx _basePageEntry;
        private BasePageTrx BasePageEntry
        {
            get
            {
                if (_basePageEntry == null)
                    _basePageEntry = (BasePageTrx)Page;
                return _basePageEntry;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdnIsAdd.Value = "1";
                hdnRowCount.Value = BasePageEntry.OnGetRowCount().ToString();

                bool IsAllowAdd, IsAllowSave, IsAllowVoid, IsAllowNextPrev;
                IsAllowAdd = IsAllowSave = IsAllowVoid = IsAllowNextPrev = true;
                BasePageEntry.SetToolbarVisibility(ref IsAllowAdd, ref IsAllowSave, ref IsAllowVoid, ref IsAllowNextPrev);
                if (!IsAllowAdd)
                    btnMPEntryNew.Style.Add("display", "none");
                if (!IsAllowSave)
                    btnMPEntrySave.Style.Add("display", "none");
                if (!IsAllowVoid)
                    btnMPEntryVoid.Style.Add("display", "none");
                if (!IsAllowNextPrev)
                {
                    btnMPEntryNext.Style.Add("display", "none");
                    btnMPEntryPrev.Style.Add("display", "none");
                }
            }
        }

        protected void cbpMPEntryContent_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string param = e.Parameter;
            int pageIndex = Convert.ToInt32(hdnPageIndex.Value);
            int rowCount = Convert.ToInt32(hdnRowCount.Value);
            if (param == "new")
            {
                BasePageEntry.ReInitControl();
                pageIndex = -1;
            }
            else if (param == "next")
                BasePageEntry.NextPageIndex(rowCount, ref pageIndex);
            else if (param == "prev")
                BasePageEntry.PrevPageIndex(rowCount, ref pageIndex);
            else if (param == "load")
                BasePageEntry.LoadPage(pageIndex);
            else if (param == "refresh")
            {
                BasePageEntry.RefreshControl();
                pageIndex = -1;
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpParam"] = param;
            panel.JSProperties["cpPageIndex"] = pageIndex;
        }

        protected void cbpMPEntryProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string retval = "";
            string result = "";
            string[] param = e.Parameter.Split('|');
            if (param[0] == "save")
            {
                bool isAdd = (param[1] == "1");
                BasePageEntry.OnBtnSaveClick(ref result, ref retval, isAdd);
            }
            else if (param[0] == "void")
                BasePageEntry.OnBtnVoidClick(ref result);

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpRetval"] = retval;
        }
    }
}