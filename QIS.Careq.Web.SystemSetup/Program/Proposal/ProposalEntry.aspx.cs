using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;
using DevExpress.Web.ASPxCallbackPanel;
using QIS.Data.Core.Dal;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class ProposalEntry : BasePageEntry
    {
        protected bool IsEditable = true;

        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.PROPOSAL_REGISTRATION;
        }

        public string GetOtherLeadSourceType()
        {
            return Constant.LeadSourceType.OTHER;
        }

        public string GetInquiryFilterExpression() 
        {
            return String.Format("GCInquiryStatus NOT IN ('{0}','{1}')", Constant.EventStatus.DELETED, Constant.EventStatus.CLOSED);
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                SetControlProperties();
                vProposalHd entity = BusinessLayer.GetvProposalHdList(String.Format("ProposalID = {0}", ID))[0];
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                txtProposalDate.Text = DateTime.Now.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
                IsAdd = true;
            }
            BindGridView();
            txtSubject.Focus();
        }

        protected override void SetControlProperties()
        {
            List<StandardCode> lst = BusinessLayer.GetStandardCodeList(String.Format("ParentID IN ('{0}','{1}') AND IsDeleted = 0", Constant.StandardCode.LEAD_SOURCE_TYPE, Constant.StandardCode.TRAINING_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboProposalType, lst, "StandardCodeName", "StandardCodeID");
            Methods.SetComboBoxField<StandardCode>(cboLanguageType, lst, "StandardCodeName", "StandardCodeID");

            Methods.SetComboBoxField<StandardCode>(cboGCItemType, lst, "StandardCodeName", "StandardCodeID");

            cboProposalType.SelectedIndex = 0;
            cboLanguageType.SelectedIndex = 0;
            cboGCItemType.SelectedIndex = 0;
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                BindGridView();
                result = "refresh";
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        public void BindGridView() 
        {
            string filterExpression = "1 = 0";
            if (hdnID.Value != "")
                filterExpression = String.Format("ProposalID = {0} AND IsActive = 1", hdnID.Value);

            List<vProposalDt> lstEntity = BusinessLayer.GetvProposalDtList(filterExpression);
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtProposalNo, new ControlEntrySetting(true, false, true));
            SetControlEntrySetting(txtProposalDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(cboProposalType, new ControlEntrySetting(true, true, true));
            
            SetControlEntrySetting(hdnCompanyID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtCompanyCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtCompanyName, new ControlEntrySetting(false, false, true));

            SetControlEntrySetting(hdnEmployeeID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtEmployeeCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEmployeeName, new ControlEntrySetting(false, false, true));

            SetControlEntrySetting(hdnTrainerID, new ControlEntrySetting(true, true));
            SetControlEntrySetting(txtTrainerCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTrainerName, new ControlEntrySetting(false, false, true));

            SetControlEntrySetting(txtMemberCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtSubject, new ControlEntrySetting(true, true, true));
        }

        private void EntityToControl(vProposalHd entity)
        {
            txtProposalNo.Text = entity.ProposalNo;
            txtProposalDate.Text = entity.ProposalDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            cboProposalType.Value = entity.GCProposalType;
            cboLanguageType.Value = entity.GCLanguageType;
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
            hdnInquiryID.Value = entity.InquiryID.ToString();
            txtInquiryNo.Text = entity.InquiryNo;
            txtSubject.Text = entity.Subject;
            txtRemarks.Text = entity.Remarks;
        }

        private void ControlToEntity(ProposalHd entity)
        {
            entity.ProposalNo = txtProposalNo.Text;
            entity.ProposalDate = Helper.GetDatePickerValue(txtProposalDate.Text);
            entity.GCProposalType = cboProposalType.Value.ToString();
            entity.GCLanguageType = cboLanguageType.Value.ToString();
            entity.CompanyID = Convert.ToInt32(hdnCompanyID.Value);
            entity.InquiryID = Convert.ToInt32(hdnInquiryID.Value);
            entity.PIC_CRO = Convert.ToInt32(hdnEmployeeID.Value);
            entity.MemberID = Convert.ToInt32(hdnMemberID.Value);
            entity.PIC_TrainerID = Convert.ToInt32(hdnTrainerID.Value);
            entity.Subject = txtSubject.Text;
            entity.Remarks = txtRemarks.Text;
        }

        #region Save ProposalHd
        protected override bool OnBeforeSaveAddRecord(ref string errMessage)
        {
            errMessage = string.Empty;

            string FilterExpression = string.Format("ProposalNo = '{0}'", txtProposalNo.Text);
            List<ProposalHd> lst = BusinessLayer.GetProposalHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Proposal With No. " + txtProposalNo.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnBeforeSaveEditRecord(ref string errMessage)
        {
            errMessage = string.Empty;
            string FilterExpression = string.Format("ProposalNo = '{0}' AND LeadID != {1}", txtProposalNo.Text, hdnID.Value);
            List<ProposalHd> lst = BusinessLayer.GetProposalHdList(FilterExpression);

            if (lst.Count > 0)
                errMessage = " Proposal With No. " + txtProposalNo.Text + " is already exist!";

            return (errMessage == string.Empty);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            try
            {
                ProposalHd entity = new ProposalHd();
                ControlToEntity(entity);
                entity.LastUpdatedBy = entity.CreatedBy = AppSession.UserLogin.UserID;
                entity.GCProposalStatus = Constant.EventStatus.OPENED;
                BusinessLayer.InsertProposalHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }

        protected override bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                ProposalHd entity = BusinessLayer.GetProposalHd(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateProposalHd(entity);
                return true;
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
        }
        #endregion

        #region Save ProposalDt
        private bool OnSaveEditRecordEntityDt(ref String errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProposalDtDao entityDaoDt = new ProposalDtDao(ctx);
            
            try
            {
                ProposalDt entityDt = entityDaoDt.Get(Convert.ToInt32(hdnEntryID.Value));
                entityDt.ItemID = Convert.ToInt32(hdnTrainingID.Value);
                entityDt.GCItemType = cboGCItemType.Value.ToString();
                entityDt.Duration = Convert.ToInt16(txtDuration.Text);
                entityDt.NoOfPerson = Convert.ToInt32(txtNoOfPerson.Text);
                entityDt.Amount = Convert.ToDecimal(txtAmount.Text);
                entityDt.MarginPercentage = Convert.ToDecimal(txtMarginPercentage.Text);
                entityDt.ProposalContent = txtProposalContent.Text;
                entityDt.IsRevision = chkIsRevision.Checked;
                entityDt.RevisionDate = DateTime.Now;
                entityDt.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDt.LastUpdatedDate = DateTime.Now;
                entityDaoDt.Update(entityDt);

                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnSaveAddRecordEntityDt(ref String errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProposalHdDao entityDaoHd = new ProposalHdDao(ctx);
            ProposalDtDao entityDaoDt = new ProposalDtDao(ctx);
            try
            {
                Int32 ProposalID = 0;

                if (hdnID.Value.ToString() == "")
                {
                    ProposalHd entity = new ProposalHd();
                    ControlToEntity(entity);
                    entity.LastUpdatedBy = entity.CreatedBy = AppSession.UserLogin.UserID;
                    entity.GCProposalStatus = Constant.EventStatus.OPENED;
                    entityDaoHd.Insert(entity);
                    ProposalID = BusinessLayer.GetProposalHdMaxID(ctx);
                }
                else 
                {
                    ProposalID = Convert.ToInt32(hdnID.Value);
                }

                ProposalDt entityDt = new ProposalDt();
                entityDt.ProposalID = ProposalID;
                entityDt.ItemID = Convert.ToInt32(hdnTrainingID.Value);
                entityDt.GCItemType = cboGCItemType.Value.ToString();
                entityDt.Duration = Convert.ToInt16(txtDuration.Text);
                entityDt.NoOfPerson = Convert.ToInt32(txtNoOfPerson.Text);
                entityDt.Amount = Convert.ToDecimal(txtAmount.Text);
                entityDt.MarginPercentage = Convert.ToDecimal(txtMarginPercentage.Text);
                entityDt.ProposalContent = txtProposalContent.Text;
                entityDt.IsRevision = chkIsRevision.Checked;
                entityDt.IsActive = true;
                entityDt.RevisionDate = DateTime.Now;
                entityDt.LastUpdatedBy = entityDt.CreatedBy = AppSession.UserLogin.UserID;
                entityDt.CreatedDate = DateTime.Now;
                entityDt.LastUpdatedDate = DateTime.Now;
                entityDaoDt.Insert(entityDt);
                
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }

        private bool OnDeleteEntityDt(ref String errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            ProposalDtDao entityDtDao = new ProposalDtDao(ctx);
            try
            {
                ProposalDt entity = entityDtDao.Get(Convert.ToInt32(hdnEntryID.Value));
                entity.IsActive = false;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entity.LastUpdatedDate = DateTime.Now;
                entityDtDao.Update(entity);
                ctx.CommitTransaction();
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                errMessage = ex.Message;
                result = false;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion

        protected void cbpProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            string errMessage = "";
            string[] param = e.Parameter.Split('|');
            result = param[0] + "|";
            if (param[0] == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
                {
                    if (OnSaveEditRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
                else
                {
                    if (OnSaveAddRecordEntityDt(ref errMessage))
                        result += "success";
                    else
                        result += string.Format("fail|{0}", errMessage);
                }
            }
            else if (param[0] == "delete")
            {
                if (OnDeleteEntityDt(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}