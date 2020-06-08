using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace DataAccess
{
    public class SendEmail
    {
        public string SendPasswordToEmail(string mailTo, string mailCC, string mailSubject, string mailBody)
        {
            string success = string.Empty;
            MailAddress from = new MailAddress("info@dropinion.net", "Dr.Opinion"); 
            using (var message = new MailMessage("info@dropinion.net", mailTo))
            {
                message.From = from;
                // message.Subject = "Password Recovery e-mail";
                message.Subject = mailSubject; //"Join Us Free !!";
                message.Body = createEmailBody(mailTo, mailSubject, mailBody);
                message.IsBodyHtml = true;
                using (SmtpClient client = new SmtpClient
                {
                    EnableSsl = false,
                    Host = "dropinion.net",
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("info@dropinion.net", "Info@123")
                })
                {
                    client.Send(message);
                    success = "Send Password to Email-id : " + mailTo + ":-" + mailSubject;
                }
            }
            return success;
        }


        private string createEmailBody(string userName, string title, string message)
        {
            title = "Password Is : ";

            string body = string.Empty;
            body = message;
            //using streamreader for reading my htmltemplate   
            string path = "~/doctorhub.html";
            //  string path = "~/Emailtamp.htm";
            //using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(path)))
            //{
            //    body = reader.ReadToEnd();
            //}

            body = body.Replace("{UserName}", userName); //replacing the required things  

            body = body.Replace("{Title}", title);

            body = body.Replace("{message}", message);

            return body;

        }
    }
}
