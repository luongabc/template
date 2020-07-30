using System;
using System.Configuration;
using System.IO;
using System.Web.Hosting;

namespace TAMS.Libs
{
    public class Logger
    {
        private readonly string _fileName = $"Log-{DateTime.Now:yyyyMMdd}.log";
        private static readonly object Sync = new object();
        public Logger() { }

        public Logger(string filename)
        {
            _fileName = filename;
        }

        public static Logger GetInstance()
        {
            return new Logger();
        }

        public void Write(Exception ex)
        {
            lock (Sync)
            {
                string curPath = GetCurPath();
                StreamWriter sw = new StreamWriter(curPath + _fileName, true);
                sw.WriteLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss.FFF}");
                sw.WriteLine(ex.Message);
                sw.WriteLine(ex.StackTrace);
                sw.Flush();
                sw.Close();
            }
        }

        public void Write(string msg)
        {
            lock (Sync)
            {
                string curPath = GetCurPath();
                StreamWriter sw = new StreamWriter(curPath + _fileName, true);
                sw.WriteLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss.FFF}");
                sw.WriteLine(msg);
                sw.Flush();
                sw.Close();
            }
        }

        private string GetCurPath()
        {
            string curPath = ConfigurationManager.AppSettings["Logfile"];
            if (!Directory.Exists(curPath)) Directory.CreateDirectory(curPath);
            if (!curPath.EndsWith("\\")) curPath += "\\";
            return curPath;
        }
    }

    public class LoggerInfo
    {
        #region [Variables]

        private int _logId;

        public int LogID
        {
            get => _logId;
            set => _logId = value;
        }
        private string _logApp;

        public string LogApp
        {
            get => _logApp;
            set => _logApp = value;
        }
        private string _logFunction;

        public string LogFunction
        {
            get => _logFunction;
            set => _logFunction = value;
        }
        private string _logType;

        public string LogType
        {
            get => _logType;
            set => _logType = value;
        }
        private string _userLogged;

        public string UserLogged
        {
            get => _userLogged;
            set => _userLogged = value;
        }
        private string _logMessage;

        public string LogMessage
        {
            get => _logMessage;
            set => _logMessage = value;
        }

        #endregion
    }

    public class LogController
    {

        public static void LogBatchFileProcess(string content, string strPath)
        {
            try
            {
                if (strPath == "")
                    strPath = "c:\\";
                if (strPath == "") return;
                string strFileName =
                    $"Process_{DateTime.Now.ToString(Const._DATE_FORMAT_ddMMyyyy)}{Const._DOT_TXT}";
                string strFileAbsolutePath = //HostingEnvironment.MapPath
                                (Path.Combine(strPath, strFileName));
                if (File.Exists(strFileAbsolutePath))
                {
                    using (var fs = new FileStream(strFileAbsolutePath ?? string.Empty, FileMode.Append, FileAccess.Write))
                    {
                        using (var sw = new StreamWriter(fs))
                        {
                            var strLog = $"{DateTime.Now.ToString(Const._DATETIME_FORMAT)} | {content}";
                            sw.WriteLine(strLog);
                            sw.Flush();
                        }
                    }
                }
                else
                {
                    using (var fs = new FileStream(strFileAbsolutePath ?? string.Empty, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (var sw = new StreamWriter(fs))
                        {
                            var strLog = $"{DateTime.Now.ToString(Const._DATETIME_FORMAT)} | {content}";

                            sw.WriteLine(strLog);
                            sw.Flush();
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void WriteFileLog(string logApp, string logFunction, string logType, string userLogged, string logMessage, string strFilePath)
        {
            try
            {
                if (strFilePath == "")
                {
                    strFilePath = "C:\\";
                }

                if (strFilePath == "") return;
                string strFileName = $"Error_{DateTime.Now.ToString(Const._DATE_FORMAT_ddMMyyyy)}{Const._DOT_TXT}";
                string strFileAbsolutePath = (Path.Combine(strFilePath, strFileName));

                if (File.Exists(strFileAbsolutePath))
                {
                    using (var fs = new FileStream(strFileAbsolutePath ?? string.Empty, FileMode.Append, FileAccess.Write))
                    {
                        using (var sw = new StreamWriter(fs))
                        {
                            var strLog =
                                $"{DateTime.Now.ToString(Const._DATETIME_FORMAT)} | {logApp} | {logFunction} | {logType} | {userLogged} | {logMessage}";

                            sw.WriteLine(strLog);
                            sw.Flush();
                        }
                    }
                }
                else
                {
                    using (var fs = new FileStream(Path.Combine(strFilePath, strFileName) ?? string.Empty, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (var sw = new StreamWriter(fs))
                        {
                            var strLog =
                                $"{DateTime.Now.ToString(Const._DATETIME_FORMAT)} | {logApp} | {logFunction} | {logType} | {userLogged} | {logMessage}";

                            sw.WriteLine(strLog);
                            sw.Flush();
                        }
                    }
                }
            }
            catch
            {
                //throw ex;
            }
        }

        public static void WriteLog(string logApp, string logFunction, string logType, string userLogged, string logMessage, string filePath)
        {
            WriteFileLog(logApp, logFunction, logType, userLogged, logMessage, filePath);
        }
    }

    public static class LogType
    {
        #region [Constants]

        public const string Information = "Information";
        public const string Warning = "Warning";
        public const string Exception = "Problem";

        #endregion
    }

    public static class LogApp
    {
        #region [Constants]

        public const string BatchfileApp = "BackupService";

        #endregion
    }
}
