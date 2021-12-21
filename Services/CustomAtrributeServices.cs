using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Entities;
using Library.Global;
using Library.Global.Enum;

namespace Library.Emails
{
    internal class CustomAttributeServices
    {
        /// <summary>
        /// Processess Custom Attributes present in emails
        /// These are of type None
        /// </summary>
        /// <param name="placeholderList"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// 

        public string Process(string placeholder, Dictionary<ObjectType, object> context = null, string dateTimeFormat = null)
        {
            try
            {
                string _customValue = string.Empty;
                CustomAttributeType _customtypeEnum = (CustomAttributeType)Enum.Parse(typeof(CustomAttributeType), placeholder.Replace("{", "").Replace("}", ""));
                switch (_customtypeEnum)
                {
                    //Get Blackpearl url
                    case CustomAttributeType.BlackpearlUrl:
                        _customValue = ServiceHostUrl.Helm;
                        break;

                    //Get default url
                    case CustomAttributeType.defaultUrl:
                        _customValue = ServiceHostUrl.default;
                        break;

                    //Get App url
                    case CustomAttributeType.AppUrl:
                        _customValue = ServiceHostUrl.App;
                        break;

                    //used in email template: EmailType.AccountVerificationLink
                    case CustomAttributeType.AccountVerificationUrl:
                    //used in email template: EmailType.ForgotPasswordLink
                    case CustomAttributeType.PasswordResetUrl:
                    //used in email template: EmailType.ForgotPasswordOnBlackpearl
                    case CustomAttributeType.BlackpearlPasswordResetUrl:
                    //used in email templates: EmailType.WelcomeEmailForNewUser & EmailType.WelcomeEmailForEmployees & EmailType.defaultUserRegistration & EmailType.CampaignRegistrationEmail & EmailType.ResellerRegistration
                    case CustomAttributeType.WelcomePassword:
                    //used in email template: EmailType.ObjectSharedWithExternalUser
                    case CustomAttributeType.RegistrationUrlWithEncodedEmail:
                    //used in email template: EmailType.EditTimesheetApprovalRequestOnBlackpearl
                    case CustomAttributeType.TimesheetApprovalRequestUrlonBlackpearl:
                    //used in email template: EmailType.EditTimesheetApprovalRequestOnBlackpearl
                    case CustomAttributeType.KeyName:
                    //used in email template: EmailType.TrialForm
                    case CustomAttributeType.RequestedPlanForTrial:
                    //used in email template: EmailType.TrialForm_ProductVersion79
                    case CustomAttributeType.RequestedProductForTrial:
                    //used in email template: EmailType.PaymentInformationonSignUp
                    case CustomAttributeType.PriceDescription:
                    //used in email template: EmailType.AccountRenewalReminderOnStripe
                    case CustomAttributeType.NoofUser:
                        if (!context.ContainsKey(ObjectType.None))
                            break;
                        _customValue= context[ObjectType.None]?.ToString();
                        break;

                    //used in email templates: EmailType.ObjectSharedWithExternalUser & EmailType.ObjectSharedWithdefaultUser
                    case CustomAttributeType.ObjectShareUrl:
                        if (!context.ContainsKey(ObjectType.Share))
                            break;
                        if (!((IDictionary<string, object>)context[ObjectType.Share]).ContainsKey("ObjectId"))
                            break;
                        if (!((IDictionary<string, object>)context[ObjectType.Share]).ContainsKey("ObjectType"))
                            break;
                        string _objectId = ((IDictionary<string, object>)context[ObjectType.Share])["ObjectId"]?.ToString();

                        string _objectTypeUrl = ((IDictionary<string, object>)context[ObjectType.Share])["ObjectType"]?.ToString().ToLower();
                        //if objecttype is dataset, the url should be appurl/data-set/id
                        _objectTypeUrl = _objectTypeUrl == "dataset" ? "data-set" : _objectTypeUrl;
                        _customValue = ServiceHostUrl.App + "/" + _objectTypeUrl + "/" + _objectId;
                        break;

                    //used in email templates: EmailType.ObjectSharedWithExternalUser & EmailType.ObjectSharedWithdefaultUser
                    case CustomAttributeType.ObjectTypeName:
                        if (!context.ContainsKey(ObjectType.Share))
                            break;
                        if (!((IDictionary<string, object>)context[ObjectType.Share]).ContainsKey("ObjectType"))
                            break;
                        _customValue = ((IDictionary<string, object>)context[ObjectType.Share])["ObjectType"]?.ToString().ToLower();
                        //if objecttype is view, it should be called dataset view
                        _customValue = _customValue == "view" ? "dataset view" : _customValue;
                        break;

                    //used in email template: EmailType.PaymentSucceededOnStripe
                    case CustomAttributeType.DateTimeNow:
                        if (string.IsNullOrWhiteSpace(dateTimeFormat))
                            _customValue = DateTime.Now.ToString();
                        else
                            _customValue = DateTime.Now.ToString(dateTimeFormat);
                        break;

                    //unused
                    case CustomAttributeType.DateTimeCst:
                        DateTime timeUtc = DateTime.UtcNow;
                        TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                        if (string.IsNullOrWhiteSpace(dateTimeFormat))
                            _customValue = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone).ToString();
                        else
                            _customValue = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone).ToString(dateTimeFormat);
                        break;

                    //unused
                    case CustomAttributeType.DateTimeUtc:
                        if (string.IsNullOrWhiteSpace(dateTimeFormat))
                            _customValue = DateTime.UtcNow.ToString();
                        else
                            _customValue = DateTime.UtcNow.ToString(dateTimeFormat);
                        break;

                }

                return _customValue;
            }
            catch
            {
                throw;
            }
        }

    }
}