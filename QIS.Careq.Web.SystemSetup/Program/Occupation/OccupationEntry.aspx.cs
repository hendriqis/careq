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
    public partial class OccupationEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.OCCUPATION;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                Occupation entity = BusinessLayer.GetOccupation(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
                SetControlProperties();
            }
            txtOccupationCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0", Constant.StandardCode.OCCUPATION_LEVEL));
            Methods.SetComboBoxField<StandardCode>(cboGCOccupationLevel, lst, "StandardCodeName", "StandardCodeID");
            cboGCOccupationLevel.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtOccupationCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtOccupationName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboGCOccupationLevel, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(Occupation entity)
        {
            txtOccupationCode.Text = entity.OccupationCode;
            txtOccupationName.Text = entity.OccupationName;
            cboGCOccupationLevel.Value = entity.GCOccupationLevel;
        }

        private void ControlToEntity(Occupation entity)
        {
            entity.OccupationCode = txtOccupationCode.Text;
            entity.OccupationName = txtOccupationName.Text;
            entity.GCOccupationLevel = cboGCOccupationLevel.Value.ToString();
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string filterExpression = string.Format("OccupationCode = '{0}'", txtOccupationCode.Text);
            List<Occupation> lst = BusinessLayer.GetOccupationList(filterExpression);

            if (lst.Count > 0)
                errMessage = " Occupation With Code " + txtOccupationCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string filterExpression = string.Format("OccupationCode = '{0}' AND OccupationID != {1}", txtOccupationCode.Text, hdnID.Value);
            List<Occupation> lst = BusinessLayer.GetOccupationList(filterExpression);

            if (lst.Count > 0)
                errMessage = " Occupation With Code " + txtOccupationCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                Occupation entity = new Occupation();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertOccupation(entity);
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
                Occupation entity = BusinessLayer.GetOccupation(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateOccupation(entity);
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