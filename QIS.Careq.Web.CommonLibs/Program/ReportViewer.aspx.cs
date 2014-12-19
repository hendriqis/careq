using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Data.Service;
using System.Reflection;
using QIS.Careq.Report;

namespace QIS.Careq.Web.CommonLibs.Program
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["param"] != null)
                hdnParam.Value = Request.Form["param"].ToString();

            string[] param = hdnParam.Value.Split('|');

            string reportCode = Page.Request.QueryString["id"];
            List<ReportMaster> lstReportMaster = BusinessLayer.GetReportMasterList(string.Format("ReportCode = '{0}'", reportCode));
            if (lstReportMaster.Count < 1)
                throw new Exception(string.Format("Report with code {0} is not defined", reportCode));
            ReportMaster reportMaster = lstReportMaster[0];
            BaseRpt report = GetReport(reportMaster.ClassName);
            report.Init(reportMaster.ReportID, reportCode, param);
            this.ReportViewer1.Report = report;

        }

        public BaseRpt GetReport(string className)
        {
            Assembly assembly = Assembly.Load("QIS.Careq.Report, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            Object o = assembly.CreateInstance("QIS.Careq.Report." + className);
            return (BaseRpt)o;
        }
    }
}