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
    public partial class TrainerEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.TRAINER;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                Trainer entity = BusinessLayer.GetTrainer(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
                SetControlProperties();
            }
            txtTrainerCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}','{2}') AND IsDeleted = 0", Constant.StandardCode.TITLE, Constant.StandardCode.SALUTATION, Constant.StandardCode.SUFFIX));
            lstSc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
            Methods.SetComboBoxField<StandardCode>(cboSalutation, lstSc.Where(sc => sc.ParentID == Constant.StandardCode.SALUTATION || sc.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboTitle, lstSc.Where(sc => sc.ParentID == Constant.StandardCode.TITLE || sc.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboSuffix, lstSc.Where(sc => sc.ParentID == Constant.StandardCode.SUFFIX || sc.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
           
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtTrainerCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboSalutation, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboTitle, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtFirstName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMiddleName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtLastName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboSuffix, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(txtEmail, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMobilePhone1, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMobilePhone2, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(Trainer entity)
        {
            txtTrainerCode.Text = entity.TrainerCode;
            cboSalutation.Value = entity.GCSalutation;
            cboTitle.Value = entity.GCTitle;
            txtFirstName.Text = entity.FirstName;
            txtMiddleName.Text = entity.MiddleName;
            txtLastName.Text = entity.LastName;
            cboSuffix.Value = entity.GCSuffix;

            txtEmail.Text = entity.EmailAddress;
            txtMobilePhone1.Text = entity.MobilePhone1;
            txtMobilePhone2.Text = entity.MobilePhone2;

            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(Trainer entity)
        {
            entity.TrainerCode = txtTrainerCode.Text;
            if (cboSalutation.Value != null && cboSalutation.Value.ToString() != "")
                entity.GCSalutation = cboSalutation.Value.ToString();
            else
                entity.GCSalutation = null;
            if (cboTitle.Value != null && cboTitle.Value.ToString() != "")
                entity.GCTitle = cboTitle.Value.ToString();
            else
                entity.GCTitle = null;
            entity.FirstName = txtFirstName.Text;
            entity.MiddleName = txtMiddleName.Text;
            entity.LastName = txtLastName.Text;
            if (cboSuffix.Value != null && cboSuffix.Value.ToString() != "")
                entity.GCSuffix = cboSuffix.Value.ToString();
            else
                entity.GCSuffix = null;

            entity.EmailAddress = txtEmail.Text;
            entity.MobilePhone1 = txtMobilePhone1.Text;
            entity.MobilePhone2 = txtMobilePhone2.Text;

            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string filterExpression = string.Format("TrainerCode = '{0}'", txtTrainerCode.Text);
            List<Trainer> lst = BusinessLayer.GetTrainerList(filterExpression);

            if (lst.Count > 0)
                errMessage = " Trainer With Code " + txtTrainerCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string filterExpression = string.Format("TrainerCode = '{0}' AND TrainerID != {1}", txtTrainerCode.Text, hdnID.Value);
            List<Trainer> lst = BusinessLayer.GetTrainerList(filterExpression);

            if (lst.Count > 0)
                errMessage = " Trainer With Code " + txtTrainerCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                Trainer entity = new Trainer();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertTrainer(entity);
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
                Trainer entity = BusinessLayer.GetTrainer(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateTrainer(entity);
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