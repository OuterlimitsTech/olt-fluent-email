namespace FluentEmail.Core
{
    public interface IEmailWhitelistConfig
    {
        IEnumerable<string> Domains { get; set; }
        IEnumerable<string> EmailAddresses { get; set; }
        bool ProductionMode { get; }

        IFluentEmail TrimEmails(IFluentEmail fluentEmail);
    }

}