using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;
using System.Web.Security;
using System.Text;

namespace QIS.Careq.Web.CommonLibs.Program
{
    public partial class RemoteLogon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["id"] != null)
            {
                string[] param = Request.Form["id"].Split('|');
                string userName = param[0];
                string password = param[1];
                List<User> lstUser = BusinessLayer.GetUserList(string.Format("UserName = '{0}' AND IsDeleted = 0", userName));
                if (lstUser.Count > 0)
                {
                    User user = lstUser[0];
                    if (user.Password.Trim() == password)
                    {
                        string roleID = param[2];

                        UserLogin userLogin = new UserLogin();
                        userLogin.UserID = user.UserID;
                        userLogin.UserName = user.UserName;
                        userLogin.RoleID = Convert.ToInt32(roleID);
                        userLogin.RoleName = BusinessLayer.GetUserRole(userLogin.RoleID).RoleName;
                        userLogin.CompanyName = BusinessLayer.GetApplication(1).CompanyName;

                        AppSession.UserLogin = userLogin;

                        GetSettingParameter();
                        Response.Redirect("Main.aspx");
                    }
                }
            }
        }

        private void GetSettingParameter()
        {
            //StringBuilder filterExpression = new StringBuilder();
            //filterExpression.AppendLine(string.Format("ParameterCode = '{0}'", Constant.SettingParameter.OUTPATIENT_CLASS));
            //filterExpression.AppendLine(string.Format(" OR ParameterCode = '{0}'", Constant.SettingParameter.VIRTUAL_DIR));
            //filterExpression.AppendLine(string.Format(" OR ParameterCode = '{0}'", Constant.SettingParameter.PHYSICAL_DIR));

            //List<SettingParameter> listSettingParameter = BusinessLayer.GetSettingParameterList(filterExpression.ToString());
            //AppSession.SettingParameter = (from p in listSettingParameter
            //                               select new Variable { Code = p.ParameterCode, Value = p.ParameterValue }).ToList();
        }
    }
}