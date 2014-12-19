using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class LeadsRegistrationEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.LEADS_REGISTRATION;
        }

        public string GetOtherLeadSourceType()
        {
            return Constant.LeadSourceType.OTHER;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                SetControlProperties();
                vLead entity = BusinessLayer.GetvLeadList(String.Format("LeadID = {0}", ID))[0];
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                txtLeadDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                IsAdd = true;
            }
            txtSubject.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0", Constant.StandardCode.LEAD_SOURCE_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboLeadSourceType, lst, "StandardCodeName", "StandardCodeID");
            cboLeadSourceType.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtLeadNo, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtLeadDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboLeadSourceType, new ControlEntrySetting(true, true, true));
            
            SetControlEntrySetting(hdnCompanyID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtCompanyCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCompanyName, new ControlEntrySetting(false, false, true));

            SetControlEntrySetting(hdnEmployeeID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtEmployeeCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEmployeeName, new ControlEntrySetting(false, false, true));

            SetControlEntrySetting(txtMemberCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtSubject, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(vLead entity)
        {
            txtLeadNo.Text = entity.LeadNo;
            txtLeadDate.Text = entity.LeadDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            cboLeadSourceType.Value = entity.GCLeadSourceType;
            if (entity.GCLeadSourceType == Constant.LeadSourceType.OTHER) 
            {
                trOtherSourceType.Style.Remove("display");
                txtOtherLeadSourceType.Text = entity.OtherLeadSourceType;
            }
            else txtOtherLeadSourceType.Text = null;
            hdnCompanyID.Value = entity.CompanyID.ToString();
            txtCompanyName.Text = entity.CompanyName;
            txtCompanyCode.Text = entity.CompanyCode;
            hdnEmployeeID.Value = entity.PIC_CRO.ToString();
            txtEmployeeCode.Text = entity.PIC_CROCode;
            txtEmployeeName.Text = entity.PIC_CROName;
            hdnMemberID.Value = entity.MemberID.ToString();
            txtMemberCode.Text = entity.MemberCode;
            txtMemberName.Text = entity.MemberName;
            txtSubject.Text = entity.Subject;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(Lead entity)
        {
            entity.LeadNo = txtLeadNo.Text;
            entity.LeadDate = Helper.GetDatePickerValue(txtLeadDate.Text);
            entity.GCLeadSourceType = cboLeadSourceType.Value.ToString();
            if (entity.GCLeadSourceType == Constant.LeadSourceType.OTHER)
                entity.OtherLeadSourceType = txtOtherLeadSourceType.Text;
            else
                entity.OtherLeadSourceType = null;

            entity.CompanyID = Convert.ToInt32(hdnCompanyID.Value);
            entity.PIC_CRO = Convert.ToInt32(hdnEmployeeID.Value);
            entity.MemberID = Convert.ToInt32(hdnMemberID.Value);
            entity.Subject = txtSubject.Text;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("LeadNo = '{0}'", txtLeadNo.Text);
            List<Lead> lst = BusinessLayer.GetLeadList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Lead With No. " + txtLeadNo.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("LeadNo = '{0}' AND LeadID != {1}", txtLeadNo.Text, hdnID.Value);
            List<Lead> lst = BusinessLayer.GetLeadList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Lead With No. " + txtLeadNo.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                Lead entity = new Lead();
                ControlToEntity(entity);
                entity.LastUpdatedBy = entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.GCLeadStatus = Constant.LeadStatus.OPENED;
                BusinessLayer.InsertLead(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                Lead entity = BusinessLayer.GetLead(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateLead(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
    }
}