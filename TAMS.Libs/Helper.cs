using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Web.Configuration;
using System.Data;
using Aspose.Cells;
using System.Web.Hosting;
using System.ComponentModel;
using System.Web;
using ConvertToPdf;
using EvoWordToPdf;
using System.Drawing;
using DevExpress.XtraRichEdit;
using DevExpress.XtraPrinting;
using QRCoder;

namespace TAMS.Libs
{
    public static class Helper
    {
        private static readonly string[] JobStatusName = new[] { "", "Drafted", "Published", "Private", "Đóng" };
        private static readonly string[] UnitPriceNames = new[] { "", "Thỏa Thuận", "Triệu", "Tỷ", "Trăm nghìn/m2", "Triệu/m2", "Trăm nghìn/Tháng", "Triệu/Tháng", "Trăm nghìn/m2/tháng", "Triệu/m2/tháng", "Nghìn/m2/tháng" };
        private static readonly string[] CateNames = new[] { "", "Dự án", "Tin rao", "Tin tức", "Kho xưởng" };
        public static string log_dir = TAMS.Libs.Helper.GetAppSettingByKey("log_dir");
        public static string ToJobStatusName(int status)
        {
            return JobStatusName[status.ToInt(0)];
        }

        public static string ToUnitPriceName(int type)
        {
            return UnitPriceNames[type.ToInt(null)];
        }

        public static string ToCateName(int type)
        {
            return CateNames[type.ToInt(null)];
        }

        public static string GetCurrentURL(string url, int? pageIndex)
        {
            if (url.IndexOf("/p" + pageIndex) != -1)
            {
                if (url.Substring(url.Length - (pageIndex.ToString().Length + 2)) == "/p" + pageIndex) return url.Substring(0, url.Length - (pageIndex.ToString().Length + 2));
            }
            return url;
        }

        //public static string GenPagging(PagingEntity pagingInfo)
        //{
        //    int pageNum = (int)Math.Ceiling((double)pagingInfo.Count / pagingInfo.PageSize);
        //    if (pageNum * pagingInfo.PageSize < pagingInfo.Count)
        //    {
        //        pageNum++;
        //    }
        //    pagingInfo.LinkSite = pagingInfo.LinkSite.TrimEnd('/') + "/";
        //    string buildlink = "<li class='inline ml5'><a href='{0}{1}' class='pt5 pb5 pl10 border-gray pr10 none-underline box-shadow radius {2}' title='{4}'>{3}</a></li>";

        //    const string strTrang = "p";
        //    string currentPage = pagingInfo.PageIndex.ToString(); // trang tiện tại
        //    string htmlPage = "";
        //    int iCurrentPage = 0;
        //    if (pagingInfo.PageIndex <= 0) iCurrentPage = 1;
        //    else iCurrentPage = pagingInfo.PageIndex;
        //    string active;
        //    if (pageNum <= 4)
        //    {
        //        if (pageNum != 1)
        //        {
        //            for (int i = 1; i <= pageNum; i++)
        //            {
        //                active = currentPage == i.ToString() ? "active" : "";
        //                if (i == 1) htmlPage += String.Format(buildlink, pagingInfo.LinkSite.TrimEnd('/'), string.Empty, active, i, "Trang " + i);
        //                else htmlPage += String.Format(buildlink, pagingInfo.LinkSite, strTrang + i, active, i, "Trang " + i);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (iCurrentPage > 1)
        //        {
        //            htmlPage += String.Format(buildlink, pagingInfo.LinkSite.TrimEnd('/'), string.Empty, "no-number", "Trang đầu", "Trang đầu");

        //        }
        //        else
        //        {
        //            for (int i = 1; i <= 3; i++)
        //            {
        //                active = currentPage == i.ToString() ? "active" : "";
        //                if (i == 1) htmlPage += String.Format(buildlink, pagingInfo.LinkSite.TrimEnd('/'), string.Empty, active, i, "Trang " + i);
        //                else htmlPage += String.Format(buildlink, pagingInfo.LinkSite, strTrang + i, active, i, "Trang " + i);
        //            }
        //            htmlPage += String.Format(buildlink, pagingInfo.LinkSite, strTrang + "4", "no-number", "Trang tiếp", "Trang tiếp");
        //        }
        //        if (iCurrentPage > 1 && iCurrentPage < pageNum)
        //        {
        //            if (iCurrentPage > 2)
        //            {
        //                if (iCurrentPage - 2 == 1)
        //                    htmlPage += String.Format(buildlink, pagingInfo.LinkSite.TrimEnd('/'), string.Empty, "no-number", "Trang trước", "Trang trước");
        //                else htmlPage += String.Format(buildlink, pagingInfo.LinkSite, strTrang + (iCurrentPage - 2), "no-number", "Trang trước", "Trang trước");

