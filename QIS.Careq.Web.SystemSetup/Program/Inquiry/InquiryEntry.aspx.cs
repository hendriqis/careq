﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;
using QIS.Data.Core.Dal;
using System.Data;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class InquiryEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.INQUIRY_REGISTRATION;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                vInquiry entity = BusinessLayer.GetvInquiryList(String.Format("InquiryID = {0}", ID))[0];
                EntityToControl(entity);
            }
            else
            {
                txtInquiryDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                IsAdd = true;
            }
            txtSubject.Focus();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtInquiryNo, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtInquiryDate, new ControlEntrySetting(true, true, true));
            
            SetControlEntrySetting(hdnCompanyID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtCompanyCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCompanyName, new ControlEntrySetting(false, false, true));

            SetControlEntrySetting(hdnEmployeeID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtEmployeeCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEmployeeName, new ControlEntrySetting(false, false, true));

            SetControlEntrySetting(hdnTrainerID, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtTrainerCode, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtTrainerName, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(txtMemberCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtSubject, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(vInquiry entity)
        {
            txtInquiryNo.Text = entity.InquiryNo;
            txtInquiryDate.Text = entity.InquiryDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            hdnCompanyID.Value = entity.CompanyID.ToString();
            txtCompanyName.Text = entity.CompanyName;
            txtCompanyCode.Text = entity.CompanyCode;
            hdnEmployeeID.Value = entity.PIC_CRO.ToString();
            txtEmployeeCode.Text = entity.PIC_CROCode;
            txtEmployeeName.Text = entity.PIC_CROName;
            hdnMemberID.Value = entity.MemberID.ToString();
            txtMemberCode.Text = entity.MemberCode;
            txtMemberName.Text = entity.MemberName;

            hdnTrainerID.Value = entity.PIC_TrainerID.ToString();
            txtTrainerCode.Text = entity.TrainerCode;
            txtTrainerName.Text = entity.TrainerName;
            txtSubject.Text = entity.Subject;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(Inquiry entity)
        {
            entity.InquiryNo = txtInquiryNo.Text;
            entity.InquiryDate = Helper.GetDatePickerValue(txtInquiryDate.Text);

            entity.CompanyID = Convert.ToInt32(hdnCompanyID.Value);
            entity.PIC_CRO = Convert.ToInt32(hdnEmployeeID.Value);
            if (hdnTrainerID.Value != "")
                entity.PIC_TrainerID = Convert.ToInt32(hdnTrainerID.Value);
            else
                entity.PIC_TrainerID = null;
            entity.MemberID = Convert.ToInt32(hdnMemberID.Value);
            entity.Subject = txtSubject.Text;
            entity.Remarks = txtRemarks.Text;
        }

        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("InquiryNo = '{0}'", txtInquiryNo.Text);
            List<Inquiry> lst = BusinessLayer.GetInquiryList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Inquiry With No. " + txtInquiryNo.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("InquiryNo = '{0}' AND InquiryID != {1}", txtInquiryNo.Text, hdnID.Value);
            List<Inquiry> lst = BusinessLayer.GetInquiryList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Inquiry With No. " + txtInquiryNo.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);            
            InquiryDao entityDao = new InquiryDao(ctx);
            try
            {
                Inquiry entity = new Inquiry();
                ControlToEntity(entity);
                entity.InquiryNo = BusinessLayer.GenerateInquiryCode(DateTime.Now.ToString("yy"), ctx);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();

                entity.LastUpdatedBy = entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.GCInquiryStatus = Constant.EventStatus.OPENED;
                entityDao.Insert(entity);

                ctx.CommitTransaction();
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

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                Inquiry entity = BusinessLayer.GetInquiry(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateInquiry(entity);
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