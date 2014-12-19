using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIS.Careq.Data.Service.Helper
{
    public class Constant
    {
        public static class FormatString
        {
            public const string DATE_FORMAT = "dd-MMM-yyyy";
            public const string DATE_PICKER_FORMAT = "dd-MM-yyyy";
            public const string DATE_TIME_FORMAT = "dd-MMM-yyyy HH:mm:ss";
        }
        public static class ConstantDate
        {
            public const string DEFAULT_NULL = "01-01-1900";
        }
        public static class SpecialField
        {
            public const string MRN = "#MRN";
        }

        public static class StandardCode
        {
            public static class ToBePerformed
            {
                public const string CURRENT_EPISODE = "X125^001";
                public const string PRIOR_TO_NEXT_VISIT = "X125^002";
                public const string SCHEDULLED = "X125^003";
            }
            public static class DosingFrequency
            {
                public const string HOUR = "X130^001";
                public const string DAY = "X130^002";
                public const string WEEK = "X130^003";
            }
        }
    }
}
