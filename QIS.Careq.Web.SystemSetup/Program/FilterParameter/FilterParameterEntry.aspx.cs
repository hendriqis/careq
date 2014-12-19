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
    public partial class FilterParameterEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.FILTER_PARAMETER;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                SetControlProperties();
                FilterParameter entity = BusinessLayer.GetFilterParameter(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtFilterParameterCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0", Constant.StandardCode.FILTER_PARAMETER_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboFilterParameterType, lst, "StandardCodeName", "StandardCodeID");
            cboFilterParameterType.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtFilterParameterCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFilterParameterName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFilterParameterCaption, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboFilterParameterType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFieldName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtMethodName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtFilterExpression, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtValueFieldName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTextFieldName, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(FilterParameter entity)
        {
            txtFilterParameterCode.Text = entity.FilterParameterCode;
            txtFilterParameterName.Text = entity.FilterParameterName;
            txtFilterParameterCaption.Text = entity.FilterParameterCaption;
            cboFilterParameterType.Value = entity.GCFilterParameterType;
            txtFieldName.Text = entity.FieldName;
            txtMethodName.Text = entity.MethodName;
            txtFilterExpression.Text = entity.FilterExpression;
            txtValueFieldName.Text = entity.ValueFieldName;
            txtTextFieldName.Text = entity.TextFieldName;
        }

        private void ControlToEntity(FilterParameter entity)
        {
            entity.FilterParameterCode = txtFilterParameterCode.Text;
            entity.FilterParameterName = txtFilterParameterName.Text;
            entity.FilterParameterCaption = txtFilterParameterCaption.Text;
            entity.GCFilterParameterType = cboFilterParameterType.Value.ToString();
            entity.FieldName = txtFieldName.Text;
            entity.MethodName = txtMethodName.Text;
            entity.FilterExpression = txtFilterExpression.Text;
            entity.ValueFieldName = txtValueFieldName.Text;
            entity.TextFieldName = txtTextFieldName.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("FilterParameterCode = '{0}'", txtFilterParameterCode.Text);
            List<FilterParameter> lst = BusinessLayer.GetFilterParameterList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Filter Parameter With Code " + txtFilterParameterCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("FilterParameterCode = '{0}' AND FilterParameterID != {1}", txtFilterParameterCode.Text, hdnID.Value);
            List<FilterParameter> lst = BusinessLayer.GetFilterParameterList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Filter Parameter With Code " + txtFilterParameterCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                FilterParameter entity = new FilterParameter();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertFilterParameter(entity);
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
                FilterParameter entity = BusinessLayer.GetFilterParameter(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateFilterParameter(entity);
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