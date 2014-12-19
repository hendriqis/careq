using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using System.Web.Security;
using QIS.Careq.Web.Common;
using QIS.Careq.Data.Service;
using DevExpress.Web.ASPxCallbackPanel;

namespace QIS.Careq.Web.CommonLibs
{
    public partial class Default : BasePage
    {
        protected string moduleName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            moduleName = Helper.GetModuleName();
        }

        protected string GetModuleImage()
        {
            return Helper.GetModuleImage(this, moduleName);
        }
    }
}