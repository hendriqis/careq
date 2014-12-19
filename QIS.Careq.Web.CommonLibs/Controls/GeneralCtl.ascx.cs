using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using QIS.Careq.Web.Common;
using System.Reflection;
using System.Collections;
using QIS.Careq.Data.Service;
using DevExpress.Web.ASPxCallbackPanel;
using QIS.Careq.Web.Common.UI;

namespace QIS.Careq.Web.CommonLibs.Controls
{
    public partial class GeneralCtl : System.Web.UI.UserControl
    {
        protected string TodayDate;
        protected string HealthcareID;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LoadRightPanelContent();
        }

        protected string GetLabel(string code)
        {
            BasePage page = (BasePage)this.Page;
            return page.GetLabel(code);
        }

        protected override void OnLoad(EventArgs e)
        {
            TodayDate = DateTime.Now.ToString("yyyyMMdd");            
        }

        private void BindGridView(string param, string searchDialogType, string baseFilterExpression, ref string intellisenseHints)
        {
            try
            {
                #region Load XML
                if (param == "open")
                {
                    XDocument xdoc = Helper.LoadXMLFile(this, "search_dialog.xml");
                    var tempSearchDialog = (from sd in xdoc.Descendants("searchdialog").Where(p => p.Attribute("type").Value == searchDialogType)
                                            select new
                                            {
                                                SearchDialogBase = sd.Attribute("searchdialogbase") != null ? sd.Attribute("searchdialogbase").Value : "",
                                                FilterExpression = sd.Attribute("filterexpression") != null ? sd.Attribute("filterexpression").Value : ""
                                            }).FirstOrDefault();
                    if (tempSearchDialog == null)
                        throw new Exception(string.Format("Search Dialog with type {0} is not defined", searchDialogType));
                    if (tempSearchDialog.SearchDialogBase == "")
                    {
                        SearchDialogState = (from sd in xdoc.Descendants("searchdialog").Where(p => p.Attribute("type").Value == searchDialogType)
                                             select new CSearchDialogState
                                             {
                                                 MethodName = sd.Attribute("methodname").Value,
                                                 KeyFieldName = sd.Attribute("keyfieldname").Value,
                                                 FilterExpression = "",
                                                 BaseFilterExpression = sd.Attribute("filterexpression") != null ? sd.Attribute("filterexpression").Value : "",
                                                 OrderByColumnIndex = sd.Attribute("orderbycolumnindex") != null ? Convert.ToInt32(sd.Attribute("orderbycolumnindex").Value) : 0,
                                                 OrderByType = sd.Attribute("orderbytype") != null ? sd.Attribute("orderbytype").Value : "ASC",
                                                 GridColumns = (from grd in sd.Descendants("gridcolumn")
                                                                select new GridColumn
                                                                {
                                                                    DataField = grd.Attribute("datafield").Value,
                                                                    HeaderText = grd.Attribute("headertext").Value,
                                                                    Width = grd.Attribute("width").Value,
                                                                    DisplayCustomField = grd.Attribute("displaycustomfield") != null ? grd.Attribute("displaycustomfield").Value : null,
                                                                    HorizontalAlign = grd.Attribute("horizontalalign") != null ? grd.Attribute("horizontalalign").Value : "left"
                                                                }).ToList<GridColumn>(),
                                                 IntellisenseTexts = (from itx in sd.Descendants("intellisensetext")
                                                                      select new QuickSearchIntellisense
                                                                      {
                                                                          DataField = itx.Attribute("datafield").Value,
                                                                          HeaderText = itx.Attribute("headertext").Value,
                                                                          Description = itx.Attribute("description").Value
                                                                      }).ToList<QuickSearchIntellisense>()

                                             }).FirstOrDefault();
                    }
                    else
                    {
                        SearchDialogState = (from sd in xdoc.Descendants("searchdialog").Where(p => p.Attribute("type").Value == tempSearchDialog.SearchDialogBase)
                                             select new CSearchDialogState
                                             {
                                                 MethodName = sd.Attribute("methodname").Value,
                                                 KeyFieldName = sd.Attribute("keyfieldname").Value,
                                                 FilterExpression = "",
                                                 BaseFilterExpression = tempSearchDialog.FilterExpression,
                                                 OrderByColumnIndex = sd.Attribute("orderbycolumnindex") != null ? Convert.ToInt32(sd.Attribute("orderbycolumnindex").Value) : 0,
                                                 OrderByType = sd.Attribute("orderbytype") != null ? sd.Attribute("orderbytype").Value : "ASC",
                                                 GridColumns = (from grd in sd.Descendants("gridcolumn")
                                                                select new GridColumn
                                                                {
                                                                    DataField = grd.Attribute("datafield").Value,
                                                                    HeaderText = grd.Attribute("headertext").Value,
                                                                    Width = grd.Attribute("width").Value,
                                                                    DisplayCustomField = grd.Attribute("displaycustomfield") != null ? grd.Attribute("displaycustomfield").Value : null,
                                                                    HorizontalAlign = grd.Attribute("horizontalalign") != null ? grd.Attribute("horizontalalign").Value : "left"
                                                                }).ToList<GridColumn>(),
                                                 IntellisenseTexts = (from itx in sd.Descendants("intellisensetext")
                                                                      select new QuickSearchIntellisense
                                                                      {
                                                                          DataField = itx.Attribute("datafield").Value,
                                                                          HeaderText = itx.Attribute("headertext").Value,
                                                                          Description = itx.Attribute("description").Value
                                                                      }).ToList<QuickSearchIntellisense>()

                                             }).FirstOrDefault();
                    }

                    if (baseFilterExpression != "")
                    {
                        if (SearchDialogState.BaseFilterExpression == "")
                            SearchDialogState.BaseFilterExpression = baseFilterExpression;
                        else
                            SearchDialogState.BaseFilterExpression += string.Format(" AND {0}", baseFilterExpression);
                    }
                }
                #endregion

                #region Bind Grid View
                string filterExpression = SearchDialogState.FilterExpression;
                if (SearchDialogState.BaseFilterExpression != "")
                {
                    if (filterExpression != "")
                        filterExpression += " AND ";
                    filterExpression += SearchDialogState.BaseFilterExpression;
                }
                string orderByExpression = string.Format("{0} {1}", SearchDialogState.GridColumns[SearchDialogState.OrderByColumnIndex].DataField, SearchDialogState.OrderByType);
                MethodInfo method = typeof(BusinessLayer).GetMethod(SearchDialogState.MethodName, new[] { typeof(string), typeof(int), typeof(int), typeof(string) });
                IList list = null;
                if (method != null)
                {
                    object obj = method.Invoke(null, new object[] { filterExpression, 200, 1, orderByExpression });
                    list = (IList)obj;
                }
                else
                {
                    method = typeof(BusinessLayer).GetMethod(SearchDialogState.MethodName, new[] { typeof(string) });
                    object obj = method.Invoke(null, new string[] { filterExpression });
                    list = (IList)obj;
                }

                List<Words> words = Helper.LoadWords(this);

                BoundField keyField = new BoundField();
                keyField.DataField = SearchDialogState.KeyFieldName;
                keyField.ItemStyle.CssClass = "keyField";
                keyField.HeaderStyle.CssClass = "keyField";
                grdSearch.Columns.Add(keyField);
                foreach (GridColumn col in SearchDialogState.GridColumns)
                {
                    BoundField field = new BoundField();
                    if (col.DisplayCustomField != null)
                        field.DataField = col.DisplayCustomField;
                    else
                        field.DataField = col.DataField;
                    field.HeaderText = Helper.GetWordsLabel(words, col.HeaderText);
                    field.HeaderStyle.Width = new Unit(col.Width);
                    switch (col.HorizontalAlign)
                    {
                        case "center": field.ItemStyle.HorizontalAlign = HorizontalAlign.Center; break;
                        case "right": field.ItemStyle.HorizontalAlign = HorizontalAlign.Right; break;
                        default: field.ItemStyle.HorizontalAlign = HorizontalAlign.Left; break;
                    }

                    grdSearch.Columns.Add(field);
                }

                grdSearch.DataSource = list;
                grdSearch.DataBind();
                #endregion

                #region Quick Search
                foreach (QuickSearchIntellisense col in SearchDialogState.IntellisenseTexts)
                {
                    if (intellisenseHints != "")
                        intellisenseHints += ",";
                    intellisenseHints += string.Format("{{ \"text\":\"{0}\",\"fieldName\":\"{1}\",\"description\":\"{2}\" }}", Helper.GetWordsLabel(words, col.HeaderText), col.DataField, col.Description);
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        protected void cbpSearchDialog_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] param = e.Parameter.Split('|');
            string searchDialogType = "";
            string baseFilterExpression = "";
            if (param[0] == "open")
            {
                searchDialogType = param[1];
                baseFilterExpression = param[2];
            }
            else if (param[0] == "refresh")
                SearchDialogState.FilterExpression = param[1];
            else if (param[0] == "sort")
            {
                SearchDialogState.OrderByColumnIndex = Convert.ToInt32(param[1]);
                SearchDialogState.OrderByType = param[2];
            }

            string intellisenseHints = "";
            BindGridView(param[0], searchDialogType, baseFilterExpression, ref intellisenseHints);

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpIntellisenseHints"] = intellisenseHints;
            panel.JSProperties["cpSortedIndex"] = SearchDialogState.OrderByColumnIndex;
            panel.JSProperties["cpSortedType"] = SearchDialogState.OrderByType;
        }

        #region Search Dialog State
        private class CSearchDialogState
        {
            public string MethodName { get; set; }
            public string KeyFieldName { get; set; }
            public string BaseFilterExpression { get; set; }
            public int OrderByColumnIndex { get; set; }
            public string OrderByType { get; set; }
            public string FilterExpression { get; set; }
            public List<GridColumn> GridColumns { get; set; }
            public List<QuickSearchIntellisense> IntellisenseTexts { get; set; }
        }

        private const string SESSION_SEARCH_DIALOG_STATE = "SearchDialogState";
        private static CSearchDialogState SearchDialogState
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_SEARCH_DIALOG_STATE] == null) HttpContext.Current.Session[SESSION_SEARCH_DIALOG_STATE] = new CSearchDialogState();
                return (CSearchDialogState)HttpContext.Current.Session[SESSION_SEARCH_DIALOG_STATE];
            }
            set
            {
                HttpContext.Current.Session[SESSION_SEARCH_DIALOG_STATE] = value;
            }
        }

        private class GridColumn
        {
            private String _HorizontalAlign;
            public String HorizontalAlign
            {
                get { return _HorizontalAlign; }
                set { _HorizontalAlign = value; }
            }

            private String _DisplayCustomField;
            public String DisplayCustomField
            {
                get { return _DisplayCustomField; }
                set { _DisplayCustomField = value; }
            }

            private String _DataField;
            public String DataField
            {
                get { return _DataField; }
                set { _DataField = value; }
            }

            private String _HeaderText;
            public String HeaderText
            {
                get { return _HeaderText; }
                set { _HeaderText = value; }
            }

            private String _Width;
            public String Width
            {
                get { return _Width; }
                set { _Width = value; }
            }
        }

        private class QuickSearchIntellisense
        {
            private String _DataField;
            public String DataField
            {
                get { return _DataField; }
                set { _DataField = value; }
            }

            private String _HeaderText;
            public String HeaderText
            {
                get { return _HeaderText; }
                set { _HeaderText = value; }
            }

            private String _Description;
            public String Description
            {
                get { return _Description; }
                set { _Description = value; }
            }
        }
        #endregion

        private void LoadRightPanelContent()
        {
            try
            {
                string IsLoadContent = Request.Form["hdnRightPanelContentIsLoadContent"] ?? "0";
                if (IsLoadContent == "1")
                {
                    string url = Request.Form["hdnRightPanelContentUrl"] ?? "";
                    Control ctlParent = pnlRightPanelContentArea;
                    BaseContentPopupCtl ctl = (BaseContentPopupCtl)LoadControl(url);
                    ctlParent.Controls.Clear();
                    ctlParent.Controls.Add(ctl);

                    string firstTimeLoad = Request.Form["hdnRightPanelContentFirstTimeLoad"] ?? "0";
                    ctl.LoadMasterControl();
                    if (firstTimeLoad == "1")
                    {
                        string param = Request.Form["hdnRightPanelContentParam"] ?? "";
                        ctl.InitializeControl(param);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void cbpRightPanelContent_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
        }
    }
}