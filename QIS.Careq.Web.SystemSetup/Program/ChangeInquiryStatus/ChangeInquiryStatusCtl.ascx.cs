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
    public partial class ChangeInquiryStatusCtl : BaseEntryPopupCtl
    {
        protected string GetInquiryProcessTypeOther()
        {
            return Constant.LeadProcessType.OTHER;
        }

        public override void InitializeDataControl(string param)
        {
            hdnInquiryID.Value = param;
            List<StandardCode> lstStandardCode = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND IsDeleted = 0", Constant.StandardCode.LEAD_PROCESS_TYPE));
            Methods.SetComboBoxField<StandardCode>(cboInquiryProcessType, lstStandardCode, "StandardCodeName", "StandardCodeID");
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(cboInquiryProcessType, new ControlEntrySetting(true, true, true));
        }

        protected override bool OnSaveAddRecord(ref string errMessage, ref string retval)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            InquiryDao entityInquiryDao = new InquiryDao(ctx);
            ProposalHdDao entityProposalHdDao = new ProposalHdDao(ctx);
            try
            {
                Inquiry entityInquiry = entityInquiryDao.Get(Convert.ToInt32(hdnInquiryID.Value));
                entityInquiry.GCInquiryProcessType = cboInquiryProcessType.Value.ToString();
                if (entityInquiry.GCInquiryProcessType == Constant.LeadProcessType.OTHER)
                    entityInquiry.OtherInquiryProcessType = txtOtherInquiryProcessType.Text;
                else
                    entityInquiry.OtherInquiryProcessType = "";
                entityInquiry.GCInquiryStatus = Constant.EventStatus.CLOSED;
                entityInquiry.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityInquiryDao.Update(entityInquiry);

                if (entityInquiry.GCInquiryProcessType == Constant.LeadProcessType.PROJECT)
                {
                    ProposalHd entityProposalHd = new ProposalHd();
                    entityProposalHd.ProposalNo = BusinessLayer.GenerateProposalCode(DateTime.Now.ToString("yy"), ctx);
                    ctx.CommandType = CommandType.Text;
                    ctx.Command.Parameters.Clear();

                    entityProposalHd.InquiryID = entityInquiry.InquiryID;
                    entityProposalHd.ProposalDate = DateTime.Now;
                    entityProposalHd.CompanyID = entityInquiry.CompanyID;
                    entityProposalHd.PIC_CRO = entityInquiry.PIC_CRO;
                    entityProposalHd.MemberID = entityInquiry.MemberID;
                    entityProposalHd.Subject = entityInquiry.Subject;
                    entityProposalHd.Remarks = entityInquiry.Remarks;
                    entityProposalHd.GCProposalStatus = Constant.EventStatus.OPENED;
                    entityProposalHd.CreatedBy = AppSession.UserLogin.UserID;
                    entityProposalHdDao.Insert(entityProposalHd);
                }

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
    }
}