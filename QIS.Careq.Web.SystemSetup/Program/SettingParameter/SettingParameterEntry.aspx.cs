using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;
using System.Reflection;
using System.Collections;
using DevExpress.Web.ASPxEditors;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class SettingParameterEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.SETTING_PARAMETER;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                SettingParameter entity = BusinessLayer.GetSettingParameter(ID);

                if (entity.GCParameterValueType == "X103^002")
                {
                    string methodName = string.Format("Get{0}List", entity.TableName);
                    MethodInfo method = typeof(BusinessLayer).GetMethod(methodName, new[] { typeof(string) });
                    object obj = method.Invoke(null, new string[] { entity.FilterExpression });
                    IList list = (IList)obj;

                    cboParameterValue.DataSource = list;
                    cboParameterValue.TextField = entity.TextField;
                    cboParameterValue.ValueField = entity.ValueField;
                    cboParameterValue.CallbackPageSize = 50;
                    cboParameterValue.EnableCallbackMode = false;
                    cboParameterValue.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cboParameterValue.DropDownStyle = DropDownStyle.DropDownList;
                    cboParameterValue.DataBind();
                }
                EntityToControl(entity);
            }
            txtParameterName.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtParameterCode, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtParameterName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboParameterValue, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtParameterValue, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtNotes, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(SettingParameter entity)
        {
            txtParameterCode.Text = entity.ParameterCode;
            txtParameterName.Text = entity.ParameterName;
            if (entity.GCParameterValueType == "X103^001")
            {
                trCboParameterValue.Style.Add("display", "none");
                trTxtParameterValue.Style.Remove("display");
                txtParameterValue.Text = entity.ParameterValue;
            }
            else
            {
                trCboParameterValue.Style.Remove("display");
                trTxtParameterValue.Style.Add("display", "none");
                cboParameterValue.Text = entity.ParameterValue;
            }
            txtNotes.Text = entity.Notes;
        }

        private void ControlToEntity(SettingParameter entity)
        {
            entity.ParameterCode = txtParameterCode.Text;
            entity.ParameterName = txtParameterName.Text;
            if (entity.GCParameterValueType == "X103^001")
                entity.ParameterValue = txtParameterValue.Text;
            else
                entity.ParameterValue = cboParameterValue.Value.ToString();
            entity.Notes = txtNotes.Text;
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                SettingParameter entity = BusinessLayer.GetSettingParameter(hdnID.Value);
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateSettingParameter(entity);
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