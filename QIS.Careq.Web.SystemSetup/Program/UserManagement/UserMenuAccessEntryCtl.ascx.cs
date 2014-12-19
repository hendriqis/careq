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
using QIS.Data.Core.Dal;
using System.Text;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class UserMenuAccessEntryCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        public override void InitializeDataControl(string param)
        {
            ListStateMenuAccess.Clear();

            hdnUserID.Value = param;

            User ur = BusinessLayer.GetUser(Convert.ToInt32(param));
            txtUserName.Text = ur.UserName;

            List<UserRole> lsUserRole = BusinessLayer.GetUserRoleList(string.Format("RoleID IN (SELECT RoleID FROM UserInRole WHERE UserID = {0})", param));
            if (lsUserRole.Count > 0)
            {
                Methods.SetComboBoxField<UserRole>(ddlUserRole, lsUserRole, "RoleName", "RoleID");
                hdnSelectedRole.Value = lsUserRole[0].RoleID.ToString();

                List<Module> lstModule = BusinessLayer.GetModuleList("");
                Methods.SetComboBoxField<Module>(ddlModule, lstModule, "ModuleName", "ModuleID");
                hdnSelectedModule.Value = lstModule[0].ModuleID;

                ListUserMenuAccess = BusinessLayer.GetUserMenuList(hdnSelectedModule.Value, Convert.ToInt32(hdnSelectedRole.Value), Convert.ToInt32(hdnUserID.Value), AppSession.UserLogin.RoleID, AppSession.UserLogin.UserID);

                BindGridView(1, true, ref PageCount);
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            List<GetUserMenuList> lstEntity = ListUserMenuAccess.Where(p => p.MenuCaption.ToLower().Contains(hdnFilterMenuCaption.Value.ToLower())).ToList();
            if (hdnFilterRead.Value != "")
            {
                bool isReadChecked = (hdnFilterRead.Value == "1");
                lstEntity = lstEntity.Where(p => p.ENABLED == isReadChecked).ToList();
            }
            if (isCountPageCount)
            {
                pageCount = Helper.GetPageCount(lstEntity.Count, Constant.GridViewPageSize.GRID_MATRIX);
            }
            List<GetUserMenuList> lst = lstEntity.Skip((pageIndex - 1) * 10).Take(10).ToList();
            foreach (GetUserMenuList entity in lst)
            {
                CUserMenuAccessState obj = ListStateMenuAccess.FirstOrDefault(p => p.RoleID == entity.RoleID && p.MenuID == entity.MenuID);
                if (obj != null)
                    entity.CRUDModeUser = obj.CRUDMode;
            }

            grdView.DataSource = lst;
            grdView.DataBind();
        }

        protected void cbpMenuAccess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                SetStateMenuAccess();

                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else if (param[0] == "changemodule")
                {
                    ListUserMenuAccess = BusinessLayer.GetUserMenuList(hdnSelectedModule.Value, Convert.ToInt32(hdnSelectedRole.Value), Convert.ToInt32(hdnUserID.Value), AppSession.UserLogin.RoleID, AppSession.UserLogin.UserID);
                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
                else
                {
                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected void cbpMenuAccessProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            SetStateMenuAccess();
            string errMessage = "";
            string result = "";
            if (SaveMenuAccess(ref errMessage))
                result += "success";
            else
                result += "fail|" + errMessage;

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private bool SaveMenuAccess(ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = false;
            try
            {
                UserMenuDao entityDao = new UserMenuDao(ctx);

                List<UserRole> lsUserRole = BusinessLayer.GetUserRoleList(string.Format("RoleID IN (SELECT RoleID FROM UserInRole WHERE UserID = {0})", hdnUserID.Value), ctx);
                foreach (UserRole role in lsUserRole)
                {
                    StringBuilder listMenuAccessID = new StringBuilder();
                    List<CUserMenuAccessState> ListState = ListStateMenuAccess.Where(p => p.RoleID == role.RoleID).ToList();
                    if (ListState.Count > 0)
                    {
                        foreach (CUserMenuAccessState row in ListState)
                        {
                            if (listMenuAccessID.ToString() != "")
                                listMenuAccessID.Append(",");
                            listMenuAccessID.Append(row.MenuID);
                        }
                        List<UserMenu> lstUserMenu = BusinessLayer.GetUserMenuList(string.Format("UserID = {0} AND RoleID = {1} AND MenuID IN ({2})", hdnUserID.Value, role.RoleID, listMenuAccessID.ToString()), ctx);

                        Int32 UserID = Convert.ToInt32(hdnUserID.Value);
                        foreach (CUserMenuAccessState row in ListState)
                        {
                            UserMenu obj = lstUserMenu.FirstOrDefault(p => p.MenuID == row.MenuID);
                            if (obj != null)
                            {
                                obj.CRUDMode = row.CRUDMode;
                                obj.LastUpdatedBy = AppSession.UserLogin.UserID;
                                entityDao.Update(obj);
                            }
                            else
                            {
                                obj = new UserMenu();
                                obj.UserID = UserID;
                                obj.RoleID = role.RoleID;
                                obj.MenuID = row.MenuID;
                                obj.CRUDMode = row.CRUDMode;
                                obj.CreatedBy = AppSession.UserLogin.UserID;
                                entityDao.Insert(obj);
                            }
                        }
                    }
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private void SetStateMenuAccess()
        {
            string[] listCRUDMode = hdnCRUDMode.Value.Split('|');
            string[] listMenuID = hdnListMenuID.Value.Split('|');

            for (int i = 0; i < listMenuID.Length; ++i)
            {
                if (listMenuID[i] != "")
                {
                    CUserMenuAccessState obj = ListStateMenuAccess.FirstOrDefault(p => p.RoleID == Convert.ToInt32(hdnPrevSelectedRole.Value) && p.MenuID == Convert.ToInt32(listMenuID[i]));
                    if (obj == null)
                    {
                        obj = new CUserMenuAccessState();
                        obj.RoleID = Convert.ToInt32(hdnPrevSelectedRole.Value);
                        obj.MenuID = Convert.ToInt32(listMenuID[i]);
                        ListStateMenuAccess.Add(obj);
                    }
                    obj.CRUDMode = listCRUDMode[i];
                }
            }
        }

        private const string SESSION_NAME_USER_MENU_ACCESS = "UserMenuAccess";
        private const string SESSION_NAME_STATE_MENU_ACCESS = "SelectedUserMenuAccess";
        private static List<CUserMenuAccessState> ListStateMenuAccess
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_NAME_STATE_MENU_ACCESS] == null) HttpContext.Current.Session[SESSION_NAME_STATE_MENU_ACCESS] = new List<CUserMenuAccessState>();
                return (List<CUserMenuAccessState>)HttpContext.Current.Session[SESSION_NAME_STATE_MENU_ACCESS];
            }
            set
            {
                HttpContext.Current.Session[SESSION_NAME_STATE_MENU_ACCESS] = value;
            }
        }
        public static List<GetUserMenuList> ListUserMenuAccess
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_NAME_USER_MENU_ACCESS] == null) HttpContext.Current.Session[SESSION_NAME_USER_MENU_ACCESS] = new List<GetUserMenuList>();
                return (List<GetUserMenuList>)HttpContext.Current.Session[SESSION_NAME_USER_MENU_ACCESS];
            }
            set
            {
                HttpContext.Current.Session[SESSION_NAME_USER_MENU_ACCESS] = value;
            }
        }

        private class CUserMenuAccessState
        {
            public int RoleID { get; set; }
            public int MenuID { get; set; }
            public string CRUDMode { get; set; }
        }

    }
}