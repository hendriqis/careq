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
using System.Text;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class MailingLabelList : BasePageList
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.MAILING_LABEL_LIST;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            txtMemberName.Attributes.Add("validationgroup", "mpEntryPopup");
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

            lstSelectedMember = hdnSelectedMember.Value.Split(',');
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

                CheckBox chkMember = (CheckBox)e.Row.FindControl("chkMember");
                if (lstSelectedMember.Contains(entity.MemberID.ToString()))
                    chkMember.Checked = true;

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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string result = "";
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=MailingLabel.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";

                StringBuilder sbResult = new StringBuilder();
                List<vMember> lstMember = new List<vMember>();
                if (string.IsNullOrEmpty(hdnSelectedMember.Value))
                {
                    lstMember = BusinessLayer.GetvMemberList(string.Empty);
                }
                else
                {
                    lstMember = BusinessLayer.GetvMemberList(string.Format("MemberID IN ({0})", hdnSelectedMember.Value.Substring(1)));
                }

                sbResult.Append("Member Name, Occupation, Company, Address, Telephone, Email 1, Email 2, Phone 1, Phone 2");
                sbResult.Append("\r\n");
                foreach (vMember member in lstMember)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(member.MemberNameWithSalutation).Append(",").Append(member.Occupation).Append(",").Append(member.CompanyName).Append(",");
                    sb.Append(member.CompanyAddress).Append(",").Append(member.CompanyPhoneNo1).Append(",");
                    sb.Append(member.EmailAddress1).Append(",").Append(member.EmailAddress2).Append(",'").Append(member.MobilePhoneNo1).Append(",'").Append(member.MobilePhoneNo2);
                    sb.Replace("\r", " ").Replace("\n", " ");
                    sb.Append("\r\n");

                    sbResult.Append(sb);
                }
                Response.Output.Write(sbResult.ToString());
                result = "success";
            }
            catch (Exception ex)
            {
                result = string.Format("fail|{0}", ex.Message);
            }
            finally
            {
                Response.Flush();
                Response.End();
            }
        }

        private string CsvEscape(string value)
        {
            if (value.Contains(","))
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }
            return value;
        }
    }
}