using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Data.Service;
using QIS.Careq.Web.Common;
using QIS.Data.Core.Dal;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class MemberIdentityCtl : BaseEntryPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            if (param != "")
            {
                IsAdd = false;
                String ID = param;
                hdnID.Value = ID;
                SetControlProperties();
                vMember entity = BusinessLayer.GetvMemberList(string.Format("MemberID = {0}", hdnID.Value))[0];
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
                SetControlProperties();
            }
            cboSalutation.Focus();
        }

        private void SetControlProperties()
        {
            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}','{2}','{3}','{4}') AND IsDeleted = 0", Constant.StandardCode.TITLE, Constant.StandardCode.SALUTATION, Constant.StandardCode.SUFFIX, Constant.StandardCode.MEMBER_STATUS, Constant.StandardCode.OCCUPATION_LEVEL));
            lstSc.Insert(0, new StandardCode { StandardCodeID = "", StandardCodeName = "" });
            Methods.SetComboBoxField<StandardCode>(cboSalutation, lstSc.Where(sc => sc.ParentID == Constant.StandardCode.SALUTATION || sc.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboTitle, lstSc.Where(sc => sc.ParentID == Constant.StandardCode.TITLE || sc.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboSuffix, lstSc.Where(sc => sc.ParentID == Constant.StandardCode.SUFFIX || sc.StandardCodeID == "").ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboMemberStatus, lstSc.Where(sc => sc.ParentID == Constant.StandardCode.MEMBER_STATUS).ToList(), "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboOccupationLevel, lstSc.Where(sc => sc.ParentID == Constant.StandardCode.OCCUPATION_LEVEL).ToList(), "StandardCodeName", "StandardCodeID");
            cboMemberStatus.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            #region Member Data
            SetControlEntrySetting(txtMemberCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(cboSalutation, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(cboTitle, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtFirstName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMiddleName, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtLastName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtPreferredName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboSuffix, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtBirthPlace, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtDOB, new ControlEntrySetting(true, true, false));
            #endregion

            #region Address
            SetControlEntrySetting(txtAddress, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtCounty, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtDistrict, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtCity, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtProvinceCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtProvinceName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(hdnZipCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtZipCode, new ControlEntrySetting(true, true, false));
            #endregion

            #region Contact
            SetControlEntrySetting(txtTelephoneNo, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtMobilePhone1, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtMobilePhone2, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtEmail, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEmail2, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtOfficeExtensionNo, new ControlEntrySetting(true, true, false));
            #endregion

            #region Additonal Information
            SetControlEntrySetting(cboMemberStatus, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtNationalityCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtNationalityName, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(hdnCompanyID, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtCompanyCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCompanyName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtDepartmentCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtDepartmentName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtOccupation, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboOccupationLevel, new ControlEntrySetting(true, true, true));
            //SetControlEntrySetting(txtRatingLevel, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtVATRegistrationNo, new ControlEntrySetting(true, true, false));
            #endregion

            #region Training Information
            SetControlEntrySetting(txtLastEventDate, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtLastEvent, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtLastCompany, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtNumberOfTraining, new ControlEntrySetting(false, false, false));
            #endregion

            #region Member Status
            SetControlEntrySetting(chkIsCompanyContactPerson, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsCSClub, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsHRDClub, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(chkIsISOClub, new ControlEntrySetting(true, true, false));
            #endregion

            #region Other Information
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
            #endregion
        }

        private void EntityToControl(vMember entity)
        {
            #region Member Data
            txtMemberCode.Text = entity.MemberCode;
            cboSalutation.Value = entity.GCSalutation;
            cboTitle.Value = entity.GCTitle;
            txtFirstName.Text = entity.FirstName;
            txtMiddleName.Text = entity.MiddleName;
            txtLastName.Text = entity.LastName;
            txtPreferredName.Text = entity.PreferredName;
            cboSuffix.Value = entity.GCSuffix;
            txtBirthPlace.Text = entity.CityOfBirth;
            txtDOB.Text = entity.DateOfBirth.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            #endregion

            #region Address
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
            #endregion

            #region Contact
            txtTelephoneNo.Text = entity.PhoneNo1;
            txtMobilePhone1.Text = entity.MobilePhoneNo1;
            txtMobilePhone2.Text = entity.MobilePhoneNo2;
            txtEmail.Text = entity.EmailAddress1;
            txtEmail2.Text = entity.EmailAddress2;
            txtOfficeExtensionNo.Text = entity.OfficeExtensionNo;
            #endregion

            #region Additonal Information
            cboMemberStatus.Text = entity.GCMemberStatus;
            if (entity.GCNationality != "")
                txtNationalityCode.Text = entity.GCNationality.Split('^')[1];
            else
                txtNationalityCode.Text = "";
            txtNationalityName.Text = entity.Nationality;
            txtDepartmentCode.Text = entity.GCDepartment.Split('^')[1];
            txtDepartmentName.Text = entity.Department;
            hdnCompanyID.Value = entity.CompanyID.ToString();
            txtCompanyCode.Text = entity.CompanyCode;
            txtCompanyName.Text = entity.CompanyName;
            txtOccupation.Text = entity.Occupation;
            cboOccupationLevel.Value = entity.GCOccupationLevel;
            hdnRatingLevel.Value = entity.RatingLevel.ToString();
            txtVATRegistrationNo.Text = entity.VATRegistrationNo;
            #endregion

            #region Training Information
            if (entity.LastEventDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT) != Constant.ConstantDate.DEFAULT_NULL)
                txtLastEventDate.Text = entity.LastEventDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtNumberOfTraining.Text = entity.NumberOfTraining.ToString();
            txtLastCompany.Text = entity.LastCompanyName;
            txtLastEvent.Text = entity.LastEventName;
            #endregion

            #region Member Status
            chkIsCompanyContactPerson.Checked = entity.IsCompanyContactPerson;
            chkIsCSClub.Checked = entity.IsCSClub;
            chkIsHRDClub.Checked = entity.IsHRDClub;
            chkIsISOClub.Checked = entity.IsISOClub;
            #endregion

            #region Other Information
            txtRemarks.Text = entity.Remarks;
            #endregion

        }

        private void ControlToEntity(Member entity, Address entityAddress)
        {
            #region Member Data
            if (cboSalutation.Value != null && cboSalutation.Value.ToString() != "")
                entity.GCSalutation = cboSalutation.Value.ToString();
            else
                entity.GCSalutation = null;
            if (cboTitle.Value != null && cboTitle.Value.ToString() != "")
                entity.GCTitle = cboTitle.Value.ToString();
            else
                entity.GCTitle = null;
            entity.FirstName = txtFirstName.Text.ToUpper();
            entity.MiddleName = txtMiddleName.Text.ToUpper();
            entity.LastName = txtLastName.Text.ToUpper();
            entity.PreferredName = txtPreferredName.Text;
            if (cboSuffix.Value != null && cboSuffix.Value.ToString() != "")
                entity.GCSuffix = cboSuffix.Value.ToString();
            else
                entity.GCSuffix = null;
            entity.CityOfBirth = txtBirthPlace.Text;
            entity.DateOfBirth = Helper.GetDatePickerValue(txtDOB);
            #endregion

            #region Address
            entityAddress.StreetName = txtAddress.Text;
            entityAddress.County = txtCounty.Text; // Desa
            entityAddress.District = txtDistrict.Text; //Kabupaten
            entityAddress.City = txtCity.Text;
            entityAddress.GCProvince = txtProvinceCode.Text == "" ? null : string.Format("{0}^{1}", Constant.StandardCode.PROVINCE, txtProvinceCode.Text);
            if (hdnZipCode.Value == "" || hdnZipCode.Value == "0")
                entityAddress.ZipCodeID = null;
            else
                entityAddress.ZipCodeID = Convert.ToInt32(hdnZipCode.Value);
            #endregion

            #region Contact
            entityAddress.PhoneNo1 = txtTelephoneNo.Text;
            entity.MobilePhoneNo1 = txtMobilePhone1.Text;
            entity.MobilePhoneNo2 = txtMobilePhone2.Text;
            entity.EmailAddress1 = txtEmail.Text;
            entity.EmailAddress2 = txtEmail2.Text;
            entity.OfficeExtensionNo = txtOfficeExtensionNo.Text;
            #endregion

            #region Additonal Information
            entity.GCMemberStatus = cboMemberStatus.Value.ToString();
            entity.GCNationality = txtNationalityCode.Text == "" ? null : string.Format("{0}^{1}", Constant.StandardCode.NATIONALITY, txtNationalityCode.Text);
            entity.GCDepartment = string.Format("{0}^{1}", Constant.StandardCode.COMPANY_DEPARTMENT, txtDepartmentCode.Text);
            entity.CompanyID = Convert.ToInt32(hdnCompanyID.Value);
            entity.Occupation = txtOccupation.Text;
            entity.GCOccupationLevel = cboOccupationLevel.Value.ToString();
            string ratingLevel = hdnRatingLevel.Value;
            if (ratingLevel != "")
                entity.RatingLevel = Convert.ToInt16(ratingLevel);
            else
                entity.RatingLevel = 0;
            #endregion

            #region Training Information
            #endregion

            #region Member Status
            entity.IsCompanyContactPerson = chkIsCompanyContactPerson.Checked;
            entity.IsCSClub = chkIsCSClub.Checked;
            entity.IsHRDClub = chkIsHRDClub.Checked;
            entity.IsISOClub = chkIsISOClub.Checked;
            #endregion

            #region Other Information
            entity.Remarks = txtRemarks.Text;
            #endregion

        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            MemberDao entityDao = new MemberDao(ctx);
            AddressDao entityAddressDao = new AddressDao(ctx);
            try
            {
                Member entity = new Member();
                Address entityAddress = new Address();
                ControlToEntity(entity, entityAddress);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.NumberOfTraining = 0;

                entityAddressDao.Insert(entityAddress);
                entity.AddressID = BusinessLayer.GetAddressMaxID(ctx);

                entityDao.Insert(entity);

                entity.MemberID = BusinessLayer.GetMemberMaxID(ctx);
                entity.MemberCode = Helper.GenerateCode(Constant.FormatCode.MEMBER, entity.MemberID);
                entityDao.Update(entity);
                retval = entity.MemberID.ToString();
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

        protected override bool OnSaveEditRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            MemberDao entityDao = new MemberDao(ctx);
            AddressDao entityAddressDao = new AddressDao(ctx);
            try
            {
                Member entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
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