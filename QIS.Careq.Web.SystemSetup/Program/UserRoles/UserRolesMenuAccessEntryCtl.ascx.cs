﻿using System;
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
    public partial class UserRolesMenuAccessEntryCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        public override void InitializeDataControl(string param)
        {
            ListStateMenuAccess.Clear();

            hdnRoleID.Value = param;

            UserRole ur = BusinessLayer.GetUserRole(Convert.ToInt32(param));
            txtUserRoleName.Text = ur.RoleName;

            List<Module> lstModule = BusinessLayer.GetModuleList("");
            Methods.SetComboBoxField<Module>(ddlModule, lstModule, "ModuleName", "ModuleID");
            hdnSelectedModule.Value = lstModule[0].ModuleID;

            ListUserRoleMenuAccess = BusinessLayer.GetUserRoleMenuList(hdnSelectedModule.Value, Convert.ToInt32(hdnRoleID.Value), AppSession.UserLogin.RoleID, AppSession.UserLogin.UserID);

            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            List<GetUserRoleMenuList> lstEntity = ListUserRoleMenuAccess.Where(p => p.MenuCaption.ToLower().Contains(hdnFilterMenuCaption.Value.ToLower())).ToList();
            if (hdnFilterRead.Value != "")
            {
                bool isReadChecked = (hdnFilterRead.Value == "1");
                lstEntity = lstEntity.Where(p => p.ENABLED == isReadChecked).ToList();
            }
            if (isCountPageCount)
            {
                pageCount = Helper.GetPageCount(lstEntity.Count, Constant.GridViewPageSize.GRID_MATRIX);
            }
            List<GetUserRoleMenuList> lst = lstEntity.Skip((pageIndex - 1) * 10).Take(10).ToList();
            foreach (GetUserRoleMenuList entity in lst)
            {
                CUserRoleMenuAccessState obj = ListStateMenuAccess.FirstOrDefault(p => p.MenuID == entity.MenuID);
                if (obj != null)
                    entity.CRUDModeUserRole = obj.CRUDMode;
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
                    ListUserRoleMenuAccess = BusinessLayer.GetUserRoleMenuList(hdnSelectedModule.Value, Convert.ToInt32(hdnRoleID.Value), AppSession.UserLogin.RoleID, AppSession.UserLogin.UserID);
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
                UserRoleMenuDao entityDao = new UserRoleMenuDao(ctx);

                StringBuilder listMenuAccessID = new StringBuilder();
                foreach (CUserRoleMenuAccessState row in ListStateMenuAccess)
                {
                    if (listMenuAccessID.ToString() != "")
                        listMenuAccessID.Append(",");
                    listMenuAccessID.Append(row.MenuID);
                }
                List<UserRoleMenu> lstUserRoleMenu = BusinessLayer.GetUserRoleMenuList(string.Format("RoleID = {0} AND MenuID IN ({1})", hdnRoleID.Value, listMenuAccessID.ToString()), ctx);

                Int32 RoleID = Convert.ToInt32(hdnRoleID.Value);
                foreach (CUserRoleMenuAccessState row in ListStateMenuAccess)
                {
                    UserRoleMenu obj = lstUserRoleMenu.FirstOrDefault(p => p.MenuID == row.MenuID);
                    if (obj != null)
                    {
                        obj.CRUDMode = row.CRUDMode;
                        obj.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDao.Update(obj);
                    }
                    else
                    {
                        obj = new UserRoleMenu();
                        obj.RoleID = RoleID;
                        obj.MenuID = row.MenuID;
                        obj.CRUDMode = row.CRUDMode;
                        obj.CreatedBy = AppSession.UserLogin.UserID;
                        entityDao.Insert(obj);
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
                    CUserRoleMenuAccessState obj = ListStateMenuAccess.FirstOrDefault(p => p.MenuID == Convert.ToInt32(listMenuID[i]));
                    if (obj == null)
                    {
                        obj = new CUserRoleMenuAccessState();
                        obj.MenuID = Convert.ToInt32(listMenuID[i]);
                        ListStateMenuAccess.Add(obj);
                    }
                    obj.CRUDMode = listCRUDMode[i];
                }
            }
        }

        private const string SESSION_NAME_ROLE_MENU_ACCESS = "UserRoleMenuAccess";
        private const string SESSION_NAME_STATE_MENU_ACCESS = "SelectedUserRoleMenuAccess";
        private static List<CUserRoleMenuAccessState> ListStateMenuAccess
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_NAME_STATE_MENU_ACCESS] == null) HttpContext.Current.Session[SESSION_NAME_STATE_MENU_ACCESS] = new List<CUserRoleMenuAccessState>();
                return (List<CUserRoleMenuAccessState>)HttpContext.Current.Session[SESSION_NAME_STATE_MENU_ACCESS];
            }
            set
            {
                HttpContext.Current.Session[SESSION_NAME_STATE_MENU_ACCESS] = value;
            }
        }
        public static List<GetUserRoleMenuList> ListUserRoleMenuAccess
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_NAME_ROLE_MENU_ACCESS] == null) HttpContext.Current.Session[SESSION_NAME_ROLE_MENU_ACCESS] = new List<GetUserRoleMenuList>();
                return (List<GetUserRoleMenuList>)HttpContext.Current.Session[SESSION_NAME_ROLE_MENU_ACCESS];
            }
            set
            {
                HttpContext.Current.Session[SESSION_NAME_ROLE_MENU_ACCESS] = value;
            }
        }

        private class CUserRoleMenuAccessState
        {
            public int MenuID { get; set; }
            public string CRUDMode { get; set; }
        }

    }
}