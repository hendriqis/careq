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
    public partial class EventRegistrationDtList : BasePageList
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.EVENT_REGISTRATION;
        }
        public override string  OnGetMenuCodeRightPanel()
        {
            return String.Format("{0}DT", Constant.MenuCode.EVENT_REGISTRATION);
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            hdnEventID.Value = Request.QueryString["id"];
            BindGridView(1, true, ref PageCount);
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "Name", "Company", "Occupation", "Registration Type","Registration Status","Information Source" };
            fieldListValue = new string[] { "FirstName MiddleName LastName", "CompanyName", "Occupation", "RegistrationType", "RegistrationStatus", "InformationSource" };
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("EventID = {0} AND GCRegistrationStatus != '{1}'", hdnEventID.Value, Constant.RegistrationStatus.VOID);

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvEventRegistrationRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            //string orderBy = Helper.GetOrderByMemberColumn();
            List<vEventRegistration> lstEntity = BusinessLayer.GetvEventRegistrationList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "CompanyName");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vEventRegistration entity = e.Row.DataItem as vEventRegistration;

                CheckBox chkMember = (CheckBox)e.Row.FindControl("chkMember");
                if (entity.GCRegistrationStatus == Constant.RegistrationStatus.VOID || entity.GCRegistrationStatus == Constant.RegistrationStatus.CANCELLED)
                    chkMember.Visible = false;
                else
                    chkMember.Visible = true;

                if (lstSelectedMember.Contains(entity.MemberID.ToString()))
                    chkMember.Checked = true;
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

        protected override bool OnAddRecord(ref string url, ref string errMessage)
        {
            url = ResolveUrl(string.Format("~/Program/EventRegistration/EventRegistrationDtEntry.aspx?id={0}", hdnEventID.Value));
            return true;
        }

        protected override bool OnEditRecord(ref string url, ref string errMessage)
        {
            if (hdnMemberID.Value.ToString() != "")
            {
                url = ResolveUrl(string.Format("~/Program/EventRegistration/EventRegistrationDtEntry.aspx?id={0}|{1}", hdnEventID.Value, hdnMemberID.Value));
                return true;
            }
            return false;
        }

        protected override bool OnDeleteRecord(ref string errMessage)
        {
            if (hdnMemberID.Value.ToString() != "")
            {
                EventRegistration entity = BusinessLayer.GetEventRegistration(Convert.ToInt32(hdnEventID.Value), Convert.ToInt32(hdnMemberID.Value));
                entity.GCRegistrationStatus = Constant.RegistrationStatus.VOID;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateEventRegistration(entity);
                return true;
            }
            return false;
        }
    }
}