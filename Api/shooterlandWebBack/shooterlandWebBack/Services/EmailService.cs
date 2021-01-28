using shooterlandWebBack.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace shooterlandWebBack.Services
{
    public interface IEmailService
    {
        public void RegistrationEmail(string username, string password, string email);

        public void ForgetCredencialsEmail(string username, string newPassword, string email);

    }
    public class EmailService: IEmailService
    {
        public void ForgetCredencialsEmail(string username, string newPassword, string email)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            mail.From = new MailAddress("noreply.shooterland@gmail.com");
            mail.Subject = "Forget Credencials";
            mail.Body = "<h2>New Password !!</h2>"
                       + "<p>Username: " + username + "</p>"
                       + "<p>New Password: " + newPassword + "</p>";
            mail.IsBodyHtml = true;
            mail.To.Add(email);

            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("noreply.shooterland@gmail.com", "Shooterland2020");
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpClient.Send(mail);
        }

        /// <summary>
        /// This method sends a email when you register successfully
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        public void RegistrationEmail(string username, string password, string email)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            mail.From = new MailAddress("noreply.shooterland@gmail.com");
            mail.Subject = "Registration";
            mail.Body = "<h1>Thank you for registering with Shooterland.</h1>"
                       + "<h2>Let's kill some monsters!!</h2>"
                       + "<p>Username: " + username + "</p>"
                       + "<p>Password: " + password + "</p>";
            mail.IsBodyHtml = true;
            mail.To.Add(email);

            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("noreply.shooterland@gmail.com", "Shooterland2020");
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpClient.Send(mail);
        }
    }
}
