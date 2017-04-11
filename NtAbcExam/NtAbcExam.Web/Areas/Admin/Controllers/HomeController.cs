using System.Web.Mvc;
using NtAbcExam.Web.Areas.Admin.Common;
using NtAbcExam.Web.Areas.Admin.Filters;
using NtAbcExam.Web.Areas.Admin.Models;

namespace NtAbcExam.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [AdminSeesion]
        // GET: Home
        public ActionResult Index()
        {
            return View(AdminLoginHelper.LoginInfo);
        }

        [AdminSeesion]
        public ActionResult Welcome()
        {
            return View();
        }


        public ActionResult Login()
        {

            return View();
        }

        public ActionResult Logout()
        {
            AdminLoginHelper.Logout();
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.LoginName))
            {
                ModelState.AddModelError("", "用户名不能为空");
            }

            if (string.IsNullOrWhiteSpace(viewModel.LoginPwd))
            {
                ModelState.AddModelError("", "用户名不能为空");
            }


            if (AdminLoginHelper.Login(viewModel.LoginName, viewModel.LoginPwd))
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "用户名或密码错误");
            return View();
        }

    }
}