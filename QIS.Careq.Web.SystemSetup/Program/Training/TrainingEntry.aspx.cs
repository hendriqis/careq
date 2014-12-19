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
    public partial class TrainingEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.TRAINING;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                Training entity = BusinessLayer.GetTraining(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
                SetControlProperties();
            }
            txtTrainingCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0", Constant.StandardCode.TRAINING_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboGCTrainingType, lst, "StandardCodeName", "StandardCodeID");
            cboGCTrainingType.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtTrainingCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTrainingName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboGCTrainingType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(chkIsHasCertification, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(Training entity)
        {
            txtTrainingCode.Text = entity.TrainingCode;
            txtTrainingName.Text = entity.TrainingName;
            cboGCTrainingType.Value = entity.GCTrainingType;
            chkIsHasCertification.Checked = entity.IsHasCertification;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(Training entity)
        {
            entity.TrainingCode = txtTrainingCode.Text;
            entity.TrainingName = txtTrainingName.Text;
            entity.GCTrainingType = cboGCTrainingType.Value.ToString();
            entity.IsHasCertification = chkIsHasCertification.Checked;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string filterExpression = string.Format("TrainingCode = '{0}'", txtTrainingCode.Text);
            List<Training> lst = BusinessLayer.GetTrainingList(filterExpression);

            if (lst.Count > 0)
                errMessage = " Training With Code " + txtTrainingCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string filterExpression = string.Format("TrainingCode = '{0}' AND TrainingID != {1}", txtTrainingCode.Text, hdnID.Value);
            List<Training> lst = BusinessLayer.GetTrainingList(filterExpression);

            if (lst.Count > 0)
                errMessage = " Training With Code " + txtTrainingCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                Training entity = new Training();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertTraining(entity);
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
                Training entity = BusinessLayer.GetTraining(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTraining(entity);
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