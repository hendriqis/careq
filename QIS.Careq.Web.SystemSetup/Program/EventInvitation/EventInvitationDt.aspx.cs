using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;
using System.Text.RegularExpressions;
using DevExpress.Web.ASPxCallbackPanel;
using System.Web.UI.HtmlControls;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class EventInvitationDt : BasePageList
    {
        protected int PageCount = 1;
        protected int CurrPage = 1;
        private string[] lstSelectedMember = null;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.GENERATE_EVENT_INVITATION;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        List<TrainingCertification> lstTrainingCertification = null;
        List<TrainingOccupation> lstTrainingOccupation = null;
        List<TrainingDepartment> lstTrainingDepartment = null;

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            txtMemberName.Attributes.Add("validationgroup", "mpEntryPopup");
            txtRatingLevel.Attributes.Add("validationgroup", "mpEntryPopup");
            hdnID.Value = Request.QueryString["id"];

            int trainingID = BusinessLayer.GetEvent(Convert.ToInt32(hdnID.Value)).TrainingID;
            lstTrainingCertification = BusinessLayer.GetTrainingCertificationList(string.Format("TrainingID = {0}", trainingID));
            lstTrainingOccupation = BusinessLayer.GetTrainingOccupationList(string.Format("TrainingID = {0}", trainingID));
            lstTrainingDepartment = BusinessLayer.GetTrainingDepartmentList(string.Format("TrainingID = {0}", trainingID));

            List<StandardCode> lstSc = BusinessLayer.GetStandardCodeList(string.Format("ParentID IN ('{0}','{1}','{2}','{3}') AND IsDeleted = 0 ORDER BY StandardCodeName ASC", Constant.StandardCode.OCCUPATION_LEVEL, Constant.StandardCode.COMPANY_CERTIFICATION, Constant.StandardCode.COMPANY_DEPARTMENT, Constant.StandardCode.MEMBER_STATUS));

            Repeater rptOccupationLevel = (Repeater)ddeOccupationLevel.FindControl("rptOccupationLevel");
            rptOccupationLevel.DataSource = lstSc.Where(p => p.ParentID == Constant.StandardCode.OCCUPATION_LEVEL).ToList();
            rptOccupationLevel.DataBind();
            ddeOccupationLevel.Text = ddeOccupationLevelText;

            Repeater rptCertificate = (Repeater)ddeCertificate.FindControl("rptCertificate");
            rptCertificate.DataSource = lstSc.Where(p => p.ParentID == Constant.StandardCode.COMPANY_CERTIFICATION).ToList();
            rptCertificate.DataBind();
            ddeCertificate.Text = ddeCertificateText;

            Repeater rptDepartment = (Repeater)ddeDepartment.FindControl("rptDepartment");
            rptDepartment.DataSource = lstSc.Where(p => p.ParentID == Constant.StandardCode.COMPANY_DEPARTMENT).ToList();
            rptDepartment.DataBind();
            ddeDepartment.Text = ddeDepartmentText;

            Repeater rptMemberStatus = (Repeater)ddeMemberStatus.FindControl("rptMemberStatus");
            rptMemberStatus.DataSource = lstSc.Where(p => p.ParentID == Constant.StandardCode.MEMBER_STATUS).ToList();
            rptMemberStatus.DataBind();

            BindGridView(1, true, ref PageCount);
        }

        string ddeCertificateValue = "";
        string ddeCertificateText = "";
        string ddeOccupationLevelValue = "";
        string ddeOccupationLevelText = "";
        string ddeDepartmentValue = "";
        string ddeDepartmentText = "";
        protected void rptCertificate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                StandardCode obj = (StandardCode)e.Item.DataItem;
                TrainingCertification entity = lstTrainingCertification.FirstOrDefault(p => p.GCCompanyCertification == obj.StandardCodeID);
                if (entity != null)
                {
                    if (ddeCertificateText != "")
                        ddeCertificateText += ", ";
                    ddeCertificateText += obj.StandardCodeName;

                    if (ddeCertificateValue != "")
                        ddeCertificateValue += ",";
                    ddeCertificateValue += string.Format("'{0}'", obj.StandardCodeID);

                    HtmlInputCheckBox chkCertificate = (HtmlInputCheckBox)e.Item.FindControl("chkCertificate");
                    chkCertificate.Attributes.Add("checked", "checked");
                }
            }
        }

        protected void rptDepartment_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                StandardCode obj = (StandardCode)e.Item.DataItem;
                TrainingDepartment entity = lstTrainingDepartment.FirstOrDefault(p => p.GCDepartment == obj.StandardCodeID);
                if (entity != null)
                {
                    if (ddeDepartmentText != "")
                        ddeDepartmentText += ", ";
                    ddeDepartmentText += obj.StandardCodeName;

                    if (ddeDepartmentValue != "")
                        ddeDepartmentValue += ",";
                    ddeDepartmentValue += string.Format("'{0}'", obj.StandardCodeID);

                    HtmlInputCheckBox chkDepartment = (HtmlInputCheckBox)e.Item.FindControl("chkDepartment");
                    chkDepartment.Attributes.Add("checked", "checked");
                }
            }
        }

        protected void rptOccupationLevel_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                StandardCode obj = (StandardCode)e.Item.DataItem;
                TrainingOccupation entity = lstTrainingOccupation.FirstOrDefault(p => p.GCOccupationLevel == obj.StandardCodeID);
                if (entity != null)
                {
                    if (ddeOccupationLevelText != "")
                        ddeOccupationLevelText += ", ";
                    ddeOccupationLevelText += obj.StandardCodeName;

                    if (ddeOccupationLevelValue != "")
                        ddeOccupationLevelValue += ",";
                    ddeOccupationLevelValue += string.Format("'{0}'", obj.StandardCodeID);

                    HtmlInputCheckBox chkOccupationLevel = (HtmlInputCheckBox)e.Item.FindControl("chkOccupationLevel");
                    chkOccupationLevel.Attributes.Add("checked", "checked");
                }
            }
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression == "")
            {
                if (ddeCertificateValue != "")
                    filterExpression = "CompanyID IN (SELECT CompanyID FROM CompanyCertification WHERE GCCompanyCertification IN (" + ddeCertificateValue + "))";
                if (ddeOccupationLevelValue != "")
                {
                    if (filterExpression != "")
                        filterExpression += " AND ";
                    filterExpression += "GCOccupationLevel IN (" + ddeOccupationLevelValue + ")";
                }
                if (ddeDepartmentValue != "")
                {
                    if (filterExpression != "")
                        filterExpression += " AND ";
                    filterExpression += "GCDepartment IN (" + ddeDepartmentValue + ")";
                }
            }
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("MemberID NOT IN (SELECT MemberID FROM EventInvitation WHERE EventID = {0}) AND MemberID NOT IN (SELECT MemberID FROM EventRegistration WHERE EventID = {0}) AND IsDeleted = 0", hdnID.Value);

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvMemberRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            lstSelectedMember = hdnSelectedMember.Value.Split(',');
            //string orderBy = Helper.GetOrderByMemberColumn();
            List<vMember> lstEntity = BusinessLayer.GetvMemberList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "CompanyName");
            List<Int32> lstID = BusinessLayer.GetvMemberIDList(filterExpression);
            hdnListAllMemberID.Value = string.Join(",", lstID.ToArray());
            
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vMember entity = e.Row.DataItem as vMember;

                CheckBox chkMember = (CheckBox)e.Row.FindControl("chkMember");
                if (lstSelectedMember.Contains(entity.MemberID.ToString()))
                    chkMember.Checked = true;

                for (int i = entity.RatingLevel; i > 0; --i)
                {
                    HtmlGenericControl div = (HtmlGenericControl)e.Row.FindControl("divRating" + i);
                    div.Attributes.Remove("class");
                    div.Attributes.Add("class", "starselected");
                }
            }
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
    }
}