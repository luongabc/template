using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace TAMS.Libs
{
    public static class Ultility
    {
        /// <summary>
        /// Convert object to <c>int</c> value if exception occur return default value(if defaultValue == null return 0)
        /// </summary>
        /// <param name="obj">obj value to convert</param>
        /// <param name="defaultValue">default return value</param>
        /// <returns></returns>
        public static int ToInt(this object obj, int? defaultValue)
        {
            int ret = defaultValue ?? 0;
            try
            {
                ret = Convert.ToInt32(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }


        /// <summary>
        /// Convert object to <c>long</c> value if exception occur return default value(if defaultValue == null return 0)
        /// </summary>
        /// <param name="obj">obj value to convert</param>
        /// <param name="defaultValue">default return value</param>
        /// <returns></returns>
        public static long ToLong(this object obj, long? defaultValue)
        {
            long ret = defaultValue ?? 0;
            try
            {
                ret = Convert.ToInt64(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }


        /// <summary>
        /// Convert object to <c>double</c> value if exception occur return default value(if defaultValue == null return 0)
        /// </summary>
        /// <param name="obj">obj value to convert</param>
        /// <param name="defaultValue">default return value</param>
        /// <returns></returns>
        public static double ToDouble(this object obj, double? defaultValue)
        {
            Double ret = defaultValue ?? 0;
            try
            {
                ret = Convert.ToDouble(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }

        /// <summary>
        /// Convert object to <c>float</c> value if exception occur return default value(if defaultValue == null return 0)
        /// </summary>
        /// <param name="obj">obj value to convert</param>
        /// <param name="defaultValue">default return value</param>
        /// <returns></returns>
        public static float ToFloat(this object obj, float? defaultValue)
        {
            float ret = defaultValue ?? 0;
            try
            {
                ret = Convert.ToSingle(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }

        /// <summary>
        /// Convert object to <c>float</c> value if exception occur return default value(if defaultValue == null return 0)
        /// </summary>
        /// <param name="obj">obj value to convert</param>
        /// <param name="defaultValue">default return value</param>
        /// <returns></returns>
        public static bool ToBoolean(this object obj, bool? defaultValue)
        {
            bool ret = defaultValue ?? false;
            try
            {
                ret = Convert.ToBoolean(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }

        public static DateTime ToDateTime(this object obj, DateTime? defaultValue)
        {
            DateTime ret = defaultValue ?? DateTime.MinValue;
            try
            {
                ret = Convert.ToDateTime(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }

        /// <summary>
        /// Convert from VietNam DateTime string to standard UTC DateTime value
        /// </summary>
        /// <param name="vnDateTime">input vndatetime <c>string</c></param>
        /// <param name="splitChar">split <c>char</c> used in input string</param>
        /// <param name="defaultValue">the value u want to return if cannot convert</param>
        /// <param name="hour">Hour and munite if it have <c>int</c> </param>
        /// <returns>Utc <c>DateTime</c> value</returns>
        public static DateTime? FromVnDateTimeToUtc(this string vnDateTime, char splitChar, DateTime? defaultValue, params int[] hour)
        {
            if (!string.IsNullOrEmpty(vnDateTime))
            {
                var arrDateTime = vnDateTime.Split(splitChar);
                DateTime utcDateTime;
                try
                {
                    var year = int.Parse(arrDateTime[2].Trim());
                    var month = 0;
                    arrDateTime[1] = arrDateTime[1].Trim();
                    if (arrDateTime[1].Length > 1 && arrDateTime[1].Substring(0, 1) == "0")
                    {
                        month = int.Parse(arrDateTime[1].Substring(1, 1));
                    }
                    else
                    {
                        month = int.Parse(arrDateTime[1]);
                    }
                    var day = 0;
                    arrDateTime[0] = arrDateTime[0].Trim();
                    if (arrDateTime[0].Length > 1 && arrDateTime[0].Substring(0, 1) == "0")
                    {
                        day = int.Parse(arrDateTime[0].Substring(1, 1));
                    }
                    else
                    {
                        day = int.Parse(arrDateTime[0]);
                    }
                    utcDateTime = hour.Length == 2 ?
                                        new DateTime(year, month, day, hour[0], hour[1], 0)
                                        : new DateTime(year, month, day);
                }
                catch
                {
                    return defaultValue;
                }
                return utcDateTime;
            }
            return null;
        }

        public static string ToElapsed(this DateTime orgDate, DateTime compareDate)
        {
            string ret = string.Empty;

            if (compareDate > orgDate)
            {
                TimeSpan disTime = compareDate - orgDate;
                ret = string.Format("Cách đây{0}{1}{2}",
                                    (disTime.Days > 0 ? " " + disTime.Days + " ngày" : string.Empty),
                                   " " + disTime.Hours + " giờ", " " + disTime.Minutes + " phút");
            }
            else
            {
                TimeSpan disTime = orgDate - compareDate;
            }
            return ret;
        }

        #region stringConverter
        public static string Format = "&#{0};";
        private const string HTML_PREFIX = "http://";
        private static char[] PHRASE_SEPARATORS = new char[] {
            '.', ',', ';', ':', '?', '!', '"', '\'', '\t', '\r', '\n', '|', '(', ')', '{', '}', '-', '+'
        };
        public const char SEPARATOR = '-';
        private const char SPACE = ' ';
        private const string SPACE_CHARS = " \t\r\n";
        private const string HtmlTagPattern = "<.*?>";
        private static char[] SPECIAL_CHARS = new char[] { '\x00ba' };
        private static char[][] VnVowelTbl = new char[][] { new char[] {
                'a', 'ã', '\x00e2', 'e', '\x00ea', 'i', 'o', '\x00f4', 'õ', 'u', 'ý', 'y', '\x00e0', '?', '?', '\x00e8',
                '?', '\x00ec', '\x00f2', '?', '?', '\x00f9', '?', '?', '\x00e1', '?', '?', '\x00e9', '?', '\x00ed', '\x00f3', '?',
                '?', '\x00fa', '?', '\x00fd', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?',
                '\x00e3', '?', '?', '?', '?', '?', '\x00f5', '?', '?', '?', '?', '?', '?', '?', '?', '?',
                '?', '?', '?', '?', '?', '?', '?', '?', 'ð', '\x00f0'
            }, new char[] {
                'a', 'a', 'a', 'e', 'e', 'i', 'o', 'o', 'o', 'u', 'u', 'y', 'a', 'a', 'a', 'e',
                'e', 'i', 'o', 'o', 'o', 'u', 'u', 'y', 'a', 'a', 'a', 'e', 'e', 'i', 'o', 'o',
                'o', 'u', 'u', 'y', 'a', 'a', 'a', 'e', 'e', 'i', 'o', 'o', 'o', 'u', 'u', 'y',
                'a', 'a', 'a', 'e', 'e', 'i', 'o', 'o', 'o', 'u', 'u', 'y', 'a', 'a', 'a', 'e',
                'e', 'i', 'o', 'o', 'o', 'u', 'u', 'y', 'd', 'd'
            }
        };
        private static char[] WORD_SEPARATORS = new char[] {
            ' ', '.', ',', ';', ':', '?', '!', '"', '\'', '\t', '\r', '\n', '|', '(', ')', '{', '}', '-', '+', '>', '<', '='
        };
        public const string UniChars = "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
        public const string KoDauChars = "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";

        public static string ToSEOString(this string txt)
        {
            StringBuilder builder = new StringBuilder();
            string str = txt.ToFTS();
            foreach (char ch in str)
            {
                if (ch.IsLetterOrDigit())
                {
                    builder.Append(ch);
                }
                else
                {
                    builder.Append('-');
                }
            }
            string retval = Regex.Replace(builder.ToString(), @"[^\x20-\x7e]", String.Empty);
            retval = retval.Replace("---", "-").Replace("--", "-").TrimEnd('-');
            return HttpUtility.UrlEncode(retval);
        }

        public static bool IsLetterOrDigit(this char c)
        {
            //return ((Enumerable.All(Path.GetInvalidPathChars(), (Func<char, bool>)(i => (i != c))) && Enumerable.All(SPECIAL_CHARS, (Func<char, bool>)(i => (i != c)))) && Char.IsLetterOrDigit(c));

            //return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9');
            return Char.IsLetterOrDigit(c);

        }

        public static string ToFTS(this string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return "";
            }
            string str = ToSingleSpaceString(txt.ToLower());
            var builder = new StringBuilder();
            foreach (char ch in str)
            {
                if ((((ch != '"') && (ch != ' ')) && (ch != '/')) && !IsLetterOrDigit(ch))
                {
                    continue;
                }
                int index = 0;
                while (index < VnVowelTbl[0].Length)
                {
                    if (ch == VnVowelTbl[0][index])
                    {
                        break;
                    }
                    index++;
                }
                builder.Append((index == VnVowelTbl[0].Length) ? ch : VnVowelTbl[1][index]);
            }
            return builder.ToString();
        }

        public static string ToSingleSpaceString(this string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return "";
            }
            string str = txt;
            var builder = new StringBuilder();
            int num = 0;
            while (num < str.Length)
            {
                char ch = str[num++];
                switch (ch)
                {
                    case '\t':
                    case '\r':
                    case '\n':
                        ch = ' ';
                        break;
                }
                if ((ch != ' ') || ((builder.Length > 0) && (builder[builder.Length - 1] != ' ')))
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }

        public static string RemoveAllSpace(this string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return "";
            }
            Regex regex = new Regex(@"\s+", RegexOptions.None);
            txt = regex.Replace(txt, @"");
            return txt;
        }
        public static string RemoveSpecialString(this string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return "";
            }
            txt = Regex.Replace(txt, @"&#?[a-z0-9]{2,8};", string.Empty);
            return txt;
        }
        //public static bool IsLetterOrDigit(char c)
        //{
        //    return ((Enumerable.All<char>(Path.GetInvalidPathChars(), (Func<char, bool>)(i => (i != c))) && Enumerable.All<char>(SPECIAL_CHARS, (Func<char, bool>)(i => (i != c)))) && char.IsLetterOrDigit(c));
        //}

        public static string ConvertFromSignedtoUnsigned(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string temp = input.Normalize(NormalizationForm.FormD);
                return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            }
            return input;
        }

        public static string RemoveHtmlTag(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return Regex.Replace(input, HtmlTagPattern, string.Empty);
        }
        public static List<string> DetectPhoneNumber(string input)
        {
            var lstPhone = new List<string>();
            if (string.IsNullOrEmpty(input)) return lstPhone;
            input = Regex.Replace(input.Trim(), "(\\+84)([0-9]+)", delegate (Match m)
            {
                return "0" + m.Groups[2].Value;
            }, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            input = Regex.Replace(input, "[.,]", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var reg = new Regex("([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection matches = reg.Matches(input);
            foreach (Match m in matches)
            {
                var phone = m.Groups[0].Value;
                if (!string.IsNullOrEmpty(phone) && phone.Length > 6)
                {
                    var one = phone.Substring(0, 1);
                    var two = phone.Substring(1, 1);
                    if ("0".Equals(one))
                    {
                        if ("9".Equals(two) || "1".Equals(two))
                        {
                            if (phone.Length > 9 && !lstPhone.Contains(phone))
                            {
                                lstPhone.Add(phone);
                            }
                        }
                        else
                        {
                            if (phone.Length > 8 && !lstPhone.Contains(phone))
                            {
                                lstPhone.Add(phone);
                            }
                        }
                    }
                    else
                    {
                        if (!lstPhone.Contains(phone))
                        {
                            lstPhone.Add(phone);
                        }
                    }
                }
            }
            return lstPhone;
        }
        #endregion

        public static T JsonToObject<T>(string jsonString)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch
            {
                return default(T);
            }
        }

        public static string ObjectToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static string ReadFileContent(string filepath)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(filepath);
                return sr.ReadToEnd();
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }

        public static string RemoveHtml(this string htmlContent)
        {
            var value = Regex.Replace(htmlContent, "<[^>]*>", string.Empty).Replace("&nbsp;", "").Trim();
            if (string.IsNullOrEmpty(value))
            {
                value = "";
            }
            return value;
        }

        public static string RandomString(int size)
        {
            var random = new Random();
            const string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            var builder = new StringBuilder();
            for (var i = 0; i < size; i++)
            {
                var ch = input[random.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public const string uniChars =
           "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";

        public const string koDauChars =
            "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";


        public static string UnicodeToKoDau(this string s)
        {
            //if (string.IsNullOrEmpty(s))
            //    return s;
            //var sb = new StringBuilder();
            //foreach (var t in s)
            //{
            //    var pos = UniChars.IndexOf(t);
            //    sb.Append(pos >= 0 ? KoDauChars[pos] : t);
            //}
            //return sb.ToString();
            string retVal = String.Empty;
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += koDauChars[pos];
                else
                    retVal += s[i];
            }
            return retVal;
        }

        public static string UnicodeToKoDauAndGach(this string s)
        {
            //if (string.IsNullOrEmpty(s))
            //    return s;
            //s = UnicodeToKoDau(s.Trim());
            //s = Regex.Replace(s, "[^a-zA-Z0-9]+", "-");
            //s = Regex.Replace(s, "[-]+", "-");
            //return s.Trim('-');
            if (string.IsNullOrEmpty(s)) return string.Empty;
            const string strChar = "abcdefghijklmnopqrstxyzuvxw0123456789 -";
            //string retVal = UnicodeToKoDau(s);
            s = UnicodeToKoDau(s.ToLower().Trim());
            string sReturn = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (strChar.IndexOf(s[i]) > -1)
                {
                    if (s[i] != ' ')
                        sReturn += s[i];
                    else if (i > 0 && s[i - 1] != ' ' && s[i - 1] != '-')
                        sReturn += "-";
                }
            }
            while (sReturn.IndexOf("--") != -1)
            {
                sReturn = sReturn.Replace("--", "-");
            }
            return sReturn;
        }

        /// <summary>
        /// loại bỏ các ký tự không phải chữ, số, dấu cách thành ký tự không dấu
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharToKhongDau(string s)
        {
            string retVal = UnicodeToKoDau(s);
            Regex regex = new Regex(@"[^\d\w]+");
            retVal = regex.Replace(retVal, " ");
            while (retVal.IndexOf("  ") != -1)
            {
                retVal = retVal.Replace("  ", " ");
            }
            return retVal;
        }

        public static string RemoveSpecial(string s)
        {
            //const string REGEX = @"([^\w\dàáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ\.,\-_ ]+)";
            //s = Regex.Replace(s, REGEX, string.Empty, RegexOptions.IgnoreCase);

            return Regex.Replace(s, "[`~!@#$%^&*()_|+-=?;:'\"<>{}[]\\/]", string.Empty); //edited by vinhph

        }

        public static string PreProcessSearchString(string searchString)
        {
            string output = searchString.Replace("'", " ").Replace("\"\"", " ").Replace(">", " ").Replace("<", " ").Replace(",", " ").Replace("(", " ").Replace(")", " ").Replace("\"", " ");
            output = System.Text.RegularExpressions.Regex.Replace(output, "[ ]+", "+");
            return output.Trim();
        }

        public static string RenderPartialToString(string controlName, object viewData)
        {
            ViewPage viewPage = new ViewPage() { ViewContext = new ViewContext() };

            viewPage.ViewData = new ViewDataDictionary(viewData);
            viewPage.Controls.Add(viewPage.LoadControl(controlName));

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (HtmlTextWriter tw = new HtmlTextWriter(sw))
                {
                    viewPage.RenderControl(tw);
                }
            }

            return sb.ToString();
        }

        public static string RenderRazorViewToString(this Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        /// <summary>
        /// Kiểm tra string có phải email không
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static List<int> ToArrayList<TEnum>(this TEnum enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            List<int> listReturn = new List<int>();

            foreach (var item in Enum.GetValues(typeof(TEnum)))
            {
                listReturn.Add(item.ToInt(0));
            }

            return listReturn;
        }

        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }
    }


    public static class StringUtility
    {
        public static string ReadFileContent(this string filepath)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(filepath);
                return sr.ReadToEnd();
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }

        //public static string RemoveHtmlTag(this string htmlContent)
        //{
        //    return Regex.Replace(htmlContent, "<[^>]*>", string.Empty).Replace("&nbsp;", "").Trim();
        //}

        public static string RandomString(int size)
        {
            var random = new Random();
            const string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            var builder = new StringBuilder();
            for (var i = 0; i < size; i++)
            {
                var ch = input[random.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
