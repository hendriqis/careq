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
    public partial class CompanyList : BasePageList
    {
        protected int PageCount = 1;
        protected int CurrPage = 1;
        protected int RowCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.COMPANY;
        }

        public override string OnGetReportCode()
        {
            return Constant.ReportCode.COMPANY;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            hdnFilterExpression.Value = filterExpression;
            hdnID.Value = keyValue;
            filterExpression = GetFilterExpression();
            if (keyValue != "")
            {
                int row = BusinessLayer.GetCompanyRowIndex(filterExpression, keyValue, "CompanyName ASC") + 1;
                CurrPage = Helper.GetPageCount(row, Constant.GridViewPageSize.GRID_MASTER);
            }
            else
            {
                CurrPage = 1;
            }
            BindGridView(CurrPage, true, ref PageCount, ref RowCount);

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}','{2}') AND IsDeleted = 0 ORDER BY StandardCodeName ASC", Constant.StandardCode.COMPANY_TYPE, Constant.StandardCode.COMPANY_CERTIFICATION, Constant.StandardCode.COUNTY_OF_ORIGIN));
            Repeater rptCompanyType = (Repeater)ddeCompanyType.FindControl("rptCompanyType");
            rptCompanyType.DataSource = lstSc.Where(p => p.ParentID == Constant.StandardCode.COMPANY_TYPE).ToList();
            rptCompanyType.DataBind();

            Repeater rptCertificate = (Repeater)ddeCertificate.FindControl("rptCertificate");
            rptCertificate.DataSource = lstSc.Where(p => p.ParentID == Constant.StandardCode.COMPANY_CERTIFICATION).ToList();
            rptCertificate.DataBind();

            Repeater rptCountryOfOrigin = (Repeater)ddeCountryOfOrigin.FindControl("rptCountryOfOrigin");
            rptCountryOfOrigin.DataSource = lstSc.Where(p => p.ParentID == Constant.StandardCode.COUNTY_OF_ORIGIN).ToList();
            rptCountryOfOrigin.DataBind();
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
            fieldListText = new string[] { "Company Short Name","Company Name", "Company Type", "Phone No", "Group" };
            fieldListValue = new string[] { "ShortName", "CompanyName", "CompanyType", "PhoneNo1 PhoneNo2", "HoldingCompanyCode HoldingCompanyName" };
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount, ref int rowCount)
        {
            string filterExpression = GetFilterExpression();

            if (isCountPageCount)
            {
                rowCount = BusinessLayer.GetvCompanyRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vCompany> lstEntity = BusinessLayer.GetvCompanyList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "CompanyName ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vCompany entity = e.Row.DataItem as vCompany;
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
            url = ResolveUrl("~/Program/Company/CompanyEntry.aspx");
            return true;
        }

        protected override bool OnEditRecord(ref string url, ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                url = ResolveUrl(string.Format("~/Program/Company/CompanyEntry.aspx?id={0}", hdnID.Value));
                return true;
            }
            return false;
        }

        protected override bool OnDeleteRecord(ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                Company entity = BusinessLayer.GetCompany(Convert.ToInt32(hdnID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateCompany(entity);
                return true;
            }
            return false;
        }
    }
}