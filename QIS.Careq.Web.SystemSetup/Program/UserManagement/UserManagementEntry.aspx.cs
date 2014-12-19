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
using System.Web.Security;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class UserManagementEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.USER_ACCOUNTS;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String userID = Request.QueryString["id"];
                hdnID.Value = userID;
                User entity = BusinessLayer.GetUser(Convert.ToInt32(userID));
                EntityToControl(entity);
            }
            else
            {
                IsAdd = true;
            }
            txtUserName.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtUserName, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtEmail, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEmailPassword, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtPassword, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtConfirmPassword, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtMobilePIN, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(txtConfirmMobilePIN, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(txtSecurityQuestion, new ControlEntrySetting(true, false, false));
            SetControlEntrySetting(txtSecurityAnswer, new ControlEntrySetting(true, false, false));
        }

        private void EntityToControl(User entity)
        {
            txtUserName.Text = entity.UserName;
            txtEmail.Text = entity.Email;
            txtEmailPassword.Text = entity.EmailPassword;
            txtPassword.Text = "hidden";
            txtConfirmPassword.Text = "hidden";
            txtMobilePIN.Text = "hidden";
            txtMobilePIN.Text = "hidden";
            txtSecurityQuestion.Text = entity.PasswordQuestion;
            txtSecurityAnswer.Text = "hidden";
        }

        private void ControlToEntity(User entity)
        {
            entity.UserName = txtUserName.Text;
            entity.LoweredUserName = entity.UserName.ToLower();
            entity.Email = txtEmail.Text;
            entity.EmailPassword = txtEmailPassword.Text;
            entity.LoweredEmail = entity.Email.ToLower();
            entity.PasswordQuestion = txtSecurityQuestion.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("UserName = '{0}'", txtUserName.Text);
            List<User> lst = BusinessLayer.GetUserList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " User Name is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            UserDao entityDao = new UserDao(ctx);
            try
            {
                User entity = new User();
                ControlToEntity(entity);

                entity.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "sha1");
                entity.MobilePIN = FormsAuthentication.HashPasswordForStoringInConfigFile(txtMobilePIN.Text, "sha1");
                entity.PasswordAnswer = FormsAuthentication.HashPasswordForStoringInConfigFile(txtSecurityAnswer.Text, "sha1");
                
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
            UserDao entityDao = new UserDao(ctx);
            try
            {
                Int32 UserID = Convert.ToInt32(hdnID.Value);
                User entity = entityDao.Get(UserID);
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
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