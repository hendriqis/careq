using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Data.Service;
using QIS.Careq.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.Security;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class UserManagementList : BasePageList
    {
        protected int PageCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.USER_ACCOUNTS;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            if (AppSession.UserLogin.RoleID != 1)
                filterExpression += "UserID NOT IN (SELECT UserID FROM UserInRole WHERE RoleID = 1) AND IsDeleted = 0";
            else if (AppSession.UserLogin.UserID != 1)
                filterExpression += "UserID != 1 AND IsDeleted = 0";
            else
                filterExpression += "IsDeleted = 0";

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetUserRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<User> lstEntity = BusinessLayer.GetUserList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex," UserName ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "UserName", "Email" };
            fieldListValue = new string[] { "UserName", "Email" };
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else // refresh
                {

                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected override bool OnAddRecord(ref string url, ref string errMessage)
        {
            url = ResolveUrl("~/Program/UserManagement/UserManagementEntry.aspx");
            return true;
        }

        protected override bool OnEditRecord(ref string url, ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                url = ResolveUrl(string.Format("~/Program/UserManagement/UserManagementEntry.aspx?id={0}", hdnID.Value));
                return true;
            }
            return false;
        }

        protected override bool OnDeleteRecord(ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                int deletedID = Convert.ToInt32(hdnID.Value);
                if (AppSession.UserLogin.UserID == deletedID)
                {
                    errMessage = "Cannot Delete Your Own Account";
                    return false;
                }
                else if (deletedID == 1)
                {
                    errMessage = "Cannot Delete SysAdmin. This account is preloaded by system";
                    return false;
                }
                else
                {
                    User ua = BusinessLayer.GetUser(deletedID);
                    ua.IsDeleted = true;
                    ua.LastUpdatedBy = AppSession.UserLogin.UserID;
                    BusinessLayer.UpdateUser(ua);
                    return true;
                }
            }
            return false;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            if (type == "resetpassword")
            {
                if (hdnID.Value != "")
                {
                    String newPassword = BusinessLayer.GetSettingParameter(Constant.SettingParameter.DEFAULT_PASSWORD).ParameterValue;
                    User entity = BusinessLayer.GetUser(Convert.ToInt32(hdnID.Value));
                    entity.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(newPassword, "sha1");
                    BusinessLayer.UpdateUser(entity);
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}