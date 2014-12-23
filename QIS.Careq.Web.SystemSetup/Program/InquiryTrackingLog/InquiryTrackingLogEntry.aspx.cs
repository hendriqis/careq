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
    public partial class InquiryTrackingLogEntry : BasePageList
    {
        protected bool IsEditable = true;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.INQUIRY_TRACKING_LOG_ENTRY;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            String ID = Request.QueryString["id"];
            hdnID.Value = ID;
            vInquiry entity = BusinessLayer.GetvInquiryList(String.Format("InquiryID = {0}", ID))[0];
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

        private void EntityToControl(vInquiry entity)
        {
            hdnDefaultEmployeeID.Value = entity.PIC_CRO.ToString();
            hdnDefaultEmployeeCode.Value = entity.PIC_CROCode;
            hdnDefaultEmployeeName.Value = entity.PIC_CROName;

            txtInquiryNo.Text = entity.InquiryNo;
            txtInquiryDate.Text = entity.InquiryDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtSubject.Text = entity.Subject;
            txtRemarks.Text = entity.Remarks;
        }

        private void EntityToControlDt(vInquiryActivityLog entity) 
        {
            txtLogDate.Text = entity.LogDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtLogTime.Text = entity.LogTime;
            cboActivityType.Value = entity.GCActivityType;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(Inquiry entity)
        {
            entity.InquiryNo = txtInquiryNo.Text;
            entity.InquiryDate = Helper.GetDatePickerValue(txtInquiryDate.Text);
            entity.Subject = txtSubject.Text;
            entity.Remarks = txtRemarks.Text;
        }

        private void ControlToEntityDt(InquiryActivityLog entity) 
        {
            entity.LogDate = Helper.GetDatePickerValue(txtLogDate.Text);
            entity.LogTime = txtLogTime.Text;
            entity.InquiryID = Convert.ToInt32(hdnID.Value);
            entity.GCActivityType = cboActivityType.Value.ToString();
            entity.CRO = Convert.ToInt32(hdnEmployeeID.Value);
            entity.TrainerID = Convert.ToInt32(hdnTrainerID.Value);
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
                filterExpression = String.Format("InquiryID = {0} AND IsDeleted = 0",hdnID.Value);
            
            List<vInquiryActivityLog> lstEntity = BusinessLayer.GetvInquiryActivityLogList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        private bool OnSaveEditRecordEntityDt(ref String errMessage) 
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            InquiryActivityLogDao leadActivityLogDao = new InquiryActivityLogDao(ctx);
            try
            {
                InquiryActivityLog entity = leadActivityLogDao.Get(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntityDt(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entity.LastUpdatedDate = DateTime.Now;
                leadActivityLogDao.Update(entity);
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
            InquiryActivityLogDao leadActivityLogDao = new InquiryActivityLogDao(ctx);
            try
            {
                InquiryActivityLog entity = new InquiryActivityLog();
                ControlToEntityDt(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.CreatedDate = DateTime.Now;
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
            IDbContext ctx = DbFactory.Configure(true);
            InquiryActivityLogDao leadActivityLogDao = new InquiryActivityLogDao(ctx);
            try
            {
                InquiryActivityLog entity = new InquiryActivityLog();
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entity.LastUpdatedDate = DateTime.Now;
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