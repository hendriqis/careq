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
    public partial class TransferContactList : BasePageList
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.TRANSFER_CONTACT_INFORMATION_TO_OUTLOOK_CONTACTS;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            txtLastName.Attributes.Add("validationgroup", "mpEntryPopup");
            txtRatingLevel.Attributes.Add("validationgroup", "mpEntryPopup");
            hdnID.Value = Request.QueryString["id"];
            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("IsDeleted = 0", hdnID.Value);

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
                Response.AddHeader("content-disposition", "attachment;filename=ContactMember.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";

                StringBuilder sbResult = new StringBuilder();
                List<vMember> lstMember = BusinessLayer.GetvMemberList(string.Format("MemberID IN ({0})", hdnSelectedMember.Value.Substring(1)));
                sbResult.Append("Title, First Name, Middle Name, Last Name, Suffix, Company, Department, Job Title, Home Phone, Mobile Phone, E-mail Address, E-mail 2 Address,");
                sbResult.Append("Home Street, Home City, Home State, Home Postal Code,");
                sbResult.Append("Business Street, Business City, Business State, Business Postal Code");
                sbResult.Append("\r\n");
                foreach (vMember member in lstMember)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(member.Title).Append(",").Append(member.FirstName).Append(",").Append(member.MiddleName).Append(",");
                    sb.Append(member.LastName).Append(",").Append(member.Suffix).Append(",").Append(member.CompanyName).Append(",").Append(member.Department).Append(",");
                    sb.Append(member.Occupation).Append(",").Append(member.PhoneNo1).Append(",").Append(member.MobilePhoneNo1).Append(",");
                    sb.Append(member.EmailAddress1).Append(",").Append(member.EmailAddress2).Append(",");
                    sb.Append(CsvEscape(member.StreetName)).Append(",").Append(member.City).Append(",").Append(member.Province).Append(",").Append(member.ZipCode).Append(",");
                    sb.Append(CsvEscape(member.CompanyStreetName)).Append(",").Append(member.CompanyCity).Append(",").Append(member.CompanyProvince).Append(",").Append(member.CompanyZipCode).Append(",");
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