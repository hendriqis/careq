using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.Utils;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class PivotReporting : BasePageList
    {
        public override string OnGetMenuCode()
        {
            return Constant.MenuCode.PIVOT_REPORTING;
        }
        protected override void InitializeDataControl()
        {
            UpdatePivotGridFieldLayout();
        }


        void UpdatePivotGridFieldLayout()
        {
            ChangePivotGridFieldLayout();
        }
        void ChangePivotGridFieldLayout()
        {
            pvRegistration.BeginUpdate();
            foreach (PivotGridField field in pvRegistration.Fields)
            {
                field.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea;
                field.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Ascending;
            }
            pvRegistration.Fields["Year"].Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            pvRegistration.Fields["Year"].AreaIndex = 0;
            pvRegistration.Fields["Month"].Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            pvRegistration.Fields["Month"].AreaIndex = 1;
            pvRegistration.Fields["NumberTraining"].Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            pvRegistration.Fields["Company"].Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            pvRegistration.Fields["Company"].AreaIndex = 0;
            pvRegistration.Fields["Company"].TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.AutomaticTotals;

            pvRegistration.Width = Unit.Percentage(100);
            pvRegistration.EndUpdate();
        }

        protected void cbpView_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            DateTime startDate = Helper.GetDatePickerValue(txtValueDateFrom.Text);
            DateTime endDate = Helper.GetDatePickerValue(txtValueDateTo.Text);
            hdnFilterExpression1.Value = string.Format("(EventDate BETWEEN '{0}' AND '{1}')", startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"));
            UpdatePivotGridFieldLayout();
        }

        protected void btnSavePivot_Click(object sender, EventArgs e)
        {
            Export(true);
        }

        void Export(bool saveAs)
        {
            ASPxPivotGridExporter1.OptionsPrint.PrintHeadersOnEveryPage = chkPrintHeadersOnEveryPage.Checked;
            ASPxPivotGridExporter1.OptionsPrint.PrintFilterHeaders = chkPrintFilterHeaders.Checked ? DefaultBoolean.True : DefaultBoolean.False;
            ASPxPivotGridExporter1.OptionsPrint.PrintColumnHeaders = chkPrintColumnHeaders.Checked ? DefaultBoolean.True : DefaultBoolean.False;
            ASPxPivotGridExporter1.OptionsPrint.PrintRowHeaders = chkPrintRowHeaders.Checked ? DefaultBoolean.True : DefaultBoolean.False;
            ASPxPivotGridExporter1.OptionsPrint.PrintDataHeaders = checkPrintDataHeaders.Checked ? DefaultBoolean.True : DefaultBoolean.False;

            string fileName = "PivotGrid";
            switch (cboListExportFormat.SelectedIndex)
            {
                case 0:
                    ASPxPivotGridExporter1.ExportPdfToResponse(fileName, saveAs);
                    break;
                case 1:
                    ASPxPivotGridExporter1.ExportXlsToResponse(fileName, saveAs);
                    break;
            }
        }

    }
}