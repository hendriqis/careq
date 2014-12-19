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
    public partial class RegionEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.REGION;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                Region entity = BusinessLayer.GetRegion(Convert.ToInt32(ID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtRegionCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtRegionCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtRegionName, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(Region entity)
        {
            txtRegionCode.Text = entity.RegionCode;
            txtRegionName.Text = entity.RegionName;
        }

        private void ControlToEntity(Region entity)
        {
            entity.RegionCode = txtRegionCode.Text;
            entity.RegionName = txtRegionName.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("RegionCode = '{0}'", txtRegionCode.Text);
            List<Region> lst = BusinessLayer.GetRegionList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Region With Code " + txtRegionCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("RegionCode = '{0}' AND RegionID != {1}", txtRegionCode.Text, hdnID.Value);
            List<Region> lst = BusinessLayer.GetRegionList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Region With Code " + txtRegionCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                Region entity = new Region();
                ControlToEntity(entity);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertRegion(entity);
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
                Region entity = BusinessLayer.GetRegion(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateRegion(entity);
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