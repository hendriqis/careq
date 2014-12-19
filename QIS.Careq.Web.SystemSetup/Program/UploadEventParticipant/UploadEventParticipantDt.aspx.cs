using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Data.Service;
using QIS.Careq.Web.Common;
using QIS.Careq.Web.Common.UI;
using DevExpress.Web.ASPxCallbackPanel;
using System.Data.OleDb;
using System.Data;
using System.Text.RegularExpressions;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class UploadEventParticipantDt : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.UPLOAD_EVENT_PARTICIPANT_FROM_EXCEL;
        }

        public override void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = false;
        }

        protected override void InitializeDataControl()
        {
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string file = hdnUploadedFile1.Value;
            if (file != "")
            {
                string[] parts = Regex.Split(file, ",").Skip(1).ToArray();
                file = String.Join(",", parts);

                file = DecodeBase64(file);
                List<vMember> lstMember = new List<vMember>();
                string[] allParam = file.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                for (int i = 1; i < allParam.Length; ++i)
                {
                    if (allParam[i].Trim() != "")
                    {
                        string[] param = allParam[i].Split(',');

                        vMember entity = new vMember();
                        entity.FirstName = param[0].ToUpper();
                        entity.MiddleName = param[1].ToUpper();
                        entity.LastName = param[2].ToUpper();
                        entity.Occupation = param[3];
                        entity.MobilePhoneNo1 = param[4];
                        entity.MobilePhoneNo2 = param[5];
                        entity.EmailAddress1 = param[6];
                        entity.EmailAddress2 = param[7];
                        entity.Department = param[8];

                        lstMember.Add(entity);
                    }
                }
                grdView.DataSource = lstMember;
                grdView.DataBind();
            }
        }

        private string DecodeBase64(string text)
        {
            string result = null;
            try
            {
                var converted = Convert.FromBase64String(text);
                result = System.Text.Encoding.UTF8.GetString(converted);
            }
            catch (System.ArgumentNullException)
            {
                //handle it
            }
            catch (System.FormatException)
            {
                //handle it
            }
            return result;
        }
    }
}