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
using QIS.Data.Core.Dal;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class CloseEventList : BasePageList
    {
        protected int PageCount = 1;
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.CLOSE_EVENT;
        }

        protected override void InitializeDataControl(string filterExpression, string keyValue)
        {
            BindGridView(1, true, ref PageCount);
        }

        public override void SetFilterParameter(ref string[] fieldListText, ref string[] fieldListValue)
        {
            fieldListText = new string[] { "Event Code", "Venue Name" };
            fieldListValue = new string[] { "EventCode", "VenueName" };
        }

        private void BindGridView(int pageIndex, bool isCountPageCount, ref int pageCount)
        {
            string filterExpression = hdnFilterExpression.Value;
            if (filterExpression != "")
                filterExpression += " AND ";
            filterExpression += string.Format("GCEventStatus = '{0}'", Constant.EventStatus.OPENED);

            if (isCountPageCount)
            {
                int rowCount = BusinessLayer.GetvEventRowCount(filterExpression);
                pageCount = Helper.GetPageCount(rowCount, Constant.GridViewPageSize.GRID_MASTER);
            }

            List<vEvent> lstEntity = BusinessLayer.GetvEventList(filterExpression, Constant.GridViewPageSize.GRID_MASTER, pageIndex, "EventID DESC");
            grdView.DataSource = lstEntity;
            grdView.DataBind();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    BindGridView(Convert.ToInt32(param[1]), false, ref pageCount);
                    result = "changepage";
                }
                else // refresh
                {

                    BindGridView(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        protected override bool OnCustomButtonClick(string type, ref string errMessage)
        {
            if (type == "closeevent")
            {
                if (hdnID.Value.ToString() != "")
                {
                    bool result = true;
                    IDbContext ctx = DbFactory.Configure(true);
                    EventDao entityDao = new EventDao(ctx);
                    MemberDao entityMemberDao = new MemberDao(ctx);
                    MemberPastTrainingDao entityPastTrainingDao = new MemberPastTrainingDao(ctx);
                    try
                    {
                        Event entity = entityDao.Get(Convert.ToInt32(hdnID.Value));
                        entity.GCEventStatus = Constant.EventStatus.CLOSED;
                        entity.LastUpdatedBy = AppSession.UserLogin.UserID;
                        entityDao.Update(entity);

                        string filterExpression = string.Format("EventID = {0} AND GCRegistrationStatus = '{1}'", entity.EventID, Constant.RegistrationStatus.CONFIRMED);
                        List<EventRegistration> lstEventRegistration = BusinessLayer.GetEventRegistrationList(filterExpression, ctx);
                        foreach (EventRegistration eventRegistration in lstEventRegistration)
                        {
                            MemberPastTraining entityPastTraining = new MemberPastTraining();
                            entityPastTraining.EventID = entity.EventID;
                            entityPastTraining.Remarks = "";
                            entityPastTraining.MemberID = eventRegistration.MemberID;
                            entityPastTraining.IsFromMigration = false;
                            entityPastTraining.CreatedBy = AppSession.UserLogin.UserID;
                            entityPastTrainingDao.Insert(entityPastTraining);

                            Member member = entityMemberDao.Get(eventRegistration.MemberID);
                            entityPastTraining.ID = BusinessLayer.GetMemberPastTrainingMaxID(ctx);
                            vMemberPastTraining training = BusinessLayer.GetLatestvMemberPastTraining(string.Format("ID = {0} AND MemberID = {1} AND EventID IS NOT NULL AND IsDeleted = 0", entityPastTraining.ID, member.MemberID), ctx);

                            if (training != null)
                            {
                                if (member.LastEventID != training.EventID)
                                {
                                    if (member.LastEventDate <= training.StartDate)
                                    {
                                        member.LastEventID = training.EventID;
                                        member.LastEventDate = training.StartDate;
                                        member.LastCompanyID = eventRegistration.CompanyID;
                                    }
                                    member.NumberOfTraining++;
                                }
                            }
                            member.LastUpdatedBy = AppSession.UserLogin.UserID;
                            entityMemberDao.Update(member);
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
                return false;
            }
            return false;
        }
    }
}