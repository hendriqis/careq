using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Data.Service;
using DevExpress.Web.ASPxCallbackPanel;
using QIS.Careq.Web.Common;

namespace QIS.Careq.Web.SystemSetup.Program
{
    public partial class TemplateContentViewCtl : BaseViewPopupCtl
    {
        public override void InitializeDataControl(string param)
        {
            TemplateText entity = BusinessLayer.GetTemplateText(Convert.ToInt32(param));
            divTemplateContent.InnerHtml = entity.TemplateContent;            
        }
    }
}