using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web;
using QIS.Careq.Data.Service;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace QIS.Careq.Web.Common
{
    public class Helper
    {
        public static String GenerateCode(String formatCode, Int32 ID)
        {
            //string formatCode = "M**-**-**-**";
            int count = 0;
            for (int i = 0; i < formatCode.Length; ++i)
                if (formatCode[i] == '*')
                    count++;

            string tempCode = ID.ToString().PadLeft(count, '0');
            StringBuilder result = new StringBuilder();

            int ctrTempCode = 0;
            for (int i = 0; i < formatCode.Length; ++i)
            {
                char c = formatCode[i];
                if (c == '*')
                    result.Append(tempCode[ctrTempCode++]);
                else
                    result.Append(c);
            }
            return result.ToString();
        }

        public static String FormatMemberCode(Int32 MemberID)
        {
            //string formatCode = "M**-**-**-**";
            //int count = 0;
            //for (int i = 0; i < formatCode.Length; ++i)
            //    if (formatCode[i] == '*')
            //        count++;

            //Int32 ID = 132;
            //string tempCode = ID.ToString().PadLeft(count, '0');
            //StringBuilder result = new StringBuilder();

            //int ctrTempCode = 0;
            //for (int i = 0; i < formatCode.Length; ++i)
            //{
            //    char c = formatCode[i];
            //    if (c == '*')
            //        result.Append(tempCode[ctrTempCode++]);
            //    else
            //        result.Append(c);
            //}
            //string code = result.ToString();


            //String DefaultMRN = "00-00-00-00";
            //char SplitChar = '-';
            //String MedicalNo = MRN.ToString();
            //int ctr = MedicalNo.Length - 1;
            //for (int i = DefaultMRN.Length - 1; i >= 0; i--)
            //{
            //    if (DefaultMRN[i] == SplitChar)
            //        continue;
            //    else
            //    {
            //        if (ctr >= 0)
            //        {
            //            DefaultMRN = DefaultMRN.Remove(i, 1);
            //            DefaultMRN = DefaultMRN.Insert(i, MedicalNo[ctr].ToString());
            //            ctr--;
            //        }
            //        else
            //            break;
            //    }
            //}

            //return DefaultMRN;

            return MemberID.ToString().PadLeft(8, '0');
        }

        public static DateTime InitializeDateTimeNull()
        {
            return new DateTime(1900, 1, 1);
        }

        public static String MapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        public static XDocument LoadXMLFile(TemplateControl page, string xmlFileName)
        {
            string[] param = HttpContext.Current.Request.MapPath("~").Split('\\');
            var remStrings = param.Take(param.Length - 1);
            //string myXml = string.Format("{0}\\App_Data\\{1}", HttpContext.Current.Request.MapPath("~"), xmlFileName);
            //string myXml = string.Format("{0}\\QIS.Careq.Web.CommonLibs\\App_Data\\{1}", string.Join("\\", remStrings), xmlFileName);
            string myXml = page.ResolveUrl("~/Libs/App_Data/") + xmlFileName;
            string physicalPath = HttpContext.Current.Request.MapPath(myXml);
            XDocument xdoc = XDocument.Load(physicalPath);
            return xdoc;
        }

        public static string[] LoadTextFile(TemplateControl page, string textFileName)
        {
            string myText = page.ResolveUrl("~/Libs/App_Data/") + textFileName;
            return System.IO.File.ReadAllLines(HttpContext.Current.Request.MapPath(myText), Encoding.GetEncoding("windows-1250"));
        }

        #region Language
        public static string GetWordsLabel(List<Words> words, string code)
        {
            if (words == null)
                return code;
            Words word = words.FirstOrDefault(w => w.Code == code);
            return word == null ? code : word.Text;
        }

        public static List<Words> LoadWords(TemplateControl page)
        {
            XDocument xdoc = LoadXMLFile(page, "config.xml");
            var config = (from pg in xdoc.Descendants("page")
                          select new
                          {
                              Lang = pg.Attribute("lang").Value
                          }).FirstOrDefault();

            List<Words> words = new List<Words>();
            string[] tempWords = Helper.LoadTextFile(page, string.Format("lang/{0}.txt", config.Lang));
            foreach (string word in tempWords)
            {
                string[] param = word.Split(';');
                words.Add(new Words { Code = param[0], Text = param[1] });
            }
            return words;
        }
        #endregion

        public static Control FindControlRecursive(Control Root, string Id)
        {
            if (Root.ID == Id)
                return Root;

            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Ctl, Id);
                if (FoundCtl != null)
                    return FoundCtl;
            }

            return null;
        }

        public static void AddCssClass(WebControl ctrl, string classname)
        {
            ctrl.CssClass = String.Join(" ", ctrl.CssClass.Split(' ').Except(new string[] { "", classname }).Concat(new string[] { classname }).ToArray());
        }

        public static void AddCssClass(HtmlGenericControl ctrl, string classname)
        {
            string cssClass = ctrl.Attributes["class"];
            ctrl.Attributes.Add("class", String.Join(" ", cssClass.Split(' ').Except(new string[] { "", classname }).Concat(new string[] { classname }).ToArray()));
        }

        public static void SetDropDownListValue(DropDownList ddl, object value)
        {
            if (value != null)
            {
                if (ddl.Items.FindByValue(value.ToString()) != null)
                {
                    ddl.ClearSelection();
                    ddl.Items.FindByValue(value.ToString()).Selected = true;
                }
            }
        }

        public static String GetPasienAge(DateTime DoB)
        {
            int ageInYear = Function.GetPatientAgeInYear(DoB, DateTime.Now);
            int ageInMonth = Function.GetPatientAgeInMonth(DoB, DateTime.Now);
            int ageInDay = Function.GetPatientAgeInDay(DoB, DateTime.Now);

            return string.Format("{0} yr  {1} mo  {2} day", ageInYear, ageInMonth, ageInDay);
        }

        #region Date
        public static DateTime GetDatePickerValue(TextBox txt)
        {
            return GetDatePickerValue(txt.Text);
        }

        public static DateTime GetDatePickerValue(String text)
        {
            if (text != "")
            {
                var culture = System.Globalization.CultureInfo.CurrentCulture;
                return DateTime.ParseExact(text, "dd-MM-yyyy", culture);
            }
            return new DateTime(1900, 1, 1);
        }

        public static DateTime ConvertDateToString(string val, string format)
        {
            if (val != "")
            {
                var culture = System.Globalization.CultureInfo.CurrentCulture;
                return DateTime.ParseExact(val, format, culture);
            }
            return new DateTime(1900, 1, 1);
        }
        #endregion

        #region Module
        public static string GetModuleName()
        {
            string[] param = HttpContext.Current.Request.ApplicationPath.Split('/');
            return param.Last();
        }

        public static string GetModuleImage(TemplateControl page, string moduleName)
        {
            string img = "";
            moduleName = moduleName.ToLower();
            switch (moduleName)
            {
                case "outpatient": img = "outpatient"; break;
                case "systemsetup": img = "systemsetup"; break;
                case "inpatient": img = "inpatient"; break;
                case "emergencycare": img = "emergencycare"; break;
                case "medicalrecord": img = "medicalrecord"; break;
                case "emr": img = "medicalrecord"; break;
            }
            return page.ResolveUrl(string.Format("~/Libs/Images/Module/{0}small.png", img));
        }

        public static string GetModuleID(string moduleName)
        {
            string result = "";
            moduleName = moduleName.ToLower();
            switch (moduleName)
            {
                case "systemsetup": result = "SA"; break;
                case "outpatient": result = "OP"; break;
                case "medicalrecord": result = "RM"; break;
                case "emergencycare": result = "ER"; break;
                case "inpatient": result = "IP"; break;
                case "emr": result = "EMR"; break;
                case "medicaldiagnostic": result = "MD"; break;
                case "laboratory": result = "LB_"; break;
                case "medicalcheckup": result = "MCU_"; break;
                case "pharmacy": result = "FM_"; break;
                case "generallogistic": result = "LG_"; break;
                case "finance": result = "FN_"; break;
                case "humanresources": result = "PG_"; break;
                case "generalledger": result = "GL_"; break;
            }
            return result;
        }
        #endregion

        public static String GetHTMLEditorText(TextBox txt)
        {
            return HttpUtility.HtmlDecode(txt.Text);
        }
        public static String GetHTMLEditorText(Page page, TextBox txt)
        {
            return HttpUtility.HtmlDecode(page.Request.Form[txt.UniqueID]);
        }

        public static int GetPageCount(int RowCount, double pageSize = 16.0)
        {
            double pageCount = RowCount / pageSize;
            return (int)Math.Ceiling(pageCount);
        }

        public static DateTime DateInStringToDateTime(string value)
        {
            DateTime theTime = DateTime.ParseExact(value,
                                        "yyyyMMdd",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None);
            return theTime;
        }

        public static String GetOrderByMemberColumn()
        {
            string[] lstTemp = AppConfigManager.QISFullNameFormat.Split(' ');
            string orderBy = "";
            foreach (string temp in lstTemp)
            {
                if (orderBy != "")
                    orderBy += " + ";
                if (temp.ToUpper().Contains("NAME"))
                    orderBy += string.Format("ISNULL({0},'')", temp.Replace(",", ""));
            }
            return orderBy;
        }

        public static string NumberInWords(Int64 amount, Boolean isMoney = false)
        {
            StringBuilder strbuild;
            if (isMoney)
                strbuild = new StringBuilder("RUPIAH");
            else
                strbuild = new StringBuilder();

            String[] arrBil = { "", "SATU ", "DUA ", "TIGA ", "EMPAT ", "LIMA ", "ENAM ", "TUJUH ", "DELAPAN ", "SEMBILAN ", "SE" };
            String[] arrSatKecil = { "", "PULUH ", "RATUS " };
            String[] arrSatBesar = { "", "RIBU ", "JUTA ", "MILYAR " };
            int ctrKecil = 0;
            int ctrBesar = 0;
            if (amount == 0)
            {
                if (isMoney)
                    return "NOL RUPIAH";
                else
                    return "NOL";
            }
            else
            {
                while (amount > 0)
                {
                    long a = amount % 10;
                    amount /= 10;

                    if (a > 0)
                        strbuild.Insert(0, arrSatKecil[ctrKecil]);

                    if (a == 1 && ctrKecil > 0)
                        strbuild.Insert(0, arrBil[10]);
                    else if (ctrKecil == 0 && amount % 10 == 1 && a > 0)
                    {
                        strbuild.Insert(0, "BELAS ");
                        if (a == 1)
                            a = 10;
                        strbuild.Insert(0, arrBil[a]);
                        amount /= 10;
                        ctrKecil++;
                    }
                    else
                        strbuild.Insert(0, arrBil[a]);

                    ctrKecil++;
                    if (ctrKecil % 3 == 0)
                    {
                        ctrBesar++;
                        ctrKecil = 0;
                        if (amount > 0 && amount % 1000 > 0)
                        {
                            strbuild.Insert(0, arrSatBesar[ctrBesar]);
                        }
                    }

                }
                return strbuild.ToString();
            }
        }
    }
}
