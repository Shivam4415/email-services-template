using Library.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Library.Emails
{
    public static class CommonFunctions
    {
        public static IEnumerable<MailAddress> GetEmailAddressList(string emails, string emailToName = null)
        {
            try
            {
                List<MailAddress> _emailAddress = new List<MailAddress>();

                string[] _splittedEmails = emails.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string _email in _splittedEmails)
                {
                    if (Validator.IsValidEmail(_email))
                    {
                        if (string.IsNullOrWhiteSpace(emailToName))
                            _emailAddress.Add(new MailAddress(_email));
                        else
                            _emailAddress.Add(new MailAddress(_email, emailToName));
                    }
                }
                return _emailAddress;
            }
            catch
            {
                throw;
            }

        }
    }
}