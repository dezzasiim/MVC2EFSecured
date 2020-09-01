using System.Web.Mvc;
using MVC2EFSecured.UI.MVC.Models;
using System.Net.Mail;//mail
using System.Net;//mail
using System;//exception handling

namespace MVC2EFSecured.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            //  Check that the cvm data just passed into the method is a valid ContactViewModel
            if (ModelState.IsValid)
            {
                //Build the message

                string message = $"You have received an email from {cvm.Name} with a subject of {cvm.Subject}. Please respond to {cvm.Email} with your response to the following message: <br/>{cvm.Message}";

                MailMessage mm = new MailMessage("admin@dereksym.com", "admin@dereksym.com", cvm.Subject, message);

                //MailMessage Properties

                // Allow HTML formatting in the email
                mm.IsBodyHtml = true;

                mm.Priority = MailPriority.High;

                //Respond to the sender and not the admin@yoursiteurl.com
                mm.ReplyToList.Add(cvm.Email);
                mm.CC.Add("yourccperson@yoursite.com");

                //Smtpclient - this is the infor from the host that allows this message to be sent
                SmtpClient client = new SmtpClient("mail.yourmaildomain.com");

                //client creds
                client.Credentials = new NetworkCredential("admin@yoursite.com", "Your password");// clear test password is NOT best practice

                // Set the SMTP Port if needed
                client.Port = 8889;

                //Try to send the email
                try
                {
                    //Attempt to send 
                    client.Send(mm);
                }
                catch(Exception ex)
                {
                    ViewBag.ErrorMessage = $"We're sorry your request could not be completed at this time. Please try again later. Error Message:<br/> {ex.StackTrace}";
                    return View(cvm);
                }

                // Everything has been sent, so send the user to a confirmation page that is also strongly typed
                return View("EmailConfirmation", cvm);

            }
            else//The ModelState is Not valid
            {
                // Send back the form with the completed inputs so the user can try again and not have to re-type everything
                return View(cvm);
            }
            
        }
    }
}
