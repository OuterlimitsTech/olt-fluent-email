![fluent email logo](https://raw.githubusercontent.com/lukencode/FluentEmail/master/assets/fluentemail_logo_64x64.png "FluentEmail")

# This package add extensions for FluentEmail 

See [FluentEmail (jcamp version)](https://github.com/jcamp-code/FluentEmail) for more information


### Current Extensions

- **Whitelist** - _If the production mode is false, the extension will pull out all the email addresses not in the domain or email address safe list._

```csharp
public class EmailService {

   private IFluentEmail _fluentEmail;

   public EmailService(IFluentEmail fluentEmail) {
     _fluentEmail = fluentEmail;
   }

   public async Task Send() {

    // This would be configured by an environment variable or configuration setting.
    // Setting to true will bypass the whitelist check
    var productionMode = false;

    // Any email address ending with these domains will be sent when productionMode is false
    var testDomain = new List<string>
    {
        "mydomain.com",
        "another-domain.com"
    };

    var testEmailAddress = new List<string>
    {
        "safeemail@gmail.com",
        "anotheremail@hotmail.com"
    };

    var result = await _fluentEmail
        .To("somebody@gmail.com")
        .To("safeemail@gmail.com")

         // This must be called after all email addresses have been added, but before the Send method
        .WithWhitelist(new EmailWhitelistConfig(productionMode, testDomain, testEmailAddress))

        .SendWithTemplateAsync("d-templateIdHere", null);
   }
}
```
