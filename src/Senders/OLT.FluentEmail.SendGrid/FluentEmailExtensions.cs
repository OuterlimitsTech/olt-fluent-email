using FluentEmail.Core.Models;
using FluentEmail.Core;
using SendGrid.Helpers.Mail;

namespace FluentEmail.SendGrid
{

    public static class FluentEmailExtensions
    {
        public static async Task<SendResponse> SendWithTemplateAsync(this IFluentEmail email, string templateId, object templateData, Action<SendGridAdvancedOptions> options)
        {
            var sendGridSender = email.Sender as ISendGridAdvancedSender;
            return await sendGridSender.SendWithTemplateAsync(email, templateId, templateData, options);
        }


        public static SendGridMessage SetOptions(this SendGridMessage mailMessage, SendGridAdvancedOptions options)
        {
            if (options.DisableClickTracking)
            {
                mailMessage.SetClickTracking(false, false);
            }

            if (options.DisableOpenTracking)
            {
                mailMessage.SetOpenTracking(false);
            }

            if (options.UnsubscribeGroupId.HasValue)
            {
                mailMessage.SetAsm(options.UnsubscribeGroupId.Value);
            }

            if (options.CustomArgs.Count > 0)
            {
                mailMessage.AddCustomArgs(options.CustomArgs);
            }

            return mailMessage;
        }
    }
}