using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Data.Service;
using QIS.Careq.Web.Common;
using QIS.Careq.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using System.Data.OleDb;
using System.Data;
using System.Text.RegularExpressions;
using QIS.Data.Core.Dal;
using System.Text;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class UploadEventParticipantDt2 : BasePageList
    {
        protected int PageCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.UPLOAD_EVENT_PARTICIPANT_FROM_EXCEL;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        protected override void InitializeDataControl()
        {
            hdnID.Value = Request.QueryString["id"];
            BindGridViewDt(1, true, ref PageCount);
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string file = hdnUploadedFile1.Value;
            if (file != "")
            {
                string[] parts = Regex.Split(file, ",").Skip(1).ToArray();
                file = String.Join(",", parts);

                file = DecodeBase64(file);
                ListRegisteredMember.Clear();
                string[] allParam = file.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                for (int i = 1; i < allParam.Length; ++i)
                {
                    if (allParam[i].Trim() != "")
                    {
                        string[] param = allParam[i].Split(',');

                        vMember entity = new vMember();
                        entity.MemberID = i;
                        entity.MemberCode = param[0];
                        entity.FirstName = param[1];
                        entity.MiddleName = param[2];
                        entity.LastName = param[3];
                        entity.Occupation = param[4];
                        entity.MobilePhoneNo1 = param[5];
                        entity.MobilePhoneNo2 = param[6];
                        entity.EmailAddress1 = param[7];
                        entity.EmailAddress2 = param[8];
                        entity.Department = param[9];

                        ListRegisteredMember.Add(entity);
                    }
                }
                grdView.DataSource = ListRegisteredMember;
                grdView.DataBind();
            }
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            if (e.Parameter == "save")
            {
                result = "save|";
                List<SettingParameter> lstSettingParameter = BusinessLayer.GetSettingParameterList(string.Format("ParameterCode IN ('{0}','{1}')", Constant.SettingParameter.REGISTRATION_TYPE_BY_EMAIL, Constant.SettingParameter.INFORMATION_SOURCE_BY_EMAIL));
                string GCRegistrationType = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.REGISTRATION_TYPE_BY_EMAIL).ParameterValue;
                string GCInformationSource = lstSettingParameter.FirstOrDefault(p => p.ParameterCode == Constant.SettingParameter.INFORMATION_SOURCE_BY_EMAIL).ParameterValue;

                IDbContext ctx = DbFactory.Configure(true);
                EventRegistrationDao entityDao = new EventRegistrationDao(ctx);
                MemberDao entityMemberDao = new MemberDao(ctx);
                AddressDao entityAddressDao = new AddressDao(ctx);
                try
                {
                    string[] listSelectedMember = hdnSelectedMember.Value.Split('|');
                    foreach (vMember registeredMember in ListRegisteredMember)
                    {
                        bool isNewMember = false;
                        Member member = null;
                        if (listSelectedMember[registeredMember.MemberID - 1] != "")
                        {
                            member = entityMemberDao.Get(Convert.ToInt32(listSelectedMember[registeredMember.MemberID - 1]));
                            isNewMember = false;
                        }
                        else
                        {
                            member = new Member();
                            isNewMember = true;
                            member.GCDepartment = string.Format("{0}^999", Constant.StandardCode.COMPANY_DEPARTMENT);
                            member.GCOccupationLevel = string.Format("{0}^999", Constant.StandardCode.OCCUPATION_LEVEL);
                            member.CompanyID = Convert.ToInt32(hdnCompanyID.Value);
                            member.GCMemberStatus = "X009^001";
                        }
                        member.FirstName = registeredMember.FirstName;
                        member.MiddleName = registeredMember.MiddleName;
                        member.LastName = registeredMember.LastName;
                        if (registeredMember.EmailAddress1 != "")
                            member.EmailAddress1 = registeredMember.EmailAddress1;
                        if (registeredMember.EmailAddress2 != "")
                            member.EmailAddress2 = registeredMember.EmailAddress2;
                        if (registeredMember.MobilePhoneNo1 != "")
                            member.MobilePhoneNo1 = registeredMember.MobilePhoneNo1;
                        if (registeredMember.MobilePhoneNo2 != "")
                            member.MobilePhoneNo2 = registeredMember.MobilePhoneNo2;

                        if (isNewMember)
                        {
                            Address entityAddress = new Address();
                            entityAddressDao.Insert(entityAddress);
                            member.AddressID = BusinessLayer.GetAddressMaxID(ctx);
                            member.CreatedBy = AppSession.UserLogin.UserID;
                            entityMemberDao.Insert(member);
                            member.MemberID = BusinessLayer.GetMemberMaxID(ctx);
                        }
                        else
                        {
                            member.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityMemberDao.Update(member);
                        }
                        EventRegistration entity = new EventRegistration();
                        entity.MemberID = member.MemberID;
                        entity.CompanyID = member.CompanyID;
                        entity.Occupation = member.Occupation;
                        entity.GCOccupationLevel = member.GCOccupationLevel;
                        entity.GCRegistrationType = GCRegistrationType;
                        entity.GCInformationSource = GCInformationSource;
                        entity.CertificationNo = "";
                        entity.CertificatePrintNo = 0;
                        entity.GCRegistrationStatus = Constant.RegistrationStatus.OPEN;
                        entity.EventID = Convert.ToInt32(hdnID.Value);
                        entity.CreatedBy = AppSession.UserLogin.UserID;
                        entityDao.Insert(entity);
                    }
                    result += "success";
                    ctx.CommitTransaction();
                }
                catch (Exception ex)
                {
                    result += string.Format("fail|{0}", ex.Message);
                    ctx.RollBackTransaction();
                }
                finally
                {
                    ctx.Close();
                }
            }
            else
            {
                result = "map|";
                string filterExpression = string.Format("CompanyID = {0} AND IsDeleted = 0", hdnCompanyID.Value);
                List<vMember> lstEntity = BusinessLayer.GetvMemberList(filterExpression);
                StringBuilder sbListMember = new StringBuilder();
                foreach (vMember member in ListRegisteredMember)
                {
                    if (sbListMember.ToString() != "")
                        sbListMember.Append(";");
                    string memberID = "";
                    if (member.MemberCode != "")
                    {
                        vMember entity = lstEntity.FirstOrDefault(p => p.MemberCode == member.MemberCode);
                        if (entity != null)
                            memberID = entity.MemberID.ToString();
                    }
                    sbListMember.Append(memberID);
                }
                result += sbListMember.ToString();
            }
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private string DecodeBase64(string text)
        {
            string result = null;
            try
            {
                var converted = Convert.FromBase64String(text);
                result = System.Text.Encoding.UTF8.GetString(converted);
            }
            catch (System.ArgumentNullException)
            {
                //handle it
            }
            catch (System.FormatException)
            {
                //handle it
            }
            return result;
        }

        #region Member
        private void BindGridViewDt(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = "1 = 0";
            if (hdnCompanyID.Value != "")
            {
                filterExpression = string.Format("CompanyID = {0} AND IsDeleted = 0", hdnCompanyID.Value);
                if (hdnSearchTextCode.Value != "")
                    filterExpression += string.Format(" AND MemberCode LIKE '%{0}%'", hdnSearchTextCode.Value);
                if (hdnSearchTextName.Value != "")
                    filterExpression += string.Format(" AND (FirstName LIKE '%{0}%' OR MiddleName LIKE '%{0}%' OR MiddleName LIKE '%{0}%')", hdnSearchTextName.Value);
                if (hdnSearchTextEmail.Value != "")
                    filterExpression += string.Format(" AND (EmailAddress1 LIKE '%{1}%' OR EmailAddress2 LIKE '%{0}%')", hdnSearchTextEmail.Value);
                if (hdnSearchTextMobilePhone.Value != "")
                    filterExpression += string.Format(" AND (MobilePhoneNo1 LIKE '%{1}%' OR MobilePhoneNo2 LIKE '%{0}%')", hdnSearchTextMobilePhone.Value);

                if (isCountPageCount)
                {
                    int rowCount = BusinessLayer.GetvMemberRowCount(filterExpression);
                    pageCount = Helper.GetPageCount(rowCount, 5);
                }
            }
            string orderBy = Helper.GetOrderByMemberColumn();
            List<vMember> lstEntity = BusinessLayer.GetvMemberList(filterExpression, 5, pageIndex, orderBy);
            grdViewDt.DataSource = lstEntity;
            grdViewDt.DataBind();
        }
        protected void cbpViewDt_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridViewDt(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else // refresh
                {

                    BindGridViewDt(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
        #endregion

        private const string SESSION_NAME_REGISTERED_MEMBER = "SelectedRegisteredMember";
        public static List<vMember> ListRegisteredMember
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_NAME_REGISTERED_MEMBER] == null) HttpContext.Current.Session[SESSION_NAME_REGISTERED_MEMBER] = new List<vMember>();
                return (List<vMember>)HttpContext.Current.Session[SESSION_NAME_REGISTERED_MEMBER];
            }
            set
            {
                HttpContext.Current.Session[SESSION_NAME_REGISTERED_MEMBER] = value;
            }
        }
    }
}