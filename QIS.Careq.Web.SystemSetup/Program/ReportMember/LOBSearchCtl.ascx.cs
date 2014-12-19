using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using QIS.Careq.Data.Service;
using QIS.Careq.Web.Common;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class LOBSearchCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        public override void InitializeDataControl(string param)
        {
            hdnSelectedMember.Value = param;
            BindGridView(1, true, ref PageCount);

            if (param != "")
            {
                List<LOBClassification> lstSelected = BusinessLayer.GetLOBClassificationList(string.Format("LOBClassID IN ({0})", param));
                rptSelected.DataSource = lstSelected;
                rptSelected.DataBind();
            }
        }

        protected void cbpPopup_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
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

        private string GetFilterExpression()
        {
            string filterExpression = string.Format("LOBClassCode LIKE '%{0}%' AND LOBClassName LIKE '%{1}%' AND IsDeleted = 0", hdnFilterItemCode.Value, hdnFilterItemName.Value);
            return filterExpression;
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vLOBClassification entity = e.Row.DataItem as vLOBClassification;
                CheckBox chkIsSelected = e.Row.FindControl("chkIsSelected") as CheckBox;
                if (lstSelectedMember.Contains(entity.LOBClassID.ToString()))
                    chkIsSelected.Checked = true;
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = GetFilterExpression();
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvLOBClassificationRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 14);
            }
            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            List<vLOBClassification> lstEntity = BusinessLayer.GetvLOBClassificationList(filterExpression, 14, pageIndex, "");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }
    }
}