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
    public partial class EventConfirmationSendEmailCompanyCtl : BaseViewPopupCtl
    {
        private static List<Variable> lstCompany = null;

        public override void InitializeDataControl(string queryString)
        {
            string[] param = queryString.Split('|');

            hdnEventID.Value = param[0];
            hdnSelectedMember.Value = param[1];
            
            if (hdnSelectedMember.Value.StartsWith(","))
                hdnSelectedMember.Value = hdnSelectedMember.Value.Substring(1);
            List<vMember> lstMember = BusinessLayer.GetvMemberList(string.Format("MemberID IN ({0})", hdnSelectedMember.Value));
            lstCompany = (from m in lstMember select new Variable { Value = m.CompanyName, Code = m.CompanyID.ToString() }).GroupBy(p => p.Code).Select(p => p.First()).ToList();
            
            Methods.SetComboBoxField<Variable>(cboCompany, lstCompany, "Value", "Code");
            cboCompany.SelectedIndex = 0;

            CreatePreview(Convert.ToInt32(lstCompany[0].Code));
            
        }

        protected void cbpSendEmailRefresh_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            CreatePreview(Convert.ToInt32(cboCompany.Value));
        }

        public String CreateMessage(List<vEventRegistration> entity) 
        {
            string tempMessageBody = entity[0].DefaultEmailText2;
            tempMessageBody = tempMessageBody.Replace("&lt;", "<");
            tempMessageBody = tempMessageBody.Replace("&gt;", ">");
            tempMessageBody = tempMessageBody.Replace("<%CompanyName%>", entity[0].CompanyName);
            tempMessageBody = tempMessageBody.Replace("<%EventName%>", entity[0].EventName);
            tempMessageBody = tempMessageBody.Replace("<%VenueName%>", entity[0].VenueName);
            tempMessageBody = tempMessageBody.Replace("<%RoomName%>", entity[0].RoomName);
            tempMessageBody = tempMessageBody.Replace("<%StartDate%>", entity[0].StartDateInString);
            tempMessageBody = tempMessageBody.Replace("<%StartTime%>", entity[0].StartTime);
            tempMessageBody = tempMessageBody.Replace("<%EndDate%>", entity[0].EndDateInString);
            tempMessageBody = tempMessageBody.Replace("<%EndTime%>", entity[0].EndTime);

            tempMessageBody = tempMessageBody.Replace("<%Location%>", entity[0].EventAddress);
            tempMessageBody = tempMessageBody.Replace("<%TrainerName%>", entity[0].TrainerName);
            
            List<String> lst = (from m in entity select m.MemberName).ToList();
            String lstMemberInString = String.Join(",", lst.ToArray());
            tempMessageBody = tempMessageBody.Replace("<%ListMember%>", lstMemberInString);

            Decimal LineAmount = entity[0].Price * lst.Count();
            if (entity[0].IsFinalDiscount) LineAmount = LineAmount - entity[0].DiscountAmount;
            else LineAmount = LineAmount - (entity[0].DiscountAmount * lst.Count());
            tempMessageBody = tempMessageBody.Replace("<%Price%>", LineAmount.ToString("0,0"));
            tempMessageBody = tempMessageBody.Replace("<%PriceInWord%>", Helper.NumberInWords(Convert.ToInt64(LineAmount)));
            return tempMessageBody;
        }

        public void CreatePreview(Int32 CompanyID) 
        {
            List<vEventRegistration> entity = BusinessLayer.GetvEventRegistrationList(String.Format("EventID = {0} AND CompanyID = {1}", hdnEventID.Value, CompanyID));
            String tempMessageBody = CreateMessage(entity);

            StringBuilder lstMemberEmail = new StringBuilder();
            foreach (vEventRegistration member in entity)
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

            txtSubject.Text = string.Format("{0}", entity[0].EventName);
            txtTo.Text = lstMemberEmail.ToString();
            divContent.InnerHtml = tempMessageBody;
        }

        protected void cbpSendEmailProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string result = "";
            try
            {
                User user = BusinessLayer.GetUser(AppSession.UserLogin.UserID);
                string fromEmailAddress = user.Email;
                string emailPassword = user.EmailPassword;

                List<SettingParameter> lstSpEmailFrom = BusinessLayer.GetSettingParameterList(string.Format("ParameterCode IN ('{0}','{1}','{2}')", Constant.SettingParameter.DEFAULT_EMAIL_ADDRESS, Constant.SettingParameter.DEFAULT_EMAIL_SMTP, Constant.SettingParameter.DEFAULT_EMAIL_PORT));
                //string[] credentials = lstSpEmailFrom.FirstOrDefault(sp => sp.ParameterCode == Constant.SettingParameter.DEFAULT_EMAIL_ADDRESS).ParameterValue.Split('|');
                
                //string fromEmailAddress = credentials[0];
                //string emailPassword = credentials[1];
                
                string smpt = lstSpEmailFrom.FirstOrDefault(sp => sp.ParameterCode == Constant.SettingParameter.DEFAULT_EMAIL_SMTP).ParameterValue;
                int port = Convert.ToInt32(lstSpEmailFrom.FirstOrDefault(sp => sp.ParameterCode == Constant.SettingParameter.DEFAULT_EMAIL_PORT).ParameterValue);

                foreach (Variable company in lstCompany)  
                {
                    List<vEventRegistration> lstEntity = BusinessLayer.GetvEventRegistrationList(String.Format("EventID = {0} AND CompanyID = {1}", hdnEventID.Value, company.Code));
                    
                    string tempMessageBody = CreateMessage(lstEntity);
                    string messageBody = tempMessageBody;
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(fromEmailAddress);
                    
                    foreach (vEventRegistration member in lstEntity)
                    {
                        if (member.EmailAddress1 != "") message.To.Add(member.EmailAddress1);
                        if (member.EmailAddress2 != "") message.To.Add(member.EmailAddress2);
                    }
                    message.Subject = string.Format("{0}", lstEntity[0].EventName);
                    message.Body = messageBody;
                    message.IsBodyHtml = true;

                    SmtpClient client = new SmtpClient(smpt, port);
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(fromEmailAddress, emailPassword);
                    client.Send(message);
                    result = "success";
                }
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