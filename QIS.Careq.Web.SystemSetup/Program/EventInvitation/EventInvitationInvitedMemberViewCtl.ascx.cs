using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Data.Service;
using QIS.Careq.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class EventInvitationInvitedMemberViewCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("EventID = {0}", hdnID.Value);

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvEventInvitationRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            string orderBy = Helper.GetOrderByMemberColumn();
            List<vEventInvitation> lstEntity = BusinessLayer.GetvEventInvitationList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, orderBy);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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