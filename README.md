![alt text](https://raw.githubusercontent.com/lukencode/FluentEmail/master/assets/fluentemail_logo_64x64.png "FluentEmail")

# Extensions for FluentEmail (jcamp version)

This repo extends 


- [OLT.FluentEmail.SendGrid](src/Senders/OLT.FluentEmail.SendGrid) - Provides an Advanced SendGrid Sender with additional sender options

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
