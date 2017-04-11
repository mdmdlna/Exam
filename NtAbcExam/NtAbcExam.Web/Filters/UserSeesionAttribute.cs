using System.Web.Mvc;
using System.Web.Routing;
using NtAbcExam.Web.Common;


namespace NtAbcExam.Web.Filters
{
    public class UserSeesionAttribute : ActionFilterAttribute
    {
        private FilterResultType _resultType;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserLoginHelper.IsLogin)
            {
                //过滤可访问列表
                return;
            }


            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new
                {
                    Controller = "Home",
                    Action = "Login",
                    ResultType = FilterResultType.ResultView,
                    Area = ""
                })
            );
        }
        public UserSeesionAttribute()
        {
            _resultType = FilterResultType.ResultJson;
        }
    }
}
