using SPOffice.BusinessService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPOffice.DataAccessObject.DTO;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace SPOffice.BusinessService.Services
{
    public class MailBusiness : IMailBusiness
    {
        string EmailFromAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailFromAddress"];
        string host = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-host"];
        string smtpUserName = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-UserName"];
        string smtpPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-Password"];
        string port = System.Web.Configuration.WebConfigurationManager.AppSettings["Port"];

        public async Task<bool> MailSendAsync(Mail mailObj)
        {
            try
            {
                using (var mail = new MailMessage(new MailAddress(EmailFromAddress, "Admin_@_SPOffice"), new MailAddress(mailObj.To)))
                {
                    mail.Subject = mailObj.Subject;
                    mail.Body = mailObj.Body;
                    mail.IsBodyHtml = true;
                    using (var client = new SmtpClient())
                    {
                        client.UseDefaultCredentials = false;
                        client.Host = host;
                        client.Port = int.Parse(port);
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                        await client.SendMailAsync(mail);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}