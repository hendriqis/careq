using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QIS.Medinfras.Data.Service;
using QIS.Careq.Web.Common;
using System.Collections.Generic;

namespace QIS.Careq.Report
{
    public partial class BaseDailyLandscapeRpt : BaseRpt
    {
        public BaseDailyLandscapeRpt()
        {
            InitializeComponent();

            
        }

        public override void InitializeReport(string[] param)
        {
            lblTitle.Text = reportMaster.ReportTitle1;
            //lblReportProperties.Text = string.Format("CAREQ - {0}, Print Date/Time:{1}, User ID:{2}", reportMaster.ReportCode, DateTime.Now.ToString("dd-MMM-yyyy/HH:mm:ss"), AppSession.UserLogin.UserName);
            lblReportProperties.Text = string.Format("CAREQ - {0}, Print Date/Time:{1}, User ID:{2}", reportMaster.ReportCode, DateTime.Now.ToString("dd-MMM-yyyy/HH:mm:ss"), "admin");
        }
    }
}
