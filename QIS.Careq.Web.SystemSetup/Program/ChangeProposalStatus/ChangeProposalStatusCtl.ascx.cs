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
using QIS.Data.Core.Dal;
using System.Data;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class ChangeProposalStatusCtl : BaseEntryPopupCtl
    {
        protected string GetLeadProcessTypeOther()
        {
            return Constant.LeadProcessType.OTHER;
        }

        public override void InitializeDataControl(string param)
        {
            hdnLeadID.Value = param;
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsDeleted = 0", Constant.StandardCode.LEAD_PROCESS_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboLeadProcessType, lstStandardCode, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(cboLeadProcessType, new ControlEntrySetting(true, true, true));
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            InquiryDao entityLeadDao = new InquiryDao(ctx);
            ProposalHdDao entityInquiryDao = new ProposalHdDao(ctx);
            try
            {
                //Inquiry entityLead = entityLeadDao.Get(Convert.ToInt32(hdnLeadID.Value));
                //entityLead.GCLeadProcessType = cboLeadProcessType.Value.ToString();
                //if (entityLead.GCLeadProcessType == Constant.LeadProcessType.OTHER)
                //    entityLead.OtherLeadProcessType = txtOtherLeadProcessType.Text;
                //else
                //    entityLead.OtherLeadProcessType = "";
                //entityLead.GCLeadStatus = Constant.LeadStatus.CLOSED;
                //entityLead.LastUpdatedBy = AppSession.UserLogin.UserID;
                //entityLeadDao.Update(entityLead);

                //if (entityLead.GCLeadProcessType == Constant.LeadProcessType.PROJECT)
                //{
                //    Inquiry entityInquiry = new Inquiry();
                //    entityInquiry.InquiryNo = BusinessLayer.GenerateInquiryCode(DateTime.Now.ToString("yy"), ctx);
                //    ctx.CommandType = CommandType.Text;
                //    ctx.Command.Parameters.Clear();
                    
                //    entityInquiry.LeadID = entityLead.LeadID;
                //    entityInquiry.InquiryDate = DateTime.Now;
                //    entityInquiry.CompanyID = entityLead.CompanyID;
                //    entityInquiry.PIC_CRO = entityLead.PIC_CRO;
                //    entityInquiry.MemberID = entityLead.MemberID;
                //    entityInquiry.Subject = entityLead.Subject;
                //    entityInquiry.Remarks = entityLead.Remarks;
                //    entityInquiry.GCInquiryStatus = Constant.LeadStatus.OPENED;
                //    entityInquiry.CreatedBy = AppSession.UserLogin.UserID;
                //    entityInquiryDao.Insert(entityInquiry);
                //}

                //ctx.CommitTransaction();
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
    }
}