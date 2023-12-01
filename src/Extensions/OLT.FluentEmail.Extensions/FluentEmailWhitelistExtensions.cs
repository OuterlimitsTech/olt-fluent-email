using FluentEmail.Core.Models;

namespace FluentEmail.Core
{
    public static class FluentEmailWhitelistExtensions
    {
        /// <summary>
        /// This must be called after all email addresses are added, but before the <seealso cref="IFluentEmail.Send(CancellationToken?)"/> or <seealso cref="IFluentEmail.SendAsync(CancellationToken?)"/>
        /// </summary>
        /// <param name="email"></param>
        /// <param name="productionMode">When true, then bypass whitelist</param>
        /// <param name="domains">Specific domains (i.e., mydomain.com) when <paramref name="productionMode"/> is false</param>
        /// <param name="emailAddresses">Specific email addresses (i.e., myemail@fakedomain.com) when <paramref name="productionMode"/> is false</param>
        /// <returns></returns>
        public static IFluentEmail WithWhitelist(this IFluentEmail email, bool productionMode, IEnumerable<string> domains, IEnumerable<string> emailAddresses)
        {
            return WithWhitelist(email, new EmailWhitelistConfig(productionMode, domains, emailAddresses));
        }

        /// <summary>
        /// This must be called after all email addresses are added, but before the <seealso cref="IFluentEmail.Send(CancellationToken?)"/> or <seealso cref="IFluentEmail.SendAsync(CancellationToken?)"/>
        /// </summary>
        /// <param name="email"></param>
        /// <param name="whitelist"></param>
        /// <returns></returns>
        public static IFluentEmail WithWhitelist(this IFluentEmail email, IEmailWhitelistConfig whitelist)
        {
            return whitelist.TrimEmails(email);
        }
    }

    public interface IEmailWhitelistConfig
    {
        IEnumerable<string> Domains { get; set; }
        IEnumerable<string> EmailAddresses { get; set; }
        bool ProductionMode { get; }

        IFluentEmail TrimEmails(IFluentEmail fluentEmail);
    }

    public class EmailWhitelistConfig : IEmailWhitelistConfig
    {

        /// <summary>
        /// <see cref="FluentEmailWhitelistExtensions.WithWhitelist(IFluentEmail, IEmailWhitelistConfig)"/> options
        /// </summary>
        /// <param name="productionMode">When true, then bypass whitelist</param>
        /// <param name="domains">Specific domains (i.e., mydomain.com) when <paramref name="productionMode"/> is false</param>
        /// <param name="emailAddresses">Specific email addresses (i.e., myemail@fakedomain.com) when <paramref name="productionMode"/> is false</param>
        public EmailWhitelistConfig(bool productionMode, IEnumerable<string> domains, IEnumerable<string> emailAddresses)
        {
            ProductionMode = productionMode;
            Domains = domains;
            EmailAddresses = emailAddresses;
        }

        /// <summary>
        /// Send to the entire domain when <see cref="ProductionMode"/> is false
        /// </summary>
        /// <remarks>
        /// <list type="table">
        ///   <item>
        ///     <term>Example</term>
        ///     <description>fake-domain.com</description>
        ///   </item>        
        /// </list>
        /// </remarks>
        public virtual IEnumerable<string> Domains { get; set; } = new List<string>();

        /// <summary>
        /// Specific email addresses when <see cref="ProductionMode"/> is false
        /// </summary>
        /// <remarks>
        /// <list type="table">
        ///   <item>
        ///     <term>Example</term>
        ///     <description>john@my-domain.com</description>
        ///   </item>        
        /// </list>
        /// </remarks>
        public virtual IEnumerable<string> EmailAddresses { get; set; } = new List<string>();

        /// <summary>
        /// When true, then bypass whitelist
        /// </summary>
        public virtual bool ProductionMode { get; }

        public virtual IFluentEmail TrimEmails(IFluentEmail fluentEmail)
        {
            if (this.ProductionMode == true)
            {
                return fluentEmail;
            }

            fluentEmail.Data.ToAddresses = TrimEmails(fluentEmail.Data.ToAddresses);
            fluentEmail.Data.CcAddresses = TrimEmails(fluentEmail.Data.CcAddresses);
            fluentEmail.Data.BccAddresses = TrimEmails(fluentEmail.Data.BccAddresses);

            return fluentEmail;
        }

        protected virtual List<Address> TrimEmails(IList<Address> addresses)
        {
            var values = new List<Address>();
            foreach (var address in addresses)
            {
                if (AllowSend(address))
                {
                    values.Add(address);
                }
            }
            return values;
        }

        protected virtual bool AllowSend(Address address)
        {
            return Domains.Any(p => address.EmailAddress.EndsWith(p, StringComparison.OrdinalIgnoreCase)) ||
                   EmailAddresses.Any(p => address.EmailAddress.Equals(p, StringComparison.OrdinalIgnoreCase));
        }
    }

}