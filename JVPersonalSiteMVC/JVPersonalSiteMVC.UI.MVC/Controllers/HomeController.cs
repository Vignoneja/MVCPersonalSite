using JVPersonalSiteMVC.UI.MVC.Models;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace JVPersonalSiteMVC.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ContactAjax(ContactViewModel cvm)
        {
            string body = $"You have received an email from <strong>{cvm.Name}</strong>. The e-mail address given was <strong>{cvm.Email}</strong>.<br/>" +
                $"<strong>The following message was sent:</strong> {cvm.Message}";
            string EmailUser = ConfigurationManager.AppSettings["EmailUser"].ToString();
            string EmailClient = ConfigurationManager.AppSettings["EmailClient"].ToString();
            string EmailPassword = ConfigurationManager.AppSettings["EmailPassword"].ToString();
            string EmailTo = ConfigurationManager.AppSettings["EmailTo"].ToString();

            MailMessage mm = new MailMessage(EmailUser,EmailTo,"Vignonetech.com",body);

            mm.IsBodyHtml = true;
            mm.ReplyToList.Add(cvm.Email);

            SmtpClient smtp = new SmtpClient(EmailClient);
            //NetworkCredential cred = new NetworkCredential(EmailUser, EmailPassword);
            smtp.Port = 8889;

            smtp.Credentials = new NetworkCredential(EmailUser, EmailPassword);
            smtp.Send(mm);
            return Json(cvm);
        }//END CONTACTAJAX

        public PartialViewResult ContactConfirmation(string name, string email)
        {
            ViewBag.Name = name;
            ViewBag.Email = email;
            return PartialView("ContactConfirmation");
        }//END PARTIALVIEW
    }
}
