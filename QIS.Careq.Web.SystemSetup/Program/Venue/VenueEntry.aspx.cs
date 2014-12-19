using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;
using QIS.Data.Core.Dal;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class VenueEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.VENUE;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                vVenue entity = BusinessLayer.GetvVenueList(string.Format("VenueID = {0}", ID))[0];
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtVenueCode.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtVenueCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtVenueName, new ControlEntrySetting(true, true, true));

            SetControlEntrySetting(txtAddress, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCounty, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtDistrict, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtCity, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtProvinceCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtProvinceName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(hdnZipCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtZipCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtTelephoneNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtTelephoneNo2, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtFaxNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtFaxNo2, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(vVenue entity)
        {
            txtVenueCode.Text = entity.VenueCode;
            txtVenueName.Text = entity.VenueName;

            txtAddress.Text = entity.StreetName;
            txtCounty.Text = entity.County; // Desa
            txtDistrict.Text = entity.District; //Kabupaten
            txtCity.Text = entity.City;
            if (entity.GCProvince != "")
                txtProvinceCode.Text = entity.GCProvince.Split('^')[1];
            else
                txtProvinceCode.Text = "";
            txtProvinceName.Text = entity.Province;
            hdnZipCode.Value = entity.ZipCodeID.ToString();
            txtZipCode.Text = entity.ZipCode;
            txtTelephoneNo.Text = entity.PhoneNo1;
            txtFaxNo.Text = entity.FaxNo1;
            txtTelephoneNo2.Text = entity.PhoneNo2;
            txtFaxNo2.Text = entity.FaxNo2;

            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(Venue entity, Address entityAddress)
        {
            entity.VenueCode = txtVenueCode.Text;
            entity.VenueName = txtVenueName.Text;

            entityAddress.StreetName = txtAddress.Text;
            entityAddress.County = txtCounty.Text; // Desa
            entityAddress.District = txtDistrict.Text; //Kabupaten
            entityAddress.City = txtCity.Text;
            entityAddress.GCProvince = txtProvinceCode.Text == "" ? null : string.Format("{0}^{1}", Constant.StandardCode.PROVINCE, txtProvinceCode.Text);
            if (hdnZipCode.Value == "" || hdnZipCode.Value == "0")
                entityAddress.ZipCodeID = null;
            else
                entityAddress.ZipCodeID = Convert.ToInt32(hdnZipCode.Value);
            entityAddress.PhoneNo1 = txtTelephoneNo.Text;
            entityAddress.PhoneNo2 = txtTelephoneNo2.Text;
            entityAddress.FaxNo1 = txtFaxNo.Text;
            entityAddress.FaxNo2 = txtFaxNo2.Text;

            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("VenueCode = '{0}'", txtVenueCode.Text);
            List<Venue> lst = BusinessLayer.GetVenueList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Venue With Code " + txtVenueCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("VenueCode = '{0}' AND VenueID != {1}", txtVenueCode.Text, hdnID.Value);
            List<Venue> lst = BusinessLayer.GetVenueList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Venue With Code " + txtVenueCode.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            VenueDao entityDao = new VenueDao(ctx);
            AddressDao entityAddressDao = new AddressDao(ctx);
            try
            {
                Venue entity = new Venue();
                Address entityAddress = new Address();
                ControlToEntity(entity, entityAddress);

                entityAddressDao.Insert(entityAddress);
                entity.AddressID = BusinessLayer.GetAddressMaxID(ctx);
                entity.CreatedBy = AppSession.UserLogin.UserID;

                entityDao.Insert(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            VenueDao entityDao = new VenueDao(ctx);
            AddressDao entityAddressDao = new AddressDao(ctx);
            try
            {
                Venue entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                Address entityAddress = entityAddressDao.Get((int)entity.AddressID);
                ControlToEntity(entity, entityAddress);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;

                entityAddressDao.Update(entityAddress);
                entityDao.Update(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                result = false;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
    }
}