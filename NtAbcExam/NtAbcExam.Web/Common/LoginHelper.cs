using System;
using System.Web;
using NtAbcExam.Domain.Models;
using NtAbcExam.Domain.Repositories;
using NtAbcExam.FrameWork.Logging;

namespace NtAbcExam.Web.Common
{
    public class UserLoginHelper
    {
        private static readonly CadreInfoRepository UserRep = new CadreInfoRepository();

        public static cadre_info LoginInfo
        {
            get
            {
                if (HttpContext.Current.Session["UserInfo"] == null)
                {
                    return LoadCookie();
                }
                return HttpContext.Current.Session["UserInfo"] as cadre_info;
            }
        }

        private static cadre_info LoadCookie()
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];
                if (cookie == null)
                {
                    return null;
                }
                cookie.HttpOnly = true;
                string userId = AesHelper.Decrypt(cookie["LoginName"]);
                string password = AesHelper.Decrypt(cookie["Password"]);

                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrEmpty(password))
                {
                    return null;
                }

                if (Login(userId, password))
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("登录出错：" + ex.Message, ex);
            }
            return HttpContext.Current.Session["UserInfo"] as cadre_info;
        }

        public static bool Login(string userId, string pwd, DateTime expires)
        {
            var user = UserRep.Login(userId, pwd);

            if (user == null)
            {
                return false;
            }

            HttpContext.Current.Session["UserInfo"] = user;
            HttpCookie cookie = new HttpCookie("UserInfo");
            cookie.HttpOnly = true;
            cookie.Expires = expires;
            cookie.Values.Add("LoginName", AesHelper.Encrypt(user.UserID.ToString()));
            cookie.Values.Add("Password", AesHelper.Encrypt(user.Pwd));
            HttpContext.Current.Response.Cookies.Add(cookie);
            return true;
        }

        public static bool Login(string userId, string pwd)
        {
            var user = UserRep.Login(userId, pwd);

            if (user == null)
            {
                return false;
            }

            HttpContext.Current.Session["UserInfo"] = user;
            HttpCookie cookie = new HttpCookie("UserInfo");
            cookie.HttpOnly = true;
            cookie.Values.Add("LoginName", AesHelper.Encrypt(user.UserID.ToString()));
            cookie.Values.Add("Password", AesHelper.Encrypt(user.Pwd));
            HttpContext.Current.Response.Cookies.Add(cookie);
            return true;
        }

        public static bool IsLogin
        {
            get
            {
                //session有值
                if (HttpContext.Current.Session["UserInfo"] != null)
                {
                    return true;
                }

                //取cookie
                HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];
                if (cookie == null)
                {
                    return false;
                }
                cookie.HttpOnly = true;
                string userId = AesHelper.Decrypt(cookie["LoginName"]);
                string password = AesHelper.Decrypt(cookie["Password"]);
                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrEmpty(password))
                {
                    return false;
                }

                return Login(userId, password, cookie.Expires);
            }
        }

        public static void Logout()
        {
            HttpContext.Current.Session.Remove("UserInfo");
            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Today.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                HttpContext.Current.Request.Cookies.Remove("UserInfo");
            }
        }
    }
}