using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml.Linq;
using QIS.Medinfras.Data.Service;

namespace QIS.Careq.Web.Common.UI
{
    public abstract class BasePageContent : BasePage
    {
        public Repeater _RptTasks
        {
            get
            {
                Control ctlGeneral = Helper.FindControlRecursive(Master, "ctlGeneral");
                return (Repeater)Helper.FindControlRecursive(ctlGeneral, "rptTasks");
            }
        }
        public Repeater _RptInformation
        {
            get
            {
                Control ctlGeneral = Helper.FindControlRecursive(Master, "ctlGeneral");
                return (Repeater)Helper.FindControlRecursive(ctlGeneral, "rptInformation");
            }
        }
        public Repeater _RptPrint
        {
            get
            {
                Control ctlGeneral = Helper.FindControlRecursive(Master, "ctlGeneral");
                return (Repeater)Helper.FindControlRecursive(ctlGeneral, "rptPrint");
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (!Page.IsPostBack)
            {
                string menuCode = OnGetMenuCodeRightPanel();
                if (menuCode == "")
                    menuCode = OnGetMenuCode();
                if (menuCode != "")
                {
                    Repeater rptTasks = _RptTasks;
                    Repeater rptInformation = _RptInformation;
                    Repeater rptPrint = _RptPrint;

                    XDocument xdoc = Helper.LoadXMLFile(this, "right_panel.xml");
                    var lstQuickMenu = (from pg in xdoc.Descendants("page").Where(p => p.Attribute("menucode").Value == menuCode)
                                        select new
                                        {
                                            Tasks = (from qm in pg.Descendants("task")
                                                     select new
                                                     {
                                                         ID = qm.Attribute("id") == null ? "" : qm.Attribute("id").Value,
                                                         Code = qm.Attribute("code").Value,
                                                         Title = qm.Attribute("title").Value,
                                                         Description = qm.Attribute("description").Value,
                                                         Url = qm.Attribute("url").Value,
                                                         Width = qm.Attribute("width") == null ? "950" : qm.Attribute("width").Value,
                                                         Height = qm.Attribute("height") == null ? "600" : qm.Attribute("height").Value
                                                         //Url = Page.ResolveUrl(qm.Attribute("url").Value)
                                                     }),
                                            Information = (from qm in pg.Descendants("information")
                                                           select new 
                                                           {
                                                               Title = qm.Attribute("title").Value,
                                                               Description = qm.Attribute("description").Value,
                                                               Url = qm.Attribute("url").Value
                                                               //Url = Page.ResolveUrl(qm.Attribute("url").Value)
                                                           }),
                                            Print = (from qm in pg.Descendants("print")
                                                           select new
                                                           {
                                                               Code = qm.Attribute("code").Value,
                                                               Title = qm.Attribute("title").Value,
                                                               Url = qm.Attribute("url").Value,
                                                               Width = qm.Attribute("width") == null ? "950" : qm.Attribute("width").Value,
                                                               Height = qm.Attribute("height") == null ? "600" : qm.Attribute("height").Value
                                                           })

                                        }).FirstOrDefault();
                    if (lstQuickMenu != null)
                    {
                        if (lstQuickMenu.Tasks.Count() > 0)
                        {
                            rptTasks.DataSource = lstQuickMenu.Tasks;
                            rptTasks.DataBind();
                        }
                        if (lstQuickMenu.Information.Count() > 0)
                        {
                            rptInformation.DataSource = lstQuickMenu.Information;
                            rptInformation.DataBind();
                        }
                        if (lstQuickMenu.Print.Count() > 0)
                        {
                            rptPrint.DataSource = lstQuickMenu.Print;
                            rptPrint.DataBind();
                        }
                    }
                }
            }
        }
        public abstract string OnGetMenuCode();
        public virtual string OnGetMenuCodeRightPanel()
        {
            return "";
        }
        public virtual bool IsEntryUsePopup()
        {
            return true;
        }

        public virtual void SetCRUDMode(ref bool IsAllowAdd, ref bool IsAllowEdit, ref bool IsAllowDelete)
        {
            IsAllowAdd = IsAllowEdit = IsAllowDelete = true;
        }

        public virtual String OnGetReportCode()
        {
            return "";
        }
    }
}
