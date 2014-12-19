using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using System.Web.Security;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;
using DevExpress.Web.ASPxCallbackPanel;

namespace QIS.Careq.Web.SystemSetup
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (AppSession.UserLogin == null)
                    pnlUserLoginInformation.Style.Add("display", "none");
                else
                {
                    BindCboSelectUserRole();
                    lblUserLoginInfo.InnerHtml = AppSession.UserLogin.UserName;
                    loginContainerLoginInfo.Style.Add("display", "none");
                }

                txtUserName.Attributes.Add("validationgroup", "mpLogin");
                txtPassword.Attributes.Add("validationgroup", "mpLogin");
                Helper.AddCssClass(txtUserName, "required");
                Helper.AddCssClass(txtPassword, "required");

                txtUserName.Focus();
            }
            //FormsAuthentication.HashPasswordForStoringInConfigFile(
            //Response.Redirect(Page.ResolveUrl("~/../Outpatient/Program/Registration.aspx"));
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string param = e.Parameter;
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpParam"] = param;
            if (param == "login")
            {
                string result = "";
                string loginData = "";
                string userName = txtUserName.Text;
                string password = txtPassword.Text;
                List<User> lstUser = BusinessLayer.GetUserList(string.Format("UserName = '{0}' AND IsDeleted = 0", userName));
                if (lstUser.Count > 0)
                {
                    User user = lstUser[0];
                    if (user.Password.Trim() == FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1"))
                    {
                        loginData = string.Format("{0}|{1}", userName, user.Password);

                        UserLogin userLogin = new UserLogin();
                        userLogin.UserID = user.UserID;
                        userLogin.UserName = user.UserName;
                        userLogin.CompanyName = BusinessLayer.GetApplication(1).CompanyName;

                        AppSession.UserLogin = userLogin;
                        result = string.Format("success|{0}", user.UserName);
                    }
                    else
                        result = "fail|UserID And Password Doesn't match";
                }
                else
                    result = "fail|User Doesn't exist";
                panel.JSProperties["cpResult"] = result;
                panel.JSProperties["cpLoginData"] = loginData;
            }
            else // Get Data Login
            {
                User user = BusinessLayer.GetUser(AppSession.UserLogin.UserID);
                string loginData = string.Format("{0}|{1}", user.UserName, user.Password);
                panel.JSProperties["cpLoginData"] = loginData;
            }
        }

        protected string GetApplicationName()
        {
            return BusinessLayer.GetApplicationList("")[0].CompanyName;
        }

        protected void cbpSelectUserRole_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            BindCboSelectUserRole();
        }

        private void BindCboSelectUserRole()
        {
            List<vUserInRole> lstUserInRole = BusinessLayer.GetvUserInRoleList(string.Format("UserID = {0}", AppSession.UserLogin.UserID));
            Methods.SetComboBoxField<vUserInRole>(ddlUserRole, lstUserInRole, "RoleName", "RoleID");
            ddlUserRole.SelectedIndex = 0;
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Response.Redirect("~/Login.aspx", true);
        }
    }
}