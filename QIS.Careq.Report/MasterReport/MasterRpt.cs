using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using QIS.Medinfras.Data.Service;
using QIS.Careq.Web.Common;
using System.Collections.Generic;
using QIS.Careq.Data.Service;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace QIS.Careq.Report
{
    public partial class MasterRpt : BaseRpt
    {
        public MasterRpt()
        {
            InitializeComponent();

            string path = "~/Program/Trainer/TrainerList.aspx";

            string content = string.Empty;
            path = Helper.MapPath(path);

            StreamReader sr = new StreamReader(path);
            content = sr.ReadToEnd();

            Regex regex = new Regex("<asp:BoundField(.*)/>");

            MatchCollection matches = regex.Matches(content);
            List<string> myResultList = new List<string>();
            foreach (Match match in matches)
            {
                string result = match.Value;
                //result = result.Replace("\"", "");
                myResultList.Add(result);
            }

            trRecordHeader.Cells.Clear();
            trRecordDt.Cells.Clear();

            int numDefaultWidth = 0;
            float widthTotal = 0;
            List<BoundField> lstBoundField = new List<BoundField>();
            foreach (string field in myResultList)
            {
                BoundField boundField = new BoundField();
                boundField.DataField = GetRegexContent(field, "DataField=\"([a-zA-Z0-9]*)\"");
                boundField.HeaderText = GetRegexContent(field, "HeaderText=\"([a-zA-Z0-9 ]*)\"");
                boundField.HeaderStyleWidth = GetRegexContent(field, "HeaderStyle-Width=\"([a-zA-Z0-9]*)\"").Replace("px", "");
                boundField.HeaderStyleCssClass = GetRegexContent(field, "HeaderStyle-CssClass=\"([a-zA-Z0-9]*)\"");
                boundField.ItemStyleCssClass = GetRegexContent(field, "ItemStyle-CssClass=\"([a-zA-Z0-9]*)\"");
                if (boundField.ItemStyleCssClass != "keyField")
                {
                    if (boundField.HeaderStyleWidth == "")
                        numDefaultWidth++;
                    else
                        widthTotal += (float)Convert.ToDouble(boundField.HeaderStyleWidth);
                    XRTableCell th = new XRTableCell();
                    th.Text = boundField.HeaderText.ToUpper();
                    th.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

                    XRTableCell td = new XRTableCell();
                    td.Text = string.Format("[{0}]", boundField.DataField);
                    td.Padding = 3;

                    trRecordHeader.Cells.Add(th);
                    trRecordDt.Cells.Add(td);

                    lstBoundField.Add(boundField);
                }
            }

            int ctr = 0;

            float selisihWidth = trRecordHeader.WidthF - widthTotal;
            float automaticWidth = selisihWidth / numDefaultWidth;
            foreach (BoundField boundField in lstBoundField)
            {
                if (boundField.HeaderStyleWidth != "")
                    trRecordHeader.Cells[ctr].WidthF = trRecordDt.Cells[ctr].WidthF = (float)Convert.ToDouble(boundField.HeaderStyleWidth);
                else
                    trRecordHeader.Cells[ctr].WidthF = trRecordDt.Cells[ctr].WidthF = automaticWidth;
                ctr++;
            }
        }

        private String GetRegexContent(string content, string pattern)
        {
            var v = Regex.Match(content, pattern);
            return v.Groups[1].ToString();
        }

        class BoundField
        {
            public String DataField { get; set; }
            public String HeaderText { get; set; }
            public String HeaderStyleWidth { get; set; }
            public String HeaderStyleCssClass { get; set; }
            public String ItemStyleCssClass { get; set; }
        }
    }
}
