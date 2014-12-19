using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Data.Service;
using QIS.Careq.Web.Common;
using QIS.Careq.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class MemberList : BasePageList
    {
        protected int PageCount = 1;
        protected int CurrPage = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.MEMBER;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            hdnFilterExpression.Value = filterExpression;
            hdnID.Value = keyValue;
            filterExpression = GetFilterExpression();
            if (keyValue != "")
            {
                string orderBy = Helper.GetOrderByMemberColumn();
                int row = BusinessLayer.GetvMemberRowIndex(filterExpression, keyValue, orderBy) + 1;
                CurrPage = Helper.GetPageCount(row, Constant.GridViewPageSize.GRID_MASTER);
            }
            else
            {
                CurrPage = 1;
            }
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);

            //txtFromCreatedDate.Text = DateTime.Today.AddYears(-2).ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            //txtToCreatedDate.Text = DateTime.Today.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                
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

        private string GetFilterExpression()
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += "IsDeleted = 0";
            return filterExpression;
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "Name", "Email", "Company", "Occupation Level", "Mobile Phone" };
            fieldListValue = new string[] { "FirstName MiddleName LastName", "EmailAddress1 EmailAddress2", "CompanyName", "OccupationLevel", "MobilePhoneNo1 MobilePhoneNo2" };
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvMemberRowCount(filterExpression);
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
            int rowCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount, ref rowCount);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridView(1, true, ref pageCount, ref rowCount);
                    result = "refresh|" + pageCount + "|" + rowCount;
                }
            }

            PageCount = pageCount;
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected override bool OnAddRecord(ref string url, ref string errMessage)
        {
            url = ResolveUrl("~/Program/Member/MemberEntry.aspx");
            return true;
        }

        protected override bool OnEditRecord(ref string url, ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                url = ResolveUrl(string.Format("~/Program/Member/MemberEntry.aspx?id={0}", hdnID.Value));
                return true;
            }
            return false;
        }

        protected override bool OnDeleteRecord(ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                Member entity = BusinessLayer.GetMember(Convert.ToInt32(hdnID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateMember(entity);
                return true;
            }
            return false;
        }
    }
}