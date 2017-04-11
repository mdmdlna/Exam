using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using NtAbcExam.FrameWork.Logging;
using NtAbcExam.Web.DtoMapper;

namespace NtAbcExam.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            JsonSerializerSettings setting = new JsonSerializerSettings();
            JsonConvert.DefaultSettings = () =>
            {
                setting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                return setting;
            };

            LogHelper.SetConfig(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));

            AutoMapperProfile.InitAllProfile();
        }
    }
}
