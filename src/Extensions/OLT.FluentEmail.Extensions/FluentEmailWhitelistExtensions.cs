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
}