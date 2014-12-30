using System;
using System.Data;
using QIS.Data.Core.Dal;
namespace QIS.Careq.Data.Service
{
    #region MEMENTO Tables
    #region Address
    [Serializable]
    [Table(Name = "Address")]
    public class Address : DbDataModel
    {
        private Int32 _AddressID;
        private String _StreetName;
        private String _District;
        private String _City;
        private String _County;
        private String _GCProvince;
        private Int32? _ZipCodeID;
        private String _PhoneNo1;
        private String _PhoneNo2;
        private String _FaxNo1;
        private String _FaxNo2;
        private Boolean _IsMailingAddress;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "AddressID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "GCProvince", DataType = "String", IsNullable = true)]
        public String GCProvince
        {
            get { return _GCProvince; }
            set { _GCProvince = value; }
        }
        [Column(Name = "ZipCodeID", DataType = "Int32", IsNullable = true)]
        public Int32? ZipCodeID
        {
            get { return _ZipCodeID; }
            set { _ZipCodeID = value; }
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class AddressDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Address));
        private bool _isAuditLog = false;
        private const string p_AddressID = "@p_AddressID";
        public AddressDao() { }
        public AddressDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Address Get(Int32 AddressID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_AddressID, AddressID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Address)_helper.DataRowToObject(row, new Address());
        }
        public int Insert(Address record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Address record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 AddressID)
        {
            Address record;
            if (_ctx.Transaction == null)
                record = new AddressDao().Get(AddressID);
            else
                record = Get(AddressID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Application
    [Serializable]
    [Table(Name = "Application")]
    public class Application : DbDataModel
    {
        private Int32 _ApplicationID;
        private String _ApplicationCode;
        private String _CompanyName;
        private String _ProductKey;

        [Column(Name = "ApplicationID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ApplicationID
        {
            get { return _ApplicationID; }
            set { _ApplicationID = value; }
        }
        [Column(Name = "ApplicationCode", DataType = "String")]
        public String ApplicationCode
        {
            get { return _ApplicationCode; }
            set { _ApplicationCode = value; }
        }
        [Column(Name = "CompanyName", DataType = "String")]
        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        [Column(Name = "ProductKey", DataType = "String")]
        public String ProductKey
        {
            get { return _ProductKey; }
            set { _ProductKey = value; }
        }
    }

    public class ApplicationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Application));
        private bool _isAuditLog = false;
        private const string p_ApplicationID = "@p_ApplicationID";
        public ApplicationDao() { }
        public ApplicationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Application Get(Int32 ApplicationID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ApplicationID, ApplicationID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Application)_helper.DataRowToObject(row, new Application());
        }
        public int Insert(Application record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Application record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ApplicationID)
        {
            Application record;
            if (_ctx.Transaction == null)
                record = new ApplicationDao().Get(ApplicationID);
            else
                record = Get(ApplicationID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Company
    [Serializable]
    [Table(Name = "Company")]
    public class Company : DbDataModel
    {
        private Int32 _CompanyID;
        private String _CompanyCode;
        private String _CompanyName;
        private String _ShortName;
        private Int32 _LOBClassID;
        private String _GCCompanyType;
        private String _GCCountryOfOrigin;
        private Int32? _ContactPersonID;
        private Int32? _HoldingCompanyID;
        private Int32? _RegionID;
        private Int32 _AddressID;
        private String _WebsiteUrl;
        private String _EmailAddress;
        private Boolean _IsTaxable;
        private String _VATRegistrationNo;
        private Int16 _RatingLevel;
        private Boolean _IsGoPublicCompany;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "CompanyID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "GCCompanyType", DataType = "String")]
        public String GCCompanyType
        {
            get { return _GCCompanyType; }
            set { _GCCompanyType = value; }
        }
        [Column(Name = "GCCountryOfOrigin", DataType = "String", IsNullable = true)]
        public String GCCountryOfOrigin
        {
            get { return _GCCountryOfOrigin; }
            set { _GCCountryOfOrigin = value; }
        }
        [Column(Name = "ContactPersonID", DataType = "Int32", IsNullable = true)]
        public Int32? ContactPersonID
        {
            get { return _ContactPersonID; }
            set { _ContactPersonID = value; }
        }
        [Column(Name = "HoldingCompanyID", DataType = "Int32", IsNullable = true)]
        public Int32? HoldingCompanyID
        {
            get { return _HoldingCompanyID; }
            set { _HoldingCompanyID = value; }
        }
        [Column(Name = "RegionID", DataType = "Int32", IsNullable = true)]
        public Int32? RegionID
        {
            get { return _RegionID; }
            set { _RegionID = value; }
        }
        [Column(Name = "AddressID", DataType = "Int32")]
        public Int32 AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "WebsiteUrl", DataType = "String", IsNullable = true)]
        public String WebsiteUrl
        {
            get { return _WebsiteUrl; }
            set { _WebsiteUrl = value; }
        }
        [Column(Name = "EmailAddress", DataType = "String", IsNullable = true)]
        public String EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        [Column(Name = "IsTaxable", DataType = "Boolean", IsNullable = true)]
        public Boolean IsTaxable
        {
            get { return _IsTaxable; }
            set { _IsTaxable = value; }
        }
        [Column(Name = "VATRegistrationNo", DataType = "String", IsNullable = true)]
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
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class CompanyDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Company));
        private bool _isAuditLog = false;
        private const string p_CompanyID = "@p_CompanyID";
        public CompanyDao() { }
        public CompanyDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Company Get(Int32 CompanyID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_CompanyID, CompanyID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Company)_helper.DataRowToObject(row, new Company());
        }
        public int Insert(Company record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Company record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 CompanyID)
        {
            Company record;
            if (_ctx.Transaction == null)
                record = new CompanyDao().Get(CompanyID);
            else
                record = Get(CompanyID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region CompanyCertification
    [Serializable]
    [Table(Name = "CompanyCertification")]
    public class CompanyCertification : DbDataModel
    {
        private Int32 _CompanyID;
        private String _GCCompanyCertification;

        [Column(Name = "CompanyID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "GCCompanyCertification", DataType = "String", IsPrimaryKey = true)]
        public String GCCompanyCertification
        {
            get { return _GCCompanyCertification; }
            set { _GCCompanyCertification = value; }
        }
    }

    public class CompanyCertificationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(CompanyCertification));
        private bool _isAuditLog = false;
        private const string p_CompanyID = "@p_CompanyID";
        private const string p_GCCompanyCertification = "@p_GCCompanyCertification";
        public CompanyCertificationDao() { }
        public CompanyCertificationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public CompanyCertification Get(Int32 CompanyID, String GCCompanyCertification)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_CompanyID, CompanyID);
            _ctx.Add(p_GCCompanyCertification, GCCompanyCertification);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (CompanyCertification)_helper.DataRowToObject(row, new CompanyCertification());
        }
        public int Insert(CompanyCertification record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(CompanyCertification record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 CompanyID, String GCCompanyCertification)
        {
            CompanyCertification record;
            if (_ctx.Transaction == null)
                record = new CompanyCertificationDao().Get(CompanyID, GCCompanyCertification);
            else
                record = Get(CompanyID, GCCompanyCertification);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region CompanyContactPerson
    [Serializable]
    [Table(Name = "CompanyContactPerson")]
    public class CompanyContactPerson : DbDataModel
    {
        private Int32 _ID;
        private Int32 _CompanyID;
        private String _GCDepartment;
        private Int32 _MemberID;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "MemberID", DataType = "Int32")]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class CompanyContactPersonDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(CompanyContactPerson));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public CompanyContactPersonDao() { }
        public CompanyContactPersonDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public CompanyContactPerson Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (CompanyContactPerson)_helper.DataRowToObject(row, new CompanyContactPerson());
        }
        public int Insert(CompanyContactPerson record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(CompanyContactPerson record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            CompanyContactPerson record;
            if (_ctx.Transaction == null)
                record = new CompanyContactPersonDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Employee
    [Serializable]
    [Table(Name = "Employee")]
    public class Employee : DbDataModel
    {
        private Int32 _EmployeeID;
        private String _EmployeeCode;
        private String _GCSalutation;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _GCSuffix;
        private String _EmailAddress;
        private String _MobilePhone1;
        private String _MobilePhone2;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "EmployeeID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "GCSalutation", DataType = "String", IsNullable = true)]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "GCTitle", DataType = "String", IsNullable = true)]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "FirstName", DataType = "String", IsNullable = true)]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String", IsNullable = true)]
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
        [Column(Name = "GCSuffix", DataType = "String", IsNullable = true)]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "EmailAddress", DataType = "String", IsNullable = true)]
        public String EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        [Column(Name = "MobilePhone1", DataType = "String", IsNullable = true)]
        public String MobilePhone1
        {
            get { return _MobilePhone1; }
            set { _MobilePhone1 = value; }
        }
        [Column(Name = "MobilePhone2", DataType = "String", IsNullable = true)]
        public String MobilePhone2
        {
            get { return _MobilePhone2; }
            set { _MobilePhone2 = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class EmployeeDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Employee));
        private bool _isAuditLog = false;
        private const string p_EmployeeID = "@p_EmployeeID";
        public EmployeeDao() { }
        public EmployeeDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Employee Get(Int32 EmployeeID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_EmployeeID, EmployeeID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Employee)_helper.DataRowToObject(row, new Employee());
        }
        public int Insert(Employee record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Employee record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 EmployeeID)
        {
            Employee record;
            if (_ctx.Transaction == null)
                record = new EmployeeDao().Get(EmployeeID);
            else
                record = Get(EmployeeID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Event
    [Serializable]
    [Table(Name = "Event")]
    public class Event : DbDataModel
    {
        private Int32 _EventID;
        private String _EventCode;
        private String _EventName;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private String _StartTime;
        private String _EndTime;
        private Int32? _VenueID;
        private String _RoomName;
        private Int32 _TrainingID;
        private Int32 _TrainerID;
        private String _AssistantTrainer;
        private Decimal _Price;
        private String _DefaultEmailText1;
        private String _DefaultEmailText2;
        private String _DefaultEmailReminderText;
        private String _Remarks;
        private String _GCEventStatus;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "EventID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "EventName", DataType = "String", IsNullable = true)]
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
        [Column(Name = "VenueID", DataType = "Int32", IsNullable = true)]
        public Int32? VenueID
        {
            get { return _VenueID; }
            set { _VenueID = value; }
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
        [Column(Name = "TrainerID", DataType = "Int32")]
        public Int32 TrainerID
        {
            get { return _TrainerID; }
            set { _TrainerID = value; }
        }
        [Column(Name = "AssistantTrainer", DataType = "String", IsNullable = true)]
        public String AssistantTrainer
        {
            get { return _AssistantTrainer; }
            set { _AssistantTrainer = value; }
        }
        [Column(Name = "Price", DataType = "Decimal", IsNullable = true)]
        public Decimal Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        [Column(Name = "DefaultEmailText1", DataType = "String", IsNullable = true)]
        public String DefaultEmailText1
        {
            get { return _DefaultEmailText1; }
            set { _DefaultEmailText1 = value; }
        }
        [Column(Name = "DefaultEmailText2", DataType = "String", IsNullable = true)]
        public String DefaultEmailText2
        {
            get { return _DefaultEmailText2; }
            set { _DefaultEmailText2 = value; }
        }
        [Column(Name = "DefaultEmailReminderText", DataType = "String", IsNullable = true)]
        public String DefaultEmailReminderText
        {
            get { return _DefaultEmailReminderText; }
            set { _DefaultEmailReminderText = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCEventStatus", DataType = "String", IsNullable = true)]
        public String GCEventStatus
        {
            get { return _GCEventStatus; }
            set { _GCEventStatus = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class EventDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Event));
        private bool _isAuditLog = false;
        private const string p_EventID = "@p_EventID";
        public EventDao() { }
        public EventDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Event Get(Int32 EventID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_EventID, EventID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Event)_helper.DataRowToObject(row, new Event());
        }
        public int Insert(Event record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Event record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 EventID)
        {
            Event record;
            if (_ctx.Transaction == null)
                record = new EventDao().Get(EventID);
            else
                record = Get(EventID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region EventCompanyDt
    [Serializable]
    [Table(Name = "EventCompanyDt")]
    public class EventCompanyDt : DbDataModel
    {
        private Int32 _EventID;
        private Int32 _CompanyID;
        private Int32 _PICMemberID;
        private DateTime _PaymentDate;
        private Decimal? _Price;
        private Decimal? _DiscountAmount;
        private Boolean _IsFinalDiscount;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "EventID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 EventID
        {
            get { return _EventID; }
            set { _EventID = value; }
        }
        [Column(Name = "CompanyID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "PICMemberID", DataType = "Int32")]
        public Int32 PICMemberID
        {
            get { return _PICMemberID; }
            set { _PICMemberID = value; }
        }
        [Column(Name = "PaymentDate", DataType = "DateTime", IsNullable = true)]
        public DateTime PaymentDate
        {
            get { return _PaymentDate; }
            set { _PaymentDate = value; }
        }
        [Column(Name = "Price", DataType = "Decimal", IsNullable = true)]
        public Decimal? Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        [Column(Name = "DiscountAmount", DataType = "Decimal", IsNullable = true)]
        public Decimal? DiscountAmount
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class EventCompanyDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(EventCompanyDt));
        private bool _isAuditLog = false;
        private const string p_CompanyID = "@p_CompanyID";
        private const string p_EventID = "@p_EventID";
        public EventCompanyDtDao() { }
        public EventCompanyDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public EventCompanyDt Get(Int32 EventID, Int32 CompanyID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_CompanyID, CompanyID);
            _ctx.Add(p_EventID, EventID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (EventCompanyDt)_helper.DataRowToObject(row, new EventCompanyDt());
        }
        public int Insert(EventCompanyDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(EventCompanyDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 EventID, Int32 CompanyID)
        {
            EventCompanyDt record;
            if (_ctx.Transaction == null)
                record = new EventCompanyDtDao().Get(EventID, CompanyID);
            else
                record = Get(EventID, CompanyID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region EventInvitation
    [Serializable]
    [Table(Name = "EventInvitation")]
    public class EventInvitation : DbDataModel
    {
        private Int32 _EventID;
        private Int32 _MemberID;
        private Int32 _CompanyID;
        private Boolean _IsConfirmed;
        private DateTime _ConfirmedDate;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "EventID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 EventID
        {
            get { return _EventID; }
            set { _EventID = value; }
        }
        [Column(Name = "MemberID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "CompanyID", DataType = "Int32")]
        public Int32 CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "IsConfirmed", DataType = "Boolean")]
        public Boolean IsConfirmed
        {
            get { return _IsConfirmed; }
            set { _IsConfirmed = value; }
        }
        [Column(Name = "ConfirmedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime ConfirmedDate
        {
            get { return _ConfirmedDate; }
            set { _ConfirmedDate = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class EventInvitationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(EventInvitation));
        private bool _isAuditLog = false;
        private const string p_EventID = "@p_EventID";
        private const string p_MemberID = "@p_MemberID";
        public EventInvitationDao() { }
        public EventInvitationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public EventInvitation Get(Int32 EventID, Int32 MemberID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_EventID, EventID);
            _ctx.Add(p_MemberID, MemberID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (EventInvitation)_helper.DataRowToObject(row, new EventInvitation());
        }
        public int Insert(EventInvitation record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(EventInvitation record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 EventID, Int32 MemberID)
        {
            EventInvitation record;
            if (_ctx.Transaction == null)
                record = new EventInvitationDao().Get(EventID, MemberID);
            else
                record = Get(EventID, MemberID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region EventRegistration
    [Serializable]
    [Table(Name = "EventRegistration")]
    public class EventRegistration : DbDataModel
    {
        private Int32 _EventID;
        private Int32 _MemberID;
        private Int32? _CompanyID;
        private String _Occupation;
        private String _GCOccupationLevel;
        private String _GCRegistrationType;
        private String _GCInformationSource;
        private String _GCRegistrationStatus;
        private String _CertificationNo;
        private Int16 _CertificatePrintNo;
        private String _Remarks;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "EventID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 EventID
        {
            get { return _EventID; }
            set { _EventID = value; }
        }
        [Column(Name = "MemberID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "CompanyID", DataType = "Int32", IsNullable = true)]
        public Int32? CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "Occupation", DataType = "String")]
        public String Occupation
        {
            get { return _Occupation; }
            set { _Occupation = value; }
        }
        [Column(Name = "GCOccupationLevel", DataType = "String", IsNullable = true)]
        public String GCOccupationLevel
        {
            get { return _GCOccupationLevel; }
            set { _GCOccupationLevel = value; }
        }
        [Column(Name = "GCRegistrationType", DataType = "String")]
        public String GCRegistrationType
        {
            get { return _GCRegistrationType; }
            set { _GCRegistrationType = value; }
        }
        [Column(Name = "GCInformationSource", DataType = "String")]
        public String GCInformationSource
        {
            get { return _GCInformationSource; }
            set { _GCInformationSource = value; }
        }
        [Column(Name = "GCRegistrationStatus", DataType = "String")]
        public String GCRegistrationStatus
        {
            get { return _GCRegistrationStatus; }
            set { _GCRegistrationStatus = value; }
        }
        [Column(Name = "CertificationNo", DataType = "String", IsNullable = true)]
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
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class EventRegistrationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(EventRegistration));
        private bool _isAuditLog = false;
        private const string p_EventID = "@p_EventID";
        private const string p_MemberID = "@p_MemberID";
        public EventRegistrationDao() { }
        public EventRegistrationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public EventRegistration Get(Int32 EventID, Int32 MemberID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_EventID, EventID);
            _ctx.Add(p_MemberID, MemberID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (EventRegistration)_helper.DataRowToObject(row, new EventRegistration());
        }
        public int Insert(EventRegistration record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(EventRegistration record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 EventID, Int32 MemberID)
        {
            EventRegistration record;
            if (_ctx.Transaction == null)
                record = new EventRegistrationDao().Get(EventID, MemberID);
            else
                record = Get(EventID, MemberID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region FilterParameter
    [Serializable]
    [Table(Name = "FilterParameter")]
    public class FilterParameter : DbDataModel
    {
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
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "FilterParameterID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "FilterExpression", DataType = "String", IsNullable = true)]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "ValueFieldName", DataType = "String", IsNullable = true)]
        public String ValueFieldName
        {
            get { return _ValueFieldName; }
            set { _ValueFieldName = value; }
        }
        [Column(Name = "TextFieldName", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class FilterParameterDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(FilterParameter));
        private bool _isAuditLog = false;
        private const string p_FilterParameterID = "@p_FilterParameterID";
        public FilterParameterDao() { }
        public FilterParameterDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public FilterParameter Get(Int32 FilterParameterID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_FilterParameterID, FilterParameterID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (FilterParameter)_helper.DataRowToObject(row, new FilterParameter());
        }
        public int Insert(FilterParameter record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(FilterParameter record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 FilterParameterID)
        {
            FilterParameter record;
            if (_ctx.Transaction == null)
                record = new FilterParameterDao().Get(FilterParameterID);
            else
                record = Get(FilterParameterID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Inquiry
    [Serializable]
    [Table(Name = "Inquiry")]
    public class Inquiry : DbDataModel
    {
        private Int32 _InquiryID;
        private String _InquiryNo;
        private DateTime _InquiryDate;
        private Int32? _LeadID;
        private Int32 _CompanyID;
        private Int32 _MemberID;
        private Int32 _PIC_CRO;
        private Int32? _PIC_TrainerID;
        private String _Subject;
        private String _Remarks;
        private String _GCItemType;
        private Int32? _ItemID;
        private String _GCInquiryStatus;
        private String _GCInquiryProcessType;
        private String _OtherInquiryProcessType;
        private String _GCCloseReason;
        private String _OtherCloseReasonText;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "InquiryID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "LeadID", DataType = "Int32", IsNullable = true)]
        public Int32? LeadID
        {
            get { return _LeadID; }
            set { _LeadID = value; }
        }
        [Column(Name = "CompanyID", DataType = "Int32")]
        public Int32 CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "MemberID", DataType = "Int32")]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "PIC_CRO", DataType = "Int32")]
        public Int32 PIC_CRO
        {
            get { return _PIC_CRO; }
            set { _PIC_CRO = value; }
        }
        [Column(Name = "PIC_TrainerID", DataType = "Int32", IsNullable = true)]
        public Int32? PIC_TrainerID
        {
            get { return _PIC_TrainerID; }
            set { _PIC_TrainerID = value; }
        }
        [Column(Name = "Subject", DataType = "String")]
        public String Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "GCItemType", DataType = "String", IsNullable = true)]
        public String GCItemType
        {
            get { return _GCItemType; }
            set { _GCItemType = value; }
        }
        [Column(Name = "ItemID", DataType = "Int32", IsNullable = true)]
        public Int32? ItemID
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
        [Column(Name = "GCInquiryProcessType", DataType = "String", IsNullable = true)]
        public String GCInquiryProcessType
        {
            get { return _GCInquiryProcessType; }
            set { _GCInquiryProcessType = value; }
        }
        [Column(Name = "OtherInquiryProcessType", DataType = "String", IsNullable = true)]
        public String OtherInquiryProcessType
        {
            get { return _OtherInquiryProcessType; }
            set { _OtherInquiryProcessType = value; }
        }
        [Column(Name = "GCCloseReason", DataType = "String", IsNullable = true)]
        public String GCCloseReason
        {
            get { return _GCCloseReason; }
            set { _GCCloseReason = value; }
        }
        [Column(Name = "OtherCloseReasonText", DataType = "String", IsNullable = true)]
        public String OtherCloseReasonText
        {
            get { return _OtherCloseReasonText; }
            set { _OtherCloseReasonText = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class InquiryDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Inquiry));
        private bool _isAuditLog = false;
        private const string p_InquiryID = "@p_InquiryID";
        public InquiryDao() { }
        public InquiryDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Inquiry Get(Int32 InquiryID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_InquiryID, InquiryID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Inquiry)_helper.DataRowToObject(row, new Inquiry());
        }
        public int Insert(Inquiry record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Inquiry record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 InquiryID)
        {
            Inquiry record;
            if (_ctx.Transaction == null)
                record = new InquiryDao().Get(InquiryID);
            else
                record = Get(InquiryID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region InquiryActivityLog
    [Serializable]
    [Table(Name = "InquiryActivityLog")]
    public class InquiryActivityLog : DbDataModel
    {
        private Int32 _ID;
        private Int32 _InquiryID;
        private DateTime _LogDate;
        private String _LogTime;
        private Int32? _CRO;
        private Int32? _TrainerID;
        private String _GCActivityType;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "InquiryID", DataType = "Int32")]
        public Int32 InquiryID
        {
            get { return _InquiryID; }
            set { _InquiryID = value; }
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
        [Column(Name = "CRO", DataType = "Int32", IsNullable = true)]
        public Int32? CRO
        {
            get { return _CRO; }
            set { _CRO = value; }
        }
        [Column(Name = "TrainerID", DataType = "Int32", IsNullable = true)]
        public Int32? TrainerID
        {
            get { return _TrainerID; }
            set { _TrainerID = value; }
        }
        [Column(Name = "GCActivityType", DataType = "String")]
        public String GCActivityType
        {
            get { return _GCActivityType; }
            set { _GCActivityType = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class InquiryActivityLogDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(InquiryActivityLog));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public InquiryActivityLogDao() { }
        public InquiryActivityLogDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public InquiryActivityLog Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (InquiryActivityLog)_helper.DataRowToObject(row, new InquiryActivityLog());
        }
        public int Insert(InquiryActivityLog record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(InquiryActivityLog record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            InquiryActivityLog record;
            if (_ctx.Transaction == null)
                record = new InquiryActivityLogDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Lead
    [Serializable]
    [Table(Name = "Lead")]
    public class Lead : DbDataModel
    {
        private Int32 _LeadID;
        private String _LeadNo;
        private DateTime _LeadDate;
        private String _GCLeadSourceType;
        private String _OtherLeadSourceType;
        private Int32 _CompanyID;
        private Int32 _MemberID;
        private Int32 _PIC_CRO;
        private String _Subject;
        private String _Remarks;
        private DateTime _ResponseDate;
        private String _ResponseTime;
        private String _GCLeadStatus;
        private String _GCLeadProcessType;
        private String _OtherLeadProcessType;
        private String _GCCloseReason;
        private String _OtherCloseReason;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "LeadID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "OtherLeadSourceType", DataType = "String", IsNullable = true)]
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
        [Column(Name = "MemberID", DataType = "Int32")]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "PIC_CRO", DataType = "Int32")]
        public Int32 PIC_CRO
        {
            get { return _PIC_CRO; }
            set { _PIC_CRO = value; }
        }
        [Column(Name = "Subject", DataType = "String")]
        public String Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "ResponseDate", DataType = "DateTime", IsNullable = true)]
        public DateTime ResponseDate
        {
            get { return _ResponseDate; }
            set { _ResponseDate = value; }
        }
        [Column(Name = "ResponseTime", DataType = "String", IsNullable = true)]
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
        [Column(Name = "GCLeadProcessType", DataType = "String", IsNullable = true)]
        public String GCLeadProcessType
        {
            get { return _GCLeadProcessType; }
            set { _GCLeadProcessType = value; }
        }
        [Column(Name = "OtherLeadProcessType", DataType = "String", IsNullable = true)]
        public String OtherLeadProcessType
        {
            get { return _OtherLeadProcessType; }
            set { _OtherLeadProcessType = value; }
        }
        [Column(Name = "GCCloseReason", DataType = "String", IsNullable = true)]
        public String GCCloseReason
        {
            get { return _GCCloseReason; }
            set { _GCCloseReason = value; }
        }
        [Column(Name = "OtherCloseReason", DataType = "String", IsNullable = true)]
        public String OtherCloseReason
        {
            get { return _OtherCloseReason; }
            set { _OtherCloseReason = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class LeadDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Lead));
        private bool _isAuditLog = false;
        private const string p_LeadID = "@p_LeadID";
        public LeadDao() { }
        public LeadDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Lead Get(Int32 LeadID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_LeadID, LeadID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Lead)_helper.DataRowToObject(row, new Lead());
        }
        public int Insert(Lead record)
        {
            record.CreatedDate = record.LastUpdatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Lead record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 LeadID)
        {
            Lead record;
            if (_ctx.Transaction == null)
                record = new LeadDao().Get(LeadID);
            else
                record = Get(LeadID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region LeadActivityLog
    [Serializable]
    [Table(Name = "LeadActivityLog")]
    public class LeadActivityLog : DbDataModel
    {
        private Int32 _ID;
        private Int32 _LeadID;
        private DateTime _LogDate;
        private String _LogTime;
        private Int32 _CRO;
        private String _GCActivityType;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "CRO", DataType = "Int32")]
        public Int32 CRO
        {
            get { return _CRO; }
            set { _CRO = value; }
        }
        [Column(Name = "GCActivityType", DataType = "String")]
        public String GCActivityType
        {
            get { return _GCActivityType; }
            set { _GCActivityType = value; }
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
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class LeadActivityLogDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(LeadActivityLog));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public LeadActivityLogDao() { }
        public LeadActivityLogDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public LeadActivityLog Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (LeadActivityLog)_helper.DataRowToObject(row, new LeadActivityLog());
        }
        public int Insert(LeadActivityLog record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(LeadActivityLog record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            LeadActivityLog record;
            if (_ctx.Transaction == null)
                record = new LeadActivityLogDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region LOBClassification
    [Serializable]
    [Table(Name = "LOBClassification")]
    public class LOBClassification : DbDataModel
    {
        private Int32 _LOBClassID;
        private Int32? _ParentID;
        private String _LOBClassCode;
        private String _LOBClassName;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "LOBClassID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 LOBClassID
        {
            get { return _LOBClassID; }
            set { _LOBClassID = value; }
        }
        [Column(Name = "ParentID", DataType = "Int32", IsNullable = true)]
        public Int32? ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
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
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class LOBClassificationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(LOBClassification));
        private bool _isAuditLog = false;
        private const string p_LOBClassID = "@p_LOBClassID";
        public LOBClassificationDao() { }
        public LOBClassificationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public LOBClassification Get(Int32 LOBClassID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_LOBClassID, LOBClassID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (LOBClassification)_helper.DataRowToObject(row, new LOBClassification());
        }
        public int Insert(LOBClassification record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(LOBClassification record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 LOBClassID)
        {
            LOBClassification record;
            if (_ctx.Transaction == null)
                record = new LOBClassificationDao().Get(LOBClassID);
            else
                record = Get(LOBClassID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Member
    [Serializable]
    [Table(Name = "Member")]
    public class Member : DbDataModel
    {
        private Int32 _MemberID;
        private String _MemberCode;
        private String _GCSalutation;
        private String _GCSuffix;
        private String _GCMemberStatus;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _PreferredName;
        private String _CityOfBirth;
        private DateTime _DateOfBirth;
        private String _GCNationality;
        private Int32 _CompanyID;
        private String _GCDepartment;
        private String _Occupation;
        private String _GCOccupationLevel;
        private Int32 _AddressID;
        private String _EmailAddress1;
        private String _EmailAddress2;
        private String _MobilePhoneNo1;
        private String _MobilePhoneNo2;
        private String _OfficeExtensionNo;
        private String _VATRegistrationNo;
        private DateTime _LastEventDate;
        private Int32? _LastEventID;
        private Int32? _LastCompanyID;
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
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "MemberID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "GCSalutation", DataType = "String", IsNullable = true)]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "GCSuffix", DataType = "String", IsNullable = true)]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "GCMemberStatus", DataType = "String")]
        public String GCMemberStatus
        {
            get { return _GCMemberStatus; }
            set { _GCMemberStatus = value; }
        }
        [Column(Name = "GCTitle", DataType = "String", IsNullable = true)]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "FirstName", DataType = "String", IsNullable = true)]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CityOfBirth", DataType = "String", IsNullable = true)]
        public String CityOfBirth
        {
            get { return _CityOfBirth; }
            set { _CityOfBirth = value; }
        }
        [Column(Name = "DateOfBirth", DataType = "DateTime", IsNullable = true)]
        public DateTime DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }
        [Column(Name = "GCNationality", DataType = "String", IsNullable = true)]
        public String GCNationality
        {
            get { return _GCNationality; }
            set { _GCNationality = value; }
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
        [Column(Name = "Occupation", DataType = "String")]
        public String Occupation
        {
            get { return _Occupation; }
            set { _Occupation = value; }
        }
        [Column(Name = "GCOccupationLevel", DataType = "String", IsNullable = true)]
        public String GCOccupationLevel
        {
            get { return _GCOccupationLevel; }
            set { _GCOccupationLevel = value; }
        }
        [Column(Name = "AddressID", DataType = "Int32")]
        public Int32 AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }
        [Column(Name = "EmailAddress1", DataType = "String", IsNullable = true)]
        public String EmailAddress1
        {
            get { return _EmailAddress1; }
            set { _EmailAddress1 = value; }
        }
        [Column(Name = "EmailAddress2", DataType = "String", IsNullable = true)]
        public String EmailAddress2
        {
            get { return _EmailAddress2; }
            set { _EmailAddress2 = value; }
        }
        [Column(Name = "MobilePhoneNo1", DataType = "String", IsNullable = true)]
        public String MobilePhoneNo1
        {
            get { return _MobilePhoneNo1; }
            set { _MobilePhoneNo1 = value; }
        }
        [Column(Name = "MobilePhoneNo2", DataType = "String", IsNullable = true)]
        public String MobilePhoneNo2
        {
            get { return _MobilePhoneNo2; }
            set { _MobilePhoneNo2 = value; }
        }
        [Column(Name = "OfficeExtensionNo", DataType = "String", IsNullable = true)]
        public String OfficeExtensionNo
        {
            get { return _OfficeExtensionNo; }
            set { _OfficeExtensionNo = value; }
        }
        [Column(Name = "VATRegistrationNo", DataType = "String", IsNullable = true)]
        public String VATRegistrationNo
        {
            get { return _VATRegistrationNo; }
            set { _VATRegistrationNo = value; }
        }
        [Column(Name = "LastEventDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastEventDate
        {
            get { return _LastEventDate; }
            set { _LastEventDate = value; }
        }
        [Column(Name = "LastEventID", DataType = "Int32", IsNullable = true)]
        public Int32? LastEventID
        {
            get { return _LastEventID; }
            set { _LastEventID = value; }
        }
        [Column(Name = "LastCompanyID", DataType = "Int32", IsNullable = true)]
        public Int32? LastCompanyID
        {
            get { return _LastCompanyID; }
            set { _LastCompanyID = value; }
        }
        [Column(Name = "NumberOfTraining", DataType = "Decimal")]
        public Decimal NumberOfTraining
        {
            get { return _NumberOfTraining; }
            set { _NumberOfTraining = value; }
        }
        [Column(Name = "PictureFileName", DataType = "String", IsNullable = true)]
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
        [Column(Name = "IsISOClub", DataType = "Boolean", IsNullable = true)]
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
        [Column(Name = "SendGreetingsOn", DataType = "String", IsNullable = true)]
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
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class MemberDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Member));
        private bool _isAuditLog = false;
        private const string p_MemberID = "@p_MemberID";
        public MemberDao() { }
        public MemberDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Member Get(Int32 MemberID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_MemberID, MemberID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Member)_helper.DataRowToObject(row, new Member());
        }
        public int Insert(Member record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Member record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 MemberID)
        {
            Member record;
            if (_ctx.Transaction == null)
                record = new MemberDao().Get(MemberID);
            else
                record = Get(MemberID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region MemberGreetings
    [Serializable]
    [Table(Name = "MemberGreetings")]
    public class MemberGreetings : DbDataModel
    {
        private Int32 _MemberID;
        private String _GCGreetingType;

        [Column(Name = "MemberID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "GCGreetingType", DataType = "String", IsPrimaryKey = true)]
        public String GCGreetingType
        {
            get { return _GCGreetingType; }
            set { _GCGreetingType = value; }
        }
    }

    public class MemberGreetingsDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MemberGreetings));
        private bool _isAuditLog = false;
        private const string p_GCGreetingType = "@p_GCGreetingType";
        private const string p_MemberID = "@p_MemberID";
        public MemberGreetingsDao() { }
        public MemberGreetingsDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MemberGreetings Get(Int32 MemberID, String GCGreetingType)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_GCGreetingType, GCGreetingType);
            _ctx.Add(p_MemberID, MemberID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MemberGreetings)_helper.DataRowToObject(row, new MemberGreetings());
        }
        public int Insert(MemberGreetings record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MemberGreetings record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 MemberID, String GCGreetingType)
        {
            MemberGreetings record;
            if (_ctx.Transaction == null)
                record = new MemberGreetingsDao().Get(MemberID, GCGreetingType);
            else
                record = Get(MemberID, GCGreetingType);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region MemberPastTraining
    [Serializable]
    [Table(Name = "MemberPastTraining")]
    public class MemberPastTraining : DbDataModel
    {
        private Int32 _ID;
        private Int32 _MemberID;
        private String _TrainingDate;
        private String _TrainingMonth;
        private String _TrainingYear;
        private Int16 _TrainingDuration;
        private Int32? _EventID;
        private Int32? _TrainingID;
        private String _TrainingName;
        private String _TrainerName;
        private String _VenueName;
        private String _VenueLocation;
        private Boolean _IsFromMigration;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "TrainingDate", DataType = "String", IsNullable = true)]
        public String TrainingDate
        {
            get { return _TrainingDate; }
            set { _TrainingDate = value; }
        }
        [Column(Name = "TrainingMonth", DataType = "String", IsNullable = true)]
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
        [Column(Name = "TrainingID", DataType = "Int32", IsNullable = true)]
        public Int32? TrainingID
        {
            get { return _TrainingID; }
            set { _TrainingID = value; }
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
        [Column(Name = "IsFromMigration", DataType = "Boolean")]
        public Boolean IsFromMigration
        {
            get { return _IsFromMigration; }
            set { _IsFromMigration = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class MemberPastTrainingDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MemberPastTraining));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public MemberPastTrainingDao() { }
        public MemberPastTrainingDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MemberPastTraining Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MemberPastTraining)_helper.DataRowToObject(row, new MemberPastTraining());
        }
        public int Insert(MemberPastTraining record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MemberPastTraining record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            MemberPastTraining record;
            if (_ctx.Transaction == null)
                record = new MemberPastTrainingDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region MenuMaster
    [Serializable]
    [Table(Name = "Menu")]
    public class MenuMaster : DbDataModel
    {
        private Int32 _MenuID;
        private String _MenuCode;
        private String _ModuleID;
        private String _MenuCaption;
        private String _MenuUrl;
        private Int16 _MenuLevel;
        private Int16 _MenuIndex;
        private String _MenuTooltip;
        private Int32? _ParentID;
        private String _CRUDMode;
        private String _ImageUrl;
        private Boolean _IsHeader;
        private Boolean _IsShowInPullDownMenu;
        private Boolean _IsVisible;
        private Boolean _IsBeginGroup;
        private String _HelpLinkIDForList;
        private String _HelpLinkIDForEntry;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "MenuID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ModuleID", DataType = "String", IsNullable = true)]
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
        [Column(Name = "MenuUrl", DataType = "String", IsNullable = true)]
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
        [Column(Name = "ParentID", DataType = "Int32", IsNullable = true)]
        public Int32? ParentID
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
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "IsShowInPullDownMenu", DataType = "Boolean")]
        public Boolean IsShowInPullDownMenu
        {
            get { return _IsShowInPullDownMenu; }
            set { _IsShowInPullDownMenu = value; }
        }
        [Column(Name = "IsVisible", DataType = "Boolean")]
        public Boolean IsVisible
        {
            get { return _IsVisible; }
            set { _IsVisible = value; }
        }
        [Column(Name = "IsBeginGroup", DataType = "Boolean")]
        public Boolean IsBeginGroup
        {
            get { return _IsBeginGroup; }
            set { _IsBeginGroup = value; }
        }
        [Column(Name = "HelpLinkIDForList", DataType = "String")]
        public String HelpLinkIDForList
        {
            get { return _HelpLinkIDForList; }
            set { _HelpLinkIDForList = value; }
        }
        [Column(Name = "HelpLinkIDForEntry", DataType = "String")]
        public String HelpLinkIDForEntry
        {
            get { return _HelpLinkIDForEntry; }
            set { _HelpLinkIDForEntry = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class MenuMasterDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MenuMaster));
        private bool _isAuditLog = false;
        private const string p_MenuID = "@p_MenuID";
        public MenuMasterDao() { }
        public MenuMasterDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MenuMaster Get(Int32 MenuID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_MenuID, MenuID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MenuMaster)_helper.DataRowToObject(row, new MenuMaster());
        }
        public int Insert(MenuMaster record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MenuMaster record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 MenuID)
        {
            MenuMaster record;
            if (_ctx.Transaction == null)
                record = new MenuMasterDao().Get(MenuID);
            else
                record = Get(MenuID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Module
    [Serializable]
    [Table(Name = "Module")]
    public class Module : DbDataModel
    {
        private String _ModuleID;
        private String _ModuleName;
        private String _ModuleShortName;
        private Int16 _ModuleIndex;
        private String _ImageUrl;
        private String _DisabledImageUrl;
        private String _DefaultUrl;
        private String _Description;
        private Boolean _IsVisible;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ModuleID", DataType = "String", IsPrimaryKey = true)]
        public String ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }
        [Column(Name = "ModuleName", DataType = "String")]
        public String ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }
        [Column(Name = "ModuleShortName", DataType = "String")]
        public String ModuleShortName
        {
            get { return _ModuleShortName; }
            set { _ModuleShortName = value; }
        }
        [Column(Name = "ModuleIndex", DataType = "Int16")]
        public Int16 ModuleIndex
        {
            get { return _ModuleIndex; }
            set { _ModuleIndex = value; }
        }
        [Column(Name = "ImageUrl", DataType = "String")]
        public String ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }
        [Column(Name = "DisabledImageUrl", DataType = "String")]
        public String DisabledImageUrl
        {
            get { return _DisabledImageUrl; }
            set { _DisabledImageUrl = value; }
        }
        [Column(Name = "DefaultUrl", DataType = "String")]
        public String DefaultUrl
        {
            get { return _DefaultUrl; }
            set { _DefaultUrl = value; }
        }
        [Column(Name = "Description", DataType = "String")]
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        [Column(Name = "IsVisible", DataType = "Boolean")]
        public Boolean IsVisible
        {
            get { return _IsVisible; }
            set { _IsVisible = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime")]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ModuleDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Module));
        private bool _isAuditLog = false;
        private const string p_ModuleID = "@p_ModuleID";
        public ModuleDao() { }
        public ModuleDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Module Get(String ModuleID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ModuleID, ModuleID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Module)_helper.DataRowToObject(row, new Module());
        }
        public int Insert(Module record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Module record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(String ModuleID)
        {
            Module record;
            if (_ctx.Transaction == null)
                record = new ModuleDao().Get(ModuleID);
            else
                record = Get(ModuleID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Occupation
    [Serializable]
    [Table(Name = "Occupation")]
    public class Occupation : DbDataModel
    {
        private Int32 _OccupationID;
        private String _OccupationCode;
        private String _OccupationName;
        private String _GCOccupationLevel;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "OccupationID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class OccupationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Occupation));
        private bool _isAuditLog = false;
        private const string p_OccupationID = "@p_OccupationID";
        public OccupationDao() { }
        public OccupationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Occupation Get(Int32 OccupationID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_OccupationID, OccupationID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Occupation)_helper.DataRowToObject(row, new Occupation());
        }
        public int Insert(Occupation record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Occupation record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 OccupationID)
        {
            Occupation record;
            if (_ctx.Transaction == null)
                record = new OccupationDao().Get(OccupationID);
            else
                record = Get(OccupationID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ProposalActivityLog
    [Serializable]
    [Table(Name = "ProposalActivityLog")]
    public class ProposalActivityLog : DbDataModel
    {
        private Int32 _ID;
        private Int32 _ProposalID;
        private DateTime _LogDate;
        private String _LogTime;
        private Int32? _CRO;
        private Int32? _TrainerID;
        private String _GCActivityType;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "ProposalID", DataType = "Int32")]
        public Int32 ProposalID
        {
            get { return _ProposalID; }
            set { _ProposalID = value; }
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
        [Column(Name = "CRO", DataType = "Int32", IsNullable = true)]
        public Int32? CRO
        {
            get { return _CRO; }
            set { _CRO = value; }
        }
        [Column(Name = "TrainerID", DataType = "Int32", IsNullable = true)]
        public Int32? TrainerID
        {
            get { return _TrainerID; }
            set { _TrainerID = value; }
        }
        [Column(Name = "GCActivityType", DataType = "String")]
        public String GCActivityType
        {
            get { return _GCActivityType; }
            set { _GCActivityType = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ProposalActivityLogDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ProposalActivityLog));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public ProposalActivityLogDao() { }
        public ProposalActivityLogDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ProposalActivityLog Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ProposalActivityLog)_helper.DataRowToObject(row, new ProposalActivityLog());
        }
        public int Insert(ProposalActivityLog record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ProposalActivityLog record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            ProposalActivityLog record;
            if (_ctx.Transaction == null)
                record = new ProposalActivityLogDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ProposalDt
    [Serializable]
    [Table(Name = "ProposalDt")]
    public class ProposalDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _ProposalID;
        private String _GCItemType;
        private Int32 _ItemID;
        private Int16 _Duration;
        private Decimal _NoOfPerson;
        private Decimal _Amount;
        private Decimal _MarginPercentage;
        private String _ProposalContent;
        private Boolean _IsActive;
        private Boolean _IsRevision;
        private DateTime _RevisionDate;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "ProposalID", DataType = "Int32")]
        public Int32 ProposalID
        {
            get { return _ProposalID; }
            set { _ProposalID = value; }
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
        [Column(Name = "Duration", DataType = "Int16")]
        public Int16 Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }
        [Column(Name = "NoOfPerson", DataType = "Decimal")]
        public Decimal NoOfPerson
        {
            get { return _NoOfPerson; }
            set { _NoOfPerson = value; }
        }
        [Column(Name = "Amount", DataType = "Decimal")]
        public Decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        [Column(Name = "MarginPercentage", DataType = "Decimal")]
        public Decimal MarginPercentage
        {
            get { return _MarginPercentage; }
            set { _MarginPercentage = value; }
        }
        [Column(Name = "ProposalContent", DataType = "String")]
        public String ProposalContent
        {
            get { return _ProposalContent; }
            set { _ProposalContent = value; }
        }
        [Column(Name = "IsActive", DataType = "Boolean")]
        public Boolean IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        [Column(Name = "IsRevision", DataType = "Boolean")]
        public Boolean IsRevision
        {
            get { return _IsRevision; }
            set { _IsRevision = value; }
        }
        [Column(Name = "RevisionDate", DataType = "DateTime", IsNullable = true)]
        public DateTime RevisionDate
        {
            get { return _RevisionDate; }
            set { _RevisionDate = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ProposalDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ProposalDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public ProposalDtDao() { }
        public ProposalDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ProposalDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ProposalDt)_helper.DataRowToObject(row, new ProposalDt());
        }
        public int Insert(ProposalDt record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ProposalDt record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            ProposalDt record;
            if (_ctx.Transaction == null)
                record = new ProposalDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ProposalHd
    [Serializable]
    [Table(Name = "ProposalHd")]
    public class ProposalHd : DbDataModel
    {
        private Int32 _ProposalID;
        private String _ProposalNo;
        private DateTime _ProposalDate;
        private Int32 _InquiryID;
        private String _GCProposalType;
        private String _GCLanguageType;
        private Int32 _CompanyID;
        private Int32 _MemberID;
        private Int32 _PIC_CRO;
        private Int32 _PIC_TrainerID;
        private String _Subject;
        private String _Remarks;
        private Decimal _SuccessPercentage;
        private String _GCProposalStatus;
        private String _GCCloseReason;
        private String _CloseReasonText;
        private Int32 _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ProposalID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ProposalID
        {
            get { return _ProposalID; }
            set { _ProposalID = value; }
        }
        [Column(Name = "ProposalNo", DataType = "String")]
        public String ProposalNo
        {
            get { return _ProposalNo; }
            set { _ProposalNo = value; }
        }
        [Column(Name = "ProposalDate", DataType = "DateTime")]
        public DateTime ProposalDate
        {
            get { return _ProposalDate; }
            set { _ProposalDate = value; }
        }
        [Column(Name = "InquiryID", DataType = "Int32")]
        public Int32 InquiryID
        {
            get { return _InquiryID; }
            set { _InquiryID = value; }
        }
        [Column(Name = "GCProposalType", DataType = "String")]
        public String GCProposalType
        {
            get { return _GCProposalType; }
            set { _GCProposalType = value; }
        }
        [Column(Name = "GCLanguageType", DataType = "String")]
        public String GCLanguageType
        {
            get { return _GCLanguageType; }
            set { _GCLanguageType = value; }
        }
        [Column(Name = "CompanyID", DataType = "Int32")]
        public Int32 CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        [Column(Name = "MemberID", DataType = "Int32")]
        public Int32 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }
        [Column(Name = "PIC_CRO", DataType = "Int32")]
        public Int32 PIC_CRO
        {
            get { return _PIC_CRO; }
            set { _PIC_CRO = value; }
        }
        [Column(Name = "PIC_TrainerID", DataType = "Int32")]
        public Int32 PIC_TrainerID
        {
            get { return _PIC_TrainerID; }
            set { _PIC_TrainerID = value; }
        }
        [Column(Name = "Subject", DataType = "String")]
        public String Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        [Column(Name = "SuccessPercentage", DataType = "Decimal")]
        public Decimal SuccessPercentage
        {
            get { return _SuccessPercentage; }
            set { _SuccessPercentage = value; }
        }
        [Column(Name = "GCProposalStatus", DataType = "String")]
        public String GCProposalStatus
        {
            get { return _GCProposalStatus; }
            set { _GCProposalStatus = value; }
        }
        [Column(Name = "GCCloseReason", DataType = "String", IsNullable = true)]
        public String GCCloseReason
        {
            get { return _GCCloseReason; }
            set { _GCCloseReason = value; }
        }
        [Column(Name = "CloseReasonText", DataType = "String")]
        public String CloseReasonText
        {
            get { return _CloseReasonText; }
            set { _CloseReasonText = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32")]
        public Int32 CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ProposalHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ProposalHd));
        private bool _isAuditLog = false;
        private const string p_ProposalID = "@p_ProposalID";
        public ProposalHdDao() { }
        public ProposalHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ProposalHd Get(Int32 ProposalID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ProposalID, ProposalID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ProposalHd)_helper.DataRowToObject(row, new ProposalHd());
        }
        public int Insert(ProposalHd record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ProposalHd record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ProposalID)
        {
            ProposalHd record;
            if (_ctx.Transaction == null)
                record = new ProposalHdDao().Get(ProposalID);
            else
                record = Get(ProposalID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Region
    [Serializable]
    [Table(Name = "Region")]
    public class Region : DbDataModel
    {
        private Int32 _RegionID;
        private String _RegionCode;
        private String _RegionName;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "RegionID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class RegionDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Region));
        private bool _isAuditLog = false;
        private const string p_RegionID = "@p_RegionID";
        public RegionDao() { }
        public RegionDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Region Get(Int32 RegionID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_RegionID, RegionID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Region)_helper.DataRowToObject(row, new Region());
        }
        public int Insert(Region record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Region record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 RegionID)
        {
            Region record;
            if (_ctx.Transaction == null)
                record = new RegionDao().Get(RegionID);
            else
                record = Get(RegionID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ReportMaster
    [Serializable]
    [Table(Name = "ReportMaster")]
    public class ReportMaster : DbDataModel
    {
        private Int32 _ReportID;
        private String _ReportCode;
        private String _ReportTitle1;
        private String _ReportTitle2;
        private String _GCReportType;
        private String _ClassName;
        private String _GCDataSourceType;
        private String _ObjectTypeName;
        private String _AdditionalFilterExpression;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ReportID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "ReportTitle2", DataType = "String", IsNullable = true)]
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
        [Column(Name = "ClassName", DataType = "String")]
        public String ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }
        [Column(Name = "GCDataSourceType", DataType = "String", IsNullable = true)]
        public String GCDataSourceType
        {
            get { return _GCDataSourceType; }
            set { _GCDataSourceType = value; }
        }
        [Column(Name = "ObjectTypeName", DataType = "String", IsNullable = true)]
        public String ObjectTypeName
        {
            get { return _ObjectTypeName; }
            set { _ObjectTypeName = value; }
        }
        [Column(Name = "AdditionalFilterExpression", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }


    public class ReportMasterDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ReportMaster));
        private bool _isAuditLog = false;
        private const string p_ReportID = "@p_ReportID";
        public ReportMasterDao() { }
        public ReportMasterDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ReportMaster Get(Int32 ReportID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ReportID, ReportID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ReportMaster)_helper.DataRowToObject(row, new ReportMaster());
        }
        public int Insert(ReportMaster record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ReportMaster record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ReportID)
        {
            ReportMaster record;
            if (_ctx.Transaction == null)
                record = new ReportMasterDao().Get(ReportID);
            else
                record = Get(ReportID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ReportParameter
    [Serializable]
    [Table(Name = "ReportParameter")]
    public class ReportParameter : DbDataModel
    {
        private Int32 _ReportID;
        private Int32 _FilterParameterID;
        private Int16 _DisplayOrder;

        [Column(Name = "ReportID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 ReportID
        {
            get { return _ReportID; }
            set { _ReportID = value; }
        }
        [Column(Name = "FilterParameterID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 FilterParameterID
        {
            get { return _FilterParameterID; }
            set { _FilterParameterID = value; }
        }
        [Column(Name = "DisplayOrder", DataType = "Int16")]
        public Int16 DisplayOrder
        {
            get { return _DisplayOrder; }
            set { _DisplayOrder = value; }
        }
    }

    public class ReportParameterDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ReportParameter));
        private bool _isAuditLog = false;
        private const string p_FilterParameterID = "@p_FilterParameterID";
        private const string p_ReportID = "@p_ReportID";
        public ReportParameterDao() { }
        public ReportParameterDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ReportParameter Get(Int32 ReportID, Int32 FilterParameterID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_FilterParameterID, FilterParameterID);
            _ctx.Add(p_ReportID, ReportID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ReportParameter)_helper.DataRowToObject(row, new ReportParameter());
        }
        public int Insert(ReportParameter record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ReportParameter record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ReportID, Int32 FilterParameterID)
        {
            ReportParameter record;
            if (_ctx.Transaction == null)
                record = new ReportParameterDao().Get(ReportID, FilterParameterID);
            else
                record = Get(ReportID, FilterParameterID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region RestoreDataConfiguration
    [Serializable]
    [Table(Name = "RestoreDataConfiguration")]
    public class RestoreDataConfiguration : DbDataModel
    {
        private Int32 _ID;
        private String _TableName;
        private String _TableAlias;
        private String _FilterExpression;
        private String _GridColumns;
        private String _TableViewList;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "TableName", DataType = "String")]
        public String TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        [Column(Name = "TableAlias", DataType = "String")]
        public String TableAlias
        {
            get { return _TableAlias; }
            set { _TableAlias = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String", IsNullable = true)]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "GridColumns", DataType = "String")]
        public String GridColumns
        {
            get { return _GridColumns; }
            set { _GridColumns = value; }
        }
        [Column(Name = "TableViewList", DataType = "String", IsNullable = true)]
        public String TableViewList
        {
            get { return _TableViewList; }
            set { _TableViewList = value; }
        }
    }

    public class RestoreDataConfigurationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(RestoreDataConfiguration));
        private bool _isAuditLog = false;
        public RestoreDataConfigurationDao() { }
        public RestoreDataConfigurationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public RestoreDataConfiguration Get()
        {
            _ctx.CommandText = _helper.GetRecord();
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (RestoreDataConfiguration)_helper.DataRowToObject(row, new RestoreDataConfiguration());
        }
        public int Insert(RestoreDataConfiguration record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(RestoreDataConfiguration record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete()
        {
            RestoreDataConfiguration record;
            if (_ctx.Transaction == null)
                record = new RestoreDataConfigurationDao().Get();
            else
                record = Get();
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SettingParameter
    [Serializable]
    [Table(Name = "SettingParameter")]
    public class SettingParameter : DbDataModel
    {
        private String _ParameterCode;
        private String _ParameterName;
        private String _GCParameterValueType;
        private String _TableName;
        private String _FilterExpression;
        private String _ValueField;
        private String _TextField;
        private String _ParameterValue;
        private String _Notes;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ParameterCode", DataType = "String", IsPrimaryKey = true)]
        public String ParameterCode
        {
            get { return _ParameterCode; }
            set { _ParameterCode = value; }
        }
        [Column(Name = "ParameterName", DataType = "String")]
        public String ParameterName
        {
            get { return _ParameterName; }
            set { _ParameterName = value; }
        }
        [Column(Name = "GCParameterValueType", DataType = "String")]
        public String GCParameterValueType
        {
            get { return _GCParameterValueType; }
            set { _GCParameterValueType = value; }
        }
        [Column(Name = "TableName", DataType = "String", IsNullable = true)]
        public String TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String", IsNullable = true)]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "ValueField", DataType = "String", IsNullable = true)]
        public String ValueField
        {
            get { return _ValueField; }
            set { _ValueField = value; }
        }
        [Column(Name = "TextField", DataType = "String", IsNullable = true)]
        public String TextField
        {
            get { return _TextField; }
            set { _TextField = value; }
        }
        [Column(Name = "ParameterValue", DataType = "String", IsNullable = true)]
        public String ParameterValue
        {
            get { return _ParameterValue; }
            set { _ParameterValue = value; }
        }
        [Column(Name = "Notes", DataType = "String", IsNullable = true)]
        public String Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class SettingParameterDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(SettingParameter));
        private bool _isAuditLog = false;
        private const string p_ParameterCode = "@p_ParameterCode";
        public SettingParameterDao() { }
        public SettingParameterDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public SettingParameter Get(String ParameterCode)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ParameterCode, ParameterCode);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (SettingParameter)_helper.DataRowToObject(row, new SettingParameter());
        }
        public int Insert(SettingParameter record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(SettingParameter record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(String ParameterCode)
        {
            SettingParameter record;
            if (_ctx.Transaction == null)
                record = new SettingParameterDao().Get(ParameterCode);
            else
                record = Get(ParameterCode);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region StandardCode
    [Serializable]
    [Table(Name = "StandardCode")]
    public partial class StandardCode : DbDataModel
    {
        private String _StandardCodeID;
        private String _StandardCodeName;
        private String _TagProperty;
        private String _ParentID;
        private Boolean _IsHeader;
        private Boolean _IsDefault;
        private Boolean _IsOther;
        private Boolean _IsEditableByUser;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "StandardCodeID", DataType = "String", IsPrimaryKey = true)]
        public String StandardCodeID
        {
            get { return _StandardCodeID; }
            set { _StandardCodeID = value; }
        }
        [Column(Name = "StandardCodeName", DataType = "String")]
        public String StandardCodeName
        {
            get { return _StandardCodeName; }
            set { _StandardCodeName = value; }
        }
        [Column(Name = "TagProperty", DataType = "String", IsNullable = true)]
        public String TagProperty
        {
            get { return _TagProperty; }
            set { _TagProperty = value; }
        }
        [Column(Name = "ParentID", DataType = "String", IsNullable = true)]
        public String ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        [Column(Name = "IsHeader", DataType = "Boolean")]
        public Boolean IsHeader
        {
            get { return _IsHeader; }
            set { _IsHeader = value; }
        }
        [Column(Name = "IsDefault", DataType = "Boolean", IsNullable = true)]
        public Boolean IsDefault
        {
            get { return _IsDefault; }
            set { _IsDefault = value; }
        }
        [Column(Name = "IsOther", DataType = "Boolean")]
        public Boolean IsOther
        {
            get { return _IsOther; }
            set { _IsOther = value; }
        }
        [Column(Name = "IsEditableByUser", DataType = "Boolean")]
        public Boolean IsEditableByUser
        {
            get { return _IsEditableByUser; }
            set { _IsEditableByUser = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class StandardCodeDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(StandardCode));
        private bool _isAuditLog = false;
        private const string p_StandardCodeID = "@p_StandardCodeID";
        public StandardCodeDao() { }
        public StandardCodeDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public StandardCode Get(String StandardCodeID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_StandardCodeID, StandardCodeID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (StandardCode)_helper.DataRowToObject(row, new StandardCode());
        }
        public int Insert(StandardCode record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(StandardCode record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(String StandardCodeID)
        {
            StandardCode record;
            if (_ctx.Transaction == null)
                record = new StandardCodeDao().Get(StandardCodeID);
            else
                record = Get(StandardCodeID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region TemplateText
    [Serializable]
    [Table(Name = "TemplateText")]
    public class TemplateText : DbDataModel
    {
        private Int32 _TemplateID;
        private String _TemplateCode;
        private String _TemplateName;
        private String _GCTemplateGroup;
        private String _TemplateContent;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "TemplateID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "GCTemplateGroup", DataType = "String", IsNullable = true)]
        public String GCTemplateGroup
        {
            get { return _GCTemplateGroup; }
            set { _GCTemplateGroup = value; }
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class TemplateTextDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(TemplateText));
        private bool _isAuditLog = false;
        private const string p_TemplateID = "@p_TemplateID";
        public TemplateTextDao() { }
        public TemplateTextDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public TemplateText Get(Int32 TemplateID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_TemplateID, TemplateID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (TemplateText)_helper.DataRowToObject(row, new TemplateText());
        }
        public int Insert(TemplateText record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(TemplateText record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 TemplateID)
        {
            TemplateText record;
            if (_ctx.Transaction == null)
                record = new TemplateTextDao().Get(TemplateID);
            else
                record = Get(TemplateID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Trainer
    [Serializable]
    [Table(Name = "Trainer")]
    public class Trainer : DbDataModel
    {
        private Int32 _TrainerID;
        private String _TrainerCode;
        private String _GCSalutation;
        private String _GCTitle;
        private String _FirstName;
        private String _MiddleName;
        private String _LastName;
        private String _GCSuffix;
        private String _EmailAddress;
        private String _MobilePhone1;
        private String _MobilePhone2;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "TrainerID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "GCSalutation", DataType = "String", IsNullable = true)]
        public String GCSalutation
        {
            get { return _GCSalutation; }
            set { _GCSalutation = value; }
        }
        [Column(Name = "GCTitle", DataType = "String", IsNullable = true)]
        public String GCTitle
        {
            get { return _GCTitle; }
            set { _GCTitle = value; }
        }
        [Column(Name = "FirstName", DataType = "String", IsNullable = true)]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        [Column(Name = "MiddleName", DataType = "String", IsNullable = true)]
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
        [Column(Name = "GCSuffix", DataType = "String", IsNullable = true)]
        public String GCSuffix
        {
            get { return _GCSuffix; }
            set { _GCSuffix = value; }
        }
        [Column(Name = "EmailAddress", DataType = "String", IsNullable = true)]
        public String EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }
        [Column(Name = "MobilePhone1", DataType = "String", IsNullable = true)]
        public String MobilePhone1
        {
            get { return _MobilePhone1; }
            set { _MobilePhone1 = value; }
        }
        [Column(Name = "MobilePhone2", DataType = "String", IsNullable = true)]
        public String MobilePhone2
        {
            get { return _MobilePhone2; }
            set { _MobilePhone2 = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class TrainerDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Trainer));
        private bool _isAuditLog = false;
        private const string p_TrainerID = "@p_TrainerID";
        public TrainerDao() { }
        public TrainerDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Trainer Get(Int32 TrainerID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_TrainerID, TrainerID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Trainer)_helper.DataRowToObject(row, new Trainer());
        }
        public int Insert(Trainer record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Trainer record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 TrainerID)
        {
            Trainer record;
            if (_ctx.Transaction == null)
                record = new TrainerDao().Get(TrainerID);
            else
                record = Get(TrainerID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Training
    [Serializable]
    [Table(Name = "Training")]
    public class Training : DbDataModel
    {
        private Int32 _TrainingID;
        private String _TrainingCode;
        private String _TrainingName;
        private String _GCTrainingType;
        private Boolean _IsHasCertification;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private Int32? _LastUpdatedBy;
        private DateTime _CreatedDate;
        private DateTime _LastUpdatedDate;

        [Column(Name = "TrainingID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "IsHasCertification", DataType = "Boolean")]
        public Boolean IsHasCertification
        {
            get { return _IsHasCertification; }
            set { _IsHasCertification = value; }
        }
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class TrainingDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Training));
        private bool _isAuditLog = false;
        private const string p_TrainingID = "@p_TrainingID";
        public TrainingDao() { }
        public TrainingDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Training Get(Int32 TrainingID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_TrainingID, TrainingID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Training)_helper.DataRowToObject(row, new Training());
        }
        public int Insert(Training record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Training record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 TrainingID)
        {
            Training record;
            if (_ctx.Transaction == null)
                record = new TrainingDao().Get(TrainingID);
            else
                record = Get(TrainingID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region TrainingCertification
    [Serializable]
    [Table(Name = "TrainingCertification")]
    public class TrainingCertification : DbDataModel
    {
        private Int32 _TrainingID;
        private String _GCCompanyCertification;

        [Column(Name = "TrainingID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 TrainingID
        {
            get { return _TrainingID; }
            set { _TrainingID = value; }
        }
        [Column(Name = "GCCompanyCertification", DataType = "String", IsPrimaryKey = true)]
        public String GCCompanyCertification
        {
            get { return _GCCompanyCertification; }
            set { _GCCompanyCertification = value; }
        }
    }

    public class TrainingCertificationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(TrainingCertification));
        private bool _isAuditLog = false;
        private const string p_GCCompanyCertification = "@p_GCCompanyCertification";
        private const string p_TrainingID = "@p_TrainingID";
        public TrainingCertificationDao() { }
        public TrainingCertificationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public TrainingCertification Get(Int32 TrainingID, String GCCompanyCertification)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_GCCompanyCertification, GCCompanyCertification);
            _ctx.Add(p_TrainingID, TrainingID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (TrainingCertification)_helper.DataRowToObject(row, new TrainingCertification());
        }
        public int Insert(TrainingCertification record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(TrainingCertification record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 TrainingID, String GCCompanyCertification)
        {
            TrainingCertification record;
            if (_ctx.Transaction == null)
                record = new TrainingCertificationDao().Get(TrainingID, GCCompanyCertification);
            else
                record = Get(TrainingID, GCCompanyCertification);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region TrainingDepartment
    [Serializable]
    [Table(Name = "TrainingDepartment")]
    public class TrainingDepartment : DbDataModel
    {
        private Int32 _TrainingID;
        private String _GCDepartment;

        [Column(Name = "TrainingID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 TrainingID
        {
            get { return _TrainingID; }
            set { _TrainingID = value; }
        }
        [Column(Name = "GCDepartment", DataType = "String", IsPrimaryKey = true)]
        public String GCDepartment
        {
            get { return _GCDepartment; }
            set { _GCDepartment = value; }
        }
    }

    public class TrainingDepartmentDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(TrainingDepartment));
        private bool _isAuditLog = false;
        private const string p_GCDepartment = "@p_GCDepartment";
        private const string p_TrainingID = "@p_TrainingID";
        public TrainingDepartmentDao() { }
        public TrainingDepartmentDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public TrainingDepartment Get(Int32 TrainingID, String GCDepartment)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_GCDepartment, GCDepartment);
            _ctx.Add(p_TrainingID, TrainingID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (TrainingDepartment)_helper.DataRowToObject(row, new TrainingDepartment());
        }
        public int Insert(TrainingDepartment record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(TrainingDepartment record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 TrainingID, String GCDepartment)
        {
            TrainingDepartment record;
            if (_ctx.Transaction == null)
                record = new TrainingDepartmentDao().Get(TrainingID, GCDepartment);
            else
                record = Get(TrainingID, GCDepartment);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region TrainingOccupation
    [Serializable]
    [Table(Name = "TrainingOccupation")]
    public class TrainingOccupation : DbDataModel
    {
        private Int32 _TrainingID;
        private String _GCOccupationLevel;

        [Column(Name = "TrainingID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 TrainingID
        {
            get { return _TrainingID; }
            set { _TrainingID = value; }
        }
        [Column(Name = "GCOccupationLevel", DataType = "String", IsPrimaryKey = true)]
        public String GCOccupationLevel
        {
            get { return _GCOccupationLevel; }
            set { _GCOccupationLevel = value; }
        }
    }

    public class TrainingOccupationDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(TrainingOccupation));
        private bool _isAuditLog = false;
        private const string p_GCOccupationLevel = "@p_GCOccupationLevel";
        private const string p_TrainingID = "@p_TrainingID";
        public TrainingOccupationDao() { }
        public TrainingOccupationDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public TrainingOccupation Get(Int32 TrainingID, String GCOccupationLevel)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_GCOccupationLevel, GCOccupationLevel);
            _ctx.Add(p_TrainingID, TrainingID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (TrainingOccupation)_helper.DataRowToObject(row, new TrainingOccupation());
        }
        public int Insert(TrainingOccupation record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(TrainingOccupation record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 TrainingID, String GCOccupationLevel)
        {
            TrainingOccupation record;
            if (_ctx.Transaction == null)
                record = new TrainingOccupationDao().Get(TrainingID, GCOccupationLevel);
            else
                record = Get(TrainingID, GCOccupationLevel);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region User
    [Serializable]
    [Table(Name = "[User]")]
    public class User : DbDataModel
    {
        private Int32 _UserID;
        private String _UserName;
        private String _LoweredUserName;
        private String _Password;
        private String _MobileAlias;
        private Boolean _IsAnonymous;
        private DateTime _LastActivityDate;
        private String _MobilePIN;
        private String _Email;
        private String _EmailPassword;
        private String _LoweredEmail;
        private String _PasswordQuestion;
        private String _PasswordAnswer;
        private Boolean _IsApproved;
        private Boolean _IsLockedOut;
        private DateTime _LastLoginDate;
        private DateTime _LastPasswordChangedDate;
        private DateTime _LastLockoutDate;
        private String _Comment;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "UserID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "LoweredUserName", DataType = "String")]
        public String LoweredUserName
        {
            get { return _LoweredUserName; }
            set { _LoweredUserName = value; }
        }
        [Column(Name = "Password", DataType = "String")]
        public String Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        [Column(Name = "MobileAlias", DataType = "String", IsNullable = true)]
        public String MobileAlias
        {
            get { return _MobileAlias; }
            set { _MobileAlias = value; }
        }
        [Column(Name = "IsAnonymous", DataType = "Boolean")]
        public Boolean IsAnonymous
        {
            get { return _IsAnonymous; }
            set { _IsAnonymous = value; }
        }
        [Column(Name = "LastActivityDate", DataType = "DateTime")]
        public DateTime LastActivityDate
        {
            get { return _LastActivityDate; }
            set { _LastActivityDate = value; }
        }
        [Column(Name = "MobilePIN", DataType = "String", IsNullable = true)]
        public String MobilePIN
        {
            get { return _MobilePIN; }
            set { _MobilePIN = value; }
        }
        [Column(Name = "Email", DataType = "String", IsNullable = true)]
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        [Column(Name = "EmailPassword", DataType = "String", IsNullable = true)]
        public String EmailPassword
        {
            get { return _EmailPassword; }
            set { _EmailPassword = value; }
        }
        [Column(Name = "LoweredEmail", DataType = "String", IsNullable = true)]
        public String LoweredEmail
        {
            get { return _LoweredEmail; }
            set { _LoweredEmail = value; }
        }
        [Column(Name = "PasswordQuestion", DataType = "String", IsNullable = true)]
        public String PasswordQuestion
        {
            get { return _PasswordQuestion; }
            set { _PasswordQuestion = value; }
        }
        [Column(Name = "PasswordAnswer", DataType = "String", IsNullable = true)]
        public String PasswordAnswer
        {
            get { return _PasswordAnswer; }
            set { _PasswordAnswer = value; }
        }
        [Column(Name = "IsApproved", DataType = "Boolean")]
        public Boolean IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
        [Column(Name = "IsLockedOut", DataType = "Boolean")]
        public Boolean IsLockedOut
        {
            get { return _IsLockedOut; }
            set { _IsLockedOut = value; }
        }
        [Column(Name = "LastLoginDate", DataType = "DateTime")]
        public DateTime LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; }
        }
        [Column(Name = "LastPasswordChangedDate", DataType = "DateTime")]
        public DateTime LastPasswordChangedDate
        {
            get { return _LastPasswordChangedDate; }
            set { _LastPasswordChangedDate = value; }
        }
        [Column(Name = "LastLockoutDate", DataType = "DateTime")]
        public DateTime LastLockoutDate
        {
            get { return _LastLockoutDate; }
            set { _LastLockoutDate = value; }
        }
        [Column(Name = "Comment", DataType = "String", IsNullable = true)]
        public String Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class UserDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(User));
        private bool _isAuditLog = false;
        private const string p_UserID = "@p_UserID";
        public UserDao() { }
        public UserDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public User Get(Int32 UserID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_UserID, UserID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (User)_helper.DataRowToObject(row, new User());
        }
        public int Insert(User record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(User record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 UserID)
        {
            User record;
            if (_ctx.Transaction == null)
                record = new UserDao().Get(UserID);
            else
                record = Get(UserID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region UserInRole
    [Serializable]
    [Table(Name = "UserInRole")]
    public class UserInRole : DbDataModel
    {
        private Int32 _UserID;
        private Int32 _RoleID;

        [Column(Name = "UserID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        [Column(Name = "RoleID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
    }

    public class UserInRoleDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(UserInRole));
        private bool _isAuditLog = false;
        private const string p_RoleID = "@p_RoleID";
        private const string p_UserID = "@p_UserID";
        public UserInRoleDao() { }
        public UserInRoleDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public UserInRole Get(Int32 UserID, Int32 RoleID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_RoleID, RoleID);
            _ctx.Add(p_UserID, UserID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (UserInRole)_helper.DataRowToObject(row, new UserInRole());
        }
        public int Insert(UserInRole record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(UserInRole record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 UserID, Int32 RoleID)
        {
            UserInRole record;
            if (_ctx.Transaction == null)
                record = new UserInRoleDao().Get(UserID, RoleID);
            else
                record = Get(UserID, RoleID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region UserMenu
    [Serializable]
    [Table(Name = "UserMenu")]
    public class UserMenu : DbDataModel
    {
        private Int32 _ID;
        private Int32 _MenuID;
        private Int32 _RoleID;
        private Int32 _UserID;
        private String _CRUDMode;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class UserMenuDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(UserMenu));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public UserMenuDao() { }
        public UserMenuDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public UserMenu Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (UserMenu)_helper.DataRowToObject(row, new UserMenu());
        }
        public int Insert(UserMenu record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(UserMenu record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            UserMenu record;
            if (_ctx.Transaction == null)
                record = new UserMenuDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region UserRole
    [Serializable]
    [Table(Name = "UserRole")]
    public class UserRole : DbDataModel
    {
        private Int32 _RoleID;
        private String _RoleName;
        private String _LoweredRoleName;
        private String _Description;
        private String _DefaultPageUrl;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "RoleID", DataType = "Int32", IsIdentity = true, IsPrimaryKey = true)]
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
        [Column(Name = "LoweredRoleName", DataType = "String")]
        public String LoweredRoleName
        {
            get { return _LoweredRoleName; }
            set { _LoweredRoleName = value; }
        }
        [Column(Name = "Description", DataType = "String", IsNullable = true)]
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        [Column(Name = "DefaultPageUrl", DataType = "String")]
        public String DefaultPageUrl
        {
            get { return _DefaultPageUrl; }
            set { _DefaultPageUrl = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean")]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class UserRoleDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(UserRole));
        private bool _isAuditLog = false;
        private const string p_RoleID = "@p_RoleID";
        public UserRoleDao() { }
        public UserRoleDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public UserRole Get(Int32 RoleID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_RoleID, RoleID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (UserRole)_helper.DataRowToObject(row, new UserRole());
        }
        public int Insert(UserRole record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(UserRole record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 RoleID)
        {
            UserRole record;
            if (_ctx.Transaction == null)
                record = new UserRoleDao().Get(RoleID);
            else
                record = Get(RoleID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region UserRoleMenu
    [Serializable]
    [Table(Name = "UserRoleMenu")]
    public class UserRoleMenu : DbDataModel
    {
        private Int32 _ID;
        private Int32 _MenuID;
        private Int32 _RoleID;
        private String _CRUDMode;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "RoleID", DataType = "Int32")]
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class UserRoleMenuDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(UserRoleMenu));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public UserRoleMenuDao() { }
        public UserRoleMenuDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public UserRoleMenu Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (UserRoleMenu)_helper.DataRowToObject(row, new UserRoleMenu());
        }
        public int Insert(UserRoleMenu record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(UserRoleMenu record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            UserRoleMenu record;
            if (_ctx.Transaction == null)
                record = new UserRoleMenuDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region Venue
    [Serializable]
    [Table(Name = "Venue")]
    public class Venue : DbDataModel
    {
        private Int32 _VenueID;
        private String _VenueCode;
        private String _VenueName;
        private Int32 _AddressID;
        private String _Remarks;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "VenueID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
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
        [Column(Name = "Remarks", DataType = "String", IsNullable = true)]
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
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class VenueDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(Venue));
        private bool _isAuditLog = false;
        private const string p_VenueID = "@p_VenueID";
        public VenueDao() { }
        public VenueDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public Venue Get(Int32 VenueID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_VenueID, VenueID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (Venue)_helper.DataRowToObject(row, new Venue());
        }
        public int Insert(Venue record)
        {
            record.CreatedDate = DateTime.Now;
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(Venue record)
        {
            record.LastUpdatedDate = DateTime.Now;
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 VenueID)
        {
            Venue record;
            if (_ctx.Transaction == null)
                record = new VenueDao().Get(VenueID);
            else
                record = Get(VenueID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region ZipCodes
    [Serializable]
    [Table(Name = "ZipCodes")]
    public class ZipCodes : DbDataModel
    {
        private Int32 _ID;
        private String _ZipCode;
        private String _StreetName;
        private String _District;
        private String _County;
        private String _City;
        private String _GCProvince;
        private Decimal _Longitude;
        private Decimal _Latitude;
        private Boolean _IsDeleted;
        private Int32? _CreatedBy;
        private DateTime _CreatedDate;
        private Int32? _LastUpdatedBy;
        private DateTime _LastUpdatedDate;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "ZipCode", DataType = "String")]
        public String ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
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
        [Column(Name = "Longitude", DataType = "Decimal", IsNullable = true)]
        public Decimal Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }
        [Column(Name = "Latitude", DataType = "Decimal", IsNullable = true)]
        public Decimal Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }
        [Column(Name = "IsDeleted", DataType = "Boolean", IsNullable = true)]
        public Boolean IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }
        [Column(Name = "CreatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        [Column(Name = "CreatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        [Column(Name = "LastUpdatedBy", DataType = "Int32", IsNullable = true)]
        public Int32? LastUpdatedBy
        {
            get { return _LastUpdatedBy; }
            set { _LastUpdatedBy = value; }
        }
        [Column(Name = "LastUpdatedDate", DataType = "DateTime", IsNullable = true)]
        public DateTime LastUpdatedDate
        {
            get { return _LastUpdatedDate; }
            set { _LastUpdatedDate = value; }
        }
    }

    public class ZipCodesDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(ZipCodes));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public ZipCodesDao() { }
        public ZipCodesDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public ZipCodes Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (ZipCodes)_helper.DataRowToObject(row, new ZipCodes());
        }
        public int Insert(ZipCodes record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(ZipCodes record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            ZipCodes record;
            if (_ctx.Transaction == null)
                record = new ZipCodesDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #endregion


    #region Tools
    #region MigrationConfigurationDt
    [Serializable]
    [Table(Name = "MigrationConfigurationDt")]
    public partial class MigrationConfigurationDt : DbDataModel
    {
        private Int32 _ID;
        private Int32 _HeaderID;
        private String _TableName;
        private String _LinkColumn;
        private String _ColumnName;
        private String _ColumnCaption;
        private Boolean _IsVisible;
        private String _FromColumn;
        private String _DefaultValue;
        private Boolean _IsRequired;
        private String _Type;
        private String _MethodName;
        private String _ValueField;
        private String _TextField;
        private String _FilterExpression;
        private String _ValueChecked;
        private String _ValueUnchecked;
        private Boolean _OtherValue;
        private String _FormatDate;
        private String _SearchDialogType;
        private String _SearchDialogMethodName;
        private String _SearchDialogFilterExpression;
        private String _SearchDialogIDField;
        private String _SearchDialogCodeField;
        private String _SearchDialogNameField;
        private String _IDColumn;
        private String _FormatCode;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "HeaderID", DataType = "Int32")]
        public Int32 HeaderID
        {
            get { return _HeaderID; }
            set { _HeaderID = value; }
        }
        [Column(Name = "TableName", DataType = "String")]
        public String TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        [Column(Name = "LinkColumn", DataType = "String", IsNullable = true)]
        public String LinkColumn
        {
            get { return _LinkColumn; }
            set { _LinkColumn = value; }
        }
        [Column(Name = "ColumnName", DataType = "String")]
        public String ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }
        [Column(Name = "ColumnCaption", DataType = "String", IsNullable = true)]
        public String ColumnCaption
        {
            get { return _ColumnCaption; }
            set { _ColumnCaption = value; }
        }
        [Column(Name = "IsVisible", DataType = "Boolean")]
        public Boolean IsVisible
        {
            get { return _IsVisible; }
            set { _IsVisible = value; }
        }
        [Column(Name = "FromColumn", DataType = "String", IsNullable = true)]
        public String FromColumn
        {
            get { return _FromColumn; }
            set { _FromColumn = value; }
        }
        [Column(Name = "DefaultValue", DataType = "String", IsNullable = true)]
        public String DefaultValue
        {
            get { return _DefaultValue; }
            set { _DefaultValue = value; }
        }
        [Column(Name = "IsRequired", DataType = "Boolean")]
        public Boolean IsRequired
        {
            get { return _IsRequired; }
            set { _IsRequired = value; }
        }
        [Column(Name = "Type", DataType = "String")]
        public String Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        [Column(Name = "MethodName", DataType = "String", IsNullable = true)]
        public String MethodName
        {
            get { return _MethodName; }
            set { _MethodName = value; }
        }
        [Column(Name = "ValueField", DataType = "String", IsNullable = true)]
        public String ValueField
        {
            get { return _ValueField; }
            set { _ValueField = value; }
        }
        [Column(Name = "TextField", DataType = "String", IsNullable = true)]
        public String TextField
        {
            get { return _TextField; }
            set { _TextField = value; }
        }
        [Column(Name = "FilterExpression", DataType = "String", IsNullable = true)]
        public String FilterExpression
        {
            get { return _FilterExpression; }
            set { _FilterExpression = value; }
        }
        [Column(Name = "ValueChecked", DataType = "String", IsNullable = true)]
        public String ValueChecked
        {
            get { return _ValueChecked; }
            set { _ValueChecked = value; }
        }
        [Column(Name = "ValueUnchecked", DataType = "String", IsNullable = true)]
        public String ValueUnchecked
        {
            get { return _ValueUnchecked; }
            set { _ValueUnchecked = value; }
        }
        [Column(Name = "OtherValue", DataType = "Boolean", IsNullable = true)]
        public Boolean OtherValue
        {
            get { return _OtherValue; }
            set { _OtherValue = value; }
        }
        [Column(Name = "FormatDate", DataType = "String", IsNullable = true)]
        public String FormatDate
        {
            get { return _FormatDate; }
            set { _FormatDate = value; }
        }
        [Column(Name = "SearchDialogType", DataType = "String", IsNullable = true)]
        public String SearchDialogType
        {
            get { return _SearchDialogType; }
            set { _SearchDialogType = value; }
        }
        [Column(Name = "SearchDialogMethodName", DataType = "String", IsNullable = true)]
        public String SearchDialogMethodName
        {
            get { return _SearchDialogMethodName; }
            set { _SearchDialogMethodName = value; }
        }
        [Column(Name = "SearchDialogFilterExpression", DataType = "String", IsNullable = true)]
        public String SearchDialogFilterExpression
        {
            get { return _SearchDialogFilterExpression; }
            set { _SearchDialogFilterExpression = value; }
        }
        [Column(Name = "SearchDialogIDField", DataType = "String", IsNullable = true)]
        public String SearchDialogIDField
        {
            get { return _SearchDialogIDField; }
            set { _SearchDialogIDField = value; }
        }
        [Column(Name = "SearchDialogCodeField", DataType = "String", IsNullable = true)]
        public String SearchDialogCodeField
        {
            get { return _SearchDialogCodeField; }
            set { _SearchDialogCodeField = value; }
        }
        [Column(Name = "SearchDialogNameField", DataType = "String", IsNullable = true)]
        public String SearchDialogNameField
        {
            get { return _SearchDialogNameField; }
            set { _SearchDialogNameField = value; }
        }
        [Column(Name = "IDColumn", DataType = "String", IsNullable = true)]
        public String IDColumn
        {
            get { return _IDColumn; }
            set { _IDColumn = value; }
        }
        [Column(Name = "FormatCode", DataType = "String", IsNullable = true)]
        public String FormatCode
        {
            get { return _FormatCode; }
            set { _FormatCode = value; }
        }
    }

    public class MigrationConfigurationDtDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MigrationConfigurationDt));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public MigrationConfigurationDtDao() { }
        public MigrationConfigurationDtDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MigrationConfigurationDt Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MigrationConfigurationDt)_helper.DataRowToObject(row, new MigrationConfigurationDt());
        }
        public int Insert(MigrationConfigurationDt record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MigrationConfigurationDt record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            MigrationConfigurationDt record;
            if (_ctx.Transaction == null)
                record = new MigrationConfigurationDtDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region MigrationConfigurationHd
    [Serializable]
    [Table(Name = "MigrationConfigurationHd")]
    public class MigrationConfigurationHd : DbDataModel
    {
        private Int32 _ID;
        private String _FromTable;
        private String _ToTable;
        private String _GridColumns;

        [Column(Name = "ID", DataType = "Int32", IsPrimaryKey = true, IsIdentity = true)]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column(Name = "FromTable", DataType = "String")]
        public String FromTable
        {
            get { return _FromTable; }
            set { _FromTable = value; }
        }
        [Column(Name = "ToTable", DataType = "String")]
        public String ToTable
        {
            get { return _ToTable; }
            set { _ToTable = value; }
        }
        [Column(Name = "GridColumns", DataType = "String", IsNullable = true)]
        public String GridColumns
        {
            get { return _GridColumns; }
            set { _GridColumns = value; }
        }
    }

    public class MigrationConfigurationHdDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MigrationConfigurationHd));
        private bool _isAuditLog = false;
        private const string p_ID = "@p_ID";
        public MigrationConfigurationHdDao() { }
        public MigrationConfigurationHdDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MigrationConfigurationHd Get(Int32 ID)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ID, ID);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MigrationConfigurationHd)_helper.DataRowToObject(row, new MigrationConfigurationHd());
        }
        public int Insert(MigrationConfigurationHd record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MigrationConfigurationHd record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 ID)
        {
            MigrationConfigurationHd record;
            if (_ctx.Transaction == null)
                record = new MigrationConfigurationHdDao().Get(ID);
            else
                record = Get(ID);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region MigrationConfigurationTableLink
    [Serializable]
    [Table(Name = "MigrationConfigurationTableLink")]
    public class MigrationConfigurationTableLink : DbDataModel
    {
        private Int32 _HeaderID;
        private String _TableName;
        private String _ColumnName;
        private String _LinkTableName;
        private String _LinkTableColumn;
        private Boolean _IsOneToMany;
        private String _RepeaterTable;
        private String _RepeaterFilterExpression;
        private String _RepeaterIDValue;
        private String _RepeaterLabelValue;
        private String _DtColumnID;
        private String _DtColumnValue;

        [Column(Name = "HeaderID", DataType = "Int32", IsPrimaryKey = true)]
        public Int32 HeaderID
        {
            get { return _HeaderID; }
            set { _HeaderID = value; }
        }
        [Column(Name = "TableName", DataType = "String", IsPrimaryKey = true)]
        public String TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        [Column(Name = "ColumnName", DataType = "String", IsPrimaryKey = true)]
        public String ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }
        [Column(Name = "LinkTableName", DataType = "String")]
        public String LinkTableName
        {
            get { return _LinkTableName; }
            set { _LinkTableName = value; }
        }
        [Column(Name = "LinkTableColumn", DataType = "String")]
        public String LinkTableColumn
        {
            get { return _LinkTableColumn; }
            set { _LinkTableColumn = value; }
        }
        [Column(Name = "IsOneToMany", DataType = "Boolean")]
        public Boolean IsOneToMany
        {
            get { return _IsOneToMany; }
            set { _IsOneToMany = value; }
        }
        [Column(Name = "RepeaterTable", DataType = "String", IsNullable = true)]
        public String RepeaterTable
        {
            get { return _RepeaterTable; }
            set { _RepeaterTable = value; }
        }
        [Column(Name = "RepeaterFilterExpression", DataType = "String", IsNullable = true)]
        public String RepeaterFilterExpression
        {
            get { return _RepeaterFilterExpression; }
            set { _RepeaterFilterExpression = value; }
        }
        [Column(Name = "RepeaterIDValue", DataType = "String", IsNullable = true)]
        public String RepeaterIDValue
        {
            get { return _RepeaterIDValue; }
            set { _RepeaterIDValue = value; }
        }
        [Column(Name = "RepeaterLabelValue", DataType = "String", IsNullable = true)]
        public String RepeaterLabelValue
        {
            get { return _RepeaterLabelValue; }
            set { _RepeaterLabelValue = value; }
        }
        [Column(Name = "DtColumnID", DataType = "String", IsNullable = true)]
        public String DtColumnID
        {
            get { return _DtColumnID; }
            set { _DtColumnID = value; }
        }
        [Column(Name = "DtColumnValue", DataType = "String", IsNullable = true)]
        public String DtColumnValue
        {
            get { return _DtColumnValue; }
            set { _DtColumnValue = value; }
        }
    }

    public class MigrationConfigurationTableLinkDao
    {
        private readonly IDbContext _ctx = DbFactory.Configure();
        private readonly DbHelper _helper = new DbHelper(typeof(MigrationConfigurationTableLink));
        private bool _isAuditLog = false;
        private const string p_ColumnName = "@p_ColumnName";
        private const string p_HeaderID = "@p_HeaderID";
        private const string p_TableName = "@p_TableName";
        public MigrationConfigurationTableLinkDao() { }
        public MigrationConfigurationTableLinkDao(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public MigrationConfigurationTableLink Get(Int32 HeaderID, String TableName, String ColumnName)
        {
            _ctx.CommandText = _helper.GetRecord();
            _ctx.Add(p_ColumnName, ColumnName);
            _ctx.Add(p_HeaderID, HeaderID);
            _ctx.Add(p_TableName, TableName);
            DataRow row = DaoBase.GetDataRow(_ctx);
            return (row == null) ? null : (MigrationConfigurationTableLink)_helper.DataRowToObject(row, new MigrationConfigurationTableLink());
        }
        public int Insert(MigrationConfigurationTableLink record)
        {
            _helper.Insert(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
        public int Update(MigrationConfigurationTableLink record)
        {
            _helper.Update(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx, true);
        }
        public int Delete(Int32 HeaderID, String TableName, String ColumnName)
        {
            MigrationConfigurationTableLink record;
            if (_ctx.Transaction == null)
                record = new MigrationConfigurationTableLinkDao().Get(HeaderID, TableName, ColumnName);
            else
                record = Get(HeaderID, TableName, ColumnName);
            _helper.Delete(_ctx, record, _isAuditLog);
            return DaoBase.ExecuteNonQuery(_ctx);
        }
    }
    #endregion
    #region SysColumns
    [Serializable]
    [Table(Name = "Sys.columns")]
    public class SysColumns : DbDataModel
    {
        private String _Name;
        private Int32 _UserTypeID;
        private Boolean _IsNullable;
        private Boolean _IsIdentity;

        [Column(Name = "name", DataType = "String")]
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [Column(Name = "user_type_id", DataType = "String")]
        public Int32 UserTypeID
        {
            get { return _UserTypeID; }
            set { _UserTypeID = value; }
        }

        [Column(Name = "is_nullable", DataType = "Boolean")]
        public Boolean IsNullable
        {
            get { return _IsNullable; }
            set { _IsNullable = value; }
        }

        [Column(Name = "is_identity", DataType = "Boolean")]
        public Boolean IsIdentity
        {
            get { return _IsIdentity; }
            set { _IsIdentity = value; }
        }

        public String Type
        {
            get
            {
                switch (_UserTypeID)
                {
                    case 48: return "Int16";
                    case 56: return "Int32";
                    case 104: return "Boolean";
                    case 61:
                    case 40: return "DateTime";
                    case 62: return "Double";
                    case 108:
                    case 60: return "Decimal";
                    case 52: return "Int16";
                    case 127: return "Int64";
                }
                return "String";
            }
        }
    }
    #endregion
    #region SysObjects
    [Serializable]
    [Table(Name = "Sys.objects")]
    public class SysObjects : DbDataModel
    {
        private String _Name;
        private Int32 _ObjectID;

        [Column(Name = "name", DataType = "String")]
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [Column(Name = "object_id", DataType = "Int32")]
        public Int32 ObjectID
        {
            get { return _ObjectID; }
            set { _ObjectID = value; }
        }
    }
    #endregion
    #endregion
}
