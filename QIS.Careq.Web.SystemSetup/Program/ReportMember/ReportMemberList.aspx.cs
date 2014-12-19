using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;
using System.Text.RegularExpressions;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class ReportMemberList : BasePageList
    {
        protected int PageCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.REPORT_MEMBER_LIST;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            txtRatingLevel.Attributes.Add("validationgroup", "mpEntryPopup");
            BindGridView(1, true, ref PageCount);

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}','{2}','{3}','{4}') AND IsDeleted = 0 ORDER BY StandardCodeName ASC", Constant.StandardCode.OCCUPATION_LEVEL, Constant.StandardCode.COMPANY_CERTIFICATION, Constant.StandardCode.COMPANY_DEPARTMENT, Constant.StandardCode.MEMBER_STATUS, Constant.StandardCode.HOLIDAY_GREETINGS));

            Repeater rptOccupationLevel = (Repeater)ddeOccupationLevel.FindControl("rptOccupationLevel");
            rptOccupationLevel.DataSource = lstSc.Where(p => p.ParentID == Constant.StandardCode.OCCUPATION_LEVEL).ToList();
            rptOccupationLevel.DataBind();

            Repeater rptCertificate = (Repeater)ddeCertificate.FindControl("rptCertificate");
            rptCertificate.DataSource = lstSc.Where(p => p.ParentID == Constant.StandardCode.COMPANY_CERTIFICATION).ToList();
            rptCertificate.DataBind();

            Repeater rptDepartment = (Repeater)ddeDepartment.FindControl("rptDepartment");
            rptDepartment.DataSource = lstSc.Where(p => p.ParentID == Constant.StandardCode.COMPANY_DEPARTMENT).ToList();
            rptDepartment.DataBind();

            Repeater rptMemberStatus = (Repeater)ddeMemberStatus.FindControl("rptMemberStatus");
            rptMemberStatus.DataSource = lstSc.Where(p => p.ParentID == Constant.StandardCode.MEMBER_STATUS).ToList();
            rptMemberStatus.DataBind();

            Repeater rptMemberGreetings = (Repeater)ddeMemberGreetings.FindControl("rptMemberGreetings");
            rptMemberGreetings.DataSource = lstSc.Where(p => p.ParentID == Constant.StandardCode.HOLIDAY_GREETINGS).ToList();
            rptMemberGreetings.DataBind();
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += "IsDeleted = 0";

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvMemberRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            string orderBy = Helper.GetOrderByMemberColumn();
            List<vMember> lstEntity = BusinessLayer.GetvMemberList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, orderBy);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vMember entity = e.Row.DataItem as vMember;

                for (int i = entity.RatingLevel; i > 0; --i)
                {
                    HtmlGenericControl div = (HtmlGenericControl)e.Row.FindControl("divRating" + i);
                    div.Attributes.Remove("class");
                    div.Attributes.Add("class", "starselected");
                }
            }
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