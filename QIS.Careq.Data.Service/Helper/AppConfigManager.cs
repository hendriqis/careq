using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace QIS.Careq.Data.Service
{
    public static class AppConfigManager
    {
        static private string _QISFullNameFormat;
        static public string QISFullNameFormat { get { return _QISFullNameFormat; } }

        static AppConfigManager()
        {
            // Cache all these values in static properties.
            _QISFullNameFormat = WebConfigurationManager.AppSettings["QISFullNameFormat"];
        }
    }
}
