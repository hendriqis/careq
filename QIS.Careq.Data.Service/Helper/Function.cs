using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;

namespace QIS.Careq.Data.Service
{
    public class Function
    {
        /// <summary>
        /// parameter : string time ( yyyyMMdd )
        /// </summary>
        /// <param name="timeIn_yyyyMMdd"></param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string time)
        {
            DateTime theTime = DateTime.ParseExact(time,
                                        "yyyyMMdd",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None);
            return theTime;
        }

        #region Calculate Patient Age Based on DateOfBirth
        public static int GetPatientAgeInDay(DateTime dateOfBirth, DateTime nowDate)
        {
            int day = GetPatientAge(dateOfBirth, nowDate, 1);
            return day;
        }
        public static int GetPatientAgeInMonth(DateTime dateOfBirth, DateTime nowDate)
        {
            int month = GetPatientAge(dateOfBirth, nowDate, 2);
            return month;
        }
        public static int GetPatientAgeInYear(DateTime dateOfBirth, DateTime nowDate)
        {
            int year = GetPatientAge(dateOfBirth, nowDate, 3);
            return year;
        }
        public static int GetPatientAge(DateTime dateOfBirth, DateTime nowDate, int type)
        {
            int day = nowDate.Day - dateOfBirth.Day;
            int month = nowDate.Month - dateOfBirth.Month;
            int year = nowDate.Year - dateOfBirth.Year;
            int typo = 0;

            if (day < 0)
            {
                day = day + System.DateTime.FromOADate(nowDate.Day - System.DateTime.Now.Day).Day;
                month = month - 1;
            }
            if (month < 0)
            {
                month = month + 12;
                year = year - 1;
            }
            switch (type)
            {
                case 1: typo = day; break;
                case 2: typo = month; break;
                case 3: typo = year; break;
            }
            return typo;
        }

        public static string UrlRoot()
        {
            string url = "";
            if (HttpRuntime.AppDomainAppVirtualPath.Equals("/"))
            {
                string absolutePath = HttpContext.Current.Request.Url.AbsolutePath;
                int count = absolutePath.Split('/').Length - 2;
                for (int i = 0; i < count; i++)
                {
                    url += "../";
                }
                url = url.Substring(0, url.Length - 1);
            }
            else
                url = HttpRuntime.AppDomainAppVirtualPath;

            return url; 
        }
        #endregion

        
    }
}
