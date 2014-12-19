using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;
using System.Web.UI.HtmlControls;
using QIS.Careq.Web.Common.UI;

namespace QIS.Careq.Web.CommonLibs.MasterPage
{
    public partial class MPMain : BaseMP
    {
        protected string moduleName = "";
        public List<vUserMenu> ListMenu { get { return lstMenu; } }
        protected List<vUserMenu> lstMenu = null;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!Page.IsPostBack)
            {
                if (AppSession.UserLogin == null)
                    Response.Redirect("~/../SystemSetup/Login.aspx");
                moduleName = Helper.GetModuleName();
                string ModuleID = Helper.GetModuleID(moduleName);
                lstMenu = BusinessLayer.GetvUserMenuList(string.Format("RoleID = {0} AND UserID = {1} AND ModuleID = '{2}' AND CHARINDEX('R', CRUDMode) > 0", AppSession.UserLogin.RoleID, AppSession.UserLogin.UserID, ModuleID));
                //lstMenu = BusinessLayer.GetMenuList(string.Format("ModuleID = '{0}'", ModuleID));
                rptMenu.DataSource = lstMenu.Where(p => p.ParentID == null).OrderBy(p => p.MenuIndex).ToList();
                rptMenu.DataBind();
            }
        }

        protected string GetResolveUrl(string url)
        {
            if (url == "#")
                return "#";
            return ResolveUrl(url);
        }

        protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                vUserMenu obj = (vUserMenu)e.Item.DataItem;

                Repeater rptMenuLevel2 = (Repeater)e.Item.FindControl("rptMenuLevel2");

                List<vUserMenu> lst = GetMenuChild(obj.MenuID);
                if (lst.Count > 0)
                {
                    rptMenuLevel2.DataSource = lst;
                    rptMenuLevel2.DataBind();
                }
            }
        }

        protected void rptMenuLevel2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                vUserMenu obj = (vUserMenu)e.Item.DataItem;

                Repeater rptMenuLevel3 = (Repeater)e.Item.FindControl("rptMenuLevel3");

                List<vUserMenu> lst = GetMenuChild(obj.MenuID);
                if (lst.Count > 0)
                {
                    rptMenuLevel3.DataSource = lst;
                    rptMenuLevel3.DataBind();
                }
            }
        }

        protected void rptMenuLevel3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                vUserMenu obj = (vUserMenu)e.Item.DataItem;

                Repeater rptMenuLevel4 = (Repeater)e.Item.FindControl("rptMenuLevel4");

                List<vUserMenu> lst = GetMenuChild(obj.MenuID);
                if (lst.Count > 0)
                {
                    rptMenuLevel4.DataSource = lst;
                    rptMenuLevel4.DataBind();
                }
            }
        }

        protected List<vUserMenu> GetMenuChild(Int32 ParentID)
        {
            return lstMenu.Where(p => p.ParentID == ParentID).OrderBy(p => p.MenuIndex).ToList();
        }

        protected string GetModuleImage()
        {    
            return Helper.GetModuleImage(this, moduleName);
        }

        protected string GetApplicationName()
        {
            return AppSession.UserLogin.CompanyName;
        }

        protected string GetUserInfo()
        {
            return string.Format("{0} [{1}]", AppSession.UserLogin.UserName, AppSession.UserLogin.RoleName);
        }

        protected void cbpCloseWindow_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            HttpContext.Current.Session.Clear();
        }
    }
}