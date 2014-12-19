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
using System.Globalization;
using DevExpress.Web.ASPxEditors;
using QIS.Data.Core.Dal;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class MemberPastTrainingCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            hdnMemberID.Value = param;
            vMember entity = BusinessLayer.GetvMemberList(string.Format("MemberID = {0}", hdnMemberID.Value))[0];
            txtHeaderText.Text = string.Format("{0} - {1}", entity.MemberCode, entity.MemberName);

            BindGridView();

            fillDate();
            //txtDepartmentCode.Attributes.Add("validationgroup", "mpEntryPopup");
            //txtContactPersonName.Attributes.Add("validationgroup", "mpEntryPopup");
        }

        private void BindGridView()
        {
            grdView.DataSource = BusinessLayer.GetvMemberPastTrainingList(string.Format("MemberID = {0} AND IsDeleted = 0 ORDER BY ID DESC", hdnMemberID.Value));
            grdView.DataBind();
        }

        private void fillDate()
        {
            cboTrainingMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
            {
                MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),
                MonthNumber = a
            });
            cboTrainingMonth.TextField = "MonthName";
            cboTrainingMonth.ValueField = "MonthNumber";
            cboTrainingMonth.EnableCallbackMode = false;
            cboTrainingMonth.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboTrainingMonth.DropDownStyle = DropDownStyle.DropDownList;
            cboTrainingMonth.DataBind();
            cboTrainingMonth.Items.Insert(0, new ListEditItem { Value = "", Text = "" });

            cboTrainingYear.DataSource = Enumerable.Range(DateTime.Now.Year - 99, 100).Reverse();
            cboTrainingYear.EnableCallbackMode = false;
            cboTrainingYear.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboTrainingYear.DropDownStyle = DropDownStyle.DropDownList;
            cboTrainingYear.DataBind();
            cboTrainingYear.Items.Insert(0, new ListEditItem { Value = "", Text = "" });

            cboTrainingDate.DataSource = Enumerable.Range(1, 31);
            cboTrainingDate.EnableCallbackMode = false;
            cboTrainingDate.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cboTrainingDate.DropDownStyle = DropDownStyle.DropDownList;
            cboTrainingDate.DataBind();
            cboTrainingDate.Items.Insert(0, new ListEditItem { Value = "", Text = "" });

            cboTrainingYear.SelectedIndex = 0;
            cboTrainingMonth.SelectedIndex = 0;
            cboTrainingDate.SelectedIndex = 0;
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

            string param = e.Parameter;

            string result = param + "|";
            string errMessage = "";

            if (param == "save")
            {
                if (hdnEntryID.Value.ToString() != "")
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
            else if (param == "delete")
            {
                if (OnDeleteRecord(ref errMessage))
                    result += "success";
                else
                    result += string.Format("fail|{0}", errMessage);
            }

            BindGridView();

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private void ControlToEntity(MemberPastTraining entity)
        {
            if (hdnEventID.Value != "")
                entity.EventID = Convert.ToInt32(hdnEventID.Value);
            else
            {
                entity.TrainingYear = cboTrainingYear.Value.ToString();
                if (cboTrainingMonth.Value != null && cboTrainingMonth.Value.ToString() != "")
                    entity.TrainingMonth = String.Format("{0:00}", Convert.ToInt32(cboTrainingMonth.Value));
                else
                    entity.TrainingMonth = "00";
                if (cboTrainingDate.Value != null && cboTrainingDate.Value.ToString() != "")
                    entity.TrainingDate = String.Format("{0:00}", Convert.ToInt32(cboTrainingDate.Value));
                else
                    entity.TrainingDate = "00";

                entity.TrainingDuration = Convert.ToInt16(txtTrainingDuration.Text);
                entity.EventID = null;
                if (hdnTrainingID.Value != "")
                    entity.TrainingID = Convert.ToInt32(hdnTrainingID.Value);
                else
                {
                    entity.TrainingID = null;
                    entity.TrainingName = txtTrainingName.Text;
                }
                entity.TrainerName = txtTrainer.Text;
                entity.VenueName = txtVenueName.Text;
                entity.VenueLocation = txtVenueLocation.Text;
            }
            entity.Remarks = txtRemarks.Text;
        }

        private bool OnSaveAddRecord(ref string errMessage)
        {
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            MemberPastTrainingDao entityDao = new MemberPastTrainingDao(ctx);
            MemberDao entityMemberDao = new MemberDao(ctx);
            try
            {
                MemberPastTraining entity = new MemberPastTraining();
                ControlToEntity(entity);
                entity.MemberID = Convert.ToInt32(hdnMemberID.Value);
                entity.IsFromMigration = false;
                entity.CreatedBy = AppSession.UserLogin.UserID;
                entityDao.Insert(entity);

                Member member = entityMemberDao.Get(entity.MemberID);
                entity.ID = BusinessLayer.GetMemberPastTrainingMaxID(ctx);
                vMemberPastTraining training = BusinessLayer.GetLatestvMemberPastTraining(string.Format("ID = {0} AND MemberID = {1} AND EventID IS NOT NULL AND IsDeleted = 0", entity.ID, member.MemberID), ctx);

                if (training != null)
                {
                    if (member.LastEventID != training.EventID)
                    {
                        if (member.LastEventDate <= training.StartDate)
                        {
                            member.LastEventID = training.EventID;
                            member.LastEventDate = training.StartDate;
                            member.LastCompanyID = member.CompanyID;
                        }
                        member.NumberOfTraining++;
                    }
                }
                member.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityMemberDao.Update(member);
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

        private bool OnSaveEditRecord(ref string errMessage)
        {
            try
            {
                MemberPastTraining entity = BusinessLayer.GetMemberPastTraining(Convert.ToInt32(hdnEntryID.Value));
                ControlToEntity(entity);
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                BusinessLayer.UpdateMemberPastTraining(entity);
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
            bool result = true;
            IDbContext ctx = DbFactory.Configure(true);
            MemberPastTrainingDao entityDao = new MemberPastTrainingDao(ctx);
            MemberDao entityMemberDao = new MemberDao(ctx);
            try
            {
                MemberPastTraining entity = entityDao.Get(Convert.ToInt32(hdnEntryID.Value));
                entity.IsDeleted = true;
                entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityDao.Update(entity);

                Member member = entityMemberDao.Get(entity.MemberID);
                member.NumberOfTraining--;
                member.LastUpdatedBy = AppSession.UserLogin.UserID;
                entityMemberDao.Update(member);
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