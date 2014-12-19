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

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class MenuList : BasePageList
    {
        protected int PageCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.MENU;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            BindGridView(1, true, ref PageCount);
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "Code", "Caption" };
            fieldListValue = new string[] { "MenuCode", "MenuCaption" };
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvMenuRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vMenu> lstEntity = BusinessLayer.GetvMenuList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
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
            url = ResolveUrl("~/Program/Menu/MenuEntry.aspx");
            return true;
        }

        protected override bool OnEditRecord(ref string url, ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                url = ResolveUrl(string.Format("~/Program/Menu/MenuEntry.aspx?id={0}", hdnID.Value));
                return true;
            }
            return false;
        }

        protected override bool OnDeleteRecord(ref string errMessage)
        {
            if (hdnID.Value.ToString() != "")
            {
                bool result = true;
                IDbContext ctx = DbFactory.Configure(true);
                UserMenuDao entityUserMenuDao = new UserMenuDao(ctx);
                UserRoleMenuDao entityUserRoleMenuDao = new UserRoleMenuDao(ctx);
                try
                {
                    MenuMasterDao entityDao = new MenuMasterDao(ctx);
                    List<UserMenu> lstUserMenu = BusinessLayer.GetUserMenuList(string.Format("MenuID = {0}", hdnID.Value), ctx);
                    List<UserRoleMenu> lstUserRoleMenu = BusinessLayer.GetUserRoleMenuList(string.Format("MenuID = {0}", hdnID.Value), ctx);

                    foreach (UserMenu entityUserMenu in lstUserMenu)
                        entityUserMenuDao.Delete(entityUserMenu.ID);
                    foreach (UserRoleMenu entityUserRoleMenu in lstUserRoleMenu)
                        entityUserRoleMenuDao.Delete(entityUserRoleMenu.ID);

                    entityDao.Delete(Convert.ToInt32(hdnID.Value)); ctx.CommitTransaction();
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
            return false;
        }
    }
}