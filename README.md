![Fluent Email Logo](https://raw.githubusercontent.com/lukencode/FluentEmail/master/assets/fluentemail_logo_64x64.png "FluentEmail")

# Adds functionality/extensions using FluentEmail

See [FluentEmail (jcamp version)](https://github.com/jcamp-code/FluentEmail) for more information

| Library                                                                 | Description                                                         | Version                                                                                                                          |
| ----------------------------------------------------------------------- | ------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------- |
| [OLT.FluentEmail.Extensions](src/Extensions/OLT.FluentEmail.Extensions) | Provides general extensions for FluentEmail (Whitelist)             | [![Nuget](https://img.shields.io/nuget/v/OLT.FluentEmail.Extensions)](https://www.nuget.org/packages/OLT.FluentEmail.Extensions) |
| [OLT.FluentEmail.SendGrid](src/Senders/OLT.FluentEmail.SendGrid)        | Provides an Advanced SendGrid Sender with additional sender options | [![Nuget](https://img.shields.io/nuget/v/OLT.FluentEmail.SendGrid)](https://www.nuget.org/packages/OLT.FluentEmail.SendGrid)     |

## Advanced SendGrid Usage

### Dependency Injection

Configure the Advanced SendGridSender

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSendGridAdvancedSender("api-key-here", false);
}
```

Send an SendGrid Template with additional properties SendGrid provides

```csharp
public class EmailService {

   private IFluentEmail _fluentEmail;

   public EmailService(IFluentEmail fluentEmail) {
     _fluentEmail = fluentEmail;
   }

   public async Task Send() {
    var myTemplateJson = new MyTemplateClass();

    var result = await _fluentEmail
        .To("somebody@gmail.com")

        // NOTE: You do not have to provide template data, you can pass null
        .SendWithTemplateAsync("d-templateIdHere", myTemplateJson, opts =>
        {
            opts.DisableClickTracking = true;
            opts.DisableOpenTracking = true;
            opts.CustomArgs.Add("CustomArgName", "ArgValue");
            opts.UnsubscribeGroupId = 123456;
        });
   }
}
```

## Extensions

### Email Whitelist

#### Issue I'm trying to resolve

- I needed to send email templates (i.e., SendGrid) from a test environment. I could not use sandbox mode for SendGrid as I needed to see the template in my inbox.
- You can not test templates using an SMTP testing service like Mailtrap since the email must travel through the provider's ecosystem.
- Creating a whitelist concept would prevent unwanted emails from coming from a test system where a database was restored from production.

If the production mode is false, the extension will pull out all the email addresses not in the domain or email address safe list.

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
