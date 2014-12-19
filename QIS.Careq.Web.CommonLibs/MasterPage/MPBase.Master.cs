using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using QIS.Careq.Web.Common;
using System.Web.UI.HtmlControls;
using QIS.Careq.Data.Service;

namespace QIS.Careq.Web.CommonLibs.MasterPage
{
    public partial class MPBase : System.Web.UI.MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!Page.IsPostBack)
            {
                XDocument xdoc = Helper.LoadXMLFile(this, "config.xml");
                var theme = (from pg in xdoc.Descendants("page")
                             select new
                             {
                                 Themes = pg.Attribute("themes").Value
                             }).FirstOrDefault();
                AddLink(string.Format("../Styles/{0}/medinfras.css", theme.Themes));
                AddLink(string.Format("../Styles/{0}/jquery/jquery.ui.theme.css", theme.Themes));

                Page.Title = GetApplicationName();
            }
        }

        protected string GetApplicationName()
        {
            if(AppSession.UserLogin != null)
                return AppSession.UserLogin.CompanyName;
            return BusinessLayer.GetApplicationList("")[0].CompanyName;
        }

        private void AddLink(string href)
        {
            HtmlHead head = (HtmlHead)Page.Header;
            HtmlLink link = new HtmlLink();
            link.Attributes.Add("href", href);
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);
        }
    }
}