using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using QIS.Careq.Data.Service;
using System.Text;

namespace QIS.Careq.Web.CommonLibs.MasterPage
{
    public partial class MPList : BaseMP
    {
        protected string IntellisenseHints = "";
        protected string menuCode = "";
        private BasePageList _basePageList;
        private BasePageList BasePageList
        {
            get
            {
                if (_basePageList == null)
                    _basePageList = (BasePageList)Page;
                return _basePageList;
            }
        }

        private vUserMenu menu;
        protected String GetMenuCaption()
        {
            return menu.MenuCaption;
        }
        protected String GetBreadcrumbs()
        {
            List<vUserMenu> lstMenu = ((MPMain)Master).ListMenu;
            StringBuilder result = new StringBuilder();
            List<vUserMenu> imagesHierarchy = new List<vUserMenu>();

            vUserMenu currMenu = lstMenu.FirstOrDefault(p => p.MenuCode == menuCode);
            while (currMenu != null)
            {
                imagesHierarchy.Insert(0, currMenu);
                currMenu = lstMenu.FirstOrDefault(p => p.MenuID == currMenu.ParentID);
            }

            string breadcrumb = string.Join(" > ", imagesHierarchy.Select(i => i.MenuCaption));
            return breadcrumb;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bool IsAllowAdd, IsAllowEdit, IsAllowDelete, IsAllowPrint;
                IsAllowAdd = IsAllowEdit = IsAllowDelete = IsAllowPrint = true;
                menuCode = BasePageList.OnGetMenuCode();
                BasePageList.SetCRUDMode(ref IsAllowAdd, ref IsAllowEdit, ref IsAllowDelete);

                menu = ((MPMain)Master).ListMenu.FirstOrDefault(p => p.MenuCode == menuCode);
                string CRUDMode = menu.CRUDMode;
                
                if (!IsAllowAdd || (IsAllowAdd &&!CRUDMode.Contains("C")))
                    btnMPListAdd.Style.Add("display", "none");
                if (!IsAllowEdit || (IsAllowEdit && !CRUDMode.Contains("U")))
                    btnMPListEdit.Style.Add("display", "none");
                if (!IsAllowDelete || (IsAllowDelete && !CRUDMode.Contains("D")))
                    btnMPListDelete.Style.Add("display", "none");
                if (!IsAllowPrint || (IsAllowPrint && !CRUDMode.Contains("P")))
                    btnMPListPrint.Style.Add("display", "none");

                PopulateFilterParameter();
            }
        }

        protected string OnGetReportCode()
        {
            return BasePageList.OnGetReportCode();
        }

        private void PopulateFilterParameter()
        {
            string[] fieldListText = null;
            string[] fieldListValue = null;
            _basePageList.SetFilterParameter(ref fieldListText, ref fieldListValue);

            if (fieldListText != null && fieldListValue != null)
            {
                for (int i = 0; i < fieldListText.Length; ++i)
                {
                    if (IntellisenseHints != "")
                        IntellisenseHints += ",";
                    IntellisenseHints += string.Format("{{ \"text\":\"{0}\",\"fieldName\":\"{1}\",\"description\":\"{2}\" }}", fieldListText[i], fieldListValue[i], "");
                }
            }
            else
                tblFilter.Visible = false;
        }

        protected void cbpMPListProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string url = "";
            string[] param = e.Parameter.Split('|');
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;

            if (param[0] == "add")
                BasePageList.OnBtnAddClick(ref result, ref url);
            else if (param[0] == "edit")
                BasePageList.OnBtnEditClick(ref result, ref url);
            else if (param[0] == "delete")
                BasePageList.OnBtnDeleteClick(ref result);
            else if (param[0] == "customclick")
            {
                BasePageList.OnBtnCustomClick(ref result, param[1]);
                panel.JSProperties["cpType"] = param[1];
            }

            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpURL"] = url;
        }
    }
}