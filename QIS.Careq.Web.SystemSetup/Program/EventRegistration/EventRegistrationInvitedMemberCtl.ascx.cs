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

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class EventRegistrationInvitedMemberCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;

            List<Variable> lstVariable = new List<Variable>();
            lstVariable.Add(new Variable { Code = "0", Value = "Semua" });
            lstVariable.Add(new Variable { Code = "1", Value = "Sudah Konfirmasi" });
            lstVariable.Add(new Variable { Code = "2", Value = "Belum Konfirmasi" });
            Methods.SetComboBoxField<Variable>(cboListType, lstVariable, "Value", "Code");
            cboListType.Value = "1";

            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("EventID = {0} AND MemberID NOT IN (SELECT MemberID FROM EventRegistration WHERE EventID = {0})", hdnID.Value);

            if (cboListType.Value.ToString() == "1")
                filterExpression += " AND IsConfirmed = 1";
            else if (cboListType.Value.ToString() == "2")
                filterExpression += " AND IsConfirmed = 0";

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvEventInvitationRowCount(filterExpression);
                //pageCount = Helper.GetPageCount(rowCount, 2);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            string orderBy = Helper.GetOrderByMemberColumn();
            //List<vEventInvitation> lstEntity = BusinessLayer.GetvEventInvitationList(filterExpression, 2, pageIndex);
            List<vEventInvitation> lstEntity = BusinessLayer.GetvEventInvitationList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, orderBy);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vEventInvitation entity = e.Row.DataItem as vEventInvitation;

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

        protected void cbpCreateRegistrationProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            List<vMember> lstMember = BusinessLayer.GetvMemberList(string.Format("MemberID IN ({0})", hdnSelectedMember.Value.Substring(1)));
            List<SettingParameter> lstSettingParameter = BusinessLayer.GetSettingParameterList(string.Format("ParameterCode IN ('{0}','{1}')", Constant.SettingParameter.REGISTRATION_TYPE_BY_EMAIL, Constant.SettingParameter.INFORMATION_SOURCE_BY_EMAIL));
            string GCRegistrationType = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.REGISTRATION_TYPE_BY_EMAIL).ParameterValue;
            string GCInformationSource = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.INFORMATION_SOURCE_BY_EMAIL).ParameterValue;

            IDbContext ctx = DbFactory.Configure(true);
            EventRegistrationDao entityDao = new EventRegistrationDao(ctx);
            try
            {
                foreach (vMember member in lstMember)
                {
                    EventRegistration entity = new EventRegistration();
                    entity.MemberID = member.MemberID;
                    entity.CompanyID = member.CompanyID;
                    entity.Occupation = member.Occupation;
                    entity.GCOccupationLevel = member.GCOccupationLevel;
                    entity.GCRegistrationType = GCRegistrationType;
                    entity.GCInformationSource = GCInformationSource;
                    entity.CertificationNo = "";
                    entity.CertificatePrintNo = 0;
                    entity.GCRegistrationStatus = Constant.RegistrationStatus.OPEN;
                    entity.EventID = Convert.ToInt32(hdnID.Value);
                    entity.CreatedBy = AppSession.UserLogin.UserID;
                    entityDao.Insert(entity);
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