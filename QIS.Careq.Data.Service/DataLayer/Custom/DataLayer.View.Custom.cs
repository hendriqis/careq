using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using QIS.Careq.Data.Service.Helper;

namespace QIS.Careq.Data.Service
{
    #region vCompany
    public partial class vCompany
    {
        public String cfCompanyType
        {
            get 
            {
                if (_CountryOfOrigin == "")
                    return _CompanyType;
                return string.Format("{0} ({1})", _CompanyType, _CountryOfOrigin);
            }
        }

        public String Address
        {
            get
            {
                StringBuilder result = new StringBuilder();
                if (_StreetName != "")
                    result.Append(_StreetName).Append(" ");
                if (_County != "")
                    result.Append(_County).Append(" ");
                if (_District != "")
                    result.Append(_District).Append(" ");
                if (_City != "")
                    result.Append(_City).Append(" ");
                if (_ZipCode != "")
                    result.Append(_ZipCode).Append(" ");
                if (_Province != "")
                    result.Append(_Province).Append(" ");
                return result.ToString();
            }
        }
        public String Address1
        {
            get
            {
                StringBuilder sbResult = new StringBuilder();
                sbResult.Append(_StreetName);
                if (_County != "")
                    sbResult.Append(" ").Append(_County);
                if (_District != "")
                    sbResult.Append(" ").Append(_District);
                return sbResult.ToString();
            }
        }
        public String CompanyAddress
        {
            get
            {
                string county = String.IsNullOrEmpty(_County) ? "" : "," + _County;
                string district = String.IsNullOrEmpty(_District) ? "" : "," + _District;
                string city = String.IsNullOrEmpty(_City) ? "" : "," + _City;
                string province = String.IsNullOrEmpty(_Province) ? "" : "," + _Province;
                string zipCode = String.IsNullOrEmpty(_ZipCode) ? "" : "," + _ZipCode;
                return (string.Format("{0} {1} {2} {3} {4} {5}", _StreetName, county, district, city, province, zipCode));
            }
        }
        public String cfPhoneNo
        {
            get
            {
                StringBuilder result = new StringBuilder();

                if (_PhoneNo1 != "")
                    result.Append(_PhoneNo1);
                if (_PhoneNo2 != "")
                {
                    if (result.ToString() != "")
                        result.Append(" / ");
                    result.Append(_PhoneNo2);
                }
                return result.ToString();
            }
        }
        public String cfFaxNo
        {
            get
            {
                StringBuilder result = new StringBuilder();

                if (_FaxNo1 != "")
                    result.Append(_FaxNo1);
                if (_FaxNo2 != "")
                {
                    if (result.ToString() != "")
                        result.Append(" / ");
                    result.Append(_FaxNo2);
                }
                return result.ToString();
            }
        }
        public String cfHoldingCompanyName
        {
            get
            {
                if (_HoldingCompanyID > 0)
                    return String.Format("Group : {0}", _HoldingCompanyName);
                return "";
            }
        }
        public String ContactPersonName
        {
            get
            {
                StringBuilder result = new StringBuilder();
                result.Append(_LastName);
                if (_LastName != "" && (_MiddleName != "" || _FirstName != ""))
                    result.Append(", ");
                if (_MiddleName != "")
                    result.Append(_MiddleName).Append(" ");
                if (_FirstName != "")
                    result.Append(_FirstName);
                return result.ToString();
            }
        }
    }
    #endregion
    #region vCompanyContactPerson
    public partial class vCompanyContactPerson
    {
        public String ContactPersonName
        {
            get
            {
                StringBuilder result = new StringBuilder();
                result.Append(_LastName);
                if (_LastName != "" && (_MiddleName != "" || _FirstName != ""))
                    result.Append(", ");
                if (_MiddleName != "")
                    result.Append(_MiddleName).Append(" ");
                if (_FirstName != "")
                    result.Append(_FirstName);
                return result.ToString();
            }
        }
    }
    #endregion
    #region vEmployee
    public partial class vEmployee
    {
        public String EmployeeName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _LastName).
                    Replace("MiddleName", _MiddleName).
                    Replace("FirstName", _FirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
    }
    #endregion
    #region vEvent
    public partial class vEvent
    {
        public Int32 StartDateInYear
        {
            get { return Convert.ToInt32(_StartDate.ToString("yyyy")); }
        }
        public Int32 StartDateInMonth
        {
            get { return Convert.ToInt32(_StartDate.ToString("MM")); }
        }
        public Int32 StartDateInDate
        {
            get { return Convert.ToInt32(_StartDate.ToString("dd")); }
        }
        public Double TrainingDuration
        {
            get { return (_EndDate - _StartDate).TotalDays; }
        }

