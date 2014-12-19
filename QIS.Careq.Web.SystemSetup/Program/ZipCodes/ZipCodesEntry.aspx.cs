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
    public partial class ZipCodesEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.ZIPCODES;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                ZipCodes entity = BusinessLayer.GetZipCodes(Convert.ToInt32(ID));
                SetControlProperties();
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtZipCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(String.Format("ParentID = '{0}' AND IsDeleted = 0", Constant.StandardCode.PROVINCE));
            Methods.SetComboBoxField<StandardCode>(cboGCProvince, lst,"StandardCodeName","StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtZipCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStreetName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtDistrict, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtCounty, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtCity, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboGCProvince, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtLatitude, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtLongitude, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(ZipCodes entity)
        {
            txtZipCode.Text = entity.ZipCode;
            txtStreetName.Text = entity.StreetName;
            txtDistrict.Text = entity.District;
            txtCounty.Text = entity.County;
            txtCity.Text = entity.City;
            cboGCProvince.Value = entity.GCProvince;
            txtLatitude.Text = entity.Latitude.ToString();
            txtLongitude.Text = entity.Longitude.ToString();
        }

        private void ControlToEntity(ZipCodes entity)
        {
            entity.ZipCode = txtZipCode.Text;
            entity.StreetName = txtStreetName.Text;
            entity.District = txtDistrict.Text;
            entity.County = txtCounty.Text;
            entity.City = txtCity.Text;
            entity.GCProvince = cboGCProvince.Value.ToString();
            entity.Latitude = string.IsNullOrWhiteSpace(txtLatitude.Text) ? 0 : Convert.ToDecimal(txtLatitude.Text);
            entity.Longitude = string.IsNullOrWhiteSpace(txtLongitude.Text) ? 0 : Convert.ToDecimal(txtLongitude.Text);
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("ZipCode = '{0}'", txtZipCode.Text);
            List<ZipCodes> lst = BusinessLayer.GetZipCodesList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " ZIP Code " + txtZipCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("ZipCode = '{0}' AND ID != {1}", txtZipCode.Text, hdnID.Value);
            List<ZipCodes> lst = BusinessLayer.GetZipCodesList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " ZIP Code " + txtZipCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                ZipCodes entity = new ZipCodes();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertZipCodes(entity);
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
                ZipCodes entity = BusinessLayer.GetZipCodes(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateZipCodes(entity);
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