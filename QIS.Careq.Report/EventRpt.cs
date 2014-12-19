using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QIS.Medinfras.Data.Service;
using QIS.Careq.Web.Common;
using System.Collections.Generic;
using QIS.Careq.Data.Service;

namespace QIS.Careq.Report
{
    public partial class EventRpt : BaseDailyLandscapeRpt
    {
        public EventRpt()
        {
            InitializeComponent();
        }

        public override void InitializeReport(string[] param)
        {
            List<vEventRegistration> lstEntity = BusinessLayer.GetvEventRegistrationList(string.Format("EventID = {0}", param[0]));
            if (lstEntity.Count > 0)
            {
                lblEventName.Text = lstEntity[0].EventName;
                lblEventDate.Text = lstEntity[0].cfEventDateReport;
                lblVenueName.Text = lstEntity[0].VenueName;
            }
            else
            {
                vEvent entityEvent = BusinessLayer.GetvEventList(string.Format("EventID = {0}", param[0]))[0];
                lblEventName.Text = entityEvent.EventName;
                lblEventDate.Text = entityEvent.cfEventDate;
                lblVenueName.Text = entityEvent.VenueName;
            }

            base.InitializeReport(param);
        }
    }
}
