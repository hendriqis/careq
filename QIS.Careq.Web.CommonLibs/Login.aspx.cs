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

namespace QIS.Careq.Web.CommonLibs
{
    public partial class Login : BasePage
    {
        protected string moduleName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                moduleName = Helper.GetModuleName();

                Helper.AddCssClass(txtUserName, "required");
                Helper.AddCssClass(txtPassword, "required");
                txtUserName.Focus();

                if (AppSession.UserLogin != null)
                {
                    Response.Redirect(Page.ResolveUrl("~/Libs/Program/Main.aspx"));
                }
            }
        }

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string param = e.Parameter;
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            string result = "";
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            List<User> lstUser = BusinessLayer.GetUserList(string.Format("UserName = '{0}'", userName));
            if (lstUser.Count > 0)
            {
                User user = lstUser[0];
                if (user.Password.Trim() == FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1"))
                {
                    result = string.Format("success|{0}", user.UserName);

                    UserLogin userLogin = new UserLogin();
                    userLogin.UserID = user.UserID;
                    userLogin.UserName = user.UserName;
                    //userLogin.UserGroupID = user.UserGroupID;
                    //userLogin.UserGroupName = user.UserGroupName;

                    AppSession.UserLogin = userLogin;
                }
                else
                    result = "fail|UserID And Password Doesn't match";
            }
            else
                result = "fail|User Doesn't exist";
            panel.JSProperties["cpResult"] = result;
        }
    }
}