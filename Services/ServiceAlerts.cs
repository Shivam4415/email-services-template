using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Library.Emails
{
    public class ServiceAlerts
    {
        /// <summary>
        /// Send Email on Service stop.
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        public void Notify(string toEmail, string subject, string message)
        {
            try
            {
                Send(toEmail, subject, message);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Send email with exception if service start is failed 
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Notify(string toEmail, string subject, string message, Exception exception)
        {
            try
            {
                string _message = message;
                try
                {
                    if (exception != null)
                    {
                        if (exception.Message != null)
                        {
                            _message += " Exception:" + exception.Message;
                        }
                        if (exception.InnerException != null)
                        {
                            _message += " ,InnerException:" + exception.InnerException;
                        }
                        if (exception.StackTrace != null)
                        {
                            _message += " ,StackTrace:" + exception.StackTrace;
                        }
                    }
                }
                catch{}

                Send(toEmail, subject, _message);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Internal function for send email.
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        private void Send (string toEmail, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(toEmail))
                return;

            string _fromEmail = "noreply@com";
            string _fromName = "default Team";
            MailMessage mailMessage = new System.Net.Mail.MailMessage();



            //FromEmail in parameter list will take priority over one available through template
            mailMessage.From = new MailAddress(_fromEmail, _fromName);

            //Mail subject
            mailMessage.Subject = subject;

            //MailBody
            //  mailMessage.IsBodyHtml = false;//default

            mailMessage.Body = message;


            //Email Priority
            mailMessage.Priority = MailPriority.High;

            //ToEmail
            IEnumerable<MailAddress> _toEmails = CommonFunctions.GetEmailAddressList(toEmail);
            if (_toEmails.Count() < 1)
                return;
            foreach (var _toEmail in _toEmails)
            {
                mailMessage.To.Add(_toEmail);
            }

            new SMTPServerServices().Send(mailMessage);

        }

        /// <summary>
    }
}