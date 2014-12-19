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
using System.Text;
using System.Net.Mail;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class EventInvitationSendEmailCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string queryString)
        {
            string[] param = queryString.Split('|');

            hdnEventID.Value = param[0];
            hdnSelectedMember.Value = param[1];

            vEvent entity = BusinessLayer.GetvEventList(string.Format("EventID = {0}", hdnEventID.Value))[0];
            if (hdnSelectedMember.Value.StartsWith(","))
                hdnSelectedMember.Value = hdnSelectedMember.Value.Substring(1);
            List<vMember> lstMember = BusinessLayer.GetvMemberList(string.Format("MemberID IN ({0})", hdnSelectedMember.Value));

            string tempMessageBody = entity.DefaultEmailText1;
            tempMessageBody = tempMessageBody.Replace("&lt;", "<");
            tempMessageBody = tempMessageBody.Replace("&gt;", ">");
            tempMessageBody = tempMessageBody.Replace("<%EventName%>", entity.EventName);
            tempMessageBody = tempMessageBody.Replace("<%VenueName%>", entity.VenueName);
            tempMessageBody = tempMessageBody.Replace("<%RoomName%>", entity.RoomName);
            tempMessageBody = tempMessageBody.Replace("<%StartDate%>", entity.StartDateInString);
            tempMessageBody = tempMessageBody.Replace("<%StartTime%>", entity.StartTime);
            tempMessageBody = tempMessageBody.Replace("<%EndDate%>", entity.EndDateInString);
            tempMessageBody = tempMessageBody.Replace("<%EndTime%>", entity.EndTime);
            tempMessageBody = tempMessageBody.Replace("<%Location%>", entity.Address);
            tempMessageBody = tempMessageBody.Replace("<%TrainerName%>", entity.TrainerName);
            tempMessageBody = tempMessageBody.Replace("<%Price%>", entity.Price.ToString("0,0"));

            StringBuilder lstMemberEmail = new StringBuilder();
            foreach (vMember member in lstMember)
            {
                if (member.EmailAddress1 != "")
                {
                    if (lstMemberEmail.ToString() != "")
                        lstMemberEmail.Append(", ");
                    lstMemberEmail.Append(member.EmailAddress1);
                }
                if (member.EmailAddress2 != "")
                {
                    if (lstMemberEmail.ToString() != "")
                        lstMemberEmail.Append(", ");
                    lstMemberEmail.Append(member.EmailAddress2);
                }
            }

            txtSubject.Text = string.Format("{0}", entity.EventName);
            txtTo.Text = lstMemberEmail.ToString();
            divContent.InnerHtml = tempMessageBody;
        }

        protected void cbpSendEmailProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            try
            {
                vEvent entity = BusinessLayer.GetvEventList(string.Format("EventID = {0}", hdnEventID.Value))[0];

                List<SettingParameter> lstSpEmailFrom = BusinessLayer.GetSettingParameterList(string.Format("ParameterCode IN ('{0}','{1}','{2}')", Constant.SettingParameter.DEFAULT_EMAIL_ADDRESS, Constant.SettingParameter.DEFAULT_EMAIL_SMTP, Constant.SettingParameter.DEFAULT_EMAIL_PORT));
                string[] credentials = lstSpEmailFrom.FirstOrDefault(sp => sp.ParameterCode == Constant.SettingParameter.DEFAULT_EMAIL_ADDRESS).ParameterValue.Split('|');
                string fromEmailAddress = credentials[0];
                string emailPassword = credentials[1];
                string smpt = lstSpEmailFrom.FirstOrDefault(sp => sp.ParameterCode == Constant.SettingParameter.DEFAULT_EMAIL_SMTP).ParameterValue;
                int port = Convert.ToInt32(lstSpEmailFrom.FirstOrDefault(sp => sp.ParameterCode == Constant.SettingParameter.DEFAULT_EMAIL_PORT).ParameterValue);

                string tempMessageBody = entity.DefaultEmailText1;
                tempMessageBody = tempMessageBody.Replace("&lt;", "<");
                tempMessageBody = tempMessageBody.Replace("&gt;", ">");
                tempMessageBody = tempMessageBody.Replace("<%EventName%>", entity.EventName);
                tempMessageBody = tempMessageBody.Replace("<%VenueName%>", entity.VenueName);
                tempMessageBody = tempMessageBody.Replace("<%RoomName%>", entity.RoomName);
                tempMessageBody = tempMessageBody.Replace("<%StartDate%>", entity.StartDateInString);
                tempMessageBody = tempMessageBody.Replace("<%StartTime%>", entity.StartTime);
                tempMessageBody = tempMessageBody.Replace("<%EndDate%>", entity.EndDateInString);
                tempMessageBody = tempMessageBody.Replace("<%EndTime%>", entity.EndTime);
                tempMessageBody = tempMessageBody.Replace("<%Location%>", entity.Address);
                tempMessageBody = tempMessageBody.Replace("<%TrainerName%>", entity.TrainerName);
                tempMessageBody = tempMessageBody.Replace("<%Price%>", entity.Price.ToString("N"));

                if (hdnSelectedMember.Value != "")
                {
                    List<vMember> lstMember = BusinessLayer.GetvMemberList(string.Format("MemberID IN ({0})", hdnSelectedMember.Value));
                    List<EventInvitation> lstEventInvitation = BusinessLayer.GetEventInvitationList(string.Format("EventID = {0} AND MemberID IN ({1})", hdnEventID.Value, hdnSelectedMember.Value));
                    foreach (vMember member in lstMember)
                    {
                        if (member.EmailAddress1 != "" || member.EmailAddress2 != "")
                        {
                            if (member.EmailAddress1 != "")
                            {
                                string messageBody = tempMessageBody.Replace("<%MemberName%>", member.MemberName);
                                MailMessage message = new MailMessage();
                                message.From = new MailAddress(fromEmailAddress);
                                message.To.Add(member.EmailAddress1);
                                message.Subject = string.Format("{0}", entity.EventName);
                                message.Body = messageBody;
                                message.IsBodyHtml = true;

                                SmtpClient client = new SmtpClient(smpt, port);
                                client.UseDefaultCredentials = false;
                                client.Credentials = new System.Net.NetworkCredential(fromEmailAddress, credentials[1]);
                                client.Send(message);
                            }
                            if (member.EmailAddress2 != "")
                            {
                                string messageBody = tempMessageBody.Replace("<%MemberName%>", member.MemberName);
                                MailMessage message = new MailMessage();
                                message.From = new MailAddress(fromEmailAddress);
                                message.To.Add(member.EmailAddress2);
                                message.Subject = string.Format("{0}", entity.EventName);
                                message.Body = messageBody;
                                message.IsBodyHtml = true;

                                SmtpClient client = new SmtpClient(smpt, port);
                                client.UseDefaultCredentials = false;
                                client.Credentials = new System.Net.NetworkCredential(fromEmailAddress, credentials[1]);
                                client.Send(message);
                            }

                            EventInvitation obj = lstEventInvitation.FirstOrDefault(p => p.EventID == entity.EventID && p.MemberID == member.MemberID);
                            if (obj == null)
                            {
                                EventInvitation eventInvitation = new EventInvitation();
                                eventInvitation.EventID = entity.EventID;
                                eventInvitation.MemberID = member.MemberID;
                                eventInvitation.CompanyID = member.CompanyID;
                                eventInvitation.IsConfirmed = false;
                                eventInvitation.CreatedDate = DateTime.Now;
                                BusinessLayer.InsertEventInvitation(eventInvitation);
                            }
                        }
                    }
                }
                result = "success";
            }
            catch (Exception ex)
            {
                result = string.Format("fail|{0}", ex.Message);
            }
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }
    }
}