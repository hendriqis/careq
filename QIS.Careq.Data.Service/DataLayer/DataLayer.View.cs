using System;
using QIS.Data.Core.Dal;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace QIS.Careq.Data.Service
{
    #region MEMENTO Views
    #region vAddress
    [Serializable]
    [Table(Name = "vAddress")]
    public class vAddress
    {
        private Int32 _AddressID;
        private String _StreetName;
        private String _District;
        private String _City;
        private String _County;
        private String _GCProvince;
        private String _Province;
        private Int32 _ZipCodeID;
        private String _ZipCode;
        private String _PhoneNo1;
        private String _PhoneNo2;
        private String _FaxNo1;
        private String _FaxNo2;
        private Boolean _IsMailingAddress;

        [Column(Name = "AddressID", DataType = "Int32")]
        public Int32 AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "GCProvince", DataType = "String")]
        public String GCProvince
        {
            get { return _GCProvince; }
            set { _GCProvince = value; }
        }
        [Column(Name = "Province", DataType = "String")]
        public String Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        [Column(Name = "ZipCodeID", DataType = "Int32")]
        public Int32 ZipCodeID
        {
            get { return _ZipCodeID; }
            set { _ZipCodeID = value; }
        }
        [Column(Name = "ZipCode", DataType = "String")]
        public String ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        [Column(Name = "PhoneNo1", DataType = "String")]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "PhoneNo2", DataType = "String")]
        public String PhoneNo2
        {
            get { return _PhoneNo2; }
            set { _PhoneNo2 = value; }
        }
        [Column(Name = "FaxNo1", DataType = "String")]
        public String FaxNo1
        {
            get { return _FaxNo1; }
            set { _FaxNo1 = value; }
        }
        [Column(Name = "FaxNo2", DataType = "String")]
        public String FaxNo2
        {
            get { return _FaxNo2; }
            set { _FaxNo2 = value; }
        }
        [Column(Name = "IsMailingAddress", DataType = "Boolean")]
        public Boolean IsMailingAddress
        {
            get { return _IsMailingAddress; }
            set { _IsMailingAddress = value; }
        }
    }
    #endregion
    #region vCompany
    [Serializable]
    [Table(Name = "vCompany")]
    public partial class vCompany
    {
        private Int32 _CompanyID;
        private String _CompanyCode;
        private String _CompanyName;
        private String _ShortName;
        private Int32 _LOBClassID;
        private String _LOBClassCode;
        private String _LOBClassName;
        private String _GCCompanyType;
        private String _WebsiteUrl;
        private String _CompanyType;
        private String _GCCountryOfOrigin;
        private String _CountryOfOrigin;
        private Int32 _ContactPersonID;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private Int32 _HoldingCompanyID;
        private String _HoldingCompanyCode;
        private String _HoldingCompanyName;
        private Int32 _RegionID;
        private String _RegionCode;
        private String _RegionName;
        private Int32 _AddressID;
        private String _PhoneNo1;
        private String _PhoneNo2;
        private String _FaxNo1;
        private String _FaxNo2;
        private String _StreetName;
        private String _County;
        private String _District;
        private String _City;
        private String _GCProvince;
        private String _Province;
        private Int32 _ZipCodeID;
        private String _ZipCode;
        private String _EmailAddress;
        private Boolean _IsTaxable;
        private String _VATRegistrationNo;
        private Int16 _RatingLevel;
        private Boolean _IsGoPublicCompany;
        private String _Remarks;
        private String _CompanyCertification;
        private Boolean _IsDeleted;
        private String _CreatedByUserName;
        private DateTime _CreatedDate;
        private String _LastUpdatedByUserName;
        private DateTime _LastUpdatedDate;

        [Column(Name = "CompanyID", DataType = "Int32")]
        public Int32 CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "CompanyCode", DataType = "String")]
        public String CompanyCode
        {
            get { return _CompanyCode; }
            set { _CompanyCode = value; }
        }
        [Column(Name = "CompanyName", DataType = "String")]
        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        [Column(Name = "ShortName", DataType = "String")]
        public String ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
        [Column(Name = "LOBClassID", DataType = "Int32")]
        public Int32 LOBClassID
        {
            get { return _LOBClassID; }
            set { _LOBClassID = value; }
        }
        [Column(Name = "LOBClassCode", DataType = "String")]
        public String LOBClassCode
        {
            get { return _LOBClassCode; }
            set { _LOBClassCode = value; }
        }
        [Column(Name = "LOBClassName", DataType = "String")]
        public String LOBClassName
        {
            get { return _LOBClassName; }
            set { _LOBClassName = value; }
        }
        [Column(Name = "GCCompanyType", DataType = "String")]
        public String GCCompanyType
        {
            get { return _GCCompanyType; }
            set { _GCCompanyType = value; }
        }
        [Column(Name = "WebsiteUrl", DataType = "String")]
        public String WebsiteUrl
        {
            get { return _WebsiteUrl; }
            set { _WebsiteUrl = value; }
        }
        [Column(Name = "CompanyType", DataType = "String")]
        public String CompanyType
        {
            get { return _CompanyType; }
            set { _CompanyType = value; }
        }
        [Column(Name = "GCCountryOfOrigin", DataType = "String")]
        public String GCCountryOfOrigin
        {
            get { return _GCCountryOfOrigin; }
            set { _GCCountryOfOrigin = value; }
        }
        [Column(Name = "CountryOfOrigin", DataType = "String")]
        public String CountryOfOrigin
        {
            get { return _CountryOfOrigin; }
            set { _CountryOfOrigin = value; }
        }
        [Column(Name = "ContactPersonID", DataType = "Int32")]
        public Int32 ContactPersonID
        {
            get { return _ContactPersonID; }
            set { _ContactPersonID = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "HoldingCompanyID", DataType = "Int32")]
        public Int32 HoldingCompanyID
        {
            get { return _HoldingCompanyID; }
            set { _HoldingCompanyID = value; }
        }
        [Column(Name = "HoldingCompanyCode", DataType = "String")]
        public String HoldingCompanyCode
        {
            get { return _HoldingCompanyCode; }
            set { _HoldingCompanyCode = value; }
        }
        [Column(Name = "HoldingCompanyName", DataType = "String")]
        public String HoldingCompanyName
        {
            get { return _HoldingCompanyName; }
            set { _HoldingCompanyName = value; }
        }
        [Column(Name = "RegionID", DataType = "Int32")]
        public Int32 RegionID
        {
            get { return _RegionID; }
            set { _RegionID = value; }
        }
        [Column(Name = "RegionCode", DataType = "String")]
        public String RegionCode
        {
            get { return _RegionCode; }
            set { _RegionCode = value; }
        }
        [Column(Name = "RegionName", DataType = "String")]
        public String RegionName
        {
            get { return _RegionName; }
            set { _RegionName = value; }
        }
        [Column(Name = "AddressID", DataType = "Int32")]
        public Int32 AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "PhoneNo1", DataType = "String")]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "PhoneNo2", DataType = "String")]
        public String PhoneNo2
        {
            get { return _PhoneNo2; }
            set { _PhoneNo2 = value; }
        }
        [Column(Name = "FaxNo1", DataType = "String")]
        public String FaxNo1
        {
            get { return _FaxNo1; }
            set { _FaxNo1 = value; }
        }
        [Column(Name = "FaxNo2", DataType = "String")]
        public String FaxNo2
        {
            get { return _FaxNo2; }
            set { _FaxNo2 = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "GCProvince", DataType = "String")]
        public String GCProvince
        {
            get { return _GCProvince; }
            set { _GCProvince = value; }
        }
        [Column(Name = "Province", DataType = "String")]
        public String Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        [Column(Name = "ZipCodeID", DataType = "Int32")]
        public Int32 ZipCodeID
        {
            get { return _ZipCodeID; }
            set { _ZipCodeID = value; }
        }
        [Column(Name = "ZipCode", DataType = "String")]
        public String ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        [Column(Name = "EmailAddress", DataType = "String")]
        public String EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        [Column(Name = "IsTaxable", DataType = "Boolean")]
        public Boolean IsTaxable
        {
            get { return _IsTaxable; }
            set { _IsTaxable = value; }
        }
        [Column(Name = "VATRegistrationNo", DataType = "String")]
        public String VATRegistrationNo
        {
            get { return _VATRegistrationNo; }
            set { _VATRegistrationNo = value; }
        }
        [Column(Name = "RatingLevel", DataType = "Int16")]
        public Int16 RatingLevel
        {
            get { return _RatingLevel; }
            set { _RatingLevel = value; }
        }
        [Column(Name = "IsGoPublicCompany", DataType = "Boolean")]
        public Boolean IsGoPublicCompany
        {
            get { return _IsGoPublicCompany; }
            set { _IsGoPublicCompany = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "CompanyCertification", DataType = "String")]
        public String CompanyCertification
        {
            get { return _CompanyCertification; }
            set { _CompanyCertification = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedByUserName", DataType = "String")]
        public String CreatedByUserName
        {
            get { return _CreatedByUserName; }
            set { _CreatedByUserName = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedByUserName", DataType = "String")]
        public String LastUpdatedByUserName
        {
            get { return _LastUpdatedByUserName; }
            set { _LastUpdatedByUserName = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime")]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }
    #endregion
    #region vCompanyCertification
    [Serializable]
    [Table(Name = "vCompanyCertification")]
    public class vCompanyCertification
    {
        private Int32 _CompanyID;
        private String _GCCompanyCertification;
        private String _CompanyCertification;

        [Column(Name = "CompanyID", DataType = "Int32")]
        public Int32 CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "GCCompanyCertification", DataType = "String")]
        public String GCCompanyCertification
        {
            get { return _GCCompanyCertification; }
            set { _GCCompanyCertification = value; }
        }
        [Column(Name = "CompanyCertification", DataType = "String")]
        public String CompanyCertification
        {
            get { return _CompanyCertification; }
            set { _CompanyCertification = value; }
        }
    }
    #endregion
    #region vCompanyContactPerson
    [Serializable]
    [Table(Name = "vCompanyContactPerson")]
    public partial class vCompanyContactPerson
    {
        private Int32 _ID;
        private Int32 _CompanyID;
        private String _GCDepartment;
        private String _Department;
        private Int32 _MemberID;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "CompanyID", DataType = "Int32")]
        public Int32 CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "GCDepartment", DataType = "String")]
        public String GCDepartment
        {
            get { return _GCDepartment; }
            set { _GCDepartment = value; }
        }
        [Column(Name = "Department", DataType = "String")]
        public String Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        [Column(Name = "MemberID", DataType = "Int32")]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vEmployee
    [Serializable]
    [Table(Name = "vEmployee")]
    public partial class vEmployee
    {
        private Int32 _EmployeeID;
        private String _EmployeeCode;
        private String _GCSalutation;
        private String _Salutation;
        private String _GCTitle;
        private String _Title;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _GCSuffix;
        private String _Suffix;
        private String _EmailAddress;
        private String _MobilePhone1;
        private String _MobilePhone2;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "EmployeeID", DataType = "Int32")]
        public Int32 EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }
        [Column(Name = "EmployeeCode", DataType = "String")]
        public String EmployeeCode
        {
            get { return _EmployeeCode; }
            set { _EmployeeCode = value; }
        }
        [Column(Name = "GCSalutation", DataType = "String")]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "Salutation", DataType = "String")]
        public String Salutation
        {
            get { return _Salutation; }
            set { _Salutation = value; }
        }
        [Column(Name = "GCTitle", DataType = "String")]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "Title", DataType = "String")]
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "GCSuffix", DataType = "String")]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "Suffix", DataType = "String")]
        public String Suffix
        {
            get { return _Suffix; }
            set { _Suffix = value; }
        }
        [Column(Name = "EmailAddress", DataType = "String")]
        public String EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        [Column(Name = "MobilePhone1", DataType = "String")]
        public String MobilePhone1
        {
            get { return _MobilePhone1; }
            set { _MobilePhone1 = value; }
        }
        [Column(Name = "MobilePhone2", DataType = "String")]
        public String MobilePhone2
        {
            get { return _MobilePhone2; }
            set { _MobilePhone2 = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vEvent
    [Serializable]
    [Table(Name = "vEvent")]
    public partial class vEvent
    {
        private Int32 _EventID;
        private String _EventCode;
        private String _EventName;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _StartTime;
        private String _EndTime;
        private Int32 _VenueID;
        private String _VenueCode;
        private String _VenueName;
        private Int32 _AddressID;
        private String _PhoneNo1;
        private String _PhoneNo2;
        private String _FaxNo1;
        private String _FaxNo2;
        private String _StreetName;
        private String _County;
        private String _District;
        private String _City;
        private String _GCProvince;
        private String _Province;
        private Int32 _ZipCodeID;
        private String _ZipCode;
        private String _RoomName;
        private Int32 _TrainingID;
        private String _TrainingCode;
        private String _TrainingName;
        private Int32 _TrainerID;
        private String _TrainerCode;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _Suffix;
        private String _AssistantTrainer;
        private Decimal _Price;
        private String _DefaultEmailText1;
        private String _DefaultEmailText2;
        private String _DefaultEmailReminderText;
        private String _Remarks;
        private String _GCEventStatus;

        [Column(Name = "EventID", DataType = "Int32")]
        public Int32 EventID
        {
            get { return _EventID; }
            set { _EventID = value; }
        }
        [Column(Name = "EventCode", DataType = "String")]
        public String EventCode
        {
            get { return _EventCode; }
            set { _EventCode = value; }
        }
        [Column(Name = "EventName", DataType = "String")]
        public String EventName
        {
            get { return _EventName; }
            set { _EventName = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "VenueID", DataType = "Int32")]
        public Int32 VenueID
        {
            get { return _VenueID; }
            set { _VenueID = value; }
        }
        [Column(Name = "VenueCode", DataType = "String")]
        public String VenueCode
        {
            get { return _VenueCode; }
            set { _VenueCode = value; }
        }
        [Column(Name = "VenueName", DataType = "String")]
        public String VenueName
        {
            get { return _VenueName; }
            set { _VenueName = value; }
        }
        [Column(Name = "AddressID", DataType = "Int32")]
        public Int32 AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "PhoneNo1", DataType = "String")]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "PhoneNo2", DataType = "String")]
        public String PhoneNo2
        {
            get { return _PhoneNo2; }
            set { _PhoneNo2 = value; }
        }
        [Column(Name = "FaxNo1", DataType = "String")]
        public String FaxNo1
        {
            get { return _FaxNo1; }
            set { _FaxNo1 = value; }
        }
        [Column(Name = "FaxNo2", DataType = "String")]
        public String FaxNo2
        {
            get { return _FaxNo2; }
            set { _FaxNo2 = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "GCProvince", DataType = "String")]
        public String GCProvince
        {
            get { return _GCProvince; }
            set { _GCProvince = value; }
        }
        [Column(Name = "Province", DataType = "String")]
        public String Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        [Column(Name = "ZipCodeID", DataType = "Int32")]
        public Int32 ZipCodeID
        {
            get { return _ZipCodeID; }
            set { _ZipCodeID = value; }
        }
        [Column(Name = "ZipCode", DataType = "String")]
        public String ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "TrainingID", DataType = "Int32")]
        public Int32 TrainingID
        {
            get { return _TrainingID; }
            set { _TrainingID = value; }
        }
        [Column(Name = "TrainingCode", DataType = "String")]
        public String TrainingCode
        {
            get { return _TrainingCode; }
            set { _TrainingCode = value; }
        }
        [Column(Name = "TrainingName", DataType = "String")]
        public String TrainingName
        {
            get { return _TrainingName; }
            set { _TrainingName = value; }
        }
        [Column(Name = "TrainerID", DataType = "Int32")]
        public Int32 TrainerID
        {
            get { return _TrainerID; }
            set { _TrainerID = value; }
        }
        [Column(Name = "TrainerCode", DataType = "String")]
        public String TrainerCode
        {
            get { return _TrainerCode; }
            set { _TrainerCode = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "Suffix", DataType = "String")]
        public String Suffix
        {
            get { return _Suffix; }
            set { _Suffix = value; }
        }
        [Column(Name = "AssistantTrainer", DataType = "String")]
        public String AssistantTrainer
        {
            get { return _AssistantTrainer; }
            set { _AssistantTrainer = value; }
        }
        [Column(Name = "Price", DataType = "Decimal")]
        public Decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        [Column(Name = "DefaultEmailText1", DataType = "String")]
        public String DefaultEmailText1
        {
            get { return _DefaultEmailText1; }
            set { _DefaultEmailText1 = value; }
        }
        [Column(Name = "DefaultEmailText2", DataType = "String")]
        public String DefaultEmailText2
        {
            get { return _DefaultEmailText2; }
            set { _DefaultEmailText2 = value; }
        }
        [Column(Name = "DefaultEmailReminderText", DataType = "String")]
        public String DefaultEmailReminderText
        {
            get { return _DefaultEmailReminderText; }
            set { _DefaultEmailReminderText = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCEventStatus", DataType = "String")]
        public String GCEventStatus
        {
            get { return _GCEventStatus; }
            set { _GCEventStatus = value; }
        }
    }
    #endregion
    #region vEventCompanyDt
    [Serializable]
    [Table(Name = "vEventCompanyDt")]
    public partial class vEventCompanyDt
    {
        private Int32 _EventID;
        private String _EventName;
        private String _VenueName;
        private String _RoomName;
        private DateTime _StartDate;
        private String _StartTime;
        private DateTime _EndDate;
        private String _EndTime;
        private String _StreetName;
        private String _County;
        private String _District;
        private String _City;
        private String _Province;
        private String _TrainerFirstName;
        private String _TrainerMiddleName;
        private String _TrainerLastName;
        private String _Suffix;
        private Int32 _CompanyID;
        private String _CompanyCode;
        private String _CompanyName;
        private Int32 _PICMemberID;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private DateTime _PaymentDate;
        private Decimal _Price;
        private Decimal _DiscountAmount;
        private Boolean _IsFinalDiscount;
        private String _DefaultEmailText1;
        private String _DefaultEmailText2;

        [Column(Name = "EventID", DataType = "Int32")]
        public Int32 EventID
        {
            get { return _EventID; }
            set { _EventID = value; }
        }
        [Column(Name = "EventName", DataType = "String")]
        public String EventName
        {
            get { return _EventName; }
            set { _EventName = value; }
        }
        [Column(Name = "VenueName", DataType = "String")]
        public String VenueName
        {
            get { return _VenueName; }
            set { _VenueName = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "Province", DataType = "String")]
        public String Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        [Column(Name = "TrainerFirstName", DataType = "String")]
        public String TrainerFirstName
        {
            get { return _TrainerFirstName; }
            set { _TrainerFirstName = value; }
        }
        [Column(Name = "TrainerMiddleName", DataType = "String")]
        public String TrainerMiddleName
        {
            get { return _TrainerMiddleName; }
            set { _TrainerMiddleName = value; }
        }
        [Column(Name = "TrainerLastName", DataType = "String")]
        public String TrainerLastName
        {
            get { return _TrainerLastName; }
            set { _TrainerLastName = value; }
        }
        [Column(Name = "Suffix", DataType = "String")]
        public String Suffix
        {
            get { return _Suffix; }
            set { _Suffix = value; }
        }
        [Column(Name = "CompanyID", DataType = "Int32")]
        public Int32 CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "CompanyCode", DataType = "String")]
        public String CompanyCode
        {
            get { return _CompanyCode; }
            set { _CompanyCode = value; }
        }
        [Column(Name = "CompanyName", DataType = "String")]
        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        [Column(Name = "PICMemberID", DataType = "Int32")]
        public Int32 PICMemberID
        {
            get { return _PICMemberID; }
            set { _PICMemberID = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "PaymentDate", DataType = "DateTime")]
        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }
        [Column(Name = "Price", DataType = "Decimal")]
        public Decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        [Column(Name = "DiscountAmount", DataType = "Decimal")]
        public Decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        [Column(Name = "IsFinalDiscount", DataType = "Boolean")]
        public Boolean IsFinalDiscount
        {
            get { return _IsFinalDiscount; }
            set { _IsFinalDiscount = value; }
        }
        [Column(Name = "DefaultEmailText1", DataType = "String")]
        public String DefaultEmailText1
        {
            get { return _DefaultEmailText1; }
            set { _DefaultEmailText1 = value; }
        }
        [Column(Name = "DefaultEmailText2", DataType = "String")]
        public String DefaultEmailText2
        {
            get { return _DefaultEmailText2; }
            set { _DefaultEmailText2 = value; }
        }
    }
    #endregion
    #region vEventInvitation
    [Serializable]
    [Table(Name = "vEventInvitation")]
    public partial class vEventInvitation
    {
        private Int32 _EventID;
        private Int32 _MemberID;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _Suffix;
        private String _Occupation;
        private String _Department;
        private Int32 _CompanyID;
        private String _CompanyName;
        private Boolean _IsConfirmed;
        private DateTime _ConfirmedDate;

        [Column(Name = "EventID", DataType = "Int32")]
        public Int32 EventID
        {
            get { return _EventID; }
            set { _EventID = value; }
        }
        [Column(Name = "MemberID", DataType = "Int32")]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "Suffix", DataType = "String")]
        public String Suffix
        {
            get { return _Suffix; }
            set { _Suffix = value; }
        }
        [Column(Name = "Occupation", DataType = "String")]
        public String Occupation
        {
            get { return _Occupation; }
            set { _Occupation = value; }
        }
        [Column(Name = "Department", DataType = "String")]
        public String Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        [Column(Name = "CompanyID", DataType = "Int32")]
        public Int32 CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "CompanyName", DataType = "String")]
        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        [Column(Name = "IsConfirmed", DataType = "Boolean")]
        public Boolean IsConfirmed
        {
            get { return _IsConfirmed; }
            set { _IsConfirmed = value; }
        }
        [Column(Name = "ConfirmedDate", DataType = "DateTime")]
        public DateTime ConfirmedDate
        {
            get { return _ConfirmedDate; }
            set { _ConfirmedDate = value; }
        }
    }
    #endregion
    #region vEventRegistration
    [Serializable]
    [Table(Name = "vEventRegistration")]
    public partial class vEventRegistration
    {
        private Int32 _EventID;
        private String _EventName;
        private Int32 _MemberID;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _PreferredName;
        private Int32 _AddressID;
        private String _StreetName;
        private String _County;
        private String _District;
        private String _City;
        private String _EmailAddress1;
        private String _EmailAddress2;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private String _FaxNo1;
        private Int32 _CompanyID;
        private String _CompanyName;
        private String _LOBClassName;
        private String _PhoneNo1;
        private String _Occupation;
        private String _GCOccupationLevel;
        private String _OccupationLevel;
        private String _GCDepartment;
        private String _Department;
        private String _GCRegistrationType;
        private String _RegistrationType;
        private String _GCInformationSource;
        private String _InformationSource;
        private String _GCRegistrationStatus;
        private String _RegistrationStatus;
        private String _CertificationNo;
        private Int16 _CertificatePrintNo;
        private DateTime _StartDate;
        private String _StartTime;
        private DateTime _EndDate;
        private String _EndTime;
        private String _TrainingName;
        private String _VenueName;
        private String _RoomName;
        private String _TrainerFirstName;
        private String _TrainerMiddleName;
        private String _TrainerLastName;
        private String _TrainerSuffix;
        private String _EventStreetName;
        private String _EventCounty;
        private String _EventDistrict;
        private String _EventCity;
        private String _EventProvince;
        private Decimal _Price;
        private Decimal _DiscountAmount;
        private Boolean _IsFinalDiscount;
        private String _Remarks;
        private String _DefaultEmailText1;
        private String _DefaultEmailText2;

        [Column(Name = "EventID", DataType = "Int32")]
        public Int32 EventID
        {
            get { return _EventID; }
            set { _EventID = value; }
        }
        [Column(Name = "EventName", DataType = "String")]
        public String EventName
        {
            get { return _EventName; }
            set { _EventName = value; }
        }
        [Column(Name = "MemberID", DataType = "Int32")]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "PreferredName", DataType = "String")]
        public String PreferredName
        {
            get { return _PreferredName; }
            set { _PreferredName = value; }
        }
        [Column(Name = "AddressID", DataType = "Int32")]
        public Int32 AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "EmailAddress1", DataType = "String")]
        public String EmailAddress1
        {
            get { return _EmailAddress1; }
            set { _EmailAddress1 = value; }
        }
        [Column(Name = "EmailAddress2", DataType = "String")]
        public String EmailAddress2
        {
            get { return _EmailAddress2; }
            set { _EmailAddress2 = value; }
        }
        [Column(Name = "MobilePhoneNo1", DataType = "String")]
        public String MobilePhoneNo1
        {
            get { return _MobilePhoneNo1; }
            set { _MobilePhoneNo1 = value; }
        }
        [Column(Name = "MobilePhoneNo2", DataType = "String")]
        public String MobilePhoneNo2
        {
            get { return _MobilePhoneNo2; }
            set { _MobilePhoneNo2 = value; }
        }
        [Column(Name = "FaxNo1", DataType = "String")]
        public String FaxNo1
        {
            get { return _FaxNo1; }
            set { _FaxNo1 = value; }
        }
        [Column(Name = "CompanyID", DataType = "Int32")]
        public Int32 CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "CompanyName", DataType = "String")]
        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        [Column(Name = "LOBClassName", DataType = "String")]
        public String LOBClassName
        {
            get { return _LOBClassName; }
            set { _LOBClassName = value; }
        }
        [Column(Name = "PhoneNo1", DataType = "String")]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "Occupation", DataType = "String")]
        public String Occupation
        {
            get { return _Occupation; }
            set { _Occupation = value; }
        }
        [Column(Name = "GCOccupationLevel", DataType = "String")]
        public String GCOccupationLevel
        {
            get { return _GCOccupationLevel; }
            set { _GCOccupationLevel = value; }
        }
        [Column(Name = "OccupationLevel", DataType = "String")]
        public String OccupationLevel
        {
            get { return _OccupationLevel; }
            set { _OccupationLevel = value; }
        }
        [Column(Name = "GCDepartment", DataType = "String")]
        public String GCDepartment
        {
            get { return _GCDepartment; }
            set { _GCDepartment = value; }
        }
        [Column(Name = "Department", DataType = "String")]
        public String Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        [Column(Name = "GCRegistrationType", DataType = "String")]
        public String GCRegistrationType
        {
            get { return _GCRegistrationType; }
            set { _GCRegistrationType = value; }
        }
        [Column(Name = "RegistrationType", DataType = "String")]
        public String RegistrationType
        {
            get { return _RegistrationType; }
            set { _RegistrationType = value; }
        }
        [Column(Name = "GCInformationSource", DataType = "String")]
        public String GCInformationSource
        {
            get { return _GCInformationSource; }
            set { _GCInformationSource = value; }
        }
        [Column(Name = "InformationSource", DataType = "String")]
        public String InformationSource
        {
            get { return _InformationSource; }
            set { _InformationSource = value; }
        }
        [Column(Name = "GCRegistrationStatus", DataType = "String")]
        public String GCRegistrationStatus
        {
            get { return _GCRegistrationStatus; }
            set { _GCRegistrationStatus = value; }
        }
        [Column(Name = "RegistrationStatus", DataType = "String")]
        public String RegistrationStatus
        {
            get { return _RegistrationStatus; }
            set { _RegistrationStatus = value; }
        }
        [Column(Name = "CertificationNo", DataType = "String")]
        public String CertificationNo
        {
            get { return _CertificationNo; }
            set { _CertificationNo = value; }
        }
        [Column(Name = "CertificatePrintNo", DataType = "Int16")]
        public Int16 CertificatePrintNo
        {
            get { return _CertificatePrintNo; }
            set { _CertificatePrintNo = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "StartTime", DataType = "String")]
        public String StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "EndTime", DataType = "String")]
        public String EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
        [Column(Name = "TrainingName", DataType = "String")]
        public String TrainingName
        {
            get { return _TrainingName; }
            set { _TrainingName = value; }
        }
        [Column(Name = "VenueName", DataType = "String")]
        public String VenueName
        {
            get { return _VenueName; }
            set { _VenueName = value; }
        }
        [Column(Name = "RoomName", DataType = "String")]
        public String RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        [Column(Name = "TrainerFirstName", DataType = "String")]
        public String TrainerFirstName
        {
            get { return _TrainerFirstName; }
            set { _TrainerFirstName = value; }
        }
        [Column(Name = "TrainerMiddleName", DataType = "String")]
        public String TrainerMiddleName
        {
            get { return _TrainerMiddleName; }
            set { _TrainerMiddleName = value; }
        }
        [Column(Name = "TrainerLastName", DataType = "String")]
        public String TrainerLastName
        {
            get { return _TrainerLastName; }
            set { _TrainerLastName = value; }
        }
        [Column(Name = "TrainerSuffix", DataType = "String")]
        public String TrainerSuffix
        {
            get { return _TrainerSuffix; }
            set { _TrainerSuffix = value; }
        }
        [Column(Name = "EventStreetName", DataType = "String")]
        public String EventStreetName
        {
            get { return _EventStreetName; }
            set { _EventStreetName = value; }
        }
        [Column(Name = "EventCounty", DataType = "String")]
        public String EventCounty
        {
            get { return _EventCounty; }
            set { _EventCounty = value; }
        }
        [Column(Name = "EventDistrict", DataType = "String")]
        public String EventDistrict
        {
            get { return _EventDistrict; }
            set { _EventDistrict = value; }
        }
        [Column(Name = "EventCity", DataType = "String")]
        public String EventCity
        {
            get { return _EventCity; }
            set { _EventCity = value; }
        }
        [Column(Name = "EventProvince", DataType = "String")]
        public String EventProvince
        {
            get { return _EventProvince; }
            set { _EventProvince = value; }
        }
        [Column(Name = "Price", DataType = "Decimal")]
        public Decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        [Column(Name = "DiscountAmount", DataType = "Decimal")]
        public Decimal DiscountAmount
        {
            get { return _DiscountAmount; }
            set { _DiscountAmount = value; }
        }
        [Column(Name = "IsFinalDiscount", DataType = "Boolean")]
        public Boolean IsFinalDiscount
        {
            get { return _IsFinalDiscount; }
            set { _IsFinalDiscount = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "DefaultEmailText1", DataType = "String")]
        public String DefaultEmailText1
        {
            get { return _DefaultEmailText1; }
            set { _DefaultEmailText1 = value; }
        }
        [Column(Name = "DefaultEmailText2", DataType = "String")]
        public String DefaultEmailText2
        {
            get { return _DefaultEmailText2; }
            set { _DefaultEmailText2 = value; }
        }
    }
    #endregion
    #region vEventRegistrationPivot
    [Serializable]
    [Table(Name = "vEventRegistrationPivot")]
    public partial class vEventRegistrationPivot
    {
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _PreferredName;
        private Decimal _NumberOfTraining;
        private String _OccupationLevel;
        private String _CompanyName;
        private String _CompanyType;
        private String _TrainingName;
        private DateTime _EventDate;
        

        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "PreferredName", DataType = "String")]
        public String PreferredName
        {
            get { return _PreferredName; }
            set { _PreferredName = value; }
        }
        [Column(Name = "NumberOfTraining", DataType = "Decimal")]
        public Decimal NumberOfTraining
        {
            get { return _NumberOfTraining; }
            set { _NumberOfTraining = value; }
        }
        [Column(Name = "OccupationLevel", DataType = "String")]
        public String OccupationLevel
        {
            get { return _OccupationLevel; }
            set { _OccupationLevel = value; }
        }
        [Column(Name = "CompanyName", DataType = "String")]
        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        [Column(Name = "CompanyType", DataType = "String")]
        public String CompanyType
        {
            get { return _CompanyType; }
            set { _CompanyType = value; }
        }
        [Column(Name = "TrainingName", DataType = "String")]
        public String TrainingName
        {
            get { return _TrainingName; }
            set { _TrainingName = value; }
        }
        [Column(Name = "EventDate", DataType = "DateTime")]
        public DateTime EventDate
        {
            get { return _EventDate; }
            set { _EventDate = value; }
        }
    }
    #endregion
    #region vFilterParameter
    [Serializable]
    [Table(Name = "vFilterParameter")]
    public class vFilterParameter
    {
        private Int32 _FilterParameterID;
        private String _FilterParameterCode;
        private String _FilterParameterName;
        private String _ControlName;
        private String _FilterParameterCaption;
        private String _GCFilterParameterType;
        private String _FilterParameterType;
        private String _MethodName;
        private String _FilterExpression;
        private String _ValueFieldName;
        private String _TextFieldName;
        private String _FieldName;
        private Boolean _IsDeleted;

        [Column(Name = "FilterParameterID", DataType = "Int32")]
        public Int32 FilterParameterID
        {
            get { return _FilterParameterID; }
            set { _FilterParameterID = value; }
        }
        [Column(Name = "FilterParameterCode", DataType = "String")]
        public String FilterParameterCode
        {
            get { return _FilterParameterCode; }
            set { _FilterParameterCode = value; }
        }
        [Column(Name = "FilterParameterName", DataType = "String")]
        public String FilterParameterName
        {
            get { return _FilterParameterName; }
            set { _FilterParameterName = value; }
        }
        [Column(Name = "ControlName", DataType = "String")]
        public String ControlName
        {
            get { return _ControlName; }
            set { _ControlName = value; }
        }
        [Column(Name = "FilterParameterCaption", DataType = "String")]
        public String FilterParameterCaption
        {
            get { return _FilterParameterCaption; }
            set { _FilterParameterCaption = value; }
        }
        [Column(Name = "GCFilterParameterType", DataType = "String")]
        public String GCFilterParameterType
        {
            get { return _GCFilterParameterType; }
            set { _GCFilterParameterType = value; }
        }
        [Column(Name = "FilterParameterType", DataType = "String")]
        public String FilterParameterType
        {
            get { return _FilterParameterType; }
            set { _FilterParameterType = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String")]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "ValueFieldName", DataType = "String")]
        public String ValueFieldName
        {
            get { return _ValueFieldName; }
            set { _ValueFieldName = value; }
        }
        [Column(Name = "TextFieldName", DataType = "String")]
        public String TextFieldName
        {
            get { return _TextFieldName; }
            set { _TextFieldName = value; }
        }
        [Column(Name = "FieldName", DataType = "String")]
        public String FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vInquiry
    [Serializable]
    [Table(Name = "vInquiry")]
    public partial class vInquiry
    {
        private Int32 _InquiryID;
        private String _InquiryNo;
        private DateTime _InquiryDate;
        private Int32 _LeadID;
        private String _LeadNo;
        private Int32 _CompanyID;
        private String _CompanyCode;
        private String _CompanyName;
        private Int32 _PIC_CRO;
        private String _PIC_CROCode;
        private String _EmployeeSalutation;
        private String _EmployeeTitle;
        private String _EmplyoeeFirstName;
        private String _EmployeeMiddleName;
        private String _EmployeeLastName;
        private String _EmployeeSuffix;
        private Int32 _MemberID;
        private String _MemberCode;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private Int32 _PIC_TrainerID;
        private String _TrainerCode;
        private String _TrainerSalutation;
        private String _TrainerTitle;
        private String _TrainerFirstName;
        private String _TrainerMiddleName;
        private String _TrainerLastName;
        private String _TrainerSuffix;
        private String _Subject;
        private String _Remarks;
        private String _GCItemType;
        private Int32 _ItemID;
        private String _GCInquiryStatus;
        private String _GCInquiryProcessType;
        private String _OtherInquiryProcessType;
        private String _GCCloseReason;
        private String _OtherCloseReasonText;

        [Column(Name = "InquiryID", DataType = "Int32")]
        public Int32 InquiryID
        {
            get { return _InquiryID; }
            set { _InquiryID = value; }
        }
        [Column(Name = "InquiryNo", DataType = "String")]
        public String InquiryNo
        {
            get { return _InquiryNo; }
            set { _InquiryNo = value; }
        }
        [Column(Name = "InquiryDate", DataType = "DateTime")]
        public DateTime InquiryDate
        {
            get { return _InquiryDate; }
            set { _InquiryDate = value; }
        }
        [Column(Name = "LeadID", DataType = "Int32")]
        public Int32 LeadID
        {
            get { return _LeadID; }
            set { _LeadID = value; }
        }
        [Column(Name = "LeadNo", DataType = "String")]
        public String LeadNo
        {
            get { return _LeadNo; }
            set { _LeadNo = value; }
        }
        [Column(Name = "CompanyID", DataType = "Int32")]
        public Int32 CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "CompanyCode", DataType = "String")]
        public String CompanyCode
        {
            get { return _CompanyCode; }
            set { _CompanyCode = value; }
        }
        [Column(Name = "CompanyName", DataType = "String")]
        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        [Column(Name = "PIC_CRO", DataType = "Int32")]
        public Int32 PIC_CRO
        {
            get { return _PIC_CRO; }
            set { _PIC_CRO = value; }
        }
        [Column(Name = "PIC_CROCode", DataType = "String")]
        public String PIC_CROCode
        {
            get { return _PIC_CROCode; }
            set { _PIC_CROCode = value; }
        }
        [Column(Name = "EmployeeSalutation", DataType = "String")]
        public String EmployeeSalutation
        {
            get { return _EmployeeSalutation; }
            set { _EmployeeSalutation = value; }
        }
        [Column(Name = "EmployeeTitle", DataType = "String")]
        public String EmployeeTitle
        {
            get { return _EmployeeTitle; }
            set { _EmployeeTitle = value; }
        }
        [Column(Name = "EmplyoeeFirstName", DataType = "String")]
        public String EmplyoeeFirstName
        {
            get { return _EmplyoeeFirstName; }
            set { _EmplyoeeFirstName = value; }
        }
        [Column(Name = "EmployeeMiddleName", DataType = "String")]
        public String EmployeeMiddleName
        {
            get { return _EmployeeMiddleName; }
            set { _EmployeeMiddleName = value; }
        }
        [Column(Name = "EmployeeLastName", DataType = "String")]
        public String EmployeeLastName
        {
            get { return _EmployeeLastName; }
            set { _EmployeeLastName = value; }
        }
        [Column(Name = "EmployeeSuffix", DataType = "String")]
        public String EmployeeSuffix
        {
            get { return _EmployeeSuffix; }
            set { _EmployeeSuffix = value; }
        }
        [Column(Name = "MemberID", DataType = "Int32")]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "MemberCode", DataType = "String")]
        public String MemberCode
        {
            get { return _MemberCode; }
            set { _MemberCode = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "PIC_TrainerID", DataType = "Int32")]
        public Int32 PIC_TrainerID
        {
            get { return _PIC_TrainerID; }
            set { _PIC_TrainerID = value; }
        }
        [Column(Name = "TrainerCode", DataType = "String")]
        public String TrainerCode
        {
            get { return _TrainerCode; }
            set { _TrainerCode = value; }
        }
        [Column(Name = "TrainerSalutation", DataType = "String")]
        public String TrainerSalutation
        {
            get { return _TrainerSalutation; }
            set { _TrainerSalutation = value; }
        }
        [Column(Name = "TrainerTitle", DataType = "String")]
        public String TrainerTitle
        {
            get { return _TrainerTitle; }
            set { _TrainerTitle = value; }
        }
        [Column(Name = "TrainerFirstName", DataType = "String")]
        public String TrainerFirstName
        {
            get { return _TrainerFirstName; }
            set { _TrainerFirstName = value; }
        }
        [Column(Name = "TrainerMiddleName", DataType = "String")]
        public String TrainerMiddleName
        {
            get { return _TrainerMiddleName; }
            set { _TrainerMiddleName = value; }
        }
        [Column(Name = "TrainerLastName", DataType = "String")]
        public String TrainerLastName
        {
            get { return _TrainerLastName; }
            set { _TrainerLastName = value; }
        }
        [Column(Name = "TrainerSuffix", DataType = "String")]
        public String TrainerSuffix
        {
            get { return _TrainerSuffix; }
            set { _TrainerSuffix = value; }
        }
        [Column(Name = "Subject", DataType = "String")]
        public String Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCItemType", DataType = "String")]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32")]
        public Int32 ItemID
        {
            get { return _ItemID; }
            set { _ItemID = value; }
        }
        [Column(Name = "GCInquiryStatus", DataType = "String")]
        public String GCInquiryStatus
        {
            get { return _GCInquiryStatus; }
            set { _GCInquiryStatus = value; }
        }
        [Column(Name = "GCInquiryProcessType", DataType = "String")]
        public String GCInquiryProcessType
        {
            get { return _GCInquiryProcessType; }
            set { _GCInquiryProcessType = value; }
        }
        [Column(Name = "OtherInquiryProcessType", DataType = "String")]
        public String OtherInquiryProcessType
        {
            get { return _OtherInquiryProcessType; }
            set { _OtherInquiryProcessType = value; }
        }
        [Column(Name = "GCCloseReason", DataType = "String")]
        public String GCCloseReason
        {
            get { return _GCCloseReason; }
            set { _GCCloseReason = value; }
        }
        [Column(Name = "OtherCloseReasonText", DataType = "String")]
        public String OtherCloseReasonText
        {
            get { return _OtherCloseReasonText; }
            set { _OtherCloseReasonText = value; }
        }
    }
    #endregion
    #region vLead
    [Serializable]
    [Table(Name = "vLead")]
    public partial class vLead
    {
        private Int32 _LeadID;
        private String _LeadNo;
        private DateTime _LeadDate;
        private String _GCLeadSourceType;
        private String _LeadSourceType;
        private String _OtherLeadSourceType;
        private Int32 _CompanyID;
        private String _CompanyCode;
        private String _CompanyName;
        private Int32 _PIC_CRO;
        private String _PIC_CROCode;
        private String _EmployeeSalutation;
        private String _EmployeeTitle;
        private String _EmplyoeeFirstName;
        private String _EmployeeMiddleName;
        private String _EmployeeLastName;
        private String _EmployeeSuffix;
        private Int32 _MemberID;
        private String _MemberCode;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _Subject;
        private String _Remarks;
        private DateTime _ResponseDate;
        private String _ResponseTime;
        private String _GCLeadStatus;
        private String _GCCloseReason;
        private String _OtherCloseReason;

        [Column(Name = "LeadID", DataType = "Int32")]
        public Int32 LeadID
        {
            get { return _LeadID; }
            set { _LeadID = value; }
        }
        [Column(Name = "LeadNo", DataType = "String")]
        public String LeadNo
        {
            get { return _LeadNo; }
            set { _LeadNo = value; }
        }
        [Column(Name = "LeadDate", DataType = "DateTime")]
        public DateTime LeadDate
        {
            get { return _LeadDate; }
            set { _LeadDate = value; }
        }
        [Column(Name = "GCLeadSourceType", DataType = "String")]
        public String GCLeadSourceType
        {
            get { return _GCLeadSourceType; }
            set { _GCLeadSourceType = value; }
        }
        [Column(Name = "LeadSourceType", DataType = "String")]
        public String LeadSourceType
        {
            get { return _LeadSourceType; }
            set { _LeadSourceType = value; }
        }
        [Column(Name = "OtherLeadSourceType", DataType = "String")]
        public String OtherLeadSourceType
        {
            get { return _OtherLeadSourceType; }
            set { _OtherLeadSourceType = value; }
        }
        [Column(Name = "CompanyID", DataType = "Int32")]
        public Int32 CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "CompanyCode", DataType = "String")]
        public String CompanyCode
        {
            get { return _CompanyCode; }
            set { _CompanyCode = value; }
        }
        [Column(Name = "CompanyName", DataType = "String")]
        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        [Column(Name = "PIC_CRO", DataType = "Int32")]
        public Int32 PIC_CRO
        {
            get { return _PIC_CRO; }
            set { _PIC_CRO = value; }
        }
        [Column(Name = "PIC_CROCode", DataType = "String")]
        public String PIC_CROCode
        {
            get { return _PIC_CROCode; }
            set { _PIC_CROCode = value; }
        }
        [Column(Name = "EmployeeSalutation", DataType = "String")]
        public String EmployeeSalutation
        {
            get { return _EmployeeSalutation; }
            set { _EmployeeSalutation = value; }
        }
        [Column(Name = "EmployeeTitle", DataType = "String")]
        public String EmployeeTitle
        {
            get { return _EmployeeTitle; }
            set { _EmployeeTitle = value; }
        }
        [Column(Name = "EmplyoeeFirstName", DataType = "String")]
        public String EmplyoeeFirstName
        {
            get { return _EmplyoeeFirstName; }
            set { _EmplyoeeFirstName = value; }
        }
        [Column(Name = "EmployeeMiddleName", DataType = "String")]
        public String EmployeeMiddleName
        {
            get { return _EmployeeMiddleName; }
            set { _EmployeeMiddleName = value; }
        }
        [Column(Name = "EmployeeLastName", DataType = "String")]
        public String EmployeeLastName
        {
            get { return _EmployeeLastName; }
            set { _EmployeeLastName = value; }
        }
        [Column(Name = "EmployeeSuffix", DataType = "String")]
        public String EmployeeSuffix
        {
            get { return _EmployeeSuffix; }
            set { _EmployeeSuffix = value; }
        }
        [Column(Name = "MemberID", DataType = "Int32")]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "MemberCode", DataType = "String")]
        public String MemberCode
        {
            get { return _MemberCode; }
            set { _MemberCode = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "Subject", DataType = "String")]
        public String Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "ResponseDate", DataType = "DateTime")]
        public DateTime ResponseDate
        {
            get { return _ResponseDate; }
            set { _ResponseDate = value; }
        }
        [Column(Name = "ResponseTime", DataType = "String")]
        public String ResponseTime
        {
            get { return _ResponseTime; }
            set { _ResponseTime = value; }
        }
        [Column(Name = "GCLeadStatus", DataType = "String")]
        public String GCLeadStatus
        {
            get { return _GCLeadStatus; }
            set { _GCLeadStatus = value; }
        }
        [Column(Name = "GCCloseReason", DataType = "String")]
        public String GCCloseReason
        {
            get { return _GCCloseReason; }
            set { _GCCloseReason = value; }
        }
        [Column(Name = "OtherCloseReason", DataType = "String")]
        public String OtherCloseReason
        {
            get { return _OtherCloseReason; }
            set { _OtherCloseReason = value; }
        }
    }
    #endregion
    #region vLeadActivityLog
    [Serializable]
    [Table(Name = "vLeadActivityLog")]
    public partial class vLeadActivityLog
    {
        private Int32 _ID;
        private Int32 _LeadID;
        private String _LeadNo;
        private DateTime _LeadDate;
        private DateTime _LogDate;
        private String _LogTime;
        private String _GCActivityType;
        private String _ActivityType;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "LeadID", DataType = "Int32")]
        public Int32 LeadID
        {
            get { return _LeadID; }
            set { _LeadID = value; }
        }
        [Column(Name = "LeadNo", DataType = "String")]
        public String LeadNo
        {
            get { return _LeadNo; }
            set { _LeadNo = value; }
        }
        [Column(Name = "LeadDate", DataType = "DateTime")]
        public DateTime LeadDate
        {
            get { return _LeadDate; }
            set { _LeadDate = value; }
        }
        [Column(Name = "LogDate", DataType = "DateTime")]
        public DateTime LogDate
        {
            get { return _LogDate; }
            set { _LogDate = value; }
        }
        [Column(Name = "LogTime", DataType = "String")]
        public String LogTime
        {
            get { return _LogTime; }
            set { _LogTime = value; }
        }
        [Column(Name = "GCActivityType", DataType = "String")]
        public String GCActivityType
        {
            get { return _GCActivityType; }
            set { _GCActivityType = value; }
        }
        [Column(Name = "ActivityType", DataType = "String")]
        public String ActivityType
        {
            get { return _ActivityType; }
            set { _ActivityType = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vLOBClassification
    [Serializable]
    [Table(Name = "vLOBClassification")]
    public class vLOBClassification
    {
        private Int32 _LOBClassID;
        private String _LOBClassCode;
        private String _LOBClassName;
        private Int32 _ParentID;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32 _Level;
        private String _Path;

        [Column(Name = "LOBClassID", DataType = "Int32")]
        public Int32 LOBClassID
        {
            get { return _LOBClassID; }
            set { _LOBClassID = value; }
        }
        [Column(Name = "LOBClassCode", DataType = "String")]
        public String LOBClassCode
        {
            get { return _LOBClassCode; }
            set { _LOBClassCode = value; }
        }
        [Column(Name = "LOBClassName", DataType = "String")]
        public String LOBClassName
        {
            get { return _LOBClassName; }
            set { _LOBClassName = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32")]
        public Int32 ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
        [Column(Name = "Path", DataType = "String")]
        public String Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
    }
    #endregion
    #region vMember
    [Serializable]
    [Table(Name = "vMember")]
    public partial class vMember
    {
        private Int32 _MemberID;
        private String _MemberCode;
        private String _GCSalutation;
        private String _Salutation;
        private String _GCSuffix;
        private String _Suffix;
        private String _GCMemberStatus;
        private String _MemberStatus;
        private String _GCTitle;
        private String _Title;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _PreferredName;
        private String _CityOfBirth;
        private DateTime _DateOfBirth;
        private String _GCNationality;
        private String _Nationality;
        private Int32 _CompanyID;
        private String _CompanyCode;
        private String _CompanyName;
        private Int32 _LOBClassID;
        private String _LOBClassCode;
        private String _LOBClassName;
        private String _RegionName;
        private String _GCDepartment;
        private String _Department;
        private String _Occupation;
        private String _GCOccupationLevel;
        private String _OccupationLevel;
        private Int32 _AddressID;
        private String _PhoneNo1;
        private String _StreetName;
        private String _County;
        private String _District;
        private String _City;
        private String _GCProvince;
        private String _Province;
        private Int32 _ZipCodeID;
        private String _ZipCode;
        private Int32 _CompanyAddressID;
        private String _CompanyPhoneNo1;
        private String _CompanyStreetName;
        private String _CompanyCounty;
        private String _CompanyDistrict;
        private String _CompanyCity;
        private String _CompanyGCProvince;
        private String _CompanyProvince;
        private Int32 _CompanyZipCodeID;
        private String _CompanyZipCode;
        private String _EmailAddress1;
        private String _EmailAddress2;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private String _OfficeExtensionNo;
        private String _VATRegistrationNo;
        private DateTime _LastEventDate;
        private DateTime _EndDate;
        private Int32 _LastEventID;
        private String _LastEventName;
        private Int32 _LastCompanyID;
        private String _LastCompanyName;
        private Decimal _NumberOfTraining;
        private String _PictureFileName;
        private Boolean _IsCSClub;
        private Boolean _IsHRDClub;
        private Boolean _IsISOClub;
        private Boolean _IsCompanyContactPerson;
        private String _SendGreetingsOn;
        private Int16 _RatingLevel;
        private String _Remarks;
        private Boolean _IsDeleted;
        private String _CreatedByUserName;
        private DateTime _CreatedDate;
        private String _LastUpdatedByUserName;
        private DateTime _LastUpdatedDate;

        [Column(Name = "MemberID", DataType = "Int32")]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "MemberCode", DataType = "String")]
        public String MemberCode
        {
            get { return _MemberCode; }
            set { _MemberCode = value; }
        }
        [Column(Name = "GCSalutation", DataType = "String")]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "Salutation", DataType = "String")]
        public String Salutation
        {
            get { return _Salutation; }
            set { _Salutation = value; }
        }
        [Column(Name = "GCSuffix", DataType = "String")]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "Suffix", DataType = "String")]
        public String Suffix
        {
            get { return _Suffix; }
            set { _Suffix = value; }
        }
        [Column(Name = "GCMemberStatus", DataType = "String")]
        public String GCMemberStatus
        {
            get { return _GCMemberStatus; }
            set { _GCMemberStatus = value; }
        }
        [Column(Name = "MemberStatus", DataType = "String")]
        public String MemberStatus
        {
            get { return _MemberStatus; }
            set { _MemberStatus = value; }
        }
        [Column(Name = "GCTitle", DataType = "String")]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "Title", DataType = "String")]
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "PreferredName", DataType = "String")]
        public String PreferredName
        {
            get { return _PreferredName; }
            set { _PreferredName = value; }
        }
        [Column(Name = "CityOfBirth", DataType = "String")]
        public String CityOfBirth
        {
            get { return _CityOfBirth; }
            set { _CityOfBirth = value; }
        }
        [Column(Name = "DateOfBirth", DataType = "DateTime")]
        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        [Column(Name = "GCNationality", DataType = "String")]
        public String GCNationality
        {
            get { return _GCNationality; }
            set { _GCNationality = value; }
        }
        [Column(Name = "Nationality", DataType = "String")]
        public String Nationality
        {
            get { return _Nationality; }
            set { _Nationality = value; }
        }
        [Column(Name = "CompanyID", DataType = "Int32")]
        public Int32 CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "CompanyCode", DataType = "String")]
        public String CompanyCode
        {
            get { return _CompanyCode; }
            set { _CompanyCode = value; }
        }
        [Column(Name = "CompanyName", DataType = "String")]
        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        [Column(Name = "LOBClassID", DataType = "Int32")]
        public Int32 LOBClassID
        {
            get { return _LOBClassID; }
            set { _LOBClassID = value; }
        }
        [Column(Name = "LOBClassCode", DataType = "String")]
        public String LOBClassCode
        {
            get { return _LOBClassCode; }
            set { _LOBClassCode = value; }
        }
        [Column(Name = "LOBClassName", DataType = "String")]
        public String LOBClassName
        {
            get { return _LOBClassName; }
            set { _LOBClassName = value; }
        }
        [Column(Name = "RegionName", DataType = "String")]
        public String RegionName
        {
            get { return _RegionName; }
            set { _RegionName = value; }
        }
        [Column(Name = "GCDepartment", DataType = "String")]
        public String GCDepartment
        {
            get { return _GCDepartment; }
            set { _GCDepartment = value; }
        }
        [Column(Name = "Department", DataType = "String")]
        public String Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        [Column(Name = "Occupation", DataType = "String")]
        public String Occupation
        {
            get { return _Occupation; }
            set { _Occupation = value; }
        }
        [Column(Name = "GCOccupationLevel", DataType = "String")]
        public String GCOccupationLevel
        {
            get { return _GCOccupationLevel; }
            set { _GCOccupationLevel = value; }
        }
        [Column(Name = "OccupationLevel", DataType = "String")]
        public String OccupationLevel
        {
            get { return _OccupationLevel; }
            set { _OccupationLevel = value; }
        }
        [Column(Name = "AddressID", DataType = "Int32")]
        public Int32 AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "PhoneNo1", DataType = "String")]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "GCProvince", DataType = "String")]
        public String GCProvince
        {
            get { return _GCProvince; }
            set { _GCProvince = value; }
        }
        [Column(Name = "Province", DataType = "String")]
        public String Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        [Column(Name = "ZipCodeID", DataType = "Int32")]
        public Int32 ZipCodeID
        {
            get { return _ZipCodeID; }
            set { _ZipCodeID = value; }
        }
        [Column(Name = "ZipCode", DataType = "String")]
        public String ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        [Column(Name = "CompanyAddressID", DataType = "Int32")]
        public Int32 CompanyAddressID
        {
            get { return _CompanyAddressID; }
            set { _CompanyAddressID = value; }
        }
        [Column(Name = "CompanyPhoneNo1", DataType = "String")]
        public String CompanyPhoneNo1
        {
            get { return _CompanyPhoneNo1; }
            set { _CompanyPhoneNo1 = value; }
        }
        [Column(Name = "CompanyStreetName", DataType = "String")]
        public String CompanyStreetName
        {
            get { return _CompanyStreetName; }
            set { _CompanyStreetName = value; }
        }
        [Column(Name = "CompanyCounty", DataType = "String")]
        public String CompanyCounty
        {
            get { return _CompanyCounty; }
            set { _CompanyCounty = value; }
        }
        [Column(Name = "CompanyDistrict", DataType = "String")]
        public String CompanyDistrict
        {
            get { return _CompanyDistrict; }
            set { _CompanyDistrict = value; }
        }
        [Column(Name = "CompanyCity", DataType = "String")]
        public String CompanyCity
        {
            get { return _CompanyCity; }
            set { _CompanyCity = value; }
        }
        [Column(Name = "CompanyGCProvince", DataType = "String")]
        public String CompanyGCProvince
        {
            get { return _CompanyGCProvince; }
            set { _CompanyGCProvince = value; }
        }
        [Column(Name = "CompanyProvince", DataType = "String")]
        public String CompanyProvince
        {
            get { return _CompanyProvince; }
            set { _CompanyProvince = value; }
        }
        [Column(Name = "CompanyZipCodeID", DataType = "Int32")]
        public Int32 CompanyZipCodeID
        {
            get { return _CompanyZipCodeID; }
            set { _CompanyZipCodeID = value; }
        }
        [Column(Name = "CompanyZipCode", DataType = "String")]
        public String CompanyZipCode
        {
            get { return _CompanyZipCode; }
            set { _CompanyZipCode = value; }
        }
        [Column(Name = "EmailAddress1", DataType = "String")]
        public String EmailAddress1
        {
            get { return _EmailAddress1; }
            set { _EmailAddress1 = value; }
        }
        [Column(Name = "EmailAddress2", DataType = "String")]
        public String EmailAddress2
        {
            get { return _EmailAddress2; }
            set { _EmailAddress2 = value; }
        }
        [Column(Name = "MobilePhoneNo1", DataType = "String")]
        public String MobilePhoneNo1
        {
            get { return _MobilePhoneNo1; }
            set { _MobilePhoneNo1 = value; }
        }
        [Column(Name = "MobilePhoneNo2", DataType = "String")]
        public String MobilePhoneNo2
        {
            get { return _MobilePhoneNo2; }
            set { _MobilePhoneNo2 = value; }
        }
        [Column(Name = "OfficeExtensionNo", DataType = "String")]
        public String OfficeExtensionNo
        {
            get { return _OfficeExtensionNo; }
            set { _OfficeExtensionNo = value; }
        }
        [Column(Name = "VATRegistrationNo", DataType = "String")]
        public String VATRegistrationNo
        {
            get { return _VATRegistrationNo; }
            set { _VATRegistrationNo = value; }
        }
        [Column(Name = "LastEventDate", DataType = "DateTime")]
        public DateTime LastEventDate
        {
            get { return _LastEventDate; }
            set { _LastEventDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "LastEventID", DataType = "Int32")]
        public Int32 LastEventID
        {
            get { return _LastEventID; }
            set { _LastEventID = value; }
        }
        [Column(Name = "LastEventName", DataType = "String")]
        public String LastEventName
        {
            get { return _LastEventName; }
            set { _LastEventName = value; }
        }
        [Column(Name = "LastCompanyID", DataType = "Int32")]
        public Int32 LastCompanyID
        {
            get { return _LastCompanyID; }
            set { _LastCompanyID = value; }
        }
        [Column(Name = "LastCompanyName", DataType = "String")]
        public String LastCompanyName
        {
            get { return _LastCompanyName; }
            set { _LastCompanyName = value; }
        }
        [Column(Name = "NumberOfTraining", DataType = "Decimal")]
        public Decimal NumberOfTraining
        {
            get { return _NumberOfTraining; }
            set { _NumberOfTraining = value; }
        }
        [Column(Name = "PictureFileName", DataType = "String")]
        public String PictureFileName
        {
            get { return _PictureFileName; }
            set { _PictureFileName = value; }
        }
        [Column(Name = "IsCSClub", DataType = "Boolean")]
        public Boolean IsCSClub
        {
            get { return _IsCSClub; }
            set { _IsCSClub = value; }
        }
        [Column(Name = "IsHRDClub", DataType = "Boolean")]
        public Boolean IsHRDClub
        {
            get { return _IsHRDClub; }
            set { _IsHRDClub = value; }
        }
        [Column(Name = "IsISOClub", DataType = "Boolean")]
        public Boolean IsISOClub
        {
            get { return _IsISOClub; }
            set { _IsISOClub = value; }
        }
        [Column(Name = "IsCompanyContactPerson", DataType = "Boolean")]
        public Boolean IsCompanyContactPerson
        {
            get { return _IsCompanyContactPerson; }
            set { _IsCompanyContactPerson = value; }
        }
        [Column(Name = "SendGreetingsOn", DataType = "String")]
        public String SendGreetingsOn
        {
            get { return _SendGreetingsOn; }
            set { _SendGreetingsOn = value; }
        }
        [Column(Name = "RatingLevel", DataType = "Int16")]
        public Int16 RatingLevel
        {
            get { return _RatingLevel; }
            set { _RatingLevel = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedByUserName", DataType = "String")]
        public String CreatedByUserName
        {
            get { return _CreatedByUserName; }
            set { _CreatedByUserName = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedByUserName", DataType = "String")]
        public String LastUpdatedByUserName
        {
            get { return _LastUpdatedByUserName; }
            set { _LastUpdatedByUserName = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime")]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }
    #endregion
    #region vMemberGreetings
    [Serializable]
    [Table(Name = "vMemberGreetings")]
    public class vMemberGreetings
    {
        private Int32 _MemberID;
        private String _GCGreetingType;
        private String _GreetingType;

        [Column(Name = "MemberID", DataType = "Int32")]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "GCGreetingType", DataType = "String")]
        public String GCGreetingType
        {
            get { return _GCGreetingType; }
            set { _GCGreetingType = value; }
        }
        [Column(Name = "GreetingType", DataType = "String")]
        public String GreetingType
        {
            get { return _GreetingType; }
            set { _GreetingType = value; }
        }
    }
    #endregion
    #region vMemberPastTraining
    [Serializable]
    [Table(Name = "vMemberPastTraining")]
    public partial class vMemberPastTraining
    {
        private Int32 _ID;
        private Int32 _MemberID;
        private String _TrainingDate;
        private String _TrainingMonth;
        private String _TrainingYear;
        private Int16 _TrainingDuration;
        private Int32? _EventID;
        private String _EventCode;
        private String _EventName;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private Int32? _TrainingID;
        private String _TrainingCode;
        private String _TrainingName;
        private String _TrainerName;
        private String _VenueName;
        private String _VenueLocation;
        private String _StreetName;
        private String _County;
        private String _District;
        private String _City;
        private String _Province;
        private String _TrainerFirstName;
        private String _TrainerMiddleName;
        private String _TrainerLastName;
        private Boolean _IsFromMigration;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "MemberID", DataType = "Int32")]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "TrainingDate", DataType = "String")]
        public String TrainingDate
        {
            get { return _TrainingDate; }
            set { _TrainingDate = value; }
        }
        [Column(Name = "TrainingMonth", DataType = "String")]
        public String TrainingMonth
        {
            get { return _TrainingMonth; }
            set { _TrainingMonth = value; }
        }
        [Column(Name = "TrainingYear", DataType = "String")]
        public String TrainingYear
        {
            get { return _TrainingYear; }
            set { _TrainingYear = value; }
        }
        [Column(Name = "TrainingDuration", DataType = "Int16")]
        public Int16 TrainingDuration
        {
            get { return _TrainingDuration; }
            set { _TrainingDuration = value; }
        }
        [Column(Name = "EventID", DataType = "Int32", IsNullable = true)]
        public Int32? EventID
        {
            get { return _EventID; }
            set { _EventID = value; }
        }
        [Column(Name = "EventCode", DataType = "String")]
        public String EventCode
        {
            get { return _EventCode; }
            set { _EventCode = value; }
        }
        [Column(Name = "EventName", DataType = "String")]
        public String EventName
        {
            get { return _EventName; }
            set { _EventName = value; }
        }
        [Column(Name = "StartDate", DataType = "DateTime")]
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        [Column(Name = "EndDate", DataType = "DateTime")]
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        [Column(Name = "TrainingID", DataType = "Int32", IsNullable = true)]
        public Int32? TrainingID
        {
            get { return _TrainingID; }
            set { _TrainingID = value; }
        }
        [Column(Name = "TrainingCode", DataType = "String")]
        public String TrainingCode
        {
            get { return _TrainingCode; }
            set { _TrainingCode = value; }
        }
        [Column(Name = "TrainingName", DataType = "String")]
        public String TrainingName
        {
            get { return _TrainingName; }
            set { _TrainingName = value; }
        }
        [Column(Name = "TrainerName", DataType = "String")]
        public String TrainerName
        {
            get { return _TrainerName; }
            set { _TrainerName = value; }
        }
        [Column(Name = "VenueName", DataType = "String")]
        public String VenueName
        {
            get { return _VenueName; }
            set { _VenueName = value; }
        }
        [Column(Name = "VenueLocation", DataType = "String")]
        public String VenueLocation
        {
            get { return _VenueLocation; }
            set { _VenueLocation = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "Province", DataType = "String")]
        public String Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        [Column(Name = "TrainerFirstName", DataType = "String")]
        public String TrainerFirstName
        {
            get { return _TrainerFirstName; }
            set { _TrainerFirstName = value; }
        }
        [Column(Name = "TrainerMiddleName", DataType = "String")]
        public String TrainerMiddleName
        {
            get { return _TrainerMiddleName; }
            set { _TrainerMiddleName = value; }
        }
        [Column(Name = "TrainerLastName", DataType = "String")]
        public String TrainerLastName
        {
            get { return _TrainerLastName; }
            set { _TrainerLastName = value; }
        }
        [Column(Name = "IsFromMigration", DataType = "Boolean")]
        public Boolean IsFromMigration
        {
            get { return _IsFromMigration; }
            set { _IsFromMigration = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vMenu
    [Serializable]
    [Table(Name = "vMenu")]
    public class vMenu
    {
        private Int32 _MenuID;
        private String _MenuCode;
        private String _ModuleID;
        private String _MenuCaption;
        private String _MenuUrl;
        private Int16 _MenuLevel;
        private Int16 _MenuIndex;
        private String _MenuTooltip;
        private Int32 _ParentID;
        private String _CRUDMode;
        private String _ImageUrl;
        private Int32 _Level;

        [Column(Name = "MenuID", DataType = "Int32")]
        public Int32 MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }
        [Column(Name = "MenuCode", DataType = "String")]
        public String MenuCode
        {
            get { return _MenuCode; }
            set { _MenuCode = value; }
        }
        [Column(Name = "ModuleID", DataType = "String")]
        public String ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        [Column(Name = "MenuCaption", DataType = "String")]
        public String MenuCaption
        {
            get { return _MenuCaption; }
            set { _MenuCaption = value; }
        }
        [Column(Name = "MenuUrl", DataType = "String")]
        public String MenuUrl
        {
            get { return _MenuUrl; }
            set { _MenuUrl = value; }
        }
        [Column(Name = "MenuLevel", DataType = "Int16")]
        public Int16 MenuLevel
        {
            get { return _MenuLevel; }
            set { _MenuLevel = value; }
        }
        [Column(Name = "MenuIndex", DataType = "Int16")]
        public Int16 MenuIndex
        {
            get { return _MenuIndex; }
            set { _MenuIndex = value; }
        }
        [Column(Name = "MenuTooltip", DataType = "String")]
        public String MenuTooltip
        {
            get { return _MenuTooltip; }
            set { _MenuTooltip = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32")]
        public Int32 ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "CRUDMode", DataType = "String")]
        public String CRUDMode
        {
            get { return _CRUDMode; }
            set { _CRUDMode = value; }
        }
        [Column(Name = "ImageUrl", DataType = "String")]
        public String ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }
        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
    }
    #endregion
    #region vOccupation
    [Serializable]
    [Table(Name = "vOccupation")]
    public class vOccupation
    {
        private Int32 _OccupationID;
        private String _OccupationCode;
        private String _OccupationName;
        private String _GCOccupationLevel;
        private String _OccupationLevel;
        private Boolean _IsDeleted;

        [Column(Name = "OccupationID", DataType = "Int32")]
        public Int32 OccupationID
        {
            get { return _OccupationID; }
            set { _OccupationID = value; }
        }
        [Column(Name = "OccupationCode", DataType = "String")]
        public String OccupationCode
        {
            get { return _OccupationCode; }
            set { _OccupationCode = value; }
        }
        [Column(Name = "OccupationName", DataType = "String")]
        public String OccupationName
        {
            get { return _OccupationName; }
            set { _OccupationName = value; }
        }
        [Column(Name = "GCOccupationLevel", DataType = "String")]
        public String GCOccupationLevel
        {
            get { return _GCOccupationLevel; }
            set { _GCOccupationLevel = value; }
        }
        [Column(Name = "OccupationLevel", DataType = "String")]
        public String OccupationLevel
        {
            get { return _OccupationLevel; }
            set { _OccupationLevel = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vReportMaster
    [Serializable]
    [Table(Name = "vReportMaster")]
    public class vReportMaster
    {
        private Int32 _ReportID;
        private String _ReportCode;
        private String _ReportTitle1;
        private String _ReportTitle2;
        private String _GCReportType;
        private String _ReportType;
        private String _ClassName;
        private String _GCDataSourceType;
        private String _DataSourceType;
        private String _ObjectTypeName;
        private String _AdditionalFilterExpression;
        private Boolean _IsDeleted;

        [Column(Name = "ReportID", DataType = "Int32")]
        public Int32 ReportID
        {
            get { return _ReportID; }
            set { _ReportID = value; }
        }
        [Column(Name = "ReportCode", DataType = "String")]
        public String ReportCode
        {
            get { return _ReportCode; }
            set { _ReportCode = value; }
        }
        [Column(Name = "ReportTitle1", DataType = "String")]
        public String ReportTitle1
        {
            get { return _ReportTitle1; }
            set { _ReportTitle1 = value; }
        }
        [Column(Name = "ReportTitle2", DataType = "String")]
        public String ReportTitle2
        {
            get { return _ReportTitle2; }
            set { _ReportTitle2 = value; }
        }
        [Column(Name = "GCReportType", DataType = "String")]
        public String GCReportType
        {
            get { return _GCReportType; }
            set { _GCReportType = value; }
        }
        [Column(Name = "ReportType", DataType = "String")]
        public String ReportType
        {
            get { return _ReportType; }
            set { _ReportType = value; }
        }
        [Column(Name = "ClassName", DataType = "String")]
        public String ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }
        [Column(Name = "GCDataSourceType", DataType = "String")]
        public String GCDataSourceType
        {
            get { return _GCDataSourceType; }
            set { _GCDataSourceType = value; }
        }
        [Column(Name = "DataSourceType", DataType = "String")]
        public String DataSourceType
        {
            get { return _DataSourceType; }
            set { _DataSourceType = value; }
        }
        [Column(Name = "ObjectTypeName", DataType = "String")]
        public String ObjectTypeName
        {
            get { return _ObjectTypeName; }
            set { _ObjectTypeName = value; }
        }
        [Column(Name = "AdditionalFilterExpression", DataType = "String")]
        public String AdditionalFilterExpression
        {
            get { return _AdditionalFilterExpression; }
            set { _AdditionalFilterExpression = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vReportParameter
    [Serializable]
    [Table(Name = "vReportParameter")]
    public class vReportParameter
    {
        private Int32 _ReportID;
        private Int32 _FilterParameterID;
        private String _FilterParameterCode;
        private String _FilterParameterName;
        private String _ControlName;
        private String _FilterParameterCaption;
        private String _GCFilterParameterType;
        private String _MethodName;
        private String _FilterExpression;
        private String _ValueFieldName;
        private String _TextFieldName;
        private String _FieldName;
        private Boolean _IsDeleted;
        private Int16 _DisplayOrder;

        [Column(Name = "ReportID", DataType = "Int32")]
        public Int32 ReportID
        {
            get { return _ReportID; }
            set { _ReportID = value; }
        }
        [Column(Name = "FilterParameterID", DataType = "Int32")]
        public Int32 FilterParameterID
        {
            get { return _FilterParameterID; }
            set { _FilterParameterID = value; }
        }
        [Column(Name = "FilterParameterCode", DataType = "String")]
        public String FilterParameterCode
        {
            get { return _FilterParameterCode; }
            set { _FilterParameterCode = value; }
        }
        [Column(Name = "FilterParameterName", DataType = "String")]
        public String FilterParameterName
        {
            get { return _FilterParameterName; }
            set { _FilterParameterName = value; }
        }
        [Column(Name = "ControlName", DataType = "String")]
        public String ControlName
        {
            get { return _ControlName; }
            set { _ControlName = value; }
        }
        [Column(Name = "FilterParameterCaption", DataType = "String")]
        public String FilterParameterCaption
        {
            get { return _FilterParameterCaption; }
            set { _FilterParameterCaption = value; }
        }
        [Column(Name = "GCFilterParameterType", DataType = "String")]
        public String GCFilterParameterType
        {
            get { return _GCFilterParameterType; }
            set { _GCFilterParameterType = value; }
        }
        [Column(Name = "MethodName", DataType = "String")]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String")]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "ValueFieldName", DataType = "String")]
        public String ValueFieldName
        {
            get { return _ValueFieldName; }
            set { _ValueFieldName = value; }
        }
        [Column(Name = "TextFieldName", DataType = "String")]
        public String TextFieldName
        {
            get { return _TextFieldName; }
            set { _TextFieldName = value; }
        }
        [Column(Name = "FieldName", DataType = "String")]
        public String FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
    }
    #endregion
    #region vTemplateText
    [Serializable]
    [Table(Name = "vTemplateText")]
    public class vTemplateText
    {
        private Int32 _TemplateID;
        private String _TemplateCode;
        private String _TemplateName;
        private String _GCTemplateGroup;
        private String _TemplateGroup;
        private String _TemplateContent;
        private Boolean _IsDeleted;

        [Column(Name = "TemplateID", DataType = "Int32")]
        public Int32 TemplateID
        {
            get { return _TemplateID; }
            set { _TemplateID = value; }
        }
        [Column(Name = "TemplateCode", DataType = "String")]
        public String TemplateCode
        {
            get { return _TemplateCode; }
            set { _TemplateCode = value; }
        }
        [Column(Name = "TemplateName", DataType = "String")]
        public String TemplateName
        {
            get { return _TemplateName; }
            set { _TemplateName = value; }
        }
        [Column(Name = "GCTemplateGroup", DataType = "String")]
        public String GCTemplateGroup
        {
            get { return _GCTemplateGroup; }
            set { _GCTemplateGroup = value; }
        }
        [Column(Name = "TemplateGroup", DataType = "String")]
        public String TemplateGroup
        {
            get { return _TemplateGroup; }
            set { _TemplateGroup = value; }
        }
        [Column(Name = "TemplateContent", DataType = "String")]
        public String TemplateContent
        {
            get { return _TemplateContent; }
            set { _TemplateContent = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vTrainer
    [Serializable]
    [Table(Name = "vTrainer")]
    public partial class vTrainer
    {
        private Int32 _TrainerID;
        private String _TrainerCode;
        private String _GCSalutation;
        private String _Salutation;
        private String _GCTitle;
        private String _Title;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _GCSuffix;
        private String _Suffix;
        private String _EmailAddress;
        private String _MobilePhone1;
        private String _MobilePhone2;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "TrainerID", DataType = "Int32")]
        public Int32 TrainerID
        {
            get { return _TrainerID; }
            set { _TrainerID = value; }
        }
        [Column(Name = "TrainerCode", DataType = "String")]
        public String TrainerCode
        {
            get { return _TrainerCode; }
            set { _TrainerCode = value; }
        }
        [Column(Name = "GCSalutation", DataType = "String")]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "Salutation", DataType = "String")]
        public String Salutation
        {
            get { return _Salutation; }
            set { _Salutation = value; }
        }
        [Column(Name = "GCTitle", DataType = "String")]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "Title", DataType = "String")]
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        [Column(Name = "FirstName", DataType = "String")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String")]
        public String MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        [Column(Name = "LastName", DataType = "String")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Column(Name = "GCSuffix", DataType = "String")]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "Suffix", DataType = "String")]
        public String Suffix
        {
            get { return _Suffix; }
            set { _Suffix = value; }
        }
        [Column(Name = "EmailAddress", DataType = "String")]
        public String EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        [Column(Name = "MobilePhone1", DataType = "String")]
        public String MobilePhone1
        {
            get { return _MobilePhone1; }
            set { _MobilePhone1 = value; }
        }
        [Column(Name = "MobilePhone2", DataType = "String")]
        public String MobilePhone2
        {
            get { return _MobilePhone2; }
            set { _MobilePhone2 = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vTraining
    [Serializable]
    [Table(Name = "vTraining")]
    public class vTraining
    {
        private Int32 _TrainingID;
        private String _TrainingCode;
        private String _TrainingName;
        private String _GCTrainingType;
        private String _TrainingType;
        private Boolean _IsHasCertification;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "TrainingID", DataType = "Int32")]
        public Int32 TrainingID
        {
            get { return _TrainingID; }
            set { _TrainingID = value; }
        }
        [Column(Name = "TrainingCode", DataType = "String")]
        public String TrainingCode
        {
            get { return _TrainingCode; }
            set { _TrainingCode = value; }
        }
        [Column(Name = "TrainingName", DataType = "String")]
        public String TrainingName
        {
            get { return _TrainingName; }
            set { _TrainingName = value; }
        }
        [Column(Name = "GCTrainingType", DataType = "String")]
        public String GCTrainingType
        {
            get { return _GCTrainingType; }
            set { _GCTrainingType = value; }
        }
        [Column(Name = "TrainingType", DataType = "String")]
        public String TrainingType
        {
            get { return _TrainingType; }
            set { _TrainingType = value; }
        }
        [Column(Name = "IsHasCertification", DataType = "Boolean")]
        public Boolean IsHasCertification
        {
            get { return _IsHasCertification; }
            set { _IsHasCertification = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vTrainingCertification
    [Serializable]
    [Table(Name = "vTrainingCertification")]
    public class vTrainingCertification
    {
        private Int32 _TrainingID;
        private String _GCCompanyCertification;
        private String _CompanyCertification;

        [Column(Name = "TrainingID", DataType = "Int32")]
        public Int32 TrainingID
        {
            get { return _TrainingID; }
            set { _TrainingID = value; }
        }
        [Column(Name = "GCCompanyCertification", DataType = "String")]
        public String GCCompanyCertification
        {
            get { return _GCCompanyCertification; }
            set { _GCCompanyCertification = value; }
        }
        [Column(Name = "CompanyCertification", DataType = "String")]
        public String CompanyCertification
        {
            get { return _CompanyCertification; }
            set { _CompanyCertification = value; }
        }
    }
    #endregion
    #region vTrainingDepartment
    [Serializable]
    [Table(Name = "vTrainingDepartment")]
    public class vTrainingDepartment
    {
        private Int32 _TrainingID;
        private String _GCDepartment;
        private String _Department;

        [Column(Name = "TrainingID", DataType = "Int32")]
        public Int32 TrainingID
        {
            get { return _TrainingID; }
            set { _TrainingID = value; }
        }
        [Column(Name = "GCDepartment", DataType = "String")]
        public String GCDepartment
        {
            get { return _GCDepartment; }
            set { _GCDepartment = value; }
        }
        [Column(Name = "Department", DataType = "String")]
        public String Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
    }
    #endregion
    #region vTrainingOccupation
    [Serializable]
    [Table(Name = "vTrainingOccupation")]
    public class vTrainingOccupation
    {
        private Int32 _TrainingID;
        private String _GCOccupationLevel;
        private String _OccupationLevel;

        [Column(Name = "TrainingID", DataType = "Int32")]
        public Int32 TrainingID
        {
            get { return _TrainingID; }
            set { _TrainingID = value; }
        }
        [Column(Name = "GCOccupationLevel", DataType = "String")]
        public String GCOccupationLevel
        {
            get { return _GCOccupationLevel; }
            set { _GCOccupationLevel = value; }
        }
        [Column(Name = "OccupationLevel", DataType = "String")]
        public String OccupationLevel
        {
            get { return _OccupationLevel; }
            set { _OccupationLevel = value; }
        }
    }
    #endregion
    #region vUserInRole
    [Serializable]
    [Table(Name = "vUserInRole")]
    public class vUserInRole
    {
        private Int32 _UserID;
        private String _UserName;
        private Int32 _RoleID;
        private String _RoleName;

        [Column(Name = "UserID", DataType = "Int32")]
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        [Column(Name = "UserName", DataType = "String")]
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        [Column(Name = "RoleID", DataType = "Int32")]
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        [Column(Name = "RoleName", DataType = "String")]
        public String RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
    }
    #endregion
    #region vUserMenu
    [Serializable]
    [Table(Name = "vUserMenu")]
    public class vUserMenu
    {
        private Int32 _ID;
        private Int32 _MenuID;
        private String _ModuleID;
        private String _MenuCode;
        private String _MenuCaption;
        private Int16 _MenuIndex;
        private Int32? _ParentID;
        private String _MenuUrl;
        private String _ImageUrl;
        private Int16 _MenuLevel;
        private Boolean _IsVisible;
        private Int32 _RoleID;
        private Int32 _UserID;
        private String _CRUDMode;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "MenuID", DataType = "Int32")]
        public Int32 MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }
        [Column(Name = "ModuleID", DataType = "String")]
        public String ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        [Column(Name = "MenuCode", DataType = "String")]
        public String MenuCode
        {
            get { return _MenuCode; }
            set { _MenuCode = value; }
        }
        [Column(Name = "MenuCaption", DataType = "String")]
        public String MenuCaption
        {
            get { return _MenuCaption; }
            set { _MenuCaption = value; }
        }
        [Column(Name = "MenuIndex", DataType = "Int16")]
        public Int16 MenuIndex
        {
            get { return _MenuIndex; }
            set { _MenuIndex = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32")]
        public Int32? ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "MenuUrl", DataType = "String")]
        public String MenuUrl
        {
            get { return _MenuUrl; }
            set { _MenuUrl = value; }
        }
        [Column(Name = "ImageUrl", DataType = "String")]
        public String ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }
        [Column(Name = "MenuLevel", DataType = "Int16")]
        public Int16 MenuLevel
        {
            get { return _MenuLevel; }
            set { _MenuLevel = value; }
        }
        [Column(Name = "IsVisible", DataType = "Boolean")]
        public Boolean IsVisible
        {
            get { return _IsVisible; }
            set { _IsVisible = value; }
        }
        [Column(Name = "RoleID", DataType = "Int32")]
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        [Column(Name = "UserID", DataType = "Int32")]
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        [Column(Name = "CRUDMode", DataType = "String")]
        public String CRUDMode
        {
            get { return _CRUDMode; }
            set { _CRUDMode = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vVenue
    [Serializable]
    [Table(Name = "vVenue")]
    public partial class vVenue
    {
        private Int32 _VenueID;
        private String _VenueCode;
        private String _VenueName;
        private Int32 _AddressID;
        private String _PhoneNo1;
        private String _PhoneNo2;
        private String _FaxNo1;
        private String _FaxNo2;
        private String _StreetName;
        private String _County;
        private String _District;
        private String _City;
        private String _GCProvince;
        private String _Province;
        private Int32 _ZipCodeID;
        private String _ZipCode;
        private String _Remarks;
        private Boolean _IsDeleted;

        [Column(Name = "VenueID", DataType = "Int32")]
        public Int32 VenueID
        {
            get { return _VenueID; }
            set { _VenueID = value; }
        }
        [Column(Name = "VenueCode", DataType = "String")]
        public String VenueCode
        {
            get { return _VenueCode; }
            set { _VenueCode = value; }
        }
        [Column(Name = "VenueName", DataType = "String")]
        public String VenueName
        {
            get { return _VenueName; }
            set { _VenueName = value; }
        }
        [Column(Name = "AddressID", DataType = "Int32")]
        public Int32 AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "PhoneNo1", DataType = "String")]
        public String PhoneNo1
        {
            get { return _PhoneNo1; }
            set { _PhoneNo1 = value; }
        }
        [Column(Name = "PhoneNo2", DataType = "String")]
        public String PhoneNo2
        {
            get { return _PhoneNo2; }
            set { _PhoneNo2 = value; }
        }
        [Column(Name = "FaxNo1", DataType = "String")]
        public String FaxNo1
        {
            get { return _FaxNo1; }
            set { _FaxNo1 = value; }
        }
        [Column(Name = "FaxNo2", DataType = "String")]
        public String FaxNo2
        {
            get { return _FaxNo2; }
            set { _FaxNo2 = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "GCProvince", DataType = "String")]
        public String GCProvince
        {
            get { return _GCProvince; }
            set { _GCProvince = value; }
        }
        [Column(Name = "Province", DataType = "String")]
        public String Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        [Column(Name = "ZipCodeID", DataType = "Int32")]
        public Int32 ZipCodeID
        {
            get { return _ZipCodeID; }
            set { _ZipCodeID = value; }
        }
        [Column(Name = "ZipCode", DataType = "String")]
        public String ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }
        [Column(Name = "Remarks", DataType = "String")]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #region vZipCodes
    [Serializable]
    [Table(Name = "vZipCodes")]
    public partial class vZipCodes
    {
        private Int32 _ID;
        private String _ZIpCode;
        private String _StreetName;
        private String _District;
        private String _County;
        private String _City;
        private String _GCProvince;
        private String _Province;
        private Decimal _Longitude;
        private Decimal _Latitude;
        private Boolean _IsDeleted;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "ZIpCode", DataType = "String")]
        public String ZIpCode
        {
            get { return _ZIpCode; }
            set { _ZIpCode = value; }
        }
        [Column(Name = "StreetName", DataType = "String")]
        public String StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }
        [Column(Name = "District", DataType = "String")]
        public String District
        {
            get { return _District; }
            set { _District = value; }
        }
        [Column(Name = "County", DataType = "String")]
        public String County
        {
            get { return _County; }
            set { _County = value; }
        }
        [Column(Name = "City", DataType = "String")]
        public String City
        {
            get { return _City; }
            set { _City = value; }
        }
        [Column(Name = "GCProvince", DataType = "String")]
        public String GCProvince
        {
            get { return _GCProvince; }
            set { _GCProvince = value; }
        }
        [Column(Name = "Province", DataType = "String")]
        public String Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        [Column(Name = "Longitude", DataType = "Decimal")]
        public Decimal Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }
        [Column(Name = "Latitude", DataType = "Decimal")]
        public Decimal Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
    }
    #endregion
    #endregion
}
