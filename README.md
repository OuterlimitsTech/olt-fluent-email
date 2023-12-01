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


## Email Whitelist Extension 

### Issue I'm trying to resolve

- I needed the ability to send email templates (i.e., from SendGrid) from a test environment.  I could not use sandbox mode for SendGrid
- You can not test templates from provider using an SMTP testing service like mailtrap.io.  
- This prevents a backup copy of a production database from sending emails to actual people from a test environment

I created an extension that will pull out all the email addresses that are not in the domain or email address safe list.


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
