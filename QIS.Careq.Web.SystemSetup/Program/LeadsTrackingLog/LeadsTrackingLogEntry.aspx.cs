using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;
using DevExpress.Web.ASPxCallbackPanel;
using QIS.Data.Core.Dal;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class LeadsTrackingLogEntry : BasePageEntry
    {
        protected bool IsEditable = true;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.LEADS_TRACKING_LOG_ENTRY;
        }
        
        protected override void InitializeDataControl()
        {
            String ID = Request.QueryString["id"];
            hdnID.Value = ID;
            vLead entity = BusinessLayer.GetvLeadList(String.Format("LeadID = {0}", ID))[0];
            EntityToControl(entity);
            SetControlProperties();
            BindGridView();
            txtSubject.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0", Constant.StandardCode.ACTIVITY_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboActivityType, lst, "StandardCodeName", "StandardCodeID");
            cboActivityType.SelectedIndex = 0;

            txtLogDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtLogTime.Text = DateTime.Now.ToString("HH:mm");
        }

        private void EntityToControl(vLead entity)
        {
            txtLeadNo.Text = entity.LeadNo;
            txtLeadDate.Text = entity.LeadDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtSubject.Text = entity.Subject;
            txtRemarks.Text = entity.Remarks;
        }

        private void EntityToControlDt(vLeadActivityLog entity) 
        {
            txtLogDate.Text = entity.LogDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtLogTime.Text = entity.LogTime;
            cboActivityType.Value = entity.GCActivityType;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(Lead entity)
        {
            entity.LeadNo = txtLeadNo.Text;
            entity.LeadDate = Helper.GetDatePickerValue(txtLeadDate.Text);
            //entity.GCLeadSourceType = cboLeadSourceType.Value.ToString();
            //if (entity.GCLeadSourceType == Constant.LeadSourceType.OTHER)
            //    entity.OtherLeadSourceType = txtOtherLeadSourceType.Text;
            //else
            //    entity.OtherLeadSourceType = null;
            
            //entity.CompanyID = Convert.ToInt32(hdnCompanyID.Value);
            //entity.MemberID = Convert.ToInt32(hdnMemberID.Value);
            entity.Subject = txtSubject.Text;
            entity.Remarks = txtRemarks.Text;
        }

        private void ControlToEntityDt(LeadActivityLog entity) 
        {
            entity.LogDate = Helper.GetDatePickerValue(txtLogDate.Text);
            entity.LogTime = txtLogTime.Text;
            entity.LeadID = Convert.ToInt32(hdnID.Value);
            entity.GCActivityType = cboActivityType.Value.ToString();
            entity.Remarks = txtRemarks.Text;
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                BindGridView();
                result = "refresh";
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void BindGridView() 
        {
            string filterExpression = "1 = 0";
            if (hdnID.Value != "")
                filterExpression = String.Format("LeadID = {0}",hdnID.Value);
            
            List<vLeadActivityLog> lstEntity = BusinessLayer.GetvLeadActivityLogList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        private bool OnSaveEditRecordEntityDt(ref String errMessage) 
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            LeadDao leadDao = new LeadDao(ctx);
            LeadActivityLogDao leadActivityLogDao = new LeadActivityLogDao(ctx);
            try
            {
                LeadActivityLog entity = leadActivityLogDao.Get(Convert.ToInt32(hdnEntryID.Value));
                Lead entityLead = leadDao.Get(Convert.ToInt32(hdnID.Value));
                ControlToEntityDt(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entity.LastUpdatedDate = DateTime.Now;
                entityLead.ResponseDate = DateTime.Now;
                entityLead.ResponseTime = DateTime.Now.ToString("HH:mm");
                entityLead.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityLead.LastUpdatedDate = DateTime.Now;
                leadActivityLogDao.Update(entity);
                leadDao.Update(entityLead);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally 
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnSaveAddRecordEntityDt(ref String errMessage) 
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            LeadDao leadDao = new LeadDao(ctx);
            LeadActivityLogDao leadActivityLogDao = new LeadActivityLogDao(ctx);
            try
            {
                Lead entityLead = leadDao.Get(Convert.ToInt32(hdnID.Value));
                LeadActivityLog entity = new LeadActivityLog();
                ControlToEntityDt(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.CreatedDate = DateTime.Now;
                entityLead.ResponseDate = DateTime.Now;
                entityLead.ResponseTime = DateTime.Now.ToString("HH:mm");
                entityLead.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityLead.LastUpdatedDate = DateTime.Now;
                leadDao.Update(entityLead);
                leadActivityLogDao.Insert(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally 
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnDeleteEntityDt(ref String errMessage) 
        {
            bool result = true;
            try
            {
                BusinessLayer.DeleteLeadActivityLog(Convert.ToInt32(hdnEntryID.Value));
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
            }
            return result;
        }

        #region Process Detail
        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
        #endregion
    }
}