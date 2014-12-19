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
using System.Data;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class EventRegistrationPrintCertificateListCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        private string[] lstSelectedMember = null;
        private string[] lstRevisionNo = null;
        private string[] lstRevisionNoKey = null;
        public override void InitializeDataControl(string param)
        {
            hdnID.Value = param;
            hdnIsRevisionNoChanged.Value = "0";
            BindGridView(1, true, ref PageCount);
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("EventID = {0}", hdnID.Value);
            
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvEventRegistrationRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MATRIX);

                List<Int32> lstMemberID = BusinessLayer.GetvEventRegistrationMemberIDList(filterExpression);
                hdnListAllMemberID.Value = "0";
                hdnListAllMemberID.Value = String.Format(";{0}", String.Join(";", lstMemberID.ToArray()));
            }

            lstSelectedMember = hdnSelectedMember.Value.Split(';');
            lstRevisionNo = hdnRevisionNo.Value.Split(';');
            lstRevisionNoKey = hdnRevisionNoKey.Value.Split(';');

            string orderBy = Helper.GetOrderByMemberColumn();
            List<vEventRegistration> lstEntity = BusinessLayer.GetvEventRegistrationList(filterExpression, Constant.GridViewPageSize.GRID_MATRIX, pageIndex, orderBy);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                vEventRegistration entity = e.Row.DataItem as vEventRegistration;

                CheckBox chkMember = (CheckBox)e.Row.FindControl("chkMember");
                if (lstSelectedMember.Contains(entity.MemberID.ToString()))
                    chkMember.Checked = true;

                TextBox txtRevisionNo = (TextBox)e.Row.FindControl("txtRevisionNo");
                int idx = Array.IndexOf(lstRevisionNoKey, entity.MemberID.ToString());
                if (idx > -1)
                    txtRevisionNo.Text = lstRevisionNo[idx];
                else
                    txtRevisionNo.Text = entity.CertificatePrintNo.ToString();
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

        protected void cbpGenerateCertificationNo_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            IDbContext ctx = DbFactory.Configure(true);
            EventRegistrationDao entityDao = new EventRegistrationDao(ctx);
            try
            {
                string filterExpression = string.Format("EventID = {0} AND MemberID IN ({1}) AND CertificationNo IS NULL", hdnID.Value, hdnSelectedMember.Value.Substring(1).Replace(';', ','));
                List<EventRegistration> lstEventRegistration = BusinessLayer.GetEventRegistrationList(filterExpression, ctx);
                if (lstEventRegistration.Count > 0)
                {
                    string maxID = BusinessLayer.GetMaxCertificationNo(Convert.ToInt32(hdnID.Value), ctx);
                    int ctr = Convert.ToInt32(maxID.Substring(0, 5));
                    maxID = maxID.Substring(5);
                    ctx.CommandType = CommandType.Text;
                    ctx.Command.Parameters.Clear();
                    foreach (EventRegistration entity in lstEventRegistration)
                    {
                        ctr++;
                        entity.CertificationNo = string.Format("{0}{1}", ctr.ToString().PadLeft(5, '0'), maxID);
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDao.Update(entity);
                    }
                }

                if (hdnRevisionNoKey.Value != "")
                {
                    lstRevisionNo = hdnRevisionNo.Value.Substring(1).Split(';');
                    lstRevisionNoKey = hdnRevisionNoKey.Value.Substring(1).Split(';');

                    filterExpression = string.Format("EventID = {0} AND MemberID IN ({1})", hdnID.Value, hdnRevisionNoKey.Value.Substring(1).Replace(';', ','));
                    lstEventRegistration = BusinessLayer.GetEventRegistrationList(filterExpression, ctx);
                    if (lstEventRegistration.Count > 0)
                    {
                        foreach (EventRegistration entity in lstEventRegistration)
                        {
                            int idx = Array.IndexOf(lstRevisionNoKey, entity.MemberID.ToString());
                            if (idx > -1)
                            {
                                entity.CertificatePrintNo = Convert.ToInt16(lstRevisionNo[idx]);
                                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                                entityDao.Update(entity);
                            }
                        }
                    }
                }

                result = "success";
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                result = "fail|" + ex.Message;
                ctx.RollBackTransaction();
            }
            finally
            {
                ctx.Close();
            }
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}