namespace FluentEmail.SendGrid
{
    public class SendGridAdvancedOptions
    {
        public int? UnsubscribeGroupId { get; set; }
        public bool DisableClickTracking { get; set; }
        public bool DisableOpenTracking { get; set; }
        public Dictionary<string, string> CustomArgs { get; set; } = new Dictionary<string, string>();

    }
}