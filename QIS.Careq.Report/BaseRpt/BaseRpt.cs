using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QIS.Medinfras.Data.Service;
using QIS.Careq.Web.Common;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using QIS.Careq.Data.Service;

namespace QIS.Careq.Report
{
    public partial class BaseRpt : DevExpress.XtraReports.UI.XtraReport
    {
        protected ReportMaster reportMaster = null;
        public BaseRpt()
        {
            InitializeComponent();
        }

        public void Init(int reportID, string reportCode, string[] param)
        {
            reportMaster = BusinessLayer.GetReportMaster(reportID);
            InitializeReport(param);

            if (reportMaster.GCDataSourceType == Constant.DataSourceType.VIEW)
                BindingView(reportMaster, reportID, param);
        }

        public virtual void InitializeReport(string[] param)
        {
        }

        private void BindingView(ReportMaster reportMaster, int reportID, string[] param)
        {
            List<vReportParameter> listReportParameter = BusinessLayer.GetvReportParameterList(string.Format("ReportID = {0} ORDER BY DisplayOrder", reportID));
            string filterExpression = String.Empty;
            for (int i = 1; i <= listReportParameter.Count; ++i)
            {
                if (i > 1)
                    filterExpression += " AND ";
                if (listReportParameter[i - 1].GCFilterParameterType == Constant.FilterParameterType.FREE_TEXT)
                    filterExpression += param[i - 1];
                else
                {
                    filterExpression += listReportParameter[i - 1].FieldName;

                    if (listReportParameter[i - 1].GCFilterParameterType == Constant.FilterParameterType.DATE ||
                        listReportParameter[i - 1].GCFilterParameterType == Constant.FilterParameterType.PAST_PERIOD ||
                        listReportParameter[i - 1].GCFilterParameterType == Constant.FilterParameterType.UPCOMING_PERIOD)
                    {
                        string[] date = param[i - 1].Split(';');
                        string startDate = date[0];
                        string endDate = date[1];
                        filterExpression += string.Format(" BETWEEN '{0}' AND '{1}'", startDate, endDate);

                    }
                    else if (listReportParameter[i - 1].GCFilterParameterType == Constant.FilterParameterType.COMBO_BOX)
                    {
                        string[] paramSplit = param[i - 1].Split(';');
                        string value = paramSplit[0];
                        filterExpression += string.Format(" = '{0}'", value);
                    }
                    else
                    {
                        string[] paramSplit = param[i - 1].Split(';');
                        StringBuilder sbFilterExpressionVal = new StringBuilder();
                        StringBuilder sbTemp = new StringBuilder();

                        for (int idxValue = 0; idxValue < paramSplit.Length; idxValue++)
                        {
                            string value = paramSplit[idxValue];
                            if (sbTemp.ToString() != "")
                                sbTemp.Append(",");

                            sbTemp.Append("'").Append(value).Append("'");
                        }
                        sbFilterExpressionVal.Append(" IN (").Append(sbTemp.ToString()).Append(")");
                        filterExpression += sbFilterExpressionVal.ToString();
                    }
                }
            }
            //if (filterExpression != "")
            //    filterExpression += " AND ";
            //filterExpression += reportMaster.AdditionalFilterExpression;
            if (reportMaster.AdditionalFilterExpression != "")
                filterExpression += " AND ";
            filterExpression += reportMaster.AdditionalFilterExpression;

            MethodInfo method = typeof(BusinessLayer).GetMethod(reportMaster.ObjectTypeName, new[] { typeof(string) });
            object obj = method.Invoke(null, new string[] { filterExpression });
            this.DataSource = obj;
        }
    }
}
