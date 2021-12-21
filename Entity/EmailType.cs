using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Emails
{
    public enum EmailType
    {
        None = 0,

        AccountVerificationLink = 1,
        AccountDeletedByAccountOwner = 2,
        AccountFutureUse2 = 3,
        AccountFutureUse3 = 4,
        AccountFutureUse4 = 5,
        AccountFutureUse5 = 6,
        AccountFutureUse6 = 7,

        ForgotPasswordLink = 8,
        ForgotPasswordFutureUse2 = 9,
        ForgotPasswordFutureUse3 = 10,

        ObjectSharedWithdefaultUser = 11,
        ObjectSharedWithExternalUser = 12,
        ObjectSharedFutureUse2 = 13,
        ObjectSharedFutureUse3 = 14,

        WelcomeEmailForNewUser = 15,
        UserDeletedBySelf = 16,
        UserDeletedByAccountOwner = 17,
        UsergetsDistanceGridByRoute = 18,
        WelcomeEmailForNewDelegatedUser = 19,
        WelcomeEmailForGuestUser = 20,
        UserEmailFutureUse4 = 21,
        UserEmailFutureUse5 = 22,

        ApplicationFutureUse1 = 23,
        ApplicationFutureUse2 = 24,
        ApplicationFutureUse3 = 25,
        ApplicationFutureUse4 = 26,
        ApplicationFutureUse5 = 27,

        RegistrationEmailforPaymentInformation = 28,
        RegistrationEmailforCampaign = 29,
        RegistrationEmailFutureUse2 =30,
        RegistrationEmailFutureUse3=31,
        RegistrationEmailFutureUse4 = 32,
        RegistrationEmailFutureUse5 = 33,

        //ResellerForgotPassword = 34,
        //ResellerRegistration = 35,
        //ResellerFutureUse2 = 36,
        //ResellerFurtueUse3 = 37,
        //ResellerFutureUse4 = 38,

        ResendVerificationLinkFromBlackpearl = 39,
        WelcomeEmailForEmployeesinBlackpearl = 40,
        ForgotPasswordOnBlackpearl = 41,
        EditTimesheetApprovalRequestOnBlackpearl = 42,
        SupervisorResponseOnEditedTimesheetOnBlackpearl = 43,
        InvoicePoOpenSend = 44,
        InvoicePoPaidSend = 45,
        InvoicePoUncollectableSend = 46,
        InvoiceNoPoOpenSend = 47,
        InvoiceNoPoPaidSend = 48,
        InvoiceNoPoUncollectableSend = 49,
        BlackpearlFutureUse8 = 50,
        BlackpearlFutureUse9 = 51,
        BlackpearlFutureUse10 = 52,

        StripeFailedPayment = 53,
        AccountDelinquent = 54,
        AccountUnpaid = 55,
        SubscriptionCancelledOnStripe = 56,
        PaymentSucceededOnStripe = 57,
        AccountRenewalReminderOnStripe = 58,
        WebhookFutureUse1 = 59,
        WebhookFutureUse2 = 60,
        WebhookFutureUse3 = 61,
        WebhookFutureUse4 = 62,
        WebhookFutureUse5 = 63,
        WebhookFutureUse6 = 64,
        WebhookFutureUse7 = 65,
        WebhookFutureUse8 = 66,

        UserInsertFailInternalEmail = 67,
        HangfireServiceStoppedInternalEmail = 68,
        FeedServiceStoppedInternalEmail = 69,
        SubscriptionErrorInternalEmail = 70,
        StripeSubscriptionFailInternalEmail=71,
		RealtimeStoppedInternalEmail = 72,
        InternalEmailFutureUse2 = 73,
        InternalEmailFutureUse3 = 74,
        InternalEmailFutureUse4 = 75,
        InternalEmailFutureUse5 = 76,
        InternalEmailFutureUse6 = 77,
        //GframKeyChangedToSecondary = 78

        SupportTicket = 79,

        ObjectSharedWithExternalUser_ProductVersion12 = 80,
        RegistrationEmailforPaymentInformation_ProductVersion28 = 81,
        SubscriptionErrorInternalEmail_ProductVersion70 = 82,
        PaymentSucceededOnStripe_ProductVersion57 = 83,
        AccountRenewalReminderOnStripe_ProductVersion58 =84,
        SupportTicket_ProductVersion79 = 85
    };
}