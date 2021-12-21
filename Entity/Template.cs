using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Library.Global;

namespace Library.Emails
{
    public class Template:ICloneable
    {

         public string FromEmail { get; set; }="noreply@com";
        public EmailType EmailType { get; set; }
        public MailPriority MailPriority { get; set; }
        public string Subject { get; set; }
        public string HTMLTemplate { get; set; }
        public string TextTemplate { get; set; }

        public bool IsBodyHTML { get; set; } = false;


        public string FromName { get; set; } = "default";
     
        public Template(string message, string Subject, string fromEmail, string fromName)
        {
            this.HTMLTemplate = message;
            this.TextTemplate = message;
            this.FromName = fromName;
            this.FromEmail = fromEmail;
            this.Subject = Subject;
        }

        public Template()
        {

        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}