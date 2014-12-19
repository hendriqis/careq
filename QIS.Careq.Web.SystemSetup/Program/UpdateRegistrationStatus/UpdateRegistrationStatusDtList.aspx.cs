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
using DevExpress.Web.ASPxEditors;
using QIS.Data.Core.Dal;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class UpdateRegistrationStatusDtList : BasePageList
    {
        protected int PageCount = 1;
        protected int PageCountPerMember = 1;
        private string[] lstSelectedMember = null;
        private List<StandardCode> lstRegistrationStatus = null;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.UPDATE_REGISTRATION_STATUS;
        }

        public override string OnGetMenuCodeRightPanel()
        {
            return String.Format("{0}DT", Constant.MenuCode.UPDATE_REGISTRATION_STATUS);
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowDelete = IsAllowEdit = false;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            hdnEventID.Value = Request.QueryString["id"];

            lstRegistrationStatus = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsDeleted = 0", Constant.StandardCode.REGISTRATION_STATUS));
            Methods.SetComboBoxField<StandardCode>(cboUpdateStatusTo, lstRegistrationStatus, "StandardCodeName", "StandardCodeID");
            cboUpdateStatusTo.SelectedIndex = 0;

            BindGridView(1, true, ref PageCount);
            BindGridViewPerMember(1, true, ref PageCountPerMember);            
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "Name", "Company", "Occupation", "Department", "Registration Type","Registration Status","Information Source" };
            fieldListValue = new string[] { "FirstName MiddleName LastName", "CompanyName", "Occupation", "Department", "RegistrationType", "RegistrationStatus", "InformationSource" };
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            if (cboUpdateStatusTo.Value != null)
            {
                string registrationStatus = cboUpdateStatusTo.Value.ToString();

                string filterExpression = hdnFilterExpression.Value;
                if (filterExpression != "")
                    filterExpression += " AND ";
                filterExpression += string.Format("EventID = {0} AND GCRegistrationStatus != '{1}'", hdnEventID.Value, Constant.RegistrationStatus.VOID);

                //switch (registrationStatus)
                //{
                //    case Constant.RegistrationStatus.CHECKED_IN:
                //    case Constant.RegistrationStatus.CANCELLED:
                //    case Constant.RegistrationStatus.NO_SHOW:
                //        filterExpression += string.Format(" AND GCRegistrationStatus IN ('{0}','{1}','{2}')", Constant.RegistrationStatus.OPEN, Constant.RegistrationStatus.CONFIRMED, Constant.RegistrationStatus.WAITING_FOR_CONFIRMATION);break;
                //}

                if (registrationStatus == Constant.RegistrationStatus.CANCELLED)
                    filterExpression += string.Format(" AND GCRegistrationStatus IN ('{0}','{1}')", Constant.RegistrationStatus.OPEN, Constant.RegistrationStatus.CONFIRMED);

                if (isCountPageCount)
                {
                    int rowCount = BusinessLayer.GetvEventRegistrationRowCount(filterExpression);
                    pageCount = Helper.GetPageCount(rowCount, 16);
                }
                lstSelectedMember = hdnSelectedMember.Value.Split(',');
                //string orderBy = Helper.GetOrderByMemberColumn();
                List<vEventRegistration> lstEntity = BusinessLayer.GetvEventRegistrationList(filterExpression, 16, pageIndex, "CompanyName");
                grdView.DataSource = lstEntity;
                grdView.DataBind();
            }
        }

        private void BindGridViewPerMember(int pageIndex, bool isCountPageCount, ref int pageCount)
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
            if (lstRegistrationStatus == null)
                lstRegistrationStatus = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsDeleted = 0", Constant.StandardCode.REGISTRATION_STATUS));

            string orderBy = Helper.GetOrderByMemberColumn();
            List<vEventRegistration> lstEntity = BusinessLayer.GetvEventRegistrationList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, orderBy);
            grdViewPerMember.DataSource = lstEntity;
            grdViewPerMember.DataBind();
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

        protected void grdViewPerMember_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vEventRegistration entity = e.Row.DataItem as vEventRegistration;
                ASPxComboBox cboRegistrationStatus = e.Row.FindControl("cboRegistrationStatus") as ASPxComboBox;
                Methods.SetComboBoxField<StandardCode>(cboRegistrationStatus, lstRegistrationStatus, "StandardCodeName", "StandardCodeID");
                cboRegistrationStatus.Value = entity.GCRegistrationStatus;
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

        protected void cbpViewPerMember_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridViewPerMember(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else // refresh
                {

                    BindGridViewPerMember(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected void cbpSaveRegistrationStatus_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            try
            {
                string[] param = e.Parameter.Split('|');
                int memberID = Convert.ToInt32(param[0]);
                String GCRegistrationStatus = param[1];

                EventRegistration entity = BusinessLayer.GetEventRegistrationList(string.Format("EventID = {0} AND MemberID = {1}", hdnEventID.Value, memberID))[0];
                entity.GCRegistrationStatus = GCRegistrationStatus;
                BusinessLayer.UpdateEventRegistration(entity);
                result = "success";
            }
            catch (Exception ex)
            {
                result = string.Format("fail|{0}", ex.Message);
            }
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected void cbpProcessAll_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            IDbContext ctx = DbFactory.Configure(true);
            EventRegistrationDao entityDao = new EventRegistrationDao(ctx);
            string result = "";
            try
            {
                string GCRegistrationStatus = cboUpdateStatusTo.Value.ToString();
                List<EventRegistration> lstEntity = BusinessLayer.GetEventRegistrationList(string.Format("EventID = {0} AND MemberID IN ({1})", hdnEventID.Value, hdnSelectedMember.Value.Substring(1)));
                foreach (EventRegistration entity in lstEntity)
                {
                    entity.GCRegistrationStatus = GCRegistrationStatus;
                    entityDao.Update(entity);
                }
                result = "success";
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                result = string.Format("fail|{0}", ex.Message);
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}