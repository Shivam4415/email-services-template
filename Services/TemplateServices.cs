using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using Library.Global;
using Library.Entities;
using System.Collections;
using System.Text.RegularExpressions;
using Library.Global.Enum;

namespace Library.Emails
{
    public class TemplateServices
    {
        /// <summary>
        /// Get EmailTemplates list
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Template>  Get()
        {
            IEnumerable<Template> list = CacheManager.Get("EmailTemplates", "MasterData") as IEnumerable<Template>;

            if (list != null)
                return list;

            list = new TemplateRepository().GetAll();

            if (list == null)
                return null;

            if (list.Count() == 0)
                return null;

            CacheManager.Set("EmailTemplates", list, "MasterData", new CacheItemPolicy()
            {
                AbsoluteExpiration = System.Runtime.Caching.ObjectCache.InfiniteAbsoluteExpiration,
                SlidingExpiration = System.Runtime.Caching.ObjectCache.NoSlidingExpiration,
                Priority = CacheItemPriority.Default
            });

            return list;
        }
        /// <summary>
        /// get Template for given emailType
        /// </summary>
        /// <param name="emailType"></param>
        /// <returns></returns>
        public static Template Find(EmailType emailType)
        {
            try
            {
                IEnumerable<Template> templateList = TemplateServices.Get();
                 return templateList.FirstOrDefault(i=>i.EmailType==emailType);

            }
            catch
            {
                throw;
            }
        }
        

        /// <summary>
        /// get Email template for specific template
        /// </summary>
        /// <param name="emailType"></param>
        /// <param name="toEmail"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Template Process(EmailType emailType, Dictionary<ObjectType, object> context = null, Template template=null,string dateTimeFormat=null)
        {

            try
            {
                //Get Email-template if template is not provided
                if (template == null)
                {
                    if (emailType != EmailType.None)
                    {
                        template = (Template)TemplateServices.Find(emailType)?.Clone();

                        if (template == null)
                            return null;
                    }
                    else
                    {
                        return null;
                    }
                }



                //GetPlaceholder list will contain list of unique placeholders
                //Dictionary contains placeholders as key and values as value
                List<string> placeHolderList = GetPlaceHolderList(template);
                if (placeHolderList.Count > 0)
                {
                    Dictionary<string, string> _placeHolderValues = new AttributeServices().Process(placeHolderList, context, dateTimeFormat);
                   
                    
                    //---------------------------------------------------
                    /* No placeholders in following emails 
                   *  UserDeleteByAccountOwner = 8,
                   AccountOwnerLoginDeleted = 9,
                   AccountMemberLoginDeleted = 10,
                   */

                    //Replace placeholders with values in HTMLTemplate
                    template.HTMLTemplate = ReplacePlaceholders(template.HTMLTemplate, _placeHolderValues);

                    //Replace placeholders with values in TextTemplate
                    template.TextTemplate = ReplacePlaceholders(template.TextTemplate, _placeHolderValues);

                    //Replace placeholders with values in Subject
                    template.Subject = ReplacePlaceholders(template.Subject, _placeHolderValues);
                }

                return template;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// returns unique placeholderlist present in the email-template
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        private List<string> GetPlaceHolderList(Template template)
        {
            try
            {
                List<string> templatePlaceholders = new List<string>();
                templatePlaceholders = GetPlaceHolders(template.HTMLTemplate);
                templatePlaceholders = templatePlaceholders.Union(GetPlaceHolders(template.TextTemplate)).ToList();
                templatePlaceholders = templatePlaceholders.Union(GetPlaceHolders(template.Subject)).ToList();

                return templatePlaceholders;
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// returns placeholders present in the string
        /// </summary>
        /// <param name="templateString"></param>
        /// <returns></returns>
        private List<string> GetPlaceHolders(string templateString)
        {
            try
            {
                List<string> templatePlaceholders = new List<string>();

                //Always the placeholder pattern is {var}
                //fetch all the placeholders from EmailTemplates
                String pattern = @"({[^}]*})";

                if (!string.IsNullOrWhiteSpace(templateString))
                {
                    foreach (Match m in Regex.Matches(templateString, pattern))
                    {
                        if (!(templatePlaceholders.Contains(m.Groups[1].Value)))
                            templatePlaceholders.Add(m.Groups[1].Value);

                    }

                }

                return templatePlaceholders;
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// replace placeholder texts in email-templates
        /// </summary>
        /// <param name="templateValue"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        private string ReplacePlaceholders(string templateValue, Dictionary<string, string> attributes)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(templateValue))
                {
                    foreach (var attribute in attributes)
                    {
                        if (templateValue.Contains(attribute.Key))
                             templateValue = templateValue.Replace(attribute.Key, attributes[attribute.Key]);

                    }
                    
                }
                return templateValue;
            }
            catch
            {
                throw;
            }
        }
    }
}