        public String TrainerName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _LastName).
                    Replace("MiddleName", _MiddleName).
                    Replace("FirstName", _FirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
        public String StartDateInString
        {
            get { return _StartDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String EndDateInString
        {
            get { return _EndDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String Address
        {
            get
            {
                StringBuilder result = new StringBuilder();
                if (_StreetName != "")
                    result.Append(_StreetName).Append(" ");
                if (_County != "")
                    result.Append(_County).Append(" ");
                if (_District != "")
                    result.Append(_District).Append(" ");
                if (_City != "")
                    result.Append(_City).Append(" ");
                if (_Province != "")
                    result.Append(_Province).Append(" ");
                return result.ToString();
            }
        }
        public String cfEventDate
        {
            get
            {
                string eventDate = "";
                if (DateTime.Compare(_StartDate, _EndDate) == 0)
                    eventDate = _StartDate.ToString("MMMM dd, yyyy");
                else if (_StartDate.Month == _EndDate.Month)
                    eventDate = string.Format("{0} {1}-{2}, {3}", _StartDate.ToString("MMMM"), _StartDate.Day, _EndDate.Day, _StartDate.Year);
                else
                    eventDate = string.Format("{0} - {1}", _StartDate.ToString("MMMM dd, yyyy"), _EndDate.ToString("MMMM dd, yyyy"));
                StringBuilder result = new StringBuilder().Append(eventDate);
                return result.ToString();
            }
        }
    }
    #endregion
    #region vEventCompanyDt
    public partial class vEventCompanyDt
    {
        public String PICMemberName
        {
            get
            {
                StringBuilder result = new StringBuilder();
                result.Append(_LastName);
                if (_LastName != "" && (_MiddleName != "" || _FirstName != ""))
                    result.Append(", ");
                if (_MiddleName != "")
                    result.Append(_MiddleName).Append(" ");
                if (_FirstName != "")
                    result.Append(_FirstName);
                return result.ToString();
            }
        }
        public String PaymentDateInString
        {
            get { return _PaymentDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String PaymentDateInDatePickerFormat
        {
            get { return _PaymentDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }
        public String cfPrice
        {
            get { if (_Price != 0) return _Price.ToString("N"); return ""; }
        }
        public String cfDiscountAmount
        {
            get { if (_Price != 0) return _DiscountAmount.ToString("N"); return ""; }
        }
        public String TrainerName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _TrainerLastName).
                    Replace("MiddleName", _TrainerMiddleName).
                    Replace("FirstName", _TrainerFirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
        public String StartDateInString
        {
            get { return _StartDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String EndDateInString
        {
            get { return _EndDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String Address
        {
            get
            {
                StringBuilder result = new StringBuilder();
                if (_StreetName != "")
                    result.Append(_StreetName).Append(" ");
                if (_County != "")
                    result.Append(_County).Append(" ");
                if (_District != "")
                    result.Append(_District).Append(" ");
                if (_City != "")
                    result.Append(_City).Append(" ");
                if (_Province != "")
                    result.Append(_Province).Append(" ");
                return result.ToString();
            }
        }
    }
    #endregion
    #region vEventInvitation
    public partial class vEventInvitation
    {
        public String MemberName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _LastName).
                    Replace("MiddleName", _MiddleName).
                    Replace("FirstName", _FirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }

        public String ConfirmedDateInString
        {
            get
            {
                if (_ConfirmedDate.ToString("dd-MM-yyyy") == Constant.ConstantDate.DEFAULT_NULL)
                    return "";
                return _ConfirmedDate.ToString("dd MMMM yyyy");
            }
        }
    }
    #endregion
    #region vEventRegistration
    public partial class vEventRegistration
    {
        public String MemberName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _LastName).
                    Replace("MiddleName", _MiddleName).
                    Replace("FirstName", _FirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
        public String EventDateInString
        {
            get
            {
                return _StartDate.ToString("dd MMMM yyyy");
            }
        }
        public String cfEmailAddress
        {
            get
            {
                if (_EmailAddress1 != "")
                    return _EmailAddress1;
                if (_EmailAddress2 != "")
                    return _EmailAddress2;
                return "";
            }
        }
        public String cfMobilePhoneNo
        {
            get
            {
                if (_MobilePhoneNo1 != "")
                    return _MobilePhoneNo1;
                if (_MobilePhoneNo2 != "")
                    return _MobilePhoneNo2;
                return "";
            }
        }
        public String cfEventDate
        {
            get
            {
                string eventDate = "";
                if (DateTime.Compare(_StartDate, _EndDate) == 0)
                    eventDate = _StartDate.ToString("MMMM dd, yyyy");
                else if (_StartDate.Month == _EndDate.Month)
                    eventDate = string.Format("{0} {1}-{2}, {3}", _StartDate.ToString("MMMM"), _StartDate.Day, _EndDate.Day, _StartDate.Year);
                else
                    eventDate = string.Format("{0} - {1}", _StartDate.ToString("MMMM dd, yyyy"), _EndDate.ToString("MMMM dd, yyyy"));
                StringBuilder result = new StringBuilder().Append("Held on ").Append(eventDate);
                return result.ToString();
            }
        }
        public String cfEventDateReport
        {
            get
            {
                string eventDate = "";
                if (DateTime.Compare(_StartDate, _EndDate) == 0)
                    eventDate = _StartDate.ToString("MMMM dd, yyyy");
                else if (_StartDate.Month == _EndDate.Month)
                    eventDate = string.Format("{0}-{1} {2} {3}", _StartDate.Day, _EndDate.Day, _StartDate.ToString("MMMM"), _StartDate.Year);
                else
                    eventDate = string.Format("{0} - {1}", _StartDate.ToString("dd MMMM yyyy"), _EndDate.ToString("dd MMMM yyyy"));
                StringBuilder result = new StringBuilder().Append(eventDate);
                return result.ToString();
            }
        }
        public String cfCertificationNo
        {
            get
            {
                return _CertificationNo.Replace("{N}", _CertificatePrintNo.ToString());
            }
        }
        public String TrainerName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _TrainerLastName).
                    Replace("MiddleName", _TrainerMiddleName).
                    Replace("FirstName", _TrainerFirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
        public String StartDateInString
        {
            get { return _StartDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String EndDateInString
        {
            get { return _EndDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String EventAddress
        {
            get
            {
                StringBuilder result = new StringBuilder();
                if (_EventStreetName != "")
                    result.Append(_EventStreetName).Append(" ");
                if (_EventCounty != "")
                    result.Append(_EventCounty).Append(" ");
                if (_EventDistrict != "")
                    result.Append(_EventDistrict).Append(" ");
                if (_EventCity != "")
                    result.Append(_EventCity).Append(" ");
                if (_EventProvince != "")
                    result.Append(_EventProvince).Append(" ");
                return result.ToString();
            }
        }
    }
    #endregion
    #region vEventRegistrationPivot
    public partial class vEventRegistrationPivot
    {
        public String MemberName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _LastName).
                    Replace("MiddleName", _MiddleName).
                    Replace("FirstName", _FirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
    }
    #endregion
    #region vInquiry
    public partial class vInquiry
    {
        public String PIC_CROName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _EmployeeLastName).
                    Replace("MiddleName", _EmployeeMiddleName).
                    Replace("FirstName", _EmployeeFirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
        public string InquiryDateInString
        {
            get { return _InquiryDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String MemberName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _LastName).
                    Replace("MiddleName", _MiddleName).
                    Replace("FirstName", _FirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
        public String TrainerName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _TrainerLastName).
                    Replace("MiddleName", _TrainerMiddleName).
                    Replace("FirstName", _TrainerFirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
    }
    #endregion
    #region vInquiryActivityLog
    public partial class vInquiryActivityLog
    {
        public String CROName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _CROLastName).
                    Replace("MiddleName", _CROMiddleName).
                    Replace("FirstName", _CROFirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
        public String TrainerName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _TrainerLastName).
                    Replace("MiddleName", _TrainerMiddleName).
                    Replace("FirstName", _TrainerFirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
        public string LogDateInString
        {
            get { return _LogDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public string LogDateInDatePicker
        {
            get { return _LogDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }
    }
    #endregion
    #region vLead
    public partial class vLead
    {
        public String PIC_CROName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _EmployeeLastName).
                    Replace("MiddleName", _EmployeeMiddleName).
                    Replace("FirstName", _EmployeeFirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
        public string LeadDateInString 
        {
            get { return _LeadDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public String MemberName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _LastName).
                    Replace("MiddleName", _MiddleName).
                    Replace("FirstName", _FirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
    }
    #endregion
    #region vLeadActivityLog
    public partial class vLeadActivityLog
    {
        public String CROName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _CROLastName).
                    Replace("MiddleName", _CROMiddleName).
                    Replace("FirstName", _CROFirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
        public string LogDateInString
        {
            get { return _LogDate.ToString(Constant.FormatString.DATE_FORMAT); }
        }

        public string LogDateInDatePicker
        {
            get { return _LogDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT); }
        }
    }
    #endregion
    #region vMember
    public partial class vMember
    {
        public String CompanyAddress1
        {
            get
            {
                StringBuilder sbResult = new StringBuilder();
                sbResult.Append(_CompanyStreetName);
                if (_CompanyCounty != "")
                    sbResult.Append(" ").Append(_CompanyCounty);
                if (_CompanyDistrict != "")
                    sbResult.Append(" ").Append(_CompanyDistrict);
                return sbResult.ToString();
            }
        }
        public String MemberName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _LastName).
                    Replace("MiddleName", _MiddleName).
                    Replace("FirstName", _FirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
        public String MemberNameWithTitle
        {
            get
            {
                StringBuilder result = new StringBuilder();
                result = result.Append(_Salutation).Append(" ").Append(_Title).Append(" ").Append(AppConfigManager.QISFullNameFormat).Append(" ").Append(_Suffix).Replace("LastName", _LastName).
                    Replace("MiddleName", _MiddleName).
                    Replace("FirstName", _FirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
        public String MemberNameWithSalutation
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Insert(0, _Salutation).Append(" ").Replace("LastName", _LastName).
                    Replace("MiddleName", _MiddleName).
                    Replace("FirstName", _FirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
        public String cfMobilePhoneNo
        {
            get
            {
                StringBuilder result = new StringBuilder();

                if (_MobilePhoneNo1 != "")
                    result.Append(_MobilePhoneNo1);
                if (_MobilePhoneNo2 != "")
                {
                    if (result.ToString() != "")
                        result.Append(" / ");
                    result.Append(_MobilePhoneNo2);
                }
                return result.ToString();
            }
        }
        public String cfEmailAddress
        {
            get
            {
                StringBuilder result = new StringBuilder();

                if (_EmailAddress1 != "")
                    result.Append(_EmailAddress1);
                if (_EmailAddress2 != "")
                {
                    if (result.ToString() != "")
                        result.Append(" / ");
                    result.Append(_EmailAddress2);
                }
                return result.ToString();
            }
        }
        public String DateOfBirthInString
        {
            get { return _DateOfBirth.ToString(Constant.FormatString.DATE_FORMAT); }
        }
        public int AgeInYear
        {
            get
            {
                return Function.GetPatientAgeInYear(_DateOfBirth, DateTime.Now);
            }
        }
        public int AgeInMonth
        {
            get
            {
                return Function.GetPatientAgeInMonth(_DateOfBirth, DateTime.Now);
            }
        }
        public int AgeInDay
        {
            get
            {
                return Function.GetPatientAgeInDay(_DateOfBirth, DateTime.Now);
            }
        }
        public String CompanyAddress
        {
            get
            {
                StringBuilder result = new StringBuilder();
                if (_CompanyStreetName != "")
                    result.Append(_CompanyStreetName).Append(" ");
                if (_CompanyCounty != "")
                    result.Append(_CompanyCounty).Append(" ");
                if (_CompanyDistrict != "")
                    result.Append(_CompanyDistrict).Append(" ");
                if (_CompanyCity != "")
                    result.Append(_CompanyCity).Append(" ");
                if (_CompanyProvince != "")
                    result.Append(_CompanyProvince).Append(" ");
                if (_CompanyZipCode != "")
                    result.Append(_CompanyZipCode).Append(" ");
                return result.ToString();
            }
        }
    }
    #endregion
    #region vMemberPastTraining
    public partial class vMemberPastTraining
    {
        public String DisplayTrainingInYear
        {
            get
            {
                if (_EventID == null)
                    return _TrainingYear;
                return _StartDate.ToString("yyyy");
            }
        }
        public String DisplayTrainingInMonth
        {
            get
            {
                if (_EventID == null)
                    return _TrainingMonth;
                return _StartDate.ToString("MM");
            }
        }
        public String DisplayTrainingInDate
        {
            get
            {
                if (_EventID == null)
                    return _TrainingDate;
                return _StartDate.ToString("dd");
            }
        }
        public Double DisplayTrainingDuration
        {
            get
            {
                if (_EventID == null)
                    return _TrainingDuration;
                return (_EndDate - _StartDate).TotalDays;
            }
        }

        public String DisplayTrainingDate
        {
            get
            {
                if (_EventID == null)
                {
                    int year = Convert.ToInt32(_TrainingYear);
                    int month = Convert.ToInt32(_TrainingMonth);
                    int day = Convert.ToInt32(_TrainingDate);

                    StringBuilder result = new StringBuilder();
                    if (day > 0)
                        result.Append(day).Append('-');
                    if (month > 0)
                        result.Append(DateTimeFormatInfo.CurrentInfo.GetMonthName(month).Substring(0, 3)).Append('-');
                    if (year > 0)
                        result.Append(year);
                    return result.ToString();
                }
                return _StartDate.ToString(Constant.FormatString.DATE_FORMAT);
            }
        }
        public String DisplayTrainerName
        {
            get
            {
                if (_EventID == null)
                    return _TrainerName;

                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _TrainerLastName).
                    Replace("MiddleName", _TrainerMiddleName).
                    Replace("FirstName", _TrainerFirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
        public String Address
        {
            get
            {
                if (_EventID == null)
                    return _VenueLocation;

                StringBuilder result = new StringBuilder();
                if (_StreetName != "")
                    result.Append(_StreetName).Append(" ");
                if (_County != "")
                    result.Append(_County).Append(" ");
                if (_District != "")
                    result.Append(_District).Append(" ");
                if (_City != "")
                    result.Append(_City).Append(" ");
                if (_Province != "")
                    result.Append(_Province).Append(" ");
                return result.ToString();
            }
        }
    }
    #endregion
    #region vTrainer
    public partial class vTrainer
    {
        public String TrainerName
        {
            get
            {
                StringBuilder result = new StringBuilder(AppConfigManager.QISFullNameFormat);
                result = result.Replace("LastName", _LastName).
                    Replace("MiddleName", _MiddleName).
                    Replace("FirstName", _FirstName).
                    Replace(",  ", "").
                    Replace("  ", " ");
                return result.ToString();
            }
        }
    }
    #endregion
    #region vVenue
    public partial class vVenue
    {
        public String Address
        {
            get
            {
                StringBuilder result = new StringBuilder();
                if (_StreetName != "")
                    result.Append(_StreetName).Append(" ");
                if (_County != "")
                    result.Append(_County).Append(" ");
                if (_District != "")
                    result.Append(_District).Append(" ");
                if (_City != "")
                    result.Append(_City).Append(" ");
                if (_Province != "")
                    result.Append(_Province).Append(" ");
                return result.ToString();
            }
        }
    }
    #endregion
    #region vZipCodes
    public partial class vZipCodes
    {
        public String cfGCProvince
        {
            get
            {
                if (_GCProvince == "")
                    return "";
                return _GCProvince.Split('^')[1];
            }
        }
    }
    #endregion
}