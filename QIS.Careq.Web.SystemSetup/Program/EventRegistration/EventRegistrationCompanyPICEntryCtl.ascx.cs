using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Data.Service;
using DevExpress.Web.ASPxCallbackPanel;
using QIS.Careq.Web.Common;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class EventRegistrationCompanyPICEntryCtl : BaseViewPopupCtl
    {
        protected int PageCount = 1;
        public override void InitializeDataControl(string param)
        {
            hdnEventID.Value = param;
            Event entity = BusinessLayer.GetEvent(Convert.ToInt32(hdnEventID.Value));
            txtHeaderText.Text = string.Format("{0} - {1}", entity.EventCode, entity.EventName);

            BindGridView(1, true, ref PageCount);

            txtCompanyCode.Attributes.Add("validationgroup", "mpEntryPopup");
            txtContactPersonName.Attributes.Add("validationgroup", "mpEntryPopup");
            txtPaymentDate.Attributes.Add("validationgroup", "mpEntryPopup");
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = string.Format("EventID = {0}", hdnEventID.Value);
            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvEventCompanyDtRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, 8);
            }

            List<vEventCompanyDt> lstEntity = BusinessLayer.GetvEventCompanyDtList(filterExpression, 8, pageIndex, "CompanyName ASC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    e.Row.Cells[i].Text = GetLabel(e.Row.Cells[i].Text);
            }

        }

        protected void cbpEntryPopupView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            LoadWords();

            int pageCount = 1;

            string[] param = e.Parameter.Split('|');

            string result = param[0] + "|";
            string errMessage = "";

            if (param[0] == "changepage")
            {
                BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                result = "changepage";
            }
            else
            {
                if (param[0] == "save")
                {
                    if (hdnIsAdd.Value.ToString() == "0")
                    {
                        if (OnSaveEditRecord(ref errMessage))
                            result += "success";
                        else
                            result += string.Format("fail|{0}", errMessage);
                    }
                    else
                    {
                        if (OnSaveAddRecord(ref errMessage))
                            result += "success";
                        else
                            result += string.Format("fail|{0}", errMessage);
                    }
                }
                else if (param[0] == "delete")
                {
                    if (OnDeleteRecord(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }

                BindGridView(1, true, ref pageCount);
                result += "|" + pageCount;
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(EventCompanyDt entity)
        {
            entity.PICMemberID = Convert.ToInt32(hdnContactPersonID.Value);
            entity.PaymentDate = Helper.GetDatePickerValue(txtPaymentDate);
            if (chkIsSpecialPrice.Checked)
            {
                entity.Price = Convert.ToDecimal(txtPrice.Text);
                entity.DiscountAmount = Convert.ToDecimal(txtDiscountAmount.Text);
                entity.IsFinalDiscount = chkIsFinalDiscount.Checked;
            }
            else
            {
                entity.Price = null;
                entity.DiscountAmount = null;
                entity.IsFinalDiscount = false;
            }
        }

        private bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                EventCompanyDt entity = new EventCompanyDt();
                ControlToEntity(entity);
                entity.EventID = Convert.ToInt32(hdnEventID.Value);
                entity.CompanyID = Convert.ToInt32(hdnCompanyID.Value);
                entity.CreatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.InsertEventCompanyDt(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                EventCompanyDt entity = BusinessLayer.GetEventCompanyDt(Convert.ToInt32(hdnEventID.Value), Convert.ToInt32(hdnCompanyID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateEventCompanyDt(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        private bool OnDeleteRecord(ref string errMessage)
        {
            try
            {
                BusinessLayer.DeleteEventCompanyDt(Convert.ToInt32(hdnEventID.Value), Convert.ToInt32(hdnCompanyID.Value));
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
    }
}