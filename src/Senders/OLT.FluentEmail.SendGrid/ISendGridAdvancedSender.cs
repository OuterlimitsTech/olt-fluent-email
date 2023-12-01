using FluentEmail.Core.Models;
using FluentEmail.Core;

namespace FluentEmail.SendGrid
{
    public interface ISendGridAdvancedSender : ISendGridSender
    {


        /// <summary>
        /// SendGrid specific extension method that allows you to use a template instead of a message body.
        /// For more information, see: https://sendgrid.com/docs/ui/sending-email/how-to-send-an-email-with-dynamic-transactional-templates/.
        /// </summary>
        /// <param name="email">Fluent email.</param>
        /// <param name="templateId">SendGrid template ID.</param>
        /// <param name="templateData">SendGrid template data.</param>
        /// <param name="options">Advanced SendGrid Options</param>
        /// <param name="token">Optional cancellation token.</param>
        /// <returns>A SendResponse object.</returns>
        Task<SendResponse> SendWithTemplateAsync(IFluentEmail email, string templateId, object templateData, Action<SendGridAdvancedOptions> options, CancellationToken? token = null);
    }
}