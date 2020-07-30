using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;

using System.IO;
using System.Net;

namespace TAMS.Libs
{
    public class JsonModelBinder : ActionFilterAttribute
    {
        public JsonModelBinder()
        {
        }

        public Type ActionParameterType { get; set; }
        public string ActionParameterName { get; set; }

        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    HttpRequestBase request = filterContext.HttpContext.Request;
        //    Stream stream = request.InputStream;
        //    stream.Position = 0;
        //    StreamReader reader = new StreamReader(stream);
        //    var json = reader.ReadToEnd();
        //    var obj = Activator.CreateInstance(ActionParameterType);
        //    if (!string.IsNullOrEmpty(json) && Common.IsJsonFormat(json))
        //    {
        //        obj = Newtonsoft.Json.JsonConvert.DeserializeObject(json, ActionParameterType);
        //        filterContext.ActionParameters[ActionParameterName] = obj;
        //    }
        //}
    }

    public static class Common
    {
        /// <summary>
        /// Auto mapping data from IDataReader to entity object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="row"></param>
        public static void AutoMappingColumn<T>(T item, IDataReader row)
        {
            Type type = typeof(T);
            object objInstanceClass = Activator.CreateInstance(type);
            Type objInstanceMethod = objInstanceClass.GetType();
            var lstProperty = objInstanceMethod.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in lstProperty)
            {
                for (int i = 0; i < row.FieldCount; i++)
                {
                    if (property.Name.ToLower().Equals(row.GetName(i).ToLower()))
                    {
                        var obj = row[property.Name];
                        if (obj != null)
                        {
                            if (property.PropertyType.GetGenericArguments() != null &&
                                property.PropertyType.GetGenericArguments().Length > 0 &&
                                property.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                            {
                                var genericType = property.PropertyType.GetGenericArguments()[0];
                                property.SetValue(item, Convert.ChangeType(obj, genericType), null);
                            }
                            else
                            {
                                if (property.PropertyType.IsEnum)
                                {
                                    property.SetValue(item, Enum.Parse(property.PropertyType, obj.ToString()), null);
                                }
                                else {
                                    property.SetValue(item, Convert.ChangeType(obj, property.PropertyType), null);
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Deserialize json string to Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToDeserialize<T>(this string json)
        {
            var ret = default(T);
            if (!string.IsNullOrEmpty(json))
            {
                try
                {
                    ret = JsonConvert.DeserializeObject<T>(json);
                }
                catch
                {
                    return ret;
                }
            }
            return ret;
        }

        /// <summary>
        /// Convert object dynamic to ExpandoObject
        /// </summary>
        /// <param name="anonymousObject"></param>
        /// <returns></returns>
        //public static ExpandoObject ToExpando(this object anonymousObject)
        //{
        //    IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
        //    IDictionary<string, object> expando = new ExpandoObject();
        //    foreach (var item in anonymousDictionary)
        //        expando.Add(item);
        //    return (ExpandoObject)expando;
        //}

        /// <summary>
        /// Extension convert datetime to int
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int ToInt32(this DateTime dt)
        {
            return int.Parse(string.Format("{0:yyyyMMdd}", dt));
        }

        /// <summary>
        /// Detect a string be a json format
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static bool IsJsonFormat(string json)
        {
            if (string.IsNullOrEmpty(json)) return false;

            var reg1 = new Regex("[[][{].*?[}][]]", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var reg2 = new Regex("[^[]*[{].*?[}][^]]*", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            if (reg1.IsMatch(json) || reg2.IsMatch(json)) return true;
            else return false;
        }

        /// <summary>
        /// Make a request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string MakeGetRequest(string url) {
            string data = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-ms-application, image/jpeg, application/xaml+xml";
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.1) Gecko/20060111 Firefox/1.5.0.1";
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }
                data = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
            }
            return data;
        }

        public const string VIETNAMESE =
            "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";

        public const string LATIN_BASIC =
            "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";

        /// <summary>
        /// Chuyển các chữ cái trong tiếng việt thành chữ latin cơ bản và phân cách giữa các từ bằng ký tự '-'
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveVietnamese(this string s)
        {
            s = s.Trim().ToLower();
            string retVal = String.Empty;
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = VIETNAMESE.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += LATIN_BASIC[pos];
                else
                    retVal += s[i];
            }
            retVal = retVal.Replace("-", "");
            retVal = retVal.Replace("  ", "");
            retVal = retVal.Replace(":", "");
            retVal = retVal.Replace(";", "");
            retVal = retVal.Replace("+", "");
            retVal = retVal.Replace("@", "");
            retVal = retVal.Replace(">", "");
            retVal = retVal.Replace("<", "");
            retVal = retVal.Replace("*", "");
            retVal = retVal.Replace("{", "");
            retVal = retVal.Replace("}", "");
            retVal = retVal.Replace("|", "");
            retVal = retVal.Replace("^", "");
            retVal = retVal.Replace("~", "");
            retVal = retVal.Replace("]", "");
            retVal = retVal.Replace("[", "");
            retVal = retVal.Replace("`", "");
            retVal = retVal.Replace(".", "");
            retVal = retVal.Replace("'", "");
            retVal = retVal.Replace("(", "");
            retVal = retVal.Replace(")", "");
            retVal = retVal.Replace(",", "");
            retVal = retVal.Replace("”", "");
            retVal = retVal.Replace("“", "");
            retVal = retVal.Replace("?", "");
            retVal = retVal.Replace("\"", "");
            retVal = retVal.Replace("&", "");
            retVal = retVal.Replace("$", "");
            retVal = retVal.Replace("#", "");
            retVal = retVal.Replace("_", "");
            retVal = retVal.Replace("=", "");
            retVal = retVal.Replace("%", "");
            retVal = retVal.Replace("…", "");
            retVal = retVal.Replace("/", "");
            retVal = retVal.Replace("\\", "");
            retVal = retVal.Replace(" ", "-");
            retVal = retVal.Replace("--", "-");
            retVal = retVal.TrimStart('-').TrimEnd('-');
            return retVal;
        }

        public static bool isContainSpecialCharacters(this string inputString)
        {
            var regexItem = new Regex(@"[~`!@#$%^&*()+=|\{}':;.,<>/?[\]\\""]");
            return regexItem.IsMatch(inputString);
        }
    }
}
