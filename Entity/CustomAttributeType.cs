using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Emails
{
    /// <summary>
    /// These are custom attributes that could be present in Email
    /// </summary>
    public enum CustomAttributeType
    {
        DateTimeCst = 1,
        DateTimeUtc = 2,
        DateTimeNow = 3,
        AppUrl = 4,
        defaultUrl = 5,
        BlackpearlUrl = 6,
        KeyName = 7,
        AccountVerificationUrl = 8,
        PasswordResetUrl = 9,
        WelcomePassword = 10,      
        ObjectShareUrl = 11,
        ObjectTypeName = 12,
        NoofUser = 13,
        PriceDescription = 14,
        RegistrationUrlWithEncodedEmail = 15,
        BlackpearlPasswordResetUrl = 16,
        TimesheetApprovalRequestUrlonBlackpearl = 17,
        RequestedPlanForTrial = 18,
        RequestedProductForTrial = 19
    }
}