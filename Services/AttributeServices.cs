using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Library.Global;
using Library.Entities;
using System.Collections;
using System.Data;
using Library.Global.Enum;

namespace Library.Emails
{
    internal class AttributeServices
    {

        /// <summary>
        /// Fetch list EmailAttributes from database and store in cache
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Attribute> GetAll()
        {
            try
            {
                IEnumerable<Attribute> list = CacheManager.Get("EmailAttributes", "MasterData") as IEnumerable<Attribute>;

                if (list != null)
                    return list;

                list = new AttributeRepository().GetAll();

                if (list == null)
                    return null;

                if (list.Count() == 0)
                    return null;

                CacheManager.Set("EmailAttributes", list, "MasterData", new CacheItemPolicy()
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
        /// get EmailAttributes object list for given Placeholders
        /// </summary>
        /// <param name="placeholderList"></param>
        /// <returns></returns>
        private List<Attribute> Get(List<string> placeholderList)
        {
            try
            {

                IEnumerable<Attribute> _attributesAll = GetAll();
                List<Attribute> _attributesList = new List<Attribute>();
                Attribute _attribute = new Attribute();

                foreach (string placeholder in placeholderList)
                {
                    _attribute = _attributesAll.FirstOrDefault(i => i.PlaceHolderTag == placeholder);
                    if (_attribute != null)
                        _attributesList.Add(_attributesAll.FirstOrDefault(i => i.PlaceHolderTag == placeholder));
                }

                return _attributesList;
            }
            catch
            {
                throw;
            }
        }



        /// <summary>
        /// Return placeholder and their values to be used replacing placeholders in Email templates
        /// </summary>
        /// <param name="placeholderList"></param>
        /// <param name="context"></param>
        /// <param name="Username"></param>
        /// <returns></returns>
        public Dictionary<string, string> Process(List<string> placeholderList, Dictionary<ObjectType, object> context = null, string dateTimeFormat = null)
        {
            try
            {
                //Dictionary object will contain list of placeholders with their values
                Dictionary<string, string> placeholderValuesPair = new Dictionary<string, string>();

                //Fetch EmailAttributes object list for the received placeholders
                IEnumerable<Attribute> _attributes = new List<Attribute>();
                if (placeholderList.Count > 0)
                    _attributes = new AttributeServices().Get(placeholderList);

                //for each of the remaining placeholders, fetch the values in the dictionary containing placeholder key and value
                foreach (var _attribute in _attributes)
                {

                    string propertyValue = string.Empty;
                    switch (_attribute.ObjectType)
                    {
                        case ObjectType.None:
                            propertyValue = new CustomAttributeServices().Process(_attribute.PlaceHolderTag, context, dateTimeFormat);
                            break;

                        case ObjectType.Share:
                            if (!context.ContainsKey(ObjectType.Share))
                                break;
                            if (!((IDictionary<string, object>)context[ObjectType.Share]).ContainsKey(_attribute.ObjectPropertyName))
                                break;
                            propertyValue = ((IDictionary<string, object>)context[_attribute.ObjectType])[_attribute.ObjectPropertyName]?.ToString();
                            break;

                        case ObjectType.Map:
                        case ObjectType.Dataset:
                        case ObjectType.User:
                        case ObjectType.Account:
                        case ObjectType.Product:
                        case ObjectType.Price:
                        case ObjectType.Employee:
                        case ObjectType.Payment:
                        case ObjectType.Subscription:
                        case ObjectType.Invoice:
                        case ObjectType.Currency:
                        case ObjectType.SupportTicket:
                            if (!context.ContainsKey(_attribute.ObjectType))
                                break;
                            if (!string.IsNullOrWhiteSpace(dateTimeFormat)
                                && (context[_attribute.ObjectType]?.GetType().GetProperties().FirstOrDefault(pi => pi?.Name == _attribute.ObjectPropertyName)?.PropertyType.FullName == typeof(DateTime).ToString()
                                      || context[_attribute.ObjectType]?.GetType().GetProperties().FirstOrDefault(pi => pi?.Name == _attribute.ObjectPropertyName)?.PropertyType == typeof(Nullable<DateTime>)))
                            {
                                if (context[_attribute.ObjectType]?.GetType().GetProperties().FirstOrDefault(pi => pi?.Name == _attribute.ObjectPropertyName)?.GetValue(context[_attribute.ObjectType]) != null)
                                    propertyValue = ((DateTime)context[_attribute.ObjectType]?.GetType().GetProperties().FirstOrDefault(pi => pi?.Name == _attribute.ObjectPropertyName)?.GetValue(context[_attribute.ObjectType], null)).ToString(dateTimeFormat);
                                break;
                            }

                            propertyValue = context[_attribute.ObjectType]?.GetType().GetProperties().FirstOrDefault(pi => pi?.Name == _attribute.ObjectPropertyName)?.GetValue(context[_attribute.ObjectType], null)?.ToString();

                            break;
                    }
                    //Dictionary contains placeholders as key and values as value
                    placeholderValuesPair.Add(_attribute.PlaceHolderTag, propertyValue);
                }

                return placeholderValuesPair;

            }
            catch
            {
                throw;
            }
        }
    }
}