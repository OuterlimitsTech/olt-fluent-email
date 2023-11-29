using FluentEmail.Core.Interfaces;
using FluentEmail.SendGrid;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FluentEmailSendGridBuilderExtensions
    {
        public static FluentEmailServicesBuilder AddSendGridAdvancedSender(this FluentEmailServicesBuilder builder, string apiKey, bool sandBoxMode = false)
        {
            builder.Services.TryAdd(ServiceDescriptor.Singleton<ISender>(_ => new SendGridSenderAdvanced(apiKey, sandBoxMode)));
            return builder;
        }
    }
}
