using System;
using System.Collections.Generic;
using System.Data;
using QIS.Data.Core.Dal;
using System.Linq;

namespace QIS.Careq.Data.Service
{
    public static partial class BusinessLayer
    {
        #region MEMENTO Tables
        #region Address
        public static Address GetAddress(Int32 AddressID)
        {
            return new AddressDao().Get(AddressID);
        }
        public static int InsertAddress(Address record)
        {
            return new AddressDao().Insert(record);
        }
        public static int UpdateAddress(Address record)
        {
            return new AddressDao().Update(record);
        }
        public static int DeleteAddress(Int32 AddressID)
        {
            return new AddressDao().Delete(AddressID);
        }
        public static List<Address> GetAddressList(string filterExpression)
        {
            List<Address> result = new List<Address>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Address));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Address)helper.IDataReaderToObject(reader, new Address()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetAddressMaxID(IDbContext ctx = null)
        {
            Int32 result = 0;
            if (ctx == null)
                ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Address));
                ctx.CommandText = helper.SelectMaxColumn("AddressID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Application
        public static Application GetApplication(Int32 ApplicationID)
        {
            return new ApplicationDao().Get(ApplicationID);
        }
        public static int InsertApplication(Application record)
        {
            return new ApplicationDao().Insert(record);
        }
        public static int UpdateApplication(Application record)
        {
            return new ApplicationDao().Update(record);
        }
        public static int DeleteApplication(Int32 ApplicationID)
        {
            return new ApplicationDao().Delete(ApplicationID);
        }
        public static List<Application> GetApplicationList(string filterExpression)
        {
            List<Application> result = new List<Application>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Application));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Application)helper.IDataReaderToObject(reader, new Application()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Company
        public static Company GetCompany(Int32 CompanyID)
        {
            return new CompanyDao().Get(CompanyID);
        }
        public static int InsertCompany(Company record)
        {
            return new CompanyDao().Insert(record);
        }
        public static int UpdateCompany(Company record)
        {
            return new CompanyDao().Update(record);
        }
        public static int DeleteCompany(Int32 CompanyID)
        {
            return new CompanyDao().Delete(CompanyID);
        }
        public static List<Company> GetCompanyList(string filterExpression)
        {
            List<Company> result = new List<Company>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Company));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Company)helper.IDataReaderToObject(reader, new Company()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCompanyRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Company));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "CompanyID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetCompanyMaxID(IDbContext ctx = null)
        {
            Int32 result = 0;
            if (ctx == null)
                ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Company));
                ctx.CommandText = helper.SelectMaxColumn("CompanyID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region CompanyCertification
        public static CompanyCertification GetCompanyCertification(Int32 CompanyID, String GCCompanyCertification)
        {
            return new CompanyCertificationDao().Get(CompanyID, GCCompanyCertification);
        }
        public static int InsertCompanyCertification(CompanyCertification record)
        {
            return new CompanyCertificationDao().Insert(record);
        }
        public static int UpdateCompanyCertification(CompanyCertification record)
        {
            return new CompanyCertificationDao().Update(record);
        }
        public static int DeleteCompanyCertification(Int32 CompanyID, String GCCompanyCertification)
        {
            return new CompanyCertificationDao().Delete(CompanyID, GCCompanyCertification);
        }
        public static List<CompanyCertification> GetCompanyCertificationList(string filterExpression)
        {
            List<CompanyCertification> result = new List<CompanyCertification>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CompanyCertification));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CompanyCertification)helper.IDataReaderToObject(reader, new CompanyCertification()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region CompanyContactPerson
        public static CompanyContactPerson GetCompanyContactPerson(Int32 ID)
        {
            return new CompanyContactPersonDao().Get(ID);
        }
        public static int InsertCompanyContactPerson(CompanyContactPerson record)
        {
            return new CompanyContactPersonDao().Insert(record);
        }
        public static int UpdateCompanyContactPerson(CompanyContactPerson record)
        {
            return new CompanyContactPersonDao().Update(record);
        }
        public static int DeleteCompanyContactPerson(Int32 ID)
        {
            return new CompanyContactPersonDao().Delete(ID);
        }
        public static List<CompanyContactPerson> GetCompanyContactPersonList(string filterExpression)
        {
            List<CompanyContactPerson> result = new List<CompanyContactPerson>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(CompanyContactPerson));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((CompanyContactPerson)helper.IDataReaderToObject(reader, new CompanyContactPerson()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Employee
        public static Employee GetEmployee(Int32 EmployeeID)
        {
            return new EmployeeDao().Get(EmployeeID);
        }
        public static int InsertEmployee(Employee record)
        {
            return new EmployeeDao().Insert(record);
        }
        public static int UpdateEmployee(Employee record)
        {
            return new EmployeeDao().Update(record);
        }
        public static int DeleteEmployee(Int32 EmployeeID)
        {
            return new EmployeeDao().Delete(EmployeeID);
        }
        public static List<Employee> GetEmployeeList(string filterExpression)
        {
            List<Employee> result = new List<Employee>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Employee));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Employee)helper.IDataReaderToObject(reader, new Employee()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Event
        public static Event GetEvent(Int32 EventID)
        {
            return new EventDao().Get(EventID);
        }
        public static int InsertEvent(Event record)
        {
            return new EventDao().Insert(record);
        }
        public static int UpdateEvent(Event record)
        {
            return new EventDao().Update(record);
        }
        public static int DeleteEvent(Int32 EventID)
        {
            return new EventDao().Delete(EventID);
        }
        public static List<Event> GetEventList(string filterExpression)
        {
            List<Event> result = new List<Event>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Event));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Event)helper.IDataReaderToObject(reader, new Event()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region EventCompanyDt
        public static EventCompanyDt GetEventCompanyDt(Int32 EventID, Int32 CompanyID)
        {
            return new EventCompanyDtDao().Get(EventID, CompanyID);
        }
        public static int InsertEventCompanyDt(EventCompanyDt record)
        {
            return new EventCompanyDtDao().Insert(record);
        }
        public static int UpdateEventCompanyDt(EventCompanyDt record)
        {
            return new EventCompanyDtDao().Update(record);
        }
        public static int DeleteEventCompanyDt(Int32 EventID, Int32 CompanyID)
        {
            return new EventCompanyDtDao().Delete(EventID, CompanyID);
        }
        public static List<EventCompanyDt> GetEventCompanyDtList(string filterExpression)
        {
            List<EventCompanyDt> result = new List<EventCompanyDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(EventCompanyDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((EventCompanyDt)helper.IDataReaderToObject(reader, new EventCompanyDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region EventInvitation
        public static EventInvitation GetEventInvitation(Int32 EventID, Int32 MemberID)
        {
            return new EventInvitationDao().Get(EventID, MemberID);
        }
        public static int InsertEventInvitation(EventInvitation record)
        {
            return new EventInvitationDao().Insert(record);
        }
        public static int UpdateEventInvitation(EventInvitation record)
        {
            return new EventInvitationDao().Update(record);
        }
        public static int DeleteEventInvitation(Int32 EventID, Int32 MemberID)
        {
            return new EventInvitationDao().Delete(EventID, MemberID);
        }
        public static List<EventInvitation> GetEventInvitationList(string filterExpression)
        {
            List<EventInvitation> result = new List<EventInvitation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(EventInvitation));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((EventInvitation)helper.IDataReaderToObject(reader, new EventInvitation()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region EventRegistration
        public static EventRegistration GetEventRegistration(Int32 EventID, Int32 MemberID)
        {
            return new EventRegistrationDao().Get(EventID, MemberID);
        }
        public static int InsertEventRegistration(EventRegistration record)
        {
            return new EventRegistrationDao().Insert(record);
        }
        public static int UpdateEventRegistration(EventRegistration record)
        {
            return new EventRegistrationDao().Update(record);
        }
        public static int DeleteEventRegistration(Int32 EventID, Int32 MemberID)
        {
            return new EventRegistrationDao().Delete(EventID, MemberID);
        }
        public static List<EventRegistration> GetEventRegistrationList(string filterExpression)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetEventRegistrationList(filterExpression, ctx);
        }
        public static List<EventRegistration> GetEventRegistrationList(string filterExpression, IDbContext ctx)
        {
            List<EventRegistration> result = new List<EventRegistration>();
            try
            {
                DbHelper helper = new DbHelper(typeof(EventRegistration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((EventRegistration)helper.IDataReaderToObject(reader, new EventRegistration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region FilterParameter
        public static FilterParameter GetFilterParameter(Int32 FilterParameterID)
        {
            return new FilterParameterDao().Get(FilterParameterID);
        }
        public static int InsertFilterParameter(FilterParameter record)
        {
            return new FilterParameterDao().Insert(record);
        }
        public static int UpdateFilterParameter(FilterParameter record)
        {
            return new FilterParameterDao().Update(record);
        }
        public static int DeleteFilterParameter(Int32 FilterParameterID)
        {
            return new FilterParameterDao().Delete(FilterParameterID);
        }
        public static List<FilterParameter> GetFilterParameterList(string filterExpression)
        {
            List<FilterParameter> result = new List<FilterParameter>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(FilterParameter));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((FilterParameter)helper.IDataReaderToObject(reader, new FilterParameter()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Lead
        public static Lead GetLead(Int32 LeadID)
        {
            return new LeadDao().Get(LeadID);
        }
        public static int InsertLead(Lead record)
        {
            return new LeadDao().Insert(record);
        }
        public static int UpdateLead(Lead record)
        {
            return new LeadDao().Update(record);
        }
        public static int DeleteLead(Int32 LeadID)
        {
            return new LeadDao().Delete(LeadID);
        }
        public static List<Lead> GetLeadList(string filterExpression)
        {
            List<Lead> result = new List<Lead>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Lead));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Lead)helper.IDataReaderToObject(reader, new Lead()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region LeadActivityLog
        public static LeadActivityLog GetLeadActivityLog(Int32 ID)
        {
            return new LeadActivityLogDao().Get(ID);
        }
        public static int InsertLeadActivityLog(LeadActivityLog record)
        {
            return new LeadActivityLogDao().Insert(record);
        }
        public static int UpdateLeadActivityLog(LeadActivityLog record)
        {
            return new LeadActivityLogDao().Update(record);
        }
        public static int DeleteLeadActivityLog(Int32 ID)
        {
            return new LeadActivityLogDao().Delete(ID);
        }
        public static List<LeadActivityLog> GetLeadActivityLogList(string filterExpression)
        {
            List<LeadActivityLog> result = new List<LeadActivityLog>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(LeadActivityLog));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((LeadActivityLog)helper.IDataReaderToObject(reader, new LeadActivityLog()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region LOBClassification
        public static LOBClassification GetLOBClassification(Int32 LOBClassID)
        {
            return new LOBClassificationDao().Get(LOBClassID);
        }
        public static int InsertLOBClassification(LOBClassification record)
        {
            return new LOBClassificationDao().Insert(record);
        }
        public static int UpdateLOBClassification(LOBClassification record)
        {
            return new LOBClassificationDao().Update(record);
        }
        public static int DeleteLOBClassification(Int32 LOBClassID)
        {
            return new LOBClassificationDao().Delete(LOBClassID);
        }
        public static List<LOBClassification> GetLOBClassificationList(string filterExpression)
        {
            List<LOBClassification> result = new List<LOBClassification>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(LOBClassification));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((LOBClassification)helper.IDataReaderToObject(reader, new LOBClassification()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Member
        public static Member GetMember(Int32 MemberID)
        {
            return new MemberDao().Get(MemberID);
        }
        public static int InsertMember(Member record)
        {
            return new MemberDao().Insert(record);
        }
        public static int UpdateMember(Member record)
        {
            return new MemberDao().Update(record);
        }
        public static int DeleteMember(Int32 MemberID)
        {
            return new MemberDao().Delete(MemberID);
        }
        public static List<Member> GetMemberList(string filterExpression)
        {
            List<Member> result = new List<Member>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Member));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Member)helper.IDataReaderToObject(reader, new Member()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMemberRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Member));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "MemberID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMemberMaxID(IDbContext ctx = null)
        {
            Int32 result = 0;
            if (ctx == null)
                ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Member));
                ctx.CommandText = helper.SelectMaxColumn("MemberID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region MemberGreetings
        public static MemberGreetings GetMemberGreetings(Int32 MemberID, String GCGreetingType)
        {
            return new MemberGreetingsDao().Get(MemberID, GCGreetingType);
        }
        public static int InsertMemberGreetings(MemberGreetings record)
        {
            return new MemberGreetingsDao().Insert(record);
        }
        public static int UpdateMemberGreetings(MemberGreetings record)
        {
            return new MemberGreetingsDao().Update(record);
        }
        public static int DeleteMemberGreetings(Int32 MemberID, String GCGreetingType)
        {
            return new MemberGreetingsDao().Delete(MemberID, GCGreetingType);
        }
        public static List<MemberGreetings> GetMemberGreetingsList(string filterExpression)
        {
            List<MemberGreetings> result = new List<MemberGreetings>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MemberGreetings));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MemberGreetings)helper.IDataReaderToObject(reader, new MemberGreetings()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region MemberPastTraining
        public static MemberPastTraining GetMemberPastTraining(Int32 ID)
        {
            return new MemberPastTrainingDao().Get(ID);
        }
        public static int InsertMemberPastTraining(MemberPastTraining record)
        {
            return new MemberPastTrainingDao().Insert(record);
        }
        public static int UpdateMemberPastTraining(MemberPastTraining record)
        {
            return new MemberPastTrainingDao().Update(record);
        }
        public static int DeleteMemberPastTraining(Int32 ID)
        {
            return new MemberPastTrainingDao().Delete(ID);
        }
        public static Int32 GetMemberPastTrainingMaxID(IDbContext ctx = null)
        {
            Int32 result = 0;
            if (ctx == null)
                ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MemberPastTraining));
                ctx.CommandText = helper.SelectMaxColumn("ID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<MemberPastTraining> GetMemberPastTrainingList(string filterExpression)
        {
            List<MemberPastTraining> result = new List<MemberPastTraining>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MemberPastTraining));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MemberPastTraining)helper.IDataReaderToObject(reader, new MemberPastTraining()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region MenuMaster
        public static MenuMaster GetMenuMaster(Int32 MenuID)
        {
            return new MenuMasterDao().Get(MenuID);
        }
        public static int InsertMenuMaster(MenuMaster record)
        {
            return new MenuMasterDao().Insert(record);
        }
        public static int UpdateMenuMaster(MenuMaster record)
        {
            return new MenuMasterDao().Update(record);
        }
        public static int DeleteMenuMaster(Int32 MenuID)
        {
            return new MenuMasterDao().Delete(MenuID);
        }
        public static List<MenuMaster> GetMenuMasterList(string filterExpression)
        {
            List<MenuMaster> result = new List<MenuMaster>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MenuMaster));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MenuMaster)helper.IDataReaderToObject(reader, new MenuMaster()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Module
        public static Module GetModule(String ModuleID)
        {
            return new ModuleDao().Get(ModuleID);
        }
        public static int InsertModule(Module record)
        {
            return new ModuleDao().Insert(record);
        }
        public static int UpdateModule(Module record)
        {
            return new ModuleDao().Update(record);
        }
        public static int DeleteModule(String ModuleID)
        {
            return new ModuleDao().Delete(ModuleID);
        }
        public static List<Module> GetModuleList(string filterExpression)
        {
            List<Module> result = new List<Module>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Module));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Module)helper.IDataReaderToObject(reader, new Module()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Occupation
        public static Occupation GetOccupation(Int32 OccupationID)
        {
            return new OccupationDao().Get(OccupationID);
        }
        public static int InsertOccupation(Occupation record)
        {
            return new OccupationDao().Insert(record);
        }
        public static int UpdateOccupation(Occupation record)
        {
            return new OccupationDao().Update(record);
        }
        public static int DeleteOccupation(Int32 OccupationID)
        {
            return new OccupationDao().Delete(OccupationID);
        }
        public static List<Occupation> GetOccupationList(string filterExpression)
        {
            List<Occupation> result = new List<Occupation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Occupation));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Occupation)helper.IDataReaderToObject(reader, new Occupation()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Region
        public static Region GetRegion(Int32 RegionID)
        {
            return new RegionDao().Get(RegionID);
        }
        public static int InsertRegion(Region record)
        {
            return new RegionDao().Insert(record);
        }
        public static int UpdateRegion(Region record)
        {
            return new RegionDao().Update(record);
        }
        public static int DeleteRegion(Int32 RegionID)
        {
            return new RegionDao().Delete(RegionID);
        }
        public static List<Region> GetRegionList(string filterExpression)
        {
            List<Region> result = new List<Region>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Region));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Region)helper.IDataReaderToObject(reader, new Region()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<Region> GetRegionList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<Region> result = new List<Region>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Region));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Region)helper.IDataReaderToObject(reader, new Region()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRegionRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Region));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "RegionID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRegionRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Region));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ReportMaster
        public static ReportMaster GetReportMaster(Int32 ReportID)
        {
            return new ReportMasterDao().Get(ReportID);
        }
        public static int InsertReportMaster(ReportMaster record)
        {
            return new ReportMasterDao().Insert(record);
        }
        public static int UpdateReportMaster(ReportMaster record)
        {
            return new ReportMasterDao().Update(record);
        }
        public static int DeleteReportMaster(Int32 ReportID)
        {
            return new ReportMasterDao().Delete(ReportID);
        }
        public static List<ReportMaster> GetReportMasterList(string filterExpression)
        {
            List<ReportMaster> result = new List<ReportMaster>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ReportMaster));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ReportMaster)helper.IDataReaderToObject(reader, new ReportMaster()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ReportParameter
        public static ReportParameter GetReportParameter(Int32 ReportID, Int32 FilterParameterID)
        {
            return new ReportParameterDao().Get(ReportID, FilterParameterID);
        }
        public static int InsertReportParameter(ReportParameter record)
        {
            return new ReportParameterDao().Insert(record);
        }
        public static int UpdateReportParameter(ReportParameter record)
        {
            return new ReportParameterDao().Update(record);
        }
        public static int DeleteReportParameter(Int32 ReportID, Int32 FilterParameterID)
        {
            return new ReportParameterDao().Delete(ReportID, FilterParameterID);
        }
        public static List<ReportParameter> GetReportParameterList(string filterExpression)
        {
            List<ReportParameter> result = new List<ReportParameter>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ReportParameter));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ReportParameter)helper.IDataReaderToObject(reader, new ReportParameter()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region RestoreDataConfiguration
        public static RestoreDataConfiguration GetRestoreDataConfiguration()
        {
            return new RestoreDataConfigurationDao().Get();
        }
        public static int InsertRestoreDataConfiguration(RestoreDataConfiguration record)
        {
            return new RestoreDataConfigurationDao().Insert(record);
        }
        public static int UpdateRestoreDataConfiguration(RestoreDataConfiguration record)
        {
            return new RestoreDataConfigurationDao().Update(record);
        }
        public static int DeleteRestoreDataConfiguration()
        {
            return new RestoreDataConfigurationDao().Delete();
        }
        public static List<RestoreDataConfiguration> GetRestoreDataConfigurationList(string filterExpression)
        {
            List<RestoreDataConfiguration> result = new List<RestoreDataConfiguration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RestoreDataConfiguration));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RestoreDataConfiguration)helper.IDataReaderToObject(reader, new RestoreDataConfiguration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<RestoreDataConfiguration> GetRestoreDataConfigurationList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<RestoreDataConfiguration> result = new List<RestoreDataConfiguration>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RestoreDataConfiguration));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((RestoreDataConfiguration)helper.IDataReaderToObject(reader, new RestoreDataConfiguration()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetRestoreDataConfigurationRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(RestoreDataConfiguration));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SettingParameter
        public static SettingParameter GetSettingParameter(String ParameterCode)
        {
            return new SettingParameterDao().Get(ParameterCode);
        }
        public static int InsertSettingParameter(SettingParameter record)
        {
            return new SettingParameterDao().Insert(record);
        }
        public static int UpdateSettingParameter(SettingParameter record)
        {
            return new SettingParameterDao().Update(record);
        }
        public static int DeleteSettingParameter(String ParameterCode)
        {
            return new SettingParameterDao().Delete(ParameterCode);
        }
        public static List<SettingParameter> GetSettingParameterList(string filterExpression)
        {
            List<SettingParameter> result = new List<SettingParameter>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SettingParameter));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SettingParameter)helper.IDataReaderToObject(reader, new SettingParameter()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<SettingParameter> GetSettingParameterList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<SettingParameter> result = new List<SettingParameter>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SettingParameter));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SettingParameter)helper.IDataReaderToObject(reader, new SettingParameter()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetSettingParameterRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SettingParameter));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region StandardCode
        public static StandardCode GetStandardCode(String StandardCodeID)
        {
            return new StandardCodeDao().Get(StandardCodeID);
        }
        public static int InsertStandardCode(StandardCode record)
        {
            return new StandardCodeDao().Insert(record);
        }
        public static int UpdateStandardCode(StandardCode record)
        {
            return new StandardCodeDao().Update(record);
        }
        public static int DeleteStandardCode(String StandardCodeID)
        {
            return new StandardCodeDao().Delete(StandardCodeID);
        }
        public static List<StandardCode> GetStandardCodeList(string filterExpression)
        {
            List<StandardCode> result = new List<StandardCode>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StandardCode));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StandardCode)helper.IDataReaderToObject(reader, new StandardCode()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<StandardCode> GetStandardCodeList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<StandardCode> result = new List<StandardCode>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StandardCode));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((StandardCode)helper.IDataReaderToObject(reader, new StandardCode()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetStandardCodeRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(StandardCode));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TemplateText
        public static TemplateText GetTemplateText(Int32 TemplateID)
        {
            return new TemplateTextDao().Get(TemplateID);
        }
        public static int InsertTemplateText(TemplateText record)
        {
            return new TemplateTextDao().Insert(record);
        }
        public static int UpdateTemplateText(TemplateText record)
        {
            return new TemplateTextDao().Update(record);
        }
        public static int DeleteTemplateText(Int32 TemplateID)
        {
            return new TemplateTextDao().Delete(TemplateID);
        }
        public static List<TemplateText> GetTemplateTextList(string filterExpression)
        {
            List<TemplateText> result = new List<TemplateText>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TemplateText));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TemplateText)helper.IDataReaderToObject(reader, new TemplateText()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Trainer
        public static Trainer GetTrainer(Int32 TrainerID)
        {
            return new TrainerDao().Get(TrainerID);
        }
        public static int InsertTrainer(Trainer record)
        {
            return new TrainerDao().Insert(record);
        }
        public static int UpdateTrainer(Trainer record)
        {
            return new TrainerDao().Update(record);
        }
        public static int DeleteTrainer(Int32 TrainerID)
        {
            return new TrainerDao().Delete(TrainerID);
        }
        public static List<Trainer> GetTrainerList(string filterExpression)
        {
            List<Trainer> result = new List<Trainer>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Trainer));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Trainer)helper.IDataReaderToObject(reader, new Trainer()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Training
        public static Training GetTraining(Int32 TrainingID)
        {
            return new TrainingDao().Get(TrainingID);
        }
        public static int InsertTraining(Training record)
        {
            return new TrainingDao().Insert(record);
        }
        public static int UpdateTraining(Training record)
        {
            return new TrainingDao().Update(record);
        }
        public static int DeleteTraining(Int32 TrainingID)
        {
            return new TrainingDao().Delete(TrainingID);
        }
        public static List<Training> GetTrainingList(string filterExpression)
        {
            List<Training> result = new List<Training>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Training));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Training)helper.IDataReaderToObject(reader, new Training()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetTrainingRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Training));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "TrainingID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TrainingCertification
        public static TrainingCertification GetTrainingCertification(Int32 TrainingID, String GCCompanyCertification)
        {
            return new TrainingCertificationDao().Get(TrainingID, GCCompanyCertification);
        }
        public static int InsertTrainingCertification(TrainingCertification record)
        {
            return new TrainingCertificationDao().Insert(record);
        }
        public static int UpdateTrainingCertification(TrainingCertification record)
        {
            return new TrainingCertificationDao().Update(record);
        }
        public static int DeleteTrainingCertification(Int32 TrainingID, String GCCompanyCertification)
        {
            return new TrainingCertificationDao().Delete(TrainingID, GCCompanyCertification);
        }
        public static List<TrainingCertification> GetTrainingCertificationList(string filterExpression)
        {
            List<TrainingCertification> result = new List<TrainingCertification>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TrainingCertification));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TrainingCertification)helper.IDataReaderToObject(reader, new TrainingCertification()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TrainingDepartment
        public static TrainingDepartment GetTrainingDepartment(Int32 TrainingID, String GCDepartment)
        {
            return new TrainingDepartmentDao().Get(TrainingID, GCDepartment);
        }
        public static int InsertTrainingDepartment(TrainingDepartment record)
        {
            return new TrainingDepartmentDao().Insert(record);
        }
        public static int UpdateTrainingDepartment(TrainingDepartment record)
        {
            return new TrainingDepartmentDao().Update(record);
        }
        public static int DeleteTrainingDepartment(Int32 TrainingID, String GCDepartment)
        {
            return new TrainingDepartmentDao().Delete(TrainingID, GCDepartment);
        }
        public static List<TrainingDepartment> GetTrainingDepartmentList(string filterExpression)
        {
            List<TrainingDepartment> result = new List<TrainingDepartment>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TrainingDepartment));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TrainingDepartment)helper.IDataReaderToObject(reader, new TrainingDepartment()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region TrainingOccupation
        public static TrainingOccupation GetTrainingOccupation(Int32 TrainingID, String GCOccupationLevel)
        {
            return new TrainingOccupationDao().Get(TrainingID, GCOccupationLevel);
        }
        public static int InsertTrainingOccupation(TrainingOccupation record)
        {
            return new TrainingOccupationDao().Insert(record);
        }
        public static int UpdateTrainingOccupation(TrainingOccupation record)
        {
            return new TrainingOccupationDao().Update(record);
        }
        public static int DeleteTrainingOccupation(Int32 TrainingID, String GCOccupationLevel)
        {
            return new TrainingOccupationDao().Delete(TrainingID, GCOccupationLevel);
        }
        public static List<TrainingOccupation> GetTrainingOccupationList(string filterExpression)
        {
            List<TrainingOccupation> result = new List<TrainingOccupation>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(TrainingOccupation));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((TrainingOccupation)helper.IDataReaderToObject(reader, new TrainingOccupation()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region User
        public static User GetUser(Int32 UserID)
        {
            return new UserDao().Get(UserID);
        }
        public static int InsertUser(User record)
        {
            return new UserDao().Insert(record);
        }
        public static int UpdateUser(User record)
        {
            return new UserDao().Update(record);
        }
        public static int DeleteUser(Int32 UserID)
        {
            return new UserDao().Delete(UserID);
        }
        public static List<User> GetUserList(string filterExpression)
        {
            List<User> result = new List<User>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(User));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((User)helper.IDataReaderToObject(reader, new User()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetUserMaxID(IDbContext ctx = null)
        {
            Int32 result = 0;
            if (ctx == null)
                ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(User));
                ctx.CommandText = helper.SelectMaxColumn("UserID");
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<User> GetUserList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<User> result = new List<User>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(User));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((User)helper.IDataReaderToObject(reader, new User()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetUserRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(User));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region UserInRole
        public static UserInRole GetUserInRole(Int32 UserID, Int32 RoleID)
        {
            return new UserInRoleDao().Get(UserID, RoleID);
        }
        public static int InsertUserInRole(UserInRole record)
        {
            return new UserInRoleDao().Insert(record);
        }
        public static int UpdateUserInRole(UserInRole record)
        {
            return new UserInRoleDao().Update(record);
        }
        public static int DeleteUserInRole(Int32 UserID, Int32 RoleID)
        {
            return new UserInRoleDao().Delete(UserID, RoleID);
        }
        public static List<UserInRole> GetUserInRoleList(string filterExpression)
        {
            List<UserInRole> result = new List<UserInRole>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserInRole));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserInRole)helper.IDataReaderToObject(reader, new UserInRole()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region UserMenu
        public static UserMenu GetUserMenu(Int32 ID)
        {
            return new UserMenuDao().Get(ID);
        }
        public static int InsertUserMenu(UserMenu record)
        {
            return new UserMenuDao().Insert(record);
        }
        public static int UpdateUserMenu(UserMenu record)
        {
            return new UserMenuDao().Update(record);
        }
        public static int DeleteUserMenu(Int32 ID)
        {
            return new UserMenuDao().Delete(ID);
        }
        public static List<UserMenu> GetUserMenuList(string filterExpression)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetUserMenuList(filterExpression, ctx);
        }
        public static List<UserMenu> GetUserMenuList(string filterExpression, IDbContext ctx)
        {
            List<UserMenu> result = new List<UserMenu>();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserMenu));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserMenu)helper.IDataReaderToObject(reader, new UserMenu()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region UserRole
        public static UserRole GetUserRole(Int32 RoleID)
        {
            return new UserRoleDao().Get(RoleID);
        }
        public static int InsertUserRole(UserRole record)
        {
            return new UserRoleDao().Insert(record);
        }
        public static int UpdateUserRole(UserRole record)
        {
            return new UserRoleDao().Update(record);
        }
        public static int DeleteUserRole(Int32 RoleID)
        {
            return new UserRoleDao().Delete(RoleID);
        }
        public static List<UserRole> GetUserRoleList(string filterExpression)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetUserRoleList(filterExpression, ctx);
        }
        public static List<UserRole> GetUserRoleList(string filterExpression, IDbContext ctx)
        {
            List<UserRole> result = new List<UserRole>();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserRole));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserRole)helper.IDataReaderToObject(reader, new UserRole()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<UserRole> GetUserRoleList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<UserRole> result = new List<UserRole>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserRole));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserRole)helper.IDataReaderToObject(reader, new UserRole()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetUserRoleRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserRole));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region UserRoleMenu
        public static UserRoleMenu GetUserRoleMenu(Int32 ID)
        {
            return new UserRoleMenuDao().Get(ID);
        }
        public static int InsertUserRoleMenu(UserRoleMenu record)
        {
            return new UserRoleMenuDao().Insert(record);
        }
        public static int UpdateUserRoleMenu(UserRoleMenu record)
        {
            return new UserRoleMenuDao().Update(record);
        }
        public static int DeleteUserRoleMenu(Int32 ID)
        {
            return new UserRoleMenuDao().Delete(ID);
        }
        public static List<UserRoleMenu> GetUserRoleMenuList(string filterExpression, IDbContext ctx)
        {
            List<UserRoleMenu> result = new List<UserRoleMenu>();
            try
            {
                DbHelper helper = new DbHelper(typeof(UserRoleMenu));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((UserRoleMenu)helper.IDataReaderToObject(reader, new UserRoleMenu()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<UserRoleMenu> GetUserRoleMenuList(string filterExpression)
        {
            IDbContext ctx = DbFactory.Configure();
            return GetUserRoleMenuList(filterExpression, ctx);
        }
        #endregion
        #region Venue
        public static Venue GetVenue(Int32 VenueID)
        {
            return new VenueDao().Get(VenueID);
        }
        public static int InsertVenue(Venue record)
        {
            return new VenueDao().Insert(record);
        }
        public static int UpdateVenue(Venue record)
        {
            return new VenueDao().Update(record);
        }
        public static int DeleteVenue(Int32 VenueID)
        {
            return new VenueDao().Delete(VenueID);
        }
        public static List<Venue> GetVenueList(string filterExpression)
        {
            List<Venue> result = new List<Venue>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(Venue));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((Venue)helper.IDataReaderToObject(reader, new Venue()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region ZipCodes
        public static ZipCodes GetZipCodes(Int32 ID)
        {
            return new ZipCodesDao().Get(ID);
        }
        public static int InsertZipCodes(ZipCodes record)
        {
            return new ZipCodesDao().Insert(record);
        }
        public static int UpdateZipCodes(ZipCodes record)
        {
            return new ZipCodesDao().Update(record);
        }
        public static int DeleteZipCodes(Int32 ID)
        {
            return new ZipCodesDao().Delete(ID);
        }
        public static List<ZipCodes> GetZipCodesList(string filterExpression)
        {
            List<ZipCodes> result = new List<ZipCodes>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ZipCodes));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ZipCodes)helper.IDataReaderToObject(reader, new ZipCodes()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<ZipCodes> GetZipCodesList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<ZipCodes> result = new List<ZipCodes>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ZipCodes));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((ZipCodes)helper.IDataReaderToObject(reader, new ZipCodes()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetZipCodesRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ZipCodes));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetZipCodesRowIndex(string filterExpression, string keyValue, string orderByExpression = "")
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(ZipCodes));
                ctx.CommandText = helper.GetRowIndex(filterExpression, "ID", keyValue, orderByExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #endregion

        #region Tools
        #region MigrationConfigurationDt
        public static MigrationConfigurationDt GetMigrationConfigurationDt(Int32 ID)
        {
            return new MigrationConfigurationDtDao().Get(ID);
        }
        public static int InsertMigrationConfigurationDt(MigrationConfigurationDt record)
        {
            return new MigrationConfigurationDtDao().Insert(record);
        }
        public static int UpdateMigrationConfigurationDt(MigrationConfigurationDt record)
        {
            return new MigrationConfigurationDtDao().Update(record);
        }
        public static int DeleteMigrationConfigurationDt(Int32 ID)
        {
            return new MigrationConfigurationDtDao().Delete(ID);
        }
        public static List<MigrationConfigurationDt> GetMigrationConfigurationDtList(string filterExpression)
        {
            List<MigrationConfigurationDt> result = new List<MigrationConfigurationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationDt));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MigrationConfigurationDt)helper.IDataReaderToObject(reader, new MigrationConfigurationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<MigrationConfigurationDt> GetMigrationConfigurationDtList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<MigrationConfigurationDt> result = new List<MigrationConfigurationDt>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationDt));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MigrationConfigurationDt)helper.IDataReaderToObject(reader, new MigrationConfigurationDt()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMigrationConfigurationDtRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationDt));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region MigrationConfigurationHd
        public static MigrationConfigurationHd GetMigrationConfigurationHd(Int32 ID)
        {
            return new MigrationConfigurationHdDao().Get(ID);
        }
        public static int InsertMigrationConfigurationHd(MigrationConfigurationHd record)
        {
            return new MigrationConfigurationHdDao().Insert(record);
        }
        public static int UpdateMigrationConfigurationHd(MigrationConfigurationHd record)
        {
            return new MigrationConfigurationHdDao().Update(record);
        }
        public static int DeleteMigrationConfigurationHd(Int32 ID)
        {
            return new MigrationConfigurationHdDao().Delete(ID);
        }
        public static List<MigrationConfigurationHd> GetMigrationConfigurationHdList(string filterExpression)
        {
            List<MigrationConfigurationHd> result = new List<MigrationConfigurationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationHd));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MigrationConfigurationHd)helper.IDataReaderToObject(reader, new MigrationConfigurationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<MigrationConfigurationHd> GetMigrationConfigurationHdList(string filterExpression, int numRows, int pageIndex, string orderByExpression = "")
        {
            List<MigrationConfigurationHd> result = new List<MigrationConfigurationHd>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationHd));
                ctx.CommandText = helper.Select(filterExpression, numRows, pageIndex, orderByExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MigrationConfigurationHd)helper.IDataReaderToObject(reader, new MigrationConfigurationHd()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static Int32 GetMigrationConfigurationHdRowCount(string filterExpression)
        {
            Int32 result = 0;
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationHd));
                ctx.CommandText = helper.GetRowCount(filterExpression);
                DataRow row = DaoBase.GetDataRow(ctx);
                result = Convert.ToInt32(row.ItemArray.GetValue(0));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region MigrationConfigurationTableLink
        public static MigrationConfigurationTableLink GetMigrationConfigurationTableLink(Int32 HeaderID, String TableName, String ColumnName)
        {
            return new MigrationConfigurationTableLinkDao().Get(HeaderID, TableName, ColumnName);
        }
        public static int InsertMigrationConfigurationTableLink(MigrationConfigurationTableLink record)
        {
            return new MigrationConfigurationTableLinkDao().Insert(record);
        }
        public static int UpdateMigrationConfigurationTableLink(MigrationConfigurationTableLink record)
        {
            return new MigrationConfigurationTableLinkDao().Update(record);
        }
        public static int DeleteMigrationConfigurationTableLink(Int32 HeaderID, String TableName, String ColumnName)
        {
            return new MigrationConfigurationTableLinkDao().Delete(HeaderID, TableName, ColumnName);
        }
        public static List<MigrationConfigurationTableLink> GetMigrationConfigurationTableLinkList(string filterExpression)
        {
            List<MigrationConfigurationTableLink> result = new List<MigrationConfigurationTableLink>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(MigrationConfigurationTableLink));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((MigrationConfigurationTableLink)helper.IDataReaderToObject(reader, new MigrationConfigurationTableLink()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region SysColumns
        public static List<SysColumns> GetSysColumnsList(string filterExpression)
        {
            List<SysColumns> result = new List<SysColumns>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SysColumns));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SysColumns)helper.IDataReaderToObject(reader, new SysColumns()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        public static List<String> GetSysColumnsPKList(string tableName)
        {
            List<String> result = new List<String>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SysColumns));
                ctx.CommandText = string.Format("SELECT column_name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1 AND table_name = '{0}'", tableName);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add(reader[0].ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        #endregion
        #region SysObjects
        public static List<SysObjects> GetSysObjectsList(string filterExpression)
        {
            List<SysObjects> result = new List<SysObjects>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(SysObjects));
                ctx.CommandText = helper.Select(filterExpression);
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((SysObjects)helper.IDataReaderToObject(reader, new SysObjects()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        #endregion
        #endregion
    }
}