        //            }
        //            for (int i = (iCurrentPage - 1); i <= (iCurrentPage + 1); i++)
        //            {
        //                active = currentPage == i.ToString() ? "active" : "";
        //                if (i == 1) htmlPage += String.Format(buildlink, pagingInfo.LinkSite.TrimEnd('/'), string.Empty, active, i, "Trang " + i);
        //                else htmlPage += String.Format(buildlink, pagingInfo.LinkSite, strTrang + i, active, i, "Trang " + i);
        //            }
        //            if (iCurrentPage <= pageNum - 2)
        //            {
        //                htmlPage += String.Format(buildlink, pagingInfo.LinkSite, strTrang + (iCurrentPage + 2), "no-number", "Trang tiếp", "Trang tiếp");
        //            }
        //        }
        //        int intCurrentPage = 0;
        //        int.TryParse(currentPage, out intCurrentPage);
        //        if (intCurrentPage == 0) intCurrentPage = 1;
        //        if (intCurrentPage < pageNum)
        //        {
        //            htmlPage += String.Format(buildlink, pagingInfo.LinkSite, strTrang + pageNum, "no-number", "Trang cuối", "Trang cuối");
        //        }
        //        else
        //        {
        //            if (pageNum - 4 == 1)
        //                htmlPage += String.Format(buildlink, pagingInfo.LinkSite.TrimEnd('/'), string.Empty, "no-number", "Trang trước", "Trang trước");
        //            else
        //                htmlPage += String.Format(buildlink, pagingInfo.LinkSite, strTrang + (pageNum - 4), "no-number", "Trang trước", "Trang trước");
        //            int j = 3;
        //            for (int i = pageNum; i >= pageNum - 3; i--)
        //            {
        //                active = currentPage == (pageNum - j).ToString() ? "active" : "";
        //                if (pageNum - j == 1)
        //                    htmlPage += String.Format(buildlink, pagingInfo.LinkSite, strTrang + (pageNum - j), active, pageNum - j, "Trang " + (pageNum - j));
        //                else
        //                    htmlPage += String.Format(buildlink, pagingInfo.LinkSite, strTrang + (pageNum - j), active, pageNum - j, "Trang " + (pageNum - j));
        //                j--;
        //            }
        //        }
        //    }
        //    htmlPage = string.Format("<div class='page'><ul class='paging tr'>{0}</ul></div>", htmlPage);
        //    return htmlPage;
        //}

        public static string MySubStringNotHtml(string input, int length)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            String regex = "<a(.*?)</a>";
            Match math = Regex.Match(input, regex);
            String result = string.Empty;
            if (math.Success)
            {
                result = math.Groups[0].Value;
                input = Regex.Replace(input, "<a(.*?)</a>", "_temphtml_");
            }
            System.Text.StringBuilder __returnStr = new System.Text.StringBuilder();
            string[] __separator = new string[] { " " };
            string[] __arrStr = input.Split(__separator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < __arrStr.Length; i++)
            {
                if (i < length && i < __arrStr.Length)
                {
                    __returnStr.Append(__arrStr[i] + " ");

                }
                else
                {
                    break;
                }
            }
            return __returnStr.Replace("_temphtml_", result).ToString();
        }

        public static string MySubStringWithDot(string input, int length)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            if (input.Length <= length)
            {
                return MySubStringNotHtml(input, length);
            }

