using System;
using System.IO;

namespace NtAbcExam.FrameWork.Logging
{
    public class LogHelper
    {
        private static readonly log4net.ILog InfoLog = log4net.LogManager.GetLogger("InfoLog");
        private static readonly log4net.ILog ErrorLog = log4net.LogManager.GetLogger("ErrorLog");
        private static readonly log4net.ILog DebugLog = log4net.LogManager.GetLogger("DebugLog");

        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void SetConfig(string configFilePath)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(configFilePath));
        }

        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }


        public static void Info(string info)
        {
            if (InfoLog.IsInfoEnabled)
            {
                InfoLog.Info(info);
            }
        }

        public static void Debug(string debug)
        {
            if (DebugLog.IsDebugEnabled)
            {
                DebugLog.Info(debug);
            }
        }

        public static void Error(string error)
        {
            if (ErrorLog.IsErrorEnabled)
            {
                ErrorLog.Info(error);
            }
        }

        public static void Error(Exception ex)
        {
            if (ErrorLog.IsErrorEnabled)
            {
                ErrorLog.Info(ex);
            }
        }

        public static void Error(string error, Exception ex)
        {
            if (ErrorLog.IsErrorEnabled)
            {
                ErrorLog.Info(error, ex);
            }
        }
    }
}
