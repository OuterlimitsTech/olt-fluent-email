using FluentEmail.Core;

namespace OLT.Core
{
    public static class OltEmailGeneralExtensions
    {
        public static IFluentEmail OltAppError(this IFluentEmail email, Exception exception, string appName, string environment)
        {
            return email
                .Subject($"[{appName}] APPLICATION ERROR in {environment} Environment occurred at {DateTimeOffset.Now:f}")
                .Body($"The following error occurred:{Environment.NewLine}{exception}");
        }
    }
}