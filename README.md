![alt text](https://raw.githubusercontent.com/lukencode/FluentEmail/master/assets/fluentemail_logo_64x64.png "FluentEmail")

# Extensions for FluentEmail (jcamp version)

- [OLT.FluentEmail.SendGrid](src/Senders/OLT.FluentEmail.SendGrid) - Provides an Advanced SendGrid Sender with additional sender options
- [OLT.FluentEmail.Extensions](src/Extensions/OLT.FluentEmail.Extensions) - Provides general extensions for FluentEmail (Whitelist)

## Advanced SendGrid Usage

### Dependency Injection

Configure the Advanced SendGridSender

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSendGridAdvancedSender("api-key-here", false);
}
```

Send an Email Template

```csharp
public class EmailService {

   private IFluentEmail _fluentEmail;

   public EmailService(IFluentEmail fluentEmail) {
     _fluentEmail = fluentEmail;
   }

   public async Task Send() {    
    var result = await _fluentEmail
        .To("somebody@gmail.com")                
        .SendWithTemplateAsync("d-templateIdHere", null, opts =>
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

- I needed the ability to send email templates (i.e., SendGrid) from a test environment.  I could not use sandbox mode for SendGrid as I needed to see the actual template in my inbox.
- You can not test templates using an SMTP testing service like mailtrap.io. as the email must travel through the provider's ecosystem.
- By creating a whitelist concept, the would prevent any unwanted emails coming from a test system where a database was restored from production.

I created an extension that will pull out all the email addresses that are not in the domain or email address safe list if the production mode is set to false.


```csharp
public class EmailService {

   private IFluentEmail _fluentEmail;

   public EmailService(IFluentEmail fluentEmail) {
     _fluentEmail = fluentEmail;
   }

   public async Task Send() {    
    var productionMode = false;  //This would be configured by a environment variable or configuration setting.  Setting to true will bypass the whitelist check

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

        //This must be called after all email addresses have been added, but before the Send method
        .WithWhitelist(new EmailWhitelistConfig(productionMode, testDomain, testEmailAddress))

        .SendWithTemplateAsync("d-templateIdHere", null);
   }
}
```
