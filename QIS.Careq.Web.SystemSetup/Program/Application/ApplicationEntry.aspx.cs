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
    public partial class ApplicationEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.APPLICATION;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                Application entity = BusinessLayer.GetApplication(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtApplicationCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtApplicationCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCompanyName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtProductKey, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(Application entity)
        {
            txtApplicationCode.Text = entity.ApplicationCode;
            txtCompanyName.Text = entity.CompanyName;
            txtProductKey.Text = entity.ProductKey;
        }

        private void ControlToEntity(Application entity)
        {
            entity.ApplicationCode = txtApplicationCode.Text;
            entity.CompanyName = txtCompanyName.Text;
            entity.ProductKey = txtProductKey.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("ApplicationCode = '{0}'", txtApplicationCode.Text);
            List<Application> lst = BusinessLayer.GetApplicationList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Application with Code " + txtApplicationCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            Int32 menuID = Convert.ToInt32(hdnID.Value);
            string FilterExpression = string.Format("ApplicationCode = '{0}' AND ApplicationID != {1}", txtApplicationCode.Text, menuID);
            List<Application> lst = BusinessLayer.GetApplicationList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Application with Code " + txtApplicationCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                Application entity = new Application();
                ControlToEntity(entity);
                BusinessLayer.InsertApplication(entity);
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
                Application entity = BusinessLayer.GetApplication(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                BusinessLayer.UpdateApplication(entity);
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