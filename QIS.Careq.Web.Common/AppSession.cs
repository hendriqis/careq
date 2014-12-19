using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using QIS.Careq.Data.Service;

namespace QIS.Careq.Web.Common
{
    public class AppSession
    {
        public static UserLogin UserLogin
        {
            get
            {
                if (HttpContext.Current.Session["_UserName"] == null)
                {
                    //HttpContext.Current.Session["_UserName"] = new UserLogin();
                    //HttpContext.Current.Response.Redirect("~/Login.aspx");
                    return null;
                }

                return ((UserLogin)(HttpContext.Current.Session["_UserName"]));
            }
            set { HttpContext.Current.Session["_UserName"] = value; }
        }

        public static List<Variable> SettingParameter
        {
            get
            {
                if (HttpContext.Current.Session["SettingParameter"] == null)
                    HttpContext.Current.Session["SettingParameter"] = new List<Variable>();
                return ((List<Variable>)(HttpContext.Current.Session["SettingParameter"]));
            }
            set { HttpContext.Current.Session["SettingParameter"] = value; }
        }

        public static RegisteredPatient RegisteredPatient
        {
            get
            {
                if (HttpContext.Current.Session["_RegisteredPatient"] == null)
                {
                    //HttpContext.Current.Session["_UserName"] = new UserLogin();
                    //HttpContext.Current.Response.Redirect("~/Login.aspx");
                    return null;
                }

                return ((RegisteredPatient)(HttpContext.Current.Session["_RegisteredPatient"]));
            }
            set { HttpContext.Current.Session["_RegisteredPatient"] = value; }
        }
    }
}
