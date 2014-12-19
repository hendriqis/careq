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
    public partial class EventInvitationConfirmationDt : BasePageList
    {
        protected int PageCount = 1;
        protected int CurrPage = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.EVENT_INVITATION_CONFIRMATION;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "Member Name", "Company Name" };
            fieldListValue = new string[] { "FirstName MiddleName LastName", "CompanyName" };
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            hdnID.Value = Request.QueryString["id"];
            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("EventID = {0} AND MemberID NOT IN (SELECT MemberID FROM EventRegistration WHERE EventID = {0})", hdnID.Value);

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvEventInvitationRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vEventInvitation> lstEntity = BusinessLayer.GetvEventInvitationList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vEventInvitation entity = e.Row.DataItem as vEventInvitation;
                if (entity.IsConfirmed)
                {
                    HtmlInputText txtConfirmDate = e.Row.FindControl("txtConfirmDate") as HtmlInputText;
                    txtConfirmDate.Value = entity.ConfirmedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                }
            }
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref PageCount);
                    result = "changepage";
                }
                else // refresh
                {

                    BindGridView(1, true, ref PageCount);
                    result = "refresh|" + PageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected void cbpSaveConfirm_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            try
            {
                string[] param = e.Parameter.Split('|');
                int memberID = Convert.ToInt32(param[0]);
                bool isConfirm = (param[1] == "1");
                DateTime confirmDate = Helper.GetDatePickerValue(param[2]);

                EventInvitation entity = BusinessLayer.GetEventInvitationList(string.Format("EventID = {0} AND MemberID = {1}", hdnID.Value, memberID))[0];
                entity.IsConfirmed = isConfirm;
                if (isConfirm)
                    entity.ConfirmedDate = confirmDate;
                BusinessLayer.UpdateEventInvitation(entity);
                result = "success";
            }
            catch (Exception ex)
            {
                result = string.Format("fail|{0}", ex.Message);
            }
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;            
        }
    }
}