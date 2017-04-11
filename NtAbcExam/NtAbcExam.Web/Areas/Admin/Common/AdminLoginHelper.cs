using System;
using System.Web;
using NtAbcExam.Domain.DataModels;
using NtAbcExam.Domain.Repositories;
using NtAbcExam.FrameWork.Logging;
using NtAbcExam.Web.Common;

namespace NtAbcExam.Web.Areas.Admin.Common
{
    public class AdminLoginHelper
    {
        private static readonly AdminUserRepository AdminUserRep = new AdminUserRepository();

        public static AdminUser LoginInfo
        {
            get
            {
                if (HttpContext.Current.Session["SyUserInfo"] == null)
                {
                    return LoadCookie();
                }
                return HttpContext.Current.Session["SyUserInfo"] as AdminUser;
            }
        }

        private static AdminUser LoadCookie()
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["SyUserInfo"];
                if (cookie == null)
                {
                    return null;
                }
                cookie.HttpOnly = true;
                string loginName = AesHelper.Decrypt(cookie["LoginName"]);
                string password = AesHelper.Decrypt(cookie["Password"]);

                if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(password))
                {
                    return null;
                }

                if (Login(loginName, password))
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("登录出错：" + ex.Message, ex);
            }
            return HttpContext.Current.Session["SyUserInfo"] as AdminUser;
        }

        public static bool Login(string loginName, string pwd)
        {
            AdminUser adminUser = AdminUserRep.Login(loginName, pwd);

            if (adminUser == null)
            {
                return false;
            }

            HttpContext.Current.Session["SyUserInfo"] = adminUser;
            HttpCookie cookie = new HttpCookie("SyUserInfo");
            cookie.HttpOnly = true;
            cookie.Values.Add("LoginName", AesHelper.Encrypt(adminUser.UserName));
            cookie.Values.Add("Password", AesHelper.Encrypt(adminUser.UserPwd));
            HttpContext.Current.Response.Cookies.Add(cookie);
            return true;
        }

        public static bool IsLogin
        {
            get
            {
                //session有值
                if (HttpContext.Current.Session["SyUserInfo"] != null)
                {
                    return true;
                }

                //取cookie
                HttpCookie cookie = HttpContext.Current.Request.Cookies["SyUserInfo"];
                if (cookie == null)
                {
                    return false;
                }
                cookie.HttpOnly = true;
                string loginName = AesHelper.Decrypt(cookie["LoginName"]);
                string password = AesHelper.Decrypt(cookie["Password"]);
                if (string.IsNullOrEmpty(loginName) || string.IsNullOrEmpty(password))
                {
                    return false;
                }

                return Login(loginName, password);
            }
        }

        public static void Logout()
        {
            HttpContext.Current.Session.Remove("SyUserInfo");
            HttpCookie cookie = HttpContext.Current.Request.Cookies["SyUserInfo"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Today.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                HttpContext.Current.Request.Cookies.Remove("SyUserInfo");
            }
        }
    }
}