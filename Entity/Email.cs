using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Global;

namespace Library.Emails
{
       
    public class Email
    {
        private string _fromEmail;
        private string _toEmail;
        public string ToEmail
        {
            get { return _toEmail; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("ToEmail must not be blank");

                _toEmail = value;
            }
        }
        public string CCEmail { get; set; }
        

        public string FromEmail
        {
            get { return _fromEmail; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("FromEmail must not be blank");

                _fromEmail = value;
            }
        }

        public string DisplayName { get; set; }
        public EmailType EmailType { get; set; }
        public int MailPriority { get; set; }
        public string Subject { get; set; }
        public string HTMLTemplate { get; set; }
        public string TextTemplate { get; set; }
        public bool IsBodyHTML { get; set; }

        public byte[] Attach { get; set; }
        public Dictionary<string, string> Fields { get; set; }

        public Email()
        {
            Fields = new Dictionary<string, string>();
            this.FromEmail = "noreply@com";
        }

        public Email(EmailType emailType, string toEmail, Dictionary<string, string> fieldValues=null)
        {
            this.FromEmail = "noreply@com";
            this.EmailType = emailType;
            this.ToEmail = toEmail;
            this.Fields = fieldValues;

        }

        public void AddField(string key, string value)
        {
            Fields.Add(key, value);
        }
    }
}