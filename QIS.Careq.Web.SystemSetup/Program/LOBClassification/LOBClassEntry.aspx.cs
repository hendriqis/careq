using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;
using System.Text;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class LOBClassEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.LOB_CLASSIFICATION;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String LOBClassID = Request.QueryString["id"];
                hdnID.Value = LOBClassID;
                LOBClassification entity = BusinessLayer.GetLOBClassification(Convert.ToInt32(LOBClassID));
                LOBClassification entityParent = null;
                if(entity.ParentID != null && entity.ParentID > 0)
                    entityParent = BusinessLayer.GetLOBClassification((int)entity.ParentID);
                EntityToControl(entity, entityParent);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtLOBClassCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtLOBClassCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtLOBClassName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtParentCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtParentName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnParentID, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(LOBClassification entity, LOBClassification entityParent)
        {
            txtLOBClassCode.Text = entity.LOBClassCode;
            txtLOBClassName.Text = entity.LOBClassName;
            hdnParentID.Value = entity.ParentID.ToString();
            if (entityParent != null)
            {
                txtParentCode.Text = entityParent.LOBClassCode;
                txtParentName.Text = entityParent.LOBClassName;
            }
        }

        private void ControlToEntity(LOBClassification entity)
        {
            entity.LOBClassCode = txtLOBClassCode.Text;
            entity.LOBClassName = txtLOBClassName.Text;
            if (hdnParentID.Value == "" || hdnParentID.Value == "0")
                entity.ParentID = null;
            else
                entity.ParentID = Convert.ToInt32(hdnParentID.Value);
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("LOBClassCode = '{0}'", txtLOBClassCode.Text);
            List<LOBClassification> lst = BusinessLayer.GetLOBClassificationList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " LOB Class with Code " + txtLOBClassCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            Int32 menuID = Convert.ToInt32(hdnID.Value);
            string FilterExpression = string.Format("LOBClassCode = '{0}' AND LOBClassID != {1}", txtLOBClassCode.Text, menuID);
            List<LOBClassification> lst = BusinessLayer.GetLOBClassificationList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " LOB Class with Code " + txtLOBClassCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                LOBClassification entity = new LOBClassification();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertLOBClassification(entity);
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
                LOBClassification entity = BusinessLayer.GetLOBClassification(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;

                BusinessLayer.UpdateLOBClassification(entity);
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