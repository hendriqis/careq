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
    public partial class EventRegistrationDtEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.EVENT_REGISTRATION;
        }

        protected override void InitializeDataControl()
        {
            string[] param = Request.QueryString["id"].Split('|');
            hdnEventID.Value = param[0];
            if (param.Length > 1)
            {
                IsAdd = false;
                hdnMemberID.Value = param[1];
                SetControlProperties();
                vEventRegistration entity = BusinessLayer.GetvEventRegistrationList(string.Format("EventID = {0} AND MemberID = {1}", hdnEventID.Value, hdnMemberID.Value))[0];
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtMemberName.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(String.Format("ParentID IN ('{0}','{1}') AND IsDeleted = 0", Constant.StandardCode.REGISTRATION_TYPE, Constant.StandardCode.INFORMATION_SOURCE));
            Methods.SetComboBoxField<StandardCode>(cboGCRegistrationType, lst.Where(p => p.ParentID == Constant.StandardCode.REGISTRATION_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboGCInformationSource, lst.Where(p => p.ParentID == Constant.StandardCode.INFORMATION_SOURCE).ToList(), "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(hdnMemberID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtMemberName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(cboGCRegistrationType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboGCInformationSource, new ControlEntrySetting(true, true, true));

            SetControlEntrySetting(lblMember, new ControlEntrySetting(true, false));
        }

        private void EntityToControl(vEventRegistration entity)
        {
            hdnMemberID.Value = entity.MemberID.ToString();
            hdnCompanyID.Value = entity.CompanyID.ToString();
            hdnOccupation.Value = entity.Occupation;
            hdnGCOccupationLevel.Value = entity.GCOccupationLevel;
            cboGCRegistrationType.Value = entity.GCRegistrationType;
            cboGCInformationSource.Value = entity.GCInformationSource;
            txtMemberName.Text = entity.MemberName;
        }

        private void ControlToEntity(EventRegistration entity)
        {
            entity.MemberID = Convert.ToInt32(hdnMemberID.Value);
            entity.CompanyID = Convert.ToInt32(hdnCompanyID.Value);
            entity.Occupation = hdnOccupation.Value;
            entity.GCOccupationLevel = hdnGCOccupationLevel.Value;
            entity.GCRegistrationType = cboGCRegistrationType.Value.ToString();
            entity.GCInformationSource = cboGCInformationSource.Value.ToString();
        }

        EventRegistration er = null;
        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("EventID = {0} AND MemberID = {1}", hdnEventID.Value, hdnMemberID.Value);
            List<EventRegistration> lst = BusinessLayer.GetEventRegistrationList(FilterExpression);

            if (lst.Count > 0)
            {
                er = lst.FirstOrDefault();
                if (er.GCRegistrationStatus != Constant.RegistrationStatus.VOID)
                    errMessage = " This Member Has Already Registered To This Event!";
            }

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                EventRegistration entity = null;
                if (er != null && er.GCRegistrationStatus == Constant.RegistrationStatus.VOID)
                    entity = er;
                else
                    entity = new EventRegistration();
                ControlToEntity(entity);
                entity.GCRegistrationStatus = Constant.RegistrationStatus.OPEN;
                entity.EventID = Convert.ToInt32(hdnEventID.Value);
                entity.CertificatePrintNo = 0;
                entity.CertificationNo = "";
                if (er != null)
                {
                    entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateEventRegistration(entity);
                }
                else
                {
                    entity.CreatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.InsertEventRegistration(entity);
                }
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
                EventRegistration entity = BusinessLayer.GetEventRegistration(Convert.ToInt32(hdnEventID.Value), Convert.ToInt32(hdnMemberID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateEventRegistration(entity);
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