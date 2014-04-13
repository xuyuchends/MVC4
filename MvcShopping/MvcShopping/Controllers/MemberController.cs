using MvcShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcShopping.Controllers
{
    public class MemberController : Controller
    {
        MvcShoppingContext db = new MvcShoppingContext();
        private string pwSalt = "AlrySqloPe2Mh784QQwG6jRAfkdPpDa90J0i";
        // 會員註冊頁面
        public ActionResult Register()
        {
            return View();
        }

        // 寫入會員資料
        [HttpPost]
        public ActionResult Register([Bind(Exclude="RegisterOn,AuthCode")] Member member)
        {
            // 检查会员是否存在
            var chkNumber = db.Members.Where(p => p.Email == member.Email).FirstOrDefault();
            if (chkNumber != null)
            {
                ModelState.AddModelError("Email", "您输入的Email已经有人注册过了");
            }
            if (ModelState.IsValid)
            {
                member.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pwSalt + member.Password, "SHA1");
                member.RegisterOn = DateTime.Now;
                member.AuthCode = Guid.NewGuid().ToString();
                db.Members.Add(member);
                db.SaveChanges();

                sendAuthMember(member);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// 发送注册信息
        /// </summary>
        /// <param name="member"></param>
        private void sendAuthMember(Member member)
        {
            string mailBody = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MemberRegisterMailTemplate.html"));
            mailBody = mailBody.Replace("{{name}}", member.Name);
            var authurl = new UriBuilder(Request.Url)
            {
                Path = Url.Action("ValidateRegister", new { id = member.AuthCode }),
                Query = ""
            };
            mailBody = mailBody.Replace("{{authurl}}", authurl.ToString());
            try
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("xuyuchends", "qwe123!@#");
                SmtpServer.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("xuyuchends@gmail.com");
                mail.To.Add(member.Email);
                mail.Subject = "会员注册确认信";
                mail.Body = mailBody;
                mail.IsBodyHtml = true;
             //   SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
                // 记录到数据库，以免有会员无法登陆
            }
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
            ModelState.AddModelError("", "您输入的账号或密码错误");
            return View();
        }

        private bool ValidateUser(string email, string password)
        {
            var hashpw = FormsAuthentication.HashPasswordForStoringInConfigFile(pwSalt + password, "SHA1");
            var member = (from p in db.Members where p.Email == email && p.Password == hashpw select p).FirstOrDefault();

            if (member != null)
            {
                if (member.AuthCode == null)
                {
                    return true;
                }
                else
                {
                    ModelState.AddModelError("", "您尚未通过会员验证，请收信并点击会员验证链接");
                    return false;
                }
            }
            else
            {
                ModelState.AddModelError("", "您输入的账号或密码错误");
                return false;
            }
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

        public ActionResult ValidateRegister(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            var member = db.Members.Where(p => p.AuthCode == id).FirstOrDefault();
            if (member != null)
            {
                TempData["LastTempMessage"] = "会员验证成功，您现在可以登录网站了";
                // 验证后清空
                member.AuthCode = null;
                db.SaveChanges();
            }
            else
            {
                TempData["LastTempMessage"] = "查无此会员验证码，您可能已经验证过了";
            }
            return RedirectToAction("Login", "Member");
        }

        [HttpPost]
        public ActionResult CheckDup(string Email)
        {
            var member = db.Members.Where(p => p.Email=Email).FirstOrDefault();
            if (member != null)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }
    }
}
