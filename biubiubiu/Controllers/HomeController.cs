using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using biubiubiu.Models;
using biubiubiu.Utlis;
using System.Runtime.InteropServices.ComTypes;

namespace biubiubiu.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationManager appManager = ApplicationManager.Load();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult ReturnLovePage()
        {
            SendEmail("有人打开了你的页面");
            return View();
        }

        public IActionResult CheckNum()
        {
            string data = HttpContext.Request.Query["num"];
            string res = string.IsNullOrEmpty(data) ? "kong" : data;
            if (res == "190826")
            {
                SendEmail("你的小伙伴登录了系统");
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        public IActionResult Nop()
        {
            return Content("ok");
        }
        public IActionResult Okk()
        {
            SendEmail("对方原谅了你");
            return Content("ok");
        }
        public IActionResult Wait()
        {
            SendEmail("对方需要时间考虑");
            return Content("ok");
        }
        public IActionResult Refuse()
        {
            SendEmail("对方拒绝了你，请你好自为之");
            return View();
        }
        public IActionResult WaitingForYou()
        {
            return View();
        }


        public IActionResult Crash()
        {
            appManager.Stop();
            return null;
        }
        public IActionResult QuestionPage()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public static bool SendEmail(string info)
        {
            string senderServerIp = "smtp.126.com";   //使用glbschool代理邮箱服务器
            string toMailAddress = "wqshj@126.com";              //要发送对象的邮箱
            string fromMailAddress = "wqshj@126.com";           
            string subjectInfo = "来自原谅系统的通知";                  //主题
            string bodyInfo = "<p>"+ info + "</p>";//以Html格式发送的邮件
            string mailUsername = "自己填写";                //登录邮箱的用户名（自己填写 邮箱）
            string mailPassword = "自己填写";         //对应的登录邮箱的密码（自己填写 一般用pop3协议）
            string mailPort = "25";                      //发送邮箱的端口号
            //string attachPath = "E:\\123123.txt; E:\\haha.pdf";   //邮件附件
            //创建发送邮箱的对象
            EmailHelper email = new EmailHelper(senderServerIp, toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, false, false);
            bool ok = email.Send();

            return ok;
        }
    }
}
