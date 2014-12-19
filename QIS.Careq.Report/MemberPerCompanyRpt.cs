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
    public partial class MemberPerCompanyRpt : BaseDailyLandscapeRpt
    {
        public MemberPerCompanyRpt()
        {
            InitializeComponent();
        }

        public override void InitializeReport(string[] param)
        {
            vCompany entityEvent = BusinessLayer.GetvCompanyList(param[0])[0];
            lblCompanyName.Text = entityEvent.CompanyName;

            base.InitializeReport(param);
        }
    }
}
