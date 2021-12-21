using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Caching;
using Library.Global;


namespace Library.Emails
{
    internal class SMTPServerServices
    {
        /// <summary>
        /// Fetch SMTP server from cache/Database
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SmtpServer> GetSmtpServers()
        {
            try
            {
                IEnumerable<SmtpServer> list = CacheManager.Get("SmtpServers", "MasterData") as IEnumerable<SmtpServer>;

                if (list != null)
                    return list;

                list = new SMTPServerRepository().GetAll().ToList();

                if (list == null)
                    return null;

                if (list.Count() == 0)
                    return null;

                CacheManager.Set("SmtpServers", list, "MasterData", new CacheItemPolicy()
                {
                    AbsoluteExpiration = System.Runtime.Caching.ObjectCache.InfiniteAbsoluteExpiration,
                    SlidingExpiration = System.Runtime.Caching.ObjectCache.NoSlidingExpiration,
                    Priority = CacheItemPriority.Default
                });

                return list;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get SMTP values: used for ending email
        /// </summary>
        /// <param name="smtpServer"></param>
        /// <returns></returns>
        private static SmtpClient GetSmtpClient(SmtpServer smtpServer)
        {
            try
            {
                SmtpClient smtp = new SmtpClient(smtpServer.Host, smtpServer.Port);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(smtpServer.Username, smtpServer.Password);
                return smtp;
            }
            catch
            {
                throw;
            }
        }

     /// <summary>
     /// Send Email for the mail message
     /// </summary>
     /// <param name="mailMessage"></param>
     /// <returns></returns>
        public bool Send(MailMessage mailMessage)
        {
            try
            {
                //Get SMTP server list from cache/database
                IEnumerable<SmtpServer> _smtpServers = new SMTPServerServices().GetSmtpServers();
                SmtpServer _smtpServer = _smtpServers.FirstOrDefault(item => item.IsDefault == true);

                //try sending email using default SMTP Server
                if (_smtpServer != null)
                {
                    try
                    {
                        GetSmtpClient(_smtpServer).Send(mailMessage);
                        return true;
                    }
                    catch (SmtpException)
                    {
                        return new SMTPServerServices().SendMailUsingAllOption(_smtpServers, mailMessage);
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    return new SMTPServerServices().SendMailUsingAllOption(_smtpServers, mailMessage);
                }


            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Send email through this option, if default smtp server fails
        /// </summary>
        /// <param name="smtpServers"></param>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        public bool SendMailUsingAllOption(IEnumerable<SmtpServer> smtpServers, MailMessage mailMessage)
        {
            foreach (SmtpServer smtpServer in smtpServers)
            {
                try
                {
                    GetSmtpClient(smtpServer).Send(mailMessage);
                }
                catch (SmtpException)
                {
                    continue;// try the next smtp server
                }
                catch
                {
                    throw;
                }
                return true;
            }

            throw new ArgumentException("SmtpException with all smtp servers. Unable to send email");
        }


    }


}