            return string.Concat(MySubStringNotHtml(input, length).Trim(), "...");
        }

        public static string MySubStringNoDot(string input, int length)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }
            string[] arr = input.Split(' ');
            if (arr.Length <= length)
            {
                return MySubStringNotHtml(input, length);
            }

            return string.Concat(MySubStringNotHtml(input, length).Trim(), "...");
        }

        public static string RemoveHTMLTag(string htmlString)
        {
            if (string.IsNullOrEmpty(htmlString)) return string.Empty;
            string pattern = @"(<[^>]+>)";
            string text = System.Text.RegularExpressions.Regex.Replace(htmlString, pattern, string.Empty);
            return text;
        }

        public static bool SendMail(string to, string subject, string body)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            try
            {
                MailAddress fromAddress = new MailAddress("bdschungcu.com.vn.noreply@gmail.com");
                message.From = fromAddress;
                message.To.Add(to);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("bdschungcu.com.vn.noreply@gmail.com", "123123a@");
                smtpClient.Send(message);

                return true;
            }
            catch
            {
                return false;
            }
        }

        //public static string SendMail(string listTo, string subject, string body)
        //{
        //    int port = 25;
        //    string smptServer = "email-smtp.us-east-1.amazonaws.com";
        //    string fromEmail = "bdschungcu.com.vn.noreply@gmail.com";
        //    string credentialName = "AKIAJ3HM4HDVTH5PLQNA";
        //    string credentialPassword = "Ak7MdIdHZ50JHsQWP6JRG2k8FyqmEuZX6Io/1omqhsOe";
        //    string[] lstMailTo;
        //    if (!string.IsNullOrEmpty(listTo))
        //    {
        //        listTo = listTo.TrimEnd(';');
        //        lstMailTo = listTo.Split(';');
        //    }
        //    else return "noEmailTo";

        //    MailMessage message = new MailMessage();
        //    SmtpClient smtpClient = new SmtpClient();
        //    string msg = string.Empty;
        //    try
        //    {
        //        MailAddress fromAddress = new MailAddress(fromEmail);
        //        message.From = fromAddress;
        //        for (int i = 0; i < lstMailTo.Length; i++)
        //        {
        //            message.To.Add(lstMailTo[i]);
        //        }
        //        message.Subject = subject;
        //        message.IsBodyHtml = true;
        //        message.Body = body;
        //        smtpClient.Host = smptServer;
        //        smtpClient.Port = port;
        //        smtpClient.EnableSsl = true;
        //        smtpClient.UseDefaultCredentials = true;
        //        smtpClient.Credentials = new System.Net.NetworkCredential(credentialName, credentialPassword);
        //        smtpClient.Send(message);
        //        msg = "true";
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //    }
        //    return msg;
        //}

        public static bool IsSqlInjection(string keyword)
        {
            // KiênCT tạm bỏ phần SQLInjection
            return false;

            bool isNotInject = false;

            if (string.IsNullOrEmpty(keyword)) { return isNotInject; }
            keyword = keyword.ToLower().UnicodeToKoDau();
            try
            {
                isNotInject = !CharacterOk(keyword);
            }
            catch (Exception)
            {
                isNotInject = true;
            }
            return isNotInject;
        }


        public const string koDauChars =
            "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU0123456789@.";

        public static bool CharacterOk(string s)
        {
            bool retVal = true;
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                if (!string.IsNullOrEmpty(s[i].ToString()))
                {
                    string ch = s[i].ToString();
                    if (!string.IsNullOrEmpty(ch) && !string.IsNullOrWhiteSpace(ch))
                    {
                        pos = koDauChars.IndexOf(s[i].ToString());
                        bool isValid = Regex.IsMatch(ch, @"^[a-zA-Z0-9@.]+$");
                        if (!isValid)
                        {
                            retVal = false;
                            break;
                        }
                    }
                }
            }
            return retVal;
        }


        public static string VNPTHRMAPI = WebConfigurationManager.AppSettings["VNPTHRMAPI"];

        public static string API_CHITIETHSNS = string.Concat(VNPTHRMAPI, Const.VNPTHRMAPI_CHITIETHSNS);
        public static string API_HSNS = string.Concat(VNPTHRMAPI, Const.VNPTHRMAPI_HSNS);
        public static string API_PHONGBAN = string.Concat(VNPTHRMAPI, Const.VNPTHRMAPI_PHONGBAN);
        public static string API_SYNCPROFILE = string.Concat(VNPTHRMAPI, Const.VNPTHRMAPI_SYNCPROFILE);
        public static string VNPTHRM_GetEmployeeDetail(string email)
        {
            string urlAddress = API_CHITIETHSNS;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate1, chain, sslPolicyErrors) => { return true; };
            var request = (HttpWebRequest)WebRequest.Create(urlAddress);
            string stringData = "{\"email\" : \"" + email + "\"}";

            var data = Encoding.ASCII.GetBytes(stringData); // or UTF8

            request.Method = "POST";
            request.ContentType = "text/html; charset=utf-8";
            request.ContentLength = data.Length;

            var newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);

            newStream.Close();
            HttpWebResponse res = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8);
            string backstr = sr.ReadToEnd();
            sr.Close();
            res.Close();

            return backstr;
        }
        /// <summary>
        /// Đồng bộ hồ sơ ứng viên mới tuyển dụng sang HRM
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static string VNPTHRM_SyncProfile(string jsonData)
        {
            string urlAddress = API_SYNCPROFILE;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate1, chain, sslPolicyErrors) => { return true; };
            var request = (HttpWebRequest)WebRequest.Create(urlAddress);
            string stringData = jsonData;

            var data = Encoding.ASCII.GetBytes(stringData); // or UTF8

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            var newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);

            newStream.Close();
            HttpWebResponse res = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8);
            string backstr = sr.ReadToEnd();
            sr.Close();
            res.Close();

            return backstr;
        }

        public static string RandomString(int length)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var builder = new StringBuilder();

            Random random = new Random();
            for (var i = 0; i < length; i++)
            {
                var c = pool[random.Next(0, pool.Length)];
                builder.Append(c);
            }

            return builder.ToString();
        }

        public static void BuildExcelByTemplate(string urlTemplate, DataTable dataHeader, DataTable dataContent)
        {
            #region
            //WorkbookDesigner designer = default(WorkbookDesigner);
            //try
            //{
            //    Aspose.Cells.License l = new Aspose.Cells.License();
            //    string strLicense = @"~/Content/Template/ReportLicenses/Aspose.Cells.lic";
            //    l.SetLicense(HostingEnvironment.MapPath(strLicense));

            //    string targetLocation = HostingEnvironment.MapPath(urlTemplate);
            //    designer = new WorkbookDesigner();
            //    designer.Open(targetLocation);
            //    designer.SetDataSource(dataHeader);

            //    if (dataContent != null)
            //    {
            //        int intCols = dataContent.Columns.Count;
            //        for (int i = 0; i <= intCols - 1; i++)
            //        {
            //            designer.SetDataSource(dataContent.Columns[i].ColumnName.ToString(), dataContent.Rows[0].ItemArray[i].ToString());
            //        }
            //    }
            //    designer.Process();
            //    designer.Workbook.CalculateFormula();
            //    string ext = urlTemplate.Substring(urlTemplate.LastIndexOf(".") + 1).ToLower();
            //    XlsSaveOptions save = new XlsSaveOptions(SaveFormat.Xlsx);
            //    switch (ext)
            //    {
            //        case "xls":
            //            save = new XlsSaveOptions(SaveFormat.Excel97To2003);
            //            break;
            //        case "xlsx":
            //            save = new XlsSaveOptions(SaveFormat.Xlsx);
            //            break;
            //    }
            //    designer.Workbook.Save("E:\\VNPTSOFTWARE\\a.xlsx");
            //    var b = designer.Workbook.SaveToStream();
            //    return b.ToArray();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            #endregion

            WorkbookDesigner designer = default(WorkbookDesigner);
            try
            {
                Aspose.Cells.License l = new Aspose.Cells.License();
                string strLicense = @"/Content/Template/ReportLicenses/Aspose.Cells.lic";
                l.SetLicense(HostingEnvironment.MapPath(strLicense));


                designer = new WorkbookDesigner();
                designer.Open(HostingEnvironment.MapPath(urlTemplate));

                designer.SetDataSource(dataHeader);

                if (dataContent != null)
                {
                    int intCols = dataContent.Columns.Count;
                    for (int i = 0; i <= intCols - 1; i++)
                    {
                        designer.SetDataSource(dataContent.Columns[i].ColumnName.ToString(), dataContent.Rows[0].ItemArray[i].ToString());
                    }
                }
                designer.Process();
                designer.Workbook.CalculateFormula();
                designer.Workbook.Save("E:\\VNPTSOFTWARE\\aaxx.xlsx");
                //var b = designer.Workbook.SaveToStream();
                //return b.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name.ToUpper(), Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        public static string ConvertToImage(string b_file)
        {
            try
            {
                string temp_file = b_file;
                string partShare = WebConfigurationManager.AppSettings["ServerLinkFile"];
                string LinkServerLocal = WebConfigurationManager.AppSettings["ServerLocal"];
                if (string.IsNullOrEmpty(LinkServerLocal))
                {
                    b_file = string.Concat("////", partShare, "//", b_file.TrimStart('/'));
                }
                else
                {
                    b_file = string.Concat(LinkServerLocal, "//", b_file.TrimStart('/'));
                }
                //b_file = HttpContext.Current.Server.MapPath("~") + b_file;

                util a = new util();

                if (!File.Exists(b_file))
                {
                    return string.Empty;
                }
                string b_out = b_file.Replace(".pdf", ".png");
                if (File.Exists(b_out))
                {
                    //b_out = b_out.Replace(HttpContext.Current.Server.MapPath("~"), string.Empty);
                    if (string.IsNullOrEmpty(LinkServerLocal))
                    {
                        b_out = b_out.Replace("////" + partShare, "");
                    }
                    else
                    {
                        b_out = b_out.Replace(LinkServerLocal, "");
                    }
                    return b_out;
                }
                string temp_temp = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                string temp_file_png = HttpContext.Current.Server.MapPath("~/Files/Temp/") + temp_temp;

                a.ConvertPdfToPng(b_file, temp_file_png, System.Drawing.Printing.PaperKind.A3);
                b_out = "/Files/Temp/" + temp_temp;
                //a.ConvertPdfToPng(b_file, b_out, System.Drawing.Printing.PaperKind.A3);
                //if (string.IsNullOrEmpty(LinkServerLocal))
                //{
                //    b_out = b_out.Replace("////" + partShare, "");
                //}
                //else
                //{
                //    b_out = b_out.Replace(LinkServerLocal, "");
                //}



                //b_out = b_out.Replace(HttpContext.Current.Server.MapPath("~"), string.Empty);
                return b_out;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string ConvertPdf2Png(string b_file, string b_out)
        {
            return b_out;
        }

        public static string ConvertToPDF(string b_file)
        {
            try
            {
                string partShare = WebConfigurationManager.AppSettings["ServerLinkFile"];
                string LinkServerLocal = WebConfigurationManager.AppSettings["ServerLocal"];
                string fileUpload = string.Empty;
                if (string.IsNullOrEmpty(LinkServerLocal))
                {
                    fileUpload = string.Concat("////", partShare, "//", b_file.TrimStart('/'));
                }
                else
                {
                    fileUpload = string.Concat(LinkServerLocal, "//", b_file.TrimStart('/'));
                }
                if (b_file.ToLower().IndexOf(".pdf") > 0)
                {
                    return b_file;
                }
                if (b_file.ToLower().IndexOf(".doc") > 0)
                {
                    return ConvertDocToPDF(fileUpload);
                }
                if (b_file.ToLower().IndexOf(".xls") > 0)
                {
                    return ConvertExelToPDF(fileUpload);
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

            return string.Empty;
        }

        public static string ConvertExelToPDF(string b_file)
        {
            //b_file = HttpContext.Current.Server.MapPath("~") + b_file;
            string partShare = WebConfigurationManager.AppSettings["ServerLinkFile"];
            string LinkServerLocal = WebConfigurationManager.AppSettings["ServerLocal"];
            util a = new util();

            if (!File.Exists(b_file))
            {
                return string.Empty;
            }
            string ex = ".xls";
            if (b_file.IndexOf(".xlsx") > 0)
            {
                ex = ".xlsx";
            }
            string b_out = b_file.Replace(ex, ".pdf");

            if (File.Exists(b_out))
            {
                //b_out = b_out.Replace(HttpContext.Current.Server.MapPath("~"), string.Empty);
                if (!string.IsNullOrEmpty(LinkServerLocal))
                {
                    b_out = b_out.Replace(LinkServerLocal, string.Empty);
                }
                else
                {
                    b_out = b_out.Replace("////" + partShare, string.Empty);
                }
                return b_out;
            }

            a.ConvertXLSToPdf(b_file, b_out, System.Drawing.Printing.PaperKind.A3);
            if (!string.IsNullOrEmpty(LinkServerLocal))
            {
                b_out = b_out.Replace(LinkServerLocal, string.Empty);
            }
            else
            {
                b_out = b_out.Replace("////" + partShare, string.Empty);
            }
            //b_out = b_out.Replace(HttpContext.Current.Server.MapPath("~"), string.Empty);
            return b_out;
        }

        public static string ConvertDocToPDF(string b_file)
        {
            try
            {
                string partShare = WebConfigurationManager.AppSettings["ServerLinkFile"];
                string LinkServerLocal = WebConfigurationManager.AppSettings["ServerLocal"];
                //b_file = HttpContext.Current.Server.MapPath("~") + b_file;
                if (!File.Exists(b_file))
                {
                    return string.Empty;
                }
                string ex = System.IO.Path.GetExtension(b_file);

                DateTime lastModified = System.IO.File.GetLastWriteTime(b_file);

                var lastModifiedString = lastModified != null ? lastModified.ToString("yyyyMMddHHmmss") : "0";

                string b_out = b_file.Replace("/Files/", "/Files/tmp/").Replace(ex, string.Format("_{0}.pdf", lastModifiedString));

                var temp = Path.GetDirectoryName(b_out);
                if (!Directory.Exists(temp))
                {
                    Directory.CreateDirectory(temp);
                }

                if (File.Exists(b_out))
                {
                    if (!string.IsNullOrEmpty(LinkServerLocal))
                    {
                        b_out = b_out.Replace(LinkServerLocal, string.Empty);
                    }
                    else
                    {
                        b_out = b_out.Replace("////" + partShare, string.Empty);
                    }

                    return b_out;
                }

                // Create a Word to PDF converter object with default settings
                WordToPdfConverter wordToPdfConverter = new WordToPdfConverter();

                // Set license key received after purchase to use the converter in licensed mode
                // Leave it not set to use the converter in demo mode
                wordToPdfConverter.LicenseKey = "gA4eDxofD2YCeE5dSlVwHR8eGA8YARsPHB4BHh0BFhYWFg8e";

                // Convert the Word document to a PDF document
                byte[] outPdfBuffer = wordToPdfConverter.ConvertWordFile(b_file);

                // Write the memory buffer in a PDF file
                System.IO.File.WriteAllBytes(b_out, outPdfBuffer);

                if (!string.IsNullOrEmpty(LinkServerLocal))
                {
                    b_out = b_out.Replace(LinkServerLocal, string.Empty);
                }
                else
                {
                    b_out = b_out.Replace("////" + partShare, string.Empty);
                }
                return b_out;
            }
            catch (Exception ex)
            {
                LogController.WriteLog(LogApp.BatchfileApp, "Helper.ConvertDocToPDF", LogType.Exception, "", ex.ToString(), log_dir);
                return string.Empty;
            }

        }
        public static void ConvertDocToDocx(string path)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();

            if (path.ToLower().EndsWith(".doc"))
            {
                var sourceFile = new FileInfo(path);
                var document = word.Documents.Open(sourceFile.FullName);

                string newFileName = sourceFile.FullName.Replace(".doc", ".docx");
                document.SaveAs2(newFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXMLDocument,
                                 CompatibilityMode: Microsoft.Office.Interop.Word.WdCompatibilityMode.wdWord2010);

                word.ActiveDocument.Close();
                word.Quit();

                //File.Delete(path);
            }
        }
        /// <summary>
        ///  Hàm build link trỏ đến File dựa vào Webconfig ServerLinkFile và ServerLocal
        ///  Phân biệt 97, 98
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string BuildLinkFileWithConfig(string path)
        {
            string partShare = WebConfigurationManager.AppSettings["ServerLinkFile"];
            string LinkServerLocal = WebConfigurationManager.AppSettings["ServerLocal"];
            if (string.IsNullOrEmpty(LinkServerLocal))
            {
                path = string.Concat("////", partShare, "//", path.TrimStart('/'));
            }
            else
            {
                path = string.Concat(LinkServerLocal, "//", path.TrimStart('/'));
            }
            return path;
        }
        public static string GetAppSettingByKey(string key)
        {
            string value = WebConfigurationManager.AppSettings[key];
            return value;
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static object GetColumnValue(DataRow row, string column)
        {
            return row.Table.Columns.Contains(column) ? row[column] : null;
        }

        private static readonly Random _random = new Random();
        /// <summary>
        /// random string with size
        /// </summary>
        /// <param name="size"></param>
        /// <param name="lowerCase"></param>
        /// <returns></returns>
        public static string RandomCharacter(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        #region tạo mã QR code
        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        public static string GetQRCode(string qrText)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText,
            QRCodeGenerator.ECCLevel.L);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return Convert.ToBase64String(BitmapToBytes(qrCodeImage));
        }
        #endregion
    }
}
