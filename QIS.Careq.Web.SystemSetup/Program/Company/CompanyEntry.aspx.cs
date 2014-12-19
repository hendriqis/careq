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
    public partial class CompanyEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.COMPANY;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                hdnID.Value = Request.QueryString["id"];
                vCompany entity = BusinessLayer.GetvCompanyList(String.Format("CompanyID = {0}", hdnID.Value))[0];
                vAddress entityAddress = BusinessLayer.GetvAddressList(string.Format("AddressID = {0}", entity.AddressID))[0];
                SetControlProperties();
                EntityToControl(entity, entityAddress);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;
            }
            txtCompanyCode.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}') AND IsDeleted = 0", Constant.StandardCode.COMPANY_TYPE, Constant.StandardCode.COUNTY_OF_ORIGIN));
            Methods.SetComboBoxField<StandardCode>(cboGCCompanyType, lstStandardCode.Where(sc => sc.ParentID == Constant.StandardCode.COMPANY_TYPE).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboGCCountryOfOrigin, lstStandardCode.Where(sc => sc.ParentID == Constant.StandardCode.COUNTY_OF_ORIGIN).ToList(), "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtCompanyCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtCompanyName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtShortName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(hdnLOBClassID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtLOBClassCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtLOBClassName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(cboGCCompanyType, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtWebsiteUrl, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboGCCountryOfOrigin, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtVATRegistrationNo, new ControlEntrySetting(true, true, false));
            //SetControlEntrySetting(txtRatingLevel, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(hdnHoldingCompanyID, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtHoldingCompanyCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtHoldingCompanyName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(hdnContactPersonID, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtContactPersonName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnRegionID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtRegionCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRegionName, new ControlEntrySetting(false, false, false));
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
            SetControlEntrySetting(txtEmail, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(chkIsGoPublicCompany, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsTaxable, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(lblContactPerson, new ControlEntrySetting(false, true));

            SetControlEntrySetting(txtCreatedBy, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtCreatedDate, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtLastUpdatedBy, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtLastUpdatedDate, new ControlEntrySetting(false, false, false));
        }

        private void EntityToControl(vCompany entity, vAddress entityAddress)
        {
            txtCompanyCode.Text = entity.CompanyCode;
            txtCompanyName.Text = entity.CompanyName;
            txtShortName.Text = entity.ShortName;
            hdnLOBClassID.Value = entity.LOBClassID.ToString();
            txtLOBClassCode.Text = entity.LOBClassCode;
            txtLOBClassName.Text = entity.LOBClassName;
            cboGCCompanyType.Value = entity.GCCompanyType;
            txtWebsiteUrl.Text = entity.WebsiteUrl;
            cboGCCountryOfOrigin.Value = entity.GCCountryOfOrigin;
            txtVATRegistrationNo.Text = entity.VATRegistrationNo;
            hdnRatingLevel.Value = entity.RatingLevel.ToString();
            hdnContactPersonID.Value = entity.ContactPersonID.ToString();
            txtContactPersonName.Text = entity.ContactPersonName;
            hdnHoldingCompanyID.Value = entity.HoldingCompanyID.ToString();
            txtHoldingCompanyCode.Text = entity.HoldingCompanyCode;
            txtHoldingCompanyName.Text = entity.HoldingCompanyName;

            hdnRegionID.Value = entity.RegionID.ToString();
            txtRegionCode.Text = entity.RegionCode;
            txtRegionName.Text = entity.RegionName;
            txtAddress.Text = entityAddress.StreetName;
            txtCounty.Text = entityAddress.County; // Desa
            txtDistrict.Text = entityAddress.District; //Kabupaten
            txtCity.Text = entityAddress.City;
            if (entityAddress.GCProvince != "")
                txtProvinceCode.Text = entityAddress.GCProvince.Split('^')[1];
            else
                txtProvinceCode.Text = "";
            txtProvinceName.Text = entityAddress.Province;
            hdnZipCode.Value = entityAddress.ZipCodeID.ToString();
            txtZipCode.Text = entityAddress.ZipCode;
            txtTelephoneNo.Text = entityAddress.PhoneNo1;
            txtFaxNo.Text = entityAddress.FaxNo1;
            txtTelephoneNo2.Text = entityAddress.PhoneNo2;
            txtFaxNo2.Text = entityAddress.FaxNo2;
            txtEmail.Text = entity.EmailAddress;

            chkIsGoPublicCompany.Checked = entity.IsGoPublicCompany;
            chkIsTaxable.Checked = entity.IsTaxable;

            txtRemarks.Text = entity.Remarks;

            txtCreatedBy.Text = entity.CreatedByUserName;
            txtCreatedDate.Text = entity.CreatedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtLastUpdatedBy.Text = entity.LastUpdatedByUserName;
            txtLastUpdatedDate.Text = entity.LastUpdatedDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
        }

        private void ControlToEntity(Company entity, Address entityAddress)
        {
            entity.CompanyCode = txtCompanyCode.Text.ToUpper();
            entity.CompanyName = txtCompanyName.Text.ToUpper();
            entity.ShortName = txtShortName.Text.ToUpper();
            entity.LOBClassID = Convert.ToInt32(hdnLOBClassID.Value);
            entity.GCCompanyType = cboGCCompanyType.Value.ToString();
            entity.WebsiteUrl = txtWebsiteUrl.Text;
            if (cboGCCountryOfOrigin.Value != null && cboGCCountryOfOrigin.Value.ToString() != "")
                entity.GCCountryOfOrigin = cboGCCountryOfOrigin.Value.ToString();
            else
                entity.GCCountryOfOrigin = null;
            entity.VATRegistrationNo = txtVATRegistrationNo.Text;

            string ratingLevel = hdnRatingLevel.Value;
            if (ratingLevel != "")
                entity.RatingLevel = Convert.ToInt16(ratingLevel);
            else
                entity.RatingLevel = 0;
            if (hdnHoldingCompanyID.Value != "" && hdnHoldingCompanyID.Value != "0")
                entity.HoldingCompanyID = Convert.ToInt32(hdnHoldingCompanyID.Value);
            else
                entity.HoldingCompanyID = null;
            if (hdnContactPersonID.Value != "" && hdnContactPersonID.Value != "0")
                entity.ContactPersonID = Convert.ToInt32(hdnContactPersonID.Value);
            else
                entity.ContactPersonID = null;

            if (hdnRegionID.Value != "" && hdnRegionID.Value != "0")
                entity.RegionID = Convert.ToInt32(hdnRegionID.Value);
            else
                entity.RegionID = null;
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
            entity.EmailAddress = txtEmail.Text;

            entity.IsGoPublicCompany = chkIsGoPublicCompany.Checked;
            entity.IsTaxable = chkIsTaxable.Checked;

            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CompanyDao entityDao = new CompanyDao(ctx);
            AddressDao entityAddressDao = new AddressDao(ctx);
            try
            {
                Company entity = new Company();
                Address entityAddress = new Address();
                ControlToEntity(entity, entityAddress);

                entityAddressDao.Insert(entityAddress);
                entity.AddressID = BusinessLayer.GetAddressMaxID(ctx);
                entity.CreatedBy = AppSession.UserLogin.UserID;

                entityDao.Insert(entity);
                entity.CompanyID = BusinessLayer.GetCompanyMaxID(ctx);
                entity.CompanyCode = Helper.GenerateCode(Constant.FormatCode.COMPANY, entity.CompanyID);
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

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            CompanyDao entityDao = new CompanyDao(ctx);
            AddressDao entityAddressDao = new AddressDao(ctx);
            try
            {
                Company entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
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