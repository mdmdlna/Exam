using System.Web.Mvc;
using System.Web.Routing;
using NtAbcExam.Web.Areas.Admin.Common;

namespace NtAbcExam.Web.Areas.Admin.Filters
{
    public class AdminSeesionAttribute : ActionFilterAttribute
    {
        private FilterResultType _resultType;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //session没有过期,不做验证
            if (AdminLoginHelper.IsLogin)
            {
                //过滤可访问列表
                //if (roleRights == null || !roleRights.Contains(Convert.ToInt32(Code)))
                //{
                //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Error", Action = "Forbidden", ResultType = resultType, code = Code, Area = "" }));
                //    return;
                //}
                return;
            }


            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new
                {
                    Controller = "Home",
                    Action = "Login"
                })
            );
        }
        public AdminSeesionAttribute()
        {
            _resultType = FilterResultType.ResultJson;
        }
    }
}
