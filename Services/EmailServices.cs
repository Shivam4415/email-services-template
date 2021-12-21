using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.IO;
using Library.Entities;
using Library.Global;
using Library.Global.Enum;

namespace Library.Emails
{
    public class EmailServices
    {
       /// <summary>
       /// Send Email
       /// </summary>
       /// <param name="emailType"></param>
       /// <param name="toEmail"></param>
       /// <param name="context"></param>
       /// <param name="ccEmail"></param>
       /// <param name="attachments"></param>
       /// <param name="template"></param>
       /// <param name="fromEmail"></param>
       /// <param name="dateTimeFormat"></param>
       /// <param name="emailToName">Name of user, whom sending email</param>
       /// <returns></returns>
        public bool Send(EmailType emailType, string toEmail, Dictionary<ObjectType, object> context = null, string ccEmail = null, 
            Dictionary<string,  byte[]> attachments = null, Template template = null,string fromEmail = null,string dateTimeFormat=null, string emailToName= null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(toEmail))
                    return false;

                template = new TemplateServices().Process(emailType, context, template,dateTimeFormat);


                //MAIL BODY
                //if email body is not present, send No mail body message
                if (string.IsNullOrWhiteSpace(template.TextTemplate) && string.IsNullOrWhiteSpace(template.HTMLTemplate))
                    return false;


                MailMessage mailMessage = new System.Net.Mail.MailMessage();

                if (!string.IsNullOrWhiteSpace(fromEmail))
                    template.FromEmail = fromEmail;

                //FromEmail in parameter list will take priority over one available through template
                mailMessage.From = new MailAddress(template.FromEmail, template.FromName);

                //Mail subject
                mailMessage.Subject = template.Subject;

                //MailBody
                mailMessage.IsBodyHtml = false;//default
                                               //if texttemplate is not null
                if (!string.IsNullOrWhiteSpace(template.TextTemplate))
                {
                    mailMessage.Body = template.TextTemplate;
                    //if html template is not null
                    if (!string.IsNullOrWhiteSpace(template.HTMLTemplate))
                    {
                        //// Add the alternate body to the message.
                        AlternateView alternate = AlternateView.CreateAlternateViewFromString(template.HTMLTemplate, null, "text/html");
                        mailMessage.AlternateViews.Add(alternate);
                        mailMessage.IsBodyHtml = true;
                    }
                }

                //if texttemplate is null
                if (string.IsNullOrWhiteSpace(template.TextTemplate))
                {
                    mailMessage.Body = template.HTMLTemplate;
                    mailMessage.IsBodyHtml = true;
                }

                //Email Priority
                mailMessage.Priority = template.MailPriority;

                //ToEmail
                IEnumerable<MailAddress> _toEmails = CommonFunctions.GetEmailAddressList(toEmail,emailToName);
                if (_toEmails.Count() < 1)
                    return false;
                foreach (var _toEmail in _toEmails)
                {
                    mailMessage.To.Add(_toEmail);
                }

                //CCEmail
                if (!string.IsNullOrWhiteSpace(ccEmail))
                {
                    IEnumerable<MailAddress> _ccEmails = CommonFunctions.GetEmailAddressList(ccEmail);
                    foreach (var _ccEmail in _ccEmails)
                    {
                        mailMessage.CC.Add(_ccEmail);
                    }
                }

                //Email Attachment
                //Keep the field as Attachment
                if (attachments != null)
                {
                    //  We can support more then one attachement at a time
                    foreach (KeyValuePair<string,byte[]> attachment in attachments)
                    {
                        if (attachment.Key != null && attachment.Value != null)
                        {
                            MemoryStream file = new MemoryStream(attachment.Value);
                            file.Seek(0, SeekOrigin.Begin);
                            Attachment data = new Attachment(file, attachment.Key, "application/pdf");
                            file.Position = 0;
                            mailMessage.Attachments.Add(data);
                        }
                    }
                }

                return new SMTPServerServices().Send(mailMessage);
            }
            catch
            {
                throw;
            }
        }
       
        /// <summary>
        /// Return list of validated emailsIds
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        
      

        public static bool IsEmailBlackListed(string email)
        {
            try
            {
                return new EmailRepository().IsEmailBlackListed(email);
            }
            catch
            {
                throw;
            }
        }
    }
}