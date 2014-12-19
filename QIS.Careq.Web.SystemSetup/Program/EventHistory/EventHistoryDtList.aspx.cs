using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;
using System.Net.Mail;
using System.Text.RegularExpressions;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class EventHistoryDtList : BasePageList
    {
        protected int PageCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.EVENT_HISTORY;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            hdnEventID.Value = Request.QueryString["id"];
            BindGridView(1, true, ref PageCount);
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "Name", "Company", "Occupation", "Department", "Registration Type", "Registration Status", "Information Source" };
            fieldListValue = new string[] { "FirstName MiddleName LastName", "CompanyName", "Occupation", "Department", "RegistrationType", "RegistrationStatus", "InformationSource" };
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("EventID = {0} AND GCRegistrationStatus = '{1}'", hdnEventID.Value, Constant.RegistrationStatus.CONFIRMED);

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvEventRegistrationRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            string orderBy = Helper.GetOrderByMemberColumn();
            List<vEventRegistration> lstEntity = BusinessLayer.GetvEventRegistrationList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, orderBy);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else // refresh
                {

                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}