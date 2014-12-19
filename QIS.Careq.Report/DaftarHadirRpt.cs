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
    public partial class DaftarHadirRpt : BaseRpt
    {
        public DaftarHadirRpt()
        {
            InitializeComponent();
        }

        private List<XRTableCell> lstTd = null;
        public override void InitializeReport(string[] param)
        {
            vEvent entity = BusinessLayer.GetvEventList(string.Format("EventID = {0}", param[0]))[0];
            lblEventName.Text = entity.EventName;
            lblVenueName.Text = entity.VenueName;

            string eventDate = "";
            if (DateTime.Compare(entity.StartDate, entity.EndDate) == 0)
                eventDate = entity.StartDate.ToString("dd MMMM yyyy");
            else if (entity.StartDate.Month == entity.EndDate.Month)
                eventDate = string.Format("{0} - {1} {2}", entity.StartDate.Day, entity.EndDate.Day, entity.StartDate.ToString("MMMM yyyy"));
            else
                eventDate = string.Format("{0} - {1}", entity.StartDate.ToString("dd MMMM yyyy"), entity.EndDate.ToString("dd MMMM yyyy"));

            lblEventDate.Text = eventDate;

            lstTd = new List<XRTableCell>();
            
            int numDays = Convert.ToInt32((entity.EndDate - entity.StartDate).TotalDays);
            AddColumn("Day #1", true);
            for (int i = 0; i <= numDays; ++i)
            {
                string headerText = string.Format("Day #{0}", (i + 2));
                if (i < numDays)
                    AddColumn(headerText, true);
                else
                {
                    AddColumn("#", true);
                    AddColumn(headerText, false);
                }
            }
            foreach (XRTableCell td in lstTd)
            {
                td.WidthF = 60;
            }

            trRecordHeader.Cells.RemoveAt(trRecordHeader.Cells.Count - 1);
            trRecordDt.Cells.RemoveAt(trRecordDt.Cells.Count - 1);
        }

        private void AddColumn(string headerText, bool isAddToList)
        {
            XRTableCell tdHeader = new XRTableCell();
            tdHeader.WidthF = 60;
            tdHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            tdHeader.Text = headerText;
            trRecordHeader.Cells.Add(tdHeader);

            XRTableCell td = new XRTableCell();
            td.WidthF = 60;
            trRecordDt.Cells.Add(td);

            if (isAddToList)
            {
                lstTd.Add(tdHeader);
                lstTd.Add(td);
            }
        }
    }
}
