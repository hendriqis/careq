using System;
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
    public partial class CreateEventEntry : BasePageEntry
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.CREATE_EVENT;
        }

        protected override void InitializeDataControl()
        {
            if (Request.QueryString.Count > 0)
            {
                IsAdd = false;
                String ID = Request.QueryString["id"];
                hdnID.Value = ID;
                SetControlProperties();
                vEvent entity = BusinessLayer.GetvEventList(string.Format("EventID = {0}", ID))[0];
                EntityToControl(entity);
            }
            else
            {
                SetControlProperties();
                IsAdd = true;

                string[] textFileResult = Helper.LoadTextFile(this, Constant.DEFAULT_EMAIL_TEMPLATE_TEXT);
                string[] textFileResultConf = Helper.LoadTextFile(this, Constant.DEFAULT_EMAIL_CONFIRMATION_TEXT);
                string tempMessageBodyConf = String.Join("<br>", textFileResultConf);
                string tempMessageBody = String.Join("<br>", textFileResult);
                txtEmailInvitation.Text = tempMessageBody;
                txtEmailConfirmation.Text = tempMessageBodyConf;
            }
            txtEventName.Focus();
        }

        protected override void SetControlProperties()
        {
            List<TemplateText> lstTemplate = BusinessLayer.GetTemplateTextList(string.Format("GCTemplateGroup IN('X032^001','X032^002') AND IsDeleted = 0"));
            Methods.SetComboBoxField<TemplateText>(cboTemplate, lstTemplate.Where(p =>p.GCTemplateGroup == "X032^001").ToList<TemplateText>(), "TemplateName", "TemplateID");
            Methods.SetComboBoxField<TemplateText>(cboTemplateCompany, lstTemplate.Where(p => p.GCTemplateGroup == "X032^001").ToList<TemplateText>(), "TemplateName", "TemplateID");
            Methods.SetComboBoxField<TemplateText>(cboTemplateConfirmation, lstTemplate.Where(p => p.GCTemplateGroup == "X032^002").ToList<TemplateText>(), "TemplateName", "TemplateID");

            //cboTemplate.SelectedIndex = 0;
        }

        protected override void OnControlEntrySetting()
        {
            SetControlEntrySetting(txtEventCode, new ControlEntrySetting(false, false, false));
            SetControlEntrySetting(txtEventName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStartDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtStartTime, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEndDate, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtEndTime, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(hdnTrainingID, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtTrainingCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTrainingName, new ControlEntrySetting(false, false, true));

            SetControlEntrySetting(hdnVenueID, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtVenueCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtVenueName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtRoomName, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtVenueAddress, new ControlEntrySetting(false, false, false));

            SetControlEntrySetting(hdnTrainerID, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtTrainerCode, new ControlEntrySetting(true, true, true));
            SetControlEntrySetting(txtTrainerName, new ControlEntrySetting(false, false, true));
            SetControlEntrySetting(txtAssistantTrainer, new ControlEntrySetting(true, true, false));

            SetControlEntrySetting(txtPrice, new ControlEntrySetting(true, true, false));
            SetControlEntrySetting(txtRemarks, new ControlEntrySetting(true, true, false));
        }

        private void EntityToControl(vEvent entity)
        {
            txtEventCode.Text = entity.EventCode;
            txtEventName.Text = entity.EventName;
            txtStartDate.Text = entity.StartDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtStartTime.Text = entity.StartTime;
            txtEndDate.Text = entity.EndDate.ToString(Constant.FormatString.DATE_PICKER_FORMAT);
            txtEndTime.Text = entity.EndTime;
            hdnTrainingID.Value = entity.TrainingID.ToString();
            txtTrainingCode.Text = entity.TrainingCode;
            txtTrainingName.Text = entity.TrainingName;

            hdnVenueID.Value = entity.VenueID.ToString();
            txtVenueCode.Text = entity.VenueCode;
            txtVenueName.Text = entity.VenueName;
            txtVenueAddress.Text = entity.Address;
            txtRoomName.Text = entity.RoomName;

            hdnTrainerID.Value = entity.TrainerID.ToString();
            txtTrainerCode.Text = entity.TrainerCode;
            txtTrainerName.Text = entity.TrainerName;
            txtAssistantTrainer.Text = entity.AssistantTrainer;

            txtPrice.Text = entity.Price.ToString();
            txtRemarks.Text = entity.Remarks;
            txtEmailInvitation.Text = entity.DefaultEmailText1;
            txtEmailCompanyInvitation.Text = entity.DefaultEmailText2;
            txtEmailConfirmation.Text = entity.DefaultEmailReminderText;
        }

        private void ControlToEntity(Event entity)
        {
            entity.EventCode = txtEventCode.Text;
            entity.EventName = txtEventName.Text;
            entity.StartDate = Helper.GetDatePickerValue(txtStartDate);
            entity.StartTime = txtStartTime.Text;
            entity.EndDate = Helper.GetDatePickerValue(txtEndDate);
            entity.EndTime = txtEndTime.Text;
            entity.TrainingID = Convert.ToInt32(hdnTrainingID.Value);

            entity.VenueID = Convert.ToInt32(hdnVenueID.Value);
            entity.RoomName = txtRoomName.Text;

            entity.TrainerID = Convert.ToInt32(hdnTrainerID.Value);
            entity.AssistantTrainer = txtAssistantTrainer.Text;

            if (txtPrice.Text != "")
                entity.Price = Convert.ToDecimal(txtPrice.Text);
            else
                entity.Price = 0;
            entity.Remarks = txtRemarks.Text;
            entity.DefaultEmailText1 = Helper.GetHTMLEditorText(this, txtEmailInvitation);
            entity.DefaultEmailText2 = Helper.GetHTMLEditorText(this, txtEmailCompanyInvitation);
            entity.DefaultEmailReminderText = Helper.GetHTMLEditorText(this, txtEmailConfirmation);
        }

        protected override bool OnSaveAddRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            EventDao entityDao = new EventDao(ctx);
            try
            {
                Event entity = new Event();
                ControlToEntity(entity);

                entity.EventCode = BusinessLayer.GenerateEventCode(entity.TrainingID, DateTime.Now.ToString("yy"), ctx);
                ctx.CommandType = CommandType.Text;
                ctx.Command.Parameters.Clear();

                entity.GCEventStatus = Constant.EventStatus.OPENED;
                entity.CreatedBy = AppSession.UserLogin.UserID;
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
                Event entity = BusinessLayer.GetEvent(Convert.ToInt32(hdnID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateEvent(entity);
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