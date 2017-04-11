using System;
using System.Web.Mvc;
using NtAbcExam.FrameWork.Logging;

namespace NtAbcExam.Web.Areas.Admin.Filters
{
    public class ExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is ArgumentOutOfRangeException)
            {
                int val = (int)(((ArgumentOutOfRangeException)filterContext.Exception).ActualValue);

                //filterContext.Result = new RedirectResult("~/Content/RangeErrorPage.html");
                filterContext.Result = new ViewResult
                {
                    ViewName = "RangeError",
                    ViewData = new ViewDataDictionary<int>(val)
                };

                LogHelper.Error(filterContext.Exception);
                filterContext.ExceptionHandled = true;
            }
        }
    }
}