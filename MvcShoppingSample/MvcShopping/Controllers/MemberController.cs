using MvcShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcShopping.Controllers
{
    public class MemberController : Controller
    {
        // 會員註冊頁面
        public ActionResult Register()
        {
            return View();
        }

        // 寫入會員資料
        [HttpPost]
        public ActionResult Register(Member member)
        {
            return View();
        }

        // 顯示會員登入頁面
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        // 執行會員登入
        [HttpPost]
        public ActionResult Login(string email, string password, string returnUrl)
        {
            if (ValidateUser(email, password))
            {
                FormsAuthentication.SetAuthCookie(email, false);

                if (String.IsNullOrEmpty(returnUrl)) {
                    return RedirectToAction("Index", "Home");
                } else {
                    return Redirect(returnUrl);
                }
            }

            return View();
        }

        private bool ValidateUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        // 執行會員登出
        public ActionResult Logout()
        {
            // 清除表單驗證的 Cookies
            FormsAuthentication.SignOut();

            // 清除所有曾經寫入過的 Session 資料
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
