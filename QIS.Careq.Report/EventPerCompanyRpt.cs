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
    public partial class EventPerCompanyRpt : BaseDailyLandscapeRpt
    {
        public EventPerCompanyRpt()
        {
            InitializeComponent();
        }

        public override void InitializeReport(string[] param)
        {
            List<vEventRegistration> lstEntity = BusinessLayer.GetvEventRegistrationList(param[0]);
            if (lstEntity.Count > 0)
            {
                lblCompanyName.Text = lstEntity[0].CompanyName;
            }

            base.InitializeReport(param);
        }
    }
}
