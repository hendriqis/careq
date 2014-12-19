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
using QIS.Data.Core.Dal;
using System.Data;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class EventRegistrationPrintNameTagCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            hdnIsRevisionNoChanged.Value = "0";
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
                int rowCount = BusinessLayer.GetvEventRegistrationRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MATRIX);

                List<Int32> lstMemberID = BusinessLayer.GetvEventRegistrationMemberIDList(filterExpression);
                hdnListAllMemberID.Value = String.Format(";{0}", String.Join(";", lstMemberID.ToArray()));
            }

            lstSelectedMember = hdnSelectedMember.Value.Split(';');

            string orderBy = Helper.GetOrderByMemberColumn();
            List<vEventRegistration> lstEntity = BusinessLayer.GetvEventRegistrationList(filterExpression, Constant.GridViewPageSize.GRID_MATRIX, pageIndex, orderBy);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vEventRegistration entity = e.Row.DataItem as vEventRegistration;

                CheckBox chkMember = (CheckBox)e.Row.FindControl("chkMember");
                if (lstSelectedMember.Contains(entity.MemberID.ToString()))
                    chkMember.Checked = true;
            }